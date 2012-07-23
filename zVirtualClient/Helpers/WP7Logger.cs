using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Helpers
{
    public class WP7Logger<T> : ILog
    {


        public void Debug(object message)
        {
            //if(IsDebugEnabled) Logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            //if (IsDebugEnabled) Logger.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            //if (IsDebugEnabled) Logger.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            //if (IsInfoEnabled) Logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            //if (IsInfoEnabled) Logger.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            //if (IsInfoEnabled) Logger.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            //if (IsWarnEnabled) Logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            //if (IsWarnEnabled) Logger.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            //if (IsWarnEnabled) Logger.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            //if (IsErrorEnabled) Logger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            //if (IsErrorEnabled) Logger.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            //if (IsErrorEnabled) Logger.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            //if (IsFatalEnabled) Logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            //if (IsFatalEnabled) Logger.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            //if (IsFatalEnabled) Logger.FatalFormat(format, args);
        }

        public bool IsDebugEnabled
        {
            //get { return Logger.IsDebugEnabled; }
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            //            get { return Logger.IsInfoEnabled; }
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            //          get { return Logger.IsWarnEnabled; }
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            //    get { return Logger.IsErrorEnabled; }
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            //    get { return Logger.IsFatalEnabled; }
            get { return false; }

        }
    }
}