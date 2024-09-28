using System.Data;
using DataAccess.Models;
namespace Service.DTO.Request;

public class RequestCreateOrderDTO
{
    public DateTime OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public double TotalAmount { get; set; }

    public int? CustomerId { get; set; }
    
    
    public Order ToCustomer()
    {
        return new Order()
        {
            OrderDate = OrderDate,
            DeliveryDate = DeliveryDate,
            Status = Status,
            TotalAmount = TotalAmount,
            CustomerId = CustomerId
        };
    }

}