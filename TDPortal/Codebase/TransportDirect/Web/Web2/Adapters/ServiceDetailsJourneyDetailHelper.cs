// NAME			: ServiceDetailsJourneyDetailHelper.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-25
// DESCRIPTION	: Helper to get journey detail applicable
//				  to the current ServiceDetails class.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ServiceDetailsJourneyDetailHelper.cs-arc  $
//
//   Rev 1.3   Mar 21 2013 10:13:02   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Mar 31 2008 12:59:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:30   mturner
//Initial revision.
//
//   Rev 1.6   Mar 22 2006 20:27:50   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 14 2006 10:31:36   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.4   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.2.1   Mar 10 2006 11:47:18   NMoorhouse
//Ensure correct journey is selected from front end selected segment
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3.2.0   Feb 23 2006 18:51:06   NMoorhouse
//updated for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3.1.0   Jan 10 2006 15:17:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 23 2005 12:06:12   RPhilpott
//Do not need to send selected itinerary segment to -1 in extension -- this can cause NullReferenceException.
//Resolution for 2677: Del 7.1 - CMS issues with Service Details page.
//
//   Rev 1.2   Aug 16 2005 17:53:34   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 28 2005 12:07:36   RPhilpott
//Unit test fix.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 25 2005 18:05:44   RPhilpott
//Initial revision.
//
//

using System;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;


namespace TransportDirect.UserPortal.Web.Adapters
{	/// <summary>
	/// Summary description for ServiceDetailsJourneyDetailHelper.
	/// </summary>
	public class ServiceDetailsJourneyDetailHelper
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ServiceDetailsJourneyDetailHelper()
		{
		}

        /// <summary>
        /// Gets the PublicJourney relating to the currently 
        /// selected journey from the session data.  
        /// </summary>
        /// <returns>PublicJourney for currently seletced</returns>
        public PublicJourney GetJourney()
        {
            Journey journey = null;

            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

            bool outward = TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward];

            if (outward)
            {
                journey = TDItineraryManager.Current.SelectedOutwardJourney;
            }
            else
            {
                journey = TDItineraryManager.Current.SelectedReturnJourney;
            }

            if (viewState == null)
            {
                return null;
            }
            else if (viewState.SelectedIntermediateItinerarySegment >= 0)
            {
                if (outward)
                {
                    journey = TDItineraryManager.Current.GetOutwardJourney(viewState.SelectedIntermediateItinerarySegment);
                }
                else
                {
                    journey = TDItineraryManager.Current.GetReturnJourney(viewState.SelectedIntermediateItinerarySegment);
                }
            }

            if (journey != null && journey is PublicJourney)
            {
                return (PublicJourney)journey;
            }

            return null;
        }

		/// <summary>
		/// Gets the PublicJourneyDetail relating to the currently 
		/// selected journey leg from the session data.  
		/// </summary>
		/// <returns>PublicJourneyDetail for currently seletced leg</returns>
		public PublicJourneyDetail GetJourneyDetail()
		{
			Journey journey = GetJourney();

			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

			if	(journey != null && viewState != null) 
			{
                if (journey is PublicJourney)
                {
                    return ((PublicJourney)journey).Details[viewState.SelectedJourneyLeg];
                }
                else 
                    return null;
			}
			else
			{
				return null;
			}
		}

	}
}
