using ESRI.ArcGIS.esriSystem;
// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using EmployeeTracker.Common;
    using EmployeeTracker.EntityFramework;
    //using EmployeeTracker.Fakes;
    using EmployeeTracker.Model.Interfaces;
    using EmployeeTracker.View;
    using EmployeeTracker.ViewModel;
    using ESRI.ArcGIS.esriSystem;
    //using WpfMvvmHoodAnalyzer.Helpers;
    using EmployeeTracker.Model;
    using EmployeeTracker.Services;
    using EmployeeTracker.Interfaces;
    


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Context is disposed when app exits.")]
    public partial class App : Application
    {
        private LicenseInitializer m_AOLicenseInitializer = new EmployeeTracker.LicenseInitializer();
        
    
        App()
        {
           // InitializeEngineLicense();
        }

       

       // private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();

        //private void InitializeEngineLicense()
        //{
        //    //AoInitialize aoi = new AoInitialize();
        //    //Additional license choices can be included here.
            
        //   // esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeAdvanced;

        //  //  m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeAdvanced },
        //  //new esriLicenseExtensionCode[] { });
        //   // ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
        //    if (ESRI.ArcGIS.RuntimeManager.ActiveRuntime == null)
        //        ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
        //    //if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
        //    //{
        //    //    aoi.Initialize(productCode);
        //    //}
        //}

        /// <summary>
        /// The unit of work co-ordinating changes for the application
        /// </summary>
        private  IEmployeeContext context;
                     

        /// <summary>
        /// If true an fake in-memory context will be used
        /// If false an ADO.Net Entity Framework context will be used
        /// </summary>
        private bool useFakes = false;

        /// <summary>
        /// Lauches the entry form on startup
        /// </summary>
        /// <param name="e">Arguments of the startup event</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (this.useFakes)
            {
               //this.context =  Generation.BuildFakeSession();
            }
            else
            {
                //NOTE: If there is not a Microsof-t SQL Server Express instance available at .\SQLEXPRESS 
                //      you will need to update the "EmployeeEntities" connection string in App.config
                this.context = new EmployeeEntities();
                //this.context.mapObject.AxMap = new ESRI.ArcGIS.MapControl.AxMapControl();
            }

            //initialize all the services needed for dependency injection
            //we use the unity framework for dependency injection, but you can choose others
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());

            //register the IModalDialog using an instance of the HoodAnalystDialogView
            //this sets up the view
            ServiceProvider.Instance.Register<IModalDialog, HoodAnalystDialogView>(); 

            IDepartmentRepository departmentRepository = new DepartmentRepository(this.context);
            IEmployeeRepository employeeRepository = new EmployeeRepository(this.context);
            IMapObjectRepository mapRepository = new MapRepository(this.context);
            IUnitOfWork unit = new UnitOfWork(this.context);

            MainViewModel main = new MainViewModel(unit, departmentRepository, employeeRepository,mapRepository );
            MainView window = new View.MainView { DataContext = main };
            window.Show();
        }

        /// <summary>
        /// Cleans up any resources on exit
        /// </summary>
        /// <param name="e">Arguments of the exit event</param>
        protected override void OnExit(ExitEventArgs e)
        {
            this.context.Dispose();

            base.OnExit(e);
        }

        void Application_Startup(object sender, StartupEventArgs e)
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
            new esriLicenseExtensionCode[] { });
            
        }

        void Application_Exit(object sender, ExitEventArgs e)
        {
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
           
        }
    }
}
