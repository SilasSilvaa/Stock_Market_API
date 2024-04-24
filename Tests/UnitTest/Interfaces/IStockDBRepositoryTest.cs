using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Xunit;

namespace Tests.Interfaces
{
    public class IStockDBRepositoryTest
    {
        [Fact]
        public void ShouldHaveAllMethods()
        {
            // Arrange

            // Act

            // Assert
            AssertInterfaceMethod<IStockDBRepository>("GetAllAsync", typeof(QueryObject));
            AssertInterfaceMethod<IStockDBRepository>("GetByIdAsync", typeof(int));
            AssertInterfaceMethod<IStockDBRepository>("GetBySymbolAsync", typeof(string));
            AssertInterfaceMethod<IStockDBRepository>("CreateAsync", typeof(StockDB));
            AssertInterfaceMethod<IStockDBRepository>("UpdateAsync", typeof(int), typeof(UpdateStockRequestDto
            ));
            AssertInterfaceMethod<IStockDBRepository>("DeleteAsync", typeof(int));
        }

        private void AssertInterfaceMethod<TInterface>(string methodName, params Type[] parameterTypes)
        {
            var method = typeof(TInterface).GetMethod(methodName);

            Assert.NotNull(method);
            Assert.True(method.IsPublic);
            Assert.True(typeof(Task).IsAssignableFrom(method.ReturnType));
            Assert.Equal(typeof(Task<>), method.ReturnType.GetGenericTypeDefinition());
            Assert.Equal(parameterTypes.Length, method.GetParameters().Length);

            for (int i = 0; i < parameterTypes.Length; i++)
            {
                Assert.Equal(parameterTypes[i], method.GetParameters()[i].ParameterType);
            }
        }
    }
}