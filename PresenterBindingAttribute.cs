using System;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// Used to define bindings between presenters and a views.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PresenterBindingAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type PresenterType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type ViewType { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PresenterBindingAttribute"/> class.
        /// </summary>
        /// <param name="presenterType">The <see cref="IPresenter{T}"/> type.</param>
        public PresenterBindingAttribute(Type presenterType)
        {
            PresenterType = presenterType;
            ViewType = null;
        }
    }
}