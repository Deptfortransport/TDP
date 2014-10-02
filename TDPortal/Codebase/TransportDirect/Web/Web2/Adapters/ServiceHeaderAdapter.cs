// *********************************************** 
// NAME			: ServiceHeaderAdapter.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-14
// DESCRIPTION	: Responsible for formatting  
//                service header details 
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ServiceHeaderAdapter.cs-arc  $
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
//   Rev 1.2.1.0   Jan 10 2006 15:17:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 16 2005 17:53:34   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 19:57:58   RPhilpott
//Development of ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:09:48   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Responsible for formatting service header details.
	/// </summary>
	public class ServiceHeaderAdapter : TDWebAdapter
	{

		/// <summary>
		/// Constructor.
		/// </summary>
		public ServiceHeaderAdapter()
		{
			this.LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
		/// Returns a single line of header text describing 
		/// the service that operates on teh current leg. 
		/// </summary>
		/// <param name="detail">PublicJourneyDetail for the curent leg.</param>
		/// <returns>The header text</returns>
		public string GetHeaderText(PublicJourneyDetail detail)
		{
			string resourceString = GetResource("RailServiceHeader.Text");

			string modeString = GetResource("TransportMode." + detail.Mode.ToString());

			Object[] args = new Object[4];
			
			args[0] = modeString;

			args[1] = detail.Origin.DepartureDateTime.ToString("HH:mm");
			args[2] = detail.Origin.Location.Description;
			args[3] = detail.Destination.Location.Description;

			return string.Format(CultureInfo.InvariantCulture, resourceString, args);
		}
	
	}
}
