// ***********************************************
// NAME 		: IJourneyPlanner.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This interface specifies methods for performing journey planning asynchronously
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/IJourneyPlanner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:30   mturner
//Initial revision.
//
//   Rev 1.0   Jan 11 2006 13:39:02   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;

namespace TransportDirect.UserPortal.JourneyPlannerService
{
	/// <summary>
	/// This interface specifies methods for performing journey planning asynchronously
	/// </summary>
	public interface IJourneyPlanner
	{

		 void PlanPublicJourney(ExposedServiceContext context, PublicJourneyRequest request);

	}

}
