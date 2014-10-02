// *********************************************** 
// NAME                 : TestTDCryptlib.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 27/10/2003 
// DESCRIPTION			: Unit test for the crypt library
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/TestTDCryptlib.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:54   mturner
//Initial revision.
//
//   Rev 1.2   May 12 2005 14:38:42   NMoorhouse
//Added extra valid expected result for TestDecryptionFailure. Decrypting using a different key may not throw an exception but as long as the new decrypted value is not equal to the original value.
//
//   Rev 1.1   Feb 07 2005 09:24:38   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Oct 30 2003 14:50:24   PNorell
//Initial Revision
using System;
using System.Security.Cryptography;
using NUnit.Framework;

namespace TransportDirect.Common.PropertyService.Properties
{
	/// <summary>
	/// Summary description for TestTDCryptlib.
	/// </summary>
	[TestFixture]
	public class TestTDCryptlib
	{
		/// <summary>
		/// The TDCrypt object being tested.
		/// </summary>
		private TDCrypt tdcrypt;

		/// <summary>
		/// Sets up all parameters and keys for the rest of the test.
		/// The test uses it's own key container to ensure it does not wipe out an
		/// existing key container.
		/// </summary>
		[SetUp()]
		public void SetUp()
		{
			// Create CSP parameters
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = "TestStore";
			csp.Flags = CspProviderFlags.UseMachineKeyStore;

			// Creates or fetches an existing RSA provider
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

			// Creates an AES
			Rijndael rij = RijndaelManaged.Create();

			// Generates the AES key
			rij.GenerateKey();

			// Generates the initialisation vector
			rij.GenerateIV();

			// Decrypt the crypted key into readable value
			// Encrypts the rij key with the RSA key.
			byte[] crSymKey = rsa.Encrypt( rij.Key , false );

			// Stores the initilisation vector
			byte[] symIV = rij.IV;

			// Creates an tdcrypt object with the given limitations of machine key and csp storage
			tdcrypt = new TDCrypt(crSymKey, symIV, false, true, csp.KeyContainerName);
		}

		/// <summary>
		/// Tests that a value is encrypted properly.
		/// </summary>
		[Test()]
		public void TestEncryption()
		{
			string orgString = "This is the data that should be crypted";

			string cryString = tdcrypt.Encrypt( orgString );

			Assert.IsNotNull(cryString,  "Cryptographic value is null");

			Assert.IsFalse(orgString.Equals( cryString ),  "Encrypted value is in clear");
		}

		/// <summary>
		/// Test so that a encrypted key can be decrypted with the right sort of key.
		/// </summary>
		[Test()]
		public void TestDecryption()
		{
			string orgString = "This is the data that should be crypted";

			string cryString = tdcrypt.Encrypt( orgString );

			Assert.IsNotNull(cryString, "Cryptographic value is null");

			Assert.IsFalse(orgString.Equals( cryString ),"Encrypted value is in clear");


			string decryptedString = tdcrypt.Decrypt( cryString );

			Assert.IsTrue(orgString.Equals( decryptedString ), "Orginal message not equal after decryption");
		}

		/// <summary>
		/// Test so that an encrypted key can not be decrypted using another key
		/// </summary>
		[Test()]
		public void TestDecryptionFailure()
		{
			// Create CSP parameters
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = "TestStore";
			csp.Flags = CspProviderFlags.UseMachineKeyStore;

			// Creates or fetches an existing RSA provider
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
			
			string orgString = "This is the data that should be crypted";

			string cryString = tdcrypt.Encrypt( orgString );

			Assert.IsNotNull(cryString, "Cryptographic value is null");

			Assert.IsFalse(orgString.Equals( cryString ), "Encrypted value is in clear");

			// Creates an AES
			Rijndael rij = RijndaelManaged.Create();

			// Generates a new AES key
			rij.GenerateKey();

			// Generates the initialisation vector
			rij.GenerateIV();

			// Decrypt the crypted key into readable value
			// Encrypts the rij key with the RSA key.
			byte[] crSymKey = rsa.Encrypt( rij.Key , false );

			// Stores the initilisation vector
			byte[] symIV = rij.IV;

			TDCrypt newKey = new TDCrypt(crSymKey, symIV, false, true, csp.KeyContainerName);

			// should throw System.Security.Cryptography.CryptographicException 
			// or return a decrypted string not equal orgString
			try
			{
				string decryptedString = newKey.Decrypt( cryString );
				Assert.IsFalse(orgString.Equals( decryptedString ),"Decrypted value (decrypted using different key) same as Original value");
				
			}
			catch (CryptographicException)
			{
			}

		}
	}
}
