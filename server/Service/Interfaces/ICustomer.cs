using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Service.Interfaces;

public interface ICustomer
{
    Task<ResponseCustomerDTO> CreateCustomer(RequestCreateCustomerDTO requestCreateCustomerDto);
    
    Task<List<ResponseCustomerDTO>> GetAllCustomers();
    
    Task<Customer> GetCustomerById(int id);
    
    Task<ResponseCustomerDTO> UpdateCustomer(int id, RequestCreateCustomerDTO requestCreateCustomerDto);
    
    Task DeleteCustomer(int id);
    
}