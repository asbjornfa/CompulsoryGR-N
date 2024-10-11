using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service.DTO.Request;
using Service.Services;

namespace Tests.UnitTests;



    public class OrderTest
    {
        private readonly MyDbContext _context;
        private readonly Mock<IValidator<RequestCreateOrderDTO>> _createOrderValidatorMock;
        private readonly OrderService _orderService;

        public OrderTest()
        {
            // In-memory database
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new MyDbContext(options);
            _createOrderValidatorMock = new Mock<IValidator<RequestCreateOrderDTO>>();
            _orderService = new OrderService(_context, _createOrderValidatorMock.Object);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsCorrectOrderHistory()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Status = "Completed", OrderDate = DateTime.UtcNow, TotalAmount = 100.0 },
                new Order { Id = 2, CustomerId = 2, Status = "Pending", OrderDate = DateTime.UtcNow, TotalAmount = 50.0 }
            };

            // Tilføj test-ordrer til den in-memory database
            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderService.GetAllOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Der bør være to ordrer
            Assert.Equal(orders[0].Id, result[0].Id); // Tjekker første ordres ID
            Assert.Equal(orders[1].Status, result[1].Status); // Tjekker status for anden ordre
            Assert.Equal(orders[1].TotalAmount, result[1].TotalAmount); // Tjekker beløbet på anden ordre
        }
        
    }
    
