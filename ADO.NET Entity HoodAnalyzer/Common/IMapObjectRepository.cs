using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeTracker.Model;


namespace EmployeeTracker.Common
{
    
    /// <summary>
    /// Repository for retrieving MapObject data
    /// </summary>
    public interface IMapObjectRepository
    {
        MapObject GetMap();
    }
}
