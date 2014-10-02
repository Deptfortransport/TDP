// *********************************************** 
// NAME                 : PageControllerFactory.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 21/07/2003 
// DESCRIPTION  : Factory that allows the
// ServiceDiscovery to create an instance of the
// PageController class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/PageControllerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:48   mturner
//Initial revision.
//
//   Rev 1.3   Apr 23 2004 14:36:40   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.2   Aug 07 2003 13:54:02   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:16   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Factory used by Service Discovery to create a PageController.
	/// </summary>
	public class PageControllerFactory : IServiceFactory
	{
		private IPageController current;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PageControllerFactory()
		{
			IPageTransferDataCache pageTransferDataCache =
				new PageTransferDataCache();
			current = new PageController(pageTransferDataCache);
		}

		/// <summary>
		///  Method used by the ServiceDiscovery to get the
		///  instance of the PageController.
		/// </summary>
		/// <returns>The current instance of the PageController.</returns>
		public Object Get()
		{
			return current;
		}
	}
}
