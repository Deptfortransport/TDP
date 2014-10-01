// *********************************************** 
// NAME                 : TestMockProperties.cs 
// AUTHOR               : James Cotton (Based on the Kenny Cheung Original)
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Contains mock properties that
// are used by the NUnit tests.  (This simulates
// the property service).  The reason why mock
// objects are used instead of actual property
// files is because it allows tests with 
// different properties to executed all at once
// without having to manually make changes to a
// properties file.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/TestMockProperties.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:56   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:17:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.2.0   Nov 25 2005 18:00:28   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Jun 16 2004 14:43:44   CHosegood
//Changed namespace to TransportDirect.Common.DatabaseInfrastructure
//
//   Rev 1.0   Sep 01 2003 16:26:42   jcotton
//Initial Revision
//

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.Common.DatabaseInfrastructure
{
	/// <summary>
	/// Initialisation class for the Service Discovery.
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// This stub is required even though it doesn't do anything.
		/// </summary>
		public void Populate(Hashtable serviceCache){}
	}
	
	public class TestMockGoodProperties : IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockGoodProperties()
		{
			properties = new Hashtable();
			
			// filenames
			properties["DatabaseConfigFile"] = @"td.common.databaseinfrastructure.dll.config";
			properties["DatabaseLookUpFile"] = @"td.common.databaseinfrastructure.dll.xml";
			
			// Connection Strings
			properties["DefaultDB"] = @"Server=.;Initial Catalog=Northwind;Trusted_Connection=true;";
			properties["EsriDB"] = @"Server=.;Initial Catalog=TD_ESRI;Trusted_Connection=true;";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}
		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this[string key, int partnerID]
		{
			get {return string.Empty;}
		}
				


		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}
		
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}


	}

}
