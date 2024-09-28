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

    
    public async Task<ResponseCreateOrderDTO> CreateOrder(RequestCreateOrderDTO requestCreateOrderDto)
    {
        _createOrderValidator.ValidateAndThrow(requestCreateOrderDto);

        var order = requestCreateOrderDto.ToCustomer();

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return new ResponseCreateOrderDTO()
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId
        };
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