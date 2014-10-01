// *********************************************** 
// NAME                 : RDHandlerFactory.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 17/01/2005 
// DESCRIPTION  : Service Factory implemenztion for RDHandler
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RDHandlerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:38   mturner
//Initial revision.
//
//   Rev 1.1   Feb 20 2006 15:40:18   build
//Automatically merged from branch for stream0017
//
//   Rev 1.0.1.0   Jan 30 2006 11:08:26   tolomolaiye
//Changes for RTTI Internal Event
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.1   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added
//
//   Rev 1.0   Jan 17 2005 11:38:00   schand
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Service Factory implemenztion for RDHandler.
	/// </summary>
	public class RDHandlerFactory: IServiceFactory
	{	 
		/// <summary>
		/// Empty constructor class to allow instantiation
		/// </summary>
		public RDHandlerFactory()
		{}

		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of an IRDHandler.
		/// </summary>
		/// <returns>A new RDHandler</returns>
		public Object Get()
		{
            // Determine if the RDHandler (for using NRE EnquiryPorts socket based connection)
            // or the LDBHandler (for using NRE LiveDepartureBoards web service based connection)
            // for making rail station board enquiries
            bool useLDBWebService = false;

            if (!bool.TryParse(Properties.Current["DepartureBoardService.RTTIManager.UseLDBWebService.Switch"], out useLDBWebService))
            {
                useLDBWebService = false;
            }

            if (useLDBWebService)
                return new LDBHandler();
            else
                return new RDHandler();
		}
	}
}