using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// A strategy for discovery presenters based on [PresenterBinding] attributes being placed
    /// on views and view hosts.
    /// </summary>
    public class AttributeBasedPresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        public PresenterBinding GetBinding(IView viewInstance)
        {
            IPresenter presenter = null;

            Type t = viewInstance.GetType();    //获取该视图的类类型

            object[] attrs = t.GetCustomAttributes(typeof(PresenterBindingAttribute), true);    //获取该类上的附加特性集合
            
            foreach (PresenterBindingAttribute pba in attrs)
            {
                //Type newt = pba.PresenterType;    //获取Presenter类类型
                //                                  //建立Presenter实例，这里的构造参数是View的对象，这样就使两者建立了联系
                //Object obj = Activator.CreateInstance(pba.PresenterType, viewInstance);
                //presenter = obj as IPresenter;
                
                return new PresenterBinding(pba.PresenterType, t, viewInstance);
            }

            return null;
        }
    }

    /// <summary>
    /// Defines that contract for classes that implement logic for finding relevant presenters given
    /// some hosts and some view instances.
    /// </summary>
    public interface IPresenterDiscoveryStrategy
    {
        /// <summary>
        /// Gets the presenter binding for passed views.
        /// </summary>
        /// <param name="viewInstance">A view instances (user control, form, ...).</param>
        PresenterBinding GetBinding(IView viewInstance);
    }

    /// <summary/>
    public class PresenterBinding
    {
        readonly Type presenterType;
        readonly Type viewType;
        readonly IView viewInstance;

        /// <summary/>
        public PresenterBinding(
            Type presenterType,
            Type viewType,
            IView viewInstance)
        {
            this.presenterType = presenterType;
            this.viewType = viewType;
            this.viewInstance = viewInstance;
        }

        /// <summary/>
        public Type PresenterType
        {
            get { return presenterType; }
        }

        /// <summary/>
        public Type ViewType
        {
            get { return viewType; }
        }

        /// <summary/>
        public IView ViewInstance
        {
            get { return viewInstance; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="PresenterBinding"/> is equal to the current <see cref="PresenterBinding"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="PresenterBinding"/> is equal to the current <see cref="PresenterBinding"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="PresenterBinding"/> to compare with the current <see cref="PresenterBinding"/>.</param>
        public override bool Equals(object obj)
        {
            var target = obj as PresenterBinding;
            if (target == null) return false;

            return
                PresenterType == target.PresenterType &&
                ViewType == target.ViewType &&
                ViewInstance.Equals(target.ViewInstance); // todo: override Equals of IView perhaps.
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="PresenterBinding"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return
                PresenterType.GetHashCode() |
                ViewType.GetHashCode() |
                ViewInstance.GetHashCode();
        }
    }

    /// <summary>
    /// Defines the methods of a factory class capable of creating presenters.
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        /// Creates a new instance of the specific presenter type, for the specified
        /// view type and instance.
        /// </summary>
        /// <param name="presenterType">The type of presenter to create.</param>
        /// <param name="viewType">The type of the view as defined by the binding that matched.</param>
        /// <param name="viewInstance">The view instance to bind this presenter to.</param>
        /// <returns>An instantitated presenter.</returns>
        IPresenter Create(Type presenterType, Type viewType, IView viewInstance);

        /// <summary>
        /// Releases the specified presenter from any of its lifestyle demands.
        /// This method's activities are implementation specific - for example,
        /// an IoC based factory would return the presenter to the container.
        /// </summary>
        /// <param name="presenter">The presenter to release.</param>
        void Release(IPresenter presenter);
    }

    public class DefaultPresenterFactory : IPresenterFactory
    {
        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            // Returns the presenter
            return Activator.CreateInstance(presenterType, viewInstance) as IPresenter;
        }

        public void Release(IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class PresenterBinder
    {
        private static IPresenterFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterBinder"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="httpContext">The owning HTTP context.</param>
        public PresenterBinder()
        {

        }

        ///<summary>
        /// Gets or sets the factory that the binder will use to create
        /// new presenter instances. This is pre-initialized to a
        /// default implementation but can be overriden if desired.
        /// This property can only be set once.
        ///</summary>
        ///<exception cref="ArgumentNullException">Thrown if a null value is passed to the setter.</exception>
        ///<exception cref="InvalidOperationException">Thrown if the property is being set for a second time.</exception>
        public static IPresenterFactory Factory
        {
            get
            {
                if (factory == null)
                    factory = new DefaultPresenterFactory();

                return factory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (factory != null)
                {
                    throw new InvalidOperationException(
                        factory is DefaultPresenterFactory
                            ? "The factory has already been set, and can be not changed at a later time. In this case, it has been set to the default implementation. This happens if the factory is used before being explicitly set. If you wanted to supply your own factory, you need to do this in your Application_Start event."
                            : "You can only set your factory once, and should really do this in Application_Start.");
                }

                factory = value;
            }
        }

        /// <summary>
        /// Attempts to bind any already registered views.
        /// </summary>
        public void PerformBinding(IView viewInstance)
        {
            try
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return;

                PerformBinding(
                    viewInstance,
                    Factory);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " " + e.ToString());
            }
        }

        private static IPresenter PerformBinding(
            IView candidate,
            IPresenterFactory presenterFactory)
        {
            var a = new AttributeBasedPresenterDiscoveryStrategy();

            PresenterBinding b = a.GetBinding(candidate);
            
            var newPresenter = BuildPresenter(
                presenterFactory,
                a.GetBinding(candidate),
                candidate);

            return newPresenter;
        }

        private static IPresenter BuildPresenter(
                   IPresenterFactory presenterFactory,
                   PresenterBinding binding,
                   IView viewInstance)
        {
            var presenter = presenterFactory.Create(
                binding.PresenterType, binding.ViewType, viewInstance);

            return presenter;
        }
    }
}
