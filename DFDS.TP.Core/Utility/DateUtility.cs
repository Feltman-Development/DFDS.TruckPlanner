namespace DFDS.TP.Core.Utility;

public static class DateUtility
{
    /// <summary>
    /// Get the date formatted for UI.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetUIDate(DateTime date = default) => date != default ? $"{date.Year}-{date.Month}-{date.Day}" : $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month}-{DateTime.UtcNow.Day}";

    #region Extensions

    /// <summary>
    /// Get the date formatted for UI.
    /// </summary>
    public static string AsUIDate(this DateTime date) => GetUIDate(date);

    /// <summary>
    /// Get the date formatted for UI.
    /// </summary>
    public static DateOnly AsDateOnly(this DateTime date) => new(date.Year, date.Month, date.Day);
    
    #endregion Extensions   
}
