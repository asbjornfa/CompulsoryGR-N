using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;


namespace Service.Services;

public class PaperService : IPaper
{
    
    //Hvis vi vil implementere yderligere Validator metoder skal de ind her.
    
    private readonly MyDbContext _context;
    private readonly IValidator<RequestCreatePaperDTO> _CreatePaperValidator;

    public PaperService(MyDbContext context, IValidator<RequestCreatePaperDTO> CreatePaperValidator)
    {
        _context = context;
        _CreatePaperValidator = CreatePaperValidator;
    }
    
    //Der skal indhold i Metoderne.
    
    public Task<ResponseCreatePaperDTO> CreatePaper(RequestCreatePaperDTO requestCreatePaperDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<Paper>> GetAllPapers()
    {
        throw new NotImplementedException();
    }

    public Task<Paper> GetPaperById(int id)
    {
        throw new NotImplementedException();
    }
}