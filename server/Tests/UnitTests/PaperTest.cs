using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Service.Services;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Tests.UnitTests
{
    public class PaperTests
    {
        private readonly MyDbContext _context;
        private readonly Mock<IValidator<RequestCreatePaperDTO>> _createPaperValidatorMock;
        private readonly PaperService _paperService;

        public PaperTests()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // In-memory database
                .Options;

            _context = new MyDbContext(options);
            _createPaperValidatorMock = new Mock<IValidator<RequestCreatePaperDTO>>();
            _paperService = new PaperService(_context, _createPaperValidatorMock.Object);
        }

        [Fact]
        public async Task CreatePaper_ValidPaper_AddsPaperToDatabase()
        {
            // Arrange
            var requestCreatePaperDTO = new RequestCreatePaperDTO
            {
                Name = "Sample Paper",
                Discontinued = false,
                Stock = 100,
                Price = 50.0,
                PropertyIds = new List<int>()
            };

            // Mock successful validation
            _createPaperValidatorMock.Setup(v => v.Validate(It.IsAny<RequestCreatePaperDTO>()))
                .Returns(new ValidationResult());

            // Act
            var result = await _paperService.CreatePaper(requestCreatePaperDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(requestCreatePaperDTO.Name, result.Name);
            Assert.Equal(requestCreatePaperDTO.Stock, result.Stock);
            Assert.Equal(requestCreatePaperDTO.Price, result.Price);

            // Verify that the paper was added to the in-memory database
            var savedPaper = await _context.Papers.FirstOrDefaultAsync(p => p.Name == requestCreatePaperDTO.Name);
            Assert.NotNull(savedPaper);
        }

        [Fact]
        public async Task GetAllPapers_ReturnsCorrectData()
        {
            // Arrange
            var papers = new List<Paper>
            {
                new Paper { Id = 1, Name = "Paper 1", Price = 10, Stock = 50, Discontinued = false },
                new Paper { Id = 2, Name = "Paper 2", Price = 20, Stock = 100, Discontinued = true }
            };

            await _context.Papers.AddRangeAsync(papers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _paperService.GetAllPapers();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Paper 1", result[0].Name);
            Assert.Equal(10, result[0].Price);
            Assert.Equal("Paper 2", result[1].Name);
            Assert.True(result[1].Discontinued);
        }

        [Fact]
        public async Task UpdatePaper_UpdatesExistingPaper()
        {
            // Arrange
            var paper = new Paper { Id = 1, Name = "Old Paper", Price = 10, Stock = 50, Discontinued = false };
            await _context.Papers.AddAsync(paper);
            await _context.SaveChangesAsync();

            var updateRequest = new RequestCreatePaperDTO
            {
                Name = "Updated Paper",
                Price = 15,
                Stock = 60,
                Discontinued = true,
                PropertyIds = new List<int>() // Assuming no properties are updated in this test
            };

            // Act
            var result = await _paperService.UpdatePaper(paper.Id, updateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Paper", result.Name);
            Assert.Equal(15, result.Price);
            Assert.Equal(60, result.Stock);
            Assert.True(result.Discontinued);

            // Verify that the paper was updated in the database
            var updatedPaper = await _context.Papers.FindAsync(paper.Id);
            Assert.NotNull(updatedPaper);
            Assert.Equal("Updated Paper", updatedPaper.Name);
        }

        [Fact]
        public async Task DeletePaper_RemovesPaperFromDatabase()
        {
            // Arrange
            var paper = new Paper { Id = 1, Name = "Test Paper", Price = 10, Stock = 50, Discontinued = false };
            await _context.Papers.AddAsync(paper);
            await _context.SaveChangesAsync();

            // Act
            await _paperService.DeletePaper(paper.Id);

            // Assert
            var deletedPaper = await _context.Papers.FindAsync(paper.Id);
            Assert.Null(deletedPaper); // Verify that paper was removed
        }
    }
}
