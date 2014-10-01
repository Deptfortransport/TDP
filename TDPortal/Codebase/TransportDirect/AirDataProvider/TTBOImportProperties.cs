// *********************************************** 
// NAME                 : TTBOImportProperties.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 22/09/2004
// DESCRIPTION  : Static class that contains methods
// for reading properties used by the TTBO import.
// Also support class & enum for this.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/TTBOImportProperties.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:24   mturner
//Initial revision.
//
//   Rev 1.0   Sep 29 2004 12:45:52   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.AirDataProvider
{
	#region TTBOImportProperties class

	/// <summary>
	/// Contains static methods for reading properties used
	/// by the TTBO Import.
	/// </summary>
	public sealed class TTBOImportProperties
	{

		/// <summary>
		/// Private constructor to prevent class instantiation
		/// </summary>
		private TTBOImportProperties()
		{ }

		/// <summary>
		/// Reads a value from the properties service and splits it into an array of strings using 
		/// a space as the delimeter. Removes duplicates and blanks
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static string[] ReadMultipleValueProperty(IPropertyProvider properties, string propertyName)
		{
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Reading multiple value property [{0}]", propertyName) ));

			string propertyValue = properties[propertyName];

			if ( (propertyValue != null) && (propertyValue.Length != 0) )
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Property [{0}] has value [{1}]", propertyName, propertyValue) ));

				string[] resultsTemp;
				resultsTemp = propertyValue.Split(" ".ToCharArray());

				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Split into {0} elements. Processing for duplicates and blanks", resultsTemp.Length) ));

				ArrayList results = new ArrayList(resultsTemp.Length);
				string sTemp;
				foreach (string s in resultsTemp)
				{
					// Only add to the results list if not blank
					sTemp = s.Trim();
					if (sTemp.Length != 0)
					{
						// Only add to the results list if not already present - if it is there,
						// write a warning but continue
						if (results.Contains(sTemp))
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.TTBODuplicateValueInProperty, sTemp, propertyName)));
						else
							results.Add(sTemp);
					}
				}
				// Returm the results
				if (results.Count == 0)
				{
					if (TDTraceSwitch.TraceVerbose)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Processed list contains no elements"));
					return new string[0];
				}
				else
				{
					if (TDTraceSwitch.TraceVerbose)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Processed list contains {0} elements.", results.Count) ));
					return (string[])results.ToArray(typeof(string));
				}
			}
			else
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Property [{0}] is null or was not found", propertyName) ));

				return new string[0];
			}
		}

		/// <summary>
		/// Reads the details for a single server
		/// </summary>
		/// <param name="properties">A property provider</param>
		/// <param name="feedType">At time of writing, Keys.Rail or Keys.Air</param>
		/// <param name="serverName">Name of the server to read properties for</param>
		/// <param name="serverType"></param>
		/// <returns></returns>
		public static ServerDetails ReadServerDetails(IPropertyProvider properties, string feedType, string serverName, ServerType serverType)
		{
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Reading details for feed [{0}], server type [{1}], server name [{2}]", feedType, serverType.ToString(), serverName) ));

			string fileLocationKey = String.Format(Keys.ServerFileLocation, feedType, serverName);
			string remoteObjectKey = String.Format(Keys.ServerRemoteObject, feedType, serverName);

			string fileLocation = properties[fileLocationKey];
			string remoteObject = properties[remoteObjectKey];

			if (fileLocation != null)
				fileLocation = fileLocation.Trim();

			if (remoteObject != null)
				remoteObject = remoteObject.Trim();

			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("File location = [{0}], remote object = [{1}]", fileLocation, remoteObject) ));

			if ((fileLocation == null) || (fileLocation.Length == 0) || (remoteObject == null) || (remoteObject.Length == 0) )
			{
				// Invalid parameters. Raise operational event and throw error
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.TTBOImportMissingParametersForServer, fileLocationKey, fileLocation, remoteObjectKey, remoteObject));
				Trace.Write(oe);
				throw new TDException(oe.Message, true, TDExceptionIdentifier.DGInvalidConfiguration);
			}
				
			

			return new ServerDetails(serverName, serverType, fileLocation, remoteObject);
		}

		/// <summary>
		/// Reads the details for multiple servers
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="feedType"></param>
		/// <param name="serverNames"></param>
		/// <param name="serverType"></param>
		/// <returns></returns>
		public static ServerDetails[] ReadServerDetails(IPropertyProvider properties, string feedType, string[] serverNames, ServerType serverType)
		{
			ServerDetails[] results = new ServerDetails[serverNames.Length];
			for (int index = 0; index < serverNames.Length; index++)
				results[index] = ReadServerDetails(properties, feedType, serverNames[index], serverType);

			return results;
		}

		/// <summary>
		/// Reads all server details for a feed
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="feedType"></param>
		/// <returns></returns>
		public static ServerDetails[] ReadServersForFeed(IPropertyProvider properties, string feedType)
		{
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loading all server details for feed [{0}]", feedType)));

			if ( (feedType == null) || (feedType.Length == 0) )
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.TTBOImportUnexpectedFeedName);
				Trace.Write(oe);
				throw new TDException(oe.Message, true, TDExceptionIdentifier.DGUnexpectedFeedName);
			}

			string[] cjpServers = ReadMultipleValueProperty(properties, String.Format(Keys.CJPServers, feedType));
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded names for {0} CJP servers", cjpServers.Length)));
			
			string[] ttboServers = ReadMultipleValueProperty(properties, String.Format(Keys.TTBOServers, feedType));
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded names for {0} TTBO servers", ttboServers.Length)));

			ServerDetails[] cjpServerDetails = ReadServerDetails(properties, feedType, cjpServers, ServerType.CJP);
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded details for {0} CJP servers", cjpServerDetails.Length)));

			ServerDetails[] ttboServerDetails = ReadServerDetails(properties, feedType, ttboServers, ServerType.TTBO);
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded details for {0} TTBO servers", ttboServerDetails.Length)));

			if ( (cjpServerDetails.Length == 0) && (ttboServerDetails.Length == 0) )
			{
				// No results. Raise an error
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.TTBOImportNoServerDetails, feedType));
				Trace.Write(oe);
				throw new TDException(oe.Message, true, TDExceptionIdentifier.DGInvalidConfiguration);
			}
			else if (cjpServerDetails.Length == 0)
			{
				// Only TTBO servers
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "No CJP server details were found. Returning TTBO details only."));

				return ttboServerDetails;
			}
			else if (ttboServerDetails.Length == 0)
			{
				// Only CJP servers
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "No TTBO server details were found. Returning CJP details only."));

				return cjpServerDetails;
			}
			else
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Combining CJP and TTBO server details."));

				// Both types
				ServerDetails[] results = new ServerDetails[cjpServerDetails.Length + ttboServerDetails.Length];
				cjpServerDetails.CopyTo(results, 0);
				ttboServerDetails.CopyTo(results, cjpServerDetails.Length);

				if (TDTraceSwitch.TraceVerbose)
					Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Returning combined array of {0} servers.", results.Length)));
				
				return results;
			}

		}

	}

	#endregion

	#region ServerType enumeration

	/// <summary>
	/// Possible server types. When updating a CJP server, the update is achieved by calling a
	/// command on the CJP, this in turn updates the TTBO on that server. When updating a TTBO
	/// server, the update method is called directly.
	/// </summary>
	public enum ServerType
	{
		Unknown,
		CJP,
		TTBO
	}

	#endregion

	#region ServerDetails class

	/// <summary>
	/// Holds file location and remote object info for a single server
	/// </summary>
	public class ServerDetails
	{
		private readonly string id = string.Empty;
		private readonly ServerType type = ServerType.Unknown;
		private readonly string fileLocation = string.Empty;
		private readonly string remoteObject = string.Empty;

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="type"></param>
		/// <param name="fileLocation"></param>
		/// <param name="remoteObject"></param>
		public ServerDetails(string id, ServerType type, string fileLocation, string remoteObject)
		{
			this.id = id;
			this.type = type;
			this.fileLocation = fileLocation;
			this.remoteObject = remoteObject;
		}

		public string Id
		{
			get { return id; }
		}

		public ServerType Type
		{
			get { return this.type; }
		}

		public string FileLocation
		{
			get { return fileLocation; }
		}

		public string RemoteObject 
		{
			get { return remoteObject; }
		}
	}

	#endregion

}
