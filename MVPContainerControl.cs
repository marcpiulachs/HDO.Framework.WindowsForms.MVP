﻿using System;
using System.Windows.Forms;

namespace HDO.Framework.WindowsForms.MVP
{
    /// <summary>
    /// Represents the base class form MVP UserControl views.
    /// </summary>
    public partial class MVPContainerControl : Panel , IView
    {
        public event EventHandler OnViewClosed;
        public event EventHandler OnViewLoaded;
        public event EventHandler OnViewActivated;

        /// <summary>
        /// Holds a refernce to the presenter bindier for this view.
        /// </summary>
        private readonly PresenterBinder presenterBinder = new PresenterBinder();

        /// <summary>
        /// Creates a new instance of the <see cref="MVPUserControl"/> class.
        /// </summary>
        public MVPContainerControl()
        {
            InitializeComponent();

            // Attach the presenter binding
            presenterBinder.PerformBinding(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        
            if (DesignMode)
                return;

            if (OnViewLoaded != null)
                OnViewLoaded(this, EventArgs.Empty);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            if (DesignMode)
                return;

            if (OnViewClosed != null)
                OnViewClosed(this, EventArgs.Empty);
        }
    }
}