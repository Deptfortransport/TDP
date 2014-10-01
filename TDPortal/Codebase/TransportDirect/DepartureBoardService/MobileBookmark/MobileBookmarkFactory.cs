// *********************************************** 
// NAME                 : MobileBookmarkFactory.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 21/06/2005 
// DESCRIPTION  		: Service Factory implementation for TDMobileBookmark.
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/MobileBookmarkFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:32   mturner
//Initial revision.
//
//   Rev 1.1   Jul 15 2005 13:39:52   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.0   Jun 23 2005 12:52:08   schand
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService;    

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// Service Factory implementation for TDMobileBookmark.
	/// </summary>
	public class MobileBookmarkFactory	:IServiceFactory 
	{
		
		#region Constructor
		/// <summary>
		/// Public contructor
		/// </summary>
		public MobileBookmarkFactory()
		{

		}

		#endregion

		
		#region Public Methods
		/// <summary>
		/// Method used by the ServiceDiscovery to get an instance of the TDMobileBookmark.
		/// </summary>
		/// <returns>An instance of the ITDMobileBookmark.</returns>
		public Object Get()
		{
			return new TDMobileBookmark();
		}
		#endregion


		#region Private Methods

		#endregion
	}
}

