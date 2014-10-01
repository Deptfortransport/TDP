// ************************************************************** 
// NAME			: CostSearchRunnerCallerFactory.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 13/10/2005 
// DESCRIPTION	: CostSearchRunnerCallerFactory factory class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/CostSearchRunnerCallerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:36   mturner
//Initial revision.
//
//   Rev 1.0   Oct 17 2005 13:37:58   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// Factory class that returns a new instance of CostSearchRunnerCaller.
	/// </summary>
	[Serializable()]
	public class CostSearchRunnerCallerFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor 
		/// </summary>
		public CostSearchRunnerCallerFactory()
		{
			
		}				
		/// <summary>
		///  Method used by ServiceDiscovery to get a new instance of the CostSearchRunnerCaller class.
		/// </summary>
		/// <returns>A new instance of a CostSearchRunnerCaller.</returns>
		public Object Get()
		{
			return new CostSearchRunnerCaller();
		}
	}
}
