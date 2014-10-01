// *********************************************** 
// NAME                 : DataServicesFactory.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/09/2003 
// DESCRIPTION  : Factory class for the DataServices component. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/DataServicesFactory.cs-arc  $ 
//
//   Rev 1.1   Mar 10 2008 15:16:04   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:20:46   mturner
//Initial revision.
//
//   Rev 1.0   Sep 16 2003 12:23:04   passuied
//Initial Revision

// Note : This factory is placed here temporarily to avoid circular dependency with Web project.
using System;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;



namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Factory class for the DataServices component
	/// </summary>
	public class DataServicesFactory : IServiceFactory
	{
		DataServices current;
		public DataServicesFactory(TDResourceManager rm)
		{
			current = new DataServices(rm);
				
		}

		public object Get()
		{
			return current;
		}
	}
}
