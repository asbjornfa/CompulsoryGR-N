using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Service.Interfaces;

public interface IOrder
{
    Task<Order> CreateOrder(RequestCreateOrderDTO requestCreateOrderDto);
    Task<List<ResponseCreateOrderDTO>> GetAllOrders();
    Task<Order> GetOrderById(int id);
    Task<ResponseCreateOrderDTO> UpdateOrder(int id, RequestCreateOrderDTO requestCreateOrderDto);
    
}