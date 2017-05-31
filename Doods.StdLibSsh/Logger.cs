using System;
using NLog;

namespace Doods.StdLibSsh
{
    internal class Logger
    {
        private static Logger _instance = null;
        private static readonly object Padlock = new object();
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Padlock)
                    {
                        var loo = new Logger();

                        return _instance ?? (_instance = new Doods.StdLibSsh.Logger());
                    }
                }
                return _instance;
            }
        }

        private readonly NLog.Logger _log;

        private Logger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Error(Exception e)
        {
            _log.Error(e);
        }

        public void Error(string msg)
        {
            _log.Error(msg);
        }

        public void Info(string msg)
        {
            _log.Info(msg);
        }

        public void Warning(Exception e)
        {
            _log.Warn(e);
        }

        public void Warning(string msg)
        {
            _log.Warn(msg);
        }
    }
}
