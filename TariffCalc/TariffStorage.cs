using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TariffCalc
{
    public class TariffStorage : IEnumerable<Tariff>
    {
        decimal[][] _data =
        {
            new [] { 0.15m, 10, 0 },
            new [] { 0.20m, 25, 130, 12, 20, 24, 35 },
            new [] { 0.20m, 35, 190, 12, 25, 24, 40 },
            //You can add here tariffs.
            //The first three values is MinuteRate, FlatMonthlyFee and IncludedMinutes
            //Then following pairs of bonuses: Duration, Value
        };

        public IEnumerator<Tariff> GetEnumerator()
        {
            string[] _names = Enumerable.Range('A', 'Z' - 'A' + 1).
                Select(i => Convert.ToString((char)i)).
                ToArray();

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].Length < 3 || _data[i].Length % 2 == 0)
                    throw new Exception("Data is invalid");

                var tariff = new Tariff
                {
                    Name = _names[i],
                    MinuteRate = _data[i][0],
                    FlatMonthlyFee = _data[i][1],
                    IncludedMinutes = Convert.ToInt16(_data[i][2])
                };

                for (int j = 3; j < _data[i].Length; j += 2)
                {
                    tariff.AddBonus(new Bonus
                    {
                        Duration = Convert.ToInt16(_data[i][j]),
                        Value = Convert.ToInt16(_data[i][j + 1]),
                    });
                }

                yield return tariff;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
