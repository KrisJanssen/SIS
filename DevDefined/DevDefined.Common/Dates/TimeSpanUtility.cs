using System;

namespace DevDefined.Common.Dates
{
    public class TimeSpanUtility
    {
        public static string MajorMinorDuration(TimeSpan span)
        {
            double delta = span.TotalSeconds;

            if (delta < 1)
            {
                return "0 seconds";
            }
            else if (delta < 2)
            {
                return "1 second";
            }
            else if (delta < 60)
            {
                return span.Seconds + " seconds";
            }
            else if (delta < (60*60))
            {
                return FormatMajorMinor("minute", span.Minutes, "second", span.Seconds);
            }
            else if (delta < (60*60*24))
            {
                return FormatMajorMinor("hour", span.Hours, "minute", span.Minutes);
            }
            else if (delta < (60*60*24*7))
            {
                return FormatMajorMinor("day", span.Days, "hour", span.Hours);
            }
            else
            {
                return FormatMajorMinor("week", span.Days/7, "day", span.Days%7);
            }
        }

        private static string FormatMajorMinor(string singularMajor, int major, string singularMinor, int minor)
        {
            if (major == 1)
            {
                return major + " " + singularMajor + FormatMinor(singularMinor, minor);
            }
            else
            {
                return major + " " + singularMajor + "s" + FormatMinor(singularMinor, minor);
            }
        }

        private static string FormatMinor(string singluar, int value)
        {
            if (value == 0)
            {
                return "";
            }
            else if (value == 1)
            {
                return ", 1 " + singluar;
            }

            return ", " + value + " " + singluar + "s";
        }

        public static string Ago(TimeSpan span)
        {
            double delta = span.TotalSeconds;

            if (delta <= 1)
            {
                return "a second ago";
            }
            else if (delta < 60)
            {
                return span.Seconds + " seconds ago";
            }
            else if (delta < 120)
            {
                return "about a minute ago";
            }
            else if (delta < (45*60))
            {
                return span.Minutes + " minutes ago";
            }
            else if (delta < (90*60))
            {
                return "about an hour ago";
            }
            else if (delta < (24*60*60))
            {
                int hour = (span.Minutes > 45) ? span.Hours + 1 : span.Hours;
                return "about " + hour + " hours ago";
            }
            else if (delta < (48*60*60))
            {
                return "1 day ago";
            }

            return span.Days + " days ago";
        }
    }
}