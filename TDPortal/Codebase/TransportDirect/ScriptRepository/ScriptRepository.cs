// ***********************************************
// NAME 		: ScriptRepository.cs
// AUTHOR 		: James Broome
// DATE CREATED : 16-Apr-2004
// DESCRIPTION 	: Script Repository.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScriptRepository/ScriptRepository.cs-arc  $
//
//   Rev 1.4   Jul 12 2010 15:27:24   apatel
//Updated to expose temp script folder path with property
//Resolution for 5572: DropDownGaz - Deletion of old data file versions
//
//   Rev 1.3   Jun 14 2010 14:25:50   apatel
//Updated for DropDownGaz
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 07 2010 12:08:44   apatel
//Added logic to log the size of the temp file generated in AddTempScript method
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.1   Oct 02 2008 11:50:30   mturner
//Updated for XHTML compliance
//
//   Rev 1.0.1.0   Sep 15 2008 10:42:54   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:47:56   mturner
//Initial revision.
//
//   Rev 1.6   Nov 03 2005 17:08:02   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.5.1.0   Oct 19 2005 13:53:46   rhopkins
//Standardise text case used for default DOM ID to be uppercase
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.5   Sep 26 2005 17:24:40   rhopkins
//Merge stream 2596 back into trunk
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.4   Aug 18 2005 11:30:18   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.3.1.0   Aug 03 2005 14:20:48   jgeorge
//Updated detect script code to include return statement.
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.3   May 19 2004 12:29:28   jbroome
//Corrected error in AddTempScript
//
//   Rev 1.2   May 19 2004 12:12:30   jbroome
//Added AddTempScript method
//
//   Rev 1.1   May 12 2004 16:24:12   jbroome
//Removed use of Server.MapPath so can use NUnit.
//Added TDException handling
//
//   Rev 1.0   Apr 30 2004 13:53:10   jbroome
//Initial revision.

using System;
using System.Xml;
using System.Text;
using System.Collections;
using System.Web;
using System.IO;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.ScriptRepository
{
	/// <summary>
	/// A simple repository to allow JavaScript files to be selected based on the script name and the 
	/// DOM support of the browser.
	/// </summary>
	public class ScriptRepository
	{
	
		/// <summary>
		/// Maintains a flag indicating of cache has been loaded.
		/// </summary>
		static private volatile bool CacheIsLoaded = false;

		// Const variables for detection file properties
		static string DETECTION_SCRIPT_ACTION = "ObjectDetection();";
		static string DETECTION_SCRIPT_NAME ="JavaScriptDetection";

		// The Dom support to use if no dom specific script is specified
		static string DEFAULT_DOM = "W3C_STYLE";
	
		// The Hashtable of all the scripts and their associated files
		static Hashtable scripts = null;

		static string rootFilePath = string.Empty;	
		static string configFilePath = string.Empty;	

		#region Constructor
		/// <summary>
		/// Load up the script details from the configuration file and store them in the scripts Hashtable
		/// </summary>
		public ScriptRepository(string rootFilePath, string configFilePath)
		{
			if(CacheIsLoaded == false)
			{
				ScriptRepository.rootFilePath = rootFilePath;
				ScriptRepository.configFilePath = configFilePath;

				lock(typeof(ScriptRepository))  // Lock ScriptRepository type at this stage.
				{
					// Test again in case it got loaded when lock was acquired.
					if(CacheIsLoaded == false)
					{
						scripts = new Hashtable();
						CacheIsLoaded = true;
						LoadScriptCache();
					}
				} // lock
			} // If cache is not loaded
		}
		#endregion

		#region Load Script Cache
		/// <summary>
		/// Loads the script cache from XML configuration file.
		/// </summary>
		private void LoadScriptCache()
		{
			// Load the script configuration file
			XmlDocument scriptDocument = new XmlDocument();
			
			scriptDocument.Load(configFilePath);			

			XmlNodeList scriptNodes = scriptDocument.DocumentElement.GetElementsByTagName("script");

			string scriptName = null;
			Hashtable scriptFiles = null;
			foreach(XmlNode node in scriptNodes)
			{
				scriptName = node.Attributes["name"].Value;
				scriptFiles = new Hashtable();
				scripts.Add(scriptName, scriptFiles);
				foreach(XmlNode script in node.ChildNodes)
				{
					scriptFiles.Add(script.Attributes["domsupport"].Value, script.Attributes["filepath"].Value);
				}
			}		
		}
		#endregion

		#region Public Interface
		/// <summary>
		/// Retrieve the script to determine the browser support for JavaScript
		/// </summary>
		public string DetectionScript
		{
			get {return GetScript(DETECTION_SCRIPT_NAME,DEFAULT_DOM);}
		}
	
		/// <summary>
		/// Retrieve the name of the detection script for use as a key
		/// </summary>
		public string DetectionScriptName
		{
			get {return DETECTION_SCRIPT_NAME;}
		}

		/// <summary>
		/// Retrieve the statement used to call the detection script
		/// </summary>
		public string DetectionScriptAction
		{
			get { return DETECTION_SCRIPT_ACTION;}
		}

        /// <summary>
        /// Returns full path for the TempScript folder
        /// </summary>
        public string TempScriptFolderPath
        {
            get
            {
                // Create TempScript directory in case it does not exist
                Directory.CreateDirectory(Directory.GetParent(configFilePath).FullName + @"\tempscripts");
                DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts");
                DirectoryInfo tempDir = asubDir[0];

                return tempDir.FullName;
            }
        }

		/// <summary>
		/// Look up the appropriate script in the Hashtable of script for the given scriptName
		/// If there is a browser specific script we use that, otherwise we use the default script.
		/// A piece of JavaScript referencing the file is then returned for embedding in a page.
		/// </summary>
		/// <param name="scriptName"></param>
		/// <param name="browserSupport"></param>
		/// <returns></returns>
		public string GetScript(string scriptName, string browserSupport)
		{
			Hashtable scriptFiles = (Hashtable)scripts[scriptName];
			try 
			{
				if (scriptFiles.ContainsKey(browserSupport)) 
				{
					return GenerateJavaScript((string)scriptFiles[browserSupport]);
				} 
				else 
				{
					return GenerateJavaScript((string)scriptFiles[DEFAULT_DOM]);
				}
			}
			catch (NullReferenceException)
			{
				// Log the exception
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"ScriptName \""+ scriptName +"\" does not exist in the script repository.");

				Logger.Write(oe);

				throw new TDException("ScriptName \""+ scriptName +"\" does not exist in the script repository.", true, TDExceptionIdentifier.WEBScriptRepositoryInvalidScriptName);
			}
		}

		/// <summary>
		/// Add a dynamically generated script to the repository at run-time and
		/// return JavaScript reference to it.
		/// </summary>
		/// <param name="scriptName"></param>
		/// <param name="browserSupport"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		public string AddTempScript(string scriptName, string browserSupport, StringBuilder body)
		{
			// Create TempScript directory in case it does not exist
			Directory.CreateDirectory(Directory.GetParent(configFilePath).FullName + @"\tempscripts");
			DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts"); 
			DirectoryInfo tempDir = asubDir[0];
			
			string strTempFolder = tempDir.FullName;
			string strTempFileName =  scriptName + "_" + browserSupport + ".js";
			string strTempFileRef = Directory.GetParent(configFilePath).Name + "/"
										+ tempDir.Name + "/" 
											+ strTempFileName;
			
			// Creates new file or overwrites existing file.
			StreamWriter sw = File.CreateText(strTempFolder + @"\" + strTempFileName);
			sw.Write(body.ToString());
			sw.Close();

            // Log the size of the file created
            FileInfo scriptInfo = new FileInfo(strTempFolder + @"\" + strTempFileName);
            if (scriptInfo.Exists)
            {
                // Log the size of the file
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Infrastructure,
                    TDTraceLevel.Info,
                    "ScriptName \"" + scriptName + "\" generated with size of " + scriptInfo.Length);

                Logger.Write(oe);
            }

			// Check if script exists in repository
			Hashtable scriptFiles = (Hashtable)scripts[scriptName];
			
			if (scriptFiles == null) 
			{
				// Script does not exist
				scriptFiles = new Hashtable();
				scripts.Add(scriptName, scriptFiles);
				scriptFiles.Add(browserSupport, strTempFileRef);
			}
			else if (!scriptFiles.ContainsKey(browserSupport))
			{
				// Script exists, but not for current DOM style
				scriptFiles.Add(browserSupport, strTempFileRef);
			}

			return GetScript(scriptName, browserSupport);
		}

        /// <summary>
        /// Registers a dynamically generated script to the repository at run-time and
        /// return JavaScript reference to it.
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="browserSupport"></param>
        /// <returns></returns>
        public bool RegisterTempScript(string scriptName, string browserSupport)
        {
            // Create TempScript directory in case it does not exist
            Directory.CreateDirectory(Directory.GetParent(configFilePath).FullName + @"\tempscripts");
            DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts");
            DirectoryInfo tempDir = asubDir[0];

            string strTempFolder = tempDir.FullName;
            string strTempFileName = scriptName + "_" + browserSupport + ".js";
            string strTempFileRef = Directory.GetParent(configFilePath).Name + "/"
                                        + tempDir.Name + "/"
                                            + strTempFileName;

            // Log error if no script exists
            FileInfo scriptInfo = new FileInfo(strTempFolder + @"\" + strTempFileName);
            if (!scriptInfo.Exists)
            {
                // Log the size of the file
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Infrastructure,
                    TDTraceLevel.Error,
                    "ScriptName \"" + scriptName + "\" does not exist in " + scriptInfo.FullName);

                Logger.Write(oe);

                return false;
            }

            // Check if script exists in repository
            Hashtable scriptFiles = (Hashtable)scripts[scriptName];

            if (scriptFiles == null)
            {
                // Script does not exist
                scriptFiles = new Hashtable();
                scripts.Add(scriptName, scriptFiles);
                scriptFiles.Add(browserSupport, strTempFileRef);
            }
            else if (!scriptFiles.ContainsKey(browserSupport))
            {
                // Script exists, but not for current DOM style
                scriptFiles.Add(browserSupport, strTempFileRef);
            }

            return true;
        }

        /// <summary>
        /// Removes temporary javascript from the repository at run time
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="browserSupport"></param>
        /// <param name="deleteFiles">if true deletes the physical files as well</param>
        public void RemoveTempScript(string scriptName, string browserSupport, bool deleteFiles)
        {
            DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts");
            DirectoryInfo tempDir = asubDir[0];

            string strTempFolder = tempDir.FullName;
            string strTempFileName = scriptName + "_" + browserSupport + ".js";
            string strTempFileRef = Directory.GetParent(configFilePath).Name + "/"
                                        + tempDir.Name + "/"
                                            + strTempFileName;

            if (deleteFiles)
            {
                if (File.Exists(strTempFolder + @"\" + strTempFileName))
                {
                    File.Delete(strTempFolder + @"\" + strTempFileName);
                }
            }

            // Check if script exists in repository
            Hashtable scriptFiles = (Hashtable)scripts[scriptName];

            if (scriptFiles != null)
            {
                scripts.Remove(scriptName);
            }
            
        }

		/// <summary>
		/// Return a piece of JavaScript to reference the JavaScript file:
		/// <script language=""JavaScript"" src=""scriptsFolder/fileName""></script>
		/// </summary>
		private string GenerateJavaScript(string filePath)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(@"<script language=""JavaScript"" src=""");
			sb.Append(rootFilePath);
			sb.Append(@"/");
			sb.Append(filePath);
			sb.Append(@""" type=""text/javascript""></script>");
			return sb.ToString();
		}
		#endregion

	}


}

