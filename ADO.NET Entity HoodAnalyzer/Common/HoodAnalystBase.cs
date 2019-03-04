using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using System.Windows.Forms;

namespace EmployeeTracker.Common
{
    public class HoodAnalystBase : ViewModelBase
    {
        protected bool ShowSaveFileDialog(IGxDialog pGxDialog, out string path, out string name, out string genericName, out string datasetType)
        {
            path = ""; name = ""; genericName = ""; datasetType = "";
            pGxDialog.Title = "Save File as:";
            if (!pGxDialog.DoModalSave(0))
            { return false; }

            datasetType = pGxDialog.ObjectFilter.Name;

            //    ' delete the existing shapefile if user wants to replace it.
            if (pGxDialog.ReplacingObject)
            {
                switch (datasetType)
                {
                    case "Shapefiles":
                        IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                        IFeatureWorkspace pFWS = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pGxDialog.FinalLocation.FullName, 0);//        mFile(pGxDialullName, 0)
                        IFeatureClass pFeatClass = pFWS.OpenFeatureClass(pGxDialog.Name);
                        if (!DeleteShapefile(pFeatClass))
                        {
                            MessageBox.Show("Please specify a different name for the output shapefile", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                        break;
                }
            }

            if (!(pGxDialog.Name.IndexOf(".") == 0))
            {
                genericName = pGxDialog.Name;
            }
            else
            {
                genericName = pGxDialog.Name.Substring(0, pGxDialog.Name.IndexOf("."));
            }

            //if (sTelNo.IndexOf('/') >= 0)
            //{
            //    sTelNo = sTelNo.Substring(0, sTelNo.IndexOf('/'));
            //}
            switch (datasetType)
            {
                case "Shapefiles":
                    name = genericName + ".shp";
                    break;
                case "Raster Datasets":
                    name = genericName + ".img";
                    break;
            }

            path = pGxDialog.FinalLocation.FullName;

            return true;
        }

        protected bool ShowOpenFileDialog(IGxDialog pGxDialog, out string path, out string name, out string genericName, out string datasetType)
        {
            IEnumGxObject pEnumGX;
            IGxObject pEnumGxObj;
            path = ""; name = ""; genericName = ""; datasetType = "";
            pGxDialog.Title = "Save File as:";
            if (!pGxDialog.DoModalOpen(0, out pEnumGX))
            {
                return false;
            }
            datasetType = pGxDialog.ObjectFilter.Name;
            pEnumGX.Reset();
            pEnumGxObj = pEnumGX.Next();
            genericName = pEnumGxObj.Name.Substring(0, pEnumGxObj.Name.IndexOf("."));
            name = pEnumGxObj.Name;
            path = pGxDialog.FinalLocation.FullName;
            return true;
        }

        protected bool DeleteShapefile(IFeatureClass pFeatClass)
        {
            try
            {
                IDataset pDS = (IDataset)pFeatClass;
                pDS.Delete();
                return true;
            }
            catch
            {
                return false;
            }

        }

        protected IFeatureClass OpenShapefile(string path, string Name, int hWnd)
        {
            IFeatureWorkspace pFWS;
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            pFWS = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(path, hWnd);     //, Application.hWnd);            
            return pFWS.OpenFeatureClass(Name);
        }

        protected bool ValidateInput()
        {
            return true;
        }

        protected IGeoFeatureLayer GetLayerByName(IMapDocument Document, string LayerName)
        {

            UID pID = new UID();

            //'*This is the ID for layers supporting the IGeoFeatureLayer interface
            pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";
            //'*Get all the FeatureLayers from the map using the UID ...
            IEnumLayer pAllFLayers = Document.ActiveView.FocusMap.get_Layers(pID, true);


            //'*Loop thru all FeatureLayers until "LayerName" is found ...
            ILayer pLayer = pAllFLayers.Next();

            do
            {
                if (pLayer.Name == LayerName)
                {
                    return (IGeoFeatureLayer)pLayer;

                }
                pLayer = pAllFLayers.Next();

            } while (pLayer != null);

            return null;





            //            Public Function GetLayerByName(Document As IMxDocument, _
            //LayerName As String) As IGeoFeatureLayer

            //    Dim pID As New esriSystem.UID
            //'*This is the ID for layers supporting the IGeoFeatureLayer interface
            //    pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
            //'*Get all the FeatureLayers from the map using the UID ...
            //    Dim pAllFLayers As IEnumLayer
            //    Set pAllFLayers = Document.FocusMap.Layers(pID, True)
            //'*Loop thru all FeatureLayers until "LayerName" is found ...
            //    Dim pLayer As ILayer
            //    Set pLayer = pAllFLayers.Next



            //    Do Until pLayer Is Nothing
            //        If pLayer.Name = LayerName Then Exit Do
            //        Set pLayer = pAllFLayers.Next
            //    Loop
            //'*Pass back the requested layer ...
            //    Set GetLayerByName = pLayer

            //End Function

        }

        protected bool BuildClusterValuesStruct(AnalysisParams analysis)
        {


            //Private Function BuildClusterValuesStruct(Analysis As AnalysisParams) As Boolean
            //Dim I As Long
            //Dim SubStr As String

            //    I = 1
            //    SubStr = ExtractSubString(txtClusterValues, ";", I)
            //    Do While Trim(SubStr) <> ""
            //        If UBound(Analysis.CP) = 0 Then
            //            ReDim Analysis.CP(1 To I) As ClusterParam
            //        Else
            //            ReDim Preserve Analysis.CP(1 To I) As ClusterParam
            //        End If
            //        If InStr(1, SubStr, "-") > 0 Then
            //            Analysis.CP(I).Type = cnstClusterValueRange
            //            Analysis.CP(I).From = CVar(ExtractSubString(SubStr, "-", 1))
            //            Analysis.CP(I).To = CVar(ExtractSubString(SubStr, "-", 2))
            //        Else
            //            Analysis.CP(I).Type = cnstClusterUniqueValue
            //            Analysis.CP(I).Equale = CVar(SubStr)
            //        End If
            //        I = I + 1
            //        SubStr = ExtractSubString(txtClusterValues, ";", I)
            //    Loop
            //End Function
        }

        public struct AnalysisParams
        {
            public string InputFilePath;
            public string InputFileNameGeneric;
            public string InputFieldName;
            public enumAnalysisType Type;
            public enumSpatialReletionship SpatialReletionships;
            public double Radius;
            public ClusterParam[] CP;
        }

        public struct ClusterParam
        {
            int Type;
            object From;
            object To;
            object Equale;
        }

        #region enums

        public enum enumFishnetType
        {
            PolygonsFishnet = 1,
            LinesFishnet = 2
        }

        public enum enumGeometryCalculationType
        {
            GeometryClaculationTypeMinX = 1,
            GeometryClaculationTypeMinY = 2,
            GeometryClaculationTypeMaxX = 3,
            GeometryClaculationTypeMaxY = 4,
            GeometryClaculationTypeArea = 5,
            GeometryClaculationTypePerimeter = 6,
            GeometryClaculationTypeCentroidX = 7,
            GeometryClaculationTypeCentroidY = 8,
            GeometryClaculationTypePointX = 9,
            GeometryClaculationTypePointY = 10
        }

        public enum enumAnalysisType
        {
            Max = 0,
            Min = 1,
            Sum = 2,
            Count = 3,
            Mean = 4,
            StDev = 5,
            ClusterAnalysis = 6
        }

        public enum enumSpatialReletionship
        {
            InverseDistance = 1,
            FixedDistance = 2
        }

        public enum enumUserInterruptType
        {
            UserInterruptTypeNone = 0,
            UserInterruptTypePause = 1,
            UserInterruptTypeResume = 2,
            UserInterruptTypeStop = 3
        }

        public enum enumWeightFuncion
        {
            WeightNumerator = 0,
            WeightMultiplier = 1
        }

        public enum enumWeightContainer
        {
            WeightInFishnet = 0,
            WeightInData = 1
        }

        public enum enumExtent
        {
            InputLayer = 1,
            CurrentView = 2,
            UserDefined = 3,
            ShapeFile = 4
        }

        #endregion

    }
}
