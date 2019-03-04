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
using ESRI.ArcGIS.DataSourcesRaster;
using System.Windows.Forms;
using StringFuncs;


 


namespace EmployeeTracker.HoodAux
{
    public class HoodAnalystBase : ViewModelBase
    {

        #region  public Data

        public const int cnstClusterUniqueValue = 1;
        public const int cnstClusterValueRange = 2;
        
        public long ProgressMaxVal;
        public long ProgressVal;
        //public frmProccessProgressControl objProgressForm;
        public enumUserInterruptType UserInterruptType;

        #endregion

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

            //if (!(pGxDialog.Name.IndexOf(".") == 0))
            //{
            //    genericName = pGxDialog.Name;
            //}
            //else
            //{
            //    genericName = pGxDialog.Name.Substring(0, pGxDialog.Name.IndexOf("."));
            //}

            if (pGxDialog.Name.Substring(1,1)==".")      
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
            return pFWS .OpenFeatureClass(Name);
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

        protected bool BuildClusterValuesStruct(ref AnalysisParams analysis, string txtClusterValues)
        {
            int i = 0;
            string subStr = ExtractSubString(txtClusterValues, ";", i + 1);

            do
            {
                //if (analysis.CP.Length == 0)
                //{  //            ReDim Analysis.CP(1 To I) As ClusterParam
                //    // System.Array.Resize<ClusterParam>(ref Analysis[0].CP, 1);
                //    System.Array.Resize<ClusterParam>(ref analysis.CP, i);
                //}
                //else
                //{ //            ReDim Preserve Analysis.CP(1 To I) As ClusterParam
                //    StringDC.ResizeArray(analysis.CP, i);
                //}

                //StringDC.ResizeArray(analysis.CP, i+2);


                //analysis = new AnalysisParams();
                //analysis.CP = null;

                System.Array.Resize<ClusterParam>(ref analysis.CP, i+1);

                if (subStr.Length>1 && subStr.Substring(1,1) == "-")
                {
                    analysis.CP[i].Type = cnstClusterValueRange;
                    analysis.CP[i].From = ExtractSubString(subStr, "-", 1);
                    analysis.CP[i].To = (ExtractSubString(subStr, "-", 2));
                }
                else
                {
                    analysis.CP[i].Type = cnstClusterUniqueValue;
                    analysis.CP[i].Equale = subStr;
                }
                i++;
                subStr = ExtractSubString(txtClusterValues, ";", i + 1);
            }
            while (subStr != "");               
       

            return true;
        }

        protected bool CustomToolsCreateFishnet(string path,string name,double x,double y,long rows,long cols,double width,double height,
            enumFishnetType fishNetType,ISpatialReference spatialReference,bool addSpatialIndex ,bool addAsLayer)
        {
            //On Error GoTo EH

            //' store the input.
            double dblX=x, dblY=y, dblHeight=height, dblWidth=width;
            long lngCols=cols, lngRows=rows, lngCurrentFeatureCount=0;
            bool blnSpatialIndex, blnGeomIsPoly;


            if (fishNetType == enumFishnetType.PolygonsFishnet)
            {
                blnGeomIsPoly = true;
            }
            else
            {
                blnGeomIsPoly = false;
            }
           

            //    ' create some fields.
            IFields pFields;
            IFieldsEdit pFieldsEdit;
            pFields = new Fields();
            pFieldsEdit =(IFieldsEdit) pFields;          

            pFieldsEdit.AddField(CreateField("Shape",esriFieldType.esriFieldTypeGeometry,null,esriGeometryType.esriGeometryPolygon,spatialReference));

            if (blnGeomIsPoly)
            {
                pFieldsEdit.AddField(CreateField("Col_Row", esriFieldType.esriFieldTypeString, 20, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("Col", esriFieldType.esriFieldTypeInteger, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("Row", esriFieldType.esriFieldTypeInteger, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("MinX", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("MinY", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("MaxX", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("MaxY", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("CentroidX", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                pFieldsEdit.AddField(CreateField("CentroidY", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
            }


            //    ' create the shapefile
            IFeatureClass pOutFeatClass = CreateShapefile(path, name , pFields);//==============================

            //    ' the output shapefile can be either polygon or polyline.
            IPointCollection pPointColl;
            IPoint pPnt1, pPnt2, pPnt3, pPnt4;
            pPnt1 = new Point();
            pPnt2 = new Point();
            pPnt3 = new Point();
            pPnt4 = new Point();
            IClone pClone;
            IFeatureCursor pInsertFeatureCursor;
            IFeatureBuffer pInsertFeatureBuffer;           
            double dblDeltaX=0.0, dblDeltaY=0.0;
            ITransform2D pTrans2D;
            IFeature tmpFeat;

            if (blnGeomIsPoly)      // ' if output shapefile is polygon.
            {   // ' create the first polygon . it will be copied many times.
                pPointColl = new Polygon();
                pPnt1.X  = dblX - dblWidth;
                pPnt1.Y = dblY - dblHeight;
                pPnt2.X = dblX - dblWidth;
                pPnt2.Y = dblY;
                pPnt3.X  = dblX;
                pPnt3.Y = dblY;
                pPnt4.X  = dblX;
                pPnt4.Y = dblY - dblHeight;
                pPointColl.AddPoint( pPnt1);
                pPointColl.AddPoint (pPnt2);
                pPointColl.AddPoint (pPnt3);
                pPointColl.AddPoint (pPnt4);
                IPolygon  pFirstPolygon ;
                pFirstPolygon =(IPolygon) pPointColl;
                pFirstPolygon.Close();
                pClone =(IClone) pFirstPolygon;


            //        ' write the polygons out to the shapefile.
            //        On Error GoTo AfterFeatCursorOpened

                pInsertFeatureCursor = pOutFeatClass.Insert(true);
                pInsertFeatureBuffer = pOutFeatClass.CreateFeatureBuffer();
                int  lngIndexPos ;
                lngIndexPos = pOutFeatClass.FindField("Col_Row");
                int lngColIndexPos = pOutFeatClass.FindField("Col");
                int lngRowIndexPos = pOutFeatClass.FindField("Row");
                int lngMinXIndexPos = pOutFeatClass.FindField("MinX");
                int lngMinYIndexPos = pOutFeatClass.FindField("MinY");
                int lngMaxXIndexPos = pOutFeatClass.FindField("MaxX");
                int lngMaxYIndexPos = pOutFeatClass.FindField("MaxY");
                int lngCentroidXIndexPos = pOutFeatClass.FindField("CentroidX");
                int lngCentroidYIndexPos = pOutFeatClass.FindField("CentroidY");
                
                IPolygon pPolygon;
                for(int i=1 ; i <= lngCols ; i++)
                {
                    dblDeltaX = (i * dblWidth);
                    for(int j=1 ; j <= lngRows ; j++)
                    {
                        dblDeltaY = (j * dblHeight);
                        pPolygon =(IPolygon) pClone.Clone();
                        pTrans2D =(ITransform2D) pPolygon; 
                        pTrans2D.Move (dblDeltaX, dblDeltaY);
                        pInsertFeatureBuffer.Shape = pPolygon;
                        pInsertFeatureBuffer.set_Value(lngIndexPos, i.ToString("0000") + ":" + j.ToString("0000"));
                        pInsertFeatureBuffer.set_Value(lngColIndexPos,i);
                        pInsertFeatureBuffer.set_Value(lngRowIndexPos,j);
                        pInsertFeatureBuffer.set_Value(lngMinXIndexPos,pInsertFeatureBuffer.Shape.Envelope.XMin);
                        pInsertFeatureBuffer.set_Value(lngMinYIndexPos,pInsertFeatureBuffer.Shape.Envelope.YMin);
                        pInsertFeatureBuffer.set_Value(lngMaxXIndexPos, pInsertFeatureBuffer.Shape.Envelope.XMax);
                        pInsertFeatureBuffer.set_Value(lngMaxYIndexPos, pInsertFeatureBuffer.Shape.Envelope.XMax);

                         tmpFeat = pInsertFeatureBuffer as IFeature ;
                        pInsertFeatureBuffer.set_Value(lngCentroidXIndexPos, GetFeatureCentroid(tmpFeat).X );
                        pInsertFeatureBuffer.set_Value(lngCentroidYIndexPos,GetFeatureCentroid(tmpFeat).Y);
                         tmpFeat = null;

                        pInsertFeatureCursor.InsertFeature (pInsertFeatureBuffer);
                        lngCurrentFeatureCount = lngCurrentFeatureCount + 1;
                       

                       // ProgressVal = ProgressVal + 1;
                        //UpdateProgressStatus ProgressVal;
                        //if(FetchUserInterrupts == UserInterruptTypeStop )
                        //{
                        //    return false;
                        //}
                       // DoEvents
                    }
                }
                pInsertFeatureCursor.Flush();
            }
            else  //' if output is polyline.
            {           
            //        ' create one vertical and one horizontal polyline. they will be copied many times.
                IPolyline pHorzPolyline, pVertPolyline;

                pPointColl = new Polyline();
                pPnt1.X = dblX;
                pPnt1.Y = dblY;
                pPnt2.X = pPnt1.X + (lngCols * dblWidth);
                pPnt2.Y = pPnt1.Y;
                pPointColl.AddPoint(pPnt1);
                pPointColl.AddPoint(pPnt2);
                pHorzPolyline =(IPolyline) pPointColl;
                pPointColl = new Polyline();
                pPnt3.X = pPnt1.X;
                pPnt3.Y= pPnt1.Y;
                pPnt4.X = pPnt3.X;
                pPnt4.Y = pPnt3.Y + (lngRows * dblHeight);
                pPointColl.AddPoint (pPnt3);
                pPointColl.AddPoint( pPnt4);
                pVertPolyline =(IPolyline)pPointColl;
                //      ' write the polylines out to the shapefile.
            //      On Error GoTo AfterFeatCursorOpened
                pInsertFeatureCursor = pOutFeatClass.Insert(true);
                pInsertFeatureBuffer = pOutFeatClass.CreateFeatureBuffer();
                IPolyline  pPolyline;
                pClone =(IClone) pVertPolyline;
                for (int icol = 0; icol <= lngCols; icol++)
                {
                    dblDeltaX = (icol * dblWidth);
                    pPolyline =(IPolyline) pClone.Clone();
                    pTrans2D = (ITransform2D)pPolyline;
                    pTrans2D.Move( dblDeltaX, 0);
                    pInsertFeatureBuffer.Shape = pPolyline;
                    pInsertFeatureCursor.InsertFeature( pInsertFeatureBuffer);
                    lngCurrentFeatureCount ++;
            //          DoEvents

                }
                pClone =(IClone) pHorzPolyline;
                   for(int irow = 0; irow <= lngRows ; irow++)
                {
                      dblDeltaY = (irow * dblHeight);
                       pPolyline =(IPolyline) pClone.Clone();
                       pTrans2D =(ITransform2D) pPolyline;
                      pTrans2D.Move( 0, dblDeltaY);
                       pInsertFeatureBuffer.Shape = pPolyline;
                      pInsertFeatureCursor.InsertFeature(pInsertFeatureBuffer);
                      lngCurrentFeatureCount = lngCurrentFeatureCount + 1;
                     // DoEvents
                }
            }
            
            pInsertFeatureBuffer = null;
            pInsertFeatureCursor = null;


            //    ' add a spatial index to the new shapefile (very important for large shapefiles).
            //    If AddSpatialIndex = True Then
            if (addSpatialIndex) if (!CreateSpatialIndex(pOutFeatClass))
                {
                    MessageBox.Show("Could not create the spatial index for the output shapefile.","Try creating it in ArcCatalog if you need it.");
                }



            //    On Error GoTo IO_Finished

            //  ' display the new shapefile
            //    If Not TypeOf Application Is IMxApplication Then ' if in ArcCatalog etc.
            //        MsgBox "The fishnet " & Name & " was sucessfully created.", _
            //                vbInformation, "Finished"
            //    Else ' add the new shapefile to a lyer if in ArcMap.
            if (addAsLayer)
            {
                //            Dim pMxDoc As IMxDocument
                //            Set pMxDoc = ThisDocument
                //            Dim pFeatLayer As IFeatureLayer
                //            Set pFeatLayer = New FeatureLayer
                //            Set pFeatLayer.FeatureClass = pOutFeatClass
                //            pFeatLayer.Name = Name
                //            pMxDoc.AddLayer pFeatLayer
                //            pMxDoc.UpdateContents
                //            Dim pEnv As IEnvelope
                //            Set pEnv = pFeatLayer.AreaOfInterest
                //            pEnv.Expand 1.1, 1.1, True
                //            pMxDoc.ActiveView.Extent = pEnv
                //            pMxDoc.ActiveView.Refresh
            }
            //    End If

            //  Exit Function

            //EH:
            //  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
            //  Exit Function


            //AfterFeatCursorOpened:
            //  MsgBox Err.Number & Chr(13) & Err.Description, vbCritical, "Can not proceed"
            //  Set pInsertFeatureBuffer = Nothing
            //  Set pInsertFeatureCursor = Nothing
            //  If DeleteShapefile(pOutFeatClass) = False Then
            //    MsgBox "Could not delete " & m_pGxFile.Path & "\" & m_strShpFileName & _
            //           Chr(13) & "Try deleting it with ArcCatalog." _
            //           , vbExclamation, "Couldn't clean up properly"
            //  End If
            //  Exit Function


           


            //IO_Finished:
            //  MsgBox "The fishnet " & Path & "\" & Name & " was sucessfully created.", _
            //          vbExclamation, "Can not display new shapefile"
            //  Exit Function

            //End Function
            return true;
        }
        
        
        protected IField CreateField(string fieldName,esriFieldType fieldType,object  fieldLength,esriGeometryType geometryType,ISpatialReference spatialReference)
        {
            //Public Function CreateField(FieldName As String, FieldType As esriFieldType, _
            //Optional FieldLength As Variant, Optional GeometryType As esriGeometryType, _
            //Optional SpatialReference As ISpatialReference) As IField

            IField pField=new Field();
            IFieldEdit pFieldEdit =(IFieldEdit) pField;
            //Set pField = New Field
            //Set pFieldEdit = pField

            pFieldEdit.Name_2 = fieldName; //** pFieldEdit.Name**
            pFieldEdit.Type_2 = fieldType; //**pFieldEdit.Type**
            pFieldEdit.IsNullable_2 = false;//**pFieldEdit.IsNullable**
            pFieldEdit.DefaultValue_2 = -1;//**pFieldEdit.DefaultValue**

            if (fieldLength != null)
            {
                pFieldEdit.Length_2 =(int) fieldLength;
            }

            if (fieldType == esriFieldType.esriFieldTypeGeometry)
            {

                if (geometryType != esriGeometryType.esriGeometryNull)
                {
                    IGeometryDef pGeomDef =new GeometryDef();
                    IGeometryDefEdit pGeomDefEdit =(IGeometryDefEdit) pGeomDef;
                    pGeomDefEdit.GeometryType_2 = geometryType;
                    if(spatialReference != null)
                    {
                        pGeomDefEdit.SpatialReference_2 = spatialReference;
                    }

                    pFieldEdit.GeometryDef_2 = pGeomDef;
                }

            }
            return pFieldEdit;
        }
       
        public string ExtractSubString(string s,string separator,long subStrinPos)
        {
            return ExtractSubStringEx(s, separator, 1, subStrinPos);       
        }

        public string ExtractSubStringEx(string s, string Seperator, long CurrPosition, long SubStringPosition)
        {
            string functionReturnValue = null;
            string ch = null;
          
            string tmpSubString = null;

            functionReturnValue = "";
            s = s.Trim();

            for (int i = 0; i < s.Length ; i++)
            {
                ch = s.Substring(i, 1);      // Strings.Mid(s, I, 1);
                if (ch != Seperator)
                {
                    tmpSubString = tmpSubString + ch;
                }
                else
                {
                    if (CurrPosition == SubStringPosition)
                    {
                        functionReturnValue = tmpSubString;
                    }
                    else
                    {
                        functionReturnValue = ExtractSubStringEx(s.Substring(i+1), Seperator, CurrPosition + 1, SubStringPosition);//Strings.Mid(s, i + 1)
                    }
                    return functionReturnValue;
                }
            }

            if (CurrPosition == SubStringPosition)
            {
                functionReturnValue = tmpSubString;
            }
            return functionReturnValue;

        }

        protected IFeatureClass CreateShapefile(string path, string name, IFields fields)
        {
            IFeatureWorkspace pFWS;
            IWorkspaceFactory pWorkspaceFactory;
           // IWorkspace pWorkspaceFactory;
            pWorkspaceFactory = new ShapefileWorkspaceFactory();
           // pWorkspaceFactory = new RasterWorkspaceFactory();


            pFWS = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(path, 0);

            return pFWS.CreateFeatureClass(name, fields, null, null,esriFeatureType.esriFTSimple , "Shape", "");
        }

        protected bool   CreateSpatialIndex(IFeatureClass FeatClass) 
        {           
            IFields pFields= new Fields();
            IFieldsEdit pFieldsEdit;          
            pFieldsEdit =(IFieldsEdit) pFields;
            pFieldsEdit.FieldCount_2 = 1;

            int h = FeatClass.FindField("Shape");

            IField pField= FeatClass.Fields.get_Field(h);

          // pFieldsEdit.get_Field(0) = pField;
            //    Set pFieldsEdit.Field(0) = pField
            pFieldsEdit.set_Field(0,pField);
          
           IIndex pIndex;
           IIndexEdit pIndexEdit;
           pIndex = new Index();
           pIndexEdit =(IIndexEdit) pIndex;
            pIndexEdit.Fields_2 = pFields;
            pIndexEdit.Name_2 = "SI";

            FeatClass.AddIndex(pIndex);
            return true;
        }

        protected IPoint GetFeatureCentroid(IFeature feature)
        {
            IPointCollection m_Points;
            m_Points = (IPointCollection)feature.Shape;
            
            double second_factor, polygon_area,x=0,y=0;
            long m_NumPoints = m_Points.PointCount;           

            //'Add the first point at the end of the array.
            //'    ReDim Preserve m_Points(1 To m_NumPoints + 1)
            //'    m_Points(m_NumPoints + 1) = m_Points(1)

            //    'Find the centroid.
            //    Select Case Feature.Shape.GeometryType
            
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                MessageBox.Show("Feature geometry does not support this type of calculation", "Cannot proceed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

                x = 0;
                y = 0;
            //    For pt = 0 To m_NumPoints - 1
            for (int pt = 0;pt<=m_NumPoints-1;pt++)
            {            
                if(pt==m_NumPoints-1)
                {
                    second_factor =m_Points.get_Point(pt).X  * m_Points.get_Point(0).Y -   m_Points.get_Point(0).X * m_Points.get_Point(pt).Y;
                    x = x + (m_Points.get_Point(pt).X + m_Points.get_Point(0).X) *  second_factor;
                    y = y + (m_Points.get_Point(pt).Y + m_Points.get_Point(0).Y) *  second_factor;
                }
                else
                {
                    second_factor =   m_Points.get_Point(pt).X * m_Points.get_Point(pt + 1).Y - m_Points.get_Point(pt + 1).X * m_Points.get_Point(pt).Y;
                        x = x + (m_Points.get_Point(pt).X + m_Points.get_Point(pt + 1).X) *   second_factor;
                        y = y + (m_Points.get_Point(pt).Y + m_Points.get_Point(pt + 1).Y) * second_factor  ;          
                }
            }

            //    ' Divide by 6 times the polygon's area.
                polygon_area = PolygonArea(feature);
                x = x / 6 / polygon_area;
                y = y / 6 / polygon_area;

            //    ' If the values are negative, the polygon is
            //    ' oriented counterclockwise. Reverse the signs.
                if( x < 0 )
                {
                    x = -x;
                    y = -y;
                }

            IPoint pPoint;
            pPoint = new Point();
            pPoint.X = x;
            pPoint.Y = y;
            return pPoint;
            
       
        }
        
        protected Single  PolygonArea(IFeature feature) 
           {
            //    ' Return the absolute value of the signed area.
            return (Single) Math.Abs(SignedPolygonArea(feature));
           }
        
        protected double SignedPolygonArea(IFeature feature) 
        {            
            double area=0;
            IPointCollection m_Points;

            m_Points = (IPointCollection)feature.Shape;
            long m_NumPoints;
            m_NumPoints = m_Points.PointCount;


            for (int pt = 0; pt <= m_NumPoints - 1; pt++)
            {
                if (pt == m_NumPoints - 1)
                {
                    area = area + (m_Points.get_Point(0).X - m_Points.get_Point(pt).X) * (m_Points.get_Point(0).Y + m_Points.get_Point(pt).Y) / 2;
                }
                else
                {
                    area = area + (m_Points.get_Point(pt + 1).X - m_Points.get_Point(pt).X) * (m_Points.get_Point(pt + 1).Y + m_Points.get_Point(pt).Y) / 2;
                }

            }
           return  area;
    }

        protected bool CustomToolsRemoveField(IFeatureClass featureClass, string fieldName)
        {            
              featureClass.DeleteField (featureClass.Fields.get_Field(featureClass.FindField(fieldName)));
            return true;
        }

        protected bool CustomToolsAddField(IFeatureClass featureClass, string fieldName, esriFieldType fieldType, esriGeometryType geometryType,
       ISpatialReference spatialReference)
        {
            if (fieldType == esriFieldType.esriFieldTypeGeometry)
            {
                featureClass.AddField(CreateField(fieldName, fieldType, null, geometryType, spatialReference));
            }
            else
            {
                featureClass.AddField(CreateField(fieldName, fieldType, null, esriGeometryType.esriGeometryNull, null));       // (CreateField(fieldName,  fieldType)
            }
            return true;
        }


        protected bool NeighborhoodStatisticsEx_DATA_IS_ARRAY_NO_INVERSE(IFeatureClass fishnetFeatClass, AnalysisParams analysis, double cellSize,
          double fishnetMinX, double fishnetMinY, long rows, long cols, string outputFilePath, string outputFileName, string outputFileNameGeneric,
           string outputDatasetType, ISpatialReference spatialReference, bool addSpatialIndex, object noDataValue, bool addAsLayer,int wHandle)
        {

            //    On Error GoTo EH

            //    'set input            
            IFeatureClass dataFeatClass = OpenShapefile(analysis.InputFilePath, analysis.InputFileNameGeneric, wHandle);
            //...............................................
         
            IFields flds = dataFeatClass.Fields;
            int cnt = flds.FieldCount;

           IFeatureDataset fds= dataFeatClass.FeatureDataset;
          
            
            //...............................................
            if (dataFeatClass.FindField("PointX") > -1)
            {
                CustomToolsRemoveField(dataFeatClass, "PointX");
            }
            CustomToolsAddField(dataFeatClass, "PointX", esriFieldType.esriFieldTypeDouble, esriGeometryType.esriGeometryNull, null);
            //    UpdateProgressStatus ProgressVal, "Calculating Data PointX ..."
            CustomToolsCalcGeometry(dataFeatClass, "PointX", enumGeometryCalculationType.PointX);
            //    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
            if (dataFeatClass.FindField("PointY") > -1)
            {
                CustomToolsRemoveField(dataFeatClass, "PointY");
            }
            CustomToolsAddField(dataFeatClass, "PointY", esriFieldType.esriFieldTypeDouble, esriGeometryType.esriGeometryNull, null);
            //    UpdateProgressStatus ProgressVal, "Calculating Data PointY ..."
            CustomToolsCalcGeometry(dataFeatClass, "PointY", enumGeometryCalculationType.PointY);
            //    If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
               if(dataFeatClass.FindField("Col") > -1)
               {
                   CustomToolsRemoveField (dataFeatClass, "Col");
               }
               CustomToolsAddField(dataFeatClass, "Col", esriFieldType.esriFieldTypeInteger,esriGeometryType.esriGeometryNull,null);
               if (dataFeatClass.FindField("Row") > -1)
               {
                   CustomToolsRemoveField(dataFeatClass, "Row");
               }
               CustomToolsAddField(dataFeatClass, "Row", esriFieldType.esriFieldTypeInteger, esriGeometryType.esriGeometryNull, null);
               CreateIndex( dataFeatClass, "PointX");
               CreateIndex( dataFeatClass, "PointY");
               CreateIndex( dataFeatClass, "Col");
               CreateIndex( dataFeatClass, "Row");

               IDataset pInsertDataset;
               IDataset pFishnetDataset;
               IDataset pOutputDataset;
               IFeatureClass pOutputFeatClass;
           

            //    'copy fishnet to output
               pFishnetDataset =(IDataset)fishnetFeatClass;
              IFeatureWorkspace pFWS= OpenFeatureWorkspace(outputFilePath);;
              //IWorkspace pFWS;Exception from HRESULT: 0x80040351
           
               pOutputDataset = pFishnetDataset.Copy(outputFileNameGeneric,(IWorkspace) pFWS);
          
               pOutputFeatClass =(IFeatureClass) pOutputDataset;


            //    'delete original fishnet
             DeleteShapefile( fishnetFeatClass);


            //    'add alalysis output fields
             switch (analysis.Type)
             {
                 case   enumAnalysisType.Max:
                     pOutputFeatClass.AddField(CreateField("Max", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
                case  enumAnalysisType.Min:
                     pOutputFeatClass.AddField(CreateField("Min", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
                 case enumAnalysisType.StDev:
                     pOutputFeatClass.AddField(CreateField("StDev", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
                 case enumAnalysisType.Sum:
                     pOutputFeatClass.AddField(CreateField("Sum", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
                 case enumAnalysisType.Count:
                     pOutputFeatClass.AddField(CreateField("Count", esriFieldType.esriFieldTypeInteger, null, esriGeometryType.esriGeometryNull, null));
                     break;
                 case enumAnalysisType.Mean:
                     pOutputFeatClass.AddField(CreateField("Mean", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
                 case enumAnalysisType.ClusterAnalysis:
                     pOutputFeatClass.AddField(CreateField("ClusterPer", esriFieldType.esriFieldTypeDouble, null, esriGeometryType.esriGeometryNull, null));
                     break;
             }

             int centroidXFieldIndex, centroidYFieldIndex, outputRowFieldIndex, outputColFieldIndex, inputRowFieldIndex, inputColFieldIndex, XFieldIndex,
              YFieldIndex, ZFieldIndex;

             centroidXFieldIndex = pOutputFeatClass.Fields.FindField("CentroidX");
                centroidYFieldIndex = pOutputFeatClass.Fields.FindField("CentroidY");
                outputRowFieldIndex = pOutputFeatClass.Fields.FindField("Row");
                outputColFieldIndex = pOutputFeatClass.Fields.FindField("Col");
                inputRowFieldIndex = dataFeatClass.Fields.FindField("Row");
                inputColFieldIndex = dataFeatClass.Fields.FindField("Col");
                XFieldIndex = dataFeatClass.Fields.FindField("PointX");
                YFieldIndex = dataFeatClass.Fields.FindField("PointY");
                ZFieldIndex = dataFeatClass.Fields.FindField(analysis.InputFieldName);
                ITable pDataTable;
                pDataTable = (ITable)dataFeatClass;
                ICursor pDataCur;
                IRow pDataRow;

            //    'applying input row coll values
            //    UpdateProgressStatus ProgressVal, "Applying data cells refernce..."
                pDataCur = GetCursor(pDataTable, "");
                pDataRow = pDataCur.NextRow();
                do
                {                    
                    //        ProgressVal = ProgressVal + 1
                    //        UpdateProgressStatus ProgressVal
                    //        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function

                    object YObject =((Math.Round(((pDataRow.get_Value(YFieldIndex) - fishnetMinY) / cellSize) + 0.5) == 0) ? 1 : Math.Round(((pDataRow.get_Value(YFieldIndex) - fishnetMinY) / cellSize) + 0.5));
                    object XObject =((Math.Round(((pDataRow.get_Value(XFieldIndex) - fishnetMinX) / cellSize) + 0.5) == 0) ? 1 : Math.Round(((pDataRow.get_Value(XFieldIndex) - fishnetMinX) / cellSize) + 0.5));
 

                    pDataRow.set_Value(inputRowFieldIndex,YObject);
                    pDataRow.set_Value(inputColFieldIndex , XObject);


                    pDataCur.UpdateRow(pDataRow);
                    pDataRow = pDataCur.NextRow();
                    //        DoEvents
                }
                while (pDataRow != null);
            pDataCur = null;

            SortFieldsParams[] sortFields = new SortFieldsParams[2];
            sortFields[0].FieldName = "Row";
            sortFields[0].Ascending = true;
            sortFields[0].CaseSensitive = false;
            sortFields[1].FieldName = "Col";
            sortFields[1].Ascending = true;
            sortFields[1].CaseSensitive = false;

            //    'sort input by row coll values
            int rowCount=0;
            pDataCur = GetReadOnlySortedCursor(pDataTable, "", sortFields,ref rowCount);
            

           // StringDC.ResizeArray(InputArr, rowCount);
            //System.Array.Resize(ref InputArr, rowCount);
            //System.Array.Resize<ClusterParam>(ref analysis.CP, i + 1);
            
            
           
            //    ReDim InputArr(1 To RowCount, 1 To 5) As Variant
            object[,] inputArr = new object[rowCount + 1, 5];
            //    Dim InputRowColIndexArr() As RowColInputIndex
            //    ReDim InputRowColIndexArr(1 To Rows, 1 To Cols) As RowColInputIndex
            RowColInputIndex[,] inputRowColIndexArr = new RowColInputIndex[rows+1,cols+1];
            long inputIndex=1;
            long rowIndex=0;
            long colIndex=0;
            //    UpdateProgressStatus ProgressVal, "Creating data index table..."

            pDataRow = pDataCur.NextRow();
            //    Do Until pDataRow Is Nothing
            do
            {
                //        ProgressVal = ProgressVal + 1
                //        UpdateProgressStatus ProgressVal
                //        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
                //        'applying input array values
                inputArr[inputIndex,0] = pDataRow.get_Value(inputRowFieldIndex);
               inputArr[inputIndex, 1] = pDataRow.get_Value(inputColFieldIndex);
                inputArr[inputIndex, 2] = pDataRow.get_Value(XFieldIndex);
               inputArr[inputIndex, 3] = pDataRow.get_Value(YFieldIndex);
               inputArr[inputIndex, 4] = pDataRow.get_Value(ZFieldIndex);

                //        'applying input index values

               if ((Int32)(inputArr[inputIndex, 0]) > rowIndex || (Int32)(inputArr[inputIndex, 1]) > colIndex)
                 {
                     if(inputIndex > 1 )
                     {
                         inputRowColIndexArr[rowIndex, colIndex].EndIndex = inputIndex - 1;
                         inputRowColIndexArr[rowIndex, colIndex].Count =  inputRowColIndexArr[rowIndex, colIndex].EndIndex -  inputRowColIndexArr[rowIndex, colIndex].StartIndex + 1;
                     }
                     rowIndex = (Int32)inputArr[inputIndex, 0];
                     colIndex = (Int32)inputArr[inputIndex, 1];
                     inputRowColIndexArr[rowIndex, colIndex].StartIndex = inputIndex;
                 }
                pDataRow = pDataCur.NextRow();
                inputIndex ++;
                //        DoEvents
            }
            while (pDataRow != null);
            inputRowColIndexArr[rowIndex, colIndex].EndIndex = inputIndex - 1;
            pDataCur = null;


            
            double s,m,mx,mn,vari;
            long c, cc, radiusCells, minRowIndex, maxRowIndex, minColIndex, maxColIndex, M, startInputIndex, endInputIndex, lngCurrentFeatureCount,I;
            IFeatureCursor pOutputFeatCur;
            IFeature pOutputFeat;

            //if (outputDatasetType == "Raster Datasets")
            //{
               object[,] pixelData = new object[cols - 1, rows - 1];
               
            //}

            //if (analysis.Type == enumAnalysisType.StDev)
            //{
            object[] neighborhoodData=null;
            //}

            lngCurrentFeatureCount = 0;

            //    'calculate statiatics...
            //    UpdateProgressStatus ProgressVal, "Calculating..."
           radiusCells =Convert.ToInt64( Math.Round((analysis.Radius / cellSize) + 0.5));
           pOutputFeatCur = GetUpdatableFeatureCursor(pOutputFeatClass, "1=1", "*");//
           pOutputFeat = pOutputFeatCur.NextFeature();
            //    Do Until pOutputFeat Is Nothing
           do
           {
               //        ProgressVal = ProgressVal + 1
               //        UpdateProgressStatus ProgressVal
               //        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
               if(analysis.Type == enumAnalysisType.StDev) 
               {
                 System.Array.Resize<object>(ref neighborhoodData,0);  //            ReDim neighborhoodData(0) As Variant
               }
               s = 0.0; c = 0; cc = 0; mx = 0; mn = 0;
             
               if (analysis.Radius == 0)
               {
                               minRowIndex = 1;
                               maxRowIndex =rows;
                               minColIndex = 1;
                               maxColIndex = cols;
               }else{
                   minRowIndex = pOutputFeat.get_Value(outputRowFieldIndex) - radiusCells;
                   minRowIndex = (minRowIndex < 1)? 1: minRowIndex;
                   maxRowIndex = pOutputFeat.get_Value(outputRowFieldIndex) + radiusCells;
                   maxRowIndex = (maxRowIndex > rows)? rows: maxRowIndex;
                   minColIndex = pOutputFeat.get_Value(outputColFieldIndex) - radiusCells;
                   minColIndex = (minColIndex < 1)? 1: minColIndex;
                   maxColIndex = pOutputFeat.get_Value(outputColFieldIndex) + radiusCells;
                   maxColIndex = (maxColIndex > cols)? cols: maxColIndex;
               }
               for(rowIndex = minRowIndex ;rowIndex<= maxRowIndex;rowIndex++)
               {
                   for (colIndex = minColIndex; colIndex <= maxColIndex; colIndex++)
                   {
                       if (inputRowColIndexArr[rowIndex, colIndex].Count > 0)
                       {
                           startInputIndex = inputRowColIndexArr[rowIndex, colIndex].StartIndex;
                           endInputIndex = inputRowColIndexArr[rowIndex, colIndex].EndIndex;
                           for(inputIndex = startInputIndex;inputIndex<=endInputIndex;inputIndex++)
                           {
                               if ((analysis.Radius == 0) || ((analysis.Radius > 0)) &&  (Math.Sqrt (Math.Pow((pOutputFeat.get_Value(centroidXFieldIndex) -
                                   (double)inputArr[inputIndex, 2]), 2) + Math.Pow((pOutputFeat.get_Value(centroidYFieldIndex) - (double)inputArr[inputIndex, 3]), 2)) <= analysis.Radius)) 
                               {
                                       c = c + 1;
                                       switch (analysis.Type)
                                       {
                                           case enumAnalysisType.Max:
                                               if (c == 1)
                                               {
                                                   mx = (double)inputArr[inputIndex, 4];

                                               }
                                               else
                                               {
                                                   if ((double)inputArr[inputIndex, 4] > mx)
                                                   {
                                                       mx = (double)inputArr[inputIndex, 4];
                                                   }
                                               }
                                       
                                               break;
                                           case enumAnalysisType.Min: 
                                               if(c==1)
                                               {
                                                   mn = (double)inputArr[inputIndex, 4];
                                               }
                                               else if ((double)inputArr[inputIndex, 4] < mn)
                                               {
                                                   mn = (double)inputArr[inputIndex, 4];
                                               }
                                                   break;
                                           case enumAnalysisType.Sum:
                                                   s = s + (double)inputArr[inputIndex, 4];
                                                   break;
                                           case enumAnalysisType.Count:
                                           case enumAnalysisType.Mean:
                                                   s = s + Convert.ToDouble(inputArr[inputIndex, 4]);//VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                                           break;
                                           case enumAnalysisType.StDev:
                                           s = s + (double)inputArr[inputIndex, 4];
                                           if (neighborhoodData.Length == 0)
                                           {
                                               System.Array.Resize<object>(ref neighborhoodData, 1);
                                           }
                                           else
                                           {
                                               System.Array.Resize<object>(ref neighborhoodData, neighborhoodData.Length+1);
                                           }
                                           neighborhoodData[neighborhoodData.Length] = inputArr[inputIndex, 4];                                           
                                           break;
                                           case enumAnalysisType.ClusterAnalysis:
                                               if(IsWithinCluster(analysis, inputArr[inputIndex, 4]))
                                               { cc = cc + 1; }
                                               break;
                                       }
                               }
                           }
                       }
                   }
               }
               //        DoEvents
               if (c > 0)
               {
                   switch (outputDatasetType)
                   {
                       case "Shapefiles":
                           switch (analysis.Type)
                           {
                               case enumAnalysisType.Max:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Max"), mx);
                                   break;
                               case enumAnalysisType.Min:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Min"), mn);
                                   break;
                               case enumAnalysisType.Sum:
                                   pOutputFeat.set_Value(  pOutputFeat.Fields.FindField("Sum"), s);
                                   break;
                               case enumAnalysisType.Count:
                                   pOutputFeat.set_Value( pOutputFeat.Fields.FindField("Count"),c);
                                   break;
                               case enumAnalysisType.Mean:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Mean"),s / c);
                                   break;
                               case enumAnalysisType.StDev:
                                   if((neighborhoodData.Length ) > 1)
                                   {
                                       vari = 0;
                                       for(int i=1; i<=neighborhoodData.Length;i++)
                                       {//  vari = vari + ((NeightborhoodData(I) - (s / C)) ^ 2)
                                           vari = vari + Math.Pow ((Convert.ToDouble(neighborhoodData[i])-(s/c)),2);
                                       }
                                       pOutputFeat.set_Value( pOutputFeat.Fields.FindField("StDev"), Math.Sqrt(vari / neighborhoodData.Length - 1));
                                   }
                                   break;
                               case enumAnalysisType.ClusterAnalysis:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("ClusterPer"), cc / c);
                                   break;
                           }
                           break;
                       case "Raster Datasets":
                           switch (analysis.Type)
                           {
                               case enumAnalysisType.Max:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = mx;
                                   break;
                               case enumAnalysisType.Min:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1,  pOutputFeat.get_Value(outputRowFieldIndex) - 1] = mn;
                                   break;
                               case enumAnalysisType.Sum:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = s;
                                   break;
                               case enumAnalysisType.Count:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = c;
                                   break;
                               case enumAnalysisType.Mean:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = s / c;
                                   break;
                               case enumAnalysisType.StDev:
                                   if((neighborhoodData.Length) > 1 )
                                   {
                                       vari = 0;
                                       for (int ig = 1 ;ig<=neighborhoodData.Length;ig++)
                                       {
                                           vari = vari + Math.Pow ((Convert.ToDouble(neighborhoodData[ig]) - (s / c)),2);
                                       }
                                       pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] 
                                           = Math.Sqrt(vari / ((neighborhoodData.Length ) - 1));
                                   }
                                   break;
                               case enumAnalysisType.ClusterAnalysis:
                                   pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = cc / c;
                                   break;
                           }
                           break;
                   }
               }else{
                   switch (outputDatasetType)
                   {
                       case "Shapefiles":
                           switch (analysis.Type)
                           {
                               case enumAnalysisType.Max:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Max"), -1);
                                   break;
                               case enumAnalysisType.Min:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Min"), -1);
                                   break;
                               case enumAnalysisType.Sum:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Sum"), -1);
                                   break;
                               case enumAnalysisType.Count:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Count"), -1);
                                   break;
                               case enumAnalysisType.Mean:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("Mean"), -1);
                                   break;
                               case enumAnalysisType.StDev:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("StDev"), -1);
                                   break;
                               case enumAnalysisType.ClusterAnalysis:
                                   pOutputFeat.set_Value(pOutputFeat.Fields.FindField("ClusterPer") , -1);
                                   break;
                           }
                           break;
                       case "Raster Datasets":
                           pixelData[pOutputFeat.get_Value(outputColFieldIndex) - 1, pOutputFeat.get_Value(outputRowFieldIndex) - 1] = -1;
                           break;
                   }
               }
            pOutputFeatCur.UpdateFeature(pOutputFeat);
               pOutputFeat = pOutputFeatCur.NextFeature();
               lngCurrentFeatureCount = lngCurrentFeatureCount + 1;
               //        DoEvents
           }
           while (pOutputFeat != null);
           pOutputFeatCur = null;


           switch (outputDatasetType)
           {
               case "Shapefiles":
               //            ' add a spatial index to the new shapefile (very important for large shapefiles).
               if(addSpatialIndex)
                   if (!CreateSpatialIndex(pOutputFeatClass))
                   {
                       MessageBox.Show("Could not create the spatial index for the output shapefile.", "Try creating it in ArcCatalog if you need it.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   }
                   break;
               case "Raster Datasets":
                   IRaster pRaster;
               //            UpdateProgressStatus ProgressVal, "Creating raster..."
                   pRaster = CreateFileRaster(outputFilePath, outputFileName, "IMAGINE Image", fishnetMinX, fishnetMinY, cols, rows, cellSize, 1,
                       pixelData, spatialReference, -1, true,wHandle);
               //            If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
                   break;
           }


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
            return true;

        }


        protected bool CustomToolsCalcGeometry(IFeatureClass featureClass, string fieldName, enumGeometryCalculationType CalculationType)
        {
            switch (CalculationType)
            {
                case enumGeometryCalculationType.MinX:
                case enumGeometryCalculationType.MinY:
                case enumGeometryCalculationType.MaxX:
                case enumGeometryCalculationType.MaxY:
                    if (featureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        MessageBox.Show("Feature geometry does not support this type of calculation.", "Can not proceed",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);                       
                    }
                    break;
                case enumGeometryCalculationType.Area:
                case enumGeometryCalculationType.Perimeter:
                case enumGeometryCalculationType.CentroidX:
                case enumGeometryCalculationType.CentroidY:
                    if (featureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                    {
                        MessageBox.Show("Feature geometry does not support this type of calculation.", "Can not proceed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case enumGeometryCalculationType.PointX :
                case enumGeometryCalculationType.PointY:
                    if (featureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        MessageBox.Show("Feature geometry does not support this type of calculation.", "Can not proceed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;            
           }

            IPoint pPoint;
            int fieldIndex = featureClass.FindField(fieldName);
            if (fieldIndex == -1)
            {
                MessageBox.Show("Field " + fieldName + " NotFound.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            IFeature pFeat;
            IFeatureCursor pFeatCur;
            pFeatCur = GetUpdatableFeatureCursor(featureClass, "1=1","*");
            pFeat = pFeatCur.NextFeature();
            do
            {
                ProgressVal = ProgressVal + 1;
                //        UpdateProgressStatus ProgressVal
                //        If FetchUserInterrupts = UserInterruptTypeStop Then Exit Function
                switch (CalculationType)
                {
                    case enumGeometryCalculationType.MinX:
                        pFeat.set_Value( fieldIndex, pFeat.Shape.Envelope.XMin);                   
                        break;
                    case enumGeometryCalculationType.MinY:
                        pFeat.set_Value(fieldIndex, pFeat.Shape.Envelope.YMin);
                        break;
                    case enumGeometryCalculationType.MaxX:
                        pFeat.set_Value(fieldIndex, pFeat.Shape.Envelope.XMax);
                        break;
                    case enumGeometryCalculationType.MaxY:
                          pFeat.set_Value(fieldIndex, pFeat.Shape.Envelope.YMax);
                        break;
                    case enumGeometryCalculationType.Area:
                        pFeat.set_Value(fieldIndex, pFeat.Shape.Envelope.YMax);
                        break;
                    case enumGeometryCalculationType.Perimeter:
                    case enumGeometryCalculationType.CentroidX:
                        pFeat.set_Value(fieldIndex, GetFeatureCentroid(pFeat).X);
                        break;
                    case enumGeometryCalculationType.CentroidY:
                        pFeat.set_Value(fieldIndex, GetFeatureCentroid(pFeat).Y);
                        break;
                    case enumGeometryCalculationType.PointX:
                        pPoint =(IPoint) pFeat.Shape;
                        pFeat.set_Value(fieldIndex, pPoint.X);
                        pPoint = null; 
                    break;
                    case enumGeometryCalculationType.PointY:
                      pPoint =(IPoint) pFeat.Shape;
                    pFeat.set_Value(fieldIndex, pPoint.Y);
                    pPoint = null;
                    break;
                }
                pFeatCur.UpdateFeature( pFeat);
                 pFeat = pFeatCur.NextFeature();
                //        DoEvents

            }
            while (pFeat != null);
            return true;
        }

        protected IFeatureCursor GetUpdatableFeatureCursor(IFeatureClass pFeatClass, string whereClause, string subFields)
        {//subFields="*"
            IQueryFilter pQFilt = new QueryFilter();
            pQFilt.WhereClause = whereClause;
            pQFilt.SubFields = subFields;          
            return pFeatClass.Update(pQFilt, false);
        }

        //protected bool CreateIndex( IFeatureClass featClass, params object[] fieldsNames)  
        //{
        //    IFields featureClassFields = featClass.Fields;

        //    IFields pFields = new Fields();
           
        //    IFieldsEdit pFieldsEdit;
        //    pFieldsEdit = (IFieldsEdit)pFields;
        //    pFieldsEdit.FieldCount_2 = fieldsNames.Length + 1;

        //    int h;
        //    string indexName=""; 

        //    for (int i = 0; i < fieldsNames.Length; i++)
        //    {
        //        h = featClass.FindField(fieldsNames[i].ToString());
        //       // pField = featClass.Fields.get_Field(h);
        //        pFieldsEdit.set_Field(i, featureClassFields.get_Field(h));
               
        //        indexName = indexName + ((i == 0) ? fieldsNames[i].ToString() : "," + fieldsNames[i].ToString());
        //    }

        //    IIndex pIndex= new Index();
        //    IIndexEdit pIndexEdit =(IIndexEdit) pIndex;
       
        //    pIndexEdit.Fields_2 = pFields;
        //    pIndexEdit.Name_2 = indexName;
        //    pIndexEdit.IsAscending_2 = false;
        //    pIndexEdit.IsUnique_2 = false;

           
        //    featClass.AddIndex(pIndex);
        //    return true;
        //}

        public bool CreateIndex(IFeatureClass featureClass, string nameOfField)
        {
            string indexName = nameOfField;
            // Ensure the feature class contains the specified field.
            int fieldIndex = featureClass.FindField(nameOfField);
            if (fieldIndex == -1)
            {
                throw new ArgumentException(
                    "The specified field does not exist in the feature class.");
            }

            // Get the specified field from the feature class.
            IFields featureClassFields = featureClass.Fields;
            IField field = featureClassFields.get_Field(fieldIndex);

            // Create a fields collection and add the specified field to it.
            IFields fields = new Fields();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            fieldsEdit.FieldCount_2 = 1;
            fieldsEdit.set_Field(0, field);

            // Create an index and cast to the IIndexEdit interface.
            IIndex index = new Index();
            IIndexEdit indexEdit = (IIndexEdit)index;

            // Set the index's properties, including the associated fields.
            indexEdit.Fields_2 = fields;
            indexEdit.IsAscending_2 = false;
            indexEdit.IsUnique_2 = false;
            indexEdit.Name_2 = indexName;

            // Add the index to the feature class.
            featureClass.AddIndex(index);
            return true;
        }

        protected IFeatureWorkspace   OpenFeatureWorkspace(string  sPath) 
        {//  ' Create FeatureWorkspace      
            IWorkspaceFactory pWsFact = new ShapefileWorkspaceFactory();
            if (pWsFact.IsWorkspace(sPath))
            {
                return (IFeatureWorkspace) pWsFact.OpenFromFile(sPath, 0);
            }
            return null;
        }

        #region Cursors

        protected ICursor GetCursor(ITable pTable,string whereClause   )
        {
            IQueryFilter pQFilt = new QueryFilter();
            pQFilt.WhereClause = whereClause;
           
            return pTable.Update(pQFilt, false);
        }

        protected ICursor GetCursor(ITable pTable, string whereClause, object RowCount)
        {
            IQueryFilter pQFilt = new QueryFilter();
            pQFilt.WhereClause = whereClause;
            
                RowCount = pTable.RowCount(pQFilt);
           
            return pTable.Update(pQFilt, false);
        }

        protected ICursor  GetReadOnlySortedCursor(ITable pTable ,string  whereClause  ,SortFieldsParams[] sortFields ,ref int rowCount ) 
        {
            IQueryFilter  pQFilt = new QueryFilter();
            pQFilt.WhereClause = whereClause;
        //    Dim I As Integer
            ITableSort pTableSort;
            pTableSort = new ESRI.ArcGIS.Geodatabase.TableSort();     //esrige/ New esriGeoDatabase.TableSort
            string fieldsString;

            int i = sortFields.GetLowerBound(0);    //Length-1;//
        //    With pTableSort
        //        I = LBound(SortFields, 1)
            fieldsString = sortFields[i].FieldName;
            pTableSort.set_Ascending(sortFields[i].FieldName, sortFields[i].Ascending);
            pTableSort.set_CaseSensitive(sortFields[i].FieldName, sortFields[i].CaseSensitive);
            for (int ix =i+1; ix <= sortFields.GetUpperBound(0); ix++)
            {
                fieldsString = fieldsString + ", " + sortFields[ix].FieldName;
                pTableSort.set_Ascending(sortFields[ix].FieldName,sortFields[ix].Ascending);
                pTableSort.set_CaseSensitive(sortFields[ix].FieldName,sortFields[ix].CaseSensitive);
            }
            pTableSort.Fields = fieldsString;
            pTableSort.QueryFilter = pQFilt;
            pTableSort.Table = pTable;
            pTableSort.Sort(null);
            if(rowCount !=null)
       
                if(rowCount !=null)
                {
                  rowCount = pTableSort.Table.RowCount(pQFilt);
                }

            return pTableSort.Rows;
        }

        public IFeatureCursor GetReadOnlyFeatureCursor(IFeatureClass pFeatClass, string whereClause, object FeatureCount)
        {
            IQueryFilter pQFilt;
            pQFilt = new QueryFilter();
            pQFilt.WhereClause = whereClause;
            if (FeatureCount != null)
            {
                FeatureCount = pFeatClass.FeatureCount(pQFilt);
            }
            return  pFeatClass.Search(pQFilt, false);
        }

        #endregion

        private bool   IsWithinCluster(AnalysisParams analysis ,object  v)
        {
            bool IsWithinCluster = false;
            for(int i = 1;i<=analysis.CP.Length;i++)
            {
                switch (analysis.CP[i].Type)
                {
                    case cnstClusterUniqueValue:
                        if(v.ToString()==analysis.CP[i].Equale)
                        {
                            IsWithinCluster= true;
                        }
                        break;
                     case cnstClusterValueRange:
                        if((Convert.ToDouble(v) >= Convert.ToDouble(analysis.CP[i].From)) && (Convert.ToDouble(v) <= Convert.ToDouble(analysis.CP[i].To)) )
                        {
                            IsWithinCluster= true;
                        }
                            break ;                                         
                }
               
            } 
            return IsWithinCluster;

        }

        private void  TestCreateFileRaster()
        {
            IMxDocument MxDoc;
            ISpatialReference pSpatialRef;
            //    Set MxDoc = ThisDocument
            //    Set pSpatialRef = MxDoc.FocusMap.SpatialReference
            //    CreateFileRaster ("C:\ArcGIS Files\TestCode", "TestRaster5.img", "IMAGINE Image", 127, 300, 100, 80, 0.03, 1, pSpatialRef, -1, True)

        }


        private IRaster CreateFileRaster(string path,string name  ,string  format ,double  originX ,double originY ,long columnCount,long rowCount ,  
          double  cellSize  ,int  bands  ,object[,] PixelData ,ISpatialReference spatialReference  ,object NoDataValue,bool permanent ,int wHandle) 
        {
        //    'open a raster workspace
            IRasterWorkspace2 pWs;
            pWs = OpenRasterWorkspace(path,wHandle);

        //    'set the origin, width and height of the raster dataset
            IPoint pOrigin;
            long width ,height ;

             pOrigin = new Point();
            pOrigin.PutCoords( originX, originY);
            width = columnCount;
            height = rowCount;
            rstPixelType pixlType= rstPixelType.PT_FLOAT;
        //    'Create the raster dataset
          //  IRasterDataset pRasterDs  = pWs.CreateRasterDataset(name, format, pOrigin, width, height, cellSize, cellSize, bands, pixlType, spatialReference,permanent);

        //    Dim pProp As IRasterProps
        //    Dim pBands As IRasterBandCollection
        //    Dim nBands As Integer
        //    Dim pBand As IRasterBand

        //    'Set NoData if necessary
        //    Set pBands = pRasterDs
        //    nBands = pBands.Count
        //    For k = 0 To nBands - 1
        //      Set pBand = pBands.Item(k)
        //      Set pProp = pBand
        //      pProp.NoDataValue = NoDataValue
        //    Next k

        //    'Create a Raster from the raster dataset
            IRaster   pRaster=null;
          //  pRaster = pRasterDs.CreateDefaultRaster();

        //    'Create a pixel block for the pixels that need to write
        //    Dim pPB As IPixelBlock3
        //    Dim pPnt As IPnt
        //    Set pPnt = New Pnt
        //    pPnt.SetCoords Width, Height
        //    Set pPB = pRaster.CreatePixelBlock(pPnt)

        //    Dim v As Variant
        //    For k = 0 To nBands - 1
        //        v = pPB.PixelData(k)
        //        'Flip pixel block vertically
        //        For I = 0 To Width - 1
        //            For J = 0 To Height - 1
        //                v(I, J) = PixelData(I, (Height - 1) - J)
        //            Next J
        //        Next I
        //        pPB.PixelData(k) = v
        //        Set v = Nothing
        //    Next k

        //    'Write the pixel block with a (0,0) offset of the upper left corner
        //    Dim pRasterEdit As IRasterEdit
        //    Set pRasterEdit = pRaster
        //    pPnt.SetCoords 0, 0
        //    pRasterEdit.Write pPnt, pPB

        //    'clean up
        //    Set pRasterEdit = Nothing
        //    'Set pRaster = Nothing
        //    Set pProp = Nothing
        //    Set pPB = Nothing
        //    Set pBand = Nothing
        //    Set pBands = Nothing
        //    Set pRasterDs = Nothing
        //    Set pWs = Nothing

       return pRaster;
    }

        
        protected IRasterWorkspace2  OpenRasterWorkspace(string sPath,int wHandle) 
        {
            //  ' Create RasterWorkspace
            IWorkspaceFactory pWsFact=new RasterWorkspaceFactory();
            IRasterWorkspace2 tmpRaster=null;
            if(pWsFact.IsWorkspace(sPath))
            {
               tmpRaster= (IRasterWorkspace2)pWsFact.OpenFromFile(sPath, wHandle);
            }
            return tmpRaster;
            pWsFact = null;

        }


        #region enums

        public enum enumFishnetType
        {
            PolygonsFishnet = 1,
            LinesFishnet = 2
        }

        public enum enumGeometryCalculationType
        {
            MinX = 1,
            MinY = 2,
            MaxX = 3,
            MaxY = 4,
            Area = 5,
            Perimeter = 6,
            CentroidX = 7,
            CentroidY = 8,
            PointX = 9,
            PointY = 10
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

      
        #region structs

        public struct SortFieldsParams
        {
            public string FieldName { get; set; }
            public bool Ascending { get; set; }
            public bool CaseSensitive { get; set; }
        }

        public struct RowColInputIndex
        {
            public long StartIndex { get; set; }
            public long EndIndex { get; set; }
            public long Count { get; set; }
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
            public int Type;
            public object From;
            public object To;
            public object Equale;
        }
        
        public struct ScatterPoint
        {
            object Value;
            double Distance;
            double InverseDistance;
            double Weight;
            object WeightedValue;
        }

        public struct Neightborhood
        {
            ScatterPoint[] ScatterPoints;
            object Sum;
            long Count;
            object Max;
            object Min;
            long ClusterCount;
            double SumInverseDistance;
        }

        #endregion
    }
}


//*****************************************************************************************************************
//*****************************************************************************************************************
//******************************************mdl_StrinMunip*********************************************************
//*****************************************************************************************************************
//*****************************************************************************************************************

//Option Explicit

//Function clearSpaces(ByVal st As String) As String
//Dim I As Integer
//Dim tmp As String
//Dim ch As String
//tmp = ""
//    For I = 1 To Len(st)
//        ch = Mid$(st, I, 1)
//        If ch <> " " Then tmp = tmp + ch
//    Next I

//    clearSpaces = tmp
//End Function

//Function clearChar(ByVal st As String, ByVal clrCH As String) As String
//Dim I As Integer
//Dim tmp As String
//Dim ch As String
//tmp = ""
//    For I = 1 To Len(st)
//        ch = Mid$(st, I, 1)
//        If ch <> clrCH Then tmp = tmp + ch
//    Next I
    
//    clearChar = tmp
//End Function

//Function clearLine(ByVal st As String) As String
//Dim I As Integer
//Dim tmp As String
//Dim ch As String
//tmp = ""
//    For I = 1 To Len(st)
//        ch = Mid$(st, I, 1)
//        If ch <> "-" Then tmp = tmp + ch
//    Next I

//    clearLine = tmp
//End Function

//Function ClearFromNumbers(ByVal st As String) As String
//Dim retST As String
//Dim I As Integer
//    retST = ""
//    For I = 1 To Len(st)
//        If Asc(Mid$(st, I, 1)) < 47 Or Asc(Mid$(st, I, 1)) > 58 Then
//            retST = retST + Mid$(st, I, 1)
//        End If
//    Next I
//    ClearFromNumbers = retST
//End Function



//Private Sub TestNotNumberstoSpace()
//Dim ret As String

//    ret = NotNumberstoSpace("aa1on12  moshe333palombo")

//End Sub

//Function NotNumberstoSpace(ByVal st As String) As String
//Dim retST As String
//Dim I As Integer
//    retST = ""
//    For I = 1 To Len(st)
//        If (Asc(Mid$(st, I, 1)) < 48 Or Asc(Mid$(st, I, 1)) > 57) And _
//        Mid$(st, I, 1) <> "-" Then
//            retST = retST + " "
//        Else
//            retST = retST + Mid$(st, I, 1)
//        End If
//    Next I
//    NotNumberstoSpace = Trim(retST)
//End Function



//Private Sub TestNumberstoSpace()
//Dim ret As String

//    ret = NumberstoSpace("aa1on12  moshe333palombo")

//End Sub


//Function NumberstoSpace(ByVal st As String) As String
//Dim retST As String
//Dim I As Integer
//    retST = ""
//    For I = 1 To Len(st)
//        If (Asc(Mid$(st, I, 1)) >= 48 And Asc(Mid$(st, I, 1)) <= 57) Then
//            retST = retST + " "
//        Else
//            retST = retST + Mid$(st, I, 1)
//        End If
//    Next I
    
//    NumberstoSpace = Trim(retST)
    
//End Function



//Function clearDiagon(ByVal st As String) As String
//Dim I As Integer
//Dim tmp As String
//Dim ch As String
//tmp = ""
//    For I = 1 To Len(st)
//        ch = Mid$(st, I, 1)
//        If ch <> "/" Then tmp = tmp + ch
//    Next I

//    clearDiagon = tmp
//End Function


//Public Sub TestExtractSubString()
//Dim ret As String

//ret = ExtractSubString("on   moshe palombo", " ", 3)

//End Sub

//Public Function ExtractSubString(s As String, _
//Seperator As String, SubStringPosition As Long) As String

//    ExtractSubString = ExtractSubStringEx(s, _
//    Seperator, 1, SubStringPosition)

//End Function
    
//Public Function ExtractSubStringEx(s As String, Seperator As String, _
//CurrPosition As Long, SubStringPosition As Long) As String
//Dim ch As String
//Dim I As Long
//Dim tmpSubString As String
        
//    ExtractSubStringEx = ""
//    s = Trim(s)
    
//    For I = 1 To Len(s)
//        ch = Mid$(s, I, 1)
//        If ch <> Seperator Then
//            tmpSubString = tmpSubString + ch
//        Else
//            If CurrPosition = SubStringPosition Then
//                ExtractSubStringEx = tmpSubString
//            Else
//                ExtractSubStringEx = ExtractSubStringEx(Mid(s, I + 1), _
//                Seperator, CurrPosition + 1, SubStringPosition)
//            End If
//            Exit Function
//        End If
//    Next I
            
//    If CurrPosition = SubStringPosition Then
//        ExtractSubStringEx = tmpSubString
//    End If
                 
//End Function

//Private Sub TestClearBracketsContent()
//Dim ret As String

//    ret = ClearBracketsContent("ôð÷ñ ãåã 18/1 ìåã(òôøåðé)")

//End Sub


//Public Function ClearBracketsContent(s As String) As String
//Dim bBracketsScope As Boolean
//Dim I As Long
//Dim s_ret As String
    
//    bBracketsScope = False
//    For I = 1 To Len(s)
//        If Mid$(s, I, 1) = "(" Then
//            bBracketsScope = True
//        ElseIf Mid$(s, I, 1) = ")" And bBracketsScope Then
//            bBracketsScope = False
//        Else
//            If Not bBracketsScope Then
//                s_ret = s_ret + Mid$(s, I, 1)
//            End If
//        End If
//    Next I
    
//    ClearBracketsContent = Trim(s_ret)


//End Function


//Public Sub TestSimilarityIndex()
//Dim SimIndx As Single
//'    SimIndx = SimilarityIndexEarlyBinding("àåï ôìåîáå", "ôìåîáå àåï")
// SimIndx = SimilarityIndex("ðèò áøðãè", "èòð áøðãè")

//End Sub

//Public Function SimilarityIndex(left As String, right As String) As Single
//Dim wm As WordsMatching.MatchsMaker
//Dim sc As Single

//Set wm = New WordsMatching.MatchsMaker

//    wm.left = left
//    wm.right = right
//    wm.Init
    
//    SimilarityIndex = wm.Score
    
//'    Dim wm2 As WordsMatching.IMatchsMaker2
//'    Set wm2 = wm 'QI


//    Set wm = Nothing
//'    Set wm2 = Nothing

//End Function


//Sub ClearScoutInvalidCharsEx()
//    Dim db As Database
//    Dim rst As Recordset
//    Dim f As Field
    
//    Set db = CurrentDb
//    Set rst = db.OpenRecordset("mgr_final", dbOpenTable)

//        Do While Not rst.EOF
//            DoEvents
//            rst.Edit
//                For Each f In rst.Fields
//                    On Error Resume Next
//                    If Not IsNull(f.Value) Then
//                        If f.Type = dbText Then
//                            f.Value = Trim(Nz(f.Value))
//                            f.Value = Replace(Nz(f.Value), """", "")
//                            f.Value = Replace(Nz(f.Value), "'", "")
//                            f.Value = Replace(Nz(f.Value), "`", "")
//                            f.Value = Replace(Nz(f.Value), ";", "")
//                            f.Value = Replace(Nz(f.Value), "\", "")
//                        End If
//                    End If
//                Next f
//            rst.Update
//            rst.MoveNext
//        Loop
    
//    rst.Close
//    Set rst = Nothing
//    db.Close
//    Set db = Nothing

//    MsgBox "done"
//End Sub


//Sub TrimAll()
//    Dim db As Database
//    Dim rst As Recordset
//    Dim f As Field
    
//    Set db = CurrentDb
//    Set rst = db.OpenRecordset("Tbl_FINISH_For_Seker", dbOpenTable)

//        Do While Not rst.EOF
//            DoEvents
//            rst.Edit
//                For Each f In rst.Fields
//                    On Error Resume Next
//                    If Not IsNull(f.Value) Then
//                        If f.Type = dbText Then
//                            f.Value = Trim(Nz(f.Value))
//                        End If
//                    End If
//                Next f
//            rst.Update
//            rst.MoveNext
//        Loop
    
//    rst.Close
//    Set rst = Nothing
//    db.Close
//    Set db = Nothing

//    MsgBox "done"
//End Sub

//Function clearEHEVI(ByVal st As String) As String
//Dim I As Integer
//Dim tmp As String
//Dim ch As String
//    tmp = ""
//    clearEHEVI = ""
//    For I = 1 To Len(st)
//        ch = Mid$(st, I, 1)
//        If ch <> "à" And ch <> "ä" And ch <> "å" And ch <> "é" Then
//            tmp = tmp + ch
//        End If
//    Next I

//    clearEHEVI = tmp
//End Function

