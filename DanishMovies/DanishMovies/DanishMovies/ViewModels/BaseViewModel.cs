using DanishMovies.Interfaces.Services;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DanishMovies
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public II18N Strings => I18N.Current;

        public INavigationService Navigation
        {
            get
            {
                return App.Navigation;
            }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Helper method to set a property and call OnPropertyChanged
        /// as part of the INotifyPropertyChanged interface.
        /// </summary>
        /// <typeparam name="T">The property</typeparam>
        /// <param name="backingStore">Property private value</param>
        /// <param name="value">Property new value</param>
        /// <param name="propertyName">We get property name via CallerMemberName attribute</param>
        /// <param name="onChanged">Optional: Action when property changes</param>
        /// <returns>True if property changed otherwise False</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value; // Set private property value.
            onChanged?.Invoke(); // Optional: Invoke property change action...
            OnPropertyChanged(propertyName); // Notify UI of property change.
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion
    }
}
