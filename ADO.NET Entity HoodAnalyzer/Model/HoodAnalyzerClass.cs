using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using EmployeeTracker.HoodAux;



namespace EmployeeTracker.Model
{
    public class HoodAnalyzerModel : HoodAnalystBase
    {
        public virtual string SelectedFeatureClass { get; set; }
        public virtual string SelectedInputField { get; set; }
        public virtual int Distance { get; set; }
        public virtual enumSpatialReletionship  SpatialRelations { get; set; }
        public virtual enumAnalysisType AnalysisType { get; set; }
        public virtual string OutPutFeatureClass { get; set; }
        public virtual bool AddLayer { get; set; }
        public virtual string ClusterValues { get; set; }
        public virtual int NoDataVal { get; set; }
        public virtual int CellSize { get; set; }
        public virtual enumExtent Extent {get; set;}
        public virtual double ExtentDataTop { get; set; }
        public virtual double ExtentDataRight { get; set; }
        public virtual double ExtentDataBottom { get; set; }
        public virtual double ExtentDataLeft { get; set; }

        public HoodAnalyzerModel()
        {
           
            
        }                
    }

    public class hField
    {
        private string fieldName;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }
    }
}
