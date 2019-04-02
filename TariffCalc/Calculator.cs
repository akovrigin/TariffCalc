using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TariffCalc
{
    public interface ICalculator
    {
        decimal Calculate(Tariff tariff, int minutes, int duration);
    }

    public class Calculator : ICalculator
    {
        private readonly Dictionary<string, List<string>> _log = new Dictionary<string, List<string>>();
        public List<string> GetLog(string tariffName) => _log[tariffName];

        public decimal Calculate(Tariff tariff, int minutes, int duration)
        {
            var overtime = Math.Abs(Math.Min(0, tariff.IncludedMinutes - minutes));
            var monthly = overtime * tariff.MinuteRate + tariff.FlatMonthlyFee;
            var result = monthly * duration - tariff.GetBonus(duration);

            //var coveredText = overtime == 0 ? "covered by included minutes" : string.Empty;
            var overtimeText = overtime == 0 ? "0" : $"({minutes} - {tariff.IncludedMinutes})";
            var total = $"({overtimeText} * {tariff.MinuteRate} + {tariff.FlatMonthlyFee}) * {duration} - {tariff.GetBonus(duration)} = {result}";

            var log = new List<string> {
                $"Tariff {tariff.Name}: {total}",
                //If it needs to create detailed log.
                //$"Overtime: {overtime} minutes ( {minutes} - {tariff.IncludedMinutes} {coveredText})",
                //$"Monthly: {overtimeText} * {tariff.MinuteRate} + {tariff.FlatMonthlyFee} = {monthly}",
                //$"Total payment: ({overtimeText} * {tariff.MinuteRate} + {tariff.FlatMonthlyFee}) * {duration} - {tariff.GetBonus(duration)} = {result}",
                //string.Empty,
            };

            if (_log.ContainsKey(tariff.Name))
                _log[tariff.Name] = log;
            else
                _log.Add(tariff.Name, log);

            return result;
        }
    }
}
