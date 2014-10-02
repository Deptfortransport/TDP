// ***********************************************
// NAME 		: JourneyPlanControlData.cs
// AUTHOR 		: Callum
// DATE CREATED : 19/09/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/JourneyPlanControlData.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.9   Oct 15 2004 12:31:24   jgeorge
//Added JourneyPlanStateData and modifications to the serialization process.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.8   Feb 16 2004 13:48:58   asinclair
//Updated for Del 5.2 - Stores the position int and random  array for the WaitPage
//
//   Rev 1.7   Oct 22 2003 12:20:38   RPhilpott
//Improve CJP error handling
//
//   Rev 1.6   Oct 10 2003 12:52:00   passuied
//fixed timeout bug (in sec not minutes)
//
//   Rev 1.5   Sep 25 2003 11:47:14   PNorell
//Fixed bug in timeout check.
//
//   Rev 1.4   Sep 23 2003 15:28:32   RPhilpott
//Add some operational logging
//
//   Rev 1.3   Sep 23 2003 14:50:08   PNorell
//Updated page states and the wait page to function according to spec.
//Updated the different controls to ensure they have correct PageId and that they call the ValidateAndRun properly.
//Removed some 'warning' messages - a clean project is nice to see.

using System;
using System.Threading;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;



namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for JourneyPlanControlData.
	/// </summary>
	/// 
	[Serializable()]
	public class JourneyPlanControlData
	{
		
		private PageId waitingPage;
		private PageId ambiguityPage;
		private PageId errorPage;
		private PageId destinationPage;
		private int waitingCount;
		private ArrayList randomPicArray;


		public PageId WaitingPage
		{
			get { return waitingPage; }
			set { waitingPage = value; }
		}

		public PageId AmbiguityPage
		{
			get { return ambiguityPage; }
			set { ambiguityPage = value; }
		}

		public PageId ErrorPage
		{
			get { return errorPage; }
			set { errorPage = value; }
		}

		public PageId DestinationPage
		{
			get { return destinationPage; }
			set { destinationPage = value; }
		}

		public JourneyPlanControlData()
		{
		}

		public int WaitingCount
		{
			get { return waitingCount; }
			set { waitingCount = value; }
		}

		public ArrayList RandomPicArray
		{
			get { return randomPicArray; }
			set { randomPicArray = value; }
		}


	}
}
