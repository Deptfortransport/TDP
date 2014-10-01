// ************************************************************** 
// NAME			: ICostSearchFacade.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the ICostSearchFacade interface
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/ICostSearchFacade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:18   mturner
//Initial revision.
//
//   Rev 1.6   Nov 09 2005 12:23:46   build
//Automatically merged from branch for stream2818
//
//   Rev 1.5.1.0   Oct 28 2005 15:29:32   RWilby
//Refactored interface for TD096 -Search by Price
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   May 06 2005 16:22:42   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.4   May 06 2005 15:32:38   jmorrissey
//Actioned code review comments.
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.3   Apr 25 2005 17:50:26   jmorrissey
//Update to AssembleFares method signature.
//
//   Rev 1.2   Mar 22 2005 10:55:48   jmorrissey
//Added overload of AssembleServices method that takes an outward and inward ticket.
//
//   Rev 1.1   Feb 22 2005 16:47:36   jmorrissey
//Update to AssembleServices signature
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// This ICostSearchFacade interface is implemented by the CostSearchFacade and 
	/// TestMockCostSearchFacade classes in the CostSearch project
	/// </summary>
	public interface ICostSearchFacade
	{
		//Looks up fares based on a CostSearchRequest. It returns a CostSearchResult which contains an array of travel dates.
		ICostSearchResult AssembleFares(ICostSearchRequest request);

		//Used by non-coach modes to look up journey services for the selected ticket
		ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket);
		
		//Used by non-coach modes to look up journey services for the two selected single tickets
		ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket);
	}
}
