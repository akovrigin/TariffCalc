using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TariffCalc
{
    public class Tariff
    {
        public string Name { get; set; }
        public decimal MinuteRate { get; set; }
        public decimal FlatMonthlyFee { get; set; }
        public int IncludedMinutes { get; set; }

        public List<Bonus> Bonuses;

        public void AddBonus(Bonus bonus)
        {
            if (Bonuses == null)
                Bonuses = new List<Bonus>();

            Bonuses.Add(bonus);
        }

        public decimal GetBonus(int months)
        {
            var bonuses = Bonuses?.Where(x => x.Duration <= months);

            if (bonuses == null || !bonuses.Any())
                return 0;

            return bonuses?.Max(x => x.Value) ?? 0;
        }
    }

    public class Bonus
    {
        public int Duration { get; set; }
        public int Value { get; set; }
    }
}