
using DataAccess.Models;

namespace Service.DTO.Request;

public class RequestCreatePaperDTO
{
    public string Name { get; set; } = null!;
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    
    public List<int>? PropertyIds { get; set; }


    public Paper ToPaper(IEnumerable<Properties> selectedProperties)
    {
        return new Paper
        {
            Name = Name,
            Discontinued = Discontinued,
            Stock = Stock,
            Price = Price,
            Properties = selectedProperties.ToList()
        };
    }
    
}

