using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;

namespace Service.Services;

public class OrderService : IOrder
{
    
        private readonly MyDbContext _context;
        private readonly IValidator<RequestCreateOrderDTO> _createOrderValidator;

        public OrderService(MyDbContext context, IValidator<RequestCreateOrderDTO> createOrderValidator)
        {
            _context = context;
            _createOrderValidator = createOrderValidator;
        }

        public async Task<Order> CreateOrder(RequestCreateOrderDTO requestCreateOrderDto)
        {
            // Validate request
            _createOrderValidator.ValidateAndThrow(requestCreateOrderDto);

            // Create order entries
            var entries = requestCreateOrderDto.Dtos.Select(dto => new OrderEntry()
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            }).ToList();

            // Create a new order
            var order = requestCreateOrderDto.ToOrder(entries);
            order.CustomerId = requestCreateOrderDto.CustomerId ?? 1; // Set customer ID

            // Update paper stock and calculate total order amount
            double totalOrderAmount = 0;

            foreach (var orderEntry in order.OrderEntries)
            {
                var paper = await _context.Papers.FindAsync(orderEntry.ProductId);
                if (paper == null)
                {
                    throw new Exception($"Product with ID {orderEntry.ProductId} not found.");
                }

                if (paper.Stock < orderEntry.Quantity)
                {
                    throw new Exception($"Insufficient stock for product ID {paper.Id}. Available: {paper.Stock}, Requested: {orderEntry.Quantity}.");
                }

                totalOrderAmount += paper.Price * orderEntry.Quantity;
                paper.Stock -= orderEntry.Quantity;
                _context.Papers.Update(paper);
            }

            order.TotalAmount = totalOrderAmount;

            // Add order to the context
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            order.OrderEntries = null; // Hide order entries in the response
            return order;
        }


    public async Task<List<ResponseCreateOrderDTO>> GetAllOrders()
    {
        return await _context.Orders.Select(o => new ResponseCreateOrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                CustomerId = o.CustomerId,
                Status = o.Status,
                TotalAmount = o.TotalAmount
            })
            .ToListAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<ResponseCreateOrderDTO> UpdateOrder(int id, RequestCreateOrderDTO request)
    {
        var existingOrder = await _context.Orders.FindAsync(id);
        if (existingOrder == null)
        {
            throw new Exception("order not found");
        }
        existingOrder.DeliveryDate = request.DeliveryDate;
        existingOrder.Status = request.Status;

        _context.Orders.Update(existingOrder);
        await _context.SaveChangesAsync();

        return new ResponseCreateOrderDTO()
        {
            Id = existingOrder.Id,
            DeliveryDate = existingOrder.DeliveryDate,
            Status = existingOrder.Status
        };
    }
}