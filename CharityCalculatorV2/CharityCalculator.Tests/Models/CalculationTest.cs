using CharityCalculatorV2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CharityCalculator.Tests.Models
{
    public class CalculationTest
    {
        private Calculation _calculation;
        private ApplicationUser _user;
        private double _amountInserted;
        private double _taxRate;
        private string _eventType;

        public CalculationTest()
        {
            _user = new ApplicationUser();
            _amountInserted = 50000;
            _taxRate = 20;
            _eventType = "Other";

            _calculation = new Calculation(_user, _amountInserted, _taxRate, _eventType);
        }

        [Theory]
        [InlineData(-50000)] // cant donate negative amount
        [InlineData(0)] // cant donate 0
        public void NewCalculation_InvalidAmount_ReturnsException(double amountInserted)
        {
            // Arrange
            var taxRate = 20;
            var eventType = "Other";

            // Assert
            Assert.Throws<ArgumentException>(() => new Calculation(_user, amountInserted, taxRate, eventType));
        }

        [Theory]
        [InlineData(-20)]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(200)]
        public void NewCalculation_InvalidTaxRate_ReturnsException(double taxRate)
        {
            var amount = 50000;
            var eventType = "Other";

            Assert.Throws<ArgumentException>(() => new Calculation(_user, amount, taxRate, eventType));
        }

        [Fact]
        public void CalculateDeductableAmount_ValidProperties_ReturnsCorrectValue()
        {
            // Arrange
            _calculation.AmountInserted = 50000;
            _calculation.UsedTaxRate = 20;
            _calculation.EventType = "Other";

            // Act
            var result = _calculation.CalculateDeductableAmount();

            // Assert
            Assert.Equal(12500, result);
        }
    }
}
