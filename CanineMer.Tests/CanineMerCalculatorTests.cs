using Xunit;
using CanineMer;
using System;

namespace CanineMer.Tests
{
    public class CanineMerCalculatorTests
    {
        [Fact]
        public void KgToLbs_ValidInput_ReturnsCorrectPounds()
        {
            // Arrange
            double kilograms = 10.0;

            // Act
            double pounds = CanineMerCalculator.KgToLbs(kilograms);

            // Assert
            Assert.Equal(22.0462, pounds, 4); // 10 * 2.20462
        }

        [Fact]
        public void KgToLbs_NegativeInput_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double kilograms = -1.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CanineMerCalculator.KgToLbs(kilograms));
            Assert.Contains("Weight cannot be negative", ex.Message);
        }

        [Fact]
        public void LbsToKg_ValidInput_ReturnsCorrectKilograms()
        {
            // Arrange
            double pounds = 22.0462;

            // Act
            double kilograms = CanineMerCalculator.LbsToKg(pounds);

            // Assert
            Assert.Equal(10.0, kilograms, 4); // 22.0462 / 2.20462
        }

        [Fact]
        public void LbsToKg_NegativeInput_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double pounds = -1.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CanineMerCalculator.LbsToKg(pounds));
            Assert.Contains("Weight cannot be negative", ex.Message);
        }

        [Fact]
        public void CalculateRer_ValidInput_ReturnsCorrectRer()
        {
            // Arrange
            double bodyWeightKg = 10.0;

            // Act
            double rer = CanineMerCalculator.CalculateRer(bodyWeightKg);

            // Assert
            Assert.Equal(70 * Math.Pow(10.0, 0.75), rer, 4); // ~394.5 kcal
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void CalculateRer_NonPositiveInput_ThrowsArgumentOutOfRangeException(double bodyWeightKg)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CanineMerCalculator.CalculateRer(bodyWeightKg));
            Assert.Contains("Body weight must be positive", ex.Message);
        }

        [Theory]
        [InlineData(LifeStageFactorsEnum.NeuteredAdult, 10.0, 1.4, 1.6)]
        [InlineData(LifeStageFactorsEnum.Puppy0To4Months, 5.0, 3.0, 3.0)]
        [InlineData(LifeStageFactorsEnum.ActiveWorkingDog, 15.0, 2.0, 5.0)]
        public void CalculateMer_ValidInput_ReturnsCorrectMer(LifeStageFactorsEnum lifeStage, double bodyWeightKg, double lowerFactor, double upperFactor)
        {
            // Arrange
            double expectedRer = CanineMerCalculator.CalculateRer(bodyWeightKg);

            // Act
            var (rer, lowerMer, upperMer) = CanineMerCalculator.CalculateMer(lifeStage, bodyWeightKg);

            // Assert
            Assert.Equal(expectedRer, rer, 4);
            Assert.Equal(expectedRer * lowerFactor, lowerMer, 4);
            Assert.Equal(expectedRer * upperFactor, upperMer, 4);
        }

        [Fact]
        public void CalculateMer_InvalidLifeStage_ThrowsArgumentException()
        {
            // Arrange
            var invalidLifeStage = (LifeStageFactorsEnum)999;
            double bodyWeightKg = 10.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => CanineMerCalculator.CalculateMer(invalidLifeStage, bodyWeightKg));
            Assert.Contains("Invalid life stage", ex.Message);
        }

        [Fact]
        public void CalculateMeanMer_ValidInput_ReturnsCorrectMean()
        {
            // Arrange
            var lifeStage = LifeStageFactorsEnum.NeuteredAdult;
            double bodyWeightKg = 10.0;
            var (rer, lowerMer, upperMer) = CanineMerCalculator.CalculateMer(lifeStage, bodyWeightKg);
            double expectedMean = (lowerMer + upperMer) / 2;

            // Act
            double mean = CanineMerCalculator.CalculateMeanMer(lifeStage, bodyWeightKg);

            // Assert
            Assert.Equal(expectedMean, mean, 4);
        }

        [Fact]
        public void CalculateMeanMer_InvalidLifeStage_ThrowsArgumentException()
        {
            // Arrange
            var invalidLifeStage = (LifeStageFactorsEnum)999;
            double bodyWeightKg = 10.0;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => CanineMerCalculator.CalculateMeanMer(invalidLifeStage, bodyWeightKg));
            Assert.Contains("Invalid life stage", ex.Message);
        }
    }
}