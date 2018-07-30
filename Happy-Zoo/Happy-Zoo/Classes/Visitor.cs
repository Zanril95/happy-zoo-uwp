using System;
using Windows.UI.Xaml;

namespace Happy_Zoo
{
    /// <summary>
    /// This class is a timer which can be set in seconds. When the time is elapsed a boolean will be set true.
    /// </summary>
    public class Visitor
    {
        private DispatcherTimer timer1;
        private bool readyToLeave = false;
        int timesTicked = 1;
        int timesToTick = 60;

        //initiate the timer
        public Visitor(int seconds) {
            timesToTick = seconds;
            timer1 = new DispatcherTimer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer1.Start();

        }

        //set boolean readyToLeave on true
        private void timer1_Tick(object sender, object e)
        {
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                readyToLeave = true;
            }
        }

        //returns boolean readyToLeave
        public bool getBool()
        {
            return readyToLeave;
        }
    }
}
