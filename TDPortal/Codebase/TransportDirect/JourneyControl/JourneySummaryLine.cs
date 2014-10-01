// *********************************************** 
// NAME			: JourneySummaryLine.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the JourneySummaryLine class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneySummaryLine.cs-arc  $
//
//   Rev 1.1   Feb 24 2010 17:33:50   mmodi
//Allow a duration to be set, to avoid needing to calculate using the depart and arrive times
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.12   Apr 04 2006 15:55:10   RGriffith
//IR3701 Fix: Moved changes to Web/Adapters/FormattedJourneySummaryLine
//
//   Rev 1.10   Apr 03 2006 16:41:26   RGriffith
//IR3701 Fix: Return Car start/end locations were incorrectly populated
//
//   Rev 1.9   Nov 01 2005 15:12:58   build
//Automatically merged from branch for stream2638
//
//   Rev 1.8.1.0   Sep 29 2005 14:03:20   tolomolaiye
//Added the AllModes property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   May 20 2004 18:03:20   JHaydock
//Intermediary check-in for FindSummary with FindSummaryResultControl operational
//
//   Rev 1.7   May 10 2004 15:04:20   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.6   Sep 09 2003 10:03:22   RPhilpott
//Update comments.
//
//   Rev 1.5   Aug 27 2003 10:11:54   kcheung
//fixed arrivalDateTime spelling mistake.
//
//   Rev 1.4   Aug 26 2003 17:13:16   kcheung
//Updated - working.
//
//   Rev 1.3   Aug 20 2003 17:55:48   AToner
//Work in progress
using System;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using System.Collections;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Class encapsulating all the data needed
	/// for a single line of output in the UI's
	/// journey summary.
	/// </summary>
	public class JourneySummaryLine
	{
		private int journeyIndex = 0;
		private string originDescription = string.Empty;
		private string destinationDescription = string.Empty;
		private TDJourneyType type = 0;
		private ModeType [] modes = null;
		private ModeType [] allmodes = null;
		private int interchangeCount = 0;
		private TDDateTime departureDateTime = null;
		private TDDateTime arrivalDateTime = null;
		private int roadMiles = 0;
		private string displayNumber = "";
		private string[] operatorNames = null;
        private TimeSpan duration = TimeSpan.Zero;

		public JourneySummaryLine(
			int journeyIndex, TDJourneyType type, ModeType[] modes, int interchangeCount,
			TDDateTime departureDateTime, TDDateTime arrivalDateTime, int roadMiles, string displayNumber)
		{
			this.journeyIndex = journeyIndex;
			this.type = type;
			this.modes = RemoveDuplicateModes(modes);
			this.allmodes = modes;
			this.interchangeCount = interchangeCount;
			this.departureDateTime = departureDateTime;
			this.arrivalDateTime = arrivalDateTime;
			this.roadMiles = roadMiles;
			this.displayNumber = displayNumber;
		}

		public JourneySummaryLine(
			int journeyIndex, string originDescription, string destinationDescription, 
			TDJourneyType type, ModeType[] modes, int interchangeCount,
			TDDateTime departureDateTime, TDDateTime arrivalDateTime, int roadMiles,
			string displayNumber, string[] operatorNames)
			: this (
			journeyIndex, type, modes, interchangeCount, 
			departureDateTime, arrivalDateTime, roadMiles, displayNumber)
		{
			this.originDescription = originDescription;
			this.destinationDescription = destinationDescription;
			this.operatorNames = operatorNames;
		}

        public JourneySummaryLine(
            int journeyIndex, string originDescription, string destinationDescription,
            TDJourneyType type, ModeType[] modes, int interchangeCount,
            TDDateTime departureDateTime, TDDateTime arrivalDateTime, TimeSpan duration, int roadMiles,
            string displayNumber, string[] operatorNames)
            : this(
            journeyIndex, originDescription, destinationDescription, 
			type, modes, interchangeCount, departureDateTime, arrivalDateTime, roadMiles,
			displayNumber, operatorNames)
        {
            this.duration = duration;
        }

		/// <summary>
		/// Removes duplicate values from the mode array
		/// </summary>
		/// <param name="dupModes">ModeType[] with duplicate values to remove</param>
		/// <returns>A ModeType[] with no duplicates</returns>
		private ModeType[] RemoveDuplicateModes(ModeType[] dupModes)
		{
			ArrayList arrModes = new ArrayList();
			for (int i = 0; i < dupModes.Length; i++)
			{
				if (!arrModes.Contains(dupModes[i]))
				{
					arrModes.Add(dupModes[i]);
				}
			}
			
			ModeType[] nodupModes = new ModeType[arrModes.Count];
			arrModes.CopyTo(nodupModes);

			return nodupModes;
		}

		public int JourneyIndex
		{
			get { return journeyIndex; }
		}

		public string OriginDescription
		{
			get { return originDescription; }
		}

		public string DestinationDescription
		{
			get { return destinationDescription; }
		}

		public TDJourneyType Type
		{
			get { return type; }
		}

		public ModeType[] Modes
		{
			get { return modes; }
		}

		public ModeType[] AllModes
		{
			get {return allmodes;} 
		}

		public int InterchangeCount
		{
			get { return interchangeCount; }
		}

		public TDDateTime DepartureDateTime
		{
			get { return departureDateTime; }
		}

		public TDDateTime ArrivalDateTime
		{
			get { return arrivalDateTime; }
		}

        public TimeSpan Duration
        {
            get { return duration; }
        }

		public int RoadMiles
		{
			get { return roadMiles; }
		}

		public string DisplayNumber
		{
			get { return displayNumber; }
		}

		public string[] OperatorNames
		{
			get { return operatorNames; }
		}
	}
}
