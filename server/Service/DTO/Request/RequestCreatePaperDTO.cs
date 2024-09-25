
namespace Service.DTO.Request;

public class RequestCreatePaperDTO
{
    public string Name { get; set; } = null!;
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    
}