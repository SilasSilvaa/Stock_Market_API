using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Interfaces;
using Xunit;

namespace Tests.Interfaces
{
    public class IFinantialModPreparingServiceTest
    {
        [Fact]
        public void ShouldHaveCorrectMethods()
        {
            //Arrange
            var finantialModPreparingService = typeof(IFinantialModPreparingService);
            
            // Act
            var method = finantialModPreparingService.GetMethod("FindStockBySymbolAsync");
        
            //Assert
            Assert.NotNull(method);
            Assert.Equal(typeof(Task<GetStockDto?>), method.ReturnType);
            Assert.Single(method.GetParameters());
            Assert.Equal(typeof(string), method.GetParameters()[0].ParameterType);
        }
    }
}