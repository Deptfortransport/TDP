//********************************************************************************
//NAME         : TestCoachFaresInterfaceInitialisation.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of TestCoachFaresInterfaceInitialisation class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestCoachFaresInterfaceInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:18   mturner
//Initial revision.
//
//   Rev 1.8   Nov 01 2005 11:22:56   mguney
//Email custom logging removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 27 2005 14:52:36   mguney
//Mock version of the ICoachOperatorLookup is used.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 25 2005 15:43:56   mguney
//CoachOperatorLookup initialisation added.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 25 2005 15:24:40   mguney
//IEventPublisher initialisation included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 24 2005 15:53:38   mguney
//Initialised the TestCoachFaresInterfaceFactory instead of the real CoachFaresInterfaceFactory.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 22 2005 15:53:08   mguney
//Factories returned according to the operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 15:05:00   mguney
//Creation dare corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 12:27:00   mguney
//SCR associated
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 12:25:18   mguney
//Initial revision.

using System;
using System.Collections;
using System.Text;
using System.Diagnostics;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.CoachFares;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// TestCoachFaresInterfaceInitialisation class implementation.
	/// </summary>
	public class TestCoachFaresInterfaceInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Constructor initialisation.
		/// </summary>
		public TestCoachFaresInterfaceInitialisation()
		{			
		}

		/// <summary>
		/// Populate method of the interface.
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{			
			// Enable PropertyService					
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			ArrayList errors = new ArrayList();
			
			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));				
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(tdEx.Message);			

				// rethrow exception - use the initial exception id as the id
				throw new Exception(message.ToString());
			}

			// Add Mock Factory for Coach Fare Interfaces
			serviceCache.Add(ServiceDiscoveryKey.CoachFaresInterface,new TestCoachFaresInterfaceFactory());	
		
			//Enable Coach Operator Lookup
			serviceCache.Add(ServiceDiscoveryKey.CoachOperatorLookup, new TestMockCoachOperatorLookup());


		}
	}
}
