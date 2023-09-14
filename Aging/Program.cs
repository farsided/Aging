using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using calcdll;
namespace Aging
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("en-US");

            DateTime start;
            DateTime end;
            TimeSpan span;

            var startFormat = "dd/MM/yyyy hh:mm:ss tt";
            var endFormat = "dd/MM/yyyy hh:mm:ss tt";

            var s = "07/05/1994 12:00:00 PM";
            var e = "09/14/2023 11:00:00 PM";

            start = Convert.ToDateTime(s, culture);
            end = Convert.ToDateTime(e, culture);

            Console.WriteLine(start);
            Console.WriteLine(end);

            Age age = new Age(start);
            Console.WriteLine( $"Years: {age.Years}\nMonths: {age.Months}\nDays: {age.Days}" );
            Console.WriteLine($"CurrentDate: {age.current}");
            Console.ReadLine();
        }
    }

}
