// ******************************************************************************************* 
// NAME                 :	BusinessLinkState.cs
// AUTHOR               :	Tolu Olomolaiye
// DATE CREATED         :	22 Nov 2005 
// DESCRIPTION			:	Class to hold the user's current state with regard to the 
//							Business Links pages
// ******************************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/BusinessLinkState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:18   mturner
//Initial revision.
//
//   Rev 1.3   Jan 23 2006 18:30:26   jbroome
//Added new LocationSubmitted value
//Resolution for 3488: Business Links:  Inconsistant behaviour of the Back button from the ambiguity page when location is unresolved
//
//   Rev 1.2   Jan 05 2006 17:45:52   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Dec 16 2005 12:28:24   jbroome
//Removed incorrect NonSerializableAttribute tag
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.0   Nov 22 2005 17:40:00   tolomolaiye
//Initial revision.

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Hold user's current business link state
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class BusinessLinkState
	{

		private TDLocation location;
		private LocationSearch locationSearch;
		private BusinessLinkTemplate templateType;
		private BusinessLinksStage currentStage;
		private bool locationSubmitted;

		/// <summary>
		/// constructor for class - call the clear method
		/// </summary>
		public BusinessLinkState()
		{
			Clear();
		}

		/// <summary>
		/// get/set prtoperty for location
		/// </summary>
		public TDLocation Location
		{
			get
			{
				return location;
			}

			set
			{
				location = value;
			}
		}

		/// <summary>
		/// get/set property for locationsearch
		/// </summary>
		public LocationSearch LocationSearch
		{
			get
			{
				return locationSearch;
			}

			set
			{
				locationSearch = value;
			}
		}

		/// <summary>
		/// get/set property for templatetype
		/// </summary>
		public BusinessLinkTemplate TemplateType
		{
			get
			{
				return templateType;
			}

			set
			{
				templateType = value;
			}
		}

		/// <summary>
		/// get/set property for currentstage
		/// </summary>
		public BusinessLinksStage CurrentStage
		{
			get
			{
				return currentStage;
			}

			set
			{
				currentStage = value;
			}
		}

		/// <summary>
		/// get/set property determining if 
		/// the user has submitted a location
		/// </summary>
		public bool LocationSubmitted
		{
			get
			{
				return locationSubmitted;
			}
			set
			{
				locationSubmitted = value;
			}
		}

		/// <summary>
		/// reset objects in the class
		/// </summary>
		public void Clear()
		{
			location = new TDLocation();
			locationSearch = new LocationSearch();
			templateType = null;
			currentStage = BusinessLinksStage.Introduction;
			locationSubmitted = false;
		}
	}
}
