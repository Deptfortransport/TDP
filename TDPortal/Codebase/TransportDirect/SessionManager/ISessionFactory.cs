// ***********************************************
// NAME 		: ISessionFactory.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 18/08/2003
// DESCRIPTION 	: Interface for the service discovery session factory.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ISessionFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.1   Aug 20 2003 15:50:26   PNorell
//Fixed header
using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager 
{
	/// <summary>
	/// Summary description for ISessionFactory.
	/// </summary>
	public interface ISessionFactory : IServiceFactory
	{
		void Remove();
	}
}
