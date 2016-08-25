using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DotNetNinja.NotifyPropertyChanged
{
    /// <summary>
    /// Base class for objects that notify properties changes.
    /// </summary>
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        /// <summary>
        /// Event indicating that a property is changed on a component.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Sets value of property and raises PropertyChanged event if the property has changed.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="backingField">Field that backs target property.</param>
        /// <param name="value">Target value.</param>
        /// <param name="propertyExpression">Expression describing property.</param>
        /// <returns>Value indicating whether property value has changed (therefore PropertyChanged was raised).</returns>
        protected bool SetProperty<T>(ref T backingField, T value, Expression<Func<T>> propertyExpression)
            => SetProperty(ref backingField, value, propertyExpression.ExtractPropertyName());

        /// <summary>
        /// Sets value of property and raises PropertyChanged event if the property has changed.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="backingField">Field that backs target property.</param>
        /// <param name="value">Target value.</param>
        /// <param name="propName">Property name.</param>
        /// <returns>Value indicating whether property value has changed (therefore PropertyChanged was raised).</returns>
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            var changed = !EqualityComparer<T>.Default.Equals(backingField, value);

            if (changed)
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }

            return changed;
        }
    }
}
