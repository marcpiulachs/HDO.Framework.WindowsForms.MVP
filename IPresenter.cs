using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// Represents a class that is a presenter with a strongly typed view in a Windows forms Model-View-Presenter application.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    public interface IPresenter<T> : IPresenter 
        where T : class, IView
    {
        /// <summary>
        /// Gets the view instance that this presenter is bound to.
        /// </summary>
        T View { get; }
    }

    /// <summary>
    /// Represents a class that is a presenter in a Windows forms application.
    /// </summary>
    public interface IPresenter
    {

    }
}
