using System;
using System.Security.Cryptography;


namespace TransportDirect.UserPortal.SupportApps.CryptoApp
{
	#region CryptoSaver helper class
	/// <summary>
	/// Helper class for serializing data to file
	/// </summary>
	[Serializable()]
	public class CryptoSaver
	{
		private RSAParameters rsap;
		/// <summary>
		/// The RSAPArameter key.
		/// </summary>
		public RSAParameters RSAParams
		{
			get 
			{
				return rsap; 
			}
			set
			{
				rsap = value;
			}

		}

		private string base64CryptSymKey = string.Empty;
		/// <summary>
		/// The encrypted symmetric key
		/// </summary>
		public string CryptSymKey
		{
			get 
			{
				return base64CryptSymKey;
			}
			set
			{
				base64CryptSymKey = value;
			}
		}

		private string base64SymIV = string.Empty;
		/// <summary>
		/// The symmetric initialisation vector
		/// </summary>
		public string SymIV
		{
			get 
			{
				return base64SymIV;
			}
			set
			{
				base64SymIV = value;
			}
		}

		private string store = string.Empty;
		/// <summary>
		/// The store name
		/// </summary>
		public string Store
		{
			get { return store; }
			set { store = value; }
		}

		private bool machineStore = true;
		/// <summary>
		/// Indicator if this key should be in the machine store or not.
		/// </summary>
		public bool UseMachineStore 
		{
			get { return machineStore; }
			set { machineStore = value; }
		}

		/// <summary>
		/// Empty constructor.
		/// </summary>
		public CryptoSaver()
		{
		}

		/// <summary>
		/// Constructs the cryptosaver with the given information
		/// </summary>
		/// <param name="rsap">The RSA parameters</param>
		/// <param name="cryptSymKey">The encrypted symmetric key</param>
		/// <param name="IV">The symmetric initialisation vector</param>
		public CryptoSaver(RSAParameters rsap, byte[] cryptSymKey, byte[] IV)
		{
			this.rsap = rsap;
			this.base64CryptSymKey = Convert.ToBase64String( cryptSymKey );
			this.base64SymIV = Convert.ToBase64String( IV );
		}


	}
	#endregion

	#region Name Value Pair Helper class
	/// <summary>
	/// Helper class for serializing data to file
	/// </summary>
	[Serializable()]
	public class NameValuePair
	{
		private string name;
		/// <summary>
		/// The name
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string val;
		/// <summary>
		/// The value
		/// </summary>
		public string Value
		{
			get { return val; }
			set { val = value; }
		}
	}
	#endregion
}
