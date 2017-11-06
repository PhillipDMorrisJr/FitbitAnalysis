using System;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    public static class MinuteConverter
    {
        public static TimeSpan ConvertMinutesToTimeSpan(int minutes)
        {
            if (minutes < 0)
            {
                throw new ArgumentException("Minutes can not be negative");
            }
            var hourinMinutes = 60;
            var minute = minutes % hourinMinutes;
            var hour = minutes / hourinMinutes;

            var timeSpan = new TimeSpan(hour, minute, 0);
            return timeSpan;
        }



    }
}
