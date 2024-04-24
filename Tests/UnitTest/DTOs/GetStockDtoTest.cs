using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using Xunit;

namespace Tests.DTOs
{
    public class GetStockDtoTest
    {
        [Fact]
        public void Initialization_ShouldSetDefaultValues()
        {
            // Arrange & Act
            var dto = new GetStockDto();

            // Assert
            Assert.Equal(string.Empty, dto.Symbol);
            Assert.Equal(string.Empty, dto.Image);
            Assert.Equal(string.Empty, dto.CompanyName);
            Assert.Equal(0m, dto.Price);
            Assert.Equal(0m, dto.Changes);
            Assert.Equal(0m, dto.LastDiv);
            Assert.Equal(0L, dto.MarketCap);
            Assert.Equal(string.Empty, dto.Currency);
            Assert.Equal(string.Empty, dto.Description);
            Assert.Equal(string.Empty, dto.Industry);
        }
    }
}