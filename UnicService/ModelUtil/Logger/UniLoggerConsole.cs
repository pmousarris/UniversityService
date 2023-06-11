using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Logger
{
    public class UniLoggerConsole : UniLoggerBase, IUniLogger
    {
        public UniLoggerConsole() : base() { }

        public override void send(string message = null, Exception exception = null, Dictionary<string, string> parameters = null, Priority priority = Priority.Error, [CallerMemberName] string caller = null)
        {
            var time = DateTime.UtcNow;
            var body = buildBody(time, message, exception, parameters, priority, caller);
#if DEBUG
            Debug.WriteLine(body);
#endif
            Console.WriteLine(body);
        }
    }
}
