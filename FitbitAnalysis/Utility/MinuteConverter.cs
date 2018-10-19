using System;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    public static class MinuteConverter
    {
        #region Methods

        public static TimeSpan ConvertMinutesToTimeSpan(int minutes)
        {
            if (minutes < 0)
            {
                throw new ArgumentException("Minutes can not be negative");
            }
            var minutesInHour = 60;
            var minute = minutes % minutesInHour;
            var hour = minutes / minutesInHour;

            var timeSpan = new TimeSpan(hour, minute, 0);
            return timeSpan;
        }

        #endregion
    }
}