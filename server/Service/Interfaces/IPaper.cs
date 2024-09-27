using DataAccess.Models;
using Service.DTO.Request;
using Service.DTO.Response;



namespace Service.Interfaces;

public interface IPaper
{
    Task<ResponseCreatePaperDTO> CreatePaper(RequestCreatePaperDTO requestCreatePaperDto);
    
    Task<List<ResponseCreatePaperDTO>> GetAllPapers();
    
    Task<Paper> GetPaperById(int id);
    Task<ResponseCreatePaperDTO> UpdatePaper(int id, RequestCreatePaperDTO requestCreatePaperDto);
    Task DeletePaper(int id);

}