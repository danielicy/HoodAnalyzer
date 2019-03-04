using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeTracker.Model;
    using EmployeeTracker.Model.Interfaces;

namespace EmployeeTracker.Common
{
    public class MapRepository : IMapObjectRepository 
    {
        private MapObject objectSet;

        /// <summary>
        /// Initializes a new instance of the DepartmentRepository class.
        /// </summary>
        /// <param name="context">Context to retrieve data from</param>
        public MapRepository(IEmployeeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.objectSet = context.mapObject ;
        } 
        
        #region IMapObjectRepository Members

        /// <summary>
        /// All departments for the company
        /// </summary>
        /// <returns>MapObject</returns>        
        public MapObject GetMap()
        {
            
            return this.objectSet;
        }

        #endregion
    }
}
