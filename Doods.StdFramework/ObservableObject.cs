using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.System.ComponentModel;

namespace Doods.StdFramework
{
    /// <summary>
    /// Implémente l’interface <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Définit la nouvelle valeur.
        /// </summary>
        /// <typeparam name="T">Le type de la propriété à modifier.</typeparam>
        /// <param name="currentValue">La valeur actuel.</param>
        /// <param name="newValue">La nouvelle valeur.</param>
        /// <param name="propertyName">Le nom de la propriété.</param>
        /// <returns><c>Ture</c>, s'il y a eu un changement.</returns>
        protected bool SetProperty<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            return PropertyChanged.SetProperty(this, ref currentValue, newValue, propertyName);
        }

        protected void SetPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    namespace System.ComponentModel
    {
        public static class ObservableObject
        {
            //Just adding some new funk.tionality to System.ComponentModel
            public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
            {
                if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                    return false;

                currentValue = newValue;

                var dirty = sender as IDirty;

                if (dirty != null)
                    dirty.IsDirty = true;

                handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
                return true;
            }
        }
    }
}
