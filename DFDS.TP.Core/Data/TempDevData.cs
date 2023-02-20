namespace DFDS.TP.Core.Data;

/// <summary>
/// Temporary placeholder for data during development.
/// - To be moved to Database, Config Files, Azure Storage or other before Production.
/// - Much rather have this placeholder, than bits of magic data scattered around the application - hard to find and move.
/// </summary>
public static class TempDevData
{
    /// <summary>
    /// Data regarding logging.
    /// </summary>
    public static class LogData
    {
        public static string DefaultDevLogFolder => "C:/SRC/LOGGING/";

        public static bool AllowLogfilesToCrossDays => false;

        public static int MaxNumberOfLinesPrLogFile => 10000;

        public static string CallerMemberName { get; } = "CallerMemberName";

        public static string CallerFilePath { get; } = "CallerFilePath";

        public static string CallerLineNumber { get; } = "CallerLineNumber";

        public static string CorrelationId { get; } = "CorrelationId";
    }

    /// <summary>
    /// Data regarding Database.
    /// </summary>
    public static class Database
    {
        public static string ConnectionStringProd => @"Data Source=mssql12.unoeuro.com;Initial Catalog=isddb_com_db_prod;Persist Security Info=True;User ID=isddb_com;Password=ElianeQxtgi68I";
        
    }

}