// ************************************************************** 
// NAME			: CostSearchFacadeFactory.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 21/02/2005 
// DESCRIPTION	: Definition of the CostSearchFacadeFactory class
// ************************************************************** 
//$Log:

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for CostSearchFacadeFactory.
	/// </summary>
	public class CostSearchFacadeFactory : IServiceFactory
	{
		
		/// <summary>
		/// Constructor
		/// </summary>
		public CostSearchFacadeFactory()
		{
			
		}		

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the CostSearchFacade class.
		/// </summary>
		/// <returns>A new instance of a CostSearchFacade.</returns>
		public Object Get()
		{
			return new CostSearchFacade();
		}
	}
}
