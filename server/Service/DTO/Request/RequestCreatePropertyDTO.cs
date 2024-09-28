using DataAccess.Models;

namespace Service.DTO.Request;

public partial class RequestCreatePropertyDTO
{
    public string PropertyName { get; set; }

    public Properties ToProperty()
    {
        return new Properties
        {
            PropertyName = PropertyName,
        };
    }
    
}