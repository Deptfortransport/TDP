// *********************************************** 
// NAME                 : DatabasePublishersInitialisation
// AUTHOR               : Andy Lole
// DATE CREATED         : 25/09/2003 
// DESCRIPTION			: Initialisation class for DatabsePublishers
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/DatabasePublishersInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:14   mturner
//Initial revision.
//
//   Rev 1.0   Nov 14 2003 20:30:26   JMorrissey
//Initial Revision
//
//   Rev 1.0   Sep 25 2003 12:00:28   ALole
//Initial Revision


using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Summary description for Initialisations.
	/// </summary>
	public class DatabasePublishersInitialisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}
	}
}
