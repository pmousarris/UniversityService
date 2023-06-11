using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Logger
{
    public interface IUniLogger
    {
        void send(string message = null, Exception exception = null, Dictionary<string, string> parameters = null, Priority priority = Priority.Error, [CallerMemberName] string caller = null);
    }
}
