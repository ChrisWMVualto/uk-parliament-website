using System.Globalization;

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
        public const string AcceptCookieName = "Cookies_80709178-f878-4b24-9345-d928521c5c30";
        public const int EpgItemMinWidth = 240;
    }
}
