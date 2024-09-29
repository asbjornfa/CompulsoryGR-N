namespace Service.DTO.Response;

public class ResponseCreateOrderEntryDTO
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
}