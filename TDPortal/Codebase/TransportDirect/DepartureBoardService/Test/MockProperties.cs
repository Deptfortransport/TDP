// *********************************************** 
// NAME                 : MockProperties.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 10/01/2005
// DESCRIPTION  : Mock properties class for unit test purposes.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/MockProperties.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:15:38   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.2.0   Nov 25 2005 18:01:12   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Mar 14 2005 15:15:20   schand
//Changes for configurable switch between CJP and RTTI.
//Added Keys.GettrainInfoFromCJP.
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.7   Feb 24 2005 14:19:56   passuied
//Changes for FxCop
//
//   Rev 1.6   Feb 16 2005 16:21:34   passuied
//changes for new past time window functionality
//
//   Rev 1.5   Jan 19 2005 16:28:40   schand
//integration of RTTI + SE manager!
//
//   Rev 1.4   Jan 17 2005 14:49:14   passuied
//Unit tests OK!
//
//   Rev 1.3   Jan 14 2005 20:59:28   passuied
//back up of unit test. under construction
//
//   Rev 1.2   Jan 14 2005 16:46:34   schand
//Added Naptan prefix code
//
//   Rev 1.1   Jan 11 2005 11:39:18   passuied
//minor change
//
//   Rev 1.0   Jan 10 2005 16:37:00   passuied
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Summary description for MockProperties.
	/// </summary>
	public class MockProperties : IServiceFactory, IPropertyProvider
	{
		
		Hashtable properties;
	
		/// <summary>
		/// Constructor
		/// </summary>
		public MockProperties()
		{

			properties = new Hashtable();
		
		
			// only property we need.
			properties[Keys.CJPTimeOutKey]="30000";
			properties[Keys.DayStartHourKey] = "3";
			properties[Keys.DefaultRangeKey] = "5";
			properties[Keys.NaptanPrefix] = "9100";
			properties[Keys.CodeTypesToRemoveKey] = "IATA Postcode";
			properties[Keys.FirstEventOnlyKey] = "true";
			properties[Keys.PastTimeWindow] = "120";
			properties["DefaultDB"] = "Server=localhost;Initial Catalog=PermanentPortal;Trusted_Connection=true;";
			properties[Keys.EachServiceKey] = "true";
			properties[Keys.GetTrainInfoFromCJP] = "false";
			

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
		public string this [string key, int partnerID]
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
