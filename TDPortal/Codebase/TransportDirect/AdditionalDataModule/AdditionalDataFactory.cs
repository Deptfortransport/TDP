// *********************************************** 
// NAME                 : AdditionalDataFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2003-09-20 
// DESCRIPTION			: Implementation of AdditionalDataFactory
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/AdditionalDataFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:06   mturner
//Initial revision.
//
//   Rev 1.0   Oct 16 2003 20:52:36   acaunt
//Initial Revision

using System;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Factory for registering TDAdditionalData with Service Discovery
	/// </summary>
	[Serializable()]
	public class AdditionalDataFactory : IServiceFactory
	{
		private IAdditionalData currentInstance; 

		/// <summary>
		/// Constructor - create the singleton instance.
		/// </summary>
		public AdditionalDataFactory()
		{
			currentInstance = new TDAdditionalData();
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the TDAdditionalData class.
		/// </summary>
		/// <returns>A new instance of a TDAdditionalData.</returns>
		public Object Get()
		{
			return currentInstance;
		}


	}
}
