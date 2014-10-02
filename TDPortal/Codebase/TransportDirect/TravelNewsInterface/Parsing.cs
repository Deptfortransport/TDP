// *********************************************** 
// NAME                 : Parsing.cs
// AUTHOR               : Annukka Viitanen
// DATE CREATED         : 01/03/2006 
// DESCRIPTION : Classes for converting travel 
// news data between internal and external values.
// Previously in Enumerations.cs
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/Parsing.cs-arc  $ 
//
//   Rev 1.2   Mar 10 2008 15:28:30   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:38   mturner
//Initial revision.
//
//   Rev 1.3   Mar 16 2006 12:00:54   AViitanen
//Added more comments. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.2   Mar 14 2006 15:46:18   AViitanen
//Updated following code review comments.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.1   Mar 13 2006 10:10:12   AViitanen
//Fxcop updates. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.0   Mar 03 2006 15:57:46   AViitanen
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
	/// <summary>
	/// Class for converting travel news data between internal and external values.
	/// </summary>
	sealed public class Parsing
	{
		/// <summary>
		/// Constructor - does nothing.
		/// </summary>
		private Parsing(){}

		/// <summary>
		/// Converts travel news incident severity level to SeverityLevel
		/// </summary>
		/// <param name="level">severity level</param>
		/// <returns>severity level as SeverityLevel</returns>
		public static SeverityLevel ParseSeverityLevel (byte level)
		{
			SeverityLevel sLevel = (SeverityLevel)level;
			return sLevel;
		}

		/// <summary>
		/// Converts travel news incident transport type to TransportType
		/// </summary>
		/// <param name="type">transport type</param>
		/// <returns>transport type as TransportType</returns>
		public static TransportType ParseTransportType ( string type)
		{
			switch (type)
			{
				case KeyValue.PublicTransport:
					return TransportType.PublicTransport;
				case KeyValue.Road:
					return TransportType.Road;
				default:
				case KeyValue.All:
					return TransportType.All;
				
			}
		}

		/// <summary>
		///  Converts travel news incident delay type to DelayType
		/// </summary>
		/// <param name="type">delay type</param>
		/// <returns>delay type as DelayType</returns>
		public static DelayType ParseDelayType (string type)
		{
			switch(type)
			{
				case KeyValue.Major:
					return DelayType.Major;
				case KeyValue.Recent:
					return DelayType.Recent;
				default:
				case KeyValue.All:
					return DelayType.All;
			}
		}

        /// <summary>
        ///  Converts travel news incidentType type to IncidentType
        /// </summary>
        /// <param name="type">incidentType type</param>
        /// <returns>incidentType type as IncidentType</returns>
        public static IncidentType ParseIncidentType(string type)
        {
            switch (type)
            {
                case KeyValue.Unplanned:
                    return IncidentType.Unplanned;
                case KeyValue.Planned:
                    return IncidentType.Planned;
                default:
                case KeyValue.All:
                    return IncidentType.All;
            }
        }

		/// <summary>
		/// Converts travel news incident display type to DisplayType
		/// </summary>
		/// <param name="type">display type</param>
		/// <returns>display type as DisplayType</returns>
		public static DisplayType ParseDisplayType (string type)
		{
			switch (type)
			{
				case KeyValue.Summary:
					return DisplayType.Summary;
				case KeyValue.Full:
				default:
					return DisplayType.Full;
			}
		}

	}
}
