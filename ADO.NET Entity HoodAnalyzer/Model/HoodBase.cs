using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//using ESRI.ArcGIS.MapControl;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;


namespace EmployeeTracker.Model
{
    public abstract class HoodBase
    {
        

        //#region enums

        //public enum enumFishnetType
        //{
        //    PolygonsFishnet = 1,
        //    LinesFishnet = 2
        //}

        //public enum enumGeometryCalculationType
        //{
        //    GeometryClaculationTypeMinX = 1,
        //    GeometryClaculationTypeMinY = 2,
        //    GeometryClaculationTypeMaxX = 3,
        //    GeometryClaculationTypeMaxY = 4,
        //    GeometryClaculationTypeArea = 5,
        //    GeometryClaculationTypePerimeter = 6,
        //    GeometryClaculationTypeCentroidX = 7,
        //    GeometryClaculationTypeCentroidY = 8,
        //    GeometryClaculationTypePointX = 9,
        //    GeometryClaculationTypePointY = 10
        //}

        //public enum enumAnalysisType
        //{
        //    Max = 0,
        //    Min = 1,
        //    Sum = 2,
        //    Count = 3,
        //    Mean = 4,
        //    StDev = 5,
        //    ClusterAnalysis = 6
        //}

        //public enum enumSpatialReletionship
        //{
        //    InverseDistance = 1,
        //    FixedDistance = 2
        //}

        //public enum enumUserInterruptType
        //{
        //    UserInterruptTypeNone = 0,
        //    UserInterruptTypePause = 1,
        //    UserInterruptTypeResume = 2,
        //    UserInterruptTypeStop = 3
        //}

        //public enum enumWeightFuncion
        //{
        //    WeightNumerator = 0,
        //    WeightMultiplier = 1
        //}

        //public enum enumWeightContainer
        //{
        //    WeightInFishnet = 0,
        //    WeightInData = 1
        //}

        //public enum enumExtent
        //{
        //    InputLayer = 1,
        //    CurrentView = 2,
        //    UserDefined = 3,
        //    ShapeFile = 4
        //}

        //#endregion

        //#region structs

        //public struct SortFieldsParams
        //{
        //    string FieldName;
        //    bool Ascending;
        //    bool CaseSensitive;
        //}

        //public struct RowColInputIndex
        //{
        //    long StartIndex;
        //    long EndIndex;
        //    long Count;
        //}

       
       
        //public struct ScatterPoint
        //{
        //    object Value;
        //    double Distance;
        //    double InverseDistance;
        //    double Weight;
        //    object WeightedValue;
        //}

        //public struct Neightborhood
        //{
        //    ScatterPoint[] ScatterPoints;
        //    object Sum;
        //    long Count;
        //    object Max;
        //    object Min;
        //    long ClusterCount;
        //    double SumInverseDistance;
        //}
        
        
        
        //public struct ClusterParam
        //{
        //    int Type;
        //    object From;
        //    object To;
        //    object Equale;
        //}


        //public struct AnalysisParams
        //{
        //    public string InputFilePath;
        //    public string InputFileNameGeneric;
        //    public string InputFieldName;
        //    public enumAnalysisType Type;
        //    public enumSpatialReletionship SpatialReletionships;
        //    public double Radius;
        //    public ClusterParam[] CP;
        //}

                
    }
}

    /// <summary>
    /// Abstract base to consolidate common functionality of all ViewModels
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when a property on this object has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this ViewModels PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that has a new value</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Raises this ViewModels PropertyChanged event
        /// </summary>
        /// <param name="e">Arguments detailing the change</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }




//Public Sub ExecuteCommandById(identifier As UID)
//  Dim pCmdItem As ICommandItem
//  ' Use ArcID module and the Name of the Save command
//  Set pCmdItem = Application.Document.CommandBars.Find(identifier)
//  pCmdItem.Execute
//End Sub

//Public Sub FillCboMyCities()
//    Dim pEnumPlace As IEnumPlace
//    Dim pPlace As IPlace

//    Set pEnumPlace = New MyPlaceCollection
//    pEnumPlace.Reset
//    Set pPlace = pEnumPlace.Next
//    Do While Not pPlace Is Nothing
//        ThisDocument.UIComboBoxControl1.AddItem pPlace.Name
//        Set pPlace = pEnumPlace.Next
//    Loop
//End Sub

//Sub ZoomToExtent(XMin, YMin, XMax, YMax)
//    Dim m_pActiveView As IActiveView
//    Dim pNewExt As IEnvelope
//    Dim pMxDoc As IMxDocument

//    ' Get the ActiveView from the document
//    Set pMxDoc = ThisDocument
//    Set m_pActiveView = pMxDoc.FocusMap

//    Set pNewExt = New Envelope
//    pNewExt.XMin = 124
//    pNewExt.XMax = 135
//    pNewExt.YMin = 208
//    pNewExt.YMax = 218

//    m_pActiveView.Extent = pNewExt
//    m_pActiveView.Refresh

//    Set pNewExt = Nothing
//    Set m_pActiveView = Nothing
//    Set pMxDoc = Nothing

//End Sub

//Sub ZoomToEnvelope(Envelope As IEnvelope)
//    Dim m_pActiveView As IActiveView
//    Dim pNewExt As IEnvelope
//    Dim pMxDoc As IMxDocument

//    ' Get the ActiveView from the document
//    Set pMxDoc = ThisDocument
//    Set m_pActiveView = pMxDoc.FocusMap

//    m_pActiveView.Extent = Envelope
//    m_pActiveView.Refresh

//    Set pNewExt = Nothing
//    Set m_pActiveView = Nothing
//    Set pMxDoc = Nothing

//End Sub

//Public Sub ApplyFilterQuery(Field As String, Value As Variant)



//End Sub

//Public Function IsFieldExists(Layer As IFeatureLayer, FieldName As String) As Boolean
//    IsFieldExists = False

//    Dim pFClass As IFeatureClass
//    Set pFClass = Layer.FeatureClass

//    Dim pFields As IFields
//    Set pFields = pFClass.Fields

//    Dim pFld As IField
//    Dim I As Integer
//    For I = 0 To pFields.FieldCount - 1
//        Set pFld = pFields.Field(I)
//        If pFld.Type <= 5 Then  'not shape, OID, or BLOB
//            If pFld.Name = FieldName Then
//                IsFieldExists = True
//                Exit Function
//            End If
//        End If
//    Next I

//End Function


//Private Sub ExportMyPlacesToTxt()
//' Make sure you add a ref to the esriControls library
//' you may need to browse to it at:
//' C:\Program Files\ArcGIS\com\esriControls.olb
//'
//' ---- Change the text file output location ----
//Const OUTPUT_FILE = "C:\Miles\MyPlaces.txt"
//' ----------------------------------------------
//Dim pEnumPlace As IEnumPlace
//Dim pPlace As IPlace

//    Open OUTPUT_FILE For Output As #1
//    Print #1, "Name,XMin,XMax,YMin,YMax"

//    Set pEnumPlace = New MyPlaceCollection
//    pEnumPlace.Reset
//    Set pPlace = pEnumPlace.Next
//    While Not pPlace Is Nothing
//        Print #1, pPlace.Name; ",";
//        Print #1, CStr(pPlace.Geometry.Envelope.XMin); ",";
//        Print #1, CStr(pPlace.Geometry.Envelope.XMax); ",";
//        Print #1, CStr(pPlace.Geometry.Envelope.YMin); ",";
//        Print #1, CStr(pPlace.Geometry.Envelope.YMax)
//        Set pPlace = pEnumPlace.Next
//    Wend

//    Close #1

//    MsgBox "Finshed Exporting My Places", vbInformation, ""

//End Sub

//Public Sub AddLayerFileToMap(Path As String)
//    Dim FilePath As String
//    FilePath = Path
//    Dim Layer As IGxLayer
//    Dim File As IGxFile

//    Set Layer = New GxLayer
//    Set File = Layer
//    File.Path = FilePath
//    Dim Doc As IMxDocument
//    Set Doc = ThisDocument
//    Doc.FocusMap.AddLayer Layer.Layer
//End Sub

//Private Sub CreateAndAddNewMap()
//    Dim pMxDoc As IMxDocument
//    Set pMxDoc = ThisDocument

//    'Create a new map
//    Dim pMap As IMap
//    Set pMap = New Map
//    pMap.Name = "My Map"

//    'Create a new MapFrame and associate map with it
//    Dim pMapFrame As IMapFrame
//    Set pMapFrame = New MapFrame
//    Set pMapFrame.Map = pMap

//    'Set the position of the new map frame
//    Dim pElement As IElement
//    Dim pEnv As IEnvelope
//    Set pElement = pMapFrame
//    Set pEnv = New Envelope
//    pEnv.PutCoords 0, 0, 5, 5
//    pElement.Geometry = pEnv

//    'Add mapframe to the layout
//    Dim pGraphicsContainer As IGraphicsContainer
//    Set pGraphicsContainer = pMxDoc.PageLayout
//    pGraphicsContainer.AddElement pMapFrame, 0

//    'Make the newly added map the focus map
//    Dim pActiveView As IActiveView
//    Set pActiveView = pMxDoc.ActiveView
//    If TypeOf pActiveView Is IPageLayout Then
//      Set pActiveView.FocusMap = pMap
//    Else
//      Set pMxDoc.ActiveView = pMap
//    End If

//   'Refresh ActiveView and TOC
//    pActiveView.Refresh
//    pMxDoc.CurrentContentsView.Refresh 0

//End Sub

//Public Sub TestExecuteGeoProcessingTool()

//    Dim pParameterArray As IVariantArray
//    Set pParameterArray = New esriSystem.VarArray
//    Dim pResult As IGeoProcessorResult


//    'create fishnet tool
//    pParameterArray.RemoveAll
//    pParameterArray.Add "C:\ArcGIS Files\TestCode\fishnet_test.shp"
//    pParameterArray.Add "113 264"
//    pParameterArray.Add "113 0"
//    pParameterArray.Add "0.1"
//    pParameterArray.Add "0.1"
//    pParameterArray.Add "85"
//    pParameterArray.Add "60"

//    Set pResult = ExecuteGeoProcessingTool( _
//    "CreateFishnet_management", pParameterArray)
//    Set pResult = Nothing



//    'buffer tool
//    pParameterArray.RemoveAll
//    pParameterArray.Add "fishnet_test_label"
//    pParameterArray.Add "C:\ArcGIS Files\TestCode\buffer_test.shp"
//    pParameterArray.Add "0.25"

//    Set pResult = ExecuteGeoProcessingTool( _
//    "Buffer", pParameterArray)
//    Set pResult = Nothing



//'    pParameterArray.RemoveAll
//'    pParameterArray.Add "Buildings"
//'    pParameterArray.Add "buffer_test"
//'
//'    Set pResult = ExecuteGeoProcessingTool( _
//'    "SpatialJoin", pParameterArray)
//'


//    Set pParameterArray = Nothing


//End Sub

//Public Function ExecuteGeoProcessingTool(Name As String, _
//Params As IVariantArray) As IGeoProcessorResult

//    'Intialize the Geoprocessor
//    Dim pGP As IGeoProcessor
//    Set pGP = New GeoProcessor

//    'Set OverWriteOutputs to True
//    pGP.OverwriteOutput = True

//    'Execute MakeFeatureLayer
//    Set ExecuteGeoProcessingTool = _
//    pGP.Execute(Name, Params, Nothing)

//    Set pGP = Nothing

//End Function


//Public Sub ExecuteGeoProcessingToolSample()
//  '--------------------------------------------------------------------------------------------------------
//  'STEP 1: Make feature layers using the MakeFeatureLayer tool for the inputs to the SelectByLocation tool.
//  '--------------------------------------------------------------------------------------------------------

//  'Intialize the Geoprocessor
//  Dim pGP As IGeoProcessor
//  Set pGP = New GeoProcessor

//  'Set OverWriteOutputs to True
//  pGP.OverwriteOutput = True

//  'Create the array of parameters for the MakeFeatureLayer Tool
//  Dim pParameterArray As IVariantArray
//  Set pParameterArray = New esriSystem.VarArray

//  pParameterArray.Add "C:\gp\nfld.gdb\wells"
//  pParameterArray.Add "Wells_Lyr"

//  Dim pResult As IGeoProcessorResult

//  'Execute MakeFeatureLayer
//  Set pResult = pGP.Execute("MakeFeatureLayer_management", pParameterArray, Nothing)
//  ReturnMessages pResult

//  'Create the array of parameters for the MakeFeatureLayer Tool
//  pParameterArray.RemoveAll
//  pParameterArray.Add "C:\gp\nfld.gdb\bedrock"
//  pParameterArray.Add "bedrock_Lyr"

//  'Execute MakeFeatureLayer
//  Set pResult = pGP.Execute("MakeFeatureLayer_management", pParameterArray, Nothing)
//  ReturnMessages pResult

//  '-----------------------------------------------------------------------------
//  'STEP 2: Execute SelectLayerByLocation using the feature layers
//  '        to select all wells that intersect theb bedrock geololgy.
//  '-----------------------------------------------------------------------------

//  'Create the array of parameters for the SelectLayerByLocation Tool
//  pParameterArray.RemoveAll
//  pParameterArray.Add "Wells_Lyr"
//  pParameterArray.Add "INTERSECT"
//  pParameterArray.Add "bedrock_Lyr"


//  'Execute SelectLayerByLocation
//  Set pResult = pGP.Execute("SelectLayerByLocation_management", pParameterArray, Nothing)
//  ReturnMessages pResult

//  '-------------------------------------------------------------------------------
//  'STEP 3: Execute SelectLayerByAttribute to select all wells that have
//  '        a well yield > 150 L/min.
//  '-------------------------------------------------------------------------------

//  'Create the array of parameters for the SelectLayerByAttribute Tool
//  pParameterArray.RemoveAll
//  pParameterArray.Add "Wells_Lyr"
//  pParameterArray.Add "New_Selection"
//  pParameterArray.Add "WELL_YIELD > 150"

//  'Execute SelectLayerByAttribute

//  Set pResult = pGP.Execute("SelectLayerByAttribute_management", pParameterArray, Nothing)
//  ReturnMessages pResult

//  '---------------------------------------------------------------------------------
//  'STEP 4: Execute CopyFeatures tool to create a new feature class of wells
//  '        with well yield > 150 L/min.
//  '---------------------------------------------------------------------------------

//  'Create the array of parameters for the CopyFeatures tool
//  pParameterArray.RemoveAll
//  pParameterArray.Add "Wells_Lyr"
//  pParameterArray.Add "C:\gp\nfld.gdb\wells_150"

//  'Set the output Coordinate System environment
//  pGP.SetEnvironmentValue "outputCoordinateSystem", _
//  "C:\Program Files\ArcGIS\Coordinate Systems\Projected Coordinate Systems\UTM\Nad 1983\NAD 1983 UTM Zone 21N.prj"

//  'Execute CopyFeatures
//  Set pResult = pGP.Execute( _
//  "CopyFeatures_management", pParameterArray, Nothing)
//  ReturnMessages pResult

//End Sub

//'----------------------------------------------------------
//'Sub routine to return tool messages
//'----------------------------------------------------------
//Public Sub ReturnMessages(ByVal messages As IGeoProcessorResult)

//  Dim I As Long
//  Dim message As String

//  For I = 0 To messages.MessageCount - 1
//     message = messages.GetMessage(I)
//     Debug.Print message
//  Next

//End Sub

//Public Sub NeighborhoodStatistics(InputFile As String, InputField As String, _
//FishnetFeatClass As IFeatureClass, Radius As Double, _
//OutputFilePath As String, OutputFileName As String, _
//x As Double, y As Double, Rows As Long, Cols As Long, _
//CellSize As Double, SpatialReference As ISpatialReference, AddAsLayer As Boolean)
//Dim FishnetFeatClass As IFeatureClass
//Dim DataFeatClass As IFeatureClass

//    Dim InputFileName As String
//    Dim InputFilePath As String
//    Dim InputFileNameGeneric As String
//    Dim OutputFileNameGeneric As String

//    InputFileName = Dir(InputFile)
//    InputFilePath = Replace(InputFile, InputFileName, "", 1)
//    InputFileNameGeneric = left(InputFileName, InStr(1, InputFileName, ".") - 1)
//    OutputFileNameGeneric = left(OutputFileName, InStr(1, OutputFileName, ".") - 1)

//    Set DataFeatClass = OpenShapefile(InputFilePath, InputFileNameGeneric)

//    'create additionsl fields
//    If DataFeatClass.FindField("PointX") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointX"
//    End If
//    CustomToolsAddField DataFeatClass, "PointX", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointX", GeometryClaculationTypePointX
//    If DataFeatClass.FindField("PointY") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointY"
//    End If
//    CustomToolsAddField DataFeatClass, "PointY", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointY", GeometryClaculationTypePointY
//    If DataFeatClass.FindField("Col") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Col"
//    End If
//    CustomToolsAddField DataFeatClass, "Col", esriFieldTypeInteger
//    If DataFeatClass.FindField("Row") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Row"
//    End If
//    CustomToolsAddField DataFeatClass, "Row", esriFieldTypeInteger

//    'create index
//    CreateIndex DataFeatClass, "PointX"
//    CreateIndex DataFeatClass, "PointY"
//    CreateIndex DataFeatClass, "Col"
//    CreateIndex DataFeatClass, "Row"

//    Dim s As Variant
//    Dim e As Variant
//    s = Time

//    '2.95
//'    NeighborhoodStatisticsEx_DATA_IS_FEATURE_CURSOR_WITH_QUERY_FILTER FishnetFeatClass, Radius, _
//    DataFeatClass, "PointX", "PointY", _
//    InputField, OutputFilePath, OutputFileNameGeneric, _
//    SpatialReference , True, AddAsLayer

//    '1.23
//'    NeighborhoodStatisticsEx_DATA_IS_FEATURE_CURSOR_WITH_SPATIAL_FILTER  FishnetFeatClass, Radius, _
//    DataFeatClass, "PointX", "PointY", _
//    InputField, OutputFilePath, OutputFileNameGeneric, _
//    SpatialReference , True, AddAsLayer

//    '1.04
//    NeighborhoodStatisticsEx_DATA_IS_ARRAY FishnetFeatClass, _
//    Radius, CellSize, x, y, Rows, Cols, DataFeatClass, "Row", "Col", _
//    "PointX", "PointY", InputField, AnalysisExpression, _
//    OutputFilePath, OutputFileNameGeneric, _
//    SpatialReference, True, AddAsLayer

//    e = Time
//    MsgBox s - e

//'    Set FeatClass = OpenShapefile(InputFilePath, InputFileNameGeneric)
//    CustomToolsRemoveField FeatClass, "PointX"
//    CustomToolsRemoveField FeatClass, "PointY"
//    CustomToolsRemoveField FeatClass, "Col"
//    CustomToolsRemoveField FeatClass, "Row"
//    Set FeatClass = Nothing

//'    Set FeatClass = OpenShapefile(OutputFilePath, "tmpFish_Net")
//'    DeleteShapefile FeatClass
//'    Set FeatClass = Nothing

//    Exit Sub

//End Sub



//Public Sub CustomToolsCreateCentroid(FeatClass As IFeatureClass, Path As String, Name As String, _
//SpatialReference As ISpatialReference, AddSpatialIndex As Boolean, AddAsLayer As Boolean)

//    On Error GoTo EH

//    Dim I As Long

//    ' copy source feature class fields .
//    Dim pFields As IFields
//    Dim pFieldsEdit As IFieldsEdit
//    Set pFields = New Fields
//    Set pFieldsEdit = pFields


//    'create geometry field
//    pFieldsEdit.AddField CreateField("Shape", esriFieldTypeGeometry, _
//     , esriGeometryPoint, SpatialReference)


//    'copy additional fields
//    Dim pClone As IClone
//    For I = 0 To FeatClass.Fields.FieldCount - 1
//        If FeatClass.Fields.Field(I).Type <> esriFieldTypeGeometry And _
//        FeatClass.Fields.Field(I).Type <> esriFieldTypeOID Then
//            Set pClone = FeatClass.Fields.Field(I)
//            pFieldsEdit.AddField pClone.Clone
//        End If
//    Next I


//    ' create the shapefile
//    Dim pInsertFeatClass As IFeatureClass
//    Set pInsertFeatClass = CreateShapefile(Path, Name, pFields)


//    ' create select feature class cursor .
//    Dim pSelectFeatCur As IFeatureCursor
//    Set pSelectFeatCur = GetReadOnlyFeatureCursor(FeatClass, "1=1")


//    ' create insert feature class cursor and buffer
//    Dim pInsertFeatCur As IFeatureCursor
//    Set pInsertFeatCur = pInsertFeatClass.Insert(True)
//    Dim pInsertFeatBuff As IFeatureBuffer
//    Set pInsertFeatBuff = pInsertFeatClass.CreateFeatureBuffer


//    'variables decleretion
//    Dim pPoint As IPoint
//    Dim pSelectFeat As IFeature
//    Dim pSelectGeo As IGeometry
//    Dim pPolygon As IPolygon
//    Dim pPointColl As IPointCollection


//    ' write the features out to the shapefile.
//    Dim lngCurrentFeatureCount As Long
//    lngCurrentFeatureCount = 0
//    Set pSelectFeat = pSelectFeatCur.NextFeature
//    Do Until pSelectFeat Is Nothing
//'        For i = 0 To pSelectFeat.Fields.FieldCount - 1
//'            If pSelectFeat.Fields.Field(i).Type = esriFieldTypeGeometry Then
//'                'get source feature's geometry
//'                Set pSelectGeo = pSelectFeat.Shape
//'                'assume geometry is polygon
//'                Set pPolygon = pSelectGeo
//'                Set pPoint = GetFeatureCentroid(pPolygon)
//'                Set pInsertFeatBuff.Shape = pPoint
//'            Else
//'                If pSelectFeat.Fields.Field(i).Type <> esriFieldTypeOID Then
//'                    pInsertFeatBuff.Value(pInsertFeatBuff.Fields.FindField _
//'                    (pSelectFeat.Fields.Field(i).Name)) = pSelectFeat.Value(i)
//'                End If
//'            End If
//'        Next i

//         'apply geometry field into insertFeature shape field
//         Set pPoint = GetFeatureCentroid(pSelectFeat)
//         Set pInsertFeatBuff.Shape = pPoint
//        'apply selectFeature fields into insertFeature fields
//        For I = 0 To pSelectFeat.Fields.FieldCount - 1
//            If pSelectFeat.Fields.Field(I).Type <> esriFieldTypeOID And _
//            pSelectFeat.Fields.Field(I).Type <> esriFieldTypeGeometry Then
//                pInsertFeatBuff.Value(pInsertFeatBuff.Fields.FindField _
//                (pSelectFeat.Fields.Field(I).Name)) = pSelectFeat.Value(I)
//            End If
//        Next I

//        pInsertFeatCur.InsertFeature pInsertFeatBuff
//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        Set pSelectFeat = pSelectFeatCur.NextFeature
//        DoEvents
//    Loop


//    Set pSelectFeatCur = Nothing
//    Set pInsertFeatBuff = Nothing
//    Set pInsertFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pInsertFeatClass) Then GoTo CantMakeSpatialIndex
//    End If

//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The centroid feature class " & Path & "\" & Name & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pInsertFeatClass
//            pFeatLayer.Name = Name
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Sub

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Sub


//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The fishnet " & Path & "\" & Name & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Sub

//End Sub

//Public Sub CustomToolsBuffer(FeatClass As IFeatureClass, Path As String, _
//Name As String, Distance As Double, SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean)

//    On Error GoTo EH


//    If FeatClass.ShapeType <> esriGeometryPoint Then
//        GoTo NotSupportedGeometry
//    End If

//    Dim I As Long

//    'copy source feature class fields .
//    Dim pFields As IFields
//    Dim pFieldsEdit As IFieldsEdit
//    Set pFields = New Fields
//    Set pFieldsEdit = pFields


//    'create geometry field
//    pFieldsEdit.AddField CreateField("Shape", esriFieldTypeGeometry, _
//     , esriGeometryPolygon, SpatialReference)


//    'copy additional fields
//    Dim pClone As IClone
//    For I = 0 To FeatClass.Fields.FieldCount - 1
//        If FeatClass.Fields.Field(I).Type <> esriFieldTypeGeometry And _
//        FeatClass.Fields.Field(I).Type <> esriFieldTypeOID Then
//            Set pClone = FeatClass.Fields.Field(I)
//            pFieldsEdit.AddField pClone.Clone
//        End If
//    Next I


//    'create distance field
//    pFieldsEdit.AddField CreateField("Distance", esriFieldTypeDouble)

//    ' create the shapefile
//    Dim pInsertFeatClass As IFeatureClass
//    Set pInsertFeatClass = CreateShapefile(Path, Name, pFields)


//    ' create select feature class cursor .
//    Dim pSelectFeatCur As IFeatureCursor
//    Set pSelectFeatCur = GetReadOnlyFeatureCursor(FeatClass, "1=1")


//    ' create insert feature class cursor and buffer
//    Dim pInsertFeatCur As IFeatureCursor
//    Set pInsertFeatCur = pInsertFeatClass.Insert(True)
//    Dim pInsertFeatBuff As IFeatureBuffer
//    Set pInsertFeatBuff = pInsertFeatClass.CreateFeatureBuffer


//    'variables decleretion
//    Dim pPoint As IPoint
//    Dim pSelectFeat As IFeature
//    Dim pSelectGeo As IGeometry
//    Dim pConstructCArc As IConstructCircularArc
//    Dim pSegmentColl As ISegmentCollection

//    ' write the features out to the shapefile.
//    Dim lngCurrentFeatureCount As Long
//    lngCurrentFeatureCount = 0
//    Set pSelectFeat = pSelectFeatCur.NextFeature
//    Do Until pSelectFeat Is Nothing
//'        For i = 0 To pSelectFeat.Fields.FieldCount - 1
//'            If pSelectFeat.Fields.Field(i).Type = esriFieldTypeGeometry Then
//'                'get source feature's geometry
//'                Set pSelectGeo = pSelectFeat.Shape
//'                'assume geometry is point
//'                Set pPoint = pSelectGeo
//'                'Create a circle with given radius
//'                'for graphical representation of buffer
//'                Set pConstructCArc = New CircularArc
//'                pConstructCArc.ConstructCircle pPoint, Distance, True
//'                'Add the circle segment to a polygon geometry
//'                Set pSegmentColl = New Polygon
//'                pSegmentColl.AddSegment pConstructCArc
//'                Set pInsertFeatBuff.Shape = pSegmentColl
//'            Else
//'                If pSelectFeat.Fields.Field(i).Type <> esriFieldTypeOID Then
//'                    pInsertFeatBuff.Value(pInsertFeatBuff.Fields.FindField _
//'                    (pSelectFeat.Fields.Field(i).Name)) = pSelectFeat.Value(i)
//'                End If
//'            End If
//'        Next i


//        If FeatClass.ShapeType = esriGeometryPoint Then
//            'Create a circle with given radius
//            'for graphical representation of buffer
//            Set pConstructCArc = New CircularArc
//            pConstructCArc.ConstructCircle pSelectFeat.Shape, Distance, True
//            'Add the circle segment to a polygon geometry
//            Set pSegmentColl = New Polygon
//            pSegmentColl.AddSegment pConstructCArc
//            Set pInsertFeatBuff.Shape = pSegmentColl
//        End If

//        For I = 0 To pSelectFeat.Fields.FieldCount - 1
//            If pSelectFeat.Fields.Field(I).Type <> esriFieldTypeOID And _
//            pSelectFeat.Fields.Field(I).Type <> esriFieldTypeGeometry Then
//                pInsertFeatBuff.Value(pInsertFeatBuff.Fields.FindField _
//                (pSelectFeat.Fields.Field(I).Name)) = pSelectFeat.Value(I)
//            End If
//        Next I

//        pInsertFeatBuff.Value(pInsertFeatBuff.Fields.FindField _
//        ("Distance")) = Distance

//        pInsertFeatCur.InsertFeature pInsertFeatBuff
//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        Set pSelectFeat = pSelectFeatCur.NextFeature
//        DoEvents
//    Loop


//    Set pSelectFeatCur = Nothing
//    Set pInsertFeatBuff = Nothing
//    Set pInsertFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pInsertFeatClass) Then GoTo CantMakeSpatialIndex
//    End If

//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The fishnet " & Path & "\" & Name & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pInsertFeatClass
//            pFeatLayer.Name = Name
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Sub

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Sub


//NotSupportedGeometry:
//  MsgBox "Not supported geometry." & vbCrLf & _
//    "Currently support only point buffering.", _
//     vbExclamation, "Can not proceed"
//  Exit Sub

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The fishnet " & Path & "\" & Name & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Sub

//End Sub


//Private Function OpenDBFConnection(Path As String) As ADODB.Connection

//    Set OpenDBFConnection = New ADODB.Connection

//    '.DataPath is /path/to/file excluding the filename
//    'OpenDBFConnection.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & _
//    'Path & ";Extended Properties=DBase IV"

//    'OpenDBFConnection.Open "driver={Microsoft dBase Driver (*.dbf)};" & _
//    "driverid=277;dbq=" & Path

//    'open connection to DBF file

//    Dim sql_dbfcon As String
//    sql_dbfcon = "DSN=Visual FoxPro Tables;UID=;SourceDB=" & Path & ";" & _
//                 "SourceType=DBF;Exclusive=No;BackgroundFetch=Yes;" & _
//                 "Collate=Machine;NUll=Yes;Deleted=Yes;"
//    With OpenDBFConnection
//        .Provider = "MSDASQL.1;"
//        .ConnectionString = sql_dbfcon
//        .Open
//    End With

//End Function

//Private Function OpenDBFRecordset(Conn As ADODB.Connection, _
//SQL As String) As ADODB.Recordset
//    Dim strSQL As String
//    Set OpenDBFRecordset = New ADODB.Recordset
//    'For DBF, the TABLE is the filename.dbf
//    OpenDBFRecordset.Open SQL, Conn, adOpenDynamic, adLockReadOnly
//End Function

//Public Function NeighborhoodStatisticsEx_DATA_IS_RS(FishnetFeatClass As IFeatureClass, _
//Radius As Double, DataConn As ADODB.Connection, _
//InputFileName As String, XFieldName As String, YFieldName As String, _
//ZFieldName As String, OutputFilePath As String, OutputFileName As String, _
//SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean) As Boolean

//   On Error GoTo EH

//    Dim pInsertDataset As IDataset
//    Dim pFishnetDataset As IDataset
//    Dim pOutputDataset As IDataset
//    Dim pOutputFeatClass As IFeatureClass

//    'copy fishnet to output
//    Dim pFWS As IFeatureWorkspace
//    Dim pWorkspaceFactory As IWorkspaceFactory
//    Set pWorkspaceFactory = New ShapefileWorkspaceFactory
//    Set pFWS = pWorkspaceFactory.OpenFromFile(OutputFilePath, 0)
//    Set pFishnetDataset = FishnetFeatClass
//    Set pOutputDataset = pFishnetDataset.Copy(OutputFileName, pFWS)
//    Set pOutputFeatClass = pOutputDataset

//    pOutputFeatClass.AddField CreateField("Sum", esriFieldTypeDouble)
//    pOutputFeatClass.AddField CreateField("Count", esriFieldTypeInteger)
//    pOutputFeatClass.AddField CreateField("Mean", esriFieldTypeDouble)

//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    CentroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY")

//    Dim pOutputFeatCur As IFeatureCursor
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1")

//    Dim pOutputFeat As IFeature
//    Dim lngCurrentFeatureCount As Long
//    lngCurrentFeatureCount = 0
//    Dim s As Double
//    Dim C As Long
//    Dim m As Double
//    Dim pDataRS As ADODB.Recordset
//    Dim sSql As String

//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        sSql = "SELECT " & XFieldName & ", " & YFieldName & ", " & ZFieldName & _
//            " FROM " & InputFileName & ".dbf" & _
//            " WHERE " & XFieldName & " >= " & _
//            pOutputFeat.Value(CentroidXFieldIndex) - Radius & _
//            " AND " & XFieldName & " <= " & _
//            pOutputFeat.Value(CentroidXFieldIndex) + Radius & _
//            " AND " & YFieldName & " >= " & _
//            pOutputFeat.Value(CentroidYFieldIndex) - Radius & _
//            " AND " & YFieldName & " <= " & _
//            pOutputFeat.Value(CentroidYFieldIndex) + Radius
//         Set pDataRS = OpenDBFRecordset(DataConn, sSql)

//        s = 0: C = 0: m = 0
//        Do Until pDataRS.EOF
//            If Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//            pDataRS(XFieldName)) ^ 2 + _
//            (pOutputFeat.Value(CentroidYFieldIndex) - _
//            pDataRS(YFieldName)) ^ 2) <= Radius Then
//                s = s + pDataRS(ZFieldName)
//                C = C + 1
//            End If
//            pDataRS.MoveNext
//            DoEvents
//        Loop

//        pDataRS.Close
//        Set pDataRS = Nothing
//        If C = 0 Then
//            m = 0
//        Else
//            m = s / C
//        End If

//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Sum")) = s
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Count")) = C
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Mean")) = m
//        pOutputFeatCur.UpdateFeature pOutputFeat

//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pOutputFeatClass) Then GoTo CantMakeSpatialIndex
//    End If

//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pOutputFeatClass
//            pFeatLayer.Name = OutputFileName
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Function

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Function

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Function

//End Function


//Public Function NeighborhoodStatisticsEx_DATA_IS_FEATURE_CURSOR_WITH_QUERY_FILTER(FishnetFeatClass As IFeatureClass, _
//Radius As Double, DataFeatClass As IFeatureClass, _
//XFieldName As String, YFieldName As String, ZFieldName As String, _
//OutputFilePath As String, OutputFileName As String, _
//SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean) As Boolean

//   On Error GoTo EH
//    Dim I As Long
//    Dim J As Long

//    Dim pInsertDataset As IDataset
//    Dim pFishnetDataset As IDataset
//    Dim pOutputDataset As IDataset
//    Dim pOutputFeatClass As IFeatureClass

//    'copy fishnet to output
//    Dim pFWS As IFeatureWorkspace
//    Dim pWorkspaceFactory As IWorkspaceFactory
//    Set pWorkspaceFactory = New ShapefileWorkspaceFactory
//    Set pFWS = pWorkspaceFactory.OpenFromFile(OutputFilePath, 0)
//    Set pFishnetDataset = FishnetFeatClass
//    Set pOutputDataset = pFishnetDataset.Copy(OutputFileName, pFWS)
//    Set pOutputFeatClass = pOutputDataset

//    'add some additional fields
//    pOutputFeatClass.AddField CreateField("Sum", esriFieldTypeDouble)
//    pOutputFeatClass.AddField CreateField("Count", esriFieldTypeInteger)
//    pOutputFeatClass.AddField CreateField("Mean", esriFieldTypeDouble)

//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    Dim XFieldIndex As Long
//    Dim YFieldIndex As Long
//    Dim ZFieldIndex As Long
//    CentroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY")
//    XFieldIndex = DataFeatClass.Fields.FindField(XFieldName)
//    YFieldIndex = DataFeatClass.Fields.FindField(YFieldName)
//    ZFieldIndex = DataFeatClass.Fields.FindField(ZFieldName)

//    Dim pOutputFeatCur As IFeatureCursor
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1")
//    Dim pOutputFeat As IFeature
//    Dim pDataFeatCur As IFeatureCursor
//    Dim pDataFeat As IFeature

//    Dim lngCurrentFeatureCount As Long
//    lngCurrentFeatureCount = 0
//    Dim s As Double
//    Dim C As Long
//    Dim m As Double
//    Dim sSql As String

//'    Set pDataFeatCur = GetReadOnlyFeatureCursor(DataFeatClass, "")
//'    i = 0
//'    Set pDataFeat = pDataFeatCur.NextFeature
//'    Do Until pDataFeat Is Nothing
//'        i = i + 1
//'        Set pDataFeat = pDataFeatCur.NextFeature
//'        DoEvents
//'    Loop
//'    Set pDataFeatCur = Nothing
//'    Dim InputArr() As Variant
//'    ReDim InputArr(1 To i, 1 To 3) As Variant
//'
//'    Set pDataFeatCur = GetReadOnlyFeatureCursor(DataFeatClass, "")
//'    i = 1
//'    Set pDataFeat = pDataFeatCur.NextFeature
//'    Do Until pDataFeat Is Nothing
//'        InputArr(i, 1) = pDataFeat.Value(XFieldIndex)
//'        InputArr(i, 2) = pDataFeat.Value(YFieldIndex)
//'        InputArr(i, 3) = pDataFeat.Value(ZFieldIndex)
//'        Set pDataFeat = pDataFeatCur.NextFeature
//'        DoEvents
//'        i = i + 1
//'    Loop

//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        'create data feature class cursor.
//        sSql = XFieldName & " >= " & _
//            pOutputFeat.Value(CentroidXFieldIndex) - Radius & _
//            " AND " & XFieldName & " <= " & _
//            pOutputFeat.Value(CentroidXFieldIndex) + Radius & _
//            " AND " & YFieldName & " >= " & _
//            pOutputFeat.Value(CentroidYFieldIndex) - Radius & _
//            " AND " & YFieldName & " <= " & _
//            pOutputFeat.Value(CentroidYFieldIndex) + Radius
//        Set pDataFeatCur = GetReadOnlyFeatureCursor(DataFeatClass, sSql)

//        s = 0: C = 0: m = 0
//        Set pDataFeat = pDataFeatCur.NextFeature
//        Do Until pDataFeat Is Nothing
//            If Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//            pDataFeat.Value(XFieldIndex)) ^ 2 + _
//            (pOutputFeat.Value(CentroidYFieldIndex) - _
//            pDataFeat.Value(YFieldIndex)) ^ 2) <= Radius Then
//                s = s + pDataFeat.Value(ZFieldIndex)
//                C = C + 1
//            End If
//            Set pDataFeat = pDataFeatCur.NextFeature
//            DoEvents
//        Loop
//        Set pDataFeatCur = Nothing
//        If C = 0 Then
//            m = 0
//        Else
//            m = s / C
//        End If

//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Sum")) = s
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Count")) = C
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Mean")) = m
//        pOutputFeatCur.UpdateFeature pOutputFeat

//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pOutputFeatClass) Then GoTo CantMakeSpatialIndex
//    End If

//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pOutputFeatClass
//            pFeatLayer.Name = OutputFileName
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Function

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Function

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Function

//End Function


//Public Function NeighborhoodStatisticsEx_DATA_IS_ARRAY(FishnetFeatClass As IFeatureClass, _
//Analysis As AnalysisParams, CellSize As Double, _
//FishnetMinX As Double, FishnetMinY As Double, Rows As Long, Cols As Long, _
//OutputFilePath As String, OutputFileName As String, OutputFileNameGeneric As String, _
//OutputDatasetType As String, SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean) As Boolean

//    On Error GoTo EH

//    'set input
//    Dim DataFeatClass As IFeatureClass
//    Set DataFeatClass = OpenShapefile(Analysis.InputFilePath, _
//    Analysis.InputFileNameGeneric)
//    If DataFeatClass.FindField("PointX") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointX"
//    End If
//    CustomToolsAddField DataFeatClass, "PointX", esriFieldTypeDouble
//    UpdateProgressStatus ProgressVal, "Calculating Data PointX ..."
//    CustomToolsCalcGeometry DataFeatClass, "PointX", GeometryClaculationTypePointX
//    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//    If DataFeatClass.FindField("PointY") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointY"
//    End If
//    CustomToolsAddField DataFeatClass, "PointY", esriFieldTypeDouble
//    UpdateProgressStatus ProgressVal, "Calculating Data PointY ..."
//    CustomToolsCalcGeometry DataFeatClass, "PointY", GeometryClaculationTypePointY
//    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//    If DataFeatClass.FindField("Col") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Col"
//    End If
//    CustomToolsAddField DataFeatClass, "Col", esriFieldTypeInteger
//    If DataFeatClass.FindField("Row") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Row"
//    End If
//    CustomToolsAddField DataFeatClass, "Row", esriFieldTypeInteger
//    CreateIndex DataFeatClass, "PointX"
//    CreateIndex DataFeatClass, "PointY"
//    CreateIndex DataFeatClass, "Col"
//    CreateIndex DataFeatClass, "Row"

//    Dim pInsertDataset As IDataset
//    Dim pFishnetDataset As IDataset
//    Dim pOutputDataset As IDataset
//    Dim pOutputFeatClass As IFeatureClass

//    'copy fishnet to output
//    Set pFishnetDataset = FishnetFeatClass

//    Dim pFWS As IFeatureWorkspace
//    Set pFWS = OpenFeatureWorkspace(OutputFilePath)
//    Set pOutputDataset = pFishnetDataset.Copy(OutputFileNameGeneric, pFWS)
//    Set pOutputFeatClass = pOutputDataset


//    'delete fishnet
//    DeleteShapefile FishnetFeatClass

//    'add alalysis output fields
//    Dim OutputFieldName As String
//    Dim OutputFieldIndex As Long
//    Select Case Analysis.Type
//        Case enumAnalysisType.AnalysisTypeMax
//            OutputFieldName = "Max_" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeMin
//            OutputFieldName = "Min_" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeStDev
//            OutputFieldName = "StDev" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeSum
//            OutputFieldName = "Sum_" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeCount
//            OutputFieldName = "Count_" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeMean
//            OutputFieldName = "Mean_" & Analysis.InputFieldName
//        Case enumAnalysisType.AnalysisTypeClusterAnalysis
//            OutputFieldName = "Clust_" & Analysis.InputFieldName
//    End Select
//    pOutputFeatClass.AddField CreateField( _
//    OutputFieldName, esriFieldTypeDouble)
//    OutputFieldIndex = pOutputFeatClass.Fields.FindField(OutputFieldName)


//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    Dim OutputRowFieldIndex As Long
//    Dim OutputColFieldIndex As Long
//    Dim InputRowFieldIndex As Long
//    Dim InputColFieldIndex As Long
//    Dim XFieldIndex As Long
//    Dim YFieldIndex As Long
//    Dim ZFieldIndex As Long

//    CentroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY")
//    OutputRowFieldIndex = pOutputFeatClass.Fields.FindField("Row")
//    OutputColFieldIndex = pOutputFeatClass.Fields.FindField("Col")
//    InputRowFieldIndex = DataFeatClass.Fields.FindField("Row")
//    InputColFieldIndex = DataFeatClass.Fields.FindField("Col")
//    XFieldIndex = DataFeatClass.Fields.FindField("PointX")
//    YFieldIndex = DataFeatClass.Fields.FindField("PointY")
//    ZFieldIndex = DataFeatClass.Fields.FindField(Analysis.InputFieldName)

//    Dim pDataTable As ITable
//    Set pDataTable = DataFeatClass
//    Dim pDataCur As icursor
//    Dim pDataRow As IRow

//    'applying input row coll values
//    UpdateProgressStatus ProgressVal, "Applying data cells refernce..."
//    Set pDataCur = GetCursor(pDataTable, "")
//    Set pDataRow = pDataCur.NextRow
//    Do Until pDataRow Is Nothing
//        ProgressVal = ProgressVal + 1
//        UpdateProgressStatus ProgressVal
//        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//        pDataRow.Value(InputRowFieldIndex) = IIf( _
//        Round(((pDataRow.Value(YFieldIndex) - FishnetMinY) / CellSize) + 0.5) = 0, _
//        1, Round(((pDataRow.Value(YFieldIndex) - FishnetMinY) / CellSize) + 0.5))
//        pDataRow.Value(InputColFieldIndex) = IIf( _
//        Round(((pDataRow.Value(XFieldIndex) - FishnetMinX) / CellSize) + 0.5) = 0, _
//        1, Round(((pDataRow.Value(XFieldIndex) - FishnetMinX) / CellSize) + 0.5))
//        pDataCur.UpdateRow pDataRow
//        Set pDataRow = pDataCur.NextRow
//        DoEvents
//    Loop
//    Set pDataCur = Nothing

//    Dim SortFields(0 To 1) As SortFieldsParams
//    SortFields(0).FieldName = "Row"
//    SortFields(0).Ascending = True
//    SortFields(0).CaseSensitive = False
//    SortFields(1).FieldName = "Col"
//    SortFields(1).Ascending = True
//    SortFields(1).CaseSensitive = False

//    'sort input by row coll values
//    Dim RowCount As Variant
//    Set pDataCur = GetReadOnlySortedCursor(pDataTable, "", SortFields, RowCount)
//    Dim InputArr() As Variant
//    ReDim InputArr(1 To RowCount, 1 To 5) As Variant
//    Dim InputRowColIndexArr() As RowColInputIndex
//    ReDim InputRowColIndexArr(1 To Rows, 1 To Cols) As RowColInputIndex
//    Dim InputIndex As Long
//    Dim RowIndex  As Long
//    Dim ColIndex  As Long
//    UpdateProgressStatus ProgressVal, "Creating data index table..."
//    RowIndex = 0
//    ColIndex = 0
//    InputIndex = 1
//    Set pDataRow = pDataCur.NextRow
//    Do Until pDataRow Is Nothing
//        ProgressVal = ProgressVal + 1
//        UpdateProgressStatus ProgressVal
//        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//        'applying input array values
//        InputArr(InputIndex, 1) = pDataRow.Value(InputRowFieldIndex)
//        InputArr(InputIndex, 2) = pDataRow.Value(InputColFieldIndex)
//        InputArr(InputIndex, 3) = pDataRow.Value(XFieldIndex)
//        InputArr(InputIndex, 4) = pDataRow.Value(YFieldIndex)
//        InputArr(InputIndex, 5) = pDataRow.Value(ZFieldIndex)

//        'applying input index values
//        If InputArr(InputIndex, 1) > RowIndex Or _
//        InputArr(InputIndex, 2) > ColIndex Then
//            If InputIndex > 1 Then
//                InputRowColIndexArr(RowIndex, ColIndex).EndIndex = InputIndex - 1
//                InputRowColIndexArr(RowIndex, ColIndex).Count = _
//                InputRowColIndexArr(RowIndex, ColIndex).EndIndex - _
//                InputRowColIndexArr(RowIndex, ColIndex).StartIndex + 1
//            End If
//            RowIndex = InputArr(InputIndex, 1)
//            ColIndex = InputArr(InputIndex, 2)
//            InputRowColIndexArr(RowIndex, ColIndex).StartIndex = InputIndex
//        End If
//        Set pDataRow = pDataCur.NextRow
//        InputIndex = InputIndex + 1
//        DoEvents
//    Loop
//    InputRowColIndexArr(RowIndex, ColIndex).EndIndex = InputIndex - 1
//    Set pDataCur = Nothing


//    Dim s As Double
//    Dim C As Long
//    Dim m As Double
//    Dim mx As Double
//    Dim mn As Double
//    Dim vari As Double
//    Dim cc As Long
//    Dim RadiusCells As Long
//    Dim pOutputFeatCur As IFeatureCursor
//    Dim pOutputFeat As IFeature
//    Dim MinRowIndex As Long
//    Dim MaxRowIndex As Long
//    Dim MinColIndex As Long
//    Dim MaxColIndex As Long
//    Dim StartInputIndex As Long
//    Dim EndInputIndex As Long
//    Dim lngCurrentFeatureCount As Long
//    Dim I As Long
//    Dim Neigborhood As Neightborhood
//    Dim NeigborhoodCount As Long

//    If OutputDatasetType = "Raster Datasets" Then
//        Dim PixelData() As Variant
//        ReDim PixelData(0 To Cols - 1, 0 To Rows - 1) As Variant
//    End If


//    lngCurrentFeatureCount = 0

//    'calculate statiatics...
//    UpdateProgressStatus ProgressVal, "Calculating..."
//    RadiusCells = Round((Analysis.Radius / CellSize) + 0.5)
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1")
//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        ProgressVal = ProgressVal + 1
//        UpdateProgressStatus ProgressVal
//        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//        ReDim Neigborhood.ScatterPoints(0) As ScatterPoint
//        NeigborhoodCount = 0
//        Neigborhood.Count = 0
//        Neigborhood.ClusterCount = 0
//        Neigborhood.Sum = 0
//        Neigborhood.Max = 0
//        Neigborhood.Min = 0
//        Neigborhood.SumInverseDistance = 0

//        If Analysis.Radius = 0 Then
//            MinRowIndex = 1
//            MaxRowIndex = Rows
//            MinColIndex = 1
//            MaxColIndex = Cols
//        Else
//            MinRowIndex = pOutputFeat.Value(OutputRowFieldIndex) - RadiusCells
//            MinRowIndex = IIf(MinRowIndex < 1, 1, MinRowIndex)
//            MaxRowIndex = pOutputFeat.Value(OutputRowFieldIndex) + RadiusCells
//            MaxRowIndex = IIf(MaxRowIndex > Rows, Rows, MaxRowIndex)
//            MinColIndex = pOutputFeat.Value(OutputColFieldIndex) - RadiusCells
//            MinColIndex = IIf(MinColIndex < 1, 1, MinColIndex)
//            MaxColIndex = pOutputFeat.Value(OutputColFieldIndex) + RadiusCells
//            MaxColIndex = IIf(MaxColIndex > Cols, Cols, MaxColIndex)
//        End If
//        For RowIndex = MinRowIndex To MaxRowIndex
//            For ColIndex = MinColIndex To MaxColIndex
//                If InputRowColIndexArr(RowIndex, ColIndex).Count > 0 Then
//                    StartInputIndex = _
//                    InputRowColIndexArr(RowIndex, ColIndex).StartIndex
//                    EndInputIndex = _
//                    InputRowColIndexArr(RowIndex, ColIndex).EndIndex
//                    For InputIndex = StartInputIndex To EndInputIndex
//                        If (Analysis.Radius = 0) Or (Analysis.Radius > 0 And _
//                        Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//                        InputArr(InputIndex, 3)) ^ 2 + _
//                        (pOutputFeat.Value(CentroidYFieldIndex) - _
//                        InputArr(InputIndex, 4)) ^ 2) <= Analysis.Radius) Then
//                            With Neigborhood
//                                NeigborhoodCount = NeigborhoodCount + 1
//                                If NeigborhoodCount = 1 Then
//                                    ReDim .ScatterPoints(1 To _
//                                    NeigborhoodCount) As ScatterPoint
//                                Else
//                                    ReDim Preserve .ScatterPoints( _
//                                    1 To NeigborhoodCount) As ScatterPoint
//                                End If
//                                .ScatterPoints( _
//                                NeigborhoodCount).Value = InputArr(InputIndex, 5)

//                                Select Case Analysis.SpatialReletionships
//                                    Case SpatialReletionshipsInverseDistance
//                                        .ScatterPoints( _
//                                        NeigborhoodCount).Distance = _
//                                        Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//                                        InputArr(InputIndex, 3)) ^ 2 + _
//                                        (pOutputFeat.Value(CentroidYFieldIndex) - _
//                                        InputArr(InputIndex, 4)) ^ 2)

//                                        .ScatterPoints( _
//                                        NeigborhoodCount).InverseDistance = _
//                                        1 / .ScatterPoints( _
//                                        NeigborhoodCount).Distance

//                                        .SumInverseDistance = _
//                                        .SumInverseDistance + _
//                                        .ScatterPoints( _
//                                        NeigborhoodCount).InverseDistance

//                                    Case SpatialReletionshipsFixedDistance
//                                        .Count = NeigborhoodCount

//                                        If NeigborhoodCount = 1 Then
//                                            .Max = _
//                                            .ScatterPoints(NeigborhoodCount).Value
//                                        Else
//                                            If .ScatterPoints(NeigborhoodCount).Value > _
//                                            .Max Then
//                                                .Max = _
//                                                .ScatterPoints(NeigborhoodCount).Value
//                                            End If
//                                        End If

//                                         If NeigborhoodCount = 1 Then
//                                            .Min = _
//                                            .ScatterPoints(NeigborhoodCount).Value
//                                        Else
//                                            If .ScatterPoints(NeigborhoodCount).Value < _
//                                            .Min Then
//                                                .Min = _
//                                                .ScatterPoints(NeigborhoodCount).Value
//                                            End If
//                                        End If

//                                        .Sum = .Sum + _
//                                        .ScatterPoints(NeigborhoodCount).Value

//                                        If Analysis.Type = _
//                                        AnalysisTypeClusterAnalysis Then
//                                            If IsWithinCluster(Analysis, _
//                                            .ScatterPoints(NeigborhoodCount).Value) Then
//                                                .ClusterCount = _
//                                                .ClusterCount + 1
//                                            End If
//                                        End If
//                                End Select
//                            End With
//                        End If
//                    Next InputIndex
//                End If
//            Next ColIndex
//        Next RowIndex
//        DoEvents

//        If NeigborhoodCount > 0 Then
//            Select Case Analysis.SpatialReletionships
//                Case SpatialReletionshipsInverseDistance
//                    With Neigborhood
//                        For I = 1 To NeigborhoodCount
//                            .ScatterPoints(I).Weight = _
//                            .ScatterPoints(I).InverseDistance / _
//                            .SumInverseDistance

//                            .ScatterPoints(I).WeightedValue = _
//                            .ScatterPoints(I).Value * _
//                            .ScatterPoints(I).Weight

//                            .Count = .Count + _
//                            .ScatterPoints(I).Weight

//                            If I = 1 Then
//                                .Max = .ScatterPoints(I).WeightedValue
//                            Else
//                                If .ScatterPoints(I).WeightedValue > _
//                                .Max Then
//                                    .Max = _
//                                    .ScatterPoints(I).WeightedValue
//                                End If
//                            End If
//                            If I = 1 Then
//                                .Min = .ScatterPoints(I).WeightedValue
//                            Else
//                                If .ScatterPoints(I).WeightedValue < _
//                                .Min Then
//                                    .Min = _
//                                    .ScatterPoints(I).WeightedValue
//                                End If
//                            End If
//                            .Sum = .Sum + _
//                            .ScatterPoints(I).WeightedValue

//                            If Analysis.Type = _
//                            AnalysisTypeClusterAnalysis Then
//                                If IsWithinCluster(Analysis, _
//                                .ScatterPoints(I).Value) Then
//                                    .ClusterCount = _
//                                    .ClusterCount + _
//                                    .ScatterPoints(I).Weight
//                                End If
//                            End If
//                        Next I
//                    End With
//            End Select


//            Select Case OutputDatasetType
//                Case "Shapefiles"
//                    Select Case Analysis.Type
//                        Case enumAnalysisType.AnalysisTypeMax
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.Max
//                        Case enumAnalysisType.AnalysisTypeMin
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.Min
//                        Case enumAnalysisType.AnalysisTypeSum
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.Sum
//                        Case enumAnalysisType.AnalysisTypeCount
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.Count
//                        Case enumAnalysisType.AnalysisTypeMean
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.Sum / Neigborhood.Count
//                        Case enumAnalysisType.AnalysisTypeStDev
//                            If UBound(NeightborhoodData) > 1 Then
//                                vari = 0
//                                For I = 1 To UBound(NeightborhoodData)
//                                vari = vari + _
//                                ((NeightborhoodData(I) - (s / C)) ^ 2)
//                                Next I
//                                pOutputFeat.Value(OutputFieldIndex) = _
//                                Sqr(vari / (UBound(NeightborhoodData) - 1))
//                            End If
//                        Case enumAnalysisType.AnalysisTypeClusterAnalysis
//                            pOutputFeat.Value(OutputFieldIndex) = _
//                            Neigborhood.ClusterCount / Neigborhood.Count
//                    End Select
//                Case "Raster Datasets"
//                    Select Case Analysis.Type
//                        Case enumAnalysisType.AnalysisTypeMax
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.Max
//                        Case enumAnalysisType.AnalysisTypeMin
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.Min
//                        Case enumAnalysisType.AnalysisTypeSum
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.Sum
//                        Case enumAnalysisType.AnalysisTypeCount
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.Count
//                        Case enumAnalysisType.AnalysisTypeMean
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.Sum / Neigborhood.Count
//                        Case enumAnalysisType.AnalysisTypeStDev
//                            If UBound(NeightborhoodData) > 1 Then
//                                vari = 0
//                                For I = 1 To UBound(NeightborhoodData)
//                                vari = vari + _
//                                ((NeightborhoodData(I) - (s / C)) ^ 2)
//                                Next I
//                                PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                                pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                                Sqr(vari / (UBound(NeightborhoodData) - 1))
//                            End If
//                        Case enumAnalysisType.AnalysisTypeClusterAnalysis
//                            PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                            pOutputFeat.Value(OutputRowFieldIndex) - 1) = _
//                            Neigborhood.ClusterCount / Neigborhood.Count
//                    End Select
//            End Select
//        Else
//            Select Case OutputDatasetType
//                Case "Shapefiles"
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField(OutputFieldName)) = -1
//                Case "Raster Datasets"
//                    PixelData(pOutputFeat.Value(OutputColFieldIndex) - 1, _
//                    pOutputFeat.Value(OutputRowFieldIndex) - 1) = -1
//            End Select
//        End If
//        pOutputFeatCur.UpdateFeature pOutputFeat
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing


//    Select Case OutputDatasetType
//        Case "Shapefiles"
//            ' add a spatial index to the new shapefile (very important for large shapefiles).
//            If AddSpatialIndex = True Then
//              If Not CreateSpatialIndex(pOutputFeatClass) Then GoTo CantMakeSpatialIndex
//            End If
//        Case "Raster Datasets"
//            Dim pRaster As IRaster
//            UpdateProgressStatus ProgressVal, "Creating raster..."
//            Set pRaster = CreateFileRaster(OutputFilePath, OutputFileName, _
//            "IMAGINE Image", FishnetMinX, FishnetMinY, Cols, Rows, _
//            CellSize, 1, PixelData, SpatialReference, -1, True)
//            If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
//    End Select


//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pEnv As IEnvelope

//            Select Case OutputDatasetType
//                Case "Shapefiles"
//                    Dim pFeatLayer As IFeatureLayer
//                    Set pFeatLayer = New FeatureLayer
//                    Set pFeatLayer.FeatureClass = pOutputFeatClass
//                    pFeatLayer.Name = OutputFileNameGeneric
//                    pMxDoc.AddLayer pFeatLayer
//                    Set pEnv = pFeatLayer.AreaOfInterest
//                Case "Raster Datasets"
//                    Dim pRasterLayer As IRasterLayer
//                    Set pRasterLayer = New RasterLayer
//                    pRasterLayer.CreateFromRaster pRaster
//                    pRasterLayer.Name = OutputFileNameGeneric
//                    pMxDoc.AddLayer pRasterLayer
//                    Set pEnv = pRasterLayer.AreaOfInterest
//            End Select

//            pMxDoc.UpdateContents
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Function

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Function

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Function

//End Function


//Public Function NeighborhoodStatisticsEx_DATA_IS_ARRAY_OLD(FishnetFeatClass As IFeatureClass, _
//Analysis As AnalysisParams, CellSize As Double, FishnetMinX As Double, _
//FishnetMinY As Double, Rows As Long, Cols As Long, _
//OutputFilePath As String, OutputFileName As String, _
//SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean) As Boolean

// '   On Error GoTo EH

//    'set input
//    Dim DataFeatClass As IFeatureClass
//    Set DataFeatClass = OpenShapefile(Analysis.InputFilePath, _
//    Analysis.InputFileNameGeneric)
//    If DataFeatClass.FindField("PointX") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointX"
//    End If
//    CustomToolsAddField DataFeatClass, "PointX", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointX", GeometryClaculationTypePointX
//    If DataFeatClass.FindField("PointY") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointY"
//    End If
//    CustomToolsAddField DataFeatClass, "PointY", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointY", GeometryClaculationTypePointY
//    If DataFeatClass.FindField("Col") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Col"
//    End If
//    CustomToolsAddField DataFeatClass, "Col", esriFieldTypeInteger
//    If DataFeatClass.FindField("Row") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "Row"
//    End If
//    CustomToolsAddField DataFeatClass, "Row", esriFieldTypeInteger
//    CreateIndex DataFeatClass, "PointX"
//    CreateIndex DataFeatClass, "PointY"
//    CreateIndex DataFeatClass, "Col"
//    CreateIndex DataFeatClass, "Row"

//    Dim pInsertDataset As IDataset
//    Dim pFishnetDataset As IDataset
//    Dim pOutputDataset As IDataset
//    Dim pOutputFeatClass As IFeatureClass

//    'copy fishnet to output
//    Dim pFWS As IFeatureWorkspace
//    Dim pWorkspaceFactory As IWorkspaceFactory
//    Set pWorkspaceFactory = New ShapefileWorkspaceFactory
//    Set pFWS = pWorkspaceFactory.OpenFromFile(OutputFilePath, 0)
//    Set pFishnetDataset = FishnetFeatClass
//    Set pOutputDataset = pFishnetDataset.Copy(OutputFileName, pFWS)
//    Set pOutputFeatClass = pOutputDataset

//    'add alalysis output fields
//    Select Case Analysis.Type
//        Case enumAnalysisType.AnalysisTypeSum
//            pOutputFeatClass.AddField CreateField( _
//            "Sum", esriFieldTypeDouble)
//        Case enumAnalysisType.AnalysisTypeCount
//            pOutputFeatClass.AddField CreateField( _
//            "Count", esriFieldTypeInteger)
//        Case enumAnalysisType.AnalysisTypeMean
//            pOutputFeatClass.AddField CreateField( _
//            "Mean", esriFieldTypeDouble)
//        Case enumAnalysisType.AnalysisTypeClusterAnalysis
//            pOutputFeatClass.AddField CreateField( _
//            "ClusterPer", esriFieldTypeDouble)
//    End Select

//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    Dim OutputRowFieldIndex As Long
//    Dim OutputColFieldIndex As Long
//    Dim InputRowFieldIndex As Long
//    Dim InputColFieldIndex As Long
//    Dim XFieldIndex As Long
//    Dim YFieldIndex As Long
//    Dim ZFieldIndex As Long

//    CentroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY")
//    OutputRowFieldIndex = pOutputFeatClass.Fields.FindField("Row")
//    OutputColFieldIndex = pOutputFeatClass.Fields.FindField("Col")
//    InputRowFieldIndex = DataFeatClass.Fields.FindField("Row")
//    InputColFieldIndex = DataFeatClass.Fields.FindField("Col")
//    XFieldIndex = DataFeatClass.Fields.FindField("PointX")
//    YFieldIndex = DataFeatClass.Fields.FindField("PointY")
//    ZFieldIndex = DataFeatClass.Fields.FindField(Analysis.InputFieldName)

//    Dim pDataTable As ITable
//    Set pDataTable = DataFeatClass
//    Dim pDataCur As icursor
//    Dim pDataRow As IRow

//    'applying input row coll values
//    Set pDataCur = GetCursor(pDataTable, "")
//    Set pDataRow = pDataCur.NextRow
//    Do Until pDataRow Is Nothing
//        pDataRow.Value(InputRowFieldIndex) = IIf( _
//        Round(((pDataRow.Value(YFieldIndex) - FishnetMinY) / CellSize) + 0.5) = 0, _
//        1, Round(((pDataRow.Value(YFieldIndex) - FishnetMinY) / CellSize) + 0.5))
//        pDataRow.Value(InputColFieldIndex) = IIf( _
//        Round(((pDataRow.Value(XFieldIndex) - FishnetMinX) / CellSize) + 0.5) = 0, _
//        1, Round(((pDataRow.Value(XFieldIndex) - FishnetMinX) / CellSize) + 0.5))
//        pDataCur.UpdateRow pDataRow
//        Set pDataRow = pDataCur.NextRow
//        DoEvents
//    Loop
//    Set pDataCur = Nothing

//    Dim SortFields(0 To 1) As SortFieldsParams
//    SortFields(0).FieldName = "Row"
//    SortFields(0).Ascending = True
//    SortFields(0).CaseSensitive = False
//    SortFields(1).FieldName = "Col"
//    SortFields(1).Ascending = True
//    SortFields(1).CaseSensitive = False

//    'sort input by row coll values
//    Dim RowCount As Variant
//    Set pDataCur = GetReadOnlySortedCursor(pDataTable, "", SortFields, RowCount)
//    Dim InputArr() As Variant
//    ReDim InputArr(1 To RowCount, 1 To 5) As Variant
//    Dim InputRowColIndexArr() As RowColInputIndex
//    ReDim InputRowColIndexArr(1 To Rows, 1 To Cols) As RowColInputIndex
//    Dim InputIndex As Long
//    Dim RowIndex  As Long
//    Dim ColIndex  As Long
//    RowIndex = 0
//    ColIndex = 0
//    InputIndex = 1
//    Set pDataRow = pDataCur.NextRow
//    Do Until pDataRow Is Nothing
//        'applying input array values
//        InputArr(InputIndex, 1) = pDataRow.Value(InputRowFieldIndex)
//        InputArr(InputIndex, 2) = pDataRow.Value(InputColFieldIndex)
//        InputArr(InputIndex, 3) = pDataRow.Value(XFieldIndex)
//        InputArr(InputIndex, 4) = pDataRow.Value(YFieldIndex)
//        InputArr(InputIndex, 5) = pDataRow.Value(ZFieldIndex)

//        'applying input index values
//        If InputArr(InputIndex, 1) > RowIndex Or _
//        InputArr(InputIndex, 2) > ColIndex Then
//            If InputIndex > 1 Then
//                InputRowColIndexArr(RowIndex, ColIndex).EndIndex = InputIndex - 1
//                InputRowColIndexArr(RowIndex, ColIndex).Count = _
//                InputRowColIndexArr(RowIndex, ColIndex).EndIndex - _
//                InputRowColIndexArr(RowIndex, ColIndex).StartIndex + 1
//            End If
//            RowIndex = InputArr(InputIndex, 1)
//            ColIndex = InputArr(InputIndex, 2)
//            InputRowColIndexArr(RowIndex, ColIndex).StartIndex = InputIndex
//        End If
//        Set pDataRow = pDataCur.NextRow
//        InputIndex = InputIndex + 1
//        DoEvents
//    Loop
//    InputRowColIndexArr(RowIndex, ColIndex).EndIndex = InputIndex - 1
//    Set pDataCur = Nothing


//    Dim s As Double
//    Dim C As Long
//    Dim m As Double
//    Dim RadiusCells As Long
//    Dim pOutputFeatCur As IFeatureCursor
//    Dim pOutputFeat As IFeature
//    Dim MinRowIndex As Long
//    Dim MaxRowIndex As Long
//    Dim MinColIndex As Long
//    Dim MaxColIndex As Long
//    Dim StartInputIndex As Long
//    Dim EndInputIndex As Long
//    Dim lngCurrentFeatureCount As Long

//    Dim cc As Long


//    lngCurrentFeatureCount = 0

//    'calculate statiatics...
//    RadiusCells = Round((Analysis.Radius / CellSize) + 0.5)
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1")
//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        s = 0: C = 0: cc = 0
//        MinRowIndex = pOutputFeat.Value(OutputRowFieldIndex) - RadiusCells
//        MinRowIndex = IIf(MinRowIndex < 1, 1, MinRowIndex)
//        MaxRowIndex = pOutputFeat.Value(OutputRowFieldIndex) + RadiusCells
//        MaxRowIndex = IIf(MaxRowIndex > Rows, Rows, MaxRowIndex)
//        MinColIndex = pOutputFeat.Value(OutputColFieldIndex) - RadiusCells
//        MinColIndex = IIf(MinColIndex < 1, 1, MinColIndex)
//        MaxColIndex = pOutputFeat.Value(OutputColFieldIndex) + RadiusCells
//        MaxColIndex = IIf(MaxColIndex > Cols, Cols, MaxColIndex)
//        For RowIndex = MinRowIndex To MaxRowIndex
//            For ColIndex = MinColIndex To MaxColIndex
//                If InputRowColIndexArr(RowIndex, ColIndex).Count > 0 Then
//                    StartInputIndex = InputRowColIndexArr(RowIndex, ColIndex).StartIndex
//                    EndInputIndex = InputRowColIndexArr(RowIndex, ColIndex).EndIndex
//                    For InputIndex = StartInputIndex To EndInputIndex
//                        If Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//                        InputArr(InputIndex, 3)) ^ 2 + _
//                        (pOutputFeat.Value(CentroidYFieldIndex) - _
//                        InputArr(InputIndex, 4)) ^ 2) <= Analysis.Radius Then
//                            C = C + 1
//                            Select Case Analysis.Type
//                                Case enumAnalysisType.AnalysisTypeSum
//                                    s = s + InputArr(InputIndex, 5)
//                                Case enumAnalysisType.AnalysisTypeCount
//                                Case enumAnalysisType.AnalysisTypeMean
//                                    s = s + InputArr(InputIndex, 5)
//                                Case enumAnalysisType.AnalysisTypeClusterAnalysis
//                                    If IsWithinCluster(Analysis, _
//                                    InputArr(InputIndex, 5)) Then cc = cc + 1
//                            End Select
//                        End If
//                    Next InputIndex
//                End If
//            Next ColIndex
//        Next RowIndex
//        DoEvents

//        If C > 0 Then
//            Select Case Analysis.Type
//                Case enumAnalysisType.AnalysisTypeSum
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Sum")) = s
//                Case enumAnalysisType.AnalysisTypeCount
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Count")) = C
//                Case enumAnalysisType.AnalysisTypeMean
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Mean")) = s / C
//                Case enumAnalysisType.AnalysisTypeClusterAnalysis
//                    pOutputFeat.Value(pOutputFeat.Fields.FindField( _
//                    "ClusterPer")) = cc / C
//            End Select
//        Else
//            Select Case Analysis.Type
//                Case enumAnalysisType.AnalysisTypeSum
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Sum")) = -1
//                Case enumAnalysisType.AnalysisTypeCount
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Count")) = -1
//                Case enumAnalysisType.AnalysisTypeMean
//                    pOutputFeat.Value( _
//                    pOutputFeat.Fields.FindField("Mean")) = -1
//                Case enumAnalysisType.AnalysisTypeClusterAnalysis
//                    pOutputFeat.Value(pOutputFeat.Fields.FindField( _
//                    "ClusterPer")) = -1
//            End Select
//        End If
//        pOutputFeatCur.UpdateFeature pOutputFeat
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pOutputFeatClass) Then GoTo CantMakeSpatialIndex
//    End If




//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pOutputFeatClass
//            pFeatLayer.Name = OutputFileName
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Function

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Function

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Function

//End Function
//=========================================================================================================




//Public Function NeighborhoodStatisticsEx_DATA_IS_FEATURE_CURSOR_WITH_SPATIAL_FILTER(FishnetFeatClass As IFeatureClass, _
//Radius As Double, DataFeatClass As IFeatureClass, _
//XFieldName As String, YFieldName As String, ZFieldName As String, _
//OutputFilePath As String, OutputFileName As String, _
//SpatialReference As ISpatialReference, _
//AddSpatialIndex As Boolean, AddAsLayer As Boolean) As Boolean

//   On Error GoTo EH
//    Dim I As Long
//    Dim J As Long

//    Dim pInsertDataset As IDataset
//    Dim pFishnetDataset As IDataset
//    Dim pOutputDataset As IDataset
//    Dim pOutputFeatClass As IFeatureClass

//    'copy fishnet to output
//    Dim pFWS As IFeatureWorkspace
//    Dim pWorkspaceFactory As IWorkspaceFactory
//    Set pWorkspaceFactory = New ShapefileWorkspaceFactory
//    Set pFWS = pWorkspaceFactory.OpenFromFile(OutputFilePath, 0)
//    Set pFishnetDataset = FishnetFeatClass
//    Set pOutputDataset = pFishnetDataset.Copy(OutputFileName, pFWS)
//    Set pOutputFeatClass = pOutputDataset

//    'add some additional fields
//    pOutputFeatClass.AddField CreateField("Sum", esriFieldTypeDouble)
//    pOutputFeatClass.AddField CreateField("Count", esriFieldTypeInteger)
//    pOutputFeatClass.AddField CreateField("Mean", esriFieldTypeDouble)

//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    Dim XFieldIndex As Long
//    Dim YFieldIndex As Long
//    Dim ZFieldIndex As Long
//    CentroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY")
//    XFieldIndex = DataFeatClass.Fields.FindField(XFieldName)
//    YFieldIndex = DataFeatClass.Fields.FindField(YFieldName)
//    ZFieldIndex = DataFeatClass.Fields.FindField(ZFieldName)

//    Dim pOutputFeatCur As IFeatureCursor
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1")
//    Dim pOutputFeat As IFeature
//    Dim pDataFeatCur As IFeatureCursor
//    Dim pDataFeat As IFeature

//    Dim lngCurrentFeatureCount As Long
//    lngCurrentFeatureCount = 0
//    Dim s As Double
//    Dim C As Long
//    Dim m As Double
//    Dim sSql As String


//'    Set pDataFeatCur = GetReadOnlyFeatureCursor(DataFeatClass, "")
//'    i = 0
//'    Set pDataFeat = pDataFeatCur.NextFeature
//'    Do Until pDataFeat Is Nothing
//'        i = i + 1
//'        Set pDataFeat = pDataFeatCur.NextFeature
//'        DoEvents
//'    Loop
//'    Set pDataFeatCur = Nothing
//'    Dim InputArr() As Variant
//'    ReDim InputArr(1 To i, 1 To 3) As Variant
//'
//'    Set pDataFeatCur = GetReadOnlyFeatureCursor(DataFeatClass, "")
//'    i = 1
//'    Set pDataFeat = pDataFeatCur.NextFeature
//'    Do Until pDataFeat Is Nothing
//'        InputArr(i, 1) = pDataFeat.Value(XFieldIndex)
//'        InputArr(i, 2) = pDataFeat.Value(YFieldIndex)
//'        InputArr(i, 3) = pDataFeat.Value(ZFieldIndex)
//'        Set pDataFeat = pDataFeatCur.NextFeature
//'        DoEvents
//'        i = i + 1
//'    Loop

//    Dim pPoint As IPoint
//    Dim pConstructCArc As IConstructCircularArc
//    Dim pSegmentColl As ISegmentCollection
//    Set pConstructCArc = New CircularArc
//    Dim pSFilter As ISpatialFilter
//    Set pSFilter = New SpatialFilter
//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        Set pPoint = New Point
//        pPoint.x = pOutputFeat.Value(CentroidXFieldIndex)
//        pPoint.y = pOutputFeat.Value(CentroidYFieldIndex)
//        pConstructCArc.ConstructCircle pPoint, Radius, True
//        'Add the circle segment to a polygon geometry
//        Set pSegmentColl = New Polygon
//        pSegmentColl.AddSegment pConstructCArc
//        Set pSFilter.Geometry = pSegmentColl
//        pSFilter.SpatialRel = esriSpatialRelContains
//        Set pDataFeatCur = DataFeatClass.Search(pSFilter, True)

//        s = 0: C = 0: m = 0
//        Set pDataFeat = pDataFeatCur.NextFeature
//        Do Until pDataFeat Is Nothing
//            s = s + pDataFeat.Value(ZFieldIndex)
//            C = C + 1
//            Set pDataFeat = pDataFeatCur.NextFeature
//            DoEvents
//        Loop
//        Set pPoint = Nothing
//        Set pSegmentColl = Nothing
//        Set pSFilter.Geometry = Nothing
//        Set pDataFeatCur = Nothing
//        If C = 0 Then
//            m = 0
//        Else
//            m = s / C
//        End If

//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Sum")) = s
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Count")) = C
//        pOutputFeat.Value(pOutputFeat.Fields.FindField("Mean")) = m
//        pOutputFeatCur.UpdateFeature pOutputFeat

//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing


//    ' add a spatial index to the new shapefile (very important for large shapefiles).
//    If AddSpatialIndex = True Then
//      If Not CreateSpatialIndex(pOutputFeatClass) Then GoTo CantMakeSpatialIndex
//    End If

//    On Error GoTo IO_Finished

//  ' display the new shapefile
//    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
//        MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//                vbInformation, "Finished"
//    Else ' add the new shapefile to a lyer if in ArcMap.
//        If AddAsLayer Then
//            Dim pMxDoc As IMxDocument
//            Set pMxDoc = ThisDocument
//            Dim pFeatLayer As IFeatureLayer
//            Set pFeatLayer = New FeatureLayer
//            Set pFeatLayer.FeatureClass = pOutputFeatClass
//            pFeatLayer.Name = OutputFileName
//            pMxDoc.AddLayer pFeatLayer
//            pMxDoc.UpdateContents
//            Dim pEnv As IEnvelope
//            Set pEnv = pFeatLayer.AreaOfInterest
//            pEnv.Expand 1.1, 1.1, True
//            pMxDoc.ActiveView.Extent = pEnv
//            pMxDoc.ActiveView.Refresh
//        End If
//    End If

//    Exit Function

//EH:
//  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
//  Exit Function

//CantMakeSpatialIndex:
//  MsgBox "Could not create the spatial index for the output shapefile." & _
//         Chr(13) & "Try creating it in ArcCatalog if you need it.", _
//          vbExclamation, "No Spatial Index"
//  Resume Next

//IO_Finished:
//  MsgBox "The File " & OutputFilePath & "\" & OutputFileName & " was sucessfully created.", _
//          vbExclamation, "Can not display new shapefile"
//  Exit Function

//End Function








//Public Function OpenTable(Path As String, Name As String) As ITable
//    Dim pFWS As IFeatureWorkspace
//    Dim pWorkspaceFactory As IWorkspaceFactory
//    Set pWorkspaceFactory = New ShapefileWorkspaceFactory
//    Set pFWS = pWorkspaceFactory.OpenFromFile(Path, Application.hWnd)
//    Set OpenTable = pFWS.OpenTable(Name)
//End Function




//Public Function EditSpatialReference( _
//CurrSpatialReference As ISpatialReference) As ISpatialReference

//    Dim pSpatialReferenceDialog As ISpatialReferenceDialog
//    Set pSpatialReferenceDialog = New SpatialReferenceDialog
//    Dim pNewSpatialReference As ISpatialReference
//    Set pNewSpatialReference = pSpatialReferenceDialog.DoModalEdit( _
//    CurrSpatialReference, False, False, False, False, False, Application.hWnd)
//    If pNewSpatialReference Is Nothing Then
//      Set EditSpatialReference = New UnknownCoordinateSystem
//    Else
//      Set EditSpatialReference = pNewSpatialReference
//    End If

//End Function




//Public Function GetReadOnlyCursor(pTable As ITable, _
//WhereClause As String, Optional RowCount As Variant) As icursor
//    Dim pQFilt As IQueryFilter
//    Set pQFilt = New QueryFilter
//    pQFilt.WhereClause = WhereClause
//    If Not IsMissing(RowCount) Then
//        RowCount = pTable.RowCount(pQFilt)
//    End If
//    Set GetReadOnlyCursor = pTable.Search(pQFilt, False)
//End Function





//Public Sub CreateMyPlacesGrids()
//    Dim MyPlaces As IEnumPlace
//    Set MyPlaces = New MyPlaceCollection
//    Dim MyPlace As IPlace
//    Dim Rows As Long
//    Dim Cols As Long
//    Dim cnt As Long
//    Dim CellSize As Double
//    Dim pSpatialRef As ISpatialReference
//    Dim m_pMxDoc As IMxDocument
//    Dim FishnetFeatClass As IFeatureClass


//    Set m_pMxDoc = ThisDocument
//    Set pSpatialRef = m_pMxDoc.FocusMap.SpatialReference
//    Set MyPlaces = New MyPlaceCollection
//    CellSize = 30
//    cnt = 0
//    MyPlaces.Reset
//    Set MyPlace = MyPlaces.Next
//    Do While Not MyPlace Is Nothing
//        With MyPlace
//            Rows = Round(((.Geometry.Envelope.YMax - _
//            .Geometry.Envelope.YMin) / CellSize) + 0.5)
//            Cols = Round(((.Geometry.Envelope.XMax - _
//            .Geometry.Envelope.XMin) / CellSize) + 0.5)


//            CustomToolsCreateFishnet "C:\Users\on.GEOKG\Desktop\New folder\ArcGIS Files", _
//            .Name & "_30_îèø", .Geometry.Envelope.XMin, _
//            .Geometry.Envelope.YMin, Rows, Cols, CellSize, _
//            CellSize, PolygonsFishnet, _
//            pSpatialRef, True, False

//            Set FishnetFeatClass = OpenShapefile("C:\ArcGIS Files\TestCode\Grids", .Name)
//            CustomToolsAddField FishnetFeatClass, "CentroidX", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "CentroidX", GeometryClaculationTypeCentroidX
//            CustomToolsAddField FishnetFeatClass, "CentroidY", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "CentroidY", GeometryClaculationTypeCentroidY
//            CustomToolsAddField FishnetFeatClass, "MinX", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "MinX", GeometryClaculationTypeMinX
//            CustomToolsAddField FishnetFeatClass, "MaxX", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "MaxX", GeometryClaculationTypeMaxX
//            CustomToolsAddField FishnetFeatClass, "MinY", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "MinY", GeometryClaculationTypeMinY
//            CustomToolsAddField FishnetFeatClass, "MaxY", esriFieldTypeDouble
//            CustomToolsCalcGeometry FishnetFeatClass, "MaxY", GeometryClaculationTypeMaxY
//        End With
//        cnt = cnt + 1
//        Set MyPlace = MyPlaces.Next
//    Loop

//End Sub





//Public Sub UpdateProgressStatus(ProgressVal As Long, _
//Optional ProgressMsg As String = "")
//    With objProgressForm
//        .pbProccessProgress.Value = ProgressVal
//        If Trim(ProgressMsg) <> "" Then
//            .txtProccessStatus = _
//            .txtProccessStatus & ProgressMsg & vbCrLf
//        End If
//        .lblProgressPercent = _
//        Round(ProgressVal / ProgressMaxVal * 100) & " %"
//    End With
//    DoEvents
//End Sub

//Public Function FetchUserInterrupts() As enumUserInterruptType
//    Do While UserInterruptType = UserInterruptTypePause
//        DoEvents
//    Loop
//    FetchUserInterrupts = UserInterruptType
//End Function



//Sub CreatePoygonFishnet()

//Dim FishnetMinX As Double
//Dim FishnetMinY As Double
//Dim FishnetMaxX As Double
//Dim FishnetMaxY As Double
//Dim FishnetCellSize As Double
//Dim pSpatialRef As ISpatialReference
//Dim pMxDoc As IMxDocument
//Dim FishnetFilePath As String
//Dim FishnetFileName As String
//Dim x As Double
//Dim y As Double
//Dim Rows As Long
//Dim Cols As Long
//Dim FishnetFeatClass As IFeatureClass


// '6200
// FishnetMinX = 174667.989
// FishnetMinY = 655162.905
// FishnetMaxX = 178063.89
// FishnetMaxY = 660437.556

// '8700
//' FishnetMinX = 185107.697
//' FishnetMinY = 675059.471
//' FishnetMaxX = 190049.1
//' FishnetMaxY = 679462.034


// FishnetCellSize = 300

// FishnetFilePath = "C:\Users\on.GEOKG\Desktop\îôòì äôéñ"
// FishnetFileName = "6200_grid"


//    'drop existing grid
//    If Dir(FishnetFilePath & "\" & FishnetFileName & ".shp") <> "" Then
//        Set FishnetFeatClass = OpenShapefile(FishnetFilePath, FishnetFileName)
//        DeleteShapefile FishnetFeatClass
//        Set FishnetFeatClass = Nothing
//    End If


//    Rows = Round(((FishnetMaxY - FishnetMinY) / FishnetCellSize) + 0.5)
//    Cols = Round(((FishnetMaxX - FishnetMinX) / FishnetCellSize) + 0.5)
//    x = FishnetMinX
//    y = FishnetMinY


//    Set pMxDoc = ThisDocument
//    Set pSpatialRef = pMxDoc.FocusMap.SpatialReference


//    'create and prepare grid
//    CustomToolsCreateFishnet FishnetFilePath, FishnetFileName, _
//    x, y, Rows, Cols, FishnetCellSize, FishnetCellSize, _
//    PolygonsFishnet, pSpatialRef, True, True

//    MsgBox "done"

//End Sub

//Sub SDWTest()
// SDW "C:\Users\on.GEOKG\Desktop\îåãì ôåèðöéàì ðâéùåú\1211\ArcGIS Files", "7100_data_migdal", _
//    "C:\Users\on.GEOKG\Desktop\îåãì ôåèðöéàì ðâéùåú\1211\ArcGIS Files", "7100_fishnet_migdal_join", _
//    30, 156192.117, 163555.681, 614269.862, 624119.825, WeightNumerator, WeightInFishnet, "Sum_Weigt"
//End Sub


//Public Sub SDW(DataFilePath As String, DataFileName As String, _
//FishnetFilePath As String, FishnetFileName As String, FishnetCellSize As Double, _
//FishnetMinX As Double, FishnetMaxX As Double, FishnetMinY As Double, FishnetMaxY As Double, _
//WNT As enumWeightFuncion, WC As enumWeightContainer, WeightFieldName As String)

//Dim pSpatialRef As ISpatialReference
//Dim Rows As Long
//Dim Cols As Long
//Dim x As Double
//Dim y As Double
//Dim FishnetFeatClass As IFeatureClass
//Dim GridCount As Long
//Dim DataCount As Long
//Dim pMxDoc As IMxDocument


//    Rows = Round(((FishnetMaxY - FishnetMinY) / FishnetCellSize) + 0.5)
//    Cols = Round(((FishnetMaxX - FishnetMinX) / FishnetCellSize) + 0.5)
//    x = FishnetMinX
//    y = FishnetMinY


//    Set pMxDoc = ThisDocument
//    Set pSpatialRef = pMxDoc.FocusMap.SpatialReference


//    'drop existing grid
//    If Dir(FishnetFilePath & "\" & FishnetFileName & ".shp") <> "" Then
//        Set FishnetFeatClass = OpenShapefile(FishnetFilePath, FishnetFileName)
//        DeleteShapefile FishnetFeatClass
//        Set FishnetFeatClass = Nothing
//    End If



//    'prepare proggess bar
//    GridCount = Rows * Cols
//    Dim DataFeatClass As IFeatureClass
//    Set DataFeatClass = OpenShapefile( _
//    DataFilePath, DataFileName)
//    DataCount = DataFeatClass.FeatureCount(Nothing)
//    Set DataFeatClass = Nothing

//'    Set objProgressForm = New frmProccessProgressControl
//'    objProgressForm.pbProccessProgress.Min = 0
//    ProgressMaxVal = (GridCount * 4) + (DataCount * 2)
//'    objProgressForm.pbProccessProgress.Max = ProgressMaxVal
//'    UserInterruptType = UserInterruptTypeNone
//'    objProgressForm.Show
//    ProgressVal = 0




//    'create and prepare grid
//    'UpdateProgressStatus ProgressVal, "Creating grid ..."
//    CustomToolsCreateFishnet FishnetFilePath, FishnetFileName, _
//    x, y, Rows, Cols, FishnetCellSize, FishnetCellSize, _
//    PolygonsFishnet, pSpatialRef, True, False
//    'If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
//    'UpdateProgressStatus ProgressVal, "done."

//    Set FishnetFeatClass = OpenShapefile( _
//    FishnetFilePath, FishnetFileName)
//    CustomToolsAddField FishnetFeatClass, "CentroidX", esriFieldTypeDouble
//    'UpdateProgressStatus ProgressVal, "Calculating cells centroidX ..."
//    CustomToolsCalcGeometry FishnetFeatClass, "CentroidX", GeometryClaculationTypeCentroidX
//    'If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
//    'UpdateProgressStatus ProgressVal, "done."
//    CustomToolsAddField FishnetFeatClass, "CentroidY", esriFieldTypeDouble
//    'UpdateProgressStatus ProgressVal, "Calculating cells centroidY ..."
//    CustomToolsCalcGeometry FishnetFeatClass, "CentroidY", GeometryClaculationTypeCentroidY
//    'If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
//    'UpdateProgressStatus ProgressVal, "done."
//    FishnetFeatClass.AddField CreateField( _
//    "SDW", esriFieldTypeDouble)

//    Dim CentroidXFieldIndex As Long
//    Dim CentroidYFieldIndex As Long
//    Dim SDWFieldIndex As Long
//    CentroidXFieldIndex = FishnetFeatClass.Fields.FindField("CentroidX")
//    CentroidYFieldIndex = FishnetFeatClass.Fields.FindField("CentroidY")
//    SDWFieldIndex = FishnetFeatClass.Fields.FindField("SDW")





//    'prepare data
//    Dim XFieldIndex As Long
//    Dim YFieldIndex As Long
//    Dim ZFieldIndex As Long
//'    Dim DataFeatClass As IFeatureClass
//    Set DataFeatClass = OpenShapefile( _
//    DataFilePath, DataFileName)


//    'add data xy
//    If DataFeatClass.FindField("PointX") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointX"
//    End If
//    CustomToolsAddField DataFeatClass, "PointX", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointX", GeometryClaculationTypePointX

//    If DataFeatClass.FindField("PointY") > -1 Then
//        CustomToolsRemoveField DataFeatClass, "PointY"
//    End If
//    CustomToolsAddField DataFeatClass, "PointY", esriFieldTypeDouble
//    CustomToolsCalcGeometry DataFeatClass, "PointY", GeometryClaculationTypePointY


//    XFieldIndex = DataFeatClass.Fields.FindField("PointX")
//    YFieldIndex = DataFeatClass.Fields.FindField("PointY")
//    Select Case WC
//        Case enumWeightContainer.WeightInData
//            ZFieldIndex = DataFeatClass.Fields.FindField(WeightFieldName)
//        Case enumWeightContainer.WeightInFishnet
//            ZFieldIndex = FishnetFeatClass.Fields.FindField(WeightFieldName)
//    End Select


//    Dim pDataTable As ITable
//    Set pDataTable = DataFeatClass
//    Dim pDataCur As icursor
//    Dim pDataRow As IRow
//    Dim RowCount As Variant
//    Set pDataCur = GetReadOnlyCursor(pDataTable, "", RowCount)



//    'applying data to array
//    Dim InputArr() As Variant
//    ReDim InputArr(1 To RowCount, 1 To 3) As Variant
//    Dim InputIndex As Long

//    InputIndex = 1
//    Set pDataRow = pDataCur.NextRow
//    Do Until pDataRow Is Nothing
//        'If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
//        'applying input array values
//        InputArr(InputIndex, 1) = pDataRow.Value(XFieldIndex)
//        InputArr(InputIndex, 2) = pDataRow.Value(YFieldIndex)
//        If WC = enumWeightContainer.WeightInData Then
//            InputArr(InputIndex, 3) = pDataRow.Value(ZFieldIndex)
//        End If
//        Set pDataRow = pDataCur.NextRow
//        InputIndex = InputIndex + 1
//        DoEvents
//    Loop
//    Set DataFeatClass = Nothing
//    Set pDataCur = Nothing





//    'calculating SDW
//    Dim SDW As Double
//    Dim W As Variant
//    Dim Dist As Double
//    Dim pOutputFeatCur As IFeatureCursor
//    Dim pOutputFeat As IFeature
//    Dim lngCurrentFeatureCount As Long
//    Dim I As Long


//    lngCurrentFeatureCount = 0
//    'UpdateProgressStatus ProgressVal, "Calculating..."
//    Set pOutputFeatCur = GetUpdatableFeatureCursor(FishnetFeatClass, "1=1")

//    Set pOutputFeat = pOutputFeatCur.NextFeature
//    Do Until pOutputFeat Is Nothing
//        SDW = 0
//        ProgressVal = ProgressVal + 1
//        'UpdateProgressStatus ProgressVal
//        'If FetchUserInterrupts = UserInterruptTypeStop Then Exit Sub
//        For I = 1 To RowCount
//            Dist = Sqr((pOutputFeat.Value(CentroidXFieldIndex) - _
//            InputArr(I, 1)) ^ 2 + _
//            (pOutputFeat.Value(CentroidYFieldIndex) - _
//            InputArr(I, 2)) ^ 2)

//            Select Case WC
//                Case enumWeightContainer.WeightInData
//                    W = InputArr(I, 3)
//                Case enumWeightContainer.WeightInFishnet
//                    W = pOutputFeat.Value(ZFieldIndex)
//            End Select

//            Select Case WNT
//                Case enumWeightFuncion.WeightMultiplier
//                    SDW = SDW + (Dist * W)
//                Case enumWeightFuncion.WeightNumerator
//                    SDW = SDW + (W / Dist)
//            End Select
//        Next I
//        pOutputFeat.Value(SDWFieldIndex) = SDW
//        pOutputFeatCur.UpdateFeature pOutputFeat
//        Set pOutputFeat = pOutputFeatCur.NextFeature
//        lngCurrentFeatureCount = lngCurrentFeatureCount + 1
//        DoEvents
//    Loop
//    Set pOutputFeatCur = Nothing
//    Set FishnetFeatClass = Nothing

//    MsgBox "done"

//End Sub


