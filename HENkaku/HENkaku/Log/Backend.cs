using System;
using Xamarin.Forms;

namespace HENkaku.Log
{
    class Backend : ILog
    {
        private FormattedString log = "";

        public FormattedString Get ()
        {
            return log;
        }

        public void Clear ()
        {
            log.Spans.Clear ();
        }

        public void WriteLine (string line)
        {
            var span = new Span {
                Text = line + "\n"
            };

            log.Spans.Add (span);
        }

        private void WriteException (Exception exception, Color color)
        {
            var span = new Span {
                Text = exception.Message + "\n"
                    + exception.StackTrace + "\n"
                    + "Help: " + exception.HelpLink + "\n",
                ForegroundColor = color
            };

            log.Spans.Add (span);
        }

        public void WriteExceptionError (Exception exception)
        {
            WriteException (exception, Color.Red);
        }

        public void WriteExceptionWarning (Exception exception)
        {
            WriteException (exception, Color.Yellow);
        }
    }
}
