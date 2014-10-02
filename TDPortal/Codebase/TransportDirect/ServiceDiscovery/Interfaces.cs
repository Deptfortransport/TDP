// *********************************************** 
// NAME                 :  Interface.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 10/07/2003 
// DESCRIPTION  : Interfaces involved in the Service Discovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ServiceDiscovery/Interfaces.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:12   mturner
//Initial revision.
//
//   Rev 1.1   Jul 17 2003 11:17:10   passuied
//Changes after code review

using System;
using System.Collections;

namespace TransportDirect.Common.ServiceDiscovery
{
	/// <summary>
	/// Interface for the service factory classes.
	/// </summary>
	public interface IServiceFactory
	{
		object Get();
	}

	/// <summary>
	/// Interface for the Service Initialisation classes
	/// </summary>
	public interface IServiceInitialisation
	{
		void Populate ( Hashtable serviceCache);
	}
}
