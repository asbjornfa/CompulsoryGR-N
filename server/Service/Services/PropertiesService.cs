using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;


namespace Service.Services;

public class PropertiesService : IProperties
{

    private readonly MyDbContext _context;
    private readonly IValidator<RequestCreatePropertyDTO> _CreatePropertyValidator;

    public PropertiesService(MyDbContext context, 
        IValidator<RequestCreatePropertyDTO> CreatePropertyValidator)
    {
        _context = context;
        _CreatePropertyValidator = CreatePropertyValidator;
    }
    
    public async Task<ResponsePropertyDTO> CreateProperty(RequestCreatePropertyDTO requestCreatePropertyDto)
    {
        _CreatePropertyValidator.ValidateAndThrow(requestCreatePropertyDto);

        var property = requestCreatePropertyDto.ToProperty();
        
        _context.Properties.Add(property);
        
        await _context.SaveChangesAsync();

        return new ResponsePropertyDTO()
        {
            Id = property.Id,
            PropertyName = property.PropertyName
        };
    }

    public async Task<List<ResponsePropertyDTO>> GetAllProperties()
    {
        return await _context.Properties.Select(pr => new ResponsePropertyDTO
            {
                Id = pr.Id,
                PropertyName = pr.PropertyName
            })
            .ToListAsync();
    }

    public async Task<Properties> GetPropertyById(int id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task<ResponsePropertyDTO> UpdateProperty(int id, RequestCreatePropertyDTO request)
    {
        var existingProperties = await _context.Properties.FindAsync(id);
        if (existingProperties == null)
        {
            throw new Exception("Property not found");
        }
        
        existingProperties.PropertyName = request.PropertyName;
        
        _context.Properties.Update(existingProperties);
        await _context.SaveChangesAsync();

        return new ResponsePropertyDTO
        {
            Id = existingProperties.Id,
            PropertyName = existingProperties.PropertyName
        };

    }

    public async Task DeleteProperty(int id)
    {
        var properties = await _context.Properties.FindAsync(id);
        if (_context != null)
        {
            _context.Properties.Remove(properties);
            await _context.SaveChangesAsync();
        }
    }
}