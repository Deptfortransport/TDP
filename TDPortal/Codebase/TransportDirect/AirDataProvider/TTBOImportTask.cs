//********************************************************************************
//NAME         : TTBOImportTask.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 13/01/2004
//DESCRIPTION  : Implementation of TTBOImportTask class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/TTBOImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:26   mturner
//Initial revision.
//
//   Rev 1.2   Sep 29 2004 12:46:36   jgeorge
//Updates to allow more than 2 CJP/Gateway servers to be updated for Air and Rail TTBO imports
//
//   Rev 1.1   Aug 13 2004 20:22:04   CHosegood
//Intermediate check in to compile against new CJP6.0.0.0
//
//   Rev 1.0   Jun 09 2004 17:41:48   CHosegood
//Initial revision.
//
//   Rev 1.0   Jan 13 2004 17:54:28   acaunt
//Initial Revision

using System;
using System.Collections;
using System.IO;
//using System.Net;
using System.Runtime.Remoting;
//using System.Runtime.Remoting.Channels;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Data Gateway import task invoked via the Data Gateway framework.
	/// Its purpose is to update the TTBO data on the Gateway and CJP servers.
	/// The Gateway servers are updated by invoking the update method on the TTBOHost service, while the 
	/// CJP servers are updated by invoking the equivalent method on the CJP service.
	/// </summary>
	public class TTBOImportTask : ImportTask
	{
        #region Instance Members

		/// <summary>
        /// Absoluate path of the file
        /// </summary>
        private string updateFilePath;
        /// <summary>
        /// Counters to track attempts and successes
        /// </summary>
        private int updatesRequired = 0;
        private int successes = 0;
        private int attempts = 0;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor, passthrough to ImportTask
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task ** NOT USED **</param>
        /// <param name="params2">Parameters passed to the task ** NOT USED **</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
		public TTBOImportTask(string feed, string params1, string params2, 
			string utility, string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{ }

		#endregion

		#region Overridden protected methods
		/// <summary>
		/// Override the base class PerformTask method. This method calls the local
		/// methods to load, validate and import the XML file into the database.
		/// </summary>
		/// <returns></returns>
		protected override int PerformTask()
		{
			// Set the full name of the file
			updateFilePath = ProcessingDirectory + importFile;

			// Assume that everyhing will be successful.
			int result = 0;

            if (dataFeed == null)
                result = (int) TDExceptionIdentifier.DGUnexpectedFeedName;
            else if ( dataFeed.Equals( Properties.Current[ Keys.RailFeedName ] ) )
                result = PerformRailUpdate();
            else if ( dataFeed.Equals( Properties.Current[ Keys.AirFeedName ] ) )
                result = PerformAirUpdate();
            else
                result = (int) TDExceptionIdentifier.DGUnexpectedFeedName;

			return result;
		}

		/// <summary>
		/// Override of LogStart method
		/// </summary>
		protected override void LogStart()
		{
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, String.Format(Messages.TTBOImportStart, dataFeed, TDDateTime.Now.ToString())));
		}

		/// <summary>
		/// Override of LogFinish method
		/// </summary>
		protected override void LogFinish(int retCode)
		{
			// Only log failure for instigating housekeeping. 
			// If housekeeping started then  we rely on the RetailBusinessObjects themselves to report on its success or failure.
			if (successes == updatesRequired) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, String.Format(Messages.TTBOImportFinishAllSuccess, successes, updatesRequired, TDDateTime.Now)));
			}
			else
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportFinishFailure, retCode, attempts, successes, (updatesRequired - attempts), TDDateTime.Now)));
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Copy the update file into the correct directory and invoke the appropriate method on the RetailBusinessObjectFacade
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private int UpdateBusinessObjects(ServerDetails parameters, IRemoteTTBOUpdateCommand command)
		{
			// Increment number of attempts
			++attempts;
			// Assume that everything will be successful
			int result = 0;

			// Extract the parameters
			string targetFile = parameters.FileLocation;
			string targetUrl = parameters.RemoteObject;
			// All this is in a large try-catch block to ensure that all exceptions are caught.
			try 
			{
				if (TDTraceSwitch.TraceVerbose)
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Copying file from [{0}] to [{1}] for processing", updateFilePath, targetFile)));

				// Copy the file into the appropriate directory for the update to begin
				File.Copy(updateFilePath, targetFile, true);

				// Obtain a proxy for the remote object
				if (command.Init(targetUrl))
				{
				if (TDTraceSwitch.TraceVerbose)
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Calling command.Update() for current remote object"));

						// Invoke the proxy to perform the update. If it fails, map this to an error
					result = command.Update();
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
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportUpdateFailed, e.Message) ,e));
			}
			catch (FileNotFoundException e)
			{
				result = (int)TDExceptionIdentifier.PRHImportIOException;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportUpdateFailed, e.Message) ,e));
			}
			catch (IOException e)
			{
				result = (int)TDExceptionIdentifier.PRHImportIOException;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportUpdateFailed, e.Message) ,e));
			}
			catch (RemotingException e)
			{
				result = (int)TDExceptionIdentifier.PRHServerUnavailable;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportUpdateFailed, e.Message) ,e));
			}
			catch (Exception e)
			{
				result = (int)TDExceptionIdentifier.PRHUnexpectedImportException;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportUpdateFailed, e.Message) ,e));
			}

			
			// Log the result for the individualupdate and update successes as appropriate
			if (result == 23) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, String.Format(Messages.TTBOImportCommandSuccess, command.Id())));
				++successes;
			}
			else
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.TTBOImportCommandFailure, command.Id(), result)));		
			}
			return result;
		}

        /// <summary>
        /// Performs an Rail TTBO update on the CJP server(s) and returns the result
        /// </summary>
        /// <returns>Returns the result code of the update, 0 is success</returns>
        private int PerformRailUpdate() 
        {
            int result = 0;

			ServerDetails[] servers;
			try
			{
				servers = TTBOImportProperties.ReadServersForFeed(Properties.Current, Keys.Rail);
			}
			catch (TDException e)
			{
                return (int)e.Identifier;
			}

			// Determine how many updates are required
			updatesRequired += servers.Length;

			// Attempts updates as required until all are complete or one fails.
			foreach (ServerDetails server in servers)
			{
				if (server.Type == ServerType.CJP)
					result = UpdateBusinessObjects(server, new RemoteCJPTTBOUpdateCommand(server.Id));
				else
					result = UpdateBusinessObjects(server, new RemoteGWTTBOUpdateCommand(server.Id));

				// 23 indicates success; if this is not returned, exit the loop
				if (result != 23)
					break;
			}

            // Map a result of 23 (success) to 0 for TNG to understand
            if (result == 23 ) 
                result = 0;

            return result;
		}
        /// <summary>
        /// Performs an Air TTBO update on the CJP server(s) and returns the result
        /// </summary>
        /// <returns>Returns the result code of the update, 0 is success</returns>
        private int PerformAirUpdate() 
        {
			int result = 0;

			ServerDetails[] servers;

			try
			{
				servers = TTBOImportProperties.ReadServersForFeed(Properties.Current, Keys.Air);
			}
			catch (TDException e)
			{
				return (int)e.Identifier;
			}

			// Determine how many updates are required
			updatesRequired += servers.Length;

			// Attempts updates as required until all are complete or one fails.
			foreach (ServerDetails server in servers)
			{
				if (server.Type == ServerType.CJP)
					result = UpdateBusinessObjects(server, new RemoteCJPAirTTBOUpdateCommand(server.Id));
				else
					result = UpdateBusinessObjects(server, new RemoteGWAirTTBOUpdateCommand(server.Id));

				// 23 indicates success; if this is not returned, exit the loop
				if (result != 23)
					break;
			}

			// Map a result of 23 (success) to 0 for TNG to understand
			if (result == 23 ) 
				result = 0;

			return result;
        }

		#endregion
	}
}