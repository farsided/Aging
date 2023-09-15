﻿using System;
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
            //DateTime end;
            //TimeSpan span;

            //var startFormat = "dd/MM/yyyy hh:mm:ss tt";
            //var endFormat = "dd/MM/yyyy hh:mm:ss tt";

            var s = "09/01/2023 12:00:00 AM";
            var e = "09/14/2002 11:00:00 PM";

            start = Convert.ToDateTime(s, culture);
            //end = Convert.ToDateTime(e, culture);

            //Console.WriteLine(start);
            ////Console.WriteLine(end);

            //Age age = new Age(start);
            //Console.WriteLine( $"Years: {age.Years}\nMonths: {age.Months}\nDays: {age.Days}" );
            //Console.WriteLine($"CurrentDate: {age.current}");

            List<DateTime> dtList = new List<DateTime>();
            dtList.Add(start.AddDays(3));
            dtList.Add(start.AddDays(4));
            dtList.Add(start.AddDays(9));
            dtList.Add(start.AddDays(1));
            dtList.Add(start.AddDays(11));
            dtList.Add(start.AddDays(0));
            dtList.Add(start.AddDays(1));

            foreach(DateTime dt in dtList)
            {
                Console.WriteLine(dt);
            }

            Console.WriteLine("Sorted Dates");


            dtList.Sort();
            foreach (DateTime dt in dtList)
            {
                Console.WriteLine(dt);
            }


            Console.WriteLine($"Included Dates from {start}");
            List<DateTime> d = Day.IncludedDates(dtList, start.AddDays(4));
            foreach (DateTime dt in d)
            {
                Console.WriteLine(dt);
            }

            Console.WriteLine(Day.IncludedIdx(dtList, start.AddDays(4)));
            Console.ReadLine();

            
        }
    }

    public class Range<T> : List<T>
    {
        public int startIdx { get; set; }
        public int endIdx { get; set; }
        public List<T> list { get; set; }

        public Range() { }

        #region operator comparator overlording : >, <, >=, <=, ==, !=

        public static bool operator >(Range<T> leftHand, Range<T> rightHand)
        {
            return ((leftHand.Where(x => x != null).Count()) > rightHand.Where(x => x != null).Count());
        }

        public static bool operator <(Range<T> leftHand, Range<T> rightHand)
        {
            return !((leftHand.Where(x => x != null).Count()) > rightHand.Where(x => x != null).Count());
        }

        public static bool operator >=(Range<T> leftHand, Range<T> rightHand)
        {
            return ((leftHand.Where(x => x != null).Count()) > rightHand.Where(x => x != null).Count());
        }

        public static bool operator <=(Range<T> leftHand, Range<T> rightHand)
        {
            return !((leftHand.Where(x => x != null).Count()) > rightHand.Where(x => x != null).Count());
        }

        public static bool operator ==(Range<T> leftHand, Range<T> rightHand)
        {
            return ((leftHand.Where(x => x != null).Count()) == rightHand.Where(x => x != null).Count());
        }

        public static bool operator !=(Range<T> leftHand, Range<T> rightHand)
        {
            return !((leftHand.Where(x => x != null).Count()) == rightHand.Where(x => x != null).Count());
        }

        #endregion

        #region assignment operator overloading : +, -

        public static Range<T> operator +(Range<T> range, T item)
        {
            range.Add(item);
            return range;
        }

        public static Range<T> operator +(Range<T> range, Range<T> list)
        {
            range.AddRange(list);
            return range;
        }

        public static Range<T> operator -(Range<T> range, T item)
        {
            range.RemoveAll(it => EqualityComparer<T>.Default.Equals(it, item) ) ;
            return range;
        }

        #endregion

        public static Range<T> ToRange(IEnumerable<T> list)
        {
            return (Range<T>)list.ToList();
        }

        public Range<T> SelectIncludedRange(Range<T> sortedListAsc, T start, T end)
        {
            return ToRange(sortedListAsc.Where(x => (Convert.ToInt16(x) >= Convert.ToInt16(start) && Convert.ToInt16(x) <= Convert.ToInt16(end)))) ;
        }

    }

    public class Day
    {
        enum DayType
        {
            Weekday,
            Weekend,
            Workday,
            SpecialHoliday,
            SpecialNonWorkingHoliday,
            RegularHoliday,
            Absentday,
            LateDay,
            LeaveDay,
        }

        public static List<DateTime> IncludedDates(List<DateTime> sortedListAsc, DateTime start, DateTime? end = null)
        {
            end = end ?? DateTime.Now;
            List<DateTime> temp = new List<DateTime>();
            for(int x = 0; x < sortedListAsc.Count; x++)
            {
                if(sortedListAsc[x]>=start && sortedListAsc[x] <= end)
                {
                    temp.Add(sortedListAsc[x]);
                }   
            }
            return temp;
        }

        public static KeyValuePair<int, int> IncludedIdx(List<DateTime> sortedListAsc, DateTime start, DateTime? end = null)
        {
            end = end ?? DateTime.Now;
            int? startIdx = null;
            int? endIdx = null;
            for (int x = 0; x < sortedListAsc.Count; x++)
            {
                startIdx = (startIdx == null) ? ((sortedListAsc[x] >= start && sortedListAsc[x] <= end) ? x : (int?)(null) ) : startIdx;
                if (startIdx != null) break;
            }
            for (int x = sortedListAsc.Count - 1; x >= 0; x++)
            {
                endIdx = (endIdx == null) ? ((sortedListAsc[x] >= start && sortedListAsc[x] <= end) ? x : (int?)(null)) : endIdx;
                if (endIdx != null) break;
            }
            return new KeyValuePair<int, int>( (int)startIdx, (int)endIdx );
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

        public Age()
        {

        }

        public Age(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            Init(start, end);
        }

        public void Init(DateTime start, DateTime? End = null)
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
                        if (current < end.AddMonths(-1) )
                        {
                            current = current.AddMonths(1);
                            dateDiff = end - current;
                            monthCount++;
                        }
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
            int dayDiff = ((DateTime)End - start).Days;
            current = current.AddDays(dayDiff);
            return dayDiff;//(int)Math.Floor((((DateTime)End - start).TotalHours / 24));
        }
    }

    class Rate
    {
        enum RateType
        {
            WorkedRegular,
            UnworkedRegular,

        }
    }
}
