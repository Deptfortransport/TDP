// NAME			: RailServiceOperationsAdaptor.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-18
// DESCRIPTION	: Responsible for formatting details  
//                of rail service validity. 
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/RailServiceOperationsAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:28   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:17:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 16 2005 17:53:32   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 19:57:58   RPhilpott
//Development of ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:09:50   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Responsible for formatting details of rail service validity.
	/// </summary>
	public class RailServiceOperationsAdaptor : TDWebAdapter
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public RailServiceOperationsAdaptor()
		{
			this.LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}
		
		/// <summary>
		/// Returns a formatted string indicating the period
		/// of validity  of the current journey leg. 
		/// </summary>
		/// <remarks>
		/// This method will always return an empty string
		/// until the CJP interface is able to return the 
		/// validity period of rail schedues in a usable form.
		/// </remarks>	
		/// <param name="detail"></param>
		/// <returns></returns>
		public string GetServiceValidityText(PublicJourneyDetail detail)
		{
			return string.Empty;
		}
	
	}
}