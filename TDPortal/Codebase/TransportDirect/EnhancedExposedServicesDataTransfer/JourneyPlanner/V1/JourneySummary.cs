// *********************************************** 
// NAME                 : JourneySummary.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner JourneySummary Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/JourneySummary.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:28   mturner
//Initial revision.
//
//   Rev 1.6   Feb 08 2006 16:27:08   halkatib
//Made changes arising from fxcop review
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Feb 02 2006 14:43:18   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.4   Jan 25 2006 16:19:12   mdambrine
//add serializable attribute
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 17 2006 09:48:30   halkatib
//Added setters to the public properties and removed unnecessary namespaces
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 16 2006 11:22:56   mdambrine
//adding property setters
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 06 2006 15:59:26   halkatib
//Applied changes required by wsdl documents for IR3407
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:04:16   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class JourneySummary
	{

		private string originDescription = string.Empty;
		private string destinationDescription = string.Empty;
		private ModeType[] modes;
		private int interchangeCount;
		private DateTime departureDateTime;
		private DateTime arrivalDateTime;
		private string[] modesText;

		/// <summary>
		/// Constructor
		/// </summary>
		public JourneySummary()
		{
			
		}

		/// <summary>
		/// Display name of origin location.
		/// </summary>
		public string OriginDescription
		{
			get
			{
				return originDescription;
			}
			set
			{
				originDescription = value;
			}
		}

		/// <summary>
		/// Display name of destination location.
		/// </summary>
		public string DestinationDescription
		{
			get
			{
				return destinationDescription;
			}
			set
			{
				destinationDescription = value;
			}
		}

		/// <summary>
		/// The journey travel modes. Duplicate modes are removed and modes are in ascending alphabetical order.
		/// </summary>
		public ModeType[] Modes
		{
			get
			{
				return modes;
			}
			set
			{
				modes = value;
			}
		}

		/// <summary>
		/// Same as Modes but represented as language sensitive text.
		/// </summary>
		public string[] ModesText
		{
			get
			{
				//not sure what to return here 
				return modesText;
			}
			set
			{
				modesText = value;
			}
		}

		/// <summary>
		/// Number of changes, excluding walk legs.
		/// </summary>
		public int InterchangeCount
		{
			get
			{
				return interchangeCount;
			}
			set
			{
				interchangeCount = value;
			}
		}

		/// <summary>
		/// Journey departure time.
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
		/// Journey arrival time.
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
	}

} 
