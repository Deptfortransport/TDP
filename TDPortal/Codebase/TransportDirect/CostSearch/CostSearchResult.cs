// ************************************************************** 
// NAME			: CostSearchResult.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the CostSearchResult class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CostSearchResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:18   mturner
//Initial revision.
//
//   Rev 1.20   Nov 09 2005 12:23:46   build
//Automatically merged from branch for stream2818
//
//   Rev 1.19.1.0   Nov 08 2005 11:28:24   RPhilpott
//Include OpenReturn tickets in the set of Return tickets obtained. 
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.19   May 10 2005 11:33:08   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.18   Apr 27 2005 16:28:52   jmorrissey
//Update to GetOutwardTickets and GetInwardTickets methods
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.17   Apr 27 2005 09:48:46   jmorrissey
//Update to GetJourneyResultsForTicket method.
//Resolution for 2290: PT - Coach - Session data for cost based searching
//
//   Rev 1.16   Apr 20 2005 13:51:50   jmorrissey
//Chnage to GetJourneyResultsForTicket method.
//
//   Rev 1.15   Apr 20 2005 10:29:54   RPhilpott
//Improve handling of error conditions.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.14   Apr 05 2005 11:10:58   jmorrissey
//Several updates after integration testing. Added HasTravelDates property. 
//
//   Rev 1.13   Mar 22 2005 17:54:30   jmorrissey
//After initial back end integration
//
//   Rev 1.12   Mar 22 2005 11:02:20   jmorrissey
//Fixed warnings.
//
//   Rev 1.11   Mar 10 2005 12:19:20   jmorrissey
//Updated GetTravelDate methods to return null if no matching TravelDate is found
//
//   Rev 1.10   Mar 08 2005 11:34:28   jmorrissey
//Updated after FxCop
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.9   Mar 07 2005 18:29:02   jmorrissey
//Updated after Nunit testing
//
//   Rev 1.8   Mar 04 2005 13:15:04   jmorrissey
//In Progress. Checked in as other files that use it are in today's build 
//
//   Rev 1.7   Feb 22 2005 16:49:22   jmorrissey
//Fix to bug in GetTravelDates. Added ClearErrors method. Added ResultId property.
//
//   Rev 1.6   Jan 27 2005 12:30:08   jmorrissey
//Added methods that return JourneyResults and JourneyRequests for a given ticket or tickets
//
//   Rev 1.5   Jan 26 2005 15:20:10   jmorrissey
//Added GetErrors method
//
//   Rev 1.4   Jan 18 2005 12:11:18   jgeorge
//Corrected spelling mistake
//
//   Rev 1.3   Jan 17 2005 14:54:52   tmollart
//Modified class and added [Serializable] directive.
//
//   Rev 1.2   Jan 14 2005 15:28:22   jmorrissey
//New method - GetSingleTickets plus other updates after change to designs
//
//   Rev 1.1   Jan 12 2005 13:52:30   jmorrissey
//Latest versions. Still in development.
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using System.Collections;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Instances of this CostSearchResult class are returned by the AssembleFares and 
	/// AssembleServices methods of CostSearchFacade
	/// </summary>
	[Serializable]
	public class CostSearchResult : ICostSearchResult
	{
		//holds search results
		private TravelDate[] travelDates;			
		private ArrayList searchErrors = new ArrayList();
		private static CostSearchTicket[] emptyTicketArray;

		//The result ID of this cost search		
		private Guid resultId;
		
		#region methods
		/// <summary>
		/// default constructor
		/// </summary>
		public CostSearchResult()
		{
			
		}
		
		/// <summary>
		/// Returns subset of results for the selected ticket type
		/// </summary>
		/// <param name="ticketType">An enum value e.g Single, Return</param>
		/// <returns>TravelDatesResultSet</returns>
		public TravelDatesResultSet GetTravelDates(TicketType ticketType)
		{
			//subset of search results to return
			TravelDatesResultSet resultsSet = new TravelDatesResultSet();					

			//only carry on if there are TravelDates...
			if (HasTravelDates)
			{
				//array list for the results - set to initial size of 32. 
				//This should be more than enough to prevent inefficient redoubling of array list size
				ArrayList subsetTravelDates = new ArrayList(32);
				
				//counters
				int i, j = 0;		

				//add all TravelDates that match the ticketType parameter
				for (i = 0; i < travelDates.Length ; i++)
				{			
					//add matches to subset of results 
					if (travelDates[i].TicketType == ticketType || (ticketType == TicketType.Return && travelDates[i].TicketType == TicketType.OpenReturn))
					{					
						subsetTravelDates.Add(travelDates[i]);
					}					
				}
			
				//if ticketType was Return, also need to add Singles if they differ by mode and dates
				//from all in subsetTravelDates
				if (ticketType == TicketType.Return)
				{
					//find Singles in the main travel dates collection
					for (i = 0; i < travelDates.Length ; i++)
					{	
						if (travelDates[i].TicketType == TicketType.Singles)
						{	
							TravelDate singlesTravelDate = travelDates[i];	
							bool existsInSubset = false;

							//step through the subset to see if a Return TravelDate with same mode and dates
							//has already been added to subset
							for (j = 0; j < subsetTravelDates.Count ; j ++)
							{		
								TravelDate subsetTravelDate = (TravelDate)subsetTravelDates[j];						
								
								if 	((TDDateTime.AreSameDate(subsetTravelDate.OutwardDate, singlesTravelDate.OutwardDate)) 
									&& (TDDateTime.AreSameDate(subsetTravelDate.ReturnDate, singlesTravelDate.ReturnDate)) 
									&& (subsetTravelDate.TravelMode == singlesTravelDate.TravelMode))
								{								
									//have found a matching Return travel date...
									existsInSubset = true;
									break;
								}															
							}	
							//if this Singles travel date did not match any of the Returns in the subset 
							//then add it in as well
							if (!existsInSubset)
							{
								subsetTravelDates.Add(singlesTravelDate);
							}
	
						}					
					}			
				}

				//convert subset of travel dates to a TravelDatesResultSet		
				resultsSet.TravelDates = new TravelDate[subsetTravelDates.Count];			
				for (i = 0; i < resultsSet.TravelDates.Length; i++)
				{
					resultsSet.TravelDates[i] = (TravelDate)subsetTravelDates[i];
				
					//set the ContainsSinglesTickets property
					if (resultsSet.TravelDates[i].TicketType == TicketType.Singles)
					{
						resultsSet.ContainsSinglesTickets = true;
					}					
				}
				//return the TravelDatesResultSet
				return resultsSet;					
			}

			//if this CostSearchResult has no travel dates, return an empty TravelDatesResultSet
			else
			{
				resultsSet.TravelDates = new TravelDate[0];
				resultsSet.ContainsSinglesTickets = false;
				return resultsSet;
			}
		}

		/// <summary>
		/// Returns array of Single tickets for the date and travel mode supplied
		/// </summary>
		/// <param name="outwardDate">a TDDateTime</param>
		/// <param name="mode">An enum value - Rail, Coach or Air</param>
		/// <returns>a CostSearchTicket array</returns>
		public CostSearchTicket[] GetSingleTickets(TDDateTime outwardDate, TicketTravelMode mode)
		{
			//used to add tickets - set to initial size of 32. 
			//This should be more than enough to prevent inefficient redoubling of array list size
			ArrayList arrayListTickets = new ArrayList(32);					

			//if Single travel dates exist that meet the criteria then add their Outward tickets to array list
			if (HasTravelDates)
			{
				//add in all single tickets matching the criteria
				foreach (TravelDate date in travelDates)
				{
					if ((date.TravelMode == mode)&& (date.TicketType == TicketType.Single) &&
						TDDateTime.AreSameDate(date.OutwardDate, outwardDate)) 
					{
						for (int i = 0; i < date.OutwardTickets.Length; i++)
						{
							arrayListTickets.Add(date.OutwardTickets[i]);							
						}					
					}				
				}		
			
				//convert array list to an array of CostSearchTickets
				CostSearchTicket[] singleTickets = (CostSearchTicket[])arrayListTickets.ToArray(typeof(CostSearchTicket));
			
				//return results
				return singleTickets;	
				
			}
			//otherwise return an empty array
			else
			{
				return emptyTicketArray;
				
			}
		}

		/// <summary>
		/// Returns array of outward Singles tickets for the dates and travel mode supplied
		/// </summary>
		/// <param name="outwardDate">outward TDDateTime</param>
		/// <param name="inwardDate">inward TDDateTime</param>
		/// <param name="mode">An enum value - Rail, Coach or Air</param>
		/// <returns>a CostSearchTicket array</returns>
		public CostSearchTicket[] GetOutwardTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode)
		{
			//used to add tickets - set to initial size of 32. 
			//This should be more than enough to prevent inefficient redoubling of array list size
			ArrayList arrayListTickets = new ArrayList(32);					

			//if travel dates exist that meet the criteria then add their Outward tickets to array list
			if (HasTravelDates)
			{
				//add tickets to results array
				foreach (TravelDate date in travelDates)
				{
					if ((date.TravelMode == mode)&& (date.TicketType == TicketType.Singles) 
						&& (TDDateTime.AreSameDate(date.OutwardDate, outwardDate))
						&& (TDDateTime.AreSameDate(date.ReturnDate, inwardDate)))	
					{
						for (int i = 0; i < date.OutwardTickets.Length; i++)
						{
							arrayListTickets.Add(date.OutwardTickets[i]);							
						}					
					}				
				}	
			
				//convert array list to an array of CostSearchTickets
				CostSearchTicket[] outwardTickets = (CostSearchTicket[])arrayListTickets.ToArray(typeof(CostSearchTicket));	
				//return the tickets
				return outwardTickets;	
			}
			//otherwise return an empty array
			else
			{
				return emptyTicketArray;
			}							
		}

		/// <summary>
		/// Returns array of inward Singles tickets for the dates and travel mode supplied
		/// </summary>
		/// <param name="outwardDate">outward TDDateTime</param>
		/// <param name="inwardDate">inward TDDateTime</param>
		/// <param name="mode">An enum value - Rail, Coach or Air</param>
		/// <returns>a CostSearchTicket array</returns>
		public CostSearchTicket[] GetInwardTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode)
		{
			//used to add tickets - set to initial size of 32. 
			//This should be more than enough to prevent inefficient redoubling of array list size
			ArrayList arrayListTickets = new ArrayList(32);					

			//if travel dates exist...
			if (HasTravelDates)
			{			
				foreach (TravelDate date in travelDates)
				{
					//add tickets that meet the criteria to array list 
					if ((date.TravelMode == mode)&& (date.TicketType == TicketType.Singles) 
						&& (TDDateTime.AreSameDate(date.OutwardDate, outwardDate))
						&& (TDDateTime.AreSameDate(date.ReturnDate, inwardDate)))					
					{
						for (int i = 0; i < date.InwardTickets.Length; i++)
						{
							arrayListTickets.Add(date.InwardTickets[i]);							
						}					
					}				
				}	
			
				//convert array list to an array of CostSearchTickets
				CostSearchTicket[] inwardTickets = (CostSearchTicket[])arrayListTickets.ToArray(typeof(CostSearchTicket));	
				//return the tickets
				return inwardTickets;	
			}
				//otherwise return an empty array
			else
			{
				return emptyTicketArray;
			}							
		}

		/// <summary>
		/// Returns array of Return tickets for the dates and travel mode supplied
		/// </summary>
		/// <param name="outwardDate">outward TDDateTime</param>
		/// <param name="inwardDate">inward TDDateTime</param>
		/// <param name="mode">An enum value - Rail, Coach or Air</param>
		/// <returns>a CostSearchTicket array</returns>
		public CostSearchTicket[] GetReturnTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode)
		{
			//used to add tickets - set to initial size of 32. 
			//This should be more than enough to prevent inefficient redoubling of array list size
			ArrayList arrayListTickets = new ArrayList(32);					

			//if travel dates exist...
			if (HasTravelDates)
			{
				//add tickets that meet the criteria to array list 
				foreach (TravelDate date in travelDates)
				{
					if ((date.TravelMode == mode)&& (date.TicketType == TicketType.Return || date.TicketType == TicketType.OpenReturn) 
						&& (TDDateTime.AreSameDate(outwardDate, date.OutwardDate))
						&& (TDDateTime.AreSameDate(inwardDate, date.ReturnDate)))					
					{
						for (int i = 0; i < date.ReturnTickets.Length; i++)
						{
							arrayListTickets.Add(date.ReturnTickets[i]);							
						}					
					}				
				}	
			
				//convert array list to an array of CostSearchTickets
				CostSearchTicket[] returnTickets = (CostSearchTicket[])arrayListTickets.ToArray(typeof(CostSearchTicket));	
				//return the tickets
				return returnTickets;	
			}
				//otherwise return an empty array
			else
			{
				return emptyTicketArray;
			}							
		}

		/// <summary>
		/// Returns array of OpenReturn tickets for the dates and travel mode supplied
		/// </summary>
		/// <param name="outwardDate">outward TDDateTime</param>		
		/// <param name="mode">An enum value - Rail, Coach or Air</param>
		/// <returns>a CostSearchTicket array</returns>
		public CostSearchTicket[] GetOpenReturnTickets(TDDateTime outwardDate, TicketTravelMode mode)
		{
			//used to add tickets - set to initial size of 32. 
			//This should be more than enough to prevent inefficient redoubling of array list size
			ArrayList arrayListTickets = new ArrayList(32);					

			//if travel dates exist...
			if (HasTravelDates)
			{
				//add in all open return tickets matching the criteria
				foreach (TravelDate date in travelDates)
				{
					if ((date.TravelMode == mode)&& (date.TicketType == TicketType.OpenReturn) &&
						TDDateTime.AreSameDate(date.OutwardDate, outwardDate)) 
					{
						for (int i = 0; i < date.OutwardTickets.Length; i++)
						{
							arrayListTickets.Add(date.OutwardTickets[i]);							
						}					
					}				
				}		
			
				//convert array list to an array of CostSearchTickets
				CostSearchTicket[] openReturnTickets = (CostSearchTicket[])arrayListTickets.ToArray(typeof(CostSearchTicket));
			
				//return tickets
				return openReturnTickets;					
			}
			else
			{
				return emptyTicketArray;
			}
				
		}

		/// <summary>
		/// Returns the TDJourneyResult associated with a ticket
		/// </summary>
		/// <param name="ticket">a CostSearchTicket</param>		
		/// <returns>the TDJourneyResult for the ticket taht was passed in</returns>
		public ITDJourneyResult GetJourneyResultsForTicket(CostSearchTicket ticket)
		{
			if (ticket.JourneysForTicket.CostJourneyResult != null)
			{ 
				return ticket.JourneysForTicket.CostJourneyResult;
			}
			else
			{
				return new TDJourneyResult();
			}					
		}

		/// <summary>
		/// Overloaded version of GetJourneyResultsForTicket that returns a combined TDJourneyResult 
		/// based on two Singles tickets
		/// </summary>
		/// <param name="outTicket">the outward CostSearchTicket</param>		
		/// <param name="inTicket">the inward CostSearchTicket</param>		
		/// <returns>a combined TDJourneyResult for the ticket that was passed in</returns>
		public ITDJourneyResult GetJourneyResultsForTicket(CostSearchTicket outTicket, CostSearchTicket inTicket )
		{
			//create a combined TDJourneyResult based on both outbound and inbound tickets
			TDJourneyResult result = new TDJourneyResult();				

			//get OUTWARD journeys from outbound ticket 
			foreach (PublicJourney pj in outTicket.JourneysForTicket.CostJourneyResult.OutwardPublicJourneys) 
			{
				result.OutwardPublicJourneys.Add(pj);
			}

			//get OUTWARD journeys from inbound ticket and set these to be return journeys for the combined Journey Result
			foreach (PublicJourney pj in inTicket.JourneysForTicket.CostJourneyResult.OutwardPublicJourneys) 
			{
				result.ReturnPublicJourneys.Add(pj);
			}

			//add CJPMessages from both tickets			
			foreach (CJPMessage message in outTicket.JourneysForTicket.CostJourneyResult.CJPMessages)
			{
				result.AddMessageToArray(message.MessageText, message.MessageResourceId, message.MajorMessageNumber, message.MinorMessageNumber);
				
			}
			foreach (CJPMessage message in inTicket.JourneysForTicket.CostJourneyResult.CJPMessages)
			{
				result.AddMessageToArray(message.MessageText, message.MessageResourceId, message.MajorMessageNumber, message.MinorMessageNumber);
			}			
			
			//set IsValid property
			result.IsValid = (outTicket.JourneysForTicket.CostJourneyResult.IsValid) &&
				(inTicket.JourneysForTicket.CostJourneyResult.IsValid);			

			//return the combined TDJourneyResult
			return result;
		}

		/// <summary>
		/// Returns the TDJourneyRequest associated with a ticket
		/// </summary>
		/// <param name="ticket">a CostSearchTicket</param>		
		/// <returns>the TDJourneyRequest for the ticket that was passed in</returns>
		public ITDJourneyRequest GetJourneyRequestForTicket(CostSearchTicket ticket)
		{
			if (ticket.JourneysForTicket.CostJourneyRequest != null)
			{ 
				return ticket.JourneysForTicket.CostJourneyRequest;
			}
			else
			{
				return new TDJourneyRequest();
			}							
		}		

		/// <summary>
		/// Overloaded version that creates a combined TDJourneyRequest based on the two Singles tickets
		/// </summary>
		/// <param name="outTicket">the outward CostSearchTicket</param>	
		/// <param name="inTicket">the inward CostSearchTicket</param>	
		/// <returns>a combined TDJourneyRequest for the tickets that were passed in</returns>
		public ITDJourneyRequest GetJourneyRequestForTicket(CostSearchTicket outTicket, CostSearchTicket inTicket)
		{
			//create one combined journey request from the outbound and inbound tickets
			TDJourneyRequest request = new TDJourneyRequest();	

			//populate it based on the out ticket
			request = outTicket.JourneysForTicket.CostJourneyRequest;

			//add details from the in ticket
			request.IsReturnRequired = true;
			request.ReturnAnyTime = inTicket.JourneysForTicket.CostJourneyRequest.OutwardAnyTime;
			request.ReturnArriveBefore = inTicket.JourneysForTicket.CostJourneyRequest.OutwardArriveBefore;
			request.ReturnDateTime = inTicket.JourneysForTicket.CostJourneyRequest.OutwardDateTime;
			request.ReturnDestinationLocation = inTicket.JourneysForTicket.CostJourneyRequest.DestinationLocation;
			request.ReturnOriginLocation = inTicket.JourneysForTicket.CostJourneyRequest.OriginLocation;
			
			//return the request
			return request;				
		}		
		
		/// <summary>
		/// Returns a particular TravelDate matching the supplied parameters
		/// </summary>		
		/// <param name="outwardDate">a TDDateTime</param>
		/// <param name="travelMode">An enum value - Rail, Coach or Air</param>
		/// <param name="ticketType">An enum value - Single, OpenReturn, Singles or Return</param>
		/// <returns>a TravelDate that matches all the supplied parameters</returns>
		public TravelDate GetTravelDate(TDDateTime outwardDate, TicketTravelMode travelMode, TicketType ticketType)
		{
			//loop through travel dates associated with this CostSearchResult
			foreach (TravelDate i in travelDates)
			{
				//find travel date that matches the supplied parameters
				if ((i.TravelMode == travelMode) && (i.TicketType == ticketType) &&
					(TDDateTime.AreSameDate(i.OutwardDate,outwardDate )))					
				{
					return i;
				}
			}	

			//if no match found return null				
			return null;	
		}

		/// <summary>
		/// Returns a particular TravelDate matching the supplied parameters
		/// </summary>
		/// <param name="outwardDate">the outward TDDateTime</param>
		/// <param name="returnDate">the return TDDateTime</param>
		/// <param name="travelMode">An enum value - Rail, Coach or Air</param>
		/// <param name="ticketType">An enum value - Single, OpenReturn, Singles or Return</param>
		/// <returns>a TravelDate that matches all the supplied parameters</returns>
		public TravelDate GetTravelDate(TDDateTime outwardDate, TDDateTime returnDate, TicketTravelMode travelMode, TicketType ticketType )
		{
			//loop through travel dates associated with this CostSearchResult

			foreach (TravelDate i in travelDates)
			{
				//find travel date that matches the supplied parameters
				if ((i.TravelMode == travelMode) && (i.TicketType == ticketType) &&					
					(TDDateTime.AreSameDate(i.OutwardDate,outwardDate )) && 
					(TDDateTime.AreSameDate(i.ReturnDate, returnDate )))
				{
					return i;
				}
			}	

			//if no match found return null				
			return null;	
		}

		/// <summary>
		/// Returns any errors that occurred during the call that produced this CostSearchResult
		/// </summary>
		/// <returns>a CostSearchError array</returns>
		public CostSearchError[] GetErrors()
		{
			return (CostSearchError[])(searchErrors.ToArray(typeof(CostSearchError)));				
		}

		/// <summary>
		/// Clears any errors 
		/// </summary>
		public void ClearErrors()
		{
			searchErrors.Clear();
		}

		/// <summary>
		/// Adds an error to the array of CostSearchErrors associated with this CostSearchResult
		/// </summary>
		/// <param name="newError">a CostSearchError to add</param>
		public void AddError(CostSearchError newError)
		{
			foreach (CostSearchError error in searchErrors)
			{
				if	(error.ResourceID.Equals(newError.ResourceID))
				{
					return;
				}
			}

			searchErrors.Add(newError);
		}

		/// <summary>
		/// Method that returns all TravelDates included in this CostSearchResult
		/// </summary>
		/// <returns>an array of TravelDates</returns>
		public TravelDate[] GetAllTravelDates()
		{
			if (HasTravelDates)
			{
				return travelDates;				
			}
			else
			{
				//if no errors, return an empty array
				return new TravelDate[0];				
			};
		}

		#endregion 

		#region properties
		
		/// <summary>
		/// Writeable property that allows travelDates to be set. To read travelDates a method is used 
		/// instead of a property because returning an array via a method is more efficient
		/// </summary>
		/// <returns>the array of TravelDates for this CostSearchResult</returns>
		public TravelDate[] TravelDates
		{			
			set
			{
				travelDates = value;
			}
		}		

		/// <summary>
		/// Read/Write Property. Gets/Sets requestId
		/// </summary>
		/// <returns>the Guid resultId for this CostSearchResult</returns>
		public Guid ResultId
		{
			get
			{

				return resultId;	
			}
			set
			{
				resultId = value;

			}
		}

		/// <summary>
		/// Read only property available to check that this results set has some travel dates 
		/// </summary>
		/// <returns>a bool indicating if this CostSearchResult has any TravelDates</returns>
		public bool HasTravelDates
		{
			get
			{
				return ((travelDates != null) && (travelDates.Length > 0));
			}
		}

		#endregion


	}
}
