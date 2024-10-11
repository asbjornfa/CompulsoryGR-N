using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service.DTO.Request;
using Service.Services;

namespace Tests.UnitTests;

public class PropertiesTest
{
     private readonly MyDbContext _context;
        private readonly Mock<IValidator<RequestCreatePropertyDTO>> _createPropertyValidatorMock;
        private readonly PropertiesService _propertiesService;

        public PropertiesTest()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // In-memory database
                .Options;
            
            _context = new MyDbContext(options);
            _createPropertyValidatorMock = new Mock<IValidator<RequestCreatePropertyDTO>>();
            _propertiesService = new PropertiesService(_context, _createPropertyValidatorMock.Object);
        }

        [Fact]
        public async Task CreateProperty_ValidProperty_AddsPropertyToDatabase()
        {
            // Arrange
            var request = new RequestCreatePropertyDTO
            {
                PropertyName = "Test Property"
            };

            // Mock for succesfuld validering
            _createPropertyValidatorMock.Setup(v => v.Validate(It.IsAny<RequestCreatePropertyDTO>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _propertiesService.CreateProperty(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.PropertyName, result.PropertyName);

            // Tjek om ejendommen blev gemt i in-memory databasen
            var savedProperty = await _context.Properties.FirstOrDefaultAsync(p => p.PropertyName == request.PropertyName);
            Assert.NotNull(savedProperty);
            Assert.Equal("Test Property", savedProperty.PropertyName);
        }
        
    }
