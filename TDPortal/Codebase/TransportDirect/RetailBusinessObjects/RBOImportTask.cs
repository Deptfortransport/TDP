//********************************************************************************
//NAME         : RBOImportTask.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 28/10/2003
//DESCRIPTION  : Implementation of RBOImportTask class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RBOImportTask.cs-arc  $
//
//   Rev 1.2   Jan 11 2009 18:16:12   mmodi
//Removed debug line
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.1   Jan 11 2009 17:12:18   mmodi
//Updated for ZPBO datafeed
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:14   mturner
//Initial revision.
//
//   Rev 1.8   Feb 17 2006 17:15:08   aviitanen
//Merge from Del8.0 to  8.1
//
//   Rev 1.7   Feb 10 2005 17:38:18   RScott
//Updated to include Reservation and Supplement Business Objects (RVBO, SBO)
//
//   Rev 1.6   Dec 17 2003 16:07:46   COwczarek
//In UpdateBusinessObjects, call File.Copy with overwrite parameter set to true to force overwrite if destination file exists
//Resolution for 445: RBOImport aborts if attempting to overwrite a file
//
//   Rev 1.5   Dec 17 2003 15:51:18   COwczarek
//UpdateBusinessObjects method now logs exceptions before returning error code
//Resolution for 446: Potential to import RBOImport logging
//
//   Rev 1.4   Dec 17 2003 14:34:40   COwczarek
//Change severity of event from Error to Info in LogStart method
//Resolution for 443: Logging in RBOImport at wrong level
//
//   Rev 1.3   Nov 20 2003 16:53:22   acaunt
//Feed names supported are now retrieved from the properties service
//
//   Rev 1.2   Oct 30 2003 10:56:26   acaunt
//Now uses TDExceptionIdentifier enum
//
//   Rev 1.1   Oct 29 2003 18:27:04   acaunt
//Checks feed name validity early now rather than late
//
//   Rev 1.0   Oct 29 2003 12:09:52   acaunt
//Initial Revision

using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Data Gateway import task which calls the RetailBusinessObjects facade to instigate housekeeping
	/// </summary>
	public class RBOImportTask : ImportTask
	{
		#region Static Properties
		// Delimeter for the parameters that we passin
		private const char PARAM_DELIMITER = ' ';

		// The different feed names that the task supports
		private string LBO_UPDATE;
		private string RBO_UPDATE;
		private string FBO_UPDATE;
		private string RVBO_UPDATE;
		private string SBO_UPDATE;
        private string ZPBO_UPDATE;

		// Absoluate path of the file
		private string updateFilePath;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor, pass
		/// </summary>
		public RBOImportTask(string feed, string params1, string params2, 
			string utility, string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
		}
		#endregion

		#region Overridden protected methods
		/// <summary>
		/// Override the base class PerformTask method. This method calls the local
		/// methods to load, validate and import the XML file into the database.
		/// </summary>
		/// <returns></returns>
		protected override int PerformTask()
		{
            // Obtain the names of the retail business object feeds
			FBO_UPDATE  = Properties.Current["datagateway.retailbusinessobjects.fbofeedname"];
			RBO_UPDATE  = Properties.Current["datagateway.retailbusinessobjects.rbofeedname"];
			LBO_UPDATE  = Properties.Current["datagateway.retailbusinessobjects.lbofeedname"];
			RVBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.rvbofeedname"];
			SBO_UPDATE  = Properties.Current["datagateway.retailbusinessobjects.sbofeedname"];
            ZPBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.zpbofeedname"];

			// Set the full name of the file
			updateFilePath = ProcessingDirectory + importFile;
			
			// Assume that everything will be successful.
			int result = 0;

			// Check that we have a valid feed name
            if (dataFeed.Equals(FBO_UPDATE) || dataFeed.Equals(RBO_UPDATE) || dataFeed.Equals(LBO_UPDATE) || dataFeed.Equals(RVBO_UPDATE) || dataFeed.Equals(SBO_UPDATE) || dataFeed.Equals(ZPBO_UPDATE))
			{
				ServerDetails[] serverDetails = RBOImportProperties.ReadAllServers(Properties.Current);

				if	(serverDetails.Length == 0) 
				{
					result = (int)TDExceptionIdentifier.PRHInvalidImportParameters;
				}

				int lastResult = 0;

				if	(result == 0)
				{
					foreach (ServerDetails detail in serverDetails)
					{
						lastResult = UpdateBusinessObjects(detail, parameters1);
						
						if	(lastResult > 0)
						{
							result = lastResult;
						}
					}
				}
			} 
			else 
			{
				result = (int)TDExceptionIdentifier.PRHUnrecognisedImportFeed;

			}

			return result;
	}

		/// <summary>
		/// Override of LogStart method
		/// </summary>
		protected override void LogStart()
		{
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Retail Business Object update initialise begun for feed "
					+dataFeed+" at "+TDDateTime.Now));			
		}

		/// <summary>
		/// Override of LogFinish method
		/// </summary>
		protected override void LogFinish(int retCode)
		{
			// Only log failure for instigating housekeeping. 
			// If housekeeping started then  we rely on the RetailBusinessObjects themselves to report on its success or failure.
			if (retCode != 0) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Retail Business Object update failed to initialise for feed "
					+dataFeed+" at "+TDDateTime.Now+". Error Code: "+retCode));			
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Copy the update file into the correct directory and invoke the appropriate method on the RetailBusinessObjectFacade
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private int UpdateBusinessObjects(ServerDetails serverDetails, string parameters)
		{
			// Assume that everything will be successful
			int result = 0;

			try 
			{
				// the destination for the file copy consists of the feed-specific
				// file name (from the parameters) appended to the server-specific 
				// file path (read from properties for the current server). 
				string fileDestination = serverDetails.FileLocation + parameters; 

				// Copy the file into the appropriate directory for the update to begin
				File.Copy(updateFilePath, fileDestination, true);
                
				// Obtain a proxy for the RetailBusinessObjectFacade
				RetailBusinessObjectsFacade proxy = (RetailBusinessObjectsFacade)RemotingServices.Connect(
					typeof(RetailBusinessObjectsFacade), serverDetails.RemoteObject);
				
			
				// Check that a proxy has been returned
				if (proxy != null) 
				{
					// Specify Windows Integrated Authentication credentials
					IDictionary Props = ChannelServices.GetChannelSinkProperties(proxy);
					Props["credentials"] = CredentialCache.DefaultCredentials;
					// Invoke the RetailBusinessObjectFacade to begin the update. If it fails, map this to an error
					if (!StartHousekeeping(proxy)) 
					{
						result = (int)TDExceptionIdentifier.PRHUnableToBeginHousekeeping;
					}
				} 
				    // Return an error code
                else
                {
                    result = (int)TDExceptionIdentifier.PRHServerUnavailable;
                }
			} 
				// Map any exceptions into an error code
			catch (DirectoryNotFoundException e)
			{
				result = (int)TDExceptionIdentifier.PRHImportIOException;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Updating business object failed: " + e.Message,e));
			}
			catch (FileNotFoundException e)
			{
				result = (int)TDExceptionIdentifier.PRHImportIOException;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Updating business object failed: " + e.Message,e));
			}
			catch (IOException e)
			{
				result = (int)TDExceptionIdentifier.PRHImportIOException;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Updating business object failed: " + e.Message,e));
			}
			catch (RemotingException e)
			{
				result = (int)TDExceptionIdentifier.PRHServerUnavailable;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Updating business object failed: " + e.Message,e));
			}
			catch (Exception e)
			{
				result = (int)TDExceptionIdentifier.PRHUnexpectedImportException;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Updating business object failed: " + e.Message,e));
			}
			
			return result;
		}

		/// <summary>
		/// Invoke the appropriate housekeeping method on a RetailBusinessObjectFacade, based on the utility name specified
		/// </summary>
		/// <param name="proxy"></param>
		/// <returns></returns>
		private bool StartHousekeeping(RetailBusinessObjectsFacade proxy)
		{
			// Assume the best
			bool success = true;
			if (dataFeed.Equals(FBO_UPDATE))
			{
				success = proxy.InitiateFBOHousekeeping(dataFeed);
			}
			else if (dataFeed.Equals(RBO_UPDATE))
			{
				success = proxy.InitiateRBOHousekeeping(dataFeed);
			}
			else if (dataFeed.Equals(LBO_UPDATE))
			{
				success = proxy.InitiateLBOHousekeeping(dataFeed);
			}
			else if (dataFeed.Equals(RVBO_UPDATE))
			{
				success = proxy.InitiateRVBOHousekeeping(dataFeed);
			}
			else if (dataFeed.Equals(SBO_UPDATE))
			{
				success = proxy.InitiateSBOHousekeeping(dataFeed);
			}
            else if (dataFeed.Equals(ZPBO_UPDATE))
            {
                success = proxy.InitiateZPBOHousekeeping(dataFeed);
            }
			else 
			{
				// Unrecognised feed name. This should have been dealt with already
				success = false;
			}
			return success;
		}
		#endregion
	}
}

