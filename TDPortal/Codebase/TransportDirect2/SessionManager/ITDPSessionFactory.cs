// *********************************************** 
// NAME             : ISessionFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Mar 2011
// DESCRIPTION  	: ITDPSessionFactory interface for the service discovery session factory
// ************************************************
// 

using TDP.Common.ServiceDiscovery;

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// Interface for TDPSessionFactory
    /// </summary>
    public interface ITDPSessionFactory : IServiceFactory
    {
        void Remove();
    }
}
