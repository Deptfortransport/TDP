// *********************************************** 
// NAME         : CostBasedFaresSearchState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostBasedFaresSearchState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:18   mturner
//Initial revision.
//
//   Rev 1.3   Apr 05 2006 15:42:40   build
//Automatically merged from branch for stream0030
//
//   Rev 1.2.1.0   Mar 29 2006 11:10:30   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.2   Nov 07 2005 11:20:10   RPhilpott
//Use new property values for time-out values.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 29 2005 14:08:24   RPhilpott
//Update ctors.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class used to control the asynchronous calls for cost based fares lookup
	/// </summary>
	[Serializable]
	public class CostBasedFaresSearchState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CostBasedFaresSearchState() : base( int.Parse(Properties.Current["FindFare.GetFares.WaitPageTimeoutSeconds"] ) )
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

			return this.ErrorPage;
		}

		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.CompletedOK
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoCompletedOkProcessing()
		{
			// Set flag to indicate that we are going to the destination page from
			// the wait page.
			TDSessionManager.Current.SetOneUseKey(SessionKey.FirstViewingOfResults, "yes");
			
			return this.DestinationPage;
		}
	
	}
}
