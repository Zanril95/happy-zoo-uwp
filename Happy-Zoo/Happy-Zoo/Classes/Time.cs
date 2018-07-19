using System;
using System.Linq;
using Windows.System.Threading;
using Windows.UI.Core;

namespace Happy_Zoo
{
    /// <summary>
    /// This class creates the time and keeps it ticking
    /// </summary>
    public class Time
    {
        private int day;
        private int month;
        private int year;
        ThreadPoolTimer PeriodicTimer;
        TimeSpan period = TimeSpan.FromSeconds(10);
        private bool paused = false;

        //initiate the day, month and thread
        public Time() {
            day = 1;
            month = 1;
            year = 2018;
            startThread();
        }

        //thead increases the day by one every ten seconds
        private void startThread()
        {
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                increaseDay();
                //
                // Update the UI thread by using the UI core dispatcher.
                //


            }, period);
        }


        //this method will pauze if the tread is running and resume if it's pauzed
        public void pauzeStartTime()
        {
            if(paused == false)
            {
                PeriodicTimer.Cancel();
                paused = true;
            }
            else
            {
                startThread();
                 paused = false;
            }
            
        }

        //increase the day by one
        private void increaseDay() {
            bool leapYear = false;
            if(year%4 == 0)
            {
                leapYear = true;
            }
            if(new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(month) && day == 31 || new[] { 4, 6, 9, 11 }.Contains(month) && day == 30 || month == 2 && day == 28 && leapYear == false || month == 2 && day == 29 && leapYear == true) {
                increaseMonth();
                day = 1;
            }
            else{
                day++;
            }
        }

        //increase the month by one
        private void increaseMonth()
        {
            if(month >= 12)
            {
                increaseYear();
                month = 1;
            }
            else
            {
                month++;
            }
        }

        // increase the year by one
        private void increaseYear()
        {
            year++;
        }

        //returns the day
        public int getDay()
        {
            return day;
        }

        //returns the month
        public int getMonth()
        {
            return month;
        }

        //returns the year
        public int getYear()
        {
            return year;
        }
    }
}
