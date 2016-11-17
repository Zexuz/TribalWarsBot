using System;

namespace TribalWarsBot.Helpers {

    public class TimeFormater {

        public static string TimeSpanToString(TimeSpan timeSpan) {
            return Math.Floor(timeSpan.TotalHours) + timeSpan.ToString("'h 'm'm 's's'");
        }

    }

}