using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;



namespace Service.Interfaces;

public interface IPaper
{
    Task<ResponseCreatePaperDTO> CreatePaper(RequestCreatePaperDTO requestCreatePaperDto);
    
    Task<List<Paper>> GetAllPapers();
    
    Task<Paper> GetPaperById(int id);
    Task DeletePaper(int id);

}