// *********************************************** 
// NAME                 : TDCrypt.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 27/10/2003 
// DESCRIPTION			: Cryptofactory handles instantiating of the crypto as well as extracting key and iv
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/CryptoFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:50   mturner
//Initial revision.
//
//   Rev 1.0   Oct 30 2003 14:50:02   PNorell
//Initial Revision
using System;
using System.Configuration;
// using System.Text

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.Common.PropertyService.Properties
{

	/// <summary>
	/// Cryptofactory handles instantiating of the crypto as well as extracting key and iv
	/// </summary>
	public class CryptoFactory : IServiceFactory
	{
		#region Constant declarations
		/// <summary>
		/// Property key for the encryption key
		/// </summary>
		private const string PROP_KEY = "propertyservice.cryptography.key";
		/// <summary>
		/// Property key used for the IV
		/// </summary>
		private const string PROP_IV = "propertyservice.cryptography.iv";
		/// <summary>
		/// Property key used for the strong crypto flag
		/// </summary>
		private const string PROP_STRONG = "propertyservice.cryptography.strong";
		#endregion

		#region Private variables
		/// <summary>
		/// Encryption/decryption engine used.
		/// </summary>
		private ITDCrypt crypt;
		#endregion

		#region Constructor logic
		/// <summary>
		/// Constructs a cryptographic factory that reads nessecary parameters
		/// from the AppSettings.
		/// </summary>
		public CryptoFactory()
		{
			// Fetch encrypted base64 rijandel string
            string cryptString = ConfigurationManager.AppSettings[PROP_KEY];
			// Fetch base64 rijandel initialisation vector
            string ivString = ConfigurationManager.AppSettings[PROP_IV];
			// Fetch flag if strong crypto should be used or not
            string strongString = ConfigurationManager.AppSettings[PROP_STRONG];
			
			// Convert strong crypto from string to boolean
			bool strongCrypt = false;
			if( strongString != null && strongString != string.Empty )
			{
				strongCrypt = Convert.ToBoolean( strongString );
			}

			// Convert key to byte array
			byte[] cryptedKey = Convert.FromBase64String( cryptString );
			// Convert IV to byte array
			byte[] IV = Convert.FromBase64String( ivString );

			// Create crypto engine
			crypt = new TDCrypt(cryptedKey, IV, strongCrypt, true);
		}
		#endregion

		#region Public Methods according to interface IServiceFactory
		/// <summary>
		/// According to IServiceFactory.
		/// CryptoFactory treats the ITDCrypt as a singleton and will not create seperate instances
		/// for each call.
		/// </summary>
		/// <returns>Returns an instance of ITDCrypt (concrete implementation TDCrypt)</returns>
		public object Get()
		{
			return crypt;
		}
		#endregion
		
	}
}
