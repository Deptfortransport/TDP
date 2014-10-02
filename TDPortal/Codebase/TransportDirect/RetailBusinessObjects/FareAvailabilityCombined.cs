//********************************************************************************
//NAME         : FareAvailabilityCombined.cs
//AUTHOR       : Richard Hopkins
//DATE CREATED : 13/10/2005
//DESCRIPTION  : Class that is used to record the availability of each Fare,
//				combines results with and without a Discount card
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/FareAvailabilityCombined.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:04   mturner
//Initial revision.
//
//   Rev 1.4   Dec 15 2005 20:44:36   RPhilpott
//Changes to correctly handle avilability requests with and without railcards.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.3   Dec 02 2005 11:21:16   jgeorge
//Correction to availability checking for trains with sleeper and seat availability
//Resolution for 3106: DN039 - NRS - Apex Single Tickets on Sleepers
//
//   Rev 1.2   Nov 24 2005 18:22:50   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Nov 11 2005 17:44:02   RWilby
//Updated for NRS compliance enhancement
//Resolution for 3003: NRS enhancement for non-compulsory reservations
//
//   Rev 1.0   Nov 02 2005 16:58:28   rhopkins
//Initial revision.
//

using System;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Availability of a specific Fare, combines results with and without a Discount card
	/// </summary>
	[CLSCompliant(false)]
	public class FareAvailabilityCombined
	{
		String fareKey;

		FareAvailability fareWithoutDiscount;
		FareAvailability fareWithDiscount;

		// Flags that indicate that the Fare is missing for one or more trains (legs)
		bool outwardResultMissingSomeLegs = false;
		bool inwardResultMissingSomeLegs = false;

		// Flags that indicate that the fare has been found for the current train (leg)
		bool outwardResultFound = true;
		bool inwardResultFound = true;

		// Flags that indicate that berths or seats have been found for one or more trains (legs)
		bool outwardReservableSeats = false;
		bool outwardReservableBerths = false;
		bool inwardReservableSeats = false;
		bool inwardReservableBerths = false;

		public FareAvailabilityCombined(string fareKey)
		{
			this.fareKey = fareKey;
		}


		/// <summary>
		/// Get/Set the Fare without a Discount
		/// </summary>
		public FareAvailability FareWithoutDiscount
		{
			get { return fareWithoutDiscount; }
			set { fareWithoutDiscount = value; }
		}

		/// <summary>
		/// Get/Set the Fare with a Discount
		/// </summary>
		public FareAvailability FareWithDiscount
		{
			get { return fareWithDiscount; }
			set { fareWithDiscount = value; }
		}

		/// <summary>
		/// Records whether the previous Request failed to return results
		/// and resets the flags for the next Request
		/// </summary>
		/// <param name="outward">True for Outward journey; False for Inward journey</param>
		public void ConsolidateResultOfPreviousRequest(bool outward)
		{
			if (outward)
			{
				if (outwardResultFound)
				{
					// The previous Request was successful so reset the
					// temporary flag for the next Train
					outwardResultFound = false;
				}
				else
				{
					// The previous Request failed to return any results
					// so set permanent flag to indicate that the Fare
					// is not available for Outward travel
					outwardResultMissingSomeLegs = true;
				}
			}
			else
			{
				if (inwardResultFound)
				{
					// The previous Request was successful so reset the
					// temporary flag for the next Train
					inwardResultFound = false;
				}
				else
				{
					// The previous Request failed to return any results
					// so set permanent flag to indicate that the Fare
					// is not available for Inward travel
					inwardResultMissingSomeLegs = true;
				}
			}
		}

		/// <summary>
		/// Gets the key that identifies the Fare
		/// </summary>
		public String FareKey
		{
			get { return fareKey; }
		}

		/// <summary>
		/// Get/Set whether Outward results have been found for the current train for this Fare
		/// </summary>
		public bool OutwardResultFound
		{
			get { return outwardResultFound; }
			set { outwardResultFound = value; }
		}

		/// <summary>
		/// Get/set whether any outward legs have reservable seats
		/// </summary>
		public bool OutwardReservableSeats
		{
			get { return outwardReservableSeats; }
			set { outwardReservableSeats = value; }
		}

		/// <summary>
		/// Get/set whether any outward legs have reservable berths
		/// </summary>
		public bool OutwardReservableBerths
		{
			get { return outwardReservableBerths; }
			set { outwardReservableBerths = value; }
		}

		/// <summary>
		/// Get/Set whether Undiscounted Outward places are available for this Fare
		/// </summary>
		public bool UndiscountedOutwardPlacesAvailable
		{
			set
			{
				if	(fareWithoutDiscount != null)
				{
					fareWithoutDiscount.OutwardBerthsAvailable = value;
					fareWithoutDiscount.OutwardSeatsAvailable  = value;
				}
			}
			get
			{
				bool reservationsPossible = OutwardReservableSeats || OutwardReservableBerths;

				if	(reservationsPossible)
				{
					return (!outwardResultMissingSomeLegs &&
						((fareWithoutDiscount != null) && ( ( OutwardReservableSeats && fareWithoutDiscount.OutwardSeatsAvailable ) || ( OutwardReservableBerths && fareWithoutDiscount.OutwardBerthsAvailable))));
				}
				else
				{
					return (!outwardResultMissingSomeLegs && (fareWithoutDiscount != null));
				}
			}
		}

		/// <summary>
		/// Get/Set whether Discounted Outward places are available for this Fare
		/// </summary>
		public bool DiscountedOutwardPlacesAvailable
		{
			set
			{
				if  (fareWithDiscount != null)
				{
					fareWithDiscount.OutwardBerthsAvailable = value;
					fareWithDiscount.OutwardSeatsAvailable  = value;
				}
			}
			get
			{
				bool reservationsPossible = OutwardReservableSeats || OutwardReservableBerths;

				if (reservationsPossible)
				{
					return (!outwardResultMissingSomeLegs &&
						((fareWithDiscount != null) && ( ( OutwardReservableSeats && fareWithDiscount.OutwardSeatsAvailable ) || ( OutwardReservableBerths && fareWithDiscount.OutwardBerthsAvailable))));
				}
				else
				{
					return (!outwardResultMissingSomeLegs && (fareWithoutDiscount != null));
				}
			}
		}

		/// <summary>
		/// Get/Set whether Inward results have been found for the current train for this Fare
		/// </summary>
		public bool InwardResultFound
		{
			get { return inwardResultFound; }
			set { inwardResultFound = value; }
		}

		/// <summary>
		/// Get/set whether any inward legs have reservable seats
		/// </summary>
		public bool InwardReservableSeats
		{
			get { return inwardReservableSeats; }
			set { inwardReservableSeats = value; }
		}

		/// <summary>
		/// Get/set whether any inward legs have reservable berths
		/// </summary>
		public bool InwardReservableBerths
		{
			get { return inwardReservableBerths; }
			set { inwardReservableBerths = value; }
		}

		/// <summary>
		/// Get/Set whether Discounted Inward places are available for this Fare
		/// </summary>
		public bool DiscountedInwardPlacesAvailable
		{
			set
			{
				if	(fareWithDiscount != null)
				{
					fareWithDiscount.InwardBerthsAvailable	= value;
					fareWithDiscount.InwardSeatsAvailable	= value;
				}
			}
			get
			{
				bool reservationsPossible = InwardReservableSeats || InwardReservableBerths;

				if	(reservationsPossible)
				{
					return (!inwardResultMissingSomeLegs &&
								((fareWithDiscount != null) && ( ( InwardReservableSeats && fareWithDiscount.InwardSeatsAvailable ) || ( InwardReservableBerths && fareWithDiscount.InwardBerthsAvailable))));
				}
				else
				{
					return (!inwardResultMissingSomeLegs && (fareWithoutDiscount != null));
				}
			}
		}

		/// <summary>
		/// Get/Set whether Undiscounted Inward places are available for this Fare
		/// </summary>
		public bool UndiscountedInwardPlacesAvailable
		{
			set
			{
				if	(fareWithoutDiscount != null)
				{
					fareWithoutDiscount.InwardBerthsAvailable	= value;
					fareWithoutDiscount.InwardSeatsAvailable	= value;
				}
			}
			get
			{
				bool reservationsPossible = InwardReservableSeats || InwardReservableBerths;

				if	(reservationsPossible)
				{
					return (!inwardResultMissingSomeLegs &&
						((fareWithoutDiscount != null) && ((InwardReservableSeats && fareWithoutDiscount.InwardSeatsAvailable ) || ( InwardReservableBerths && fareWithoutDiscount.InwardBerthsAvailable))));
				}
				else
				{
					return (!inwardResultMissingSomeLegs && (fareWithoutDiscount != null));
				}
			}
		}

	}
}
