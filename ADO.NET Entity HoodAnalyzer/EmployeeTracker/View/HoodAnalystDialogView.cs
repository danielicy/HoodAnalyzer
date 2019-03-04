using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeTracker.Interfaces;
using EmployeeTracker.View;

namespace EmployeeTracker.View
{
    public class HoodAnalystDialogView :IModalDialog 
    {

        private HoodAnalystView view;

        #region IModalDialog Members

        void IModalDialog.BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetAnalystViewDialog().DataContext = viewModel;
        }

        void IModalDialog.ShowDialog()
        {
            GetAnalystViewDialog().ShowDialog();
        }

        void IModalDialog.Close()
        {
            GetAnalystViewDialog().Close();
        }

        #endregion

        private HoodAnalystView GetAnalystViewDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new HoodAnalystView();
                view.Width = 530;
                view.Closed += new EventHandler(view_Closed);
            }
            return view;
        }

        void view_Closed(object sender, EventArgs e)
        {
            view = null;
        }

    }
}
