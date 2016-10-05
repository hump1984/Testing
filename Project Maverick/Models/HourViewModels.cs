using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using Microsoft.AspNet.Identity;

using Project_Maverick.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Project_Maverick.Models
{
    public class HourViewModels
    {
        private static int _currentWeek = -1; //Must be static to not "refresh" when "posting"
        private static DateTime _currentViewDate;

        private Dictionary<string, List<Hour>> _curDatesAndHours =  new Dictionary<string, List<Hour>>();

       // private List<List<Hour>> _curRegHours = new List<List<Hour>>();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();
        // Need a calendar.  Culture’s irrelevent since we specify start day of week
        private static Calendar cal = CultureInfo.InvariantCulture.Calendar;



        public HourViewModels()
        {
            if (_currentWeek == -1)
            {
                _currentViewDate = DateTime.Today;
                _currentWeek = GetCurrentWeek(_currentViewDate);
                _curDatesAndHours = GetDatesAndHours(FirstDateOfWeekISO8601(_currentViewDate.Year, _currentWeek));
            }

        }
        public void UpdateDates(int changeWeek)
        {
            _currentWeek += changeWeek;
            _currentViewDate = FirstDateOfWeekISO8601(_currentViewDate.Year, _currentWeek);
            _curDatesAndHours = GetDatesAndHours(FirstDateOfWeekISO8601(_currentViewDate.Year, _currentWeek));
        }

        public DateTime currentViewDate
        { 
            get 
                {
                    return _currentViewDate;
                } 

            set {
                    _currentViewDate = value;
                } 
        }

        public int currentWeek 
        {
            get
            {

                return _currentWeek;
            }

            set
            {
                _currentWeek = value;
            }
        }

        public Dictionary<string, List<Hour>> datesAndHours
        { 
            get 
            {

                return _curDatesAndHours;
            } 
            
            set 
            {
                _curDatesAndHours = value;
            } 
        }

        //public List<List<Hour>> currentHours
        //{
        //    get
        //    {

        //        return _curRegHours;
        //    }
        //    set
        //    {
        //        _curRegHours = value;
        //    }

        //}

        private Dictionary<string,List<Hour>> GetDatesAndHours(DateTime time)
        {
            Dictionary<string,List<Hour>> hours = new Dictionary<string,List<Hour>>();
            List<Hour> hour = new List<Hour>();

            int delta = DayOfWeek.Monday - time.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            DateTime monday = time.AddDays(delta);

            for (int i = 0; i < 7; i++)
            {
                DateTime date = monday.AddDays(i);

                hour = db.Hours.Where(u => u.RegDateTime.Equals(date)).ToList();

                //curRegHour = db.Hours.Where(u => u.RegDateTime.Equals(time)).ToList();

                hours.Add(monday.AddDays(i).ToShortDateString(), hour);
                
            }

            return hours;
        }


        //private List<string> GetWeekDates(DateTime time)
        //{
        //    int delta = DayOfWeek.Monday - time.DayOfWeek;
        //    if (delta > 0)
        //        delta -= 7;
        //    DateTime monday = time.AddDays(delta);

        //    for (int i = 0; i < 7; i++)
        //    {
        //        _curWeekDates.Add(monday.AddDays(i).ToShortDateString());
        //        _curRegHours.Add(GetCurrentHour(monday.AddDays(i)));
        //    }

        //    return _curWeekDates; // Changing year is buggy. Need fixing!
        //}

        //private List<Hour> GetCurrentHour(DateTime time)
        //{
        //    List<Hour> curRegHour = new List<Hour>();
                
        //        curRegHour = db.Hours.Where(u => u.RegDateTime.Equals(time)).ToList();

        //      //  foreach (var item in curRegHour)
        //        //{
        //          //  if (item == null)
        //            //{
        //              //  var temp = new Hour();
        //                //curRegHour.Add(temp);
        //            //}
        //        //}

        //    return curRegHour;
        //}


        private int GetCurrentWeek(DateTime time)
        {
            // This presumes that weeks start with Monday.
            // Week 1 is the 1st week of the year with a Thursday in it.
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it’ll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = cal.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            _currentViewDate = time;

            // Return the week of our adjusted day
            return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        }

        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result;//.AddDays(-3);
        }



        
    }
}