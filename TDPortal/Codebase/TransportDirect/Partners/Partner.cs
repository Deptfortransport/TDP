// *********************************************** 
// NAME			: Partner.cs
// AUTHOR		: Manuel Dambrine
// DATE CREATED	: 27/09/2005
// DESCRIPTION	: Represents an Partner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/Partner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:44   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 19:15:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.1   Feb 20 2006 15:36:42   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.5.1.0   Nov 25 2005 18:12:02   schand
//Added property Password and private method DecryptPassword()
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.5   Oct 14 2005 15:54:06   COwczarek
//Apply review comments from CR015
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.4   Oct 13 2005 16:17:00   COwczarek
//Add new Channel property
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2869: Del8 White Labelling - Request URL Validation
//
//   Rev 1.3   Oct 07 2005 11:23:08   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.2   Sep 30 2005 12:22:28   COwczarek
//Partner object now holds hostname in lower case
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2808: Del8 White Labelling - Page Header & Footer
//
//   Rev 1.1   Sep 29 2005 11:56:12   mdambrine
//wrong header
//
//   Rev 1.0   Sep 27 2005 16:43:56   mdambrine
//Initial revision.
//

using System;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;


namespace TransportDirect.Partners
{
	/// <summary>
	/// Represents an Partner
	/// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class Partner
    {
        private int id;
        private string hostName;
        private string name;
        private string channel;
		private string password;
		private Hashtable allowedServices;
		
        #region Constructors
        /// <summary>
        /// Creates a partnerid
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="HostName"></param>
        /// <param name="Name"></param>
        public Partner(int id, string hostName, string name, string channel, string password)
        {
            this.id = id;
            this.hostName = hostName.ToLower(CultureInfo.InvariantCulture);
            this.name = name;
            this.channel = channel.ToLower(CultureInfo.InvariantCulture);
			this.password  = DecryptPassword(password); 
			this.allowedServices = new Hashtable();
        }
	
        #endregion	

        
        #region Properties
        /// <summary>
        /// Id of the partner
        /// </summary>
        public int Id 
        {
            get 
            { 
                return id; 
            }
        }

        /// <summary>
        /// HostName of the partner
        /// </summary>
        public string HostName 
        {
            get 
            { 
                return hostName; 
            }
        }

        /// <summary>
        /// Name of the partner
        /// </summary>
        public string Name 
        {
            get 
            { 
                return name; 
            }
        }

        /// <summary>
        /// Top-level CMS channel name
        /// </summary>
        public string Channel 
        {
            get 
            { 
                return channel; 
            }
        }

		/// <summary>
		/// Partner password to access the EnhancedExposedServices
		/// </summary>
		public string Password
		{
			get 
			{
				return password; 
			}
		}
        #endregion

		#region Public Methods
		/// <summary>
		/// Checks if a partner is allowed to access a exposed service
		/// </summary>
		/// <param name="servicename"></param>
		/// <returns></returns>
		public bool IsAllowedService(string serviceName)
		{
			return allowedServices.ContainsValue(serviceName);
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// internal method that populates the allowedservice hashtable with a new entry
		/// </summary>
		/// <param name="serviceId"></param>
		/// <param name="servicename"></param>
		internal void AddAllowedService(string serviceId, string serviceName)
		{
			allowedServices.Add(serviceId, serviceName);
		}
		#endregion

		#region Private Methods
			
		/// <summary>
		/// Decrypts encrypted data and parses into a string array
		/// </summary>
		/// <param name="encrypteddata">Encrypted data</param>
		/// <returns>String array of decrpyted data</returns>
		private string DecryptPassword(string encryptedPassword)
		{
			if (encryptedPassword==null || encryptedPassword.Trim().Length < 1 )
				return encryptedPassword;
 
			ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ServiceDiscoveryKey.Crypto ];
			return decryptionEngine.Decrypt(encryptedPassword);
			
		}				
		#endregion

	}

}
