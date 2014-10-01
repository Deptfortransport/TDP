// *********************************************** 
// NAME         : CostBasedServicesSearchState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/CostBasedServicesSearchState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:34   mturner
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:04:18   jgeorge
//Initial revision.
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// Class used to control the asynchronous calls for cost based journeys lookup
	/// </summary>
	[Serializable]
	public class CostBasedServicesSearchState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CostBasedServicesSearchState() : base( int.Parse( Properties.Current[JourneyControlConstants.WaitPageRefreshSeconds] ),
			int.Parse(Properties.Current[JourneyControlConstants.WaitPageTimeoutSeconds] ) )
		{ }

		/// <summary>
		/// Constructor allowing timeout and refresh interval to be supplied
		/// </summary>
		/// <param name="waitPageRefreshInterval">Number of seconds after which the wait page should refresh</param>
		/// <param name="waitPageTimeout">Number of seconds after which the wait page should invoke the timeout processing</param>
		public CostBasedServicesSearchState(int waitPageRefreshInterval, int waitPageTimeout) : base(waitPageRefreshInterval, waitPageTimeout)
		{ }

		/// <summary>
		/// Constructor allowing pageids to be supplied
		/// </summary>
		/// <param name="ambiguityPage">Page to transfer to when the status is ValidationError</param>
		/// <param name="destinationPage">Page to transfer to when the status is CompletedOK</param>
		/// <param name="errorPage">Page to transfer to when the status is anything else</param>
		/// <param name="waitPageRefreshInterval">Number of seconds after which the wait page should refresh</param>
		/// <param name="waitPageTimeout">Number of seconds after which the wait page should invoke the timeout processing</param>
		public CostBasedServicesSearchState(PageId ambiguityPage, PageId destinationPage, PageId errorPage, int waitPageRefreshInterval, int waitPageTimeout) : base(ambiguityPage, destinationPage, errorPage, waitPageRefreshInterval, waitPageTimeout)
		{ }

		/// <summary>
		/// Implementation of the ProcessState method. Does any processing required for the current state
		/// of the object prior to redirection to the page specified in the return value.
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		public override TransportDirect.Common.PageId ProcessState()
		{
			switch (this.Status)
			{
				case AsyncCallStatus.CompletedOK:
					return DoCompletedOkProcessing();

				case AsyncCallStatus.TimedOut:
					return DoTimeoutProcessing();

				case AsyncCallStatus.ValidationError:
					return this.AmbiguityPage;

				default:
					return PageId.WaitPage;
			}
		}

		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.TimedOut
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoTimeoutProcessing()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			if (sessionManager.JourneyResult == null)
				sessionManager.JourneyResult = new TDJourneyResult();

			// Update the result to include an error message.
			sessionManager.JourneyResult.AddMessageToArray(
				string.Empty,
				JourneyControlConstants.CJPInternalError, 
				JourneyControlConstants.CjpCallError,
				0);		

			return this.DestinationPage;
		}

		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.CompletedOK
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoCompletedOkProcessing()
		{
			PageId returnPageId;
			
			ITDSessionManager sessionManager = TDSessionManager.Current;
			ITDJourneyResult result = sessionManager.JourneyResult;

			FindCostBasedPageState pageState = (FindCostBasedPageState)sessionManager.FindPageState;
			CostSearchError[] errors = pageState.SearchResult.GetErrors();

			bool messagesPresent = (errors != null && errors.Length > 0) && result != null && result.CJPMessages.Length > 0;
			bool nojourneysFound = false;
								
			if (result != null)
			{
				if (pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.OpenReturn)
					nojourneysFound = result.OutwardPublicJourneyCount == 0;	
				else
					nojourneysFound = result.OutwardPublicJourneyCount == 0 || (sessionManager.JourneyParameters.IsReturnRequired && result.ReturnPublicJourneyCount == 0);
			}
								

			if (!messagesPresent && nojourneysFound) 
			{
				returnPageId = this.ErrorPage;
				pageState.SearchResult.AddError(new CostSearchError("FindFareTicketSelection.messages.Text"));
			} 
			else 
			{
				returnPageId = this.DestinationPage;

				// Set flag to indicate that we are going to the destination page from
				// the wait page.
				sessionManager.SetOneUseKey(SessionKey.FirstViewingOfResults, "yes");
			}
			
			return returnPageId;
		}

	}
}
