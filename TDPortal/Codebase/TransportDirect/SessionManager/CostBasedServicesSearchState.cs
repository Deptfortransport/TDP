// *********************************************** 
// NAME         : CostBasedServicesSearchState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostBasedServicesSearchState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.3   Apr 05 2006 15:42:40   build
//Automatically merged from branch for stream0030
//
//   Rev 1.2.1.0   Mar 29 2006 11:10:28   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.2   Nov 07 2005 11:20:12   RPhilpott
//Use new property values for time-out values.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 29 2005 14:08:26   RPhilpott
//Update ctors.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
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
		public CostBasedServicesSearchState() : base( int.Parse(Properties.Current["FindFare.GetServices.WaitPageTimeoutSeconds"] ) )
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
