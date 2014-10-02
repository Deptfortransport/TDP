// *********************************************** 
// NAME                 : IPartnerCatalogue.cs 
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 27/09/2005
// DESCRIPTION  : Class interdace for Partnercatalogue
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/IPartnerCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:42   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:15:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.1   Feb 20 2006 15:36:40   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.3.1.0   Nov 25 2005 18:08:40   schand
//Added  additional method string GetClearPassword(string username) to the interface
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Oct 18 2005 17:53:08   mdambrine
//The resourcefile is build with the displayname and not the hostname
//
//   Rev 1.2   Oct 13 2005 16:17:30   COwczarek
//Add GetPartnerChannel
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2869: Del8 White Labelling - Request URL Validation
//
//   Rev 1.1   Oct 11 2005 11:58:06   mdambrine
//Added partnerdisplayname
//
//   Rev 1.0   Sep 27 2005 16:43:56   mdambrine
//Initial revision.
//

using System;
using System.Data;
using System.Data.SqlClient;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Summary description for IPartnerCatalogue.
	/// </summary>
	[CLSCompliant(false)]
	public interface IPartnerCatalogue
	{
		
		/// <summary>
		/// Takes a hostname and returns its associated partner id
		/// </summary>
		/// <param name="hostName">hostname of partner</param>
		/// <returns>partner id as integer</returns>
		int GetPartnerIdFromHostName(string hostName);

		/// <summary>
		/// returns the partner name from the supplied partner id
		/// </summary>
		/// <param name="partnerId">Partner Id from the partner</param>
		/// <returns>Returns the partner name as string</returns>
		string GetPartnerHostName(int partnerId);

		/// <summary>
		/// returns the partner display name from the supplied partner id
		/// </summary>
		/// <param name="partnerId">Partner Id from the partner</param>
		/// <returns>Returns the partner display name as string</returns>
		string GetPartnerDisplayName(int partnerId);

        /// <summary>
        /// returns the top level MCMS channel name for the supplied partner id
        /// </summary>
        /// <param name="partnerId">Partner Id from the partner</param>
        /// <returns>Returns top level MCMS channel name as string</returns>
        string GetPartnerChannel(int partnerId);

        /// <summary>
		/// Checks that the MCMS channel is valid for the partnerid
		/// </summary>
		/// <param name="partnerId"></param>
		/// <param name="channel"></param>
		/// <returns></returns>
		bool ValidateRequestUrl(int partnerId, string channel);

		/// <summary>
		/// This function returns the password from the given username.
		/// </summary>
		/// <param name="username">username of the partner</param>
		/// <returns>If found, it send password in clear text else exception will be thrown</returns>
		string GetClearPassword(string username);

		/// <summary>
		/// This method will return the IsAllowedService on a specific partner object
		/// </summary>
		/// <param name="hostName">the Hostname of the partner in the partner table</param>
		/// <param name="serviceName">the service name of the service to check the access on</param>
		/// <returns></returns>
		bool IsPartnerAllowedToService(string hostName, string serviceName);

	}
}
