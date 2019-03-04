using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 using EmployeeTracker.Model;
using EmployeeTracker.ViewModel.Helpers;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using EmployeeTracker.Interfaces;
using EmployeeTracker.Common;
using System.Windows.Interop;
using System.IO;
using EmployeeTracker.HoodAux;




namespace EmployeeTracker.ViewModel
{
    public class HoodViewModel :HoodAnalystBase 
    {
        #region private property Fields
                               
        private HoodAnalyzerModel _hoodModel;

        private MapObject _axMap; 
        
        private LayersClass selectedLayer;
        
        private ObservableCollection<hField> inputFields; 

        private hField selectedField;

        private int distance;

        private enumSpatialReletionship spatialRelationship;

        private enumAnalysisType analysisType;

        private string clusterValues;
      
        private string outPutFeatureClass;

        private bool addLayer;
       
        private int noDataVal;

        private int cellSize;

        private enumExtent selectedExtent;

        private double extentDataTop;
        private double extentDataRight;
        private double extentDataBottom;
        private double extentDataLeft;      
      
        private string clstBxVsbl;
        private string SHPBrwsVsbl;
      
        private string shpPath;

        #endregion

        #region private app filds

        private IntPtr windowHandle;
        private IMapDocument m_MxDoc;
        private IGeoFeatureLayer m_SelLayer;

        private string m_Name;
        private string m_Path;
        private string m_GenericName;
        private string m_DatasetType;
        private string OutputFileName;
        private string OutputFilePath;
        private string OutputFileNameGeneric;
        private string OutputDatasetType;
        private string ShapeFilePath;
        private string ShapeFileName;
        private string ShapeFileNameGeneric;

        private AnalysisParams[] Analysis = new AnalysisParams[1];

        #endregion



        #region Constructors
        public HoodViewModel(MapObject axMap)
        {
             windowHandle = new WindowInteropHelper(App.Current.MainWindow).Handle;

            if (_hoodModel == null)
            {
                _hoodModel = new HoodAnalyzerModel();
            }
            if (this.InputFeatureClass == null)
            {
                this.InputFeatureClass = new ObservableCollection<LayersClass>();
            }
           
            ClstBxVsbl = "Hidden";
            SelectedExtent = enumExtent.CurrentView ;
            AddLayer = true;
            SpatialRelationship = enumSpatialReletionship.FixedDistance;
            NoDataVal = -1;           
            
            this.BrowseCommand = new DelegateCommand((o) => this.Browse(o));
            this.OKCommand = new DelegateCommand((o) => this.OK());           
            
                _axMap = axMap;
                var lc = from s in _axMap.LayersCollection
                         where s.LayerType == "Point"
                         select s;

                foreach (LayersClass lyCl in lc)
                {
                    this.InputFeatureClass.Add(lyCl);
                }             
        }
                
        #endregion

        #region Properties

        public HoodAnalyzerModel HoodModel
        {
            get { return _hoodModel; }
            set { _hoodModel = value; }
        }
              
        public ObservableCollection<LayersClass> InputFeatureClass { get; set; }
        
        public ObservableCollection<hField> InputFields
        {
            get { return inputFields; }
            set { inputFields = value;
            OnPropertyChanged("InputFields");
            }
        }
               
        public LayersClass  SelectedLayer
        {
            get { return selectedLayer; }
            set
            {
                selectedLayer = value;

                string s = "";
                InputFields=GetFields();
                //===================================================

                //=======================================================


                _hoodModel.SelectedFeatureClass = selectedLayer.LayerName;
                OnPropertyChanged("SelectedLayer");              

            }
        }

        public hField SelectedField
        {
            get { return selectedField; }
            set { selectedField = value;
            _hoodModel.SelectedInputField = selectedField.FieldName;
            OnPropertyChanged("SelectedField");
            }
        }

        public int Distance
        {
            get { return distance; }
            set
            { 
                distance = value;
                _hoodModel.Distance = distance;
                this.OnPropertyChanged("Distance");
            }
        }

        public enumSpatialReletionship SpatialRelationship
        {
            get { return spatialRelationship; }
            set
            { 
                spatialRelationship = value;
                _hoodModel.SpatialRelations = spatialRelationship;
                this.OnPropertyChanged("SpatialRelationship");
            }
        }

        public enumAnalysisType AnalysisType
        {
            get { return analysisType; }
            set
            { 
                analysisType = value;
                _hoodModel.AnalysisType = analysisType;
                if (analysisType == enumAnalysisType.ClusterAnalysis)
                {

                    ClstBxVsbl = "Visible";
                }
                else
                {
                    ClstBxVsbl = "Hidden";
                }
                this.OnPropertyChanged("AnalysisType");
            }
        }

        public string ClusterValues
        {
            get { return clusterValues; }
            set 
            {
                clusterValues = value;
                _hoodModel.ClusterValues = clusterValues;
                this.OnPropertyChanged("ClusterValues");
            }
        }

        public string OutPutFeatureClass
        {
            get { return outPutFeatureClass; }
            set 
            { 
                outPutFeatureClass = value;
                _hoodModel.OutPutFeatureClass = outPutFeatureClass;
                this.OnPropertyChanged("OutPutFeatureClass");
            }
        }

        public int NoDataVal
        {
            get { return noDataVal; }
            set
            { 
                noDataVal = value;
                _hoodModel.NoDataVal = noDataVal;
                this.OnPropertyChanged("NoDataVal");
            }
        }

        public int CellSize
        {
            get { return cellSize; }
            set 
            { 
                cellSize = value;
                _hoodModel.CellSize = cellSize;
                this.OnPropertyChanged("CellSize");
            }
        }

        public enumExtent  SelectedExtent
        {
            get { return selectedExtent; }

            set
            {
                selectedExtent = value;
                _hoodModel.Extent = selectedExtent;
               if(_axMap!=null) ExtentSelection();
                if (selectedExtent == enumExtent.ShapeFile)
                {
                    ShpPath = "Choose a Shapefile";
                    SHPBrwsVsbl1 = "Visible";
                }
                else
                {
                    SHPBrwsVsbl1 = "Hidden";
                }
                this.OnPropertyChanged("SelectedExtent");
            }
        }              

        public double ExtentDataTop
        {
            get { return extentDataTop; }
            set
            { 
                extentDataTop = value;
                _hoodModel.ExtentDataTop = extentDataTop;
                this.OnPropertyChanged("ExtentDataTop");
            }
        }      

        public double ExtentDataRight
        {
            get { return extentDataRight; }
            set 
            {
                extentDataRight = value;
                _hoodModel.ExtentDataRight = extentDataRight;
                this.OnPropertyChanged("ExtentDataRight");
            }
        }       

        public double ExtentDataBottom
        {
            get { return extentDataBottom; }
            set 
            { 
                extentDataBottom = value;

                _hoodModel.ExtentDataBottom = extentDataBottom;
                this.OnPropertyChanged("ExtentDataBottom");
            }
        }      

        public double ExtentDataLeft
        {
            get { return extentDataLeft; }
            set 
            { 
                extentDataLeft = value;
                _hoodModel.ExtentDataLeft = extentDataLeft;
                this.OnPropertyChanged("ExtentDataLeft");
            }
        }

        public bool AddLayer
        {
            get { return addLayer; }
            set
            {
                addLayer = value;
                _hoodModel.AddLayer = addLayer;
                this.OnPropertyChanged("AddLayer");
            }
        }
        #endregion

        #region Settings

        public string ClstBxVsbl
        {
            get { return clstBxVsbl; }
            set { clstBxVsbl = value;
            this.OnPropertyChanged("ClstBxVsbl");
            }
        }

        public string SHPBrwsVsbl1
        {
            get { return SHPBrwsVsbl; }
            set { SHPBrwsVsbl = value;
            this.OnPropertyChanged("SHPBrwsVsbl1");
            }
        }


        public string ShpPath
        {
            get { return shpPath; }
            set
            { 
                shpPath = value;
                this.OnPropertyChanged("ShpPath");
            }
        }

        #endregion

        #region private methods

        private ObservableCollection<hField> GetFields()
        {
            ObservableCollection<EmployeeTracker.Model.hField> inputField = new ObservableCollection<EmployeeTracker.Model.hField>();
            if (inputField == null)
            { inputField = new ObservableCollection<EmployeeTracker.Model.hField>(); }
            inputField = new ObservableCollection<Model.hField>();
            inputField.Clear();
           // IFeatureLayer x_SelLayer;
            int index = 0;
            var d = from a in InputFeatureClass
                    where a.LayerName == selectedLayer.LayerName
                    select a.LayerIndex;
            foreach (int t in d)
            {
                index = t;

            }

            m_SelLayer = (IGeoFeatureLayer)_axMap.Map.get_Layer(index);

            IFields iFields = m_SelLayer.FeatureClass.Fields;
            for (int ix = 0; ix < m_SelLayer.FeatureClass.Fields.FieldCount - 1; ix++)
            {
                if (iFields.get_Field(ix).Type == esriFieldType.esriFieldTypeInteger ||
                    iFields.get_Field(ix).Type == esriFieldType.esriFieldTypeSmallInteger ||
                     iFields.get_Field(ix).Type == esriFieldType.esriFieldTypeSingle ||
                    iFields.get_Field(ix).Type == esriFieldType.esriFieldTypeDouble)
                {
                    EmployeeTracker.Model.hField f = new EmployeeTracker.Model.hField();
                    f.FieldName=iFields.get_Field(ix).Name;

                    inputField.Add(f);                    
                }
            }
            return inputField;
         //  this.InputFields = inpuField;
           
        }


        private void ExtentSelection()
        {
            IGeoDataset pGeoDataset;
            //    tbBrowseShapeFile.Buttons(1).Enabled = 0
            //    lblShapeFilePath.Visible = 0
            //    lblShapeFilePath = ""
            //    If OptCurrExt.Value Then
            //        With m_MxDoc.ActiveView.Extent
            switch (SelectedExtent)
            {
                case enumExtent.InputLayer:
                    pGeoDataset = (IGeoDataset)m_SelLayer.FeatureClass;
                    ExtentDataLeft = pGeoDataset.Extent.XMin;
                    ExtentDataRight = pGeoDataset.Extent.XMax;
                    ExtentDataBottom = pGeoDataset.Extent.YMin;
                    ExtentDataTop = pGeoDataset.Extent.YMax;
                    pGeoDataset = null;
                    break;
                case enumExtent.CurrentView:
                    ExtentDataLeft = _axMap.ActiveView.Extent.XMin;
                    ExtentDataRight = _axMap.ActiveView.Extent.XMax;
                    ExtentDataBottom = _axMap.ActiveView.Extent.YMin;
                    ExtentDataTop = _axMap.ActiveView.Extent.YMax;

                    break;
                case enumExtent.ShapeFile:
                    //        txtLeft = ""
                    //        txtRight = ""
                    //        txtBottom = ""
                    //        txtTop = ""
                    //        tbBrowseShapeFile.Buttons(1).Enabled = -1
                    //        lblShapeFilePath.Visible = -1

                    //'        tbBrowseShapeFile.ImageList = _
                    //'        ilButtons
                    //'        tbBrowseShapeFile.Buttons(1).Image = _
                    //'        tbBrowseShapeFile.Buttons(1).Key
                    //'        tbBrowseShapeFile.Refresh
                    break;

                case enumExtent.UserDefined:
                    ExtentDataLeft = 0;
                    ExtentDataRight = 0;
                    ExtentDataBottom = 0;
                    ExtentDataTop = 0;
                    break;
            }



        }

        #endregion

        #region Commands

        public ICommand BrowseCommand { get; private set; }

        public ICommand OKCommand { get; private set; }
        
        private void Browse(object o)
        {
            //' allows the user to specify the output shapefile path and name.
            IGxDialog pGxDialog = new GxDialog();
            object obj = o;
            IGxObjectFilterCollection FilterCol = (IGxObjectFilterCollection)pGxDialog;
            FilterCol.AddFilter(new GxFilterShapefiles(), true);
            FilterCol.AddFilter(new GxFilterRasterDatasets(), false);

           
           
                switch(o.ToString())
                {
                    case "OutputBrws": 
                        if (ShowSaveFileDialog(pGxDialog, out m_Path, out m_Name, out m_GenericName, out m_DatasetType))
                        {
                            OutputFilePath = m_Path;
                            OutputFileName = m_Name;
                            OutPutFeatureClass = OutputFilePath + "\\" + OutputFileName;
                            OutputFileNameGeneric = m_GenericName;
                            OutputDatasetType = m_DatasetType;
                        }
                            break;
                       
                    case "SHPBrws":
                            // If Not ShowOpenFileDialog()
                        if(ShowOpenFileDialog(pGxDialog,out m_Path,out m_Name,out m_GenericName,out m_DatasetType))
                        {
                         IFeatureClass FeatClass;
                         IGeoDataset GeoDataset;
                         ShapeFilePath = m_Path;
                         ShapeFileName = m_Name;
                         ShpPath  = ShapeFilePath + "/" + ShapeFileName;

                         ShapeFileNameGeneric = m_GenericName;
                         FeatClass = OpenShapefile(ShapeFilePath, ShapeFileNameGeneric, windowHandle.ToInt32());
                         GeoDataset =(IGeoDataset) FeatClass;
                         ExtentDataLeft  = GeoDataset.Extent.XMin;
                         ExtentDataRight  = GeoDataset.Extent.XMax;
                         ExtentDataBottom  = GeoDataset.Extent.YMin;
                         ExtentDataTop = GeoDataset.Extent.YMax;
                         GeoDataset = null;
                         FeatClass = null;
                        }
                         break ;
                }           
        }

        private void OK()
        {
            

            object obj = _hoodModel;           

            string inputFile, inputFileName, inputFilePath, inputFileNameGeneric, inputFieldName;
            IDataset pDataset;
            ISpatialReference pSpatialRef;
            Envelope pEnv;
            IDataset inputFileDataset;
            long   dataCount, gridCount;
            long rows, cols;
            double cellSize, x, y,radius;
            IFeatureClass fishnetFeatClass;
            IFeatureClass outputFeatClass;

            if (!ValidateInput())
            {
                return;
            }

            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);

             pDataset =(IDataset) m_SelLayer.FeatureClass;
            if(pDataset.Category== "Shapefile Feature Class")
            {
                inputFile = pDataset.Workspace.PathName + "\\" + pDataset.Name + ".shp";
            }
            else{return;}

            inputFileName = System.IO.Path.GetFileName(inputFile);         //Directory.GetFiles(inputFile)[0]; // Dir(inputFile);
            Analysis[0].InputFilePath =  inputFile.Replace(inputFileName, "");
            Analysis[0].InputFileNameGeneric = inputFileName.Substring(0,inputFileName.IndexOf("."));
            Analysis[0].InputFieldName = _hoodModel.SelectedInputField.Trim();
            Analysis[0].Type = _hoodModel.AnalysisType;
            Analysis[0].Radius = Convert.ToDouble(_hoodModel.Distance);

            if (Analysis[0].Type == enumAnalysisType.ClusterAnalysis)
            {
                System.Array.Resize<ClusterParam>(ref Analysis[0].CP, 1);
                BuildClusterValuesStruct(ref Analysis[0], ClusterValues);            
            }
            
            pEnv = new Envelope();
            pEnv.XMin = _hoodModel.ExtentDataLeft; 
            pEnv.XMax =_hoodModel.ExtentDataRight;
            pEnv.YMin = _hoodModel.ExtentDataBottom;
            pEnv.YMax = _hoodModel.ExtentDataTop;
            
            cellSize =_hoodModel.CellSize;


            rows = (long)Math.Round(((pEnv.YMax - pEnv.YMin ) / cellSize)+ 0.5); 
            cols = (long)Math.Round(((pEnv.XMax - pEnv.XMin ) / cellSize)+ 0.5); 
            x = pEnv.XMin;
            y = pEnv.YMin;
            
            pSpatialRef = _axMap.ActiveView.FocusMap.SpatialReference;

            if (File.Exists (OutputFilePath + "\\" + "tmpFish_Net" + ".shp") )
            {
                fishnetFeatClass = OpenShapefile(OutputFilePath, "tmpFish_Net", windowHandle.ToInt32());
                DeleteShapefile(fishnetFeatClass);
                fishnetFeatClass = null;
            }


            if (File.Exists(OutputFilePath + "\\" + OutputFileName))
            {
                outputFeatClass = OpenShapefile(OutputFilePath, OutputFileNameGeneric, windowHandle.ToInt32());
                DeleteShapefile(outputFeatClass);
                outputFeatClass = null;
            }

            IFeatureClass dataFeatClass;
            dataFeatClass = OpenShapefile(Analysis[0].InputFilePath, Analysis[0].InputFileNameGeneric, windowHandle.ToInt32());
            dataCount = dataFeatClass.FeatureCount(null);
            dataFeatClass = null;
            gridCount = rows * cols;
            
            
            //    Set objProgressForm = New frmProccessProgressControl
            //    objProgressForm.pbProccessProgress.Min = 0
            //    ProgressMaxVal = (GridCount * 2) + (DataCount * 4)
            //    objProgressForm.pbProccessProgress.Max = ProgressMaxVal
            //    UserInterruptType = UserInterruptTypeNone
            //    objProgressForm.Show vbModeless
            //    Me.Hide
    
            //    ProgressVal = 0
            //    UpdateProgressStatus ProgressVal, "Creating grid ..."

            CustomToolsCreateFishnet(OutputFilePath, "tmpFish_Net", x, y, rows, cols, CellSize, CellSize,enumFishnetType.PolygonsFishnet , pSpatialRef, true, false);

            //    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
            //    'UpdateProgressStatus ProgressVal, "done."

            fishnetFeatClass = OpenShapefile(OutputFilePath, "tmpFish_Net", windowHandle.ToInt32());
    
            //'    CustomToolsAddField FishnetFeatClass, "CentroidX", esriFieldTypeDouble
            //'    UpdateProgressStatus ProgressVal, "Calculating cells centroidX ..."
            //'    CustomToolsCalcGeometry FishnetFeatClass, "CentroidX", GeometryClaculationTypeCentroidX
            //'    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
            //'    'UpdateProgressStatus ProgressVal, "done."
            //'    CustomToolsAddField FishnetFeatClass, "CentroidY", esriFieldTypeDouble
            //'    UpdateProgressStatus ProgressVal, "Calculating cells centroidY ..."
            //'    CustomToolsCalcGeometry FishnetFeatClass, "CentroidY", GeometryClaculationTypeCentroidY
            //'    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
            //'    'UpdateProgressStatus ProgressVal, "done."
    
            
            object s,e;
           
            //    Dim I As Long
            //    For I = 1 To UBound(Analysis)
            for (int i = 0; i < Analysis.Length; i++)
            {
                s = DateTime.Now ;
                //        '1.04
                   NeighborhoodStatisticsEx_DATA_IS_ARRAY_NO_INVERSE(fishnetFeatClass, Analysis[i],  CellSize, x, y, rows, cols, OutputFilePath, 
                       OutputFileName, OutputFileNameGeneric, OutputDatasetType, pSpatialRef, true, _hoodModel.NoDataVal  , 
                       _hoodModel.AddLayer,windowHandle.ToInt32());
                   e = DateTime.Now;
                //        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
                //        'MsgBox s - e

                // '       CustomToolsRemoveField DataFeatClass, "PointX"
                // '       CustomToolsRemoveField DataFeatClass, "PointY"
                // '       CustomToolsRemoveField DataFeatClass, "Col"
                // '       CustomToolsRemoveField DataFeatClass, "Row"
                // '       Set DataFeatClass = Nothing
            }

            DeleteShapefile(fishnetFeatClass);
            fishnetFeatClass = null;

            //    Unload objProgressForm
            //    Set objProgressForm = Nothing

            //    Unload Me

            Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            MessageBox.Show("Done!");

            //End Sub
        }

        private void Cancel()
        {
            
        }

        #endregion
       
        
    }
}


//Option Explicit





//Private Sub cboStatType_Change()
//    txtClusterValues.Visible = _
//    IIf(cboStatType = "Cluster %", True, False)
//    Label17.Visible = _
//    IIf(cboStatType = "Cluster %", True, False)
//    Label18.Visible = _
//    IIf(cboStatType = "Cluster %", True, False)
//End Sub

//Private Sub chkAddAsLayer_Click()

//End Sub

//Private Sub cmdBrowseGrids_Click()
//    ' allows the user to specify the output shapefile path and name.
//    Dim pGxDialog As IGxDialog
//    Set pGxDialog = New GxDialog
//    Dim FilterCol As IGxObjectFilterCollection
//    Set FilterCol = pGxDialog
//    FilterCol.AddFilter New GxFilterShapefiles, True
//    If Not ShowSaveFileDialog(pGxDialog, m_Path, m_Name, m_GenericName, _
//     New GxFilterShapefiles) Then GoTo CreateFileFailed
//     txtOutput = m_Path & "\" & m_Name
//     Me.txtOutput = txtOutput
//End Sub

//Private Sub CommandButton2_Click()
//    Unload Me
//End Sub

//Private Sub CommandButton5_Click()

//End Sub

//Private Sub Frame1_Click()

//End Sub

//Private Sub Frame5_Click()

//End Sub

//Private Sub Label12_Click()

//End Sub

//Private Sub Label20_Click()

//End Sub







//Private Function GetMyPlaceEnvelope(MyPlaceName) As IEnvelope
//    Dim MyPlaces As IEnumPlace
//    Set MyPlaces = New MyPlaceCollection
//    Dim MyPlace As IPlace

//    Set MyPlaces = New MyPlaceCollection
//    MyPlaces.Reset
//    Set MyPlace = MyPlaces.Next
//    Do While Not MyPlace Is Nothing
//        If MyPlace.Name = Trim(MyPlaceName) Then
//            Set GetMyPlaceEnvelope = MyPlace.Geometry.Envelope
//            Exit Function
//        End If
//        Set MyPlace = MyPlaces.Next
//    Loop
//End Function


//Private Sub OptCurrExt_Click()
//    ExtentSelection
//End Sub

//Private Sub OptInput_Click()
//    ExtentSelection
//End Sub

//Private Sub OptionButton1_Click()

//End Sub

//Private Sub optShapeFile_Click()
//    ExtentSelection
//End Sub

//Private Sub OptUser_Click()
//    ExtentSelection
//End Sub


//Private Sub tbBrowseOutput_ButtonClick(ByVal Button As ComctlLib.Button)
//       ' allows the user to specify the output shapefile path and name.
//    Dim pGxDialog As IGxDialog
//    Set pGxDialog = New GxDialog
//    Dim FilterCol As IGxObjectFilterCollection
//    Set FilterCol = pGxDialog
//    FilterCol.AddFilter New GxFilterShapefiles, True
//    FilterCol.AddFilter New GxFilterRasterDatasets, False
//    If Not ShowSaveFileDialog(pGxDialog, m_Path, m_Name, m_GenericName, m_DatasetType) Then Exit Sub
//    OutputFilePath = m_Path
//    OutputFileName = m_Name
//    txtOutput = OutputFilePath & "\" & OutputFileName
//    OutputFileNameGeneric = m_GenericName
//    OutputDatasetType = m_DatasetType
//End Sub



//'Private Sub tbProccessProgress_ButtonClick(ByVal Button As MSComctlLib.Button)
//'    Select Case Button.Key
//'        Case "OK"
//'            mmdOk_Click
//'        Case "Cancel"
//'            Unload Me
//'    End Select
//'End Sub

//Private Sub UserForm_Initialize()
//On Error GoTo Gone
//    Set m_MxDoc = ThisDocument

//    FillLayerCombo
//    cboStatType.AddItem "Max"
//    cboStatType.AddItem "Min"
//    cboStatType.AddItem "Sum"
//    cboStatType.AddItem "Count"
//    cboStatType.AddItem "Mean"
//    cboStatType.AddItem "StDev"
//    cboStatType.AddItem "Cluster %"

//    cboSpatialRelations.AddItem "Fixed Distance Band"
//    cboSpatialRelations.AddItem "Inverse Distance"
//    cboSpatialRelations.AddItem "Inverse Distance Squered"
//    cboSpatialRelations.ListIndex = 0
//    cboSpatialRelations.Locked = True

//    OptInput.Value = 0
//    OptInput.Enabled = False
//    OptCurrExt.Value = 0
//    chkAddAsLayer.Value = 1
//    txtClusterValues.Visible = False
//    Label17.Visible = False
//    Label18.Visible = False

//    txtNoData = -1

//    tbBrowseOutput.ImageList = _
//    ilButtons
//    tbBrowseOutput.Buttons(1).Image = _
//    tbBrowseOutput.Buttons(1).Key

//    tbBrowseShapeFile.ImageList = _
//    ilButtons
//    tbBrowseShapeFile.Buttons(1).Image = _
//    tbBrowseShapeFile.Buttons(1).Key

//    tbBrowseShapeFile.Buttons(1).Enabled = False
//    lblShapeFilePath.Visible = 0


//    txtDist.Text = 0
//Exit Sub
//Gone:
//    MsgBox "There are no feature layers in the current map", vbExclamation
//End Sub



//**************************************************************************************************
//Private Sub FillcboMyPlacesCombo()
//    Dim MyPlaces As IEnumPlace
//    Set MyPlaces = New MyPlaceCollection
//    Dim MyPlace As IPlace

//    cboMyPlaces.Clear
//    Set MyPlaces = New MyPlaceCollection
//    MyPlaces.Reset
//    Set MyPlace = MyPlaces.Next
//    Do While Not MyPlace Is Nothing
//        cboMyPlaces.AddItem MyPlace.Name
//        Set MyPlace = MyPlaces.Next
//    Loop

//End Sub
