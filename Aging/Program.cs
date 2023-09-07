using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var s = "07/24/1998 12:00:00 PM";
            var e = "07/24/2005 12:00:00 PM";

            start = Convert.ToDateTime(s, culture);
            end = Convert.ToDateTime(e, culture);

            TimeSpan ss = start - end;

            int dayIt = start.Day;

            int years;
            int months;
            int weeks;
            int days;


            //DateTime.DaysInMonth
            for (int yearIt = start.Year; yearIt < end.Year;)
            {
                
                DateTime date;
                //Console.WriteLine($"{dayIt}/{7}/{yearIt} 12:00:00 AM");
                for (   int monthIt = start.Month; 
                        ( date = Convert.ToDateTime($"{(monthIt%12)+1}/{dayIt}/{yearIt} 12:00:00 AM", culture) ) <= end;
                        monthIt++   )
                {
                    Console.WriteLine($"{dayIt}/{(monthIt%12)+1}/{yearIt}\nDays in Month: {DateTime.DaysInMonth(yearIt, (monthIt % 12) + 1)}");
                    Console.ReadLine();
                    if (DateTime.IsLeapYear(yearIt))
                    {
                        Console.WriteLine($"Is Leap Year: {DateTime.IsLeapYear(yearIt)}");
                    }
                    if((((monthIt % 12) + 1) == 12))
                    {
                        yearIt++;
                    }

                }
            }

            Console.WriteLine($"start date: {start}");
            Console.WriteLine($"start Year: {start.Year}");
            Console.WriteLine($"start Month: {start.Month}");
            Console.WriteLine($"start Day: {start.Day}");

            Console.WriteLine($"end date: {end}");
            Console.WriteLine($"end Year: {end.Year}");
            Console.WriteLine($"end Month: {end.Month}");
            Console.WriteLine($"end Day: {end.Day}");

            
            Console.ReadLine();
            for (int yearIt = 0; yearIt < 100 ; yearIt++ ) {
                for (int monthIt = 0; monthIt < 100; monthIt++)
                {

                }
            }
                
        }
    }
}
