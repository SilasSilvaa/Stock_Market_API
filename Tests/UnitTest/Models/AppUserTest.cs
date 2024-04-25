using api.Models;
using Microsoft.AspNetCore.Identity;

namespace Tests.Models
{
    public class AppUserTest
    {
    [Fact]
    public void Inherits_IdentityUser()
    {
        // Arrange
        var appUser = new AppUser();

        // Assert
        Assert.IsAssignableFrom<IdentityUser>(appUser);
    }

    [Fact]
    public void HasPortfolioProperty()
    {
        // Arrange
        var appUser = new AppUser();

        // Assert
        Assert.NotNull(appUser.Portfolio);
        Assert.IsType<List<StockPortfolio>>(appUser.Portfolio);
    }

    [Fact]
    public void PortfolioProperty_DefaultValue_IsEmptyList()
    {
        // Arrange
        var appUser = new AppUser();

        // Assert
        Assert.Empty(appUser.Portfolio);
    }
    }
}