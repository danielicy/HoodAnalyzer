using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeTracker.Services
{
    public interface IServiceLocator
    {
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;

        TInterface Get<TInterface>();
    }
}
