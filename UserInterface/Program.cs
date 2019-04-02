using System;
using System.Linq;
using TariffCalc;

namespace UserInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new TariffStorage();

            foreach (var tariff in storage)
            {
                Console.WriteLine($"Tariff {tariff.Name}: MinuteRate = {tariff.MinuteRate}, FlatMonthlyFee = {tariff.FlatMonthlyFee}, IncludedMinutes = {tariff.IncludedMinutes}");
            }

            Console.WriteLine();

            Func<string, string, int> GetValueFromConsole = (text, error) =>
            {
                int value = 0;

                while (value <= 0)
                {
                    int top = Console.CursorTop;

                    Console.SetCursorPosition(0, top);
                    Console.Write(text);

                    int left = Console.CursorLeft;

                    Console.Write(new string(' ', Console.WindowWidth - left));
                    Console.SetCursorPosition(left, top);

                    if (!int.TryParse(Console.ReadLine(), out value) || value == 0)
                    {
                        var color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(error);
                        Console.ForegroundColor = color;
                        Console.CursorLeft = left;
                        Console.CursorTop = top;
                    }
                }

                return value;
            };

            int minutes = GetValueFromConsole("Please, enter the cell phone usage per month in minutes: ", "Error. Minutes value should be integer and more than zero.");
            int duration = GetValueFromConsole("Please, enter the period of usage in months: ", "Error. The duration value should be integer and more than zero.");

            var tariffs = new TariffCollection(new TariffStorage());
            var calc = new Calculator();
            var result = tariffs.OrderByRelevance(calc, minutes, duration);

            Console.WriteLine();

            foreach (var name in result.Select(x => x.Name))
                foreach (var text in calc.GetLog(name))
                    Console.WriteLine(text);

            Console.ReadKey();
        }
    }
}
