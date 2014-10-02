// *********************************************** 
// NAME                 : ITDCrypt.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 27/10/2003 
// DESCRIPTION			: Interface specification for the crypt library
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/ITDCrypt.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:50   mturner
//Initial revision.
//
//   Rev 1.1   Sep 29 2005 17:39:16   build
//Automatically merged from branch for stream2610
//
//   Rev 1.0.1.0   Sep 14 2005 09:15:12   halkatib
//Changes required for landing page. Encryption/Decryption of base 64 string.
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.0   Oct 30 2003 14:50:16   PNorell
//Initial Revision
using System;

namespace TransportDirect.Common.PropertyService.Properties
{
	/// <summary>
	/// Interface specification for the crypt library
	/// </summary>
	public interface ITDCrypt
	{
		/// <summary>
		/// Encrypts a string into a base64 string representing the byte-array it was transformed into.
		/// </summary>
		/// <param name="raw">The string to be crypted in raw format</param>
		/// <returns>Crypted value in base64</returns>
		string Encrypt(string raw);

		/// <summary>
		/// Encrypts a byte array.
		/// </summary>
		/// <param name="raw">The bytes to be crypted</param>
		/// <returns>Crypted value</returns>
		byte[] Encrypt(byte[] bRaw);

		/// <summary>
		/// Decrypts a base64 encrypted string into raw string.
		/// </summary>
		/// <param name="base64string">Base64 string to be decrypted</param>
		/// <returns>Decrypted value</returns>
		string Decrypt(string base64string);

		/// <summary>
		/// Decrypts a encrypted byte-array into raw bytes.
		/// </summary>
		/// <param name="bRaw">The encrypted bytes to be decrypted</param>
		/// <returns>Decrypted value</returns>
		byte[] Decrypt(byte[] bRaw);

		/// <summary>
		/// Decrypts encrypted string in base64 into a clear text
		/// </summary>
		/// <param name="base64string">The string to be decrypted</param>
		/// <returns>decrypted string value</returns>
		string AsymmetricDecrypt(string base64string);

		/// <summary>
		/// Encrypts cleartext into a Base64 encrypted string
		/// </summary>
		/// <param name="raw">The string to be decrypted</param>
		/// <returns>Encrypted string in Base64 format</returns>
		string AsymmetricEncrypt(string raw);

	
	}
}
