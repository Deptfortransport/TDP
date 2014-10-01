// *********************************************** 
// NAME             : MockInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Sep 2013
// DESCRIPTION  	: Initialisation class containing no services
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject
{
    class MockInitialisation : IServiceInitialisation
    {
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
        }
    }
}
