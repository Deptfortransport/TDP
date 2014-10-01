// ***********************************************
// NAME 		: IJourneyPlannerSynchronous.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This interface specifies methods for performing journey planning synchronously
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/IJourneyPlannerSynchronous.cs-arc  $
//
//   Rev 1.3   Sep 29 2010 11:27:44   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.2   Sep 08 2009 13:29:18   mmodi
//Updated for a max number of journeys in car request
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.1   Aug 04 2009 13:59:54   mmodi
//New method for PlanPrivateJourney
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:24:30   mturner
//Initial revision.
//
//   Rev 1.0   Jan 11 2006 13:39:02   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	/// <summary>
	/// This interface specifies methods for performing journey planning synchronously
	/// </summary>
	public interface IJourneyPlannerSynchronous
	{
        PublicJourneyResult PlanPublicJourney(ExposedServiceContext context, PublicJourneyRequest request);

        CarJourneyResult PlanPrivateJourney(ExposedServiceContext context, CarJourneyRequest request, int maxNumberOfJourneys);

        CycleJourneyResult PlanCycleJourney(ExposedServiceContext context, CycleJourneyRequest request);

        GradientProfileResult GetGradientProfile(ExposedServiceContext context, GradientProfileRequest request);
	}

} 
