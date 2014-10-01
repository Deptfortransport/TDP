// ***********************************************
// NAME 		: FileCopyTask.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 10/05/2004
// DESCRIPTION 	: Implementation of class FileCopyTask
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/FileCopyTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:10   mturner
//Initial revision.
//
//   Rev 1.2   Aug 27 2004 11:40:14   CHosegood
//Added verbose logging and now using processing directory as source dir for copy/move.
//
//   Rev 1.1   Jun 07 2004 17:41:24   CHosegood
//Now param1 = destination, param2 = move/copy.
//
//   Rev 1.0   May 17 2004 16:58:06   AWindley
//Initial revision.


using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Data Gateway import task invoked via the Data Gateway framework.
	/// Its purpose is to provide a general means for copying files
	/// around the file system.
	/// </summary>
	public class FileCopyTask : ImportTask
	{

		#region Static properties
		// Delimeter for the parameters passed in
		private const char PARAM_DELIMITER = ' ';
		#endregion
		
		#region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">The destination file to copy/move to</param>
        /// <param name="params2">Move or Copy</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
		public FileCopyTask(string feed, string params1, string params2, string utility, string processingDirectory) 
					: base(feed, params1, params2, utility, processingDirectory)
		{
		}
		#endregion
		
		#region Overridden protected methods
		/// <summary>
		/// Override the base class PerformTask method. This method calls the local
		/// methods to copy/move the file to its new location.
		/// </summary>
		/// <returns>An int representing the return status</returns>
		protected override int PerformTask()
		{
	
			// Assume that everyhing will be successful.
			int result = 0;

			// Validate the parameters for the primary server
			bool validParams = IsValidParameter();
			
			// Only continue if we have valid parameters
			if (validParams)
			{
				// Perform move/copy
				result = PerformCopy(parameters1);
			}
			else 
			{
				// Set error code to indicate invalid parameters
				result = (int)TDExceptionIdentifier.DGFileCopyInvalidParameters;
			}
		 
			return result;
		}

		/// <summary>
		/// Override of LogStart method
		/// </summary>
		protected override void LogStart()
		{
	        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
	               		"File Copy Task begun for feed "+dataFeed));			
		}

		/// <summary>
		/// Override of LogFinish method
		/// </summary>
		protected override void LogFinish(int retCode)
		{
			if (retCode != 0) 
			{
				// Log failure
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"File Copy Task failed for feed " +dataFeed+ ". Error Code: "+retCode));			
			}
			else
			{
				// Log success
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					"File Copy Task succeeded for feed " +dataFeed));			
			}
		}
		#endregion
		
		#region Private methods
		/// <summary>
		/// Copy/Move the incoming file into the correct directory
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private int PerformCopy(string parameters)
		{
			// Assume that everything will be successful
			int result = 0;
			
			// Extract parameters:
			// Absolute path of source location
			string sourceFilePath = GetSourceLocation();
			// Absolute path of target location
			string targetFilePath = GetTargetLocation();
			// Determine if operation is a move (copy if false)
			bool move = IsMove();

			try 
			{
                FileInfo sourceFile = new FileInfo( sourceFilePath );

                sourceFilePath = new StringBuilder( this.ProcessingDirectory.TrimEnd( '/' ).TrimEnd( '\\' ) )
                    .Append( @"\" )
                    .Append( sourceFile.Name.TrimStart( '/' ).TrimStart( '\\' ) ).ToString();

                sourceFile = new FileInfo( sourceFilePath );
                if ( TDTraceSwitch.TraceVerbose ) 
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                        new StringBuilder("File ").Append( sourceFile.FullName ).Append(" exists[").Append( sourceFile.Exists ).Append("]").ToString()));
                }

				if (!move)
				{
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                            new StringBuilder("Copying file ").Append( sourceFilePath ).Append(" to location ").Append( targetFilePath ).ToString()));
                    }

					// Copy the file into the specified directory, overwrite if necessary
					File.Copy(sourceFilePath, targetFilePath, true);
				}
				else
				{
					// Ensure that target does not already exist.
					if (File.Exists(targetFilePath))
					{
                        if ( TDTraceSwitch.TraceVerbose ) 
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                                new StringBuilder("Deleting file File ").Append( targetFilePath ).Append(" as it current exists").ToString() ));
                        }

                        File.SetAttributes( targetFilePath, FileAttributes.Normal );
						File.Delete(targetFilePath);
					}

                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                            new StringBuilder("Moving file ").Append( sourceFilePath ).Append(" to location ").Append( targetFilePath ).ToString()));
                    }
					// Move the file into the specified directory
					File.Move(sourceFilePath, targetFilePath);
				}
			} 
			// Map any exceptions onto an error code
			catch (DirectoryNotFoundException e)
			{
				result = (int)TDExceptionIdentifier.DGFileCopyDirectoryNotFound;
	               Logger.Write(new OperationalEvent(TDEventCategory.Business, 
	                   TDTraceLevel.Error, "File Copy failed: " +e.Message, e));
			}
			catch (FileNotFoundException e)
			{
				result = (int)TDExceptionIdentifier.DGFileCopyFileNotFound;
	               Logger.Write(new OperationalEvent(TDEventCategory.Business, 
	                   TDTraceLevel.Error, "File Copy failed: " + e.Message, e));
			}
			catch (IOException e)
			{
				result = (int)TDExceptionIdentifier.DGFileCopyIOException;
	               Logger.Write(new OperationalEvent(TDEventCategory.Business, 
	                   TDTraceLevel.Error, "File Copy failed: " + e.Message, e));
			}
			catch (Exception e)
			{
				result = (int)TDExceptionIdentifier.DGFileCopyUnexpectedException;
	               Logger.Write(new OperationalEvent(TDEventCategory.Business, 
	                   TDTraceLevel.Error, "File Copy failed: " + e.Message, e));
			}
			
			return result;
		}
	
		/// <summary>
		/// Confirm that the require parameters have been submitted.
		/// </summary>
		/// <returns>True, if parameters are valid</returns>
		private bool IsValidParameter()
		{
            bool result = false;

            if ( ( !string.Empty.Equals( parameters1 ) ) 
                && ( !string.Empty.Equals( parameters2 ) ))
            {
                result = true;
            }

            return result;
		}

		/// <summary>
		/// Gets the source file location string.
		/// </summary>
		private string GetSourceLocation()
		{
            return this.importFile;
		}

		/// <summary>
		/// Gets the target file location string.
		/// </summary>
		private string GetTargetLocation()
		{
            return parameters1;
		}
		
		/// <summary>
		/// Determines if the file operation is a move.
		/// </summary>
		/// <returns>True, if file should be moved not copied</returns>
		private bool IsMove()
		{
			// Return true if 'move' is specified, otherwise copy will be performed
			return ( (parameters2.ToUpper()).Equals( "MOVE" ) );
		}
		#endregion
	}
}
