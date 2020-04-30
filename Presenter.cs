using System;
using System.Windows.Forms;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// Represents a presenter in a Windows Forms Model-View-Presenter application.
    /// </summary>
    /// <typeparam name="TView">The type of the view controlled by this <see cref="Presenter"/>.</typeparam>
    public abstract class DialogPresenter<TView> : Presenter<TView>
        where TView : class, IDialogView
    {
        /// <summary>
        /// Creates an instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="view">The view object the presenter is working with</param>
        public DialogPresenter(TView view) : base(view)
        {
            view.OnViewShown += View_Shown;

            view.OnViewAccepted += View_OnViewAccepted;
            view.OnViewCanceled += View_OnViewCanceled;
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();

            view.MaximizeBox = false;
            view.MinimizeBox = false;
            view.StartPosition = FormStartPosition.CenterScreen;
            view.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void View_OnViewCanceled(object sender, EventArgs e)
        {
            //TODO : Is Ok?
            //if (IsValid())
            //{
                OnViewCancelled();
            //}
        }

        private void View_OnViewAccepted(object sender, EventArgs e)
        {
            if (IsValid())
            {
                OnViewAccepted();
            }
        }

        private void View_Shown(object sender, EventArgs e)
        {
            OnViewShown();
        }

        protected virtual void OnViewShown()
        {

        }

        protected virtual void OnViewAccepted()
        {

        }

        protected virtual void OnViewCancelled()
        {

        }
    }

    /// <summary>
    /// Represents a presenter in a Windows Forms Model-View-Presenter application.
    /// </summary>
    /// <typeparam name="TView">The type of the view controlled by this <see cref="Presenter"/>.</typeparam>
    public abstract class Presenter<TView> : IPresenter<TView>
        where TView : class, IView
    {
        /// <summary>
        /// The view object that the Presenter is working with
        /// </summary>
        protected readonly TView view = default(TView);

        /// <summary>
        /// Creates an instance of the <see cref="Presenter"/> class.
        /// </summary>
        public Presenter()
        {

        }

        /// <summary>
        /// Creates an instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="view">The view object the presenter is working with</param>
        public Presenter(TView view)
        {
            // Check for empty or non existance views.
            if (view == null)
                throw new ArgumentNullException("view");

            // Sets the view instance.
            this.view = view;

            OnViewCreated();

            // Attach basic events.
            this.view.OnViewLoaded += View_Load;
            this.view.OnViewClosed += View_Closed;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Presenter()
        {

        }

        /// <summary>
        /// Gets the view instance that this presenter is bound to.
        /// </summary>
        public TView View
        {
            get { return view; }
        }

        protected virtual void OnViewCreated()
        {

        }

        protected virtual void OnViewLoaded()
        {

        }

        protected virtual void OnViewClosed()
        {

        }

        private void View_Load(object sender, EventArgs e)
        {
            OnViewLoaded();
        }

        private void View_Closed(object sender, EventArgs e)
        {
            OnViewClosed();
        }

        /// <summary>
        /// Checks if current view is valid.
        /// </summary>
        /// <returns>Returns true if the view passes the validation.</returns>
        protected virtual bool IsValid()
        {
            return true;
        }
    }

    public abstract class Presenter<TView, TModel> : Presenter<TView>
        where TView : class, IView<TModel>
        where TModel : ViewModelBase, new()
    {
        //public Presenter()
        //{
        //    View.Model = new TModel();
        //}

        /// <summary>
        /// Creates an instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="view">The view object the presenter is working with</param>
        public Presenter(TView view) : base(view)
        {
            View.Model = new TModel();
        }
    }
}