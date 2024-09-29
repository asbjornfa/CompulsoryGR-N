using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Service.Interfaces;

public interface IOrderEntry
{
    Task<ResponseCreateOrderEntryDTO> CreateOrderEntry(RequestCreateOrderEntryDTO request);
    Task<List<ResponseCreateOrderEntryDTO>> GetAllOrderEntries();
    Task<OrderEntry> GetOrderEntryById(int id);
}