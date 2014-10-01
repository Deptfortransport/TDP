// *********************************************** 
// NAME                 : Cjp.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 02/07/2003
// DESCRIPTION  : This is a test stub for the
// Coordinate Journey Planner (CJP).
// DESIGN DOC   : IF050 Web Layer - CJP 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TestStub/TestCjp.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:24:24   mturner
//Initial revision.
//
//   Rev 1.11   Mar 14 2006 08:41:38   build
//Automatically merged from branch for stream3353
//
//   Rev 1.10.1.0   Feb 24 2006 10:22:56   pcross
//Correction to test stub
//
//   Rev 1.10   Nov 09 2005 12:31:30   build
//Automatically merged from branch for stream2818
//
//   Rev 1.9.1.0   Oct 27 2005 14:18:52   RWilby
//Updated LoadProperties method to allow relative file paths as well as absolute 
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.9   Apr 08 2005 08:44:24   jgeorge
//Added code to return correct "No journeys found" error when no matching journeys are present.
//
//   Rev 1.8   Apr 06 2005 15:29:54   jgeorge
//Updated to provide a mode where results are matched with requests
//
//   Rev 1.7   Oct 15 2004 12:51:32   jgeorge
//Modified so that the delay is treated as milliseconds rather than seconds. Changed to assist in testing for this IR. 
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.6   Jul 28 2004 10:54:06   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.5   Apr 19 2004 10:56:46   COwczarek
//Add dummy implementation for new method on ICJP interface
//Resolution for 697: Bus replacement change
//
//   Rev 1.4   Dec 16 2003 10:16:08   RPhilpott
//Add dummy implementations for new methods on ICJP interface.
//
//   Rev 1.3   Sep 26 2003 14:04:38   RPhilpott
//Tidy up NUnit testing
//
//   Rev 1.2   Sep 24 2003 17:36:36   RPhilpott
//Make test stub more flexible

// Version      Ref     Author  Date            Description 
// V1.0					kcheung 02/07/2003      Initial Version 

using System;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Text;
using System.Collections;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// CJP Test Stub.  The stub deserialises all the Xml files provided to recreate
	/// the JourneyResult objects.  These objects are then returned in sequence at
	/// each call of the JourneyPlanner method.
	/// </summary>
	public class CjpStub : MarshalByRefObject, ICJP
	{
		private JourneyResult[] journeyResults;
		private string[] xmlFilenames;
		private int currentNumber = 0;
		private int minDelay = 0;
		private int maxDelay = 0;
		private int numberOfJourneyResults = 0;
		private bool matchResultsToRequests;
		private Hashtable journeyTable;


		/// <summary>
		/// This constructor builds the JourneyResults array by deserialising all
		/// the Xml files provided, as specified in the passed in Properties file.
		/// Each request then returns the next file in the list
		/// </summary>
		public CjpStub(string filename) : this(filename, false)
		{ }

		/// <summary>
		/// This constructor builds the JourneyResults array by deserialising all
		/// the Xml files provided, as specified in the passed in Properties file.
		/// The requests are then matched against the results based on the
		/// origin and destination locations, the modes and the date.
		/// </summary>
		public CjpStub(string filename, bool matchResultsToRequests)
		{
			this.matchResultsToRequests = matchResultsToRequests;

			// load the properties
			LoadProperties(filename);

			// load the test data 
			if (matchResultsToRequests)
				ReadAndIndexFiles();
			else
				ReadFiles();

		}

		/// <summary>
		/// This constructor builds the JourneyResults array by deserialising 
		/// a single results file specified as a parameter ...
		/// </summary>
		public CjpStub(string fileName, int maxDelay)
		{
			this.maxDelay = maxDelay;
			numberOfJourneyResults = 1;
			xmlFilenames = new String[] { fileName };

			// load the test data 
			ReadFiles();
		}

		#region Methods for reading files and indexing the journeys

		private void ReadAndIndexFiles()
		{
			journeyTable = new Hashtable();

			foreach (string currentFile in xmlFilenames)
			{
				XmlSerializer xs = new XmlSerializer(typeof(JourneyResult));
				
				FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				
				// Recreate a JourneyResult object by deserialising the file.
				JourneyResult result = (JourneyResult)xs.Deserialize(fileStream);

				// Extract each PublicJourney node and store it
				foreach (JourneyPlanning.CJPInterface.PublicJourney journey in result.publicJourneys )
				{
					JourneyResultKey key = GetKeyForJourney(journey);
					if (key != null)
						GetListFromHashtable(journeyTable, key).Add( journey );
				}
			}
		}

		private static ArrayList GetListFromHashtable(Hashtable table, JourneyResultKey key)
		{
			if ( !table.ContainsKey( key ) )
				table.Add( key, new ArrayList() );

			return (ArrayList)table[key];
		}

		private static JourneyResultKey GetKeyForJourney(JourneyPlanning.CJPInterface.PublicJourney journey)
		{
			// Find the mode
			ArrayList allowedModes = new ArrayList( new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air, ModeType.Car } );
			
			// Find the mode
			ModeType mode = ModeType.Walk;
			foreach (Leg leg in journey.legs)
			{
				if ( allowedModes.Contains(leg.mode) )
				{
					mode = leg.mode;
					break;
				}
			}
			if (mode == ModeType.Walk)
				return null;

			string originNaptan = journey.GetDepartNaPTANID();
			string destinationNaptan = journey.GetArriveNaPTANID();
			DateTime dateTime = journey.GetDepartTime();

			return new JourneyResultKey( mode, originNaptan, destinationNaptan, dateTime );
		}

		private static JourneyResultKey GetKeyForJourneyNode(XmlNode journeyNode)
		{
			string mode = journeyNode.SelectSingleNode( "//Leg/mode" ).InnerXml;
			string originNaptan = journeyNode.SelectSingleNode( "//Leg/board/stop/NaPTANID" ).InnerXml;
			string destinationNaptan = journeyNode.SelectSingleNode( "//Leg[last()]/alight/stop/NaPTANID" ).InnerXml;
			string dateTime = journeyNode.SelectSingleNode( "//Leg/board/departTime" ).InnerXml;

			ModeType convertedMode = (ModeType)Enum.Parse( typeof(ModeType), mode, true );
			DateTime convertedDateTime = Convert.ToDateTime( dateTime, CultureInfo.InvariantCulture );

			return new JourneyResultKey( convertedMode, originNaptan, destinationNaptan, convertedDateTime );
		}

		#endregion

		#region Method for reading files and storing in array

		private void ReadFiles()
		{
			string currentFile = String.Empty;

			try
			{		
				// check that the number of filenames match the number of cjp results
				// check that the files exist before performing deserialisation.

				journeyResults = new JourneyResult[numberOfJourneyResults];

				// Deserialise each file and add to the CjpResults array.

				for(int i=0; i < numberOfJourneyResults; i++)
				{
					// Instantiate a new instance of the XmlSerializer/
					XmlSerializer xs = new XmlSerializer(typeof(JourneyResult));
				
					currentFile = xmlFilenames[i];
					// Open a filestream pointing to the next filename as specified
					// in the filenames array.
					FileStream fileStream =
						new FileStream(currentFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				
					// Recreate a JourneyResult object by deserialising the file.
					JourneyResult result = (JourneyResult)xs.Deserialize(fileStream);

					// Add the result to the JourneyResults array
					journeyResults[i] = result;

					fileStream.Close();
				}
			}
				// Catch some of the Xml deserialisation exceptions as well.
			catch(Exception e)
			{
				Console.WriteLine("An exception occured while attempting to " +
					"initialise the CJP test stub.  The file that MAY have cause " +
					"the error is: " + currentFile);
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				throw e;
			}
		}

		#endregion

		/// <summary>
		/// Helper function to get the current execution path
		/// </summary>
		/// <returns>execution location path</returns>
		private string GetCurrentFolderPath()
		{   			
			string replaceVal = @"file:\";
			string folderPath = string.Empty;			
			folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, string.Empty);			
			return folderPath;
			
		}

		/// <summary>
		/// Loads the properties from the Xml file.  The properties file will
		/// contain the following information.
		/// 1. Min and Max values of the network delay to simulate.
		/// 2. The number of JourneyResults to load.
		/// 3. The path of all the Xml files used to load the JourneyResults.
		/// </summary>
		private void LoadProperties(string propertiesFilename)
		{
			try
			{
				// Get the properties file to get the min, max delays, the expected
				// number of JourneyResult files, and the filenames.
						
				// Read the min and max delay from the properties file
				XmlDataDocument properties = new XmlDataDocument();
				properties.Load(propertiesFilename);
			
				XmlNode minNode = properties.DocumentElement.SelectSingleNode("//MinDelay");
			
				XmlNode maxNode = properties.DocumentElement.SelectSingleNode("//MaxDelay");
			
				minDelay = Convert.ToInt32( minNode.InnerXml.ToString(CultureInfo.InvariantCulture) );
				maxDelay = Convert.ToInt32( maxNode.InnerXml.ToString(CultureInfo.InvariantCulture) );

				// Find out if a results folder element is present
				XmlNode resultsFolder = properties.SelectSingleNode( "//ResultsFolder" );

				if (resultsFolder == null)
				{
					// Read the number of JourneyResults expected.
					XmlNode numberOfJourneyResultsNode = properties.DocumentElement.SelectSingleNode("//NumberOfJourneyResults");
			
					numberOfJourneyResults = Convert.ToInt32(numberOfJourneyResultsNode.InnerXml.ToString(CultureInfo.InvariantCulture));

					// Read the filenames from properties and populate the filenames array.
					xmlFilenames = new String[numberOfJourneyResults];
					XmlNodeList filenameNodes = properties.DocumentElement.SelectNodes("//JourneyResultPath");

					for (int i=0; i < filenameNodes.Count; i++)
					{
						xmlFilenames[i] =
							filenameNodes[i].InnerXml.ToString(CultureInfo.InvariantCulture);
					}
				}
				else
				{
					// Get the names of all the files in the folder
					DirectoryInfo dir = new DirectoryInfo( resultsFolder.InnerXml );

					if(!dir.Exists)
					{
						//Try relative path
						dir = new DirectoryInfo(GetCurrentFolderPath() + resultsFolder.InnerXml);
					}

					ArrayList fileNames = new ArrayList();
					foreach (FileInfo currentFile in dir.GetFiles() )
					{
						if (currentFile.Extension.ToLower() == ".xml")
							fileNames.Add( currentFile.FullName );
					}
					xmlFilenames = (string[])fileNames.ToArray(typeof(string));
					numberOfJourneyResults = xmlFilenames.Length;
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("An exception occurred when attempting to load " +
					"the " + propertiesFilename + " file.");
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				throw e;
			}
		}

		#region JourneyPlan method

		/// <summary>
		/// Returns a JourneyResult - this is the next result in
		/// sequence deserialized from the Xml files.
		/// </summary>
		/// <param name="request">This parameter is not used.</param>
		/// <returns>The next journey result in the sequence.</returns>
		public CJPResult JourneyPlan(CJPRequest request)
		{	
			JourneyRequest journeyRequest = request as JourneyRequest;
			if (journeyRequest == null)
				return null;
			else if (matchResultsToRequests)
				return JourneyPlanFromRequest(journeyRequest);
			else
				return JourneyPlanFromList(journeyRequest);

		}

		public CJPResult JourneyPlanFromRequest(JourneyRequest request)
		{
			ArrayList keys = new ArrayList();
			DateTime requestDate = request.origin.stops[0].timeDate;

			foreach (JourneyPlanning.CJPInterface.Mode currentMode in request.modeFilter.modes)
			{
				foreach (RequestStop currentOrigin in request.origin.stops)
				{
					foreach (RequestStop currentDestination in request.destination.stops)
					{
						keys.Add( new JourneyResultKey( currentMode.mode, currentOrigin.NaPTANID, currentDestination.NaPTANID, requestDate ) );
					}
				}
			}

			// Now for each key, get the journey results
			ArrayList results = new ArrayList();

			foreach (JourneyResultKey key in keys)
			{
				if (journeyTable.ContainsKey( key ))
					results.AddRange( (ArrayList)journeyTable[key] );
			}

			JourneyResult result = new JourneyResult();

			if ( results.Count == 0 )
			{
				JourneyWebMessage errorMessage1 = new JourneyWebMessage();
				errorMessage1.code = 0;
				errorMessage1.description = "No results found";
				errorMessage1.subClass = 1;
				Message errorMessage2 = new Message();
				errorMessage2.code = 18;
				errorMessage2.description = "Insufficient publicJourneys were returned from the previous requests, see the previous error messages for details";
				result.messages = new Message[] { errorMessage1, errorMessage2 };
			}
			else
				result.publicJourneys = (JourneyPlanning.CJPInterface.PublicJourney[])results.ToArray(typeof(JourneyPlanning.CJPInterface.PublicJourney));
			return result;
		}

		private XmlDocument CreateXmlResultFromList( ArrayList journeys )
		{
			XmlDocument results = new XmlDocument();
			results.LoadXml( "<JourneyResult xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" />" );
			try
			{
			
				foreach ( XmlNode currentNode in journeys )
				{
					XmlNode copiedNode = results.ImportNode( currentNode, true );
					results.DocumentElement.AppendChild( copiedNode );
				}

			}
			catch (Exception e)
			{
				Console.WriteLine( e.Message );
			}
			return results;
		}

		public CJPResult JourneyPlanFromList(JourneyRequest request)
		{
			try
			{
				JourneyResult result = null;

					// simulate the network delay
					Thread.Sleep(maxDelay);
					result = journeyResults[currentNumber];

					// Increment the currentNumber only if there are more
					// JourneyResults available otherwise loop back to the start.
					if(currentNumber != journeyResults.Length - 1)
					{
						// more journey results available
						currentNumber++;
					}
					else
					{
						// no more journey results available - loop back to start
						currentNumber = 0;
					}

				return result;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				throw e;
			}
		}



		#endregion
	
		#region Dummies for methods required by ICJP interface

		public Message UpdateRailData()
		{
			return null;
		}

		public Message UpdateRoadNetwork()
		{
			return null;
		}

		public Message UpdateRoadCongestionData()
		{
			return null;
		}

        public string GetVersonInfo() 
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetVersionInfo() 
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Message UpdateAirData() 
        {
            return null;
        }

		#endregion

	}

	internal class JourneyResultKey
	{
		private readonly ModeType mode;
		private readonly string originNaptan;
		private readonly string destinationNaptan;
		private readonly DateTime date;

		public JourneyResultKey(ModeType mode, string originNaptan, string destinationNaptan, DateTime date)
		{
			this.mode = mode;
			this.originNaptan = originNaptan;
			this.destinationNaptan = destinationNaptan;
			this.date = new DateTime( date.Year, date.Month, date.Day ) ;
		} 

		public ModeType Mode
		{
			get { return mode; }
		}

		public string OriginNaptan
		{
			get { return originNaptan; }
		}

		public string DestinationNaptan
		{
			get { return destinationNaptan; }
		}

		public DateTime Date
		{
			get { return date; }
		}

		public override bool Equals(object obj)
		{
			if (obj is JourneyResultKey)
			{
				JourneyResultKey key = obj as JourneyResultKey;
				return ( (this.Date == key.Date) 
					&& (this.OriginNaptan == key.OriginNaptan) 
					&& (this.DestinationNaptan == key.DestinationNaptan) 
					&& (this.Mode == key.Mode) );
			}
			else
				return false;
		}

		public override int GetHashCode()
		{
			return date.GetHashCode() + originNaptan.GetHashCode() + destinationNaptan.GetHashCode() + mode.GetHashCode();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append( mode.ToString() );
			sb.Append( originNaptan );
			sb.Append( destinationNaptan );
			sb.Append( date.ToString( CultureInfo.InvariantCulture ) );
			return sb.ToString();
		}


	}
}
