// *********************************************** 
// NAME                 : MapLogging.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/09/2003 
// DESCRIPTION  : Specific Logging class for the map control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/MapLogging.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:18:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:56   mturner
//Initial revision.
//
//   Rev 1.8   Feb 23 2006 19:16:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.0   Jan 10 2006 15:54:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Dec 18 2003 15:58:18   kcheung
//Removed previous methods of the Write method that did not take a DateTime as a parameter.  Removed code that writes an operational event for verbose logging as the Map Event now has its own FileFormatter.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.6   Dec 17 2003 16:44:04   kcheung
//Added extra method that takes a DateTime parameter.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.5   Dec 17 2003 10:17:54   PNorell
//Prepared for logging changings.
//
//   Rev 1.4   Oct 14 2003 10:36:30   PNorell
//Arranged for the map logging to work according to the spec.
//
//   Rev 1.3   Sep 15 2003 16:42:24   geaton
//MapEvent changed to DateTime instead of TDDateTime.
//
//   Rev 1.2   Sep 12 2003 10:03:28   PNorell
//Oops, my fault, corrected what was already corrected - but in Session Manager.
//
//   Rev 1.1   Sep 12 2003 09:27:46   PNorell
//Temporarly shift the session id to fetch from the session rather the TDSession to ensure project compiles.
//
//   Rev 1.0   Sep 11 2003 15:00:04   passuied
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;
using System.Web;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Specific Logging class for the map control
	/// </summary>
	public class MapLogging
	{
		
		private const int OSSTREETVIEW = 10000;
		private const int SCALECOLOURRASTER50 = 50000;
		private const int SCALECOLOURRASTER250 = 250000;
		private const int MINISCALE = 1000000;


		private MapLogging()
		{
		}

		public static void Write (MapEventCommandCategory commandCategory, MapEventDisplayCategory displayCategory, DateTime submitTime)
		{
			string sessionId = TDSessionManager.Current.Session.SessionID;
			
			bool userLoggedOn = TDSessionManager.Current.Authenticated;
			
			MapEvent me = new MapEvent(commandCategory, submitTime, displayCategory, sessionId, userLoggedOn);

			Logger.Write(me);
		}

		public static void Write( MapEventCommandCategory commandCategory, int scale, DateTime submitTime )
		{
			// MapEventCommandCategory
			// Default to log as Overview (england full view)
			MapEventDisplayCategory medc = MapEventDisplayCategory.Strategi;

			// convert scale to MapEventDisplayCategory
			if( scale <= OSSTREETVIEW )
			{
				medc = MapEventDisplayCategory.OSStreetView;
			}
			else if( scale <= SCALECOLOURRASTER50 )
			{
				medc = MapEventDisplayCategory.ScaleColourRaster50;
			}
			else if( scale <= SCALECOLOURRASTER250 )
			{
				medc = MapEventDisplayCategory.ScaleColourRaster250;
			}
			else if( scale <= MINISCALE )
			{
				medc = MapEventDisplayCategory.MiniScale; 
			}

			Write( commandCategory, medc, submitTime );
		}
	}
}
