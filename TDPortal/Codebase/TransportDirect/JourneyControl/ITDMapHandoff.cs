// ***********************************************
// NAME 			: ITDMapHandoff.cs
// AUTHOR 			: Richard Philpott
// DATE CREATED		: 28-Aug-2003
// DESCRIPTION 		: Interface to integrate TDMapHandoff 
//					  into Service Discovery Mechanism 
// ************************************************
// 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ITDMapHandoff.cs-arc  $
//
//   Rev 1.2   Oct 12 2009 09:10:56   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:42:52   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:44   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 13 2008 16:45:04   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Sep 08 2008 15:46:24   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.4   Aug 19 2005 14:03:54   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.1   Aug 02 2005 13:44:42   rgreenwood
//DD073 Map Details: Added system.text namespace
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3.1.0.1.0   Aug 02 2005 13:41:48   rgreenwood
//DD073 Map Details: Added System.Text namespace as AppendPublicJourney method references Stringbuilder type
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3.1.0   Aug 02 2005 11:29:28   rgreenwood
//DD073 Map Details: Added AppendPublicJourney signature for testing purposes
//
//   Rev 1.3   Mar 22 2005 10:26:54   jbroome
//Added new SaveJourneyResult overload method
//
//   Rev 1.2   Feb 11 2004 12:08:40   PNorell
//Updated to support multiple journeys map handoff.
//Updated to support one stored proc call instead of many.
//
//   Rev 1.1   Nov 21 2003 16:29:44   PNorell
//IR305 - Unadjusted journeys saved with correct congestion number.
//
//   Rev 1.0   Sep 25 2003 11:44:20   RPhilpott
//Initial Revision

using System;
using System.IO;
using TransportDirect.JourneyPlanning.CJPInterface;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for ITDMapHandoff.
	/// </summary>
	public interface ITDMapHandoff
	{
		/// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the overload for public transport routes.
		/// </summary>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="publicJourney">A single public journey object</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		bool SaveJourneyResult(string sessionID, PublicJourney publicJourney);

		/// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the overload for public transport routes.
		/// </summary>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="publicJourney">A single public journey object</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		bool SaveJourneyResult(string sessionID, PublicJourney[] publicJourney);

		/// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the overload for road routes.
		/// </summary>
		/// <param name="congestion">True if the journey contains congestion information</param>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="routeNum">A unique ID within the users session.</param>
		/// <param name="itnLinks">An array of TOIDS.</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		bool SaveJourneyResult(bool congestion, string sessionID, int routeNum, ITNLink[] itnLinks);

        /// <summary>
        /// This method is used to persist the journeyResult to the ESRI MasterMap database.  
        /// It is overloaded to facilitate saving public transport routes and road routes.
        /// This is the overload for road routes setting one congestion value for all the routes
        /// </summary>
        /// <param name="congestion">True if the journey contains congestion information</param>
        /// <param name="sessionID">The users session ID.</param>
        /// <param name="routeNum">A unique ID within the users session.</param>
        /// <param name="itnLinks">An array of TOIDS.</param>
        /// <param name="congestionValue">Congestion value of the routes</param>
        /// <returns>A boolean. True if no error, false for all other conditions</returns>
        bool SaveJourneyResult(bool congestion, string sessionID, int routeNum, ITNLink[] itnLinks, int congestionValue);

        /// <summary>
        /// This overloaded method is used to persist the Cycle Journey route to the ESRI MasterMap database.  
        /// This is the signiture for cycle routes.
        /// </summary>
        /// <param name="sessionID">The users session ID.</param>
        /// <param name="routeNum">A unique ID within the users session.</param>
        /// <param name="cycleRoute">The binary formatter memory stream of the ESRI CycleRoute object to save</param>
        /// <returns>A boolean. True if no error, false for all other conditions</returns>
        bool SaveJourneyResult(string sessionID, int routeNum, MemoryStream cycleRoute);

		/// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// Iterates through outward and return public transport journeys and calls 
		/// SaveJourneyResult to store them on the ESRI database for subsequent map display. 
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the overload for a TDJourneyResult containing public transport routes.
		/// </summary>
		/// <param name="result">The returned journey results</param>
		/// <param name="sessionID">Session-id, used to key journeys on the database</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		bool SaveJourneyResult(ITDJourneyResult result, string sessionID);

        /// <summary>
		/// This method handles journey legs with invlaid coordinates and appends the necessary XML
		/// via a stringbuildre class to ensure that those invalid legs are greyed out on the map display.
		/// Visibility is set to public purely for NUnit testing purposes.
		/// </summary>
		/// <param name="xml">stringbuilder class</param>
		/// <param name="pj">PublicJourney</param>
		void AppendPublicJourney(StringBuilder xml, PublicJourney pj );
	}
}
