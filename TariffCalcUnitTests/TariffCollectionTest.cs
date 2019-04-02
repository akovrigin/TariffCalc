using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TariffCalc;
using Xunit;

namespace TariffCalcUnitTests
{
    public class TariffCollectionTest
    {
        private class CalculatorMoq : ICalculator
        {
            public decimal Calculate(Tariff tariff, int minutes, int duration)
            {
                return tariff.MinuteRate;
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new string[] { "A", "B", "C" })]
        [InlineData(new int[] { 2, 1, 3 }, new string[] { "B", "A", "C" })]
        public void OrderByRelevanceTest(int[] payment, string[] result)
        {
            //MinuteRate is used as a result of payment calculation
            var tariffs = new Tariff[] {
                new Tariff { Name = "A", MinuteRate = payment[0] },
                new Tariff { Name = "B", MinuteRate = payment[1] },
                new Tariff { Name = "C", MinuteRate = payment[2] },
            };

            var tariffCollection = new TariffCollection(tariffs);
            var calc = new CalculatorMoq();
            var list = tariffCollection.OrderByRelevance(calc, 0, 0).Select(x => x.Name).ToArray();

            bool areEqual = result.SequenceEqual(list);

            Assert.True(areEqual);
        }
    }
}
