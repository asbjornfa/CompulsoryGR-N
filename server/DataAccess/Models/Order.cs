namespace DefaultNamespace;

public class Order
{
    public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
    
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    
        public List<OrderEntry> OrderEntries { get; set; }
}