namespace DefaultNamespace;

public class IRepository
{
    public interface IRepository<T> where T : class
    {
        // Hent alle poster
        List<T> GetAll();

        // Hent en post baseret på ID
        T GetById(int id);

        // Opret en ny post
        T Create(T entity);

        // Opdater en eksisterende post
        void Update(T entity);

        // Slet en post baseret på ID
        void Delete(int id);
    }
    
}