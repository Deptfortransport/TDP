// *********************************************** 
// NAME			: TDWebAdapter.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-18
// DESCRIPTION	: Base class for adapaters used by pages
//                and/or controls in the Web layer, to provide 
//				  access to the appropriate Resource Manager.
//                Not appropriate for use in cases where the 
//				  adapter is to be stored in session, because
//				  applicable language may change between calls.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/TDWebAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:30   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 12:21:34   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:17:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Nov 01 2005 15:11:44   build
//Automatically merged from branch for stream2638
//
//   Rev 1.1.1.0   Sep 19 2005 11:05:00   jbroome
//Updated for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Jul 19 2005 10:36:42   RPhilpott
//Added SCR.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:35:18   RPhilpott
//Initial revision.
//

using System;
using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Base class for adapaters used by pages
	/// and/or controls in the Web layer, to provide 
	/// access to the appropriate Resource Manager.
	/// Not appropriate for use in cases where the 
	/// adapter is to be stored in session, because
	/// applicable language may change between calls.
	/// </summary>
	public class TDWebAdapter
	{
		/// <summary>
		/// The resource manager to be used by this user control
		/// </summary>
		protected TDResourceManager resourceManager = Global.tdResourceManager;

		public TDWebAdapter()
		{
		}

		/// <summary>
		/// Sets the local resource manager for the adapter. 
		/// Setting this property means that resource strings will be obtained from the local
		/// resource manager and not the global instance (langstrings).
		/// </summary>
		public string LocalResourceManager 
		{
			set 
			{
				resourceManager = TDResourceManager.GetResourceManagerFromCache(value);
			}
		}

		/// <summary>
		/// Method that returns the resource associated with the given key
		/// using the Current UI Culture. This method will first attempt to get the 
		/// resource from the local resource manager if one has been specified.		
		/// </summary>
		/// <param name="key">resource key</param>
		/// <returns>resource string</returns>
		public string GetResource(string key)
		{
			return resourceManager.GetString(key, TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Method that returns the resource associated with the given key from 
		/// the specified non-default resource manager. 
		/// </summary>
		/// <param name="key">resource key</param>
		/// <param name="resourceFileName">name of resource file</param>
		/// <returns>resource string</returns>
		public string GetResource(string resourceFileName, string key)
		{
			return resourceManager.GetString (resourceFileName, key);
		}
	}
}
