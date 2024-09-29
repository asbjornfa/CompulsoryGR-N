using DataAccess.Models;

namespace Service.DTO.Request;

public class RequestCreateOrderEntryDTO
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }

    public OrderEntry ToOrderEntry()
    {
        return new OrderEntry()
        {
            Quantity = Quantity,
            ProductId = ProductId,
            OrderId = OrderId
        };
    }
}