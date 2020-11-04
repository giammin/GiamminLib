using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GiamminLib.ExtensionMethods
{
#nullable enable
    /// <summary>
    /// Extension Methods per <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public static class NotifyPropertyChangedExtensions
    {
        /// <summary>
        /// Se il valore <paramref name="value"/> è differente da <paramref name="field"/> lo setta e lancia l'evento <paramref name="handler"/>
        /// </summary>
        /// <typeparam name="T">il tipo della proprietà</typeparam>
        /// <param name="sender"></param>
        /// <param name="handler">l'evento da lanciare</param>
        /// <param name="field">il valore della proprietà</param>
        /// <param name="value">il nuovo valore</param>
        /// <param name="propertyName">il nome della proprietà</param>
        /// <param name="equalityComparer">eventuale  EqualityComparer da utilizzare se non va bene quello di default</param>
        /// <returns>ritorna true se il valore`era differente e quindi la proprietà è stata aggiornata e l'evento lanciato</returns>
        public static bool SetPropertyAndNotify<T>(this INotifyPropertyChanged sender, PropertyChangedEventHandler handler, ref T field, T value, [CallerMemberName] string propertyName = "", EqualityComparer<T>? equalityComparer = null)
        {
            bool rtn = false;
            var eqComp = equalityComparer ?? EqualityComparer<T>.Default;
            if (!eqComp.Equals(field,value))
            {
                field = value;
                rtn = true;
                if (handler != null)
                {
                    var args = new PropertyChangedEventArgs(propertyName);
                    handler(sender, args);
                }
            }
            return rtn;
        }
    }
	
	//todo da sistemare
	 public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}