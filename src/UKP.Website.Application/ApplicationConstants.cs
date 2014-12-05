﻿using System.Globalization;

namespace UKP.Website.Application
{
    public static class ApplicationConstants
    {
        public const string DateFormat = "dddd d MMMM h.mmtt";
        public const string YearDateTimeFormat = "dddd d MMMM yyyy h.mmtt";
        public const string YearDateFormat = "dddd d MMMM yyyy";
        public const string YearDateTimeWithoutMeridiemFormat = "dddd d MMMM yyyy h.mm";
        public const string MerdiemFormat = "tt";
        public static DateTimeFormatInfo DateTimeFormatter = new DateTimeFormatInfo() { AMDesignator = "am", PMDesignator = "pm" };
        public const string AcceptCookieName = "Cookies";
        public const int EpgHourWidth = 120;
    }
}
