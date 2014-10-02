// ********************************************************************** 
// NAME                 : RBOImportProperties.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2006-02-10
// DESCRIPTION  		: Static class that contains methods for reading
// 							reading properties used by RBO import task.
// 							Also support class & enum for this.
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RBOImportProperties.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:46:14   mturner
//Initial revision.
//
//   Rev 1.0   Feb 10 2006 18:36:28   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

	/// <summary>
	/// Contains static methods for reading 
	/// properties used by RBO Import task.
	/// </summary>
	public sealed class RBOImportProperties
	{

		/// <summary>
		/// Private constructor to prevent class instantiation
		/// </summary>
		private RBOImportProperties()
		{ }

		/// <summary>
		/// Reads a value from the properties service and splits it into an array of strings using 
		/// a space as the delimiter. Removes duplicates and blanks
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
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Property [{0}] has value [{1}]", propertyName, propertyValue) ));
				}

				string[] resultsTemp;
				resultsTemp = propertyValue.Split(" ".ToCharArray());

				if (TDTraceSwitch.TraceVerbose)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Split into {0} elements. Processing for duplicates and blanks", resultsTemp.Length) ));
				}

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
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.RBOImport_DuplicateEntryFound, sTemp, propertyName)));
						}
						else
						{
							results.Add(sTemp);
						}
					}
				}
				// Returm the results
				if (results.Count == 0)
				{
					if (TDTraceSwitch.TraceVerbose)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Processed list contains no elements"));
					}
					return new string[0];
				}
				else
				{
					if (TDTraceSwitch.TraceVerbose)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Processed list contains {0} elements.", results.Count) ));
					}
					return (string[])results.ToArray(typeof(string));
				}
			}
			else
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Property [{0}] is null or was not found", propertyName) ));
				}
				return new string[0];
			}
		}

		/// <summary>
		/// Reads all server details for a feed
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="feedType"></param>
		/// <returns></returns>
		public static ServerDetails[] ReadAllServers(IPropertyProvider properties)
		{
			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Loading all RBO server details"));
			}

			string[] rboServers = ReadMultipleValueProperty(properties, Keys.RBOServers);

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded names for {0} RBO servers", rboServers.Length)));
			}

			ServerDetails[] rboServerDetails = ReadAllServerDetails(properties, rboServers);

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Loaded server details for {0} RBO servers", rboServerDetails.Length)));
			}
			
			return rboServerDetails;
		}

		/// <summary>
		/// Reads the details for a single server
		/// </summary>
		/// <param name="properties">A property provider</param>
		/// <param name="serverName">Name of the server to read properties for</param>
		/// <param name="serverType"></param>
		/// <returns></returns>
		public static ServerDetails ReadServerDetails(IPropertyProvider properties, string serverName)
		{
			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("Reading details for server name [{0}]", serverName) ));
			}

			string fileLocationKey = String.Format(Keys.ServerFileLocation, serverName);
			string remoteObjectKey = String.Format(Keys.ServerRemoteObject, serverName);

			string fileLocation = properties[fileLocationKey];
			string remoteObject = properties[remoteObjectKey];

			if (fileLocation != null)
			{
				fileLocation = fileLocation.Trim();
			}

			if (remoteObject != null)
			{
				remoteObject = remoteObject.Trim();
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format("File location = [{0}], remote object = [{1}]", fileLocation, remoteObject) ));
			}

			if ((fileLocation == null) || (fileLocation.Length == 0) || (remoteObject == null) || (remoteObject.Length == 0) )
			{
				// Invalid parameters. Raise operational event and throw error
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.RBOImport_MissingParametersForServer, fileLocationKey, fileLocation, remoteObjectKey, remoteObject));
				Trace.Write(oe);
				throw new TDException(oe.Message, true, TDExceptionIdentifier.DGInvalidConfiguration);
			}

			return new ServerDetails(serverName, fileLocation, remoteObject);
		}


		/// <summary>
		/// Reads the details for multiple servers
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="feedType"></param>
		/// <param name="serverNames"></param>
		/// <param name="serverType"></param>
		/// <returns></returns>
		public static ServerDetails[] ReadAllServerDetails(IPropertyProvider properties, string[] serverNames)
		{
			ServerDetails[] results = new ServerDetails[serverNames.Length];

			for (int index = 0; index < serverNames.Length; index++)
			{
				results[index] = ReadServerDetails(properties, serverNames[index]);
			}
			return results;
		}
	}


	/// <summary>
	/// Holds file location and remote object info for a single server
	/// </summary>
	public class ServerDetails
	{
		private readonly string id = string.Empty;
		private readonly string fileLocation = string.Empty;
		private readonly string remoteObject = string.Empty;

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="fileLocation"></param>
		/// <param name="remoteObject"></param>
		public ServerDetails(string id, string fileLocation, string remoteObject)
		{
			this.id = id;
			this.fileLocation = fileLocation;
			this.remoteObject = remoteObject;
		}

		public string Id
		{
			get { return id; }
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
}
