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
            //Console.WriteLine(1000000.1m + 0.2m - 0.3m);
            //char i = 'y';
            //while (i=='y')
            //{
            //    var x = Math.Pow(10d, 5d);
            //    var y = Math.Pow(10d, 6d);
            //    Console.WriteLine($"x: {x} length: {x.ToString().Length}");
            //    Console.WriteLine($"y: {y} length: {y.ToString().Length}");
            //    Console.WriteLine("'y' to continue");
            //    x = Console.Read();
            //}

            //return;

            
            CultureInfo culture = new CultureInfo("en-US");

            DateTime start;
            DateTime end;
            TimeSpan span;

            var startFormat = "dd/MM/yyyy hh:mm:ss tt";
            var endFormat = "dd/MM/yyyy hh:mm:ss tt";

            var s = "07/24/2004 12:00:00 PM";
            var e = "08/26/2005 12:00:00 PM";

            start = Convert.ToDateTime(s, culture);
            end = Convert.ToDateTime(e, culture);

            Program.GetAge(start, end);

            Console.ReadLine();
            return;

            TimeSpan ss = start - end;

            int dayIt = start.Day;

            int years = 0;
            int months = 0;
            int weeks = 0;
            int days = 0;
            int tempMonth = start.Month;
            int tempDay = start.Day;

            for (int yearIt = start.Year; yearIt <= end.Year;)
            {
                DateTime date;
                int tempYear = yearIt;
                
                //Console.WriteLine($"{dayIt}/{7}/{yearIt} 12:00:00 AM");
                for (int monthIt = tempMonth; ( date = Convert.ToDateTime($"{(monthIt%12)+1}/{dayIt}/{yearIt} 12:00:00 AM", culture) ) <= end; monthIt++)
                {
                    Console.WriteLine($"{dayIt}/{(monthIt % 12) + 1}/{yearIt}\nDays in Month: {DateTime.DaysInMonth(yearIt, (monthIt % 12) + 1)}");
                    //for(; ( Convert.ToDateTime($"{(monthIt % 12) + 1}/{dayIt}/{yearIt} 12:00:00 AM", culture) ) <= end ; tempDay++)
                    //{

                    //}
                    Console.ReadLine();

                    if (((monthIt % 12) + 1) == 12)
                    {
                        yearIt = (end.Year + 1);
                    }
                    months++;

                    years = Convert.ToInt32(Math.Floor(Convert.ToDecimal(months / 12)));
                    if (years > 0)
                    {
                        Console.WriteLine($"years: {years}\nmonths: {months % 12}");
                    }
                    else if (years < 1)
                    {
                        Console.WriteLine($"months: {months % 12}");
                    }
                    else if (months < 1)
                    {
                        Console.WriteLine($"months: {months % 12}");
                    }
                    
                }
                tempMonth = 0;
                yearIt = tempYear + 1;
                
                
            }

            //Console.WriteLine($"start date: {start}");
            //Console.WriteLine($"start Year: {start.Year}");
            //Console.WriteLine($"start Month: {start.Month}");
            //Console.WriteLine($"start Day: {start.Day}");

            //Console.WriteLine($"end date: {end}");
            //Console.WriteLine($"end Year: {end.Year}");
            //Console.WriteLine($"end Month: {end.Month}");
            //Console.WriteLine($"end Day: {end.Day}");

            if(end.Day > start.Day)
            {
                Console.WriteLine($"month:{years}");
                Console.WriteLine($"month:{months}");
                Console.WriteLine($"day: {end.Day - tempDay}");
            }
            else
            {
                Console.WriteLine($"day: { (DateTime.DaysInMonth(end.Year, end.Month - 1) - start.Day + end.Day + 1)}");
                Console.WriteLine($"day: { ( DateTime.DaysInMonth(end.Year, end.Month - 1) - start.Day + end.Day + 1 )}");
            }

            Console.ReadLine();
        }

        public static void GetAge(DateTime start, DateTime end)
        {
            DateTime current = start;
            TimeSpan dateDiff = end - start;
            int maxYear = end.Year;

            int yearCount = 0;
            int monthCount = 0;
            int dayCount = 0;
            int hourCount = 0;
            int minuteCount = 0;
            int secondCount = 0;

            Console.WriteLine($"Start: {start}");
            Console.WriteLine($"End: {end}");

            //year

            for ( int year = current.Year; year < maxYear; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(current.Year);
                if (    (   ( isLeapYear && ( dateDiff.TotalDays >= 366 ) ) ||
                            ( !isLeapYear && ( dateDiff.TotalDays >= 365 ) )    )   )
                {
                        current = current.AddYears(1);
                        yearCount++;
                }
            }

            Console.WriteLine($"Current Date: {current}");
            Console.WriteLine($"yearCount: {yearCount}");

            


            //month

            for (int month = current.Month; month <= ( ( current.Year == end.Year) ? end.Month : 12) ;)
            {
                if (current.Month != (end.Month))
                {
                    if ((dateDiff.Days >= DateTime.DaysInMonth(current.Year, current.Month)))
                    {
                        current = current.AddMonths(1);
                        monthCount++;
                        dateDiff = end - current;
                    }
                    
                }
                if( current.Year == end.Year && month == end.Month - 1)
                {
                    break;
                }
                else
                {
                    month = ( ( month + 1 ) % 12 ) ;
                }
            }
            Console.WriteLine($"Current Date: {current}");
            Console.WriteLine($"monthCount: { monthCount }"); // ( (monthCount > 1 ) ? ( monthCount - 1 ) : 0 ) }");


            //day

            for ( int day = current.Day; day <= ( ( current.Month == end.Month ) ? end.Day : DateTime.DaysInMonth( current.Year, current.Month ) ); )
            {
                if( current.Day != ( end.Day ))
                {
                    if ( dateDiff.Days >= 1 )
                    {
                        current = current.AddDays(1);
                        dayCount++;
                        dateDiff = end - current;
                    }
                }
                if( current.Month == end.Month && day == end.Day - 1)
                {
                    break;
                }
                else
                {
                    day = ((day + 1) % DateTime.DaysInMonth( current.Year, current.Month ));
                }
            }

            Console.WriteLine($"Current Date: {current}");
            Console.WriteLine($"dayCount: { dayCount }"); // ( (monthCount > 1 ) ? ( monthCount - 1 ) : 0 ) }");

            return;
            int maxMonth = ((current.Year == maxYear) ? end.Month : 12);

            for (int month = current.Month; month <= maxMonth; month++)
            {
                int maxDay = ((current.Month == end.Month && current.Year == maxYear) ? end.Day : DateTime.DaysInMonth(current.Year, current.Month));
                for (int day = current.Day; day <= end.Day; day++)
                {
                    int maxHour = ((current.Day == end.Hour && current.Month == end.Month && current.Year == maxYear) ? end.Hour : 24);
                    for (int hour = current.Hour; hour <= end.Hour; hour++)
                    {
                        int maxMinute = ((current.Hour == end.Hour && current.Day == end.Day && current.Month == end.Month && current.Year == maxYear) ? end.Minute : 60);

                    }
                }
            }

        }
    }
}
