// *********************************************** 
// NAME                 : TDCrypt.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 27/10/2003 
// DESCRIPTION			: The crypt library
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/TDCrypt.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:22:58   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:37:54   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:15:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Dec 15 2005 10:17:00   schand
//Getting Partnet White Label changes for stream3129. 
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1.2.0   Oct 14 2005 10:15:10   halkatib
//Added Landing page funtionality for white label
//Resolution for 2871: Del 8 White Labelling Phase 3 - Landing Page
//
//   Rev 1.1   Dec 02 2003 10:34:32   PNorell
//Updated the TDCrypt to enable encryption with a given AES key and IV
//
//   Rev 1.0   Oct 30 2003 14:50:20   PNorell
//Initial Revision
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace TransportDirect.Common.PropertyService.Properties
{
	/// <summary>
	/// TDCrypt is a small cryptographic library for encrypting/decrypting values.
	/// It does not handle the installation/uninstallation of RSA keys used to decrypt 
	/// </summary>
	public class TDCrypt : ITDCrypt
	{
		/// <summary>
		/// Suggested prefix for encrypted strings.
		/// Note: This is not added to any of the return values for these methods.
		/// </summary>
		public const string CRYPT_PREFIX = "crypt@";

		/// <summary>
		/// The main store used for the asymmetric key if none is given.
		/// </summary>
		public const string CSP_STORE = "TDPCryptStore";

		/// <summary>
		/// Property key used for the strong crypto flag
		/// </summary>
		private const string PROP_STRONG = "propertyservice.cryptography.strong";
		
		/// <summary>
		/// The Rijandael algorithm used for encrypting/decrypting
		/// </summary>
		private Rijndael rij;

		/// <summary>
		/// The CSP Store used for this TDCrypt instance.
		/// </summary>
		private string cspStore;

		/// <summary>
		/// Creates a TDCrypt object that is not encrypted by an RSA key in the CSP.
		/// </summary>
		/// <param name="key">The key in base64 format</param>
		/// <param name="IV">The initialisation vector</param>
		public TDCrypt(string key, string IV)
		{
			rij = RijndaelManaged.Create();
			// Apply key and initialisation vector
			rij.Key = Convert.FromBase64String(key);
			rij.IV = Convert.FromBase64String(IV);

		}

		/// <summary>
		/// Creates a TDCrypt object ready for encrypting/decrypting data.
		/// </summary>
		/// <param name="encryptedKey">The crypted AES (Rijandel) key</param>
		/// <param name="IV">The AES initialisation vector</param>
		/// <param name="strongCrypto">If strong cryptography should be used or not</param>
		/// <param name="machineStore">If the machine or user store should be used</param>
		/// <param name="cspStore">The machine store that should be used to retrieve RSA key</param>
		public TDCrypt(byte[] encryptedKey, byte[] IV, bool strongCrypto, bool machineStore, string cspStore)
		{
			this.cspStore = cspStore;
			// Get the key - it will exist "readable" in-memory
			byte[] key = AsymmetricDecrypt( encryptedKey, strongCrypto, machineStore);
			// Does not really need to be encrypted
			rij = RijndaelManaged.Create();
			// Apply key and initialisation vector
			rij.Key = key;
			rij.IV = IV;

		}

		/// <summary>
		/// Creates a TDCrypt object ready for encrypting/decrypting data.
		/// </summary>
		/// <param name="encryptedKey">The crypted AES (Rijandel) key</param>
		/// <param name="IV">The AES initialisation vector</param>
		/// <param name="strongCrypto">If strong cryptography should be used or not</param>
		/// <param name="machineStore">If the machine or user store should be used</param>
		public TDCrypt(byte[] encryptedKey, byte[] IV, bool strongCrypto, bool machineStore) : this( encryptedKey, IV, strongCrypto, machineStore, CSP_STORE )
		{
		}


		/// <summary>
		/// Decrypts the AES key with the help of the currently installed RSA crypto.
		/// </summary>
		/// <param name="cryptedValue"></param>
		/// <param name="strongCrypto"></param>
		/// <param name="machineStore"></param>
		/// <returns></returns>
		private byte[] AsymmetricDecrypt(byte[] encryptedValue, bool strongCrypto, bool machineStore)
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = cspStore;
			// if( machineStore )
			csp.Flags = CspProviderFlags.UseMachineKeyStore;
			// else
			//	csp.Flags = CspProviderFlags.UseDefaultKeyContainer;

			// Find the rsa crypto
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
			
			// Decrypt the crypted key into readable value
			byte[] decryptedValue = rsa.Decrypt( encryptedValue, strongCrypto );

			// return decrypted value key
			return decryptedValue;
		}


		/// <summary>
		/// Decrypts the AES key with the help of the currently installed RSA crypto.
		/// </summary>
		/// <param name="cryptedValue"></param>
		/// <param name="strongCrypto"></param>
		/// <param name="machineStore"></param>
		/// <returns></returns>
		private byte[] AsymmetricEncrypt(byte[] clearText, bool strongCrypto, bool machineStore)
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = cspStore;
			// if( machineStore )
			csp.Flags = CspProviderFlags.UseMachineKeyStore;
			// else
			//	csp.Flags = CspProviderFlags.UseDefaultKeyContainer;

			// Find the rsa crypto
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
			
			// Decrypt the crypted key into readable value
			byte[] encryptedValue = rsa.Encrypt( clearText, strongCrypto );

			// return decrypted value key
			return encryptedValue;
		}
		
		/// <summary>
		/// REQUIRED FOR LANDING PAGE FUNCTIONALITY
		/// Decrypts encrypted string into a meaningful string
		/// </summary>
		/// <param name="base64string">The string to be decrypted</param>
		/// <returns>decrypted string value</returns>
		public string AsymmetricDecrypt(string base64string)
		{
			string strongString = ConfigurationManager.AppSettings[PROP_STRONG];
            
            			
			// Convert strong crypto from string to boolean
			bool strongCrypt = false;
			if( strongString != null && strongString.Length == 0 )
			{
				strongCrypt = Convert.ToBoolean( strongString );
			}
			byte[] decrypted = AsymmetricDecrypt(Convert.FromBase64String(base64string), strongCrypt, true);
			return ASCIIEncoding.Unicode.GetString(decrypted);
		}


		/// <summary>
		/// Encrypts cleartext into a Base64 encrypted string
		/// </summary>
		/// <param name="raw">The string to be decrypted</param>
		/// <returns>Encrypted string in Base64 format</returns>
		public string AsymmetricEncrypt(string raw)
		{
            string strongString = ConfigurationManager.AppSettings[PROP_STRONG];
			
			// Convert strong crypto from string to boolean
			bool strongCrypt = false;
			if( strongString != null && strongString.Length == 0 )
			{
				strongCrypt = Convert.ToBoolean( strongString );
			}
			byte[] encrypted = AsymmetricEncrypt( ASCIIEncoding.Unicode.GetBytes( raw ) , strongCrypt, true);
			return Convert.ToBase64String( encrypted );
		}

		

		/// <summary>
		/// Encrypts a string into a base64 string representing the byte-array it was transformed into.
		/// </summary>
		/// <param name="raw">The string to be crypted in raw format</param>
		/// <returns>Crypted value in base64</returns>
		public string Encrypt(string raw)
		{
			// Convert string into bytes and get the crypted value
			byte[] cryptedRaw = Encrypt( ASCIIEncoding.Unicode.GetBytes( raw ) );

			// convert crypted byte array into base64string
			return Convert.ToBase64String( cryptedRaw );
		}

		/// <summary>
		/// Encrypts a byte array.
		/// </summary>
		/// <param name="raw">The bytes to be crypted</param>
		/// <returns>Crypted value</returns>
		public byte[] Encrypt( byte[] bRaw )
		{
			// crypt bRaw into bCrypt using AES
			ICryptoTransform ict = rij.CreateEncryptor();
			byte[] cryptedRaw = ict.TransformFinalBlock( bRaw , 0, bRaw.Length );

			return cryptedRaw;			
		}

		/// <summary>
		/// Decrypts a base64 encrypted string into raw string.
		/// </summary>
		/// <param name="base64string">Base64 string to be decrypted</param>
		/// <returns>Decrypted value</returns>
		public string Decrypt(string base64string)
		{
			// Convert string into bytes
			byte[] bRaw = Convert.FromBase64String(base64string); 

			// Decrypt and transform into string again
			return ASCIIEncoding.Unicode.GetString( Decrypt( bRaw ) );
		}

		/// <summary>
		/// Decrypts a encrypted byte-array into raw bytes.
		/// </summary>
		/// <param name="bRaw">The encrypted bytes to be decrypted</param>
		/// <returns>Decrypted value</returns>
		public byte[] Decrypt(byte[] bRaw)
		{
			// Get transformer
			ICryptoTransform ict = rij.CreateDecryptor();
			
			// Transform the crypto
			byte[] clearRaw = ict.TransformFinalBlock( bRaw , 0, bRaw.Length );

			// Return cleartext
			return clearRaw;
		}
	}
}