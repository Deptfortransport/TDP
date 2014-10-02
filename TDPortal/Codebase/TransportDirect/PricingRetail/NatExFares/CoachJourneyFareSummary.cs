//********************************************************************************
//NAME         : CoachJourneyFareSummary.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Implementation of CoachJourneyFareSummary class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/CoachJourneyFareSummary.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:04   mturner
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:30:22   jbroome
//Initial revision.
//Resolution for 1405: Adjusting Journey causes unexpected results (DEL5.4)

using System;
using System.Collections;

using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Class used in the cost based search process. Holds collections
	/// of CoachJourneyFares and PublicJourneys to which they apply.	
	/// </summary>
	public class CoachJourneyFareSummary
	{

		#region Private members
			
		string originNaptan = string.Empty;
		string destinationNaptan = string.Empty;
		ArrayList journeyFares = null;
		ArrayList journeys = null;

		#endregion
		
		#region Constructor

		/// <summary>
		/// Constructor requires NaPTANS of origin and destination 
		/// locations. Intialises internal collections.
		/// </summary>
		/// <param name="originNaptan"></param>
		/// <param name="destinationNaptan"></param>
		public CoachJourneyFareSummary(string originNaptan, string destinationNaptan)
		{
			this.originNaptan = originNaptan;
			this.destinationNaptan = destinationNaptan;
			journeyFares = new ArrayList();
			journeys = new ArrayList();
		}
		
		#endregion

		#region Public properties and methods

		/// <summary>
		/// Read-only string property.
		/// NaPTAN of board location of first
		/// journey leg in CJP journey result.
		/// </summary>
		public string OriginNaptan
		{
			get { return originNaptan; }
		}

		/// <summary>
		/// Read-only string property.
		/// NaPTAN of alight location of final 
		/// journey leg in CJP journey result.
		/// </summary>
		public string DestinationNaptan
		{
			get { return destinationNaptan; }
		}

		/// <summary>
		/// Public method for adding a CoachJourneyFare object to the 
		/// internal collection of CoachJourneyFares. The new CoachJourneyFare
		/// will not be added to the collection if it is already present.
		/// </summary>
		/// <param name="journeyFare">CoachJourneyFare</param>
		public void AddDistinctJourneyFare(CoachJourneyFare journeyFare)
		{
			// Check if journeyFare already present in collection
			if (journeyFares.IndexOf(journeyFare) == -1)
			{
				bool exists = false;
				foreach (CoachJourneyFare existingFare in journeyFares)
				{
					// journeyFare also deemed to be represented if following properties match
					if (existingFare.IsAdult == journeyFare.IsAdult &&
						existingFare.IsDiscounted == journeyFare.IsDiscounted &&
						existingFare.IsSingle == journeyFare.IsSingle)
					{
						// JourneyFare already represented in summary
						exists = true;
						break;
					}
				}
				// If fare not represented, then add to collection
				if (!exists) journeyFares.Add(journeyFare);
			}
		}

		/// <summary>
		/// Read-only CoachJourneyFare array property.
		/// Collection of CoachJourneyFare objects which apply 
		/// to this CoachJourneyFareSummary.
		/// </summary>
		public CoachJourneyFare[] JourneyFares
		{
			get 
			{ 
				return (CoachJourneyFare[])journeyFares.ToArray(typeof(CoachJourneyFare));
			}
		}

		/// <summary>
		/// Public method for adding a PublicJourney object to the 
		/// internal collection of PublicJourneys. The new PublicJourney
		/// will not be added to the collection if it is already present.
		/// </summary>
		/// <param name="journey">PublicJourney</param>
		public void AddDistinctJourney(PublicJourney journey)
		{
			// Add journey object to ArrayList, if not already present
			if (journeys.IndexOf(journey) == -1)
			{
				journeys.Add(journey);	
			}
		}

		/// <summary>
		/// Read-only PublicJourney array property.
		/// Collection of PublicJourney objects which apply to
		/// this CoachJourneyFareSummary.
		/// </summary>
		public PublicJourney[] Journeys
		{
			get
			{
				return (PublicJourney[])journeys.ToArray(typeof(PublicJourney));
			}
		}

		#endregion

	}
}
