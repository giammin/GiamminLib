using System;
using System.Data.SqlTypes;

namespace GiamminLib.ExtensionMethods;

public static class DateTimeExtension
{
    /// <summary>
    /// Return the number of Ticks for creating a javascript/json date Es: /Date(1245398693390)/ 
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns>number of thicks</returns>
    public static long ToJsonTicks(this DateTime dateTime) => (dateTime.ToUniversalTime().Ticks - Constants.UnixEpochTicks) / 10000;

    /// <summary>
    /// check if the date can be safely stored in a sql database
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    public static bool IsValidSqlDateTime(this DateTime dateTime) => dateTime.IsBetween(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value);
    
}