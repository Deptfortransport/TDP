// *********************************************** 
// NAME                 : IPropertyProvider.cs
// AUTHOR               : Patrick ASSUIED 
// DATE CREATED         : 2/07/2003 
// DESCRIPTION  : Interface of the PropertyService component
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/IPropertyProvider.cs-arc  $ 
//
//   Rev 1.1   Mar 10 2008 15:22:56   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:37:50   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:15:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.2.0   Dec 15 2005 10:10:08   schand
//Getting Partnet White Label changes for stream3129. 
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1.1.1   Sep 29 2005 11:24:14   pcross
//Changes associated with PartnerId becoming integer
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.1.1.0   Sep 05 2005 15:33:34   pcross
//Includes an indexer that allows entry of a microsite PartnerID.
//
//   Rev 1.1   Jul 25 2003 10:38:08   passuied
//addition of CLSCompliant
//
//   Rev 1.0   Jul 23 2003 10:22:32   passuied
//Initial Revision
//
//   Rev 1.3   Jul 17 2003 12:43:58   passuied
//changes after code review

using System;
[assembly: CLSCompliant(true)]

namespace TransportDirect.Common.PropertyService.Properties
{
	
	public delegate void SupersededEventHandler(object sender, EventArgs e);

		

	/// <summary>
	/// Interface definition for the PropertyService
	/// </summary>
	
	public interface IPropertyProvider
	{
		/// Read only IsSuperseded Property
		bool IsSuperseded
		{
			get;
		}

		event SupersededEventHandler Superseded;

		/// Read-only Version property
		int Version
		{
			get;
		}

		/// Read only Indexer property
		string this[ string key]
		{
			get;
		}

		/// Read only Indexer property that also takes a partner ID. To allow white label variations.
		string this [string key, int partnerID]
		{
			get;
		}

		/// Read only ApplicationID Property
		string ApplicationID
		{
			get;
		}

		/// Read only GroupID property
		string GroupID
		{
			get;
		}
	}
}
