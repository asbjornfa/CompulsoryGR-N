namespace DefaultNamespace;

public class paper
{
    public int Id { get; set; }
        public string Name { get; set; }
        public bool Discontinued { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    
        public List<PaperProperty> PaperProperties { get; set; }
}