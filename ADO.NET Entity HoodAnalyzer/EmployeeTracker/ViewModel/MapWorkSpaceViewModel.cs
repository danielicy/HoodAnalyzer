using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeTracker.View;
using System.Windows.Input;
//using ESRI.ArcGIS.MapControl;
using ESRI.ArcGIS.Carto ;
using System.Windows.Forms;

using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Geodatabase;
//using ESRI.ArcGIS.DataSourcesGDB;
//using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Framework;
using EmployeeTracker.Interfaces;
using System.Collections.ObjectModel;
using ESRI.ArcGIS.Controls;


namespace EmployeeTracker.ViewModel
{
    using EmployeeTracker.ViewModel.Helpers;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model; 

    public class MapWorkSpaceViewModel : ViewModelBase
    {

        /// <summary>
        /// The deprtment currently selected in the workspace
        /// </summary>
        public MapViewModel  currentMap;

        /// <summary>
        /// UnitOfWork for managing changes
        /// </summary>
        private IUnitOfWork unitOfWork;
        //private IMapDocument _mapDoc;
        //private IMap imap;

        private MapObject  _axMap;

        private ObservableCollection<LayersClass> _layersCollection;

        public ObservableCollection<LayersClass> LayersCollection
        {
            get { 
                return _layersCollection; 
            }
             set {
                _layersCollection = value;
            this.OnPropertyChanged("LayersCollection");
            }
        }

        public MapWorkSpaceViewModel(MapObject mapObject, IUnitOfWork unitOfWork)//(MapViewModel mapViewModel, IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
            this.currentMap = new MapViewModel(mapObject);

            _axMap = mapObject;
            LayersCollection = currentMap.Model.LayersCollection;
            _axMap.OnMapReplaced += _axMap_OnMapReplaced;

            this.ShowHoodCommand = new DelegateCommand((o) => this.ShowHoodDialog());
          
        }

        void _axMap_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            MapObject mO = (MapObject)sender;
            LayersCollection = mO.LayersCollection;
        }
        
       // public HoodAnalystWorkSpaceViewModel HoodAnalystWorkspace { get; private set; }
                
       // public ICommand HoodAnalystOpenCommand { get; private set; }

        public ICommand ShowHoodCommand { get; private set; }

       
        private void ShowHoodDialog()
        {
           
           // customer.Mode = Mode.Add;

            IModalDialog dialog = EmployeeTracker.Services.ServiceProvider.Instance.Get<IModalDialog>();
            HoodViewModel HoodAnalyst = new HoodViewModel(_axMap );
            dialog.BindViewModel(HoodAnalyst);
            dialog.ShowDialog();
        } 
        
       // private  HoodAnalyzerModel _hAnalyzerModel;

          
        //private void HoodAnalystOpen() 
        //{
        //    _hAnalyzerModel = new HoodAnalyzerModel();
        //    HoodAnalystWorkspace = new HoodAnalystWorkSpaceViewModel(_hAnalyzerModel);            
        //    this.OnPropertyChanged("HoodAnalystWorkspace");          
        //}

        // mapControl = new AxMapControl();
           // mapHost.Child= mapControl;
        //public ICommand openMapCommand {get;private set;}

        //public ICommand addFeatureCommand { get; private set; }           
       
        //private void openMap()
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //     ofd.Filter = "ArcGIS Files |*.mxd|All Files (*.*)|*.*";
        //    if (ofd.ShowDialog() != DialogResult.Cancel)
        //    {
        //        _mapDoc = new MapDocument();
        //        _mapDoc.Open(ofd.FileName);
        //        imap = _mapDoc.get_Map(0);
        //        currentMap.Model.Map = imap;                
        //    }
        //}

        //private void addFeature()
        //{
        //    // 'make a dialog box object that will show shapefiles only
        //    IGxDialog pGxDia = new GxDialog();
        //    IGxObjectFilter pGxObFil;

        //    //'the type of filter dictates what type of files can be chosen
        //    //'in this case it is shapefiles
        //    pGxObFil = new GxFilterShapefiles();
        //    pGxDia.ObjectFilter = pGxObFil;


        //    //' make a gxEnum object to hold the files that the user selects from the dialog box
        //    IEnumGxObject gxEnum;
        //    if (!pGxDia.DoModalOpen(0, out gxEnum))
        //    {
        //        return ;
        //    }

        //    IGxObject gxObj = gxEnum.Next();

        //    //        'Identify the workspace to access the file from
        //    //'in this case it will be a shapefile workspace factory since that is the type of file that will be chosen 
        //    IWorkspaceFactory wksFact = new ShapefileWorkspaceFactory();
        //    IWorkspace wks = wksFact.OpenFromFile(gxObj.Parent.FullName, 0);

        //    //'create a new feature class object from the selected workspace
        //    //'set it to the name of the shapefile chosen in the dialog
        //    IFeatureWorkspace featWrk = (IFeatureWorkspace)wks;
        //    IFeatureClass fClass = featWrk.OpenFeatureClass(gxObj.Name);

        //    //'make a feature layer from teh featureclass object
        //    IFeatureLayer lyr = new FeatureLayer();
        //    lyr.FeatureClass = fClass;
        //    lyr.Name = gxObj.Name;


        //    currentMap.Model.AddLayer(lyr);
        //   // imap.AddLayer(lyr);          
        //}
        
    }
}
