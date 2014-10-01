// ***********************************************
// NAME                 : DummyGisQueryr.cs
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 13/10/03
// DESCRIPTION			: Implementation of the DummyGisQuery class
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TestStub/TestStubTDMapHandoff.cs-arc  $
//
//   Rev 1.2   Oct 12 2009 09:11:02   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:46   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 13 2008 16:45:04   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Sep 08 2008 15:47:32   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:24:28   mturner
//Initial revision.
//
//   Rev 1.4   Aug 19 2005 14:04:38   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.0   Aug 02 2005 16:45:00   rgreenwood
//DD073 Map Details: Added AppendPublicJourney method signature to match changes in ITDMapHandoff interface
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Mar 22 2005 10:31:10   jbroome
//Added new SaveJourneyResult overloaded method as per interface.
//
//   Rev 1.2   Feb 11 2004 12:08:42   PNorell
//Updated to support multiple journeys map handoff.
//Updated to support one stored proc call instead of many.
//

using System;
using System.IO;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;
using System.Text;

namespace  TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Stub implementation of ITDMapHandoff. Provides a modicum of fake functionality for testing purposes.
	/// </summary>
	[Serializable()]
	public class TestStubTDMapHandoff : ITDMapHandoff,  IServiceFactory
	{
		public TestStubTDMapHandoff()
		{

		}

		/// <summary>
		/// Do nothing implementation of SaveJourneyResult
		/// </summary>
		public bool SaveJourneyResult(string sessionID, PublicJourney publicJourney)
		{
			return true;
		}

		/// <summary>
		/// Do nothing implementation of SaveJourneyResult
		/// </summary>
		public bool SaveJourneyResult(string sessionID, PublicJourney[] publicJourney)
		{
			return true;
		}

        /// <summary>
        /// Do nothing implementation of SaveJourneyResult
        /// </summary>
        public bool SaveJourneyResult(bool congestion, string sessionID, int routeNum, ITNLink[] itnLinks, int congestionValue)
        {
            return true;
        }
		
		/// <summary>
		/// Do nothing implementation of SaveJourneyResult
		/// </summary>
		public bool SaveJourneyResult(bool congestion, string sessionID, int routeNum, ITNLink[] itnLinks)
		{
			return true;
		}

        /// <summary>
        /// Do nothing implementation of SaveJourneyResult
        /// </summary>
        public bool SaveJourneyResult(string sessionID, int routeNum, MemoryStream cycleRoute)
        {
            return true;
        }

		/// <summary>
		/// Do nothing implementation of SaveJourneyResult
		/// </summary>
		public bool SaveJourneyResult(ITDJourneyResult result, string sessionID)
		{
			return true;
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an nstance of the TestStubTDMapHandoff class.
		/// </summary>
		/// <returns>A new instance of a TestGisQuery.</returns>
		public Object Get()
		{
			return this;
		}

		/// <summary>
		/// Added for testing purposes for DD073 Map Details
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="pj"></param>
		public void AppendPublicJourney(StringBuilder xml, PublicJourney pj )
		{
		}

	}
}
