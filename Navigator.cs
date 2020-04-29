using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDO.WindowsForms.MVP
{
    // NavigationManager is used to transition (or navigate) 
    // between views
    public class NavigationManager
    {
        private object args = null;

        public object Args
        {
            get { return args; }
        }

        // use this override if your Presenter are non-persistent (transient)
        public DialogResult NavigateTo(IView view, object args = null)
        {
            // Set the arguments
            this.args = args;

            // Show the dialog
            return view.ShowDialog();
        }
    }
}