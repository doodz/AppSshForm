using System;
namespace Doods.StdFramework.Interfaces
{
    public static class DoodsLoggerKeys
    {
        public const string WiFiConfig = "WiFiConfigured";
        public const string CopyPassword = "CopyPassword";
        public const string LoginSuccess = "LoginSuccess";
        public const string LoginFailure = "LoginFailure";
        public const string LoginCancel = "LoginCancel";
        public const string LoginTime = "LoginTime";
        public const string Signup = "Signup";
        public const string FavoriteAdded = "FavoriteAdded";
        public const string FavoriteRemoved = "FavoriteRemoved";
        public const string ReminderAdded = "ReminderAdded";
        public const string ReminderRemoved = "ReminderRemoved";
        public const string Share = "Share";
        public const string LeaveFeedback = "LeaveFeedback";
        public const string ManualSync = "ManualSync";
        public const string LaunchedBrowser = "LaunchedBrowser";
        public const string CallHotel = "CallHotel";

        public const string Logout = "Logout";
    }
    public interface ILogger
    {
        void Error(string msg);
        void Error(Exception e);

        void Warning(string msg);
        void Warning(Exception e);

        void Info(string msg);

        void Debug(string msg);

        void TrackPage(string page, string id = null);
        void Track(string trackIdentifier);
        void Track(string trackIdentifier, string key, string value);
    }
}
