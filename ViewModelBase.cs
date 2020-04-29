using System.ComponentModel;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications
    /// This class is abstract.
    /// </summary>
    public abstract class ViewModelBase : IModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //public PropertySetter PropertySetter { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
        {
            //PropertySetter = new PropertySetter(this, OnPropertyChanged);
        }
    }
}