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



    public async Task<List<ResponseCreatePaperDTO>> GetAllPapers()
    {
        return await _context.Papers.Select(p => new ResponseCreatePaperDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Discontinued = p.Discontinued
            })
            .ToListAsync();
    }


public async Task<Paper> GetPaperById(int id)
    {
        return await _context.Papers.FindAsync(id);
    }

    public async Task<ResponseCreatePaperDTO> UpdatePaper(int id, RequestCreatePaperDTO request)
    {
        var existingPaper = await _context.Papers.FindAsync(id);
        if (existingPaper == null)
        {
            throw new Exception("paper not found");
        }

        //updater papirets værdier 
        existingPaper.Name = request.Name;
        existingPaper.Stock = request.Stock;
        existingPaper.Price = request.Price;
        existingPaper.Discontinued = request.Discontinued;

        //ændringer gemmes
        _context.Papers.Update(existingPaper);
        await _context.SaveChangesAsync();
        
        return new ResponseCreatePaperDTO
        {
            Id = existingPaper.Id,
            Name = existingPaper.Name,
            Stock = existingPaper.Stock,
            Price = existingPaper.Price,
            Discontinued = existingPaper.Discontinued
        };
       
    }
    
    public async Task DeletePaper(int id)
    {
        var paper = await _context.Papers.FindAsync(id);
        if (paper != null)
        {
            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();
        }
    }
}