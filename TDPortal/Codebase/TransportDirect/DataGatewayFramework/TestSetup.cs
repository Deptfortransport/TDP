//********************************************************************************
//NAME         : TestSetup.cs
//AUTHOR       : Jonathan George
//DATE CREATED : 02/04/2004
//DESCRIPTION  : Class to assist NUnit test classes in this project
//********************************************************************************

using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;
using System.DirectoryServices;
using Logger = System.Diagnostics.Trace;
using System.Diagnostics;

namespace TransportDirect.Datagateway.Framework
{
	#region IServiceInitialisation class for the tests

	/// <summary>
	/// This is required by the property service for this test module. It is instantiated once in the test constructor.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			// Enable the Event Logging Service
			
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				Assert.IsTrue(tdEx.Message.Length == 0, ("Test Initialisation Exception " + tdEx.Message));
				return;
			}
		}
	}

	#endregion

	/// <summary>
	/// Setup routines to initialise data, users, groups and folders for use with other test objects
	/// </summary>
	public sealed class TestSetup
	{

		#region Private variables

		private static string receptionPath;
		private static string incomingPath;
		private static string holdingPath;
		private static string processingPath;
		private static string backupPath;
		private static string ipAddress;

		private static SqlCommand cm;
		private static SqlConnection connection;

		// Setup/Teardown variables used to track changes that were made
		private static ArrayList createdFolders;
		private static ArrayList createdUsers;
		private static ArrayList createdData;
		private static ArrayList createdGroups;
		private static IPropertyProvider currProps;
		private static string nodeSchemaUser = "user", nodeSchemaGroup = "group";
		private static bool initialised = false;

		#endregion

		#region Constructor

		static TestSetup()
		{
			// Initialise service discovery
			TDServiceDiscovery.Init(new TestInitialization());
			currProps = Properties.Current;
			connection = new SqlConnection(currProps["datagateway.test.dbconnectionstring"]);	
		}

		#endregion

		#region Public methods

		public static void Setup()
		{
			if (!initialised)
			{
				createdFolders = new ArrayList();
				createdUsers = new ArrayList();
				createdGroups = new ArrayList();
				createdData = new ArrayList();

				ipAddress      = System.Environment.MachineName;

				// At this point, the clearup would be able to run
				initialised = true;

				receptionPath  = currProps["Gateway.ReceptionPath"];
				incomingPath   = currProps["Gateway.IncomingPath"];
				holdingPath    = currProps["Gateway.HoldingPath"];
				processingPath = currProps["Gateway.ProcessingPath"];
				backupPath     = currProps["Gateway.BackupPath"];

				//AddUsers();
				AddDirectories();
				AddData();
			}
		}

		public static void TearDown()
		{
			if (initialised)
			{
				initialised = false;
				RemoveFeeds();
				RemoveData();
			}
		}

		#endregion

		#region Private methods to create/remove users, directories and data

		/// <summary>
		/// Translation of the DataGatewayAddFeeds and FTPAddFeeds VBScripts used previously
		/// </summary>
		private static void AddUsers()
		{
			// Go through the feeds creating the folders
			int datasetCount = Convert.ToInt32( currProps["datagateway.test.feeds.count"] );
			string sUserID, sFullName, sDescription, sGroups, sPassword;

			DirectoryEntry directory = new DirectoryEntry("WinNT://" + ipAddress), entry = null, group = null;
			string[] groups;

			for ( int i = 1; i <= datasetCount; i++ )
			{
				// Get the user information from this row.
				sUserID = currProps[String.Format("datagateway.test.feeds.{0}.userid", i.ToString())];
				sFullName = currProps[String.Format("datagateway.test.feeds.{0}.fullname", i.ToString())];
				sDescription = currProps[String.Format("datagateway.test.feeds.{0}.description", i.ToString())];
				sGroups = currProps[String.Format("datagateway.test.feeds.{0}.groups", i.ToString())];

				// Make up a password
				sPassword = sUserID.Substring(0, 2) + DateTime.Now.Minute.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Second.ToString();

				// See if the user account exists and remove it if it does
				try
				{
					entry = directory.Children.Find(sUserID, nodeSchemaUser);
					directory.Children.Remove(entry);
				}
					//				catch (System.Runtime.InteropServices.COMException e) { }
				catch (Exception e) 
				{
					string temp = e.Message;
				}
			
				// Now create the new user
				entry = directory.Children.Add(sUserID, nodeSchemaUser);
				entry.CommitChanges();

				entry.Invoke("SetPassword", new Object[] {sPassword});

				entry.Properties["Description"].Add(sDescription);
				entry.Properties["FullName"].Add(sFullName);
				entry.Properties["HomeDirectory"].Add(receptionPath + sUserID);
				entry.CommitChanges();
				// Add user to groups
				groups = sGroups.Split(";".ToCharArray());

				for (int groupCount = 0; groupCount < groups.Length; groupCount++ )
				{
					group = null;
					try
					{
						group = directory.Children.Find(groups[groupCount], nodeSchemaGroup);
					}
						//					catch (System.Runtime.InteropServices.COMException e) { }
					catch (Exception) { }
					if (group == null)
					{
						// Create the group
						group = directory.Children.Add(groups[groupCount], nodeSchemaGroup);
						group.CommitChanges();
						createdGroups.Add(groups[groupCount]);
					}

					group.Invoke("Add", new Object[] { entry.Path });
				}

				createdUsers.Add(sUserID);

			}

		}

		/// <summary>
		/// Translation of the DataGatewayAddFeeds and FTPAddFeeds VBScripts used previously
		/// </summary>
		private static void AddDirectories()
		{
			// Get the user information from this row. This used to be loaded from Feedsconfig.csv

			// Create new standard folders as necessary
			int datasetCount = Convert.ToInt32( currProps["datagateway.test.standardfolders.count"] );
			string standardFolderCurr;
			for ( int i = 1; i <= datasetCount; i++ )
			{
				// Get the name and check if it exists
				standardFolderCurr = currProps["datagateway.test.standardfolders." + i.ToString()];
				if (!Directory.Exists(standardFolderCurr))
					// Create the folder and add to the list of created folders
					createdFolders.Add(Directory.CreateDirectory(standardFolderCurr));
			}

			// Go through the feeds creating the folders
			datasetCount = Convert.ToInt32( currProps["datagateway.test.feeds.count"] );
			string sUserID;

			for ( int i = 1; i <= datasetCount; i++ )
			{
				// Get the user information from this row.
				sUserID = currProps[String.Format("datagateway.test.feeds.{0}.userid", i.ToString())];

				// Create the user's directories
				if (!Directory.Exists(incomingPath + sUserID))
					createdFolders.Add(Directory.CreateDirectory(incomingPath + sUserID));
				if (!Directory.Exists(processingPath + sUserID))
					createdFolders.Add(Directory.CreateDirectory(processingPath + sUserID));
				if (!Directory.Exists(holdingPath + sUserID))
					createdFolders.Add(Directory.CreateDirectory(holdingPath + sUserID));
				if (!Directory.Exists(backupPath + sUserID))
					createdFolders.Add(Directory.CreateDirectory(backupPath + sUserID));
				
				// Create the users home directory
				DirectoryInfo homeDirectory;
				if (!Directory.Exists(receptionPath + sUserID))
					createdFolders.Add(Directory.CreateDirectory(receptionPath + sUserID));
				homeDirectory = new DirectoryInfo(receptionPath + sUserID);

			}

		}

		private static void AddData()
		{
			// Read test data from properties and insert into the database
			int dataRowCount = Convert.ToInt32(currProps["datagateway.test.ftpconfigdata.count"]);
			cm = new SqlCommand("insert into ftp_configuration ( ftp_client, data_feed, ip_address, username, password, local_dir, remote_dir, filename_filter, missing_feed_counter, missing_feed_threshold, data_feed_datetime, data_feed_filename, remove_files) values (@ftp_client, @data_feed, @ip_address, @username, @password, @local_dir, @remote_dir, @filename_filter, @missing_feed_counter, @missing_feed_threshold, @data_feed_datetime, @data_feed_filename, @remove_files)", connection);
			cm.CommandType = CommandType.Text;

			cm.Parameters.Add("@ftp_client", SqlDbType.Int);
			cm.Parameters.Add("@data_feed", SqlDbType.VarChar, 16);
			cm.Parameters.Add("@ip_address", SqlDbType.VarChar, 16);
			cm.Parameters.Add("@username", SqlDbType.VarChar, 32);
			cm.Parameters.Add("@password", SqlDbType.VarChar, 32);
			cm.Parameters.Add("@local_dir", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@remote_dir", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@filename_filter", SqlDbType.VarChar, 32);
			cm.Parameters.Add("@missing_feed_counter", SqlDbType.Int);
			cm.Parameters.Add("@missing_feed_threshold", SqlDbType.Int);
			cm.Parameters.Add("@data_feed_datetime", SqlDbType.VarChar, 23);
			cm.Parameters.Add("@data_feed_filename", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@remove_files", SqlDbType.Bit);
			
			string[] dataItems;
			for (int i = 1; i <= dataRowCount; i++)
			{
				dataItems = currProps["datagateway.test.ftpconfigdata." + i.ToString()].Split("|".ToCharArray());
				cm.Parameters["@ftp_client"].Value = dataItems[0].Length == 0 ? cm.Parameters[0].Value = DBNull.Value : Convert.ToInt32(dataItems[0]);
				cm.Parameters["@data_feed"].Value = dataItems[1];
				cm.Parameters["@ip_address"].Value = dataItems[2];
				cm.Parameters["@username"].Value = dataItems[3];
				cm.Parameters["@password"].Value = dataItems[4];
				cm.Parameters["@local_dir"].Value = dataItems[5];
				cm.Parameters["@remote_dir"].Value = dataItems[6];
				cm.Parameters["@filename_filter"].Value = dataItems[7];
				cm.Parameters["@missing_feed_counter"].Value = dataItems[8].Length == 0 ? cm.Parameters[8].Value = DBNull.Value : Convert.ToInt32(dataItems[8]);
				cm.Parameters["@missing_feed_threshold"].Value = dataItems[9].Length == 0 ? cm.Parameters[9].Value = DBNull.Value : Convert.ToInt32(dataItems[9]);
				cm.Parameters["@data_feed_datetime"].Value = dataItems[10];
				cm.Parameters["@data_feed_filename"].Value = dataItems[11];
				cm.Parameters["@remove_files"].Value = dataItems[12].Length == 0 ? cm.Parameters[12].Value = DBNull.Value : Convert.ToSByte(dataItems[12]);
				cm.Connection.Open();
				try
				{
					cm.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					string temp;
					temp = e.Message;
				}
				cm.Connection.Close();
				createdData.Add(dataItems[1]);
			}

			// Read test data from properties and insert into the database
			dataRowCount = Convert.ToInt32(currProps["datagateway.test.importconfigdata.count"]);
			cm = new SqlCommand("insert into import_configuration (data_feed, import_class, class_archive, import_utility, parameters1, parameters2, processing_dir) values (@data_feed, @import_class, @class_archive, @import_utility, @parameters1, @parameters2, @processing_dir)", connection);
			cm.CommandType = CommandType.Text;
			cm.Parameters.Add("@data_feed", SqlDbType.VarChar, 16);
			cm.Parameters.Add("@import_class", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@class_archive", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@import_utility", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@parameters1", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@parameters2", SqlDbType.VarChar, 128);
			cm.Parameters.Add("@processing_dir", SqlDbType.VarChar, 128);
			
			for (int i = 1; i <= dataRowCount; i++)
			{
				dataItems = currProps["datagateway.test.importconfigdata." + i.ToString()].Split("|".ToCharArray());
				cm.Parameters["@data_feed"].Value = dataItems[0];
				cm.Parameters["@import_class"].Value = dataItems[1];
				cm.Parameters["@class_archive"].Value = dataItems[2];
				cm.Parameters["@import_utility"].Value = dataItems[3];
				cm.Parameters["@parameters1"].Value = dataItems[4];
				cm.Parameters["@parameters2"].Value = dataItems[5];
				cm.Parameters["@processing_dir"].Value = dataItems[6];
				cm.Connection.Open();
				cm.ExecuteNonQuery();
				cm.Connection.Close();
				createdData.Add(dataItems[0]);
			}
		}

		private static void RemoveFeeds()
		{
			if (createdUsers.Count + createdGroups.Count > 0)
			{
				DirectoryEntry directory = new DirectoryEntry("WinNT://" + ipAddress), entry;
				foreach (object o in createdUsers)
				{
					// Delete the user
					try
					{
						entry = directory.Children.Find(o.ToString(), nodeSchemaUser);
						directory.Children.Remove(entry);
					}
					catch (Exception) { }
				}

				foreach (object o in createdGroups)
				{
					// Delete the group
					try
					{
						entry = directory.Children.Find(o.ToString(), nodeSchemaGroup);
						directory.Children.Remove(entry);
					}
					catch (Exception) { }
				}
			}

			DirectoryInfo info;
			foreach (object o in createdFolders)
			{
				try
				{
					info = (DirectoryInfo)o;
					if (info.Exists)
						info.Delete(true);
				}
				catch (Exception) { }
			}
		}

		private static void RemoveData()
		{
			SqlCommand cm = new SqlCommand("delete from ftp_configuration where data_feed = @data_feed", connection);
			cm.CommandType = CommandType.Text;
			cm.Parameters.Add("@data_feed", SqlDbType.VarChar, 16);
			foreach (object o in createdData)
			{
				cm.Parameters["@data_feed"].Value = (string)o;
				connection.Open();
				cm.ExecuteNonQuery();
				connection.Close();
			}

			cm = new SqlCommand("delete from import_configuration where data_feed = @data_feed", connection);
			cm.CommandType = CommandType.Text;
			cm.Parameters.Add("@data_feed", SqlDbType.VarChar, 16);
			foreach (object o in createdData)
			{
				cm.Parameters["@data_feed"].Value = (string)o;
				connection.Open();
				cm.ExecuteNonQuery();
				connection.Close();
			}
		}
		
		
		#endregion

		#region Static properties

		public static string ReceptionPath
		{
			get { return receptionPath; }
		}
		public static string IncomingPath
		{
			get { return incomingPath; }
		}

		public static string HoldingPath
		{
			get { return holdingPath; }
		}

		public static string ProcessingPath
		{
			get { return processingPath; }
		}

		public static string BackupPath
		{
			get { return backupPath; }
		}

		public static string IpAddress
		{
			get { return ipAddress; }
		}

		public static bool Initialised
		{
			get { return initialised; }
		}

		#endregion
	}
}
