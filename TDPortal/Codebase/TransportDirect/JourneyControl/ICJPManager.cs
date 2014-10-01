// *********************************************** 
// NAME			: CJPManager.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Definition of the CJPManager interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ICJPManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:40   mturner
//Initial revision.
//
//   Rev 1.5   Sep 21 2004 17:50:26   COwczarek
//CallCJP method now takes isExtension parameter that
//indicates if journey request is for planning a journey extension.
//Resolution for 1263: Unhelpful user friendly error message for extend results
//
//   Rev 1.4   Jul 02 2004 13:39:14   jgeorge
//Changes for user type
//
//   Rev 1.3   Sep 08 2003 15:45:22   RPhilpott
//Spli initial and amendment CallCJP calls into separate overloaded versions.
//
//   Rev 1.2   Aug 20 2003 17:55:44   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Interface for the CJPManager class.
	/// </summary>
	public interface ICJPManager
	{
		/// <summary>
		/// CallCJP handles the orchestration of the various calls to the Journey planner.
		/// This overloaded version handles initial requests.
		/// </summary>
		/// <param name="request">Encapsulates journey parameters</param>
		/// <param name="sessionID">Used for logging purposes only</param>
		/// <param name="userType">Used for logging purposes</param>
		/// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
		/// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
		/// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="isExtension">True if the request is for a journey extension, false otherwise</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
		
		ITDJourneyResult CallCJP( ITDJourneyRequest	request,
									string sessionId,
									int userType,
									bool referenceTransaction,
									bool loggedOn,
									string language,
                                    bool isExtension);

		/// <summary>
		/// CallCJP handles the orchestration of the various calls to the Journey planner
		/// This overloaded version handles amendments to an existing journey.
		/// </summary>
		/// <param name="request">Encapsulates journey parameters</param>
		/// <param name="sessionID">Used for logging purposes only</param>
		/// <param name="userType">Used for logging purposes</param>
		/// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
		/// <param name="referenceNumber">Returned by the initial enquiry</param>
		/// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
		/// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
		/// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
		/// <returns>The results of the enquiry, including any error messages</returns>

		ITDJourneyResult CallCJP( ITDJourneyRequest	request,
										string sessionId,
										int userType,
										bool referenceTransaction,
										int referenceNumber,
										int lastSequenceNumber,
										bool loggedOn,
										string language);
	}
}
