// *********************************************** 
// NAME                 : WebLogReaderPropertyValidator
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Validates properties read into the WebLogReaderController
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/WebLogReaderPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:58   mturner
//Initial revision.
//
//   Rev 1.9   Nov 11 2004 17:48:20   passuied
//Part of changes to enable WebLogReaders to read from multiple folders.
//
//   Rev 1.8   Dec 15 2003 17:49:18   geaton
//Updated to allow zero client ip exlcudes to be configured.
//
//   Rev 1.7   Dec 15 2003 17:30:26   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.6   Nov 18 2003 08:36:30   geaton
//Removed redundant references.
//
//   Rev 1.5   Nov 17 2003 20:15:36   geaton
//Refactored.
//
//   Rev 1.4   Oct 07 2003 11:56:42   PScott
//Added enums to exceptions and adjusted date time for gmt/bst change
//
//   Rev 1.3   Oct 01 2003 09:45:06   ALole
//Updated WebLogReader to support parameterisation of supported files. Also added a check to ensure that the HTTP Status code is between 200 and 299 (i.e. successful request). Changed the min page size to 5Mb.
//
//   Rev 1.2   Sep 09 2003 14:09:40   ALole
//Changed ValidateNonPageMinimumBytes error message to be more specific.
//
//   Rev 1.1   Sep 08 2003 11:14:38   ALole
//Added ValidateNonPageMinimumBytes method to validate the NonPageMinimumBytes property
//
//   Rev 1.0   Aug 28 2003 13:35:24   ALole
//Initial Revision

using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// Summary description for WebLogReaderPropertyValidator.
	/// </summary>
	public class WebLogReaderPropertyValidator : PropertyValidator
	{
		public WebLogReaderPropertyValidator(IPropertyProvider properties ) : base ( properties )
		{
		}

		/// <summary>
		/// Used to Validate the assocaiated Properties.
		/// Pass in the the WebLogReader.Machine key and the method will 
		/// validate all the other properties for each value in the key.
		/// </summary>
		/// <param name="key">Property key name.</param>
		/// <param name="errors">Used to return any errors found in property value of the key passed.</param>
		/// <returns>True if property is valid, else false</returns>
		/// <exception cref="TDException">
		/// If unknown property key is passed.
		/// </exception>
		public override bool ValidateProperty(string key, ArrayList errors)
		{
			if (key == Keys.WebLogReaderArchiveDirectory)
				return ValidateArchiveDirectory(key, errors);
			else if (key == Keys.WebLogReaderLogDirectory)
				return ValidateLogDirectory(key, errors);
			else if (key == Keys.WebLogReaderNonPageMinimumBytes)
				return ValidateNonPageMinimumBytes(key, errors);
			else if (key == Keys.WebLogReaderWebPageExtensions)
				return ValidateWebPageExtensions(key, errors);	
			else if (key == Keys.WebLogReaderClientIPExcludes)
				return ValidateClientIPExclusions(key, errors);
			else if (key == Keys.WebLogReaderWebLogFolders)
				return ValidateWebLogFolders(key, errors);
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPWebLogReaderUnknownPropertyKey );
			}
		}


		/// <summary>
		/// Validates the property value of the key passed
		/// </summary>
		/// <param name="key">key neame that contains the value to validate</param>
		/// <param name="errors">Used to return any errors found</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateWebLogFolders(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence (key, Optionality.Mandatory, errors))
			{
				string sFolders = properties[key];
				if (sFolders.Split(' ').Length == 0)
					errors.Add(string.Format(Messages.Validation_MissingWebLogsFolders, key));
			}

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateArchiveDirectory(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;

			
			
			// assumes that if arrived till here, WebLogFolders property is OK!
			string[] webLogFolders = Properties.Current[Keys.WebLogReaderWebLogFolders].Split(' ');

			foreach (string folder in webLogFolders)
			{
				string archiveDirectoryKey = string.Format(key, folder);
			
				if (ValidateExistence( archiveDirectoryKey, Optionality.Mandatory, errors ) )
				{
					if (!Directory.Exists(properties[archiveDirectoryKey]))
						errors.Add(String.Format(Messages.Validation_BadArchiveDir, properties[archiveDirectoryKey], archiveDirectoryKey));
				}
			}

			return (errorsBefore == errors.Count); 
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateLogDirectory( string key, ArrayList errors )
		{
			int errorsBefore = errors.Count;

			// assumes that if arrived till here, WebLogFolders property is OK!
			string[] webLogFolders = Properties.Current[Keys.WebLogReaderWebLogFolders].Split(' ');

			foreach (string folder in webLogFolders)
			{
				string logDirectoryKey = string.Format(key, folder);

				if (ValidateExistence( logDirectoryKey, Optionality.Mandatory, errors ) )
				{

					if (!Directory.Exists(properties[logDirectoryKey]))
					{
						errors.Add(String.Format(Messages.Validation_BadLogDir, properties[logDirectoryKey], logDirectoryKey));
					}
					else
					{
						// Ensure that the server on which logs reside is working to GMT.
						ValidateLocalTimeZone(errors);
					}
				}
			}

			return (errorsBefore == errors.Count); 
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateNonPageMinimumBytes(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;
			
			if (ValidateExistence( key, Optionality.Mandatory, errors ) )
			{
				if (int.Parse(properties[key]) < 0)
					errors.Add(String.Format(Messages.Validation_NonPageMinimumBytesInvalid, properties[key], key));
			}

			return (errorsBefore == errors.Count); 
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateLocalTimeZone(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (TimeZone.CurrentTimeZone.StandardName.ToString() != "GMT Standard Time")
				errors.Add(Messages.Validation_TimeZoneInvalid);

			return (errorsBefore == errors.Count); 
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateWebPageExtensions(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;
			
			ValidateExistence(key, Optionality.Mandatory, errors );

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Validates the property value of the key passed.
		/// </summary>
		/// <param name="key">Key name that contains value to validate.</param>
		/// <param name="errors">Used to return any errors found.</param>
		/// <returns>True if no errors found else false.</returns>
		private bool ValidateClientIPExclusions(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;
			
			if (ValidateExistence( key, Optionality.Mandatory, errors ) )
			{
				if (properties[Keys.WebLogReaderClientIPExcludes].Length != 0)
				{
					string[] ipList = properties[Keys.WebLogReaderClientIPExcludes].Split(' ');												

					foreach(string ip in ipList)
					{
						if( ip != " " )
						{
							if (!ValidateIPAddress(ip))
								errors.Add(String.Format(Messages.Validation_IPAddressInvalid, ip, key));
						}
						
					}
				}
			}

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Checks that IP address passed is in correct format.
		/// </summary>
		/// <param name="ipAddress">IP Address to validate</param>
		/// <returns>True if IP address is valid, otherwise false.</returns>
		/// <remarks>
		/// IP Address passed must be in IPv4 Decimal format.
		/// </remarks>
		private bool ValidateIPAddress(string ipAddress)
		{
			bool valid = true;
			ArrayList ipParts = new ArrayList(ipAddress.Split(('.')));

			valid = (ipParts.Count == 4);

			if (valid)
			{
				for(int i=0; i < ipParts.Count; i++)
				{
					string ipPart = ipParts[i].ToString().Replace(" ","");
					
					while(ipPart.Length<3)
						ipPart = "0" + ipPart;
							
					int ipPartAsInt = Convert.ToInt32(ipPart, 10);

					if(ipPartAsInt>=256)
					{
						valid = false;
						break;
					}
				}
			}

			return valid;
		}

	}
}
