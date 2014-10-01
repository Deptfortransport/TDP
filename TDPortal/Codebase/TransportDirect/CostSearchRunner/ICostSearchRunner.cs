// ************************************************************** 
// NAME			: ICostSearchRunner.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the ICostSearchRunner Interface
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/ICostSearchRunner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:36   mturner
//Initial revision.
//
//   Rev 1.4   Nov 09 2005 12:23:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.3.1.1   Oct 21 2005 18:06:18   jgeorge
//Added using statement
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.0   Oct 14 2005 15:08:14   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Mar 22 2005 10:59:40   jmorrissey
//Added overload of ValidateAndRunServices method.
//
//   Rev 1.2   Mar 15 2005 13:35:52   jmorrissey
//Updated method comments after code review
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.1   Jan 12 2005 10:47:20   jmorrissey
//Changed signature of ValidateAndRunServices method
//
//   Rev 1.0   Dec 22 2004 12:14:14   jmorrissey
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// This ICostSearchRunner interface is implemented by the CostSearchRunner and 
	/// TestMockCostSearchRunner classes in the CostSearchRunner project.
	/// </summary>
	public interface ICostSearchRunner
	{
		//method validates CostSearchParams and looks up fares based on those parameters. It adds the results to the session
		//and returns the search status
		AsyncCallStatus ValidateAndRunFares(CostSearchParams searchParams);

		//method looks up services based on a selected ticket. It adds the results to the session
		//and returns the search staus
		AsyncCallStatus ValidateAndRunServices(CostSearchTicket selectedTicket);

		//overloaded method looks up services based on an outward and an inward ticket. It adds the results to the session
		//and returns the search staus
		AsyncCallStatus ValidateAndRunServices(CostSearchTicket outwardTicket, CostSearchTicket inwardTicket);

	}
	
}