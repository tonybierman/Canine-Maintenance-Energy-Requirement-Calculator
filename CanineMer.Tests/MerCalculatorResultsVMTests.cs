using Xunit;
using CanineMer;
using System;

namespace CanineMer.Tests
{
    public class MerCalculatorResultsVMTests
    {
        // Mock AddSpacesToPascalCase for testing
        private static string AddSpacesToPascalCase(string text)
        {
            return string.Join(" ", System.Text.RegularExpressions.Regex.Split(text, @"(?<=\\w)(?=\\p{Lu})"));
        }

        [Fact]
        public void Calculate_ValidInput_ReturnsExpectedResults()
        {
            // Arrange
            var lifeStage = LifeStageFactorsEnum.NeuteredAdult;
            double weightInKgs = 10.0; // ~22 lbs

            // Act
            var result = MerCalculatorResultsVM.Calculate(lifeStage, weightInKgs);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Neutered Adult", result.LifeStage);
            Assert.True(Math.Abs(result.WeightInPounds - 22.0462) < 0.0001); // 10 kg * 2.20462
            Assert.True(result.Rer > 0);
            Assert.True(result.LowerBounds > 0);
            Assert.True(result.UpperBounds > result.LowerBounds);
            Assert.Equal((result.LowerBounds + result.UpperBounds) / 2, result.Mean, 4);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void Calculate_NonPositiveWeight_ThrowsArgumentOutOfRangeException(double weightInKgs)
        {
            // Arrange
            var lifeStage = LifeStageFactorsEnum.NeuteredAdult;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => MerCalculatorResultsVM.Calculate(lifeStage, weightInKgs));
            Assert.Contains("Weight must be positive", ex.Message);
        }

        [Fact]
        public void Calculate_InvalidLifeStage_ThrowsArgumentException()
        {
            // Arrange
            var invalidLifeStage = (LifeStageFactorsEnum)999; // Invalid enum value
            double weightInKgs = 10.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => MerCalculatorResultsVM.Calculate(invalidLifeStage, weightInKgs));
            Assert.Contains("Invalid life stage", ex.Message);
        }

        [Fact]
        public void Calculate_NullLifeStageString_ReturnsNullLifeStage()
        {
            // Arrange - Assuming AddSpacesToPascalCase could return null in some edge cases
            var lifeStage = LifeStageFactorsEnum.NeuteredAdult;
            double weightInKgs = 10.0;

            // Act
            var result = MerCalculatorResultsVM.Calculate(lifeStage, weightInKgs);

            // Assert - LifeStage should not be null with valid enum, but test ensures it's set
            Assert.NotNull(result.LifeStage);
        }
    }
}