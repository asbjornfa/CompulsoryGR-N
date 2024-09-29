using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;

namespace Service.Services;

public class OrderEntryService : IOrderEntry
{
    private readonly MyDbContext _context;
    private readonly IValidator<RequestCreateOrderEntryDTO> _createOrderEntryValidator;

    public OrderEntryService(MyDbContext context, IValidator<RequestCreateOrderEntryDTO> createOrderEntryValidator)
    {
        _context = context;
        _createOrderEntryValidator = createOrderEntryValidator;
    }

    public async Task<ResponseCreateOrderEntryDTO> CreateOrderEntry(RequestCreateOrderEntryDTO request)
    {
        _createOrderEntryValidator.ValidateAndThrow(request);

        var orderEntry = request.ToOrderEntry();

        _context.OrderEntries.Add(orderEntry);
        await _context.SaveChangesAsync();

        return new ResponseCreateOrderEntryDTO
        {
            Id = orderEntry.Id,
            Quantity = orderEntry.Quantity,
            ProductId = orderEntry.ProductId,
            OrderId = orderEntry.OrderId
        };
    }

    public async Task<List<ResponseCreateOrderEntryDTO>> GetAllOrderEntries()
    {
        return await _context.OrderEntries.Select(o => new ResponseCreateOrderEntryDTO
        {
            Id = o.Id,
            Quantity = o.Quantity,
            ProductId = o.ProductId,
            OrderId = o.OrderId
        })
            .ToListAsync();
    }



    public async Task<OrderEntry> GetOrderEntryById(int id)
    {
        return await _context.OrderEntries.FindAsync(id);
    }
}
