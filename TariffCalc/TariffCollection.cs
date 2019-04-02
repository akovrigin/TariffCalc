using System;
using System.Collections.Generic;
using System.Linq;

namespace TariffCalc
{
    public class TariffCollection : List<Tariff>
    {
        public TariffCollection(IEnumerable<Tariff> storage)
        {
            this.AddRange(storage.ToList());
        }

        public IEnumerable<Tariff> OrderByRelevance(ICalculator calculator, int minutes, int duration)
        {
            return this.OrderBy(tariff => calculator.Calculate(tariff, minutes, duration));
        }
    }
}
