// ***********************************************
// NAME 		: CommandLineImporterr.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 12/09/2003
// DESCRIPTION 	: An importer class that makes use of a command line interface
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/CommandLineImporter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:08   mturner
//Initial revision.
//
//   Rev 1.5   May 13 2004 18:41:24   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.4   Nov 21 2003 20:04:20   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.3   Nov 18 2003 19:17:20   JMorrissey
//Fixed clean up issues with data gateway, fixed travel news to use properties for xsd, fixed command line importer, and updated resources for data gateway.
//
//   Rev 1.2   Nov 17 2003 15:47:38   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.1   Sep 18 2003 16:02:00   MTurner
//First version
//
//   Rev 1.0   Sep 17 2003 15:38:32   MTurner
//Initial Revision

using System;
using System.Text;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// CommandLineImporter is a subclass of the ImportTask class and is used to process 
	/// any files that are processed by command line utilities.
	/// </summary>
	public class CommandLineImporter : ImportTask
	{

		private string dataFeedString;
		private string parameters1String;
		private string parameters2String;
		private string importUtilityString;
		private string processingDirectoryString;

		public CommandLineImporter(string feed, string params1, string params2, string utility,
			string directory) : base(feed, params1, params2, utility, directory)
		{
			this.dataFeed = feed;
			this.parameters1 = params1;
			this.parameters2 = params2;
			this.importUtility = utility;
			this.processingDirectory = directory;
		}

		public int Import(string fileName)
		{
			int statusCode = 0;

			try
			{
				Process p = new Process();
				StringBuilder tempString = new StringBuilder();

				if(parameters1 != null)
					tempString.Append(parameters1.Trim() + " ");
				tempString.Append(fileName.Trim());
				if(parameters2 != null)
					tempString.Append(" " + parameters2.Trim());

				// Set up process attributes.
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.CreateNoWindow = false; 
				p.StartInfo.FileName = importUtility;
				p.StartInfo.WorkingDirectory = processingDirectory;
				p.StartInfo.Arguments = tempString.ToString();

				p.Start();
				p.WaitForExit();
				statusCode = p.ExitCode;
			}
			catch(Exception e)
			{
				statusCode = (int)TDExceptionIdentifier.DGCommandLineImportError;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Error, statusCode.ToString()+ ":" +e.Message+ " has occurred for " +dataFeed+ " " +fileName);
				Logger.Write(oe);
			}
			return statusCode;
		}

		#region Properties
		/// <summary>
		/// The name of the data feed being imported
		/// </summary>
		new private string dataFeed
		{
			get
			{
				return dataFeedString; 
			}
			set
			{
				dataFeedString = value;
			}
		}

		/// <summary>
		/// Parameters to pass to the underlying import utility before the file name
		/// </summary>
		new private string parameters1
		{
			get
			{
				return parameters1String; 
			}
			set
			{
				parameters1String = value;
			}
		}

		/// <summary>
		/// Parameters to pass to the underlying import utility after the file name
		/// </summary>
		new private string parameters2
		{
			get
			{
				return parameters2String; 
			}
			set
			{
				parameters2String = value;
			}
		}

		/// <summary>
		/// The name of the import utility to be called
		/// </summary>
		new private string importUtility
		{
			get
			{
				return importUtilityString; 
			}
			set
			{
				importUtilityString = value;
			}
		}

		/// <summary>
		/// The path of the directory from which to call the Import Utility
		/// </summary>
		private string processingDirectory
		{
			get
			{
				return processingDirectoryString; 
			}
			set
			{
				processingDirectoryString = value;
			}
		}
		#endregion
	}
}
