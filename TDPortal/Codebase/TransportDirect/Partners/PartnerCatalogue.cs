// *********************************************** 
// NAME                 : PartnerCatalogue.cs 
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 27/09/2005
// DESCRIPTION  : Class that encapsulates Partner specific details
// 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/PartnerCatalogue.cs-arc  $
//
//   Rev 1.2   Mar 16 2009 12:24:08   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.0   Jan 26 2009 11:54:50   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Mar 10 2008 15:21:38   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:35:44   mturner
//Initial revision.
//
//   Rev 1.14   Feb 23 2006 19:15:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.13.1.1   Feb 20 2006 15:36:42   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.13.1.0   Nov 25 2005 18:10:26   schand
//Added string GetClearPassword(string username) mthod to this class and code to get Password
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.13   Oct 18 2005 17:53:10   mdambrine
//The resourcefile is build with the displayname and not the hostname
//
//   Rev 1.12   Oct 14 2005 16:42:50   COwczarek
//Allow URL validation to be turned on/off for a partner
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2869: Del8 White Labelling - Request URL Validation
//
//   Rev 1.11   Oct 14 2005 15:54:06   COwczarek
//Apply review comments from CR015
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.10   Oct 13 2005 16:18:14   COwczarek
//Add GetPartnerChannel. Modify ValidateRequestUrl.
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2869: Del8 White Labelling - Request URL Validation
//
//   Rev 1.9   Oct 13 2005 12:15:36   mdambrine
//Added logging of exceptions
//
//   Rev 1.8   Oct 11 2005 11:57:54   mdambrine
//Added partnerdisplayname
//
//   Rev 1.7   Oct 07 2005 11:23:08   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.6   Oct 05 2005 11:09:06   mdambrine
//Changes because of FXCop errors
//
//   Rev 1.5   Oct 04 2005 15:35:08   mdambrine
//Adding the Nunit test classes and references to the project
//
//   Rev 1.4   Oct 03 2005 11:45:30   mdambrine
//Change properties are in uppercase
//
//   Rev 1.3   Oct 03 2005 11:30:32   mdambrine
//Changed the exceptions to to throw a normal tdexception instead of a specific partner/white label error
//
//   Rev 1.2   Sep 30 2005 12:22:30   COwczarek
//Partner object now holds hostname in lower case
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2808: Del8 White Labelling - Page Header & Footer
//
//   Rev 1.1   Sep 28 2005 11:05:08   mdambrine
//Wrong database was called
//
//   Rev 1.0   Sep 27 2005 16:43:56   mdambrine
//Initial revision.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;     


namespace TransportDirect.Partners
{
	/// <summary>
	/// This class should not be used/instanciated directly, only use the copy in the TDServiceDiscovery.
	/// </summary>
	[Serializable()]
	[CLSCompliant(false)]
	[System.Runtime.InteropServices.ComVisible(false)]
	public class  PartnerCatalogue: IPartnerCatalogue
	{
	
		#region Instance variables
			private Hashtable partnersKeyedbyId;
			private Hashtable partnersKeyedByHostName;
		#endregion

		#region Contructor
		/// <summary>
		/// Get the data and store in this class instance
		/// </summary>
		public PartnerCatalogue()
		{
			SqlHelper sqlHelper = new SqlHelper();

			try
			{				
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
							
				// Execute the GetPartners stored procedure. This returns a list of partners
				SqlDataReader reader = sqlHelper.GetReader("GetPartners", new Hashtable());

				partnersKeyedbyId = new Hashtable();
				partnersKeyedByHostName = new Hashtable();

				int partnerIdColumn = reader.GetOrdinal("PartnerId");
				int hostNameColumn = reader.GetOrdinal("HostName");
				int partnerNameColumn = reader.GetOrdinal("PartnerName");
				int channelColumn = reader.GetOrdinal("Channel");
				int partnerPasswordColumn = reader.GetOrdinal("PartnerPassword"); 

				while (reader.Read())
				{

					Partner partner = new Partner(
						reader.GetInt32(partnerIdColumn),
						reader.GetString(hostNameColumn),
						reader.GetString(partnerNameColumn),
						reader.GetString(channelColumn),
						reader.GetString(partnerPasswordColumn)); 

					partnersKeyedbyId.Add(partner.Id, partner);
					partnersKeyedByHostName.Add(partner.HostName, partner);
				}   
				reader.Close();
				
				// Execute the GetPartnerAllowedServices stored procedure. This returns a list of allowed services for partners
				reader = sqlHelper.GetReader("GetPartnerAllowedServices", new Hashtable());	

				partnerIdColumn = reader.GetOrdinal("PartnerId");
				int serviceIdColumn = reader.GetOrdinal("EESTID");
				int serviceNameColumn = reader.GetOrdinal("EESTType");

				while(reader.Read())
				{
					int partnerId =  reader.GetInt32(partnerIdColumn);
					Byte serviceId = reader.GetByte(serviceIdColumn);
					string serviceName = reader.GetString(serviceNameColumn);

					//add the allowed service to the partner object
					((Partner) partnersKeyedbyId[partnerId]).AddAllowedService(serviceId.ToString(), serviceName);
				}

				reader.Close();
	

			}
			catch (SqlException sqlEx)
			{			
				string message = "SqlException : "+ sqlEx.Message;

				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Database,
					TDTraceLevel.Error,
					message,
					sqlEx );
				Logger.Write ( oe );

				throw new TDException("SqlException caught : " + sqlEx.Message, sqlEx,
					false, TDExceptionIdentifier.TNSQLHelperError);
			}
			catch (TDException tdex)
			{    
				string message = "Error Calling Stored Procedure : GetPartners " + tdex.Message;
				
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					message);
				Logger.Write(oe);

				throw new TDException(message, tdex, false, TDExceptionIdentifier.TNSQLHelperStoredProcedureFailure);
			}		
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();
			}			
		}


		#endregion

		#region Public Methods

		/// <summary>
		/// Takes a hostname and returns its associated partner id
		/// </summary>
		/// <param name="hostName">hostname of partner</param>
		/// <returns>partner id as integer</returns>
		public int GetPartnerIdFromHostName(string hostName)
		{			
			if (partnersKeyedByHostName.ContainsKey(hostName))
			{
				return ((Partner) partnersKeyedByHostName[hostName]).Id;				
			}
			else if (partnersKeyedByHostName.ContainsKey(hostName.ToLower()))
			{
				return ((Partner) partnersKeyedByHostName[hostName.ToLower()]).Id;				
			}
			else
			{
                if (hostName == "")
                {
                    return 0;
                }
                else
                {
    				//log error and throw exception
	    			string message = "Error trying to lookup the partner via the hostname : " + hostName;
                        
    				OperationalEvent oe = 
	    				new  OperationalEvent(	TDEventCategory.Business,
		    			TDTraceLevel.Error,
			    		message);
			    	Logger.Write(oe);										

			    	throw new TDException(message, false, TDExceptionIdentifier.TDPartnerLookupError);
                }
			}
				
		}

		/// <summary>
		/// returns the partner name from the supplied partner id
		/// </summary>
		/// <param name="partnerId">Partner Id from the partner</param>
		/// <returns>Returns the partner name as string</returns>
		public string GetPartnerHostName(int partnerId)
		{
			if (partnersKeyedbyId.ContainsKey(partnerId))
			{
				return ((Partner) partnersKeyedbyId[partnerId]).HostName;				
			}
			else				
			{

				//log error and throw exception
				string message = "Error trying to lookup the partner hostname via the PartnerId : " + partnerId;						

				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					message);
				Logger.Write(oe);

				throw new TDException(message, false, TDExceptionIdentifier.TDPartnerLookupError);
			}
		}

		/// <summary>
		/// returns the partner display name from the supplied partner id
		/// </summary>
		/// <param name="partnerId">Partner Id from the partner</param>
		/// <returns>Returns the partner display name as string</returns>
		public string GetPartnerDisplayName(int partnerId)
		{
			if (partnersKeyedbyId.ContainsKey(partnerId))
			{
				return ((Partner) partnersKeyedbyId[partnerId]).Name;				
			}
			else				
			{
				//log error and throw exception
				string message = "Error trying to lookup the partner displayname via the PartnerId : " + partnerId;						

				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					message);
				Logger.Write(oe);

				throw new TDException(message, false, TDExceptionIdentifier.TDPartnerLookupError);
			}
		}

        /// <summary>
        /// returns the top level MCMS channel name for the supplied partner id
        /// </summary>
        /// <param name="partnerId">Partner Id from the partner</param>
        /// <returns>Returns top level MCMS channel name as string</returns>
        public string GetPartnerChannel(int partnerId)
        {
            if (partnersKeyedbyId.ContainsKey(partnerId))
            {
                return ((Partner) partnersKeyedbyId[partnerId]).Channel;				
            }
            else				
            {
                //log error and throw exception
                string message = "Error trying to lookup the partner channel via the PartnerId : " + partnerId;						

                OperationalEvent oe = 
                    new  OperationalEvent(	TDEventCategory.Business,
                    TDTraceLevel.Error,
                    message);
                Logger.Write(oe);

                throw new TDException(message, false, TDExceptionIdentifier.TDPartnerLookupError);
            }
        }

		/// <summary>
		/// Checks that the MCMS channel is valid for the partnerid. Will always return true if
		/// URL validation disabled for partner in properties.
		/// </summary>
		/// <param name="partnerId">Partner id of partner to check URL for</param>
		/// <param name="channel">The full channel path of current MCMS channel</param>
		/// <returns>True if valid, false otherwise</returns>
		public bool ValidateRequestUrl(int partnerId, string channel)
		{
            string[] nodes = channel.Split('/');

            bool validationRequired = 
                Convert.ToBoolean(Properties.Current["PartnerConfiguration.ValidateUrl", partnerId], CultureInfo.InvariantCulture);

            if (validationRequired) 
            {
                if (nodes.Length <2) 
                {
                    return false;
                } 
                else 
                {
                    // perform case insensitive comparison between supplied channel and allowed channel names
                    return String.Compare(GetPartnerChannel(partnerId),nodes[1],true,CultureInfo.InvariantCulture) == 0;
                }
            } 
            else 
            {
                // always return check passed if URL validation disabled
                return true;
            }

		}

		/// <summary>
		/// This function returns the password from the given username.
		/// </summary>
		/// <param name="username">username of the partner</param>
		/// <returns>If found, it send password in clear text else exception will be thrown</returns>
		public string GetClearPassword(string username)
		{   
			Partner partner = GetPartnerByHostName(username);

			if (partner != null)
				return partner.Password;
			else
			{
				//log error and throw exception
				string message = "Error trying to lookup the password via the hostname : " + username;

				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					message);
				Logger.Write(oe);										

				throw new TDException(message, false, TDExceptionIdentifier.TDPartnerLookupError);
			}   

		}

		/// <summary>
		/// This method will return the IsAllowedService on a specific partner object
		/// </summary>
		/// <param name="hostName">the Hostname of the partner in the partner table</param>
		/// <param name="serviceName">the service name of the service to check the access on</param>
		/// <returns></returns>
		public bool IsPartnerAllowedToService(string hostName, string serviceName)
		{
			return GetPartnerByHostName(hostName).IsAllowedService(serviceName);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// This method will query the partnersKeyedByHostName hashtable and handle upper/lower
		/// case issues
		/// </summary>
		/// <param name="hostName">hostname to lookup on</param>
		/// <returns>partner object</returns>
		private Partner GetPartnerByHostName(string hostName)
		{
			if (partnersKeyedByHostName.ContainsKey(hostName))
				return ((Partner) partnersKeyedByHostName[hostName]);				
			else if (partnersKeyedByHostName.ContainsKey(hostName.ToLower()))
				return ((Partner) partnersKeyedByHostName[hostName.ToLower()]);						
			else
				return null;
		}
		#endregion

	}
}
