using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeTracker.View
{
    /// <summary>
    /// Interaction logic for HoddAnalystView.xaml
    /// </summary>
    public partial class HoodAnalystView : Window
    {
        public HoodAnalystView()
        {
            InitializeComponent();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
    }
}
