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


using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.esriSystem;
//using ESRI.ArcGIS.MapControl;
using EmployeeTracker.ViewModel;
using EmployeeTracker.Common;
using ESRI.ArcGIS.Controls;


namespace EmployeeTracker.View
{
    /// <summary>
    /// Interaction logic for MapWorkSpaceView.xaml
    /// </summary>
    public partial class MapWorkSpaceView : System.Windows.Controls.UserControl 
    {

        private AxMapControl _mapControl; 
        MapWorkSpaceViewModel mwsv;

        public MapWorkSpaceView() //AxMapControl mapControl
        {
            InitializeComponent();          
        }     

        // Create an AxMapControl and host it in the WindowsFormsHost element.
        private void CreateMapControl()
        {
            mwsv = ((MapWorkSpaceViewModel)this.DataContext);          
            _mapControl = mwsv.currentMap.Model;
        
            MapHost.Child = _mapControl;
        }

        private void SetMapProperties()
        {
            //Set the properties of AxMapControl.
            _mapControl.Dock = DockStyle.None;
           
            _mapControl.BackColor = System.Drawing.Color.FromArgb(233, 233, 233);                       
        }

        private void WireMapEvents()
        {
           // _mapControl.OnMouseMove += _mapControl_OnMouseMove;
        }

        void _mapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            //throw new NotImplementedException();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMapControl();
            SetMapProperties();
            WireMapEvents();
           
        }


    }
}
