using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatFileGenerator.DataGenerator.Business;

namespace FlatFileGenerator.Test.DataGeneratorTests
{
    public class CprGeneratorTests
    {
        [Fact]
        public void ShouldGenerateCprList()
        {
            // Arrange

            // Act
            var result = CprGenerator.Generate();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Count);
            Assert.All(result, r => Assert.Equal(10, r.Length));
        }

        [Fact]
        public void ShouldGenerate1Cpr()
        {
            // Arrange

            // Act
            var result = CprGenerator.Generate(1);
            var first = result.First();

            // Assert
            Assert.Single(result);
            Assert.Equal(10, first.Length);
        }

        [Fact]
        public void IsValidCpr()
        {
            // Arrange
            var cpr = "200677-3276";

            // Act
            var result = CprGenerator.IsValidCpr(cpr);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetModuloZero()
        {
            // Arrange

            // Act
            var result = CprGenerator.GetCprModulus(true);
            var valid = CprGenerator.IsValidCpr(result);

            // Assert
            Assert.True(valid);
        }
    }
}
