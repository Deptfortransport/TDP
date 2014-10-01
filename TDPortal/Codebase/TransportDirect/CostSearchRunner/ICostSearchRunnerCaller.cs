// ***********************************************
// NAME 		: ICostSearchRunnerCaller.cs
// AUTHOR 		: Russell Wilby
// DATE CREATED : 13/10/2005
// DESCRIPTION 	: Definition of the ICostSearchRunnerCaller Interface
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/ICostSearchRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:38   mturner
//Initial revision.
//
//   Rev 1.0   Oct 17 2005 13:38:06   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// ICostSearchRunnerCaller interface
	/// </summary>
	public interface ICostSearchRunnerCaller
	{
		/// <summary>
		/// Makes the call to the CostSearchFacade to look up services using the selected ticket(s)
		/// </summary>
		void CallAssembleServices(CJPSessionInfo sessionInfo);

		/// <summary>
		/// Asynchronously makes the call to the CostSearchFacade to look up services using the selected ticket(s)
		/// </summary>
		void CallAssembleServicesAsync(CJPSessionInfo sessionInfo);

		/// <summary>
		/// Makes the call to the CostSearchFacade to look up fares
		///</summary>
		void CallAssembleFares(CJPSessionInfo sessionInfo);

		/// <summary>
		/// Asynchronously makes the call to the CostSearchFacade to look up fares
		///</summary>
		void CallAssembleFaresAsync (CJPSessionInfo sessionInfo);

	}
}
