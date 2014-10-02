// ***********************************************
// NAME 		: TDUser.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 01/12/2003
// DESCRIPTION 	: A user representation for registered users in the commerce server.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDUser.cs-arc  $
//
//   Rev 1.1   Jan 30 2013 10:35:26   mmodi
//Updated to save Telecabine in favourite journey
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:48:44   mturner
//Initial revision.
//
//   Rev 1.11   May 17 2007 10:29:46   Pscott
//CCN0368
//Amend Registered user details
//
//   Rev 1.10   Apr 19 2005 16:07:00   jgeorge
//Changes from Peter Norell
//Resolution for 1991: Del 7  - Car Costing - Unable to plan journey if logged in as a user registered pre-del 7
//
//   Rev 1.9   Mar 08 2005 09:34:10   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.8   Aug 02 2004 13:37:02   COwczarek
//Replace hardcoded key values with constants defined in ProfileKeys class.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.7   Jul 14 2004 14:40:16   RPhilpott
//Get rid of compilation warning on exception.
//
//   Rev 1.6   Jul 02 2004 13:35:28   jgeorge
//Changes for user type
//
//   Rev 1.5   Apr 28 2004 16:19:52   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.4   Apr 27 2004 11:54:30   cshillan
//Replace Commerce Server profiling usage with the new TDProfile object
//
//   Rev 1.4   Apr 21 2004 10:31:08   cshillan
//Removal of Microsoft Commerce Server for user authentication and user profiles
//
//   Rev 1.3   Feb 12 2004 15:05:06   esevern
//DEL5.2 - seperation of login and registration
//
//   Rev 1.2   Jan 23 2004 16:03:54   PNorell
//Favourite journey updates.
//
//   Rev 1.1   Jan 21 2004 11:35:12   PNorell
//Updates for 5.2
//
//   Rev 1.0   Dec 02 2003 10:31:56   PNorell
//Initial Revision

using System;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// A user representation for registered users
	/// </summary>
	/// 
	[CLSCompliant(false)]
	public class TDUser
	{
		#region Constant definitions
		/// <summary>
		/// The size of the salt
		/// </summary>
		private const int SALT_SIZE = 4;

		/// <summary>
		/// Limiting the allowed characters for the salt - it would be better to have completly random byte
		/// </summary>
		private const string ALLOWED_SALT_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		/// <summary>
		/// The prefix used for encrypted passwords
		/// </summary>
		private const string PWD_PREFIX = TDCrypt.CRYPT_PREFIX;

		/// <summary>
		/// The property name for the key
		/// </summary>
		private const string PROPERTY_PASSWORDKEY = "usersupport.encryptionkey";

		/// <summary>
		/// The property name for the initialisation vector
		/// </summary>
		private const string PROPERTY_PASSWORDIV = "usersupport.encryptioniv";

		/// <summary>
		/// The max number of favourite journeys from the properties service
		/// </summary>
		private const string PROP_MAX_FAVOURITES = "favourites.maxnumberoffavouritejourneys";
		#endregion

		#region Private variables
		private string userID = string.Empty;

		/// <summary>
		/// Used for generating salt
		/// </summary>
		private Random random = new Random();

		/// <summary>
		/// The userprofile for the user
		/// </summary>
		private TDProfile userProfile = null;

		/// <summary>
		/// Contains the associated journey preferences for this user.
		/// </summary>
		private TDUserJourneyPreferences journeyPref = null;
		#endregion

		#region Constructors
		public TDUser()
		{
			this.userProfile = new TDProfile();
		}
		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the password in clear text
		/// </summary>
		public string Password
		{
			get
			{
				// Get encrypted password
				string pwd = (string)userProfile.Properties[ProfileKeys.USER_SECURITY_PASSWORD].Value;
				if( pwd.StartsWith( PWD_PREFIX ) )
				{
					// Decrypt and return
					return EncryptionEngine.Decrypt( pwd.Substring( PWD_PREFIX.Length + SALT_SIZE ) ).Substring( SALT_SIZE );
				}
				return pwd;
			}
		}

		/// <summary>
		/// Gets the username as a string
		/// </summary>
		public string Username
		{
			get
			{
				if ( userProfile != null )
				{
					return (string)userProfile.Properties[ProfileKeys.LOGON].Value;
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Type of the current user. This is passed to the CJP when making a request.
		/// </summary>
		public TDUserType UserType
		{
			get
			{
				if (userProfile.Properties[ProfileKeys.USERTYPE].Value == null)
				{
					userProfile.Properties[ProfileKeys.USERTYPE].Value = TDUserType.Standard;
					return TDUserType.Standard;
				}
				else
				{
					try
					{
						return (TDUserType)userProfile.Properties[ProfileKeys.USERTYPE].Value;
					}
					catch (InvalidCastException)
					{
						// Couldn't convert the database value to TDUserType
						userProfile.Properties[ProfileKeys.USERTYPE].Value = TDUserType.Standard;
						return TDUserType.Standard;
					}
				}
			}
			set
			{
				userProfile.Properties[ProfileKeys.USERTYPE].Value = value;
			}
		}

		/// <summary>
		/// Gets the user profile object
		/// </summary>
		public TDProfile UserProfile
		{
			get
			{
				return userProfile;
			}
		}

		/// <summary>
		/// Gets the journey preferences for this user.
		/// </summary>
		public TDUserJourneyPreferences JourneyPreferences
		{
			get
			{
				if( journeyPref == null )
				{
					this.journeyPref = new TDUserJourneyPreferences(userProfile);
				}
				return journeyPref;
			}
		}

		#endregion

		#region Public methods

		public void CreateUser( string userName, string password )
		{
			CreateUser(userName, password, TDUserType.Standard);
		}

		public void CreateUser( string userName, string password, TDUserType userType)
		{
			userProfile.Username = userName;
			userProfile.Properties[ProfileKeys.LOGON].Value = userProfile.Username;
			userProfile.Properties[ProfileKeys.LASTLOGIN].Value = new TDDateTime().GetDateTime();
			userProfile.Properties[ProfileKeys.USERTYPE].Value = userType;
			this.RegisterGivenPassword( password );
		}

		public bool FetchUser( string userName )
		{
			if ( userProfile.LoadProfile( userName ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Matches the password given and sets the last login field to current date.
		/// </summary>
		/// <param name="givenPassword">The password given by the user</param>
		/// <returns>True if successfully logged in and date given</returns>
		public bool Logon( string givenPassword )
		{
			if( PasswordMatch( givenPassword ) )
			{
				// Set last logon
				userProfile.Properties[ProfileKeys.LOGON].Value = userProfile.Username;
				userProfile.Properties[ProfileKeys.LASTLOGIN].Value = new TDDateTime().GetDateTime();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Matches the password given and deletes user.
		/// </summary>
		/// <param name="givenPassword">The password given by the user</param>
		/// <returns>True if successfully deleted</returns>
		public bool DeleteUser( string givenPassword )
		{
			if( PasswordMatch( givenPassword ) )
			{
				return userProfile.DeleteProfile(userProfile.Username);
			}
			return false;
		}

		/// <summary>
		/// Persists any data
		/// </summary>
		public void Update()
		{
			// Loop through All journey alternatives -> find all guids -> then update
			if( fj != null )
			{
				// Only needed if anyone tried to access a favourite journey
				for(int i = 0; i < fj.Length; i++)
				{
					if( fj[i] != null && fj[i].Guid != null )
					{
						userProfile.Properties[ string.Format( ProfileKeys.FAVOURITE, i+1) ].Value = fj[i].Guid;
					}
				}
			}
			userProfile.Update();
		}


		/// <summary>
		/// Returns new journey favourite if such can exists, null if journey list is full
		/// </summary>
		/// <returns></returns>
		public FavouriteJourney NewJourney()
		{
			if( fj == null )
			{
				LoadFavouriteJourneys();
			}
			for(int i = 0; i < fj.Length; i++)
			{
				if( fj[i].Guid == null )
				{
					return fj[i];
				}
			}			
			return null;
		}

		private FavouriteJourney[] fj = null;

		/// <summary>
		/// Gets an array consisting of only journeys that are currently active (has been saved by the user).
		/// </summary>
		/// <returns>Array of only active favourite journeys</returns>
		public ArrayList FindRegisteredFavourites()
		{
			if( fj == null )
			{
				// Load it!
				LoadFavouriteJourneys();
			}
			ArrayList al = new ArrayList();
			for(int i = 0; i < fj.Length; i++)
			{
				if( fj[i].Guid == null )
				{
					break;
				}
				al.Add( fj[i] );
			}
			// Loop and find
			return al;
		}

		/// <summary>
		/// Gets the next free journey index
		/// </summary>
		/// <returns>The next journey index (0 - (max journey - 1)</returns>
		public int NextFavouriteJourneyIndex()
		{
			if( fj == null )
			{
				// Load it!
				LoadFavouriteJourneys();
			}
			ArrayList al = new ArrayList();
			int i = 0;
			for(; i < fj.Length; i++)
			{
				if( fj[i].Guid == null )
				{
					return i;
				}
			}
			return i;
		}

		/// <summary>
		/// Finds a favourite journey based upon its GUID.
		/// </summary>
		/// <param name="guid">The GUID for the journey</param>
		/// <returns>The favourite journey with the correct guid if such exists or null otherwise</returns>
		public FavouriteJourney FindRegisteredFavourite(string guid)
		{
			if( fj == null )
			{
				LoadFavouriteJourneys();
			}
			for(int i = 0; i < fj.Length; i++)
			{
				//
				if( fj[i].Guid == guid )
				{
					return fj[i];
				}
			}
			// None could be founds
			return null;
		}

		/// <summary>
		/// Gets the information if the favourite list is full or not
		/// </summary>
		public bool FullFavouriteList
		{
			get 
			{
				if( fj == null )
				{
					LoadFavouriteJourneys();
				}
				for(int i = 0; i < fj.Length; i++)
				{
					if( fj[i].Guid == null )
					{
						return false;
					}
				}
				return true;
			}
		}
		#endregion

		#region Private methods

		/// <summary>
		/// Loads the favourite journeys into the favourite journey cache
		/// </summary>
		private void LoadFavouriteJourneys()
		{
			string key = string.Empty;
			string guid = string.Empty;
			int index = 0;

			int searchDistance = 3000;
			string travID = (string)userProfile.Properties[ProfileKeys.PREFERENCES].Value;

			string ws = null;
			string wt = null;
			if( travID != null )
			{
				TDProfile travelPreferences = UserProfile;
				ws = (string)travelPreferences.Properties[ProfileKeys.WALKING_SPEED].Value;
				wt = (string)travelPreferences.Properties[ProfileKeys.WALKING_TIME].Value;
			}
			if( ws == null || wt == null )
			{
				// Pick it from the data services
				DataServices.DataServices ds = (DataServices.DataServices)TDServiceDiscovery.Current[ ServiceDiscoveryKey.DataServices ];
				ws = ds.GetDefaultListControlValue( DataServiceType.WalkingSpeedDrop );
				wt = ds.GetDefaultListControlValue( DataServiceType.WalkingMaxTimeDrop );
			}

			if( ws != null && wt != null )
			{
				// What is our favourite search distance 
				int walkS = Convert.ToInt32( ws , CultureInfo.InvariantCulture.NumberFormat);
				int walkT = Convert.ToInt32( wt , CultureInfo.InvariantCulture.NumberFormat);
				searchDistance = walkS * walkT;
			}

			// Array of size x journeys
			int numJourneys = Convert.ToInt32(Properties.Current[ PROP_MAX_FAVOURITES ]);
			fj = new FavouriteJourney[ numJourneys ];
			for(int i = 0; i < fj.Length; i++)
			{
				// Actual index
				index = i+1;
				key = string.Format( ProfileKeys.FAVOURITE, index );
				guid = (string)userProfile.Properties[ key ].Value;
				fj[i] = new FavouriteJourney(index, guid );
				fj[i].SearchDistance = searchDistance;
				fj[i].Load();
				fj[i].GuidChanged += new EventHandler( this.FavouriteGuidChanged );
			}
		}


		/// <summary>
		/// Event handler for favourite guid change
		/// </summary>
		/// <param name="sender">The favourite journey just have changed or updated its GUID</param>
		/// <param name="ea">The empty event</param>
		private void FavouriteGuidChanged( object sender, EventArgs ea)
		{
			for(int i = 0; i < fj.Length; i++)
			{
				if( fj[i] == sender )
				{
					// Found you!
					userProfile.Properties[ string.Format( ProfileKeys.FAVOURITE, (i+1) ) ].Value = fj[i].Guid;
					Update();
					return;
				}
			}
		}

		/// <summary>
		/// Matches the existing password with the given password.
		/// If the existing password is not encrypted, this will also encrypt it 
		/// and register the new encrypted password with the profile. 
		/// The profile is however not automatically persisted.
		/// </summary>
		/// <param name="password">The password to match against</param>
		/// <returns>True if the passwords matches, false if the password does not match</returns>
		private bool PasswordMatch(String password)
		{
			// Get encrypted password
			string encPwd = (string)userProfile.Properties[ProfileKeys.USER_SECURITY_PASSWORD].Value;
			// Check if existing password is encrypted
			if( !encPwd.StartsWith( PWD_PREFIX ) )
			{	
				// If not - register and encrypt - then continue
				encPwd = RegisterGivenPassword( encPwd );
			}

			string salt = GetSalt( encPwd );
			// Encrypt given password
			string encGivenPwd = PWD_PREFIX + salt +  EncryptionEngine.Encrypt( salt + password );

			// Return check of match
			return encGivenPwd == encPwd;
		}

		/// <summary>
		/// Registers a password with the profile.
		/// This adds salt and encrypts the password properly. It does not persist this change as
		/// a separate call to the Update is needed for persisting.
		/// </summary>
		/// <param name="password">The password to register to the profile</param>
		/// <returns>The encrypted password</returns>
		private string RegisterGivenPassword(string password)
		{
			string salt = GenerateSalt();
			// Ensure that passwords are suffiencetly hard to spot
			string encPwd = PWD_PREFIX + salt + EncryptionEngine.Encrypt( salt + password );
			userProfile.Properties[ProfileKeys.USER_SECURITY_PASSWORD].Value = encPwd;
			return encPwd;
		}

		/// <summary>
		/// Generates the appropriate salt
		/// </summary>
		/// <returns>A suitable salt</returns>
		private string GenerateSalt()
		{
			string salt = string.Empty;
			for( int i = 0; i < SALT_SIZE; i++)
			{
				salt += ALLOWED_SALT_CHARS[random.Next( ALLOWED_SALT_CHARS.Length )];
			}
			return salt;
		}

		/// <summary>
		/// The encryption engine used for encrypting all data
		/// </summary>
		private ITDCrypt EncryptionEngine
		{
			get
			{
				// Get key
				string key = Properties.Current[PROPERTY_PASSWORDKEY];
				string iv = Properties.Current[PROPERTY_PASSWORDIV];				
				return new TDCrypt( key, iv );
			}
		}

		/// <summary>
		/// Extracts the salt from an encrypted password
		/// </summary>
		/// <param name="encryptedPasswd">The encrypted password</param>
		/// <returns>The salt</returns>
		private string GetSalt(string encryptedPasswd)
		{
			return encryptedPasswd.Substring( PWD_PREFIX.Length,SALT_SIZE );
		}
		#endregion

	}
}
