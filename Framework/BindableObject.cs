using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL.Framework
{
    /// <summary>
    /// INotifyPropertyChanged base implementation
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        #region -=Instance members=-

        #region -=INotifyPropertyChanged members=-

        public event PropertyChangedEventHandler PropertyChanged;


        private Dictionary<string, object> backingFields = new Dictionary<string, object>();

        /// <summary>
        /// Returns property value of internally stored backing field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected T Get<T>([CallerMemberName] string propertyName = null, T defaultValue = default(T))
        {
            if (!backingFields.ContainsKey(propertyName))
                backingFields[propertyName] = defaultValue;
            return (T)backingFields[propertyName];
        }
        /// <summary>
        /// Set property by using internally stored backing field and raise PropertyChanged event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            var currentValue = Get<T>(propertyName);
            return Set(ref currentValue, value, () =>
            {
                backingFields[propertyName] = value;
                return true;
            }, propertyName);
        }

        /// <summary>
        /// Set property and raise PropertyChanged event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="store"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T store, T value, [CallerMemberName] string propertyName = null)
            => Set(ref store, value, () => true, propertyName);
        /// <summary>
        /// Set property value and raise PropertyChanged events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="store"></param>
        /// <param name="value"></param>
        /// <param name="canChange"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool Set<T>(ref T store, T value, Func<bool> canChange, [CallerMemberName] string propertyName = null)
        {
            var canChangeValue = canChange();
            if (canChangeValue)
            {
                store = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #endregion
    }
}
