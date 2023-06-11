using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil
{
    public static class Util
    {
        /// <summary>
        /// Get the full message of an exception, including inner exceptions.
        /// </summary>
        /// <param name="e">The exception for which to retrieve the full message.</param>
        /// <returns>A string containing the full message of the exception, including inner exceptions. If the provided exception is null, an empty string is returned.</returns>
        public static string GetExceptionFullMessage(Exception e)
        {
            if (e != null)
                return $"{GetExceptionFullMessage(e.InnerException)}\n{e.Message}";
            return string.Empty;
        }
    }
}
