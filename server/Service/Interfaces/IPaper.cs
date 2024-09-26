
using Service.DTO.Request;
using Service.DTO.Response;
using DataAccess.Models;


namespace Service.Interfaces;

public interface IPaper
{
    Task<ResponseCreatePaperDTO> CreatePaper(RequestCreatePaperDTO requestCreatePaperDto);
    
    Task<List<Paper>> GetAllPapers();
    
    Task<Paper> GetPaperById(int id);
    
}