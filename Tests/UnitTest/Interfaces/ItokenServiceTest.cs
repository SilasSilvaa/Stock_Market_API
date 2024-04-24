using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Xunit;

namespace Tests.Interfaces
{
    public class ItokenServiceTest
    {
        [Fact]
        public void ShouldHaveCreateTokenMethod()
        {
            // Arrange
            var tokenServiceInterface = typeof(ITokenService);

            // Act
            var createTokenMethod = tokenServiceInterface.GetMethod("CreateToken");

            // Assert
            Assert.NotNull(createTokenMethod);
            Assert.Equal(typeof(string), createTokenMethod.ReturnType);
            Assert.Single(createTokenMethod.GetParameters());
            Assert.Equal(typeof(AppUser), createTokenMethod.GetParameters()[0].ParameterType);
        }
    }
}