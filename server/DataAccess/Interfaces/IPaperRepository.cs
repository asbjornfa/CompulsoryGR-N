namespace DefaultNamespace;

public class IPaperRepository
{
    // Get all paper products
        public List<Paper> GetAllPapers();
    
        // Get paper by id
        public Paper GetPaperById(int paperId);
    
        // Add new paper product
        public Paper CreatePaper(Paper paper);
    
        // Update existing paper product (e.g., stock, price, etc.)
        public void UpdatePaper(Paper paper);
    
        // Delete paper product
        public void DeletePaper(int paperId);
    
        // Get all discontinued paper products
        public List<Paper> GetDiscontinuedPapers();
    
        // Search for papers by name or description (full-text search)
        public List<Paper> SearchPapers(string searchTerm);
    
        // Get paper products by custom property (e.g., water-resistant, sturdy)
        public List<Paper> GetPapersByProperty(string propertyName);
}