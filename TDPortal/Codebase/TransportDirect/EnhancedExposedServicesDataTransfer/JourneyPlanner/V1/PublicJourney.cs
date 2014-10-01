// *********************************************** 
// NAME                 : PublicJourney.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourney Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourney.cs-arc  $
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
//   Rev 1.2   Jan 17 2006 09:48:24   halkatib
//Added setters to the public properties and removed unnecessary namespaces
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 06 2006 15:59:34   halkatib
//Applied changes required by wsdl documents for IR3407
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:04:18   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class PublicJourney
	{

		private PublicJourneyDetail[] journeyDetails;
		private JourneySummary journeySummary;

		/// <summary>
		/// constructor
		/// </summary>
		public PublicJourney()
		{
			
		}

		/// <summary>
		/// Journey leg details.
		/// </summary>
		public PublicJourneyDetail[] Details
		{
			get
			{
				return journeyDetails;
			}
			set
			{
				journeyDetails = value;
			}
		}

		/// <summary>
		/// Summary information for the journey.
		/// </summary>
		public JourneySummary Summary
		{
			get
			{
				return journeySummary;
			}
			set
			{
				journeySummary = value;
			}
		}
	}

}
