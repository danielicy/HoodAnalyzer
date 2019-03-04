using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeTracker.Model;
//using ESRI.ArcGIS.MapControl;
using System.Windows.Input;
using EmployeeTracker.ViewModel.Helpers;
using ESRI.ArcGIS.Carto;
using EmployeeTracker.Common;

namespace EmployeeTracker.ViewModel
{
   
   
    public class MapViewModel :ViewModelBase 
    {


        public MapViewModel(MapObject mapObject)
        {
            if (mapObject == null)
            {
                throw new ArgumentNullException("mapObject");
            }

            this.Model = mapObject;
          

        }

        /// <summary>
        /// Gets the underlying Map this ViewModel is based on
        /// </summary>
        public MapObject Model {
            get; private set; 
        }

       

        /// <summary>
        /// Gets or sets the Layers of the mapObject 
        /// </summary>
        //public ObservableCollection<LayersClass> LayersCollection
        //{
        //    get
        //    {
        //        return this.Model.LayersCollection;
        //    }

        //    //set
        //    //{
        //    //    this.Model.LayersCollection = value;
        //    //    this.OnPropertyChanged("LayersCollection");
        //    //}
        //}
        
      
    }
}
