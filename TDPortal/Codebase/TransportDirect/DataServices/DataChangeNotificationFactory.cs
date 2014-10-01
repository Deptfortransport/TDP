// ***********************************************
// NAME 		: DataChangeNotificationService.cs
// AUTHOR 		: Rob Greenwood
// DATE CREATED : 10-Jun-2004
// DESCRIPTION 	: Factory Class for 
// DataChangeNotification service.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/DataChangeNotificationFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:44   mturner
//Initial revision.
//
//   Rev 1.2   Jun 15 2004 14:56:52   CHosegood
//Added code documentaiton
//
//   Rev 1.1   Jun 11 2004 14:40:32   rgreenwood
//Added Header information

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;



namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Factory class for the DataServices component
	/// </summary>
	public class DataChangeNotificationFactory : IServiceFactory
	{
        /// <summary>
        /// 
        /// </summary>
		private DataChangeNotification current;

        /// <summary>
        /// 
        /// </summary>
		public DataChangeNotificationFactory()
		{
			current = new DataChangeNotification();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public object Get()
		{
			return current;
		}
	}
}
