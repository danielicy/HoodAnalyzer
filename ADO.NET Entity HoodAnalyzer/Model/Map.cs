using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ESRI.ArcGIS.MapControl;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;


namespace EmployeeTracker.Model
{
    public class MapObject :AxMapControl 
    {                
        private IEnumLayer m_enumLayers;
        private ObservableCollection<LayersClass> layerCollection;

        public MapObject()
        {
            LayersCollection=LoadLayers();
            this.OnMapReplaced += MapObject_OnMapReplaced;
        }
        
        void MapObject_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            LayersCollection = LoadLayers();
        }

        public ObservableCollection<LayersClass> LayersCollection
        {
            get
            { return layerCollection; }

            private set { layerCollection = value;
          
            }
        }

        private ObservableCollection<LayersClass> LoadLayers()
        {
            LayersClass lc;
            int index = 0;
            try
            {
                layerCollection = new ObservableCollection<LayersClass>();
                if (this.Map.LayerCount > 0)
                {
                    layerCollection.Clear();
                    m_enumLayers = this.Map.get_Layers(null, true);
                    ILayer m_layer = m_enumLayers.Next();
                    IFeatureLayer flayer;                    
                    do
                    { 
                        lc = new LayersClass();
                        try { 
                            flayer = (IFeatureLayer)m_layer;
                            switch (flayer.FeatureClass.ShapeType)
                            {
                                case esriGeometryType.esriGeometryPolygon:
                                    lc.LayerType ="Polygon";
                                    break;
                                case esriGeometryType.esriGeometryPoint:
                                    lc.LayerType ="Point";
                                    break;
                                case esriGeometryType.esriGeometryPolyline:
                                    lc.LayerType ="Polyline";
                                    break;
                                default:
                                    lc.LayerType ="Other data";
                                break;
                         } 
                        }
                        catch 
                        { lc.LayerType = "Other data"; }
                        lc.LayerIndex = index;
                        lc.LayerName = m_layer.Name;                        
                        layerCollection.Add(lc);
                        m_layer = m_enumLayers.Next();
                        index++;
                    }
                    while (m_layer != null);
                }
            }
            catch
            {
                lc = new LayersClass();
                lc.LayerName = "No Items";
                layerCollection.Add(lc);
            }

            return layerCollection;           
        }
        
    }

    public class LayersClass 
    {
        private string _layerName;
        private string _layerType;
        private int _layerIndex;

        public int LayerIndex
        {
            get { return _layerIndex; }
            set { _layerIndex = value; }
        }

        public string LayerType
        {
            get { return _layerType; }
            set { _layerType = value; }
        }

        public string LayerName
        {
            get { return _layerName; }
            set { _layerName = value;           
            }
        }


       
    }
}
//void _axMap_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        //{
        //    string str = "";
        //}

        //public  AxMapControl AxMap
        //{
        //    get { return _axMap; }
        //    set 
        //    {
        //        value = _axMap;
                     
        //       // this.AxMap.CreateControl();
        //    }
        //}