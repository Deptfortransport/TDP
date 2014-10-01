// ************************************************************** 
// NAME			: CostSearchRunnerFactory.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 11/03/2005 
// DESCRIPTION	: Definition of the CostSearchRunnerFactory class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/CostSearchRunnerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:36   mturner
//Initial revision.
//
//   Rev 1.1   Mar 15 2005 13:38:22   jmorrissey
//Changed the Get method to return a new CostSearchRunner each time, rather than a singleton instance
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.0   Mar 11 2005 09:32:26   jmorrissey
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// Factory class that returns a new instance of CostSearchRunner.
	/// </summary>
	[Serializable()]
	public class CostSearchRunnerFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor 
		/// </summary>
		public CostSearchRunnerFactory()
		{
			
		}				

		/// <summary>
		///  Method used by ServiceDiscovery to get a new instance of the CostSearchRunner class.
		/// </summary>
		/// <returns>A new instance of a CostSearchRunner.</returns>
		public Object Get()
		{
			return new CostSearchRunner();
		}
	}
}
