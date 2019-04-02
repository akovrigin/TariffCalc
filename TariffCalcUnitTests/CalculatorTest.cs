using System;
using System.Collections;
using System.Collections.Generic;
using TariffCalc;
using Xunit;

namespace TariffCalcUnitTests
{
    public class CalculatorTest
    {
        public CalculatorTest()
        {
            _tariffs = new Dictionary<string, Tariff>();

            var t = new Tariff { Name = "A", MinuteRate = 0.15m, FlatMonthlyFee = 10 };
            _tariffs.Add("A", t);

            t = new Tariff { Name = "B", MinuteRate = 0.20m, FlatMonthlyFee = 25, IncludedMinutes = 130 };
            t.AddBonus(new Bonus { Duration = 12, Value = 20, });
            t.AddBonus(new Bonus { Duration = 24, Value = 35, });
            _tariffs.Add("B", t);

            t = new Tariff { Name = "C", MinuteRate = 0.20m, FlatMonthlyFee = 35, IncludedMinutes = 190 };
            t.AddBonus(new Bonus { Duration = 12, Value = 25, });
            t.AddBonus(new Bonus { Duration = 24, Value = 40, });
            _tariffs.Add("C", t);
        }

        private Dictionary<string, Tariff> _tariffs;

        [Theory]
        [InlineData(80, 6, "A", 132)]
        [InlineData(80, 6, "B", 150)]
        [InlineData(80, 6, "C", 210)]
        [InlineData(150, 12, "A", 390)]
        [InlineData(150, 12, "B", 328)]
        [InlineData(150, 12, "C", 395)]
        public void CalculateTest(int minutes, int duration, string name, int result)
        {
            var payment = new Calculator().Calculate(_tariffs[name], minutes, duration);

            Assert.Equal(payment, result);
        }
    }
}
