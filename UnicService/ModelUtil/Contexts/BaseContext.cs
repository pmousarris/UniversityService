using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Contexts
{
    public abstract class BaseContext : DbContext
    {
#if DEBUG
        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
#endif

        protected BaseContext([NotNull] DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging().UseLoggerFactory(LoggerFactory);
#endif
        }
    }
}
