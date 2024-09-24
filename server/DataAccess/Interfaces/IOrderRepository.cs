using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IOrderReposiory    
{
  
        // Get all orders
        public List<Order> GetAllOrders();
    
        // Get orders by customer id
        public List<Order> GetOrdersByCustomerId(int customerId);
    
        // Create a new order
        public Order CreateOrder(Order order);
    
        // Get order by id
        public Order GetOrderById(int orderId);
    
        // Update an order
        public void UpdateOrder(Order order);
    
        // Get all orders including their products (order entries)
        public List<Order> GetAllOrdersWithEntries();
    
        // Get total number of orders placed
        public int GetTotalNumberOfOrders();
    
        // Get the order with the highest total amount
        public Order GetOrderWithHighestTotalAmount();
        
        // Get all orders with a specific status (e.g., pending, completed)
        public List<Order> GetOrdersByStatus(string status);
}