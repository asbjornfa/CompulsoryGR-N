using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;


namespace Service.Services;

public class PaperService : IPaper
{

    //Hvis vi vil implementere yderligere Validator metoder skal de ind her.

    private readonly MyDbContext _context;
    private readonly IValidator<RequestCreatePaperDTO> _CreatePaperValidator;

    public PaperService(MyDbContext context,
        IValidator<RequestCreatePaperDTO> CreatePaperValidator)
    {
        _context = context;
        _CreatePaperValidator = CreatePaperValidator;
    }

    //Der skal indhold i Metoderne.

    public async Task<ResponseCreatePaperDTO> CreatePaper(RequestCreatePaperDTO requestCreatePaperDto)
    {
        
        _CreatePaperValidator.ValidateAndThrow(requestCreatePaperDto);

        // Konverterer DTO til Paper model
        var paper = requestCreatePaperDto.ToPaper();

        // Tilføj Paper til databasen
        _context.Papers.Add(paper);
        await _context.SaveChangesAsync();

        // Returner Response DTO
        return new ResponseCreatePaperDTO()
        {
            Id = paper.Id,
            Discontinued = paper.Discontinued,
            Name = paper.Name,
            Price = paper.Price,
            Stock = paper.Stock
        };
    }
    


    public async Task<List<Paper>> GetAllPapers()
    {
        // Henter alle ikke-slettede papers
        return await _context.Papers.Where(p => !p.Discontinued).ToListAsync();
    }

    public async Task<Paper> GetPaperById(int id)
    {
        return await _context.Papers.FindAsync(id);
    }

    public async Task DeletePaper(int id)
    {
        var paper = await _context.Papers.FindAsync(id);
        if (paper != null)
        {
            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();
        }
        {
            
        }
    }
}