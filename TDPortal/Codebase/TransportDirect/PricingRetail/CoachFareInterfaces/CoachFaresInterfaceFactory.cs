//********************************************************************************
//NAME         : CoachFaresInterfaceFactory.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of CoachFaresInterfaceFactory class whcich produces FaresInterfaces.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFaresInterfaceFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:26   mturner
//Initial revision.
//
//   Rev 1.6   Nov 03 2005 12:17:12   RPhilpott
//Make method taking interface type param public.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 27 2005 14:51:12   mguney
//ICoachOperatorLookup is used instead of CoachOperatorLookup
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 25 2005 15:45:02   mguney
//CoachOperatorLookup is being obtained from the service discovery instead of direct initialisation.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 22 2005 15:50:48   mguney
//Factories returned according to the operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:55:30   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:14   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:28   mguney
//Initial revision.

using System;
using System.IO;
using Logger = System.Diagnostics.Trace;
using System.Runtime.Remoting;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFares;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Summary description for CoachFaresInterfaceFactory.
	/// </summary>
	public class CoachFaresInterfaceFactory : IServiceFactory, ICoachFaresInterfaceFactory
	{						
		/// <summary>
		/// Constructor. 
		/// </summary>		
		public CoachFaresInterfaceFactory()
		{
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the CoachFaresInterfaceFactory class.
		/// </summary>
		/// <returns>CoachFaresInterfaceFactory</returns>
		public Object Get()
		{
			return this;
		} 

		/// <summary>
		/// Returns the ICoachFaresInterface according to the operator code.
		/// </summary>
		/// <param name="operatorCode"></param>
		/// <returns>IFaresInterface</returns>
		public IFaresInterface GetFaresInterface(string operatorCode)
		{
			ICoachOperatorLookup coachOperatorLookup = (ICoachOperatorLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			if (coachOperatorLookup.GetOperatorDetails(operatorCode).InterfaceType == CoachFaresInterfaceType.ForRoute)
				return new FaresInterfaceForRoute();			
			else return new FaresInterfaceForJourney();			
		}

		/// <summary>
		/// Returns the ICoachFaresInterface according to the type of interface required.
		/// </summary>
		/// <param name="interfaceType">ForRoute or ForJourney</param>
		/// <returns>IFaresInterface</returns>
		public IFaresInterface GetFaresInterface(CoachFaresInterfaceType interfaceType)
		{
			if (interfaceType == CoachFaresInterfaceType.ForRoute)
				return new FaresInterfaceForRoute();
			else return new FaresInterfaceForJourney();	 
		}
	}
}
