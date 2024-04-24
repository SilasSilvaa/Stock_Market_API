using System.ComponentModel.DataAnnotations;
using api.DTOs.Stock;

namespace Tests.DTOs
{
    public class CreateStockRequestDtoTest
    {
        [Fact]
        public void ValidDto_ReturnsEmptyResult()
        {
            // Arrange
            var dto = new CreateStockRequestDto
            {
                Symbol = "AAPL",
                Image = "image.jpg",
                CompanyName = "Apple Inc.",
                Price = 150.50m,
                Changes = 2.5m,
                LastDiv = 1.5m,
                MarketCap = 2000000000,
                Currency = "USD",
                Description = "Technology company",
                Industry = "Technology"
            };

            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

    }
}