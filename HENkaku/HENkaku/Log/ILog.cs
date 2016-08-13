using System;

namespace HENkaku.Log
{
    interface ILog
    {
        void WriteLine (string line);
        void WriteExceptionError (Exception exception);
        void WriteExceptionWarning (Exception exception);
    }
}
