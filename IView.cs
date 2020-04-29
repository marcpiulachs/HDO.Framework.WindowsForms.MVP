using System;
using System.Drawing;
using System.Windows.Forms;

namespace HDO.Framework.WindowsForms.MVP
{
    public interface IControlView : IView
    {

    }

    public interface IDialogView : IView
    {
        DialogResult DialogResult { get; set; }

        void Close();
        void Hide();
        void Show();

        void Accept();
        void Cancel();

        Cursor Cursor { get; set; }

        event EventHandler OnViewAccepted;
        event EventHandler OnViewCanceled;

        event EventHandler OnViewShown;

        DialogResult ShowDialog();
        DialogResult ShowDialog(IWin32Window owner);

        string Text { get; set; }
        bool TopMost { get; set; }

        Image BackgroundImage { get; set; }
        ImageLayout BackgroundImageLayout { get; set; }

        FormStartPosition StartPosition { get; set; }
        bool MinimizeBox { get; set; }
        bool MaximizeBox { get; set; }
        bool ControlBox { get; set; }
        FormBorderStyle FormBorderStyle { get; set; }
        FormWindowState WindowState { get; set; }

        bool IsValid();
    }

    /// <summary>
    /// Represents a class that is a view in a Windows forms Model-View-Presenter application.
    /// </summary>
    public interface IView : IWin32Window
    {
        event EventHandler OnViewActivated;
        event EventHandler OnViewLoaded;
        event EventHandler OnViewClosed;

        Font Font { get; set; }

        bool Visible { get; set; }
    }

    /// <summary>
    /// Represents a class that is a view with a strongly typed view model in a Windows Forms Model-View-Presenter application.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IView<TModel> : IView
        where TModel : ViewModelBase, new()
    {
        /// <summary>
        /// Gets the model instance. The default presenter base class
        /// (<see cref="DialogPresenter{TView}"/>) initializes this automatically.
        /// </summary>
        TModel Model { get; set; }
    }

    public interface IDialogView<TModel> : IDialogView
        where TModel : ViewModelBase, new()
    {
        /// <summary>
        /// Gets the model instance. The default presenter base class
        /// (<see cref="DialogPresenter{TView}"/>) initializes this automatically.
        /// </summary>
        TModel Model { get; set; }
    }

    /// <summary>
    /// Provides the base interface for models.
    /// </summary>
    public interface IModel
    {

    }
}