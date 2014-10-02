// *********************************************** 
// NAME                 :  ServiceDiscovery.dll
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 10/07/2003 
// DESCRIPTION  :	Implementation of the TDServiceDiscovery class.
//					The Service discovery will:
//						- Ensure services are initialised correctly depending on environment
//						- Enable switching implementation to test services for selected services
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ServiceDiscovery/ServiceDiscovery.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:12   mturner
//Initial revision.
//
//   Rev 1.10   Oct 18 2004 12:10:24   jgeorge
//Added new method to reset ServiceDiscovery for testing purposes.
//
//   Rev 1.9   Oct 03 2003 13:38:42   PNorell
//Updated the new exception identifier.
//
//   Rev 1.8   Sep 16 2003 16:06:40   PNorell
//Put back the order of initialisations as code initialises it outside the initialisations.
//
//   Rev 1.7   Sep 16 2003 12:23:54   passuied
//Added a key + minor change
//
//   Rev 1.6   Sep 10 2003 15:43:16   PNorell
//Added small information on what key was not found in the service discovery.
//
//   Rev 1.5   Jul 25 2003 10:38:10   passuied
//addition of CLSCompliant
//
//   Rev 1.4   Jul 17 2003 11:17:06   passuied
//Changes after code review

using System;
using System.Collections;
using TransportDirect.Common;

[assembly: CLSCompliant(true)]
namespace TransportDirect.Common.ServiceDiscovery
{
	/// <summary>
	/// The Service discovery will:
	///  - Ensure services are initialised correctly depending on environment
	///  - Enable switching implementation to test services for selected services
	/// </summary>
	public class TDServiceDiscovery
	{
		static private TDServiceDiscovery current;
		private Hashtable serviceCache= new Hashtable();

		public TDServiceDiscovery()
		{
			
		}

		/// <summary>
		/// static read-only property that returns the current Service Discovery
		/// </summary>
		static public TDServiceDiscovery Current
		{
			get
			{
				return current;
			}
		}

		/// <summary>
		/// Read only Indexer which returns the object requested against a key
		/// </summary>
			public object this[string key]
		{
			get
			{
				if (!current.serviceCache.Contains(key))
				{
					throw new TDException("Exception trying to access the ServiceDiscovery at a given key. Key ["+key+"] does not exist", false, TDExceptionIdentifier.SDInvalidKey);
				}
				return ((IServiceFactory)current.serviceCache[key]).Get();
			}
		}
		/// <summary>
		/// Static method called at Initialisation of an application to initialise the ServiceDiscovery
		/// </summary>
		/// <param name="initContext"></param>
		static public void Init (IServiceInitialisation initContext)
		{
			if ( current == null)
			{
				current = new TDServiceDiscovery();
				current.Initialise(initContext);
			}
		}

		/// <summary>
		/// Method called to replace a serviceFactory by another one. Used to switch between a test stub and the real system.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="serviceFactory"></param>
		public void SetServiceForTest ( string key, IServiceFactory serviceFactory)
		{

			// if a key does not exist add it
			if (serviceCache.Contains(key))
				serviceCache[key] = serviceFactory;
			else
				serviceCache.Add(key, serviceFactory);
		}

		/// <summary>
		/// Method called to completely clear the service discovery. Used to clear down after a test fixture
		/// which uses the ServiceDiscovery.
		/// </summary>
		public static void ResetServiceDiscoveryForTest()
		{
			current = null;
		}

		protected void Initialise ( IServiceInitialisation initContext)
		{
			initContext.Populate(serviceCache);

		}
	}
}
