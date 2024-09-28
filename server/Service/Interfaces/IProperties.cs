using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;
using Property = Microsoft.EntityFrameworkCore.Metadata.Internal.Property;

namespace Service.Interfaces;

public interface IProperties
{
    Task<ResponsePropertyDTO> CreateProperty(RequestCreatePropertyDTO requestCreatePropertyDto);
    
    Task<List<ResponsePropertyDTO>> GetAllProperties();
    
    Task<Properties> GetPropertyById(int id);
    
    Task<ResponsePropertyDTO> UpdateProperty(int id, RequestCreatePropertyDTO requestCreatePropertyDto);
    
    Task DeleteProperty(int id);

}