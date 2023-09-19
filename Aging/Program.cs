//for checking
//browse the link:  https://www.timeanddate.com/date/durationresult.html?d1=1&m1=3&y1=1900&d2=24&m2=2&y2=2021
//and this:         https://www.timeanddate.com/date/durationresult.html?d1=28&m1=2&y1=1900&d2=24&m2=2&y2=2021
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            //TimeSpan span;

            //var startFormat = "dd/MM/yyyy hh:mm:ss tt";
            //var endFormat = "dd/MM/yyyy hh:mm:ss tt";

            var s = "02/28/1900 12:00:00 AM";
            var e = "02/24/2021 11:59:59 AM";

            start = Convert.ToDateTime(s, culture);
            end = Convert.ToDateTime(e, culture);

            Console.WriteLine(start);
            Console.WriteLine(end);

            Age age = new Age(start, end);
            Thread ageThread = new Thread(() =>
            {
                Console.WriteLine($"ageThread no added days:{start}");
                
                Console.WriteLine($"ageThread date: {age.start}");
                Console.WriteLine($"ageThread date: {age.end}");
                Console.WriteLine($"ageThread\nYears: {age.Years}\nQuarters: {age.Quarters}\nMonths: {age.Months}\nWeeks: {age.Weeks}\nDays: {age.Days}\nHours: {age.Hours}\nMinutes: {age.Minutes}\nSeconds: {age.Seconds}\n\n");
                Console.WriteLine(age.GetString(7, separator: ',', unitLong: true));
                Console.WriteLine();
            });


            Age age1 = new Age(start.AddDays(1), end);
            Thread age1Thread = new Thread(()=>
            {
                Console.WriteLine($"age1Thread added days:{start.AddDays(1)}");
                
                Console.WriteLine($"age1Thread date: {age1.start}");
                Console.WriteLine($"age1Thread date: {age1.end}");
                Console.WriteLine($"age1Thread\nYears: {age1.Years}\nQuarters: {age1.Quarters}\nMonths: {age1.Months}\nWeeks: {age1.Weeks}\nDays: {age1.Days}\nHours: {age1.Hours}\nMinutes: {age1.Minutes}\nSeconds: {age1.Seconds}\n\n");
                Console.WriteLine(age1.GetString(7, separator: ',', unitLong: true));
                Console.WriteLine();
            });

            ageThread.Start();
            age1Thread.Start();
            //Console.WriteLine( $"Years: {age.Years}\nMonths: {age.Months}\nDays: {age.Days}" );
            //Console.WriteLine($"CurrentDate: {age.current}");

            //included dates [start]
            //List<DateTime> dtList = new List<DateTime>();
            //dtList.Add(start.AddDays(3));
            //dtList.Add(start.AddDays(4));
            //dtList.Add(start.AddDays(9));
            //dtList.Add(start.AddDays(1));
            //dtList.Add(start.AddDays(11));
            //dtList.Add(start.AddDays(0));
            //dtList.Add(start.AddDays(1));

            //foreach(DateTime dt in dtList)
            //{
            //    Console.WriteLine(dt);
            //}

            //Console.WriteLine("Sorted Dates");

            //dtList.Sort();
            //foreach (DateTime dt in dtList)
            //{
            //    Console.WriteLine(dt);
            //}

            //Console.WriteLine($"Included Dates from {start}");
            //List<DateTime> d = Day.IncludedDates(dtList, start.AddDays(4));
            //foreach (DateTime dt in d)
            //{
            //    Console.WriteLine(dt);
            //}

            //Console.WriteLine(Day.IncludedIdx(dtList, start.AddDays(4)));
            //Console.ReadLine();

            //included dates [end]


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

        //public static bool operator ==(Range<T> leftHand, Range<T> rightHand)
        //{
        //    return ((leftHand.Where(x => x != null).Count()) == rightHand.Where(x => x != null).Count());
        //}

        //public static bool operator !=(Range<T> leftHand, Range<T> rightHand)
        //{
        //    return !((leftHand.Where(x => x != null).Count()) == rightHand.Where(x => x != null).Count());
        //}

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
            return (Range<T>)list;
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


        //temporary closed
        //public static KeyValuePair<int, int> IncludedIdx(List<DateTime> sortedListAsc, DateTime start, DateTime? end = null)
        //{
        //    end = end ?? DateTime.Now;
        //    int? startIdx = null;
        //    int? endIdx = null;
        //    for (int x = 0; x < sortedListAsc.Count; x++)
        //    {
        //        startIdx = (startIdx == null) ? ((sortedListAsc[x] >= start && sortedListAsc[x] <= end) ? x : (int?)(null) ) : startIdx;
        //        if (startIdx != null) break;
        //    }
        //    for (int x = sortedListAsc.Count - 1; x >= 0; x++)
        //    {
        //        endIdx = (endIdx == null) ? ((sortedListAsc[x] >= start && sortedListAsc[x] <= end) ? x : (int?)(null)) : endIdx;
        //        if (endIdx != null) break;
        //    }
        //    return new KeyValuePair<int, int>( (int)startIdx, (int)endIdx );
        //}
    }

    public class Age
    {
        public enum Combi
        {
            MIN         = 0,    //default - addaptive
            SECONDMIN,
            SECONDMAX,
            MINUTEMIN,
            MINUTEMAX,
            HOURMIN,
            HOURMAX,
            DAYMIN,
            DAYMAX,
            WEEKMIN,
            WEEKMAX,
            MONTHMIN,
            MONTHMAX,
            QUARTERMIN,
            QUARTERMAX,
            YEARMIN,
            YEARMAX,
            OUTOFBOUND,
        }

        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public DateTime current { get; set; }
        public TimeSpan diff { get { return (end - start); } }

        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        public int Quarters { get { return (Months / 4); } }
        public int Weeks { get { return Days / 7; } }
        public int Hours { get { return (diff.Hours % 24 ); } }
        public int Minutes { get { return (diff.Minutes % 60); } }
        public int Seconds { get { return (diff.Seconds % 60); } }

        public int TotalYears { get { return GetYear(start, end); } }
        public int TotalMonths { get { return GetMonth(start, end); } }
        public int TotalDays { get { return GetDay(start, end); } }

        public int UnitCount { get; set; }

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
            this.start = start;
            this.end = end; 

            Years = GetYear(current, end);
            Months = GetMonth(current, end);
            Days = GetDay(current, end);
        }

        public List<KeyValuePair<string, int>> TimeUnits { get {
                List<KeyValuePair<string, int>> set = new List<KeyValuePair<string, int>>();
                set.Add(new KeyValuePair<string, int>("Year", Years));
                set.Add(new KeyValuePair<string, int>("Month", Months));
                set.Add(new KeyValuePair<string, int>("Week", Weeks));
                set.Add(new KeyValuePair<string, int>("Day", Days % 7));
                set.Add(new KeyValuePair<string, int>("Hour", Hours));
                set.Add(new KeyValuePair<string, int>("Minute", Minutes));
                set.Add(new KeyValuePair<string, int>("Second", Seconds));
                return set; } }

        public List<KeyValuePair<string, int>> TimeUnitsShort
        {
            get
            {
                List<KeyValuePair<string, int>> set = new List<KeyValuePair<string, int>>();
                set.Add(new KeyValuePair<string, int>("Y", Years));
                set.Add(new KeyValuePair<string, int>("M", Months));
                set.Add(new KeyValuePair<string, int>("W", Weeks));
                set.Add(new KeyValuePair<string, int>("D", Days % 7));
                set.Add(new KeyValuePair<string, int>("h", Hours));
                set.Add(new KeyValuePair<string, int>("m", Minutes));
                set.Add(new KeyValuePair<string, int>("s", Seconds));
                return set;
            }
        }



        public string GetString(int unitCountMax, string suffix = "s", string replacement = "&", char separator = ',', int? unitCountMin = null, bool unitLong = false)
        {
            

            int unitCounter = 0;
            int unitAccumulator = 0;
            string str = null;
            List<KeyValuePair<string, int>> timeUnits = unitLong ? TimeUnits : TimeUnitsShort;
            for (; (unitCounter < timeUnits.Count) && (unitAccumulator < unitCountMax); )
            {
                if (timeUnits.ElementAt(unitCounter).Value > 0)
                {
                    str += $"{timeUnits.ElementAt(unitCounter).Value}{(unitLong ? " ":"")}{timeUnits.ElementAt(unitCounter).Key}{(timeUnits.ElementAt(unitCounter).Value > 1 ? ( unitLong ? suffix : "") : "")}{separator.ToString()} ";
                    unitAccumulator++;
                }
                unitCounter++;
            }
            if(str.Length > 0)
            {
                str = str.Remove(str.Length - 2, 2);
            }

            Compose(ref str, unitAccumulator, replacement:replacement, separator: separator);
            UnitCount = unitAccumulator;

            return str;
        }

        public void Compose(ref string str, int objCounter, string replacement = "&", char separator = ',')
        {
            if(objCounter > 1)
            {
                str = str.Insert(str.LastIndexOf(separator) + 1, " " + replacement);
                str = str.Remove(str.LastIndexOf(separator), 1);
            }
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
