// *********************************************** 
// NAME                 : DBSStopEvent.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Root Class for Stop/Event-related information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSStopEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:26   mturner
//Initial revision.
//
//   Rev 1.1   Mar 14 2005 15:11:44   schand
//Changes for configurable switch between CJP and RTTI.
//Added new property Mode.
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.5   Feb 23 2005 15:17:14   passuied
//added sorting for a results for LastService request
//
//   Rev 1.4   Feb 02 2005 10:19:10   passuied
//removed DepartureBoardType from results after design review
//
//   Rev 1.3   Jan 11 2005 16:32:52   passuied
//changes regarding CallingStops
//
//   Rev 1.2   Jan 10 2005 15:25:38   passuied
//changes in initialisation
//
//   Rev 1.1   Jan 06 2005 10:49:42   passuied
//fixed DBSEvent and DBSStopEvent design... Service is component of DBSStopEvent!!!	
//
//   Rev 1.0   Dec 30 2004 14:24:16   passuied
//Initial revision.

using System;
using System.Collections;
using System.Xml.Serialization;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{

	/// <summary>
	/// Comparer class for comparing DBSStopEvent objects. Used for sorting.
	/// </summary>
	public class DBSStopEventComparer : IComparer
	{
		private bool showDepartures;
		public DBSStopEventComparer(bool showDepartures)
		{
			this.showDepartures = showDepartures;
		}

		/// <summary>
		/// Implementation of IComparer interface. Compares 2 stop Events
		/// </summary>
		/// <param name="stopEvent1">first stopEvent to compare</param>
		/// <param name="stopEvent2">second stopEvent to compare</param>
		/// <returns>-1 first is lower, 1 if greater, 0 if identical</returns>
		public int Compare(object stopEvent1, object stopEvent2)
		{
			DBSStopEvent seStopEvent1 = stopEvent1 as DBSStopEvent;
			DBSStopEvent seStopEvent2 = stopEvent2 as DBSStopEvent;

			if (seStopEvent1 == null || seStopEvent2 == null)
				throw new ArgumentException(Messages.StopEventArgumentException);

			if (showDepartures)
				return DateTime.Compare(seStopEvent1.Stop.DepartTime, seStopEvent2.Stop.DepartTime);
			else
				return DateTime.Compare(seStopEvent1.Stop.ArriveTime, seStopEvent2.Stop.ArriveTime);

		}
	}
	/// <summary>
	/// Summary description for DBSStopEvent.
	/// </summary>
    [XmlInclude(typeof(TrainStopEvent))] // Define derived types to allow serialisation
    [Serializable]
	public class DBSStopEvent
	{

		private CallingStopStatus cssCallingStopStatus;
		
		private DBSEvent dbeDeparture;
		private DBSEvent[] dbePreviousIntermediates;
		private DBSEvent dbeStop;
		private DBSEvent[] dbeOnwardIntermediates;
		private DBSEvent dbeArrival;
		private DBSService svService;
		private DepartureBoardType mode;

		public DBSStopEvent()
		{
			cssCallingStopStatus = CallingStopStatus.Unknown;
			dbeDeparture = null;
			dbePreviousIntermediates = new DBSEvent[0];
			dbeStop = null;
			dbeOnwardIntermediates = new DBSEvent[0];
			dbeArrival = null;
		}

		public CallingStopStatus CallingStopStatus
		{
			get{ return cssCallingStopStatus;}
			set{ cssCallingStopStatus = value;}
		}
		
		public DBSEvent Departure
		{
			get{ return dbeDeparture;}
			set{ dbeDeparture = value;}
		}

		public DBSEvent[] PreviousIntermediates
		{
			get{ return dbePreviousIntermediates;}
			set{ dbePreviousIntermediates = value;}
		}

		public DBSEvent Stop
		{
			get{ return dbeStop;}
			set{ dbeStop = value;}
		}

		public DBSEvent[] OnwardIntermediates
		{
			get{ return dbeOnwardIntermediates;}
			set{ dbeOnwardIntermediates = value;}
		}

		public DBSEvent Arrival
		{
			get{ return dbeArrival;}
			set{ dbeArrival = value;}
		}

		public DBSService Service
		{
			get{ return svService;}
			set{ svService = value;}
		}

		public DepartureBoardType Mode
		{
			get{return mode;}
			set{mode = value;}
		}
	}
		
}
