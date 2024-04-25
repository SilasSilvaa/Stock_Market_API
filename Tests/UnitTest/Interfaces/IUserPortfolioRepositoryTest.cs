using api.Interfaces;
using api.Models;

namespace Tests.Interfaces
{
    public class IUserPortfolioRepositoryTest
    {
        [Fact]
        public void ShouldHaveCorrectMethods()
        {
            // Arrange
            var userRepositoryInterface = typeof(IUserPortfolioRepository);

            // Act
            var methods = userRepositoryInterface.GetMethods();

            // Assert
            Assert.Contains(methods, method => method.Name == "GetUserPortfolioAsync" 
            && method.ReturnType == typeof(Task<List<StockDB>>) 
            && method.GetParameters().Length == 1 
            && method.GetParameters()[0].ParameterType == typeof(AppUser));
            
            Assert.Contains(methods, method => method.Name == "CreateAsync" 
            && method.ReturnType == typeof(Task<StockPortfolio>) 
            && method.GetParameters().Length == 1 
            && method.GetParameters()[0].ParameterType == typeof(StockPortfolio));
            
            Assert.Contains(methods, method => method.Name == "DeleteAsync" 
            && method.ReturnType == typeof(Task<StockPortfolio>) 
            && method.GetParameters().Length == 2 
            && method.GetParameters()[0].ParameterType == typeof(AppUser) 
            && method.GetParameters()[1].ParameterType == typeof(int));
            
            Assert.Contains(methods, method => method.Name == "GetStockById" 
            && method.ReturnType == typeof(Task<StockDB?>) 
            && method.GetParameters().Length == 2 
            && method.GetParameters()[0].ParameterType == typeof(string) 
            && method.GetParameters()[1].ParameterType == typeof(int));
        }
    }
}