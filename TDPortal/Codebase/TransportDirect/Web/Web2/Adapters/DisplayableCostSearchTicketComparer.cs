//*********************************** 
// NAME			: DisplayableCostSearchTicketComparer.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 29/03/2005
// DESCRIPTION	: IComparer implementation for DisplayableCostSearchTicket. Used for sorting tables of tickets.
// ************************************************ 

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// IComparer implementation for DisplayableCostSearchTicket. Used for sorting tables of tickets.
	/// </summary>
	public class DisplayableCostSearchTicketComparer : IComparer
	{

        /// <summary>
        /// Constructor.
        /// </summary>
		public DisplayableCostSearchTicketComparer()
		{
		}

        /// <summary>
        /// Implementation of IComparer.Compare. 
        /// Sort order is defined as follows:
        /// <ul>
        /// <li>Use the adult or child fare of the CostSearchTickets associated with the 
        /// DisplayableCostSearchTicket as the first sort key. For combined tickets, this will be the
        /// total of the all tickets.</li>
        /// <li>Use the probability of the CostSearchTicket object associated with
        /// the DisplayableCostSearchTicket as the secondary sort key. Order is High, Medium, Low then None.
        /// For combined tickets, any CostSearchTicket object can be used.</li>
        /// </ul>
        /// </summary>
        /// <param name="x">Must be a DisplayableCostSearchTicket object</param>
        /// <param name="y">Must be a DisplayableCostSearchTicket object</param>
        /// <returns>Less than zero - x is less than y. Zero - x equals y. Greater than zero - x is greater than y.</returns>
        /// <exception cref="ArgumentException"></exception>
        public int Compare(object x, object y)
        {
            int currResult = 0;

            // Allow comparing to a null - null is considered "smaller" than anything else
            if (x == null && y == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;

            // Cast both arguments
            DisplayableCostSearchTicket cX = x as DisplayableCostSearchTicket;
            DisplayableCostSearchTicket cY = y as DisplayableCostSearchTicket;

            // Raise an error if either is null
            if (cX == null || cY == null)
                throw new ArgumentException("Both parameters must be instances of DisplayableTravelDate");

            CostSearchTicket cXCostSearchTicket = cX.CostSearchTickets[0];
            CostSearchTicket cYCostSearchTicket = cY.CostSearchTickets[0];

            float cXFareValue;
            float cYFareValue;

            // Set the Fare values
            if (float.IsNaN(cX.FareValue))
            {
                cXFareValue = 0.0F;
            }
            else
            {
                cXFareValue = cX.FareValue;
            }

            if (float.IsNaN(cY.FareValue))
            {
                cYFareValue = 0.0F;
            }
            else
            {
                cYFareValue = cY.FareValue;
            }

            // Do the comparisons
            if (cXFareValue < cYFareValue)
            {
                    currResult = -1;
            }
            else if (cXFareValue > cYFareValue)
            {
                currResult = 1;
            }
            else if (cXCostSearchTicket.Probability < cYCostSearchTicket.Probability)
            {
                currResult = 1;
            }
            else if (cXCostSearchTicket.Probability > cYCostSearchTicket.Probability)
            {
                currResult = -1;
            }
            else
            {
                currResult = 0;
            }

            return currResult;

        }

	}
}
