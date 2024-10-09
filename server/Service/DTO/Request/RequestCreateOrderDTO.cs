using System.Data;
using DataAccess.Models;
namespace Service.DTO.Request;

public class RequestCreateOrderDTO
{
    public List<RequestCreateOrderEntryDTO> Dtos { get; set; } = new List<RequestCreateOrderEntryDTO>();
    public DateTime OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public double TotalAmount { get; set; }

    public int? CustomerId { get; set; }
    
    
    public Order ToOrder(List<OrderEntry> entries)
    {
        return new Order()
        {
            OrderEntries = entries,
            OrderDate = OrderDate,
            DeliveryDate = DeliveryDate,
            Status = Status,
            TotalAmount = TotalAmount,
            CustomerId = CustomerId
        };
    }

}