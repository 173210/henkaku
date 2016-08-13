using System;
using Xamarin.Forms;

namespace HENkaku.Log
{
    class Frontend : ILog
    {
        private ILog log;

        public Frontend (ILog initialLog)
        {
            log = initialLog;
        }

        public void WriteLine (string line)
        {
            Device.BeginInvokeOnMainThread (() =>
            {
                log.WriteLine (line);
            });
        }

        public void WriteExceptionError (Exception exception)
        {
            Device.BeginInvokeOnMainThread (() =>
            {
                WriteExceptionError (exception);
            });
        }

        public void WriteExceptionWarning (Exception exception)
        {
            Device.BeginInvokeOnMainThread (() =>
            {
                WriteExceptionWarning (exception);
            });
        }
    }
}
