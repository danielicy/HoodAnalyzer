// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model.Interfaces;
    using EmployeeTracker.ViewModel.Helpers;
    using EmployeeTracker.Model;


    using ESRI.ArcGIS.CatalogUI;
    using ESRI.ArcGIS.Catalog;
    using ESRI.ArcGIS.Geodatabase;
    //using ESRI.ArcGIS.DataSourcesGDB;
    //using ESRI.ArcGIS.ArcMapUI;
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.Framework;
    using System.Windows.Forms;
    //using ESRI.ArcGIS.MapControl;
    using ESRI.ArcGIS.Carto;

    /// <summary>
    /// ViewModel for accessing all data for the company
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// UnitOfWork for co-ordinating changes
        /// </summary>
        private IUnitOfWork unitOfWork;

        private IMapDocument _mapDoc;
        private IMap imap;
               

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork for co-ordinating changes</param>
        /// <param name="departmentRepository">Repository for querying department data</param>
        /// <param name="employeeRepository">Repository for querying employee data</param>
        public MainViewModel(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IMapObjectRepository mapRepository)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (departmentRepository == null)
            {
                throw new ArgumentNullException("departmentRepository");
            }

            if (employeeRepository == null)
            {
                throw new ArgumentNullException("employeeRepository");
            }

            if (mapRepository == null)
            {
                throw new ArgumentNullException("mapRepository");
            }



            this.unitOfWork = unitOfWork;

            // Build data structures to populate areas of the application surface
            ObservableCollection<EmployeeViewModel> allEmployees = new ObservableCollection<EmployeeViewModel>();
            ObservableCollection<DepartmentViewModel> allDepartments = new ObservableCollection<DepartmentViewModel>();
            MapObject map;//= new MapObject();

            foreach (var dep in departmentRepository.GetAllDepartments())
            {
                allDepartments.Add(new DepartmentViewModel(dep));
            }

            foreach (var emp in employeeRepository.GetAllEmployees())
            {
                allEmployees.Add(new EmployeeViewModel(emp, allEmployees, allDepartments, this.unitOfWork));
            }

            map = mapRepository.GetMap();
            //map.AxMap = new ESRI.ArcGIS.MapControl.AxMapControl();
           // mapViewModel = new MapViewModel();

            this.DepartmentWorkspace = new DepartmentWorkspaceViewModel(allDepartments, unitOfWork);
            this.EmployeeWorkspace = new EmployeeWorkspaceViewModel(allEmployees, allDepartments, unitOfWork);
            this.MapWorkspace = new MapWorkSpaceViewModel(map, unitOfWork);

           // this.MapLayers = map.ActiveView.FocusMap.get_Layers();

            // Build non-interactive list of long serving employees
            List<BasicEmployeeViewModel> longServingEmployees = new List<BasicEmployeeViewModel>();
            foreach (var emp in employeeRepository.GetLongestServingEmployees(5))
            {
                longServingEmployees.Add(new BasicEmployeeViewModel(emp));
            }

            this.LongServingEmployees = longServingEmployees;

           //  this. MapLayers=    map.LayersCollection  ; 

            this.SaveCommand = new DelegateCommand((o) => this.Save());
            
            this.openMapCommand = new DelegateCommand((o) => this.openMap());
            this.addFeatureCommand = new DelegateCommand((o) => this.addFeature());
           
        }

        #region Commands
        /// <summary>
        /// Gets the command to save all changes made in the current sessions UnitOfWork
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets the command to Open map in viewer
        /// </summary>
        public ICommand openMapCommand { get; private set; }

        /// <summary>
        /// Gets the command add a feature to map
        /// </summary>
        public ICommand addFeatureCommand { get; private set; }
       
        #endregion

    
        #region WorkspaceViewModels
        /// <summary>
        /// Gets the workspace for managing Maps
        /// </summary>
        public MapWorkSpaceViewModel MapWorkspace { get; private set; }

        /// <summary>
        /// Gets the workspace for managing employees of the company
        /// </summary>
        public EmployeeWorkspaceViewModel EmployeeWorkspace { get; private set; }

        /// <summary>
        /// Gets the workspace for managing departments of the company
        /// </summary>
        public DepartmentWorkspaceViewModel DepartmentWorkspace { get; private set; }

        #endregion

        /// <summary>
        /// Gets the list of employees for the Loyalty Board
        /// </summary>
        public IEnumerable<BasicEmployeeViewModel> LongServingEmployees { get; private set; }

        public IEnumerable<LayersClass> MapLayers { get; private set; }

        /// <summary>
        /// Saves all changes made in the current sessions UnitOfWork
        /// </summary>
        private void Save()
        {
            this.unitOfWork.Save();
        }
        
        private void openMap()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ArcGIS Files |*.mxd|All Files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                _mapDoc = new MapDocument();
                _mapDoc.Open(ofd.FileName);
                imap = _mapDoc.get_Map(0);
                MapWorkspace.currentMap.Model.Map = imap;
              
            }
        }

        private void addFeature()
        {
            // 'make a dialog box object that will show shapefiles only
            IGxDialog pGxDia = new GxDialog();
            IGxObjectFilter pGxObFil;

            //'the type of filter dictates what type of files can be chosen
            //'in this case it is shapefiles
            pGxObFil = new GxFilterShapefiles();
            pGxDia.ObjectFilter = pGxObFil;


            //' make a gxEnum object to hold the files that the user selects from the dialog box
            IEnumGxObject gxEnum;
            if (!pGxDia.DoModalOpen(0, out gxEnum))
            {
                return;
            }

            IGxObject gxObj = gxEnum.Next();

            //        'Identify the workspace to access the file from
            //'in this case it will be a shapefile workspace factory since that is the type of file that will be chosen 
            IWorkspaceFactory wksFact = new ShapefileWorkspaceFactory();
            IWorkspace wks = wksFact.OpenFromFile(gxObj.Parent.FullName, 0);

            //'create a new feature class object from the selected workspace
            //'set it to the name of the shapefile chosen in the dialog
            IFeatureWorkspace featWrk = (IFeatureWorkspace)wks;
            IFeatureClass fClass = featWrk.OpenFeatureClass(gxObj.Name);

            //'make a feature layer from teh featureclass object
            IFeatureLayer lyr = new FeatureLayer();
            lyr.FeatureClass = fClass;
            lyr.Name = gxObj.Name;

            
            MapWorkspace.currentMap.Model.AddLayer(lyr);
            // imap.AddLayer(lyr);          
        }
        
    }
}
