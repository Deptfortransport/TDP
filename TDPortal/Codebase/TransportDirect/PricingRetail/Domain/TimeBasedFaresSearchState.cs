// *********************************************** 
// NAME			: TimeBasedFaresSearchState.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 20/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TimeBasedFaresSearchState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:58   mturner
//Initial revision.
//
//   Rev 1.3   Apr 05 2006 15:42:40   build
//Automatically merged from branch for stream0030
//
//   Rev 1.2.1.0   Mar 29 2006 11:10:32   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.2   Jan 18 2006 18:16:34   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.1   Nov 07 2005 11:19:26   RPhilpott
//Use new properties for time-out values.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 18:14:54   jgeorge
//Initial revision.

using System;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{

	/// <summary>
	/// Concrete subclass of TransportDirect.Common.AsyncCallState used to control the asynchronous 
	/// call for time based fare lookups.
	/// </summary>
	[Serializable]
	public class TimeBasedFaresSearchState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TimeBasedFaresSearchState() : base( int.Parse(Properties.Current["JourneyControl.PriceJourney.WaitPageTimeoutSeconds"], CultureInfo.InvariantCulture ) )
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
					return this.DestinationPage;

				case AsyncCallStatus.TimedOut:
					return this.ErrorPage;

				case AsyncCallStatus.NoResults:
					return this.ErrorPage;

				case AsyncCallStatus.None:
				
					// This situation should never happen, but because of the way the wait page
					// works if it does then the user will never get beyond the wait page.
					// To avoid this, change the status to TimedOut if we encounter it.
					this.Status = AsyncCallStatus.TimedOut;
					return this.ErrorPage;
				
				default:
					return PageId.WaitPage;
			}
		}
	}
}
