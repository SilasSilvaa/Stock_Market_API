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
    public void HasPortifolioProperty()
    {
        // Arrange
        var appUser = new AppUser();

        // Assert
        Assert.NotNull(appUser.Portifolio);
        Assert.IsType<List<StockPortifolio>>(appUser.Portifolio);
    }

    [Fact]
    public void PortifolioProperty_DefaultValue_IsEmptyList()
    {
        // Arrange
        var appUser = new AppUser();

        // Assert
        Assert.Empty(appUser.Portifolio);
    }
    }
}