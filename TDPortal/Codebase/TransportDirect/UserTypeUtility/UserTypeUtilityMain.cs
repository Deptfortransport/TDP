// ************************************************************ 
// NAME                 : UserTypeUtilityMain
// AUTHOR               : Jonathan George
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Main class for the UserTypeUtility
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserTypeUtility/UserTypeUtilityMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:04   mturner
//Initial revision.
//
//   Rev 1.1   Jul 06 2004 15:46:00   jgeorge
//Updated commenting
//
//   Rev 1.0   Jul 02 2004 13:47:40   jgeorge
//Initial revision.

using System;
using System.IO;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager.UserTypeUtility
{
	/// <summary>
	/// Contains all processing code for UserTypeUtilityMain.
	/// </summary>
	class UserTypeUtilityMain
	{
		/// <summary>
		/// Performs the requested action
		/// </summary>
		/// <param name="setValue">true if the type of the user should be updated</param>
		/// <param name="userName">name of the user</param>
		/// <param name="valueInt">new type - only used if setValue == true</param>
		/// <returns></returns>
		private static void RunUserUpdateUtility(bool setValue, string userName, TDUserType userType)
		{
			TDUser user = new TDUser();
			if (user.FetchUser(userName))
			{
				Console.WriteLine(String.Format(Messages.User_CurrentValue, userName, user.UserType.ToString()));
				if (setValue)
				{
					user.UserType = userType;
					user.UserProfile.Update();
					Console.WriteLine(String.Format(Messages.User_NewValue, userName, user.UserType.ToString()));
				}
			}
			else
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.User_NotFound, userName)));
				throw new TDException(String.Format(Messages.User_NotFound, userName), true, TDExceptionIdentifier.SMUserTypeUtilityUserNotFound);
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			int returnCode = 0;
			bool continueToMainProcessing = false;

			bool setValue = false;
			string userName = string.Empty;
			string propertyName = string.Empty;
			TDUserType userType = TDUserType.Standard;

			try
			{
				if ((args.Length) > 0 && (args[0].Trim().Length > 0) )
				{
					// Possible args are:
					// /help - prints help info
					// /test - initialises then ends
					// /set:[username]:[value]
					// /get:[username]
					if (String.Compare(args[0], "/help", true ) == 0)
					{
						Console.WriteLine(String.Format(Messages.Init_Usage, string.Join(", ", Enum.GetNames(typeof(TDUserType))) ));
						returnCode = 0;
					}
					else if (String.Compare( args[0], "/test", true ) == 0)
					{
						TDServiceDiscovery.Init(new UserUpdateUtilityInitialisation());

						returnCode = 0;

						if (TDTraceSwitch.TraceInfo)
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Util_TestSucceeded));

						Console.WriteLine(Messages.Util_TestSucceeded);

					}
					else
					{
						string[] splitArgs = args[0].Split(":".ToCharArray());
						if ((String.Compare( splitArgs[0], "/get", true ) == 0) && (splitArgs.Length == 2))
						{
							setValue = false;
							userName = splitArgs[1];
							continueToMainProcessing = true;
						}
						else if ((String.Compare( splitArgs[0], "/set", true ) == 0) && (splitArgs.Length == 3))
						{
							setValue = true;
							userName = splitArgs[1];
							string valueString = splitArgs[2];

							// Validate valueString
							try
							{
								userType = (TDUserType)Enum.Parse(typeof(TDUserType), valueString, true);
							}
							catch (ArgumentException e)
							{
								OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.Util_InvalidType, valueString));
								Trace.Write(oe);
								throw new TDException(oe.Message, e, oe.PublishedBy.Length != 0, TDExceptionIdentifier.SMUserTypeUtilityInvalidArg);
							}


							continueToMainProcessing = true;
						}
						else
						{
							// error
							Trace.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, Messages.Util_InvalidArg));
                            throw new TDException(Messages.Util_InvalidArg, true, TDExceptionIdentifier.SMUserTypeUtilityInvalidArg);
						}



					}
				}

				if ( continueToMainProcessing )
				{
					TDServiceDiscovery.Init(new UserUpdateUtilityInitialisation());

					RunUserUpdateUtility(setValue, userName, userType);

					returnCode = 0;

					if ((TDTraceSwitch.TraceInfo))
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Util_Completed));
				}
			}
			catch (TDException tdEx)
			{
				// Log error (cannot assume that TD listener has been initialised)
				if (!tdEx.Logged)
				{
					Trace.Write(String.Format(Messages.Util_Failed, tdEx.Message, tdEx.Identifier));
				}
				Console.Write(String.Format(Messages.Util_Failed, tdEx.Message, tdEx.Identifier));

				returnCode = (int)tdEx.Identifier;
			}

			return returnCode;

		}
	}
}
