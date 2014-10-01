// *********************************************** 
// NAME                 : PublicJourneyCallingPoint.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourneyCallingPoint Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourneyCallingPoint.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:30   mturner
//Initial revision.
//
//   Rev 1.4   Feb 02 2006 14:43:20   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.3   Jan 25 2006 16:19:14   mdambrine
//add serializable attribute
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 17 2006 09:48:16   halkatib
//Added setters to the public properties and removed unnecessary namespaces
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 06 2006 15:59:40   halkatib
//Applied changes required by wsdl documents for IR3407
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:04:20   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class PublicJourneyCallingPoint
	{

		private ResponseLocation location;
		private DateTime arrivalDateTime;
		private DateTime departureDateTime;
		private PublicJourneyCallingPointType type;

		/// <summary>
		/// constructor
		/// </summary>
		public PublicJourneyCallingPoint()
		{
			
		}

		/// <summary>
		/// Location details for the stop.
		/// </summary>
		public ResponseLocation Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		/// <summary>
		/// Arrival time at stop. Not defined for all stop types.
		/// </summary>
		public DateTime ArrivalDateTime
		{
			get
			{
				return arrivalDateTime;
			}
			set
			{
				arrivalDateTime = value;
			}
		}

		/// <summary>
		/// Departure time from stop. Not defined for all stop types.
		/// </summary>
		public DateTime DepartureDateTime
		{
			get
			{
				return departureDateTime;
			}
			set
			{
				departureDateTime = value;
			}
		}

		/// <summary>
		/// The stop type.
		/// </summary>
		public PublicJourneyCallingPointType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
	}// END CLASS DEFINITION PublicJourneyCallingPoint

} // TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
