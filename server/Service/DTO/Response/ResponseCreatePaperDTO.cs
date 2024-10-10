using DataAccess.Models;

namespace Service.DTO.Response;

public class ResponseCreatePaperDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
   
    public List<ResponsePropertyDTO>? Properties { get; set; }
}