// *********************************************** 
// NAME			: DisplayableCostSearchTicket.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 08.02.05
// DESCRIPTION	: This class represents a table of 
// data displayed by the FindFareTicketSelectionControl. 
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/DisplayableCostSearchTickets.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.11   Mar 06 2007 13:43:52   build
//Automatically merged from branch for stream4358
//
//   Rev 1.10.1.0   Mar 02 2007 14:44:48   asinclair
//Added code to get fare Route Restrictions
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.10   Dec 08 2005 12:23:58   RPhilpott
//Correction to combined-ticket logic when either adult or child fares are missing ...
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.9   Apr 23 2005 17:30:26   RPhilpott
//Omit child-only tickets from adult list, and v.v.
//Resolution for 2285: Del 7 - Coach - Find a Fare - Distinguishing adult and child fares for SCL journeys
//
//   Rev 1.8   Apr 22 2005 12:28:26   COwczarek
//Add comments
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview

using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Data.SqlClient;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// This class represents a table of 
	/// data displayed by the FindFareTicketSelectionControl.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class DisplayableCostSearchTickets : IListSource
	{

	    /// <summary>
	    /// Collection of DisplayableCostSearchTicket objects 
	    /// </summary>
		private ArrayList tickets;
		
		/// <summary>
		/// Indicates if any tickets have discounted fares. True if one or more
		/// tickets have discounted fares, false otherwise.
		/// </summary>
		private bool hasDiscountedFares;
		
		/// <summary>
		/// Indicates if this table of tickets is showing adult fares. True if adult,
		/// false otherwise.
		/// </summary>
		private bool adultTickets;
		
		/// <summary>
		/// The index of the DisplayableCostSearchTicket ticket object with the lowest
		/// fare with a probability of high or medium combined.
		/// </summary>
		private int lowestHighMediumFareIndex;

		/// <summary>
		/// The index of the DisplayableCostSearchTicket ticket object with the lowest
		/// fare with a probability of low.
		/// </summary>
		private int lowestLowFareIndex;
		
		
		/// <summary>
		/// The fare value of the DisplayableCostSearchTicket ticket object with the lowest
		/// fare with a probability of high or medium combined.
		/// </summary>
		private float lowestHighMediumFare;

		/// <summary>
		/// The fare value of the DisplayableCostSearchTicket ticket object with the lowest
		/// fare with a probability of low.
		/// </summary>		
		private float lowestLowFare;
		
		/// <summary>
		/// The travel mode of the tickets in the table
		/// </summary>
        private TicketTravelMode mode;

		/// <summary>
		/// Collection of Ticket Codes belonging to a displayableCostSearchTicket object
		/// </summary>
		private ArrayList listTicketTypeCodes;

		/// <summary>
		/// Collection of Route Codes
		/// </summary>
		private ArrayList listRouteCodes;

		/// <summary>
		/// Hashtable containing the RouteCode and Restriction information text
		/// </summary>
		private Hashtable restrictionInfo;

        /// <summary>
        /// Constructor for an empty table.
        /// </summary>
        public DisplayableCostSearchTickets() 
        {
            tickets = new ArrayList();
			railFareRoutes = new ArrayList();
        }

		/// <summary>
		/// Collection of Duplicate Fare Types 
		/// </summary>
		private ArrayList railFareRoutes;

        /// <summary>
        /// Constructor for creating table of tickets. A DisplayableCostSearchTicket object
        /// will be created to wrap each CostSearchTicket.
        /// The DisplayableCostSearchTicket object is then added to the collection held by
        /// this table. The ticket properties of the added tickets are examined to establish
        /// properties of the ticket table as a whole, e.g. lowest fare, whether discounts are
        /// present.
        /// </summary>
        /// <param name="adultTickets">True if table is showing adult tickets, false otherwise</param>
        /// <param name="mode">Travel mode of the tickets in the table</param>
        /// <param name="costSearchTickets">The tickets for the table</param>
		public DisplayableCostSearchTickets(bool adultTickets, TicketTravelMode mode, CostSearchTicket[] costSearchTickets) : this()
		{
            this.adultTickets = adultTickets;
            this.mode = mode;
            addTickets(costSearchTickets);
            determineLowestFare();
			restrictionInfo =  determineFareRoutes();
		}

		#region Properties

		/// <summary>
		/// Returns the index of the ticket that should be selected by default. This will
		/// be the ticket with the lowest fare for high or medium probability combined, or
		/// if all tickets have a probability of low, then the lowest fare.
		/// </summary>
		public int DefaultSelectedIndex 
		{
			get 
			{
				if (lowestHighMediumFareIndex == -1)
				{
					return lowestLowFareIndex;
				} 
				else 
				{
					return lowestHighMediumFareIndex;
				}
			}
		}

		/// <summary>
		/// Read only property that indicates if any tickets have discounted fares. 
		/// True if one or more tickets have discounted fares, false otherwise.
		/// </summary>
		public bool HasDiscountedFares
		{
			get {return hasDiscountedFares;}
		}

        /// <summary>
		/// Read only property that indicates if this table of tickets is showing adult fares.
		/// True if adult, false otherwise.
        /// </summary>
		public bool AdultTickets
		{
			get {return adultTickets;}
		}

        /// <summary>
        /// Returns a DisplayableCostSearchTicket by it's index
        /// </summary>
        public DisplayableCostSearchTicket this[int index]
        {
            get { return (DisplayableCostSearchTicket)tickets[index]; }
        }

        /// <summary>
        /// Returns the collection of DisplayableCostSearchTicket objects held by this table.
        /// </summary>
        /// <returns>IList of DisplayableCostSearchTicket objects</returns>
        //IListSource interface implementation
        public IList GetList()
        {
            return tickets;
        }

        /// <summary>
        /// Read only property that returns whether the collection is a collection of IList objects
        /// </summary>
        /// <returns>False</returns>
        //IListSource interface implementation
        public bool ContainsListCollection
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Read only property that returns the number of tickets held by this table.
        /// </summary>
        public int Length
        {
            get { return tickets.Count; }
        }

		/// <summary>
		/// Read only property that returns the travel mode of the tickets in the table
		/// </summary>
        public TicketTravelMode Mode
        {
            get {return mode;}
        }

		/// <summary>
		/// Read only property that returns a hashtable containing the RouteCode and RestrictionText.
		/// </summary>
		public Hashtable RestrictionInfo
		{
			get { return restrictionInfo;}
		}


		/// <summary>
		/// Returns the collection of DisplayableCostSearchTicket objects held by this table.
		/// </summary>
		/// <returns>IList of DisplayableCostSearchTicket objects</returns>
		//IListSource interface implementation
		public ArrayList RailFareRoutes()
		{
			return railFareRoutes;
		}

		#endregion Properties

		#region Private methods

		/// <summary>
		/// Creates a DisplayableCostSearchTicket object to wrap each supplied CostSearchTicket
		/// object then adds it to this table's collection. For each ticket, determines whether 
		/// the ticket should be treated as a combined ticket. Tickets are classed as combined
		/// if they have the same combined ticket index. Tickets with the same combined ticket
		/// index are wrapped in the same DisplayableCostSearchTicket object.
		/// </summary>
		/// <param name="costSearchTickets">The tickets to add</param>
		private void addTickets(CostSearchTicket[] costSearchTickets) 
		{

            Hashtable ticketLists = new Hashtable();
            DisplayableCostSearchTicket displayableCostSearchTicket;

			foreach(CostSearchTicket costSearchTicket in costSearchTickets) 
			{
				if (costSearchTicket.CombinedTicketIndex == 0) 
				{
					if	(adultTickets && !float.IsNaN(costSearchTicket.AdultFare)
						|| !adultTickets && !float.IsNaN(costSearchTicket.ChildFare))
					{
						displayableCostSearchTicket = new DisplayableCostSearchTicket(adultTickets, costSearchTicket);
						addDisplayableCostSearchTicket(displayableCostSearchTicket);
					
					}
				}
				else 
				{
					if (ticketLists[costSearchTicket.CombinedTicketIndex] == null) 
					{
						ticketLists[costSearchTicket.CombinedTicketIndex] = new ArrayList();
					}
					((ArrayList)ticketLists[costSearchTicket.CombinedTicketIndex]).Add(costSearchTicket);
				}
			}

            foreach(ArrayList ticketList in ticketLists.Values) 
            {
                CostSearchTicket[] costSearchTicketList = 
                	(CostSearchTicket[])ticketList.ToArray(typeof(CostSearchTicket));

				bool allTicketsValid = true;

				// check that all are valid adult or child tickets, as required

				foreach (CostSearchTicket cst in costSearchTicketList)
				{
					if	((adultTickets && float.IsNaN(cst.AdultFare))
						|| !adultTickets && float.IsNaN(cst.ChildFare))
					{
						allTicketsValid = false;
						break;
					}
				}

				if	(allTicketsValid)
				{
					displayableCostSearchTicket = new DisplayableCostSearchTicket(adultTickets, costSearchTicketList);
					addDisplayableCostSearchTicket(displayableCostSearchTicket);
				}
            }
		}


		/// <summary>
		/// Adds the supplied DisplayableCostSearchTicket object to this object's collection.
		/// For each ticket added, the ticket properties are examined to establish whether this
		/// ticket table is displaying adult tickets and whether discounted fares are present.
		/// </summary>
		/// <param name="ticket">Ticket to add to collection</param>
        private void addDisplayableCostSearchTicket(DisplayableCostSearchTicket ticket) 
        {
            tickets.Add(ticket);
            hasDiscountedFares = (hasDiscountedFares || ticket.DiscountedFare != string.Empty);
            adultTickets = adultTickets || ticket.AdultTicket;
        }

        /// <summary>
        /// Cycles through all tickets in table to establish the lowest fare for combined
        /// high and medium probability tickets and also for all low probability tickets. 
        /// </summary>
        private void determineLowestFare() 
        {
            lowestHighMediumFareIndex = -1;
            lowestLowFareIndex = -1;
            lowestHighMediumFare = float.MaxValue;
            lowestLowFare = float.MaxValue;

            for (int i=0; i < tickets.Count; i++) 
            {
                DisplayableCostSearchTicket displayableCostSearchTicket = (DisplayableCostSearchTicket)tickets[i];
                CostSearchTicket costSearchTicket = displayableCostSearchTicket.CostSearchTickets[0];

                if (costSearchTicket.Probability == Probability.High || costSearchTicket.Probability == Probability.Medium) 
                {
                    if (displayableCostSearchTicket.FareValue < lowestHighMediumFare)
                    {
                        lowestHighMediumFare = displayableCostSearchTicket.FareValue;
                        lowestHighMediumFareIndex = i;
                    }
                }
                else if (costSearchTicket.Probability == Probability.Low) 
                {
                    if (displayableCostSearchTicket.FareValue < lowestLowFare)
                    {
                        lowestLowFare = displayableCostSearchTicket.FareValue;
                        lowestLowFareIndex = i;
                    }
                }
            }
        }
		
		/// <summary>
		/// Cycles through tickets to identify those belonging to the same ticket type group.
		/// Tickets that have another ticket belonging to the same group have their RouteCode added to 
		/// an arraylist which is then passed into a Stored Procedure through a hashtable, and the Route 
		/// Description returned from the database.
		/// </summary>
		private Hashtable determineFareRoutes() 
		{
			listTicketTypeCodes = new ArrayList();
			listRouteCodes = new ArrayList();

			for (int i=0; i < tickets.Count; i++) 
			{
				
				DisplayableCostSearchTicket displayableCostSearchTicket = (DisplayableCostSearchTicket)tickets[i];
				CostSearchTicket costSearchTicket = displayableCostSearchTicket.CostSearchTickets[0];

				listRouteCodes.Add(costSearchTicket.TicketRailFareData.RouteCode);
			
			}
		
			StringBuilder routeCodeString = new StringBuilder();
			
			foreach(string ticketRoute in listRouteCodes)
			{
				routeCodeString.Append(ticketRoute + ",");
			}
			
			Hashtable restrictionInfo = new Hashtable();
			
			if(routeCodeString.Length >0)
			{
				routeCodeString.Remove(routeCodeString.Length -1, 1);
			
				SqlDataReader reader;
				SqlHelper sqlHelper = new SqlHelper();
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				
				Hashtable htParameters = new Hashtable();
				htParameters.Add("@RouteList", routeCodeString.ToString());

				reader = sqlHelper.GetReader("GetRouteRestrictions", htParameters);

				while (reader.Read())
				{
					restrictionInfo.Add(reader.GetString(0), reader.GetString(1));
				}
				reader.Close();				
			}

			return restrictionInfo;
		}

        #endregion Private methods

        #region Public methods

        /// <summary>
        /// Sorts the tickets into the order defined by the supplied sort class.
        /// </summary>
        /// <param name="sortClass">Class defining sort order algorithm</param>
        public void Sort(IComparer sortClass) 
		{
            tickets.Sort(sortClass);
            determineLowestFare();
        }

		#endregion Public methods
	}
	
}
