using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL.Framework
{

    /// <summary>
    /// Handles exceptions
    /// </summary>
    public interface IExceptionHandler
    {
        void Handle(Exception ex);
    }

    public static class ExceptionExtensions
    {
        //public static void Log(this Exception ex)
        //{
        //    var text = new StringBuilder();

        //    text.AppendLine(ex.Message);
        //    if (ex.InnerException != null)
        //    {
        //        text.AppendLine("-<InnerException>-");
        //        text.AppendLine(ex.InnerException.Message);
        //    }

        //    text.AppendLine("-<StackTrace>-");
        //    text.AppendLine(ex.StackTrace);
        //    Trace.TraceError(text.ToString());
        //    Trace.Flush();
        //}
    }
}
