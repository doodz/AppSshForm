using System;
using NLog;

namespace ApptestSsh.UWP.Services
{
    public class Logger : Doods.StdFramework.Interfaces.ILogger
    {
        private readonly NLog.Logger _log;
        bool enableHockeyApp = false;
        public Logger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            _log.Debug(msg);
        }

        public void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
            _log.Error(e);
        }

        public void Error(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            _log.Error(msg);
        }

        public void Info(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            _log.Info(msg);
        }

        public void Warning(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
            _log.Warn(e);
        }

        public void Warning(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            _log.Warn(msg);
        }

        public virtual void TrackPage(string page, string id = null)
        {
            Debug("Evolve Logger: TrackPage: " + page.ToString() + " Id: " + id ?? string.Empty);

            if (!enableHockeyApp)
                return;
        }


        public virtual void Track(string trackIdentifier)
        {
            Debug("Evolve Logger: Track: " + trackIdentifier);

            if (!enableHockeyApp)
                return;
        }

        public virtual void Track(string trackIdentifier, string key, string value)
        {
            Debug("Evolve Logger: Track: " + trackIdentifier + " key: " + key + " value: " + value);

            if (!enableHockeyApp)
                return;

            trackIdentifier = $"{trackIdentifier}-{key}-{@value}";
        }
    }
}
