using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using calcdll;
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

    public class Age
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public DateTime current { get; set; }

        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        public int TotalYears { get { return GetYear(start, end); } }
        public int TotalMonths { get { return GetMonth(start, end); } }
        public int TotalDays { get { return GetDay(start, end); } }

        public Age(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            GetAge(start, end);
        }

        public void GetAge(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            TimeSpan dateDiff = end - start;
            current = start;

            Years = GetYear(current, end);
            Months = GetMonth(current, end);
            Days = GetDay(current, end);

            return;
        }

        public int GetYear(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            int yearCount = 0;
            for (int year = start.Year; year < end.Year; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(start.Year);
                if (((isLeapYear && ((end - start).TotalDays >= 366)) ||
                            (!isLeapYear && ((end - start).TotalDays >= 365))))
                {
                    start = start.AddYears(1);
                    current = start;
                    yearCount++;
                }
            }
            return yearCount;
        }

        public int GetMonth(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            TimeSpan dateDiff = end - start;
            current = start;
            int monthCount = 0;
            for (int month = current.Month; month <= ((current.Year == end.Year) ? end.Month : 12);)
            {
                if (((current.Year == end.Year) && (current.Month != (end.Month))) || ((current.Year < end.Year)))
                {
                    if ((dateDiff.Days >= DateTime.DaysInMonth(current.Year, current.Month)) || (current.Year <= end.Year))
                    {
                        current = current.AddMonths(1);
                        monthCount++;
                        dateDiff = end - current;
                    }
                }
                if (current.Year == end.Year && month == end.Month - 1)
                {
                    break;
                }
                else
                {
                    month = ((month % 12) + 1);
                }
            }

            return monthCount;
        }

        public int GetDay(DateTime start, DateTime? End = null)
        {
            current = current.AddDays(((DateTime)End - start).Days);
            return ((DateTime)End - start).Days;
        }
    }
}
