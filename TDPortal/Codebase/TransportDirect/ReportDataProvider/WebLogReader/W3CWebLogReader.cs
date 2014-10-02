// ***************************************************** 
// NAME                 : W3CWebLogReader
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Reader for W3C Format Web Logs
// ***************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/W3CWebLogReader.cs-arc  $
//
//   Rev 1.3   Dec 15 2009 10:52:48   PScott
//USD 5347 - Partner allocations
//Resolution for 5347: WeblogReader  - Partner Allocation Changes
//
//   Rev 1.2   Nov 16 2009 13:47:38   scraddock
//Updated to resolve incorrect logging of .jpeg files
//
//   Rev 1.1   Apr 29 2008 16:37:54   mturner
//Fixes to allow the web log reader to process host names from IIS logs.  This is to resolve IR4904/USD2517876
//
//   Rev 1.0   Nov 08 2007 12:40:54   mturner
//Initial revision.
//
//   Rev 1.19   Jul 27 2005 13:47:08   pscott
//Format exceptions trapped within the weblog read loop are held and not issued so NaptanViwer space in pwd does not cause abort.
//
//   Rev 1.18   Jun 23 2004 10:26:56   jgeorge
//IR1043. Added support for new config parameter to specify whether log files are named using local time or GMT
//
//   Rev 1.17   Apr 19 2004 20:35:12   geaton
//IR785.
//
//   Rev 1.16   Apr 16 2004 13:38:58   geaton
//IR785 - changes to cope with hourly rotated web logs.
//
//   Rev 1.15   Mar 10 2004 11:24:18   geaton
//IR 652 and associated refactoring.
//
//   Rev 1.14   Dec 16 2003 12:31:36   geaton
//Added additional error information when reader fails to read web log entry.
//
//   Rev 1.13   Dec 15 2003 17:30:28   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.12   Nov 28 2003 10:40:18   geaton
//Return the number of workload events logged for use in informational logging.
//
//   Rev 1.11   Nov 17 2003 20:15:40   geaton
//Refactored.
//
//   Rev 1.10   Nov 14 2003 16:41:12   geaton
//Corrected use of abstract class and method.
//
//   Rev 1.9   Nov 14 2003 16:33:48   geaton
//Change in token (to undvik=1) to identify log entries to discard. Token change confirmed by PN.
//
//   Rev 1.8   Oct 09 2003 10:42:44   ALole
//Updated WebLogReaderMain not to give any textual output on completion/failure.
//Updated W3CWebLogReader to correctly handle non GMT time on local machine.
//
//   Rev 1.7   Oct 07 2003 16:05:02   ALole
//Updated  TDExceptionIdentifier references
//
//   Rev 1.6   Oct 07 2003 11:56:44   PScott
//Added enums to exceptions and adjusted date time for gmt/bst change
//
//   Rev 1.5   Oct 06 2003 16:32:40   pscott
//Adjusted to ignore url addresses where "undvik" is part of the query string. Tests and test procedure also  adjusted to check this type of url.
//
//   Rev 1.4   Oct 01 2003 09:45:04   ALole
//Updated WebLogReader to support parameterisation of supported files. Also added a check to ensure that the HTTP Status code is between 200 and 299 (i.e. successful request). Changed the min page size to 5Mb.
//
//   Rev 1.3   Sep 17 2003 09:24:40   ALole
//Added a check to W3CWebLogReader.FileTypeCheck to check that there is a file extension.
//Added Ignore attributes to the Tests in TestWebLogReaderController to avoid the Properties Service Duplication Problem.
//
//   Rev 1.2   Sep 16 2003 16:34:50   ALole
//Updated WebLogReader to use new WorkloadEvent
//
//   Rev 1.1   Sep 05 2003 12:13:48   ALole
//Changed the application name to td.weblogreader.exe
//Implemented code review changes
//Added support for not recording files under a certain size
//Only files automatically processed now are 'pages' i.e. asp, aspx, htm, html
//
//   Rev 1.0   Aug 28 2003 13:35:18   ALole
//Initial Revision

using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Partners;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// Defines the data within a W3C Web Log entry and methods to validate the
	/// data against a specification.
	/// </summary>
	public struct W3CWebLogData
	{
		private string uriStem;
		private int bytesSent;
		private int protocolStatus;
		private string clientIP;
		private string uriQuery;
		private string date;
		private string time;
        private string host;

		/// <summary>
		/// Gets the datetime that the web log data was logged.
		/// </summary>
		/// <remarks>
		/// Derivation of data into DateTime type is performed in the property
		/// getter, rather than constuctor, since this property will only
		/// be called for a subset of web log entries.
		/// The seconds component is always defaulted to zero. This is because web log
		/// data is aggregated at a resolution of 1 minute and therefore seconds data is irrelevant.
		/// </remarks>
		public DateTime DateTimeLogged
		{
			get 
			{
				// Split date and time data into integer components.
				int year = int.Parse(this.date.Substring(0, 4));
				int month = int.Parse(this.date.Substring(5, 2));
				int day = int.Parse(this.date.Substring(8, 2));
				int hour = int.Parse(this.time.Substring(0, 2));
				int minute = int.Parse(this.time.Substring(3, 2));
				int second = 0; // Default to zero - see remarks.
						
				// Create a datetime based on integer components.
				DateTime dateTimeLogged = new DateTime(year, month, day, hour, minute, second);
							
				// IIS always logs using Greenwich Meantime (GMT).
				// If the date is in DayLightSavingTime 
				// (also known as British Summer Time (BST))
				// then an hour must be added to get the 'real' local time.
				// (Since GMT is one hour behind BST.)
				if ( TimeZone.CurrentTimeZone.IsDaylightSavingTime(dateTimeLogged) )
				{
					// Web log entry made in BST so add an hour: 
					dateTimeLogged = dateTimeLogged.AddHours(1);
				}

				return dateTimeLogged;
			}
		}

		/// <summary>
		/// Constructs a W3C web log entry.
		/// </summary>
		/// <param name="uriStem">The URI stem data string.</param>
		/// <param name="bytesSent">The number of bytes sent.</param>
		/// <param name="protocolStatus">The HTTP protocol status number.</param>
		/// <param name="clientIP">The client IP address.</param>
		/// <param name="uriQuery">The URI query.</param>
		/// <param name="date">The date (as a string).</param>
		/// <param name="time">The time (as a string).</param>
        /// <param name="host">The host name (as a string).</param>
		public W3CWebLogData(string uriStem,
							 int bytesSent,
							 int protocolStatus,
							 string clientIP,
							 string uriQuery,
							 string date,
							 string time,
                             string host)
		{
			this.uriStem = uriStem;
			this.bytesSent = bytesSent;
			this.protocolStatus = protocolStatus;
			this.clientIP = clientIP;
			this.uriQuery = uriQuery;
			this.date = date;
			this.time = time;
            this.host = host;
		}

		private bool CheckProtocolStatus(int minStatus, int maxStatus)
		{
			return ((this.protocolStatus >= minStatus) && (this.protocolStatus <= maxStatus));
		}

		private bool CheckQueryString(string queryIgnoreMarker)
		{
			bool ok = true;

			if( Regex.IsMatch(this.uriQuery, queryIgnoreMarker) )
			{
				ok = false;
			}

			return ok;
		}
		
		private bool CheckBytesSent(int minSize)
		{
			return (bytesSent >= minSize);
		}

		private bool CheckClientIP(string[] clientIPExcludes)
		{
			bool ok = true;
			
			for (int i = 0; i < clientIPExcludes.Length; i++)
			{
				if ( Regex.IsMatch( this.clientIP, clientIPExcludes[i] ) )
				{
					ok = false;
					break;
				}
			}

			return ok;
		}

		private bool CheckFileType(string[] fileExtensions, string noFileExtensionMarker, bool allowNoFileExtension)
		{
			string[] uriStemSplit = this.uriStem.Split( '.' );
			bool ok = false;
					
			if (uriStemSplit.Length == 2)
			{
				// URI Stem includes a file extension.
				for ( int i = 0; i < fileExtensions.Length; i++ )
                {
                    // don't compare when value is no fileextensionmarker
                    if (fileExtensions[i] != noFileExtensionMarker)
                    {
					    if (Regex.IsMatch(uriStemSplit[1], fileExtensions[i]))
					    {
						    ok = true;
						    break;
					    }
				    }
                }
                }
			else if (uriStemSplit.Length == 1)
			{
				// URI Stem does NOT include a file extension.
				if (allowNoFileExtension)
					ok = true;
			}

			return ok;
		}

		/// <summary>
		/// Determines whether the web log entry meets the requirements defined
		/// by the specification passed.
		/// </summary>
		/// <param name="spec">Specification to test.</param>
		/// <returns>True if specification has been met, else false.</returns>
		public bool MeetsSpecification(WebLogDataSpecification specification)
		{
			
			return (CheckProtocolStatus(specification.MinProtocolStatusCode, specification.MaxProtocolStatusCode) && 
				    (CheckFileType(specification.WebPageFileExtensions, specification.NoFileExtensionMarker, specification.AllowNoFileExtension) || CheckBytesSent(specification.MinNonWebPageSize)) &&
					CheckQueryString(specification.QueryIgnoreMarker) &&
				    CheckClientIP(specification.ClientIPExcludes));
			
		}


        /// <summary>
        /// Read only property returning the partnerId from the partnercatalogue depending on the hostname.
        /// To allow Web log reader to produce single reports for sites using multiple hostnames any hostname
        /// with a known DisplayName returns the parent partner Id for that Partner.
        /// </summary>
        public int PartnerId
        {
            get
            {
                int Id = ((IPartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue]).GetPartnerIdFromHostName(host);
                
                //Retrieve DisplayName so we can see if this is a partner we produce reports for and if
                //it is return the parent Id rather than the actual Id found above.
                string DisplayName = ((IPartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue]).GetPartnerDisplayName(Id);
                switch(DisplayName)
                {
                    //Ideally this list of names and master Id's should come from a configurable source.
                    //This should be done the next time this code is changed.
                    case "TransportDirect":
                        return 0;
                    case "VisitBritain":
                        return 1;
                    case "BBC":
                        return 2;
                    case "DirectGov":
                        return 4;
                    // default - This is not a partner we report on so use the actual Id
                    default:
                        return Id;
                }
            }
        }
	}




	/// <summary>
	/// Web Log Reader Class for processing web logs that are in W3C format.
	/// </summary>
	public class W3CWebLogReader : IWebLogReader
	{
		// Define the field names to look for and the order to look for them in
		private readonly string[] mandatoryFieldNames = {"date", "time", "c-ip", "cs-uri-stem", "cs-uri-query", "sc-bytes", "sc-status", "cs-host"};
			
		// Define the field number of date in the fieldNames array
		private readonly int dateFieldPosition = 0;

		// Define the field number of time in the fieldNames array
		private readonly int timeFieldPosition = 1;
		
		// Define the field number of c-ip in the fieldNames array
		private readonly int clientIPFieldPosition = 2;

		// Define the field number of cs-uri-stem in the fieldNames array
		private readonly int uriFieldPosition = 3;

		// Define the field number of cs-uri-query in the fieldNames array
		private readonly int uriQueryFieldPosition = 4;

		// Define the field number of sc-bytes in the fieldNames array
		private readonly int bytesFieldPosition = 5;
		
		// Define the field number of sc-status in the fieldNames array
		private readonly int statusFieldPosition = 6;

        // Define the field number of sc-status in the fieldNames array
        private readonly int hostFieldPosition = 7;
		
		// Identifier that prefixes the field names definitions in the log file
		private readonly string	fieldDefinitionMarker = "#Fields: ";
		
		// Prefix of web log lines that do not include web log data.
		private readonly string	nonDataPrefix = "#";	
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		public W3CWebLogReader() : base()
		{}

		/// <summary>
		/// Determines positions of web log entry fields.
		/// This method caters for situations where order of fields 
		/// written to web logs are change within the same web log.
		/// </summary>
		/// <param name="actualFields">Array of fields to get positions for.</param>
		/// <param name="expectedFields">Array of expected fields.</param>
		/// <param name="fieldPositions">Used to return field positions.</param>
		/// <exception cref="TDException">A field position could not be allocated to one or more of the actual fields passed.</exception>
		private void GetFieldPositions(string[] actualFields, string[] expectedFields, int[] fieldPositions)
		{	

			// Set existing positions to an invalid position value.
			for (int a = 0; a < expectedFields.Length; a++)
				fieldPositions[a] = -1;

			// Assign a field position to each actual field, based on the expected fields.
			for (int i = 0; i < actualFields.Length; i++)
			{
				for (int j = 0; j < expectedFields.Length; j++)
				{
					if (String.Compare( actualFields[i], expectedFields[j] ) == 0 )
					{
						fieldPositions[j] = i;
					}
				}
			}

			// Validate that a field position was allocated to all actual fields.
			for (int k = 0; k < fieldPositions.Length; k++)
			{
				if (fieldPositions[k] == -1)
				{
					StringBuilder fieldRequirementMessage = new StringBuilder(100);

					for (int fieldNum=0; fieldNum < expectedFields.Length; fieldNum++)
						fieldRequirementMessage.Append(expectedFields[fieldNum] + ", ");

					throw new TDException(String.Format(Messages.W3CReader_MissingFields, fieldPositions[k], fieldRequirementMessage.ToString()), false, TDExceptionIdentifier.RDPWebLogReaderMissingFields);				
				}
			}
		}

		/// <summary>
		/// Splits a web log line into its component field names using given
		/// character to perform split.
		/// </summary>
		/// <param name="webLogLine">Web log line containing field names.</param>
		/// <param name="splitOn">Char to use to split web log line into field names.</param>
		/// <returns>Array of field names.</returns>
		private string[] GetFieldNames(string webLogLine, char splitOn)
		{
			string[] webLogLineSplit = webLogLine.Split(splitOn);

			// Take account of text at start of field definitions that should be ignored.
			string[] webLogLineFieldNames = new String[webLogLineSplit.Length - 1];

			for (int x = 1; x < webLogLineSplit.Length ; x++)
				webLogLineFieldNames[x-1]= webLogLineSplit[x];

			return webLogLineFieldNames;
		}


		/// <summary>
		/// Processes the workload of a web log in W3C Format.
		/// Logs WorkLoadEvent events for each entry read from web log
		/// that meets the specification passed.
		/// </summary>
		/// <param name="filePath">
		/// Full filepath to web log to process.
		/// </param>
		/// <param name="dataSpecification">
		/// Specification that must be met for entry to be given a workload event.
		/// </param>
		/// <returns>
		/// Number of workload events logged for the file processed.
		/// </returns>
		/// <exception cref="TDException">
		/// Thrown if error when processing the web log.
		/// </exception>
		public int ProcessWorkload(string filePath, WebLogDataSpecification dataSpecification)
		{
			bool fieldDefinitionsFound = false;
            StreamReader fileForHeader = null;
            StreamReader file = null;
            String webLogLine = string.Empty;
			int[] fieldPositions = new int[this.mandatoryFieldNames.Length];
			System.IFormatProvider formatProvider =
				new System.Globalization.CultureInfo("en-GB", false);

			// Create a hash table to store aggregated workload event counts for each minute.
			// Initialise hash table with maximum entries/minutes possible for configured rotation period.
			int maxWorkloadEvents;
			int errorsCaughtCount = 0;
            int maxPartnerCount = 120;
			StringBuilder errorString = new StringBuilder();

			if (this.HourlyRotation)
                maxWorkloadEvents = 60 * maxPartnerCount; // Number of minutes in an hour * partners.
			else
                maxWorkloadEvents = 1440 * maxPartnerCount; // Number of minutes in a day * partners.
			Hashtable workloadData = null;
			try
			{
				workloadData = new Hashtable(maxWorkloadEvents);
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.W3CReader_FailedAllocatingMemoryForData, exception.Message), false, TDExceptionIdentifier.RDPWebLogReaderFailedStoringWebLogData);
			}

			try
			{	
				// Open web log file for processing.
				fileForHeader = File.OpenText(filePath);
				
				// Before processing web log data entries, must find the field definitions.
				while ( ((webLogLine = fileForHeader.ReadLine()) != null) && (!fieldDefinitionsFound))
				{
					if ( Regex.IsMatch( webLogLine, fieldDefinitionMarker ) )
					{
						// Get the field names (this may be a subset of the fields required to process line)
						// Split the fields based on a space. (This assumes that fields do not include spaces.)
						string[] webLogLineFieldNames = GetFieldNames(webLogLine, ' ');

						// Get the positions of the relevant fields needed to process the line.
						GetFieldPositions(webLogLineFieldNames, this.mandatoryFieldNames, fieldPositions);

						fieldDefinitionsFound = true;						
					}
				}

				// Without field definitions it is not possible to process file!
				if (!fieldDefinitionsFound)
					throw new TDException(Messages.W3CReader_NoFieldTokens, false, TDExceptionIdentifier.RDPWebLogReaderNoFieldTokens);

                fileForHeader.Close();
              
                // Reset to top of file (in case 1st definitions are not at top)
                file = File.OpenText(filePath);
                webLogLine = file.ReadLine();
              
				// Process the web log file (allowing for field definitions to be redefined).
				do 
				{
					if (0 == String.Compare(webLogLine, 0, this.nonDataPrefix, 0, 1))
					{
						// Non-data fields found.

						if (Regex.IsMatch(webLogLine, fieldDefinitionMarker))
						{
							// Field definitions found (ie they have been redefined).
							string[] webLogLineFieldNames = GetFieldNames(webLogLine, ' ');
							GetFieldPositions(webLogLineFieldNames, this.mandatoryFieldNames, fieldPositions);
						}

					}
					else
					{			
						// Data fields found.

						string[] webLogLineDataFields = webLogLine.Split(' ');
						try
						{
							W3CWebLogData webLogData = 
							new W3CWebLogData(webLogLineDataFields[fieldPositions[uriFieldPosition]],
											  Int32.Parse(webLogLineDataFields[fieldPositions[bytesFieldPosition]]),
											  Int32.Parse(webLogLineDataFields[fieldPositions[statusFieldPosition]]),
											  webLogLineDataFields[fieldPositions[clientIPFieldPosition]],
											  webLogLineDataFields[fieldPositions[uriQueryFieldPosition]],
											  webLogLineDataFields[fieldPositions[dateFieldPosition]],
											  webLogLineDataFields[fieldPositions[timeFieldPosition]],
                                              webLogLineDataFields[fieldPositions[hostFieldPosition]]);

							// Store data if meets specification.
							if (webLogData.MeetsSpecification(dataSpecification))
							{
								try
								{
									// Key name is based on the web log data time logged.
                                    string key = webLogData.DateTimeLogged.ToString("g", formatProvider) + ";" + webLogData.PartnerId;
                                    int count;
									if (workloadData.ContainsKey(key))
									{
                                        int currentCount = Convert.ToInt32(((string)workloadData[key]).Split(';')[0], formatProvider);										
										// Increment count for this log time.
										count = currentCount + 1;
									}
									else
									{
										count = 1;
									}

                                    //add the partner id (concatenate because of hashtable)
                                    workloadData[key] = count +";" + webLogData.PartnerId;
								}
								catch (Exception exception)
								{
									throw new TDException(String.Format(Messages.W3CReader_FailureStoringData, exception.Message), false, TDExceptionIdentifier.RDPWebLogReaderFailedStoringWebLogData);
								}
							}
						}
						catch (Exception exception)
						{
							errorsCaughtCount++;
							errorString.Append(webLogLine);
	
							if (errorsCaughtCount == 10) 
							{
								throw new TDException(String.Format(Messages.W3CReader_FailureReadingWebLogFile, errorString.ToString(), exception.Message), false, TDExceptionIdentifier.RDPWebLogReaderFailedReadingWebLog);
							}
						}
						
					}

				} while ((webLogLine = file.ReadLine()) != null);
			}
			catch (TDException tdException)
			{
				throw tdException;
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.W3CReader_FailureReadingWebLogFile, webLogLine, exception.Message), false, TDExceptionIdentifier.RDPWebLogReaderFailedReadingWebLog);
			}
			finally
			{
				if (file != null)
					file.Close();
			}

			
			// Log the workload events using the TD Event logging service.
			int eventsLogged = 0;
			try
			{
				System.Collections.IDictionaryEnumerator dataEnumerator = workloadData.GetEnumerator();
				
				while (dataEnumerator.MoveNext())
				{
                    //Seperate the value and the partner id
                    string[] values = ((string)dataEnumerator.Value).Split(';');


                    string[] enumeratorKey = ((string)dataEnumerator.Key).Split(';');


                    Trace.Write(new WorkloadEvent(DateTime.ParseExact((string)enumeratorKey[0], "g", formatProvider), Convert.ToInt32(values[0]), Convert.ToInt32(values[1]))); 
                    eventsLogged++;
				}
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.W3CReader_FailureStoringData, exception.Message), false, TDExceptionIdentifier.RDPWebLogReaderFailedStoringWebLogData);
			}
			
			
			return eventsLogged;
		}

		/// <summary>
		/// Returns true if web log reader is configured for handling hourly rotation.
		/// Defaults to true if no configuration is configured or if the configuration has invalid value.
		/// </summary>
		private bool HourlyRotation
		{
			get 
			{
				string rolloverPeriod = Properties.Current[Keys.WebLogReaderRolloverPeriod];

				if (rolloverPeriod != null)
				{
					if (0 == String.Compare(LogRolloverPeriods.Daily.ToString(), rolloverPeriod))
						return false;
					else if (0 == String.Compare(LogRolloverPeriods.Hourly.ToString(), rolloverPeriod))
						return true;
					else
						return true;
				}
				else
				{
					return true;
				}
			}
		}

		/// <summary>
		/// Returns true if web log reader is configured for using local time. This setting should
		/// correspond to that set in IIS. Log files are named using the current time; if UseLocalTime
		/// is false, the current time as GMT (also known as UTC) should be used.
		/// </summary>
		private bool UseLocalTime
		{
			get
			{
				string time = Properties.Current[Keys.WebLogReaderUseLocalTime];
				bool parsedTime;

				// If null or empty string, return false (ie use GMT)
				if (time == null || time.Length == 0)
					return false;
				else
				{
					// Value should be boolean
					try
					{
						parsedTime = bool.Parse(time);
					}
					catch (FormatException e)
					{
						// The value didn't correspond to either true or false. Log a warning and
						// then return false
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.W3CReader_InvalidUseLocalTimeValue, time), e));
						return false;
					}
					return parsedTime;
				}
			}
		}

		/// <summary>
		/// Returns the filenames of web logs that should be treated
		/// as active and not be processed.
		/// File names are provided in W3C extended Log File format.
		/// </summary>
		/// <returns>Filenames of active web logs.</returns>
		public string[] GetActiveWebLogFileNames()
		{
			bool hourly = this.HourlyRotation;
			bool useLocal = this.UseLocalTime;

			if (TDTraceSwitch.TraceVerbose)
			{
				if (hourly)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.W3CReader_RolloverHourly));
				else
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.W3CReader_RolloverDaily));
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				if (useLocal)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.W3CReader_LocalTime));
				else
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.W3CReader_UtcTime));
			}

			
			TDDateTime now;
			if (useLocal)
				now = new TDDateTime(System.DateTime.Now);
			else
				now = new TDDateTime(System.DateTime.UtcNow);

			string year = now.Year.ToString().Substring(2, 2);
			string month =  now.Month.ToString();
			string day = now.Day.ToString();
			string hour = now.Hour.ToString();
			
			if (month.Length == 1)
				month = "0" + month;

			if (day.Length == 1)
				day = "0" + day;

			if (hour.Length == 1)
				hour = "0" + hour;

			string[] filenames = new string[2];

			if (!hourly)
			{
				filenames[0] = "ex" + year + month + day + ".log";
			}
			else
			{
				// If hourly then return log file for current hour. 
				//
				// Also return log file for next hour in
				// case IIS rolls to next hour before processing is started.
				//  - if situation occurs, then web log that was eliminated which was 
				// not active will be processed by web log reader on it's next run.

				TDDateTime next = now.AddMinutes(60);
				string nextYear = next.Year.ToString().Substring(2, 2);
				string nextMonth =  next.Month.ToString();
				string nextDay = next.Day.ToString();
				string nextHour = next.Hour.ToString();
			
				if (nextMonth.Length == 1)
					nextMonth = "0" + nextMonth;

				if (nextDay.Length == 1)
					nextDay = "0" + nextDay;

				if (nextHour.Length == 1)
					nextHour = "0" + nextHour;

				filenames[0] = "ex" + year + month + day + hour + ".log";
				filenames[1] = "ex" + nextYear + nextMonth + nextDay + nextHour + ".log";
			}

			return filenames;
		}

	}

}

