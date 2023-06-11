using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Logger
{
    public abstract class UniLoggerBase : IUniLogger
    {
        protected UniLoggerBase() { }

        /// <summary>
        /// Build a log entry string containing date, priority, caller, message, exception and parameters.
        /// </summary>
        /// <param name="time">The timestamp of the log entry.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">The exception to be logged. If no exception should be logged, this parameter can be null.</param>
        /// <param name="parameters">A dictionary containing any parameters related to the log entry. If no parameters should be logged, this parameter can be null.</param>
        /// <param name="priority">The priority level of the log entry.</param>
        /// <param name="caller">The caller of the log entry.</param>
        /// <returns>A string containing a detailed log entry.</returns>
        internal virtual string buildBody(DateTime time, string message, Exception exception, Dictionary<string, string> parameters, Priority priority, string caller)
        {
            var body = string.Format("DateTime: {1}{0}Priority: {2}{0}Caller: {3}{0}", Environment.NewLine, time, priority, caller);
           
            if (!string.IsNullOrEmpty(message))
                body += $"Message: {message.Trim()}{Environment.NewLine}";

            if (exception != null)
                body += string.Format("Exception: {1}{0}StackTrace: {2}{0}", Environment.NewLine, Util.GetExceptionFullMessage(exception), exception.StackTrace.Trim());

            if (parameters != null && parameters.Count > 0)
            {
                body += $"*Parameters*{Environment.NewLine}";
                foreach (var kvp in parameters)
                    body += $"{kvp.Key}: {kvp.Value}{Environment.NewLine}";
            }

            return body;
        }

        public abstract void send(string message = null, Exception exception = null, Dictionary<string, string> parameters = null, Priority priority = Priority.Error, [CallerMemberName] string caller = null);
    }
}
