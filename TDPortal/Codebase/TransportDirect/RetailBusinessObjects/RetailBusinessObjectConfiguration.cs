// *********************************************************** 
// NAME                 : RetailBusinessObjectConfiguration.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 23/10/2003 
// DESCRIPTION			: Provides methods to access
// configuration properties of retail business objects.
// ***********************************************************
// $Log

using System;
using System.Collections;
using System.IO;
using TransportDirect.Common; 

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Provides methods to determine retail business object configuration properties.
	/// </summary>
	public class RetailBusinessObjectConfiguration
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public RetailBusinessObjectConfiguration()
		{}

		/// <summary>
		/// Gets the server config filepath as determined from the local config file.
		/// </summary>
		/// <param name="localConfigFilepath">Filepath to the local configuration file.</param>
		/// <param name="errors">Returns any errors encountered.</param>
		/// <returns>The server configuration file path, if no errors encountered.</returns>
		public static string GetServerConfigFilepath(string localConfigFilepath, ArrayList errors)
		{
			int errorsStart = errors.Count;
			Configuration configLocal = null;
			System.IO.StreamReader srLocal = null;
			string serverConfigFilepath = String.Empty;

			try
			{
				srLocal = new System.IO.StreamReader(localConfigFilepath);
				configLocal = new Configuration(srLocal);
			}
			catch (ArgumentException argumentException)
			{
				errors.Add(String.Format(Messages.Pool_FailedToReadIni, localConfigFilepath, argumentException.Message));
			}
			catch (FileNotFoundException fileNotFoundException)
			{
				errors.Add(String.Format(Messages.Pool_FailedToReadIni, localConfigFilepath, fileNotFoundException.Message));
			}
			catch (DirectoryNotFoundException directoryNotFoundException)
			{
				errors.Add(String.Format(Messages.Pool_FailedToReadIni, localConfigFilepath, directoryNotFoundException.Message));
			}
			catch (IOException ioException)
			{
				errors.Add(String.Format(Messages.Pool_FailedToReadIni, localConfigFilepath, ioException.Message));
			}
			catch (TDException tdException)
			{
				errors.Add(String.Format(Messages.Pool_FailedToReadIni, localConfigFilepath, tdException.Message));
			}
			finally
			{
				if (srLocal != null)
					srLocal.Close();
			}
	

			if (errors.Count == errorsStart)
			{
				string localDataFlag = String.Empty;
			
				try
				{
					localDataFlag = configLocal.GetValue("General", "LocalData"); 
				}
				catch (TDException tdException)
				{
					errors.Add(tdException.Message + " IniFilepath: " + localConfigFilepath);
				}

				// In order to support housekeeping it is mandatory that server data is used.
				if (localDataFlag.Equals("Y"))
					errors.Add(String.Format(Messages.Pool_LocalFlagIncorrect, "N"));
			}

			if (errors.Count == errorsStart)
			{
				try
				{
					serverConfigFilepath = configLocal.GetValue("Server", "ServerIni");
				}
				catch (TDException tdException)
				{
					errors.Add(tdException.Message + " IniFilepath: " + serverConfigFilepath);
				}
			}

			return serverConfigFilepath;
		}

		/// <summary>
		/// Gets the housekeeping update filepath as determined from the server config file.
		/// </summary>
		/// <param name="serverConfigFilepath">Filepath to the server configuration file.</param>
		/// <param name="errors">Returns any errors encountered.</param>
		/// <returns>=Update file path.</returns>
		static public string GetUpdateFilepath(string serverConfigFilepath, ArrayList errors)
		{
			int errorsStart= errors.Count;
			string updateFilepath = String.Empty;
			
			Configuration configServer = null;
			System.IO.StreamReader srServer = null;
			
			if (errors.Count == errorsStart)
			{
				try
				{
					srServer = new System.IO.StreamReader(serverConfigFilepath);

					configServer = new Configuration(srServer);
				}
				catch (ArgumentException argumentException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, argumentException.Message));
				}
				catch (FileNotFoundException fileNotFoundException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, fileNotFoundException.Message));
				}
				catch (DirectoryNotFoundException directoryNotFoundException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, directoryNotFoundException.Message));
				}
				catch (IOException ioException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, ioException.Message));
				}
				catch (TDException tdException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, tdException.Message));
				}
				finally
				{
					if (srServer != null)
						srServer.Close();
				}
			}

			if (errors.Count == errorsStart)
			{
				try
				{
					updateFilepath = configServer.GetValue("Update", "UpdateFile");
				}
				catch (TDException tdException)
				{
					errors.Add(tdException.Message + " IniFilepath: " + serverConfigFilepath);
				}
			}

			return updateFilepath;

		}

		/// <summary>
		/// Gets the data sequence as determined from the server config file.
		/// </summary>
		/// <param name="serverConfigFilepath">Filepath to the server configuration file.</param>
		/// <param name="errors">Returns any errors encountered.</param>
		/// <returns>The data sequence, if no errors encountered.</returns>
		static public int GetCurrentDataSequence(string serverConfigFilepath, ArrayList errors)
		{
			int errorsStart= errors.Count;
			int dataSequence = 0;
			
			Configuration configServer = null;
			System.IO.StreamReader srServer = null;
			
			if (errors.Count == errorsStart)
			{
				try
				{
					srServer = new System.IO.StreamReader(serverConfigFilepath);

					configServer = new Configuration(srServer);
				}
				catch (ArgumentException argumentException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, argumentException.Message));
				}
				catch (FileNotFoundException fileNotFoundException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, fileNotFoundException.Message));
				}
				catch (DirectoryNotFoundException directoryNotFoundException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, directoryNotFoundException.Message));
				}
				catch (IOException ioException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, ioException.Message));
				}
				catch (TDException tdException)
				{
					errors.Add(String.Format(Messages.Pool_FailedToReadIni, serverConfigFilepath, tdException.Message));
				}
				finally
				{
					if (srServer != null)
						srServer.Close();
				}
			}

			if (errors.Count == errorsStart)
			{
				try
				{
					dataSequence = int.Parse(configServer.GetValue("General", "SeqNo"));
				}
				catch (TDException tdException)
				{
					errors.Add(tdException.Message + " IniFilepath: " + serverConfigFilepath);
				}
			}

			return dataSequence;
		}
	}
}
