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
    
    public OrderService(MyDbContext context, IValidator<RequestCreateOrderDTO> CreateOrderValidator)
    {
        _context = context;
        _createOrderValidator = CreateOrderValidator;
    }    

    
    public async Task<Order> CreateOrder(RequestCreateOrderDTO requestCreateOrderDto)
    {
        // Validate request
        _createOrderValidator.ValidateAndThrow(requestCreateOrderDto);

        var entries = requestCreateOrderDto.Dtos.Select(dto => new OrderEntry()
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        }).ToList();
        
            // Create a new order
            var order = requestCreateOrderDto.ToOrder(entries);
            order.CustomerId = 1; // Set customer ID

            // double totalOrderAmount = 0;
            //
            // foreach (var orderEntry in order.OrderEntries)
            // {
            //     var paper = await _context.Papers.FindAsync(orderEntry.ProductId);
            //     if (paper == null)
            //     {
            //         throw new Exception("orderEntry.ProductId");
            //     }
            //
            //     if (paper.Stock < orderEntry.Quantity)
            //     {
            //         throw new Exception("paper.Id, paper.Stock, orderEntry.Quantity");
            //     }
            //
            //     totalOrderAmount += paper.Price * orderEntry.Quantity;
            //     paper.Stock -= orderEntry.Quantity;
            //     _context.Papers.Update(paper);
            // }

            //order.TotalAmount = totalOrderAmount;

            // Auto-generate IDs
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Commit transaction
            // await transaction.CommitAsync();

            order.OrderEntries = null;
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