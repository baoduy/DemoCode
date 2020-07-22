using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace DataLayerTests
{
    public static class SqliteMemory
    {
        #region Public Properties

        public static LoggerFactory DebugLoggerFactory { get; } = new LoggerFactory(
            new[] { new DebugLoggerProvider() },
            new LoggerFilterOptions
            {
                Rules = {
                new LoggerFilterRule(null,null, LogLevel.Trace,
                    (s, info, l)
                        =>
                    {
                        return info.Contains("Database.Command");
                    })}
            });

        #endregion Public Properties

        #region Public Methods

        public static DbContextOptionsBuilder UseDebugLogger(this DbContextOptionsBuilder @this) =>
            @this.UseLoggerFactory(DebugLoggerFactory);

        public static DbContextOptionsBuilder<TDbContext> UseDebugLogger<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> @this) where TDbContext : DbContext
            => @this.UseLoggerFactory(DebugLoggerFactory);

        public static DbContextOptionsBuilder<TDbContext> UseSqliteMemory<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> @this) where TDbContext : DbContext
        {
            ((DbContextOptionsBuilder)@this).UseSqliteMemory();
            return @this;
        }

        public static DbContextOptionsBuilder UseSqliteMemory(this DbContextOptionsBuilder @this)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var sqliteConnection = new SqliteConnection(connectionStringBuilder.ToString());
            sqliteConnection.Open();

            @this.UseSqlite(sqliteConnection);
            return @this;
        }

        #endregion Public Methods
    }
}