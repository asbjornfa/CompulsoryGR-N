using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;

namespace Service.Services;

public class CustomerService : ICustomer
{
    
    private readonly MyDbContext _context;
    private readonly IValidator<RequestCreateCustomerDTO> _CreateCustomerValidator;

    public CustomerService(MyDbContext context, IValidator<RequestCreateCustomerDTO> CreateCustomerValidator)
    {
        _context = context;
        _CreateCustomerValidator = CreateCustomerValidator;
    }    
    
    public async Task<ResponseCustomerDTO> CreateCustomer(RequestCreateCustomerDTO requestCreateCustomerDto)
    {
        _CreateCustomerValidator.ValidateAndThrow(requestCreateCustomerDto);

        var customer = requestCreateCustomerDto.ToCustomer();
        
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new ResponseCustomerDTO()
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email
        };
    }

    public async Task<List<ResponseCustomerDTO>> GetAllCustomers()
    {
        return await _context.Customers.Select(c => new ResponseCustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email
            })
            .ToListAsync();
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<ResponseCustomerDTO> UpdateCustomer(int id, RequestCreateCustomerDTO request)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);
        if (existingCustomer == null)
        {
            throw new Exception("Customer not found");
        }
        
        existingCustomer.Name = request.Name;
        existingCustomer.Address = request.Address;
        existingCustomer.Phone = request.Phone;
        existingCustomer.Email = request.Email;
        
        _context.Customers.Update(existingCustomer);
        await _context.SaveChangesAsync();

        return new ResponseCustomerDTO
        {
            Id = existingCustomer.Id,
            Name = existingCustomer.Name,
            Address = existingCustomer.Address,
            Phone = existingCustomer.Phone,
            Email = existingCustomer.Email
        };
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        
    }
}