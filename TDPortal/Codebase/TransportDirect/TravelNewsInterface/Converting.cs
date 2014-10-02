// *********************************************** 
// NAME                 : Converting.cs
// AUTHOR               : Annukka Viitanen
// DATE CREATED         : 01/03/2006 
// DESCRIPTION : Class for converting travel 
// news data between internal and external values.
// Previously in Enumerations.cs
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/Converting.cs-arc  $ 
//
//   Rev 1.2   Mar 10 2008 15:28:28   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:38   mturner
//Initial revision.
//
//   Rev 1.3   Mar 16 2006 11:50:04   AViitanen
//Added comments. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.2   Mar 14 2006 15:47:54   AViitanen
//Updated following code review comments. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.1   Mar 13 2006 10:11:28   AViitanen
//Fxcop updates. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.0   Mar 03 2006 15:59:10   AViitanen
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
	/// <summary>
	/// Class for converting travel news data between internal and external values.
	/// </summary>
	sealed public class Converting
	{
		/// <summary>
		/// Constructor - does nothing.
		/// </summary>
		private Converting(){}

		/// <summary>
		/// Converts travel news incident transport type to string
		/// </summary>
		/// <param name="type">transport type</param>
		/// <returns>transport type as string</returns>
		public static string ToString( TransportType type)
		{
			switch (type)
			{
				case TransportType.PublicTransport:
					return KeyValue.PublicTransport;
				case TransportType.Road:
					return KeyValue.Road;
				case TransportType.All:
				default :
					return KeyValue.All;
			}

		}
		/// <summary>
		/// Converts travel news incident delay type to string
		/// </summary>
		/// <param name="type">delay type</param>
		/// <returns>delay type as string</returns>
		public static string ToString(DelayType type)
		{
			switch (type)
			{
				case DelayType.Major:
					return KeyValue.Major;
				case DelayType.Recent:
					return KeyValue.Recent;
				default:
				case DelayType.All:
					return KeyValue.All;
			}
		}

        /// <summary>
        /// Converts travel news incidentType type to string
        /// </summary>
        /// <param name="type">incidentType type</param>
        /// <returns>incidentType type as string</returns>
        public static string ToString(IncidentType type)
        {
            switch (type)
            {
                case IncidentType.Planned:
                    return KeyValue.Planned;
                case IncidentType.Unplanned:
                    return KeyValue.Unplanned;
                default:
                case IncidentType.All:
                    return KeyValue.All;
            }
        }
		/// <summary>
		/// Converts travel news incident display type to string
		/// </summary>
		/// <param name="type">display type</param>
		/// <returns>display type as string</returns>
		public static string ToString(DisplayType type)
		{
			switch (type)
			{
				case DisplayType.Full:
					return KeyValue.Full;
				case DisplayType.Summary:
				default:
					return KeyValue.Summary;
			}
		}

	}
}
