using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IPaperRepository
{
    IEnumerable<Paper> GetAllPapers();
    Paper GetPaperById(int id); 
    void CreatePaper(Paper paper); 
    void UpdatePaper(Paper paper); 
    void DeletePaper(int id); 
    IEnumerable<Paper> GetDiscontinuedPapers(); 
    IEnumerable<Paper> SearchPapers(string searchTerm); 
    IEnumerable<Paper> GetPapersByProperty(string property); 
}