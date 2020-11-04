using System;

namespace GiamminLib.ExtensionMethods
{
	///<summary>
	/// Estende le funzionalità del <see cref="DateTime"/>
	/// <remarks> per calcoli particolari sulle date vedere  il progetto FluentDateTime su codeplex/</remarks>
	///</summary>
	public static class DateTimeExtension
	{
        /// <summary>
        /// Calcola il numero di Ticks per creare la data nel formato Json Es: /Date(1245398693390)/ 
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns> il numero di ticks per inizializzare l'oggetto javascript date</returns>
        public static long? ToJsonTicks(this DateTime? dateTime) => dateTime?.ToJsonTicks();

        /// <summary>
        /// Calcola il numero di Ticks per creare la data nel formato Json Es: /Date(1245398693390)/ 
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns> il numero di ticks per inizializzare l'oggetto javascript date</returns>
        public static long ToJsonTicks(this DateTime dateTime) => (dateTime.ToUniversalTime().Ticks - Constants.UnixEpochTicks) / 10000;

        /// <summary>
        /// Determina se il valore può essere salvato sul database e quindi è >= di <see cref="Constants.MinSqlValue"/>
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public static bool IsValidSqlDateTime(this DateTime dateTime) => dateTime >= Constants.MinSqlValue;
        /// <summary>
        /// ritorna la data da cui partire per calcolare range annuale in base alla data di sottoscrizione
        /// subscriptionDate: 2007-06-29
        /// today: 2015-04-29 --> 2014-06-29
        /// today: 2015-06-29 --> 2015-06-29
        /// today: 2015-06-29 --> 2015-06-29
        /// </summary>
        /// <param name="subscriptionDate">data di iscrizione</param>
        /// <param name="today">data alla quale si vuole trovare il range annuale</param>
        /// <returns>range annuale in base alla data di sottoscrizione e la data indicata</returns>
        public static DateTime GetYearSubscriptionDate(this DateTime subscriptionDate,DateTime today)
        {
            int years = today.Year - subscriptionDate.Year;
			DateTime candidate = subscriptionDate.AddYears(years);
			return candidate <= today ? candidate : subscriptionDate.AddYears(years - 1);
        }
	}
}
