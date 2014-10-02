//********************************************************************************
//NAME         : FilePropertyProvider.cs
//AUTHOR       : Phil Scott
//DATE CREATED : 02/07/2003
//DESCRIPTION  : Class file providing methods to read application properties from
//             : an Xml file as defined in calling application's Web.config file.
//DESIGN DOC   : DD00601? Properties Service
//********************************************************************************
// Version   Ref        Name        Date         Description
// V1.0      DD00601    Phil Scott  02/07/2003   Initial version
//
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/FilePropertyProvider/FilePropertyProvider.cs-arc  $
//
//   Rev 1.1   Apr 03 2008 15:24:44   mmodi
//Updated to load PropertyDictionary with theme
//Resolution for 4631: Del 10 - Transaction injector is not working correctly
//
//   Rev 1.0   Nov 08 2007 12:37:44   mturner
//Initial revision.
//
//   Rev 1.15   Feb 23 2006 19:15:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.14.1.1   Oct 13 2005 10:35:20   pcross
//Updates to FilePropertyProvider from FXCop
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.14.1.0   Oct 07 2005 08:41:46   pcross
//Version for microsite. Processes PartnerId element.
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.14   Oct 03 2003 13:38:40   PNorell
//Updated the new exception identifier.
//
//   Rev 1.13   Jul 30 2003 18:50:08   geaton
//Reverted back to old OperationalEvent constructor.
//
//   Rev 1.12   Jul 29 2003 18:32:36   geaton
//Swapped OperationalEvent parameter order after change in OperationalEvent constructor.
//
//   Rev 1.11   Jul 25 2003 10:38:06   passuied
//addition of CLSCompliant
//
//   Rev 1.10   Jul 23 2003 10:22:54   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.9   Jul 18 2003 11:01:12   passuied
//useless exception removed
//
//   Rev 1.8   Jul 17 2003 17:17:16   passuied
//added exception handler
//
//   Rev 1.7   Jul 17 2003 14:59:18   passuied
//updated after code review

using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Timers;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using System.Xml.XPath;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.PropertyService.FilePropertyProvider
{
	/// <summary>
	/// This class provides a method of extracting property definitions
	/// from an Xml file.  The Xml filepath/name is defined in the 
	/// application web config file. The class inherits from the general
	/// properties class which implements the IPropertyProvider interface. 
	/// </summary>
	public class FilePropertyProvider : Properties.Properties
	{


        private int themeId = 0;

		public FilePropertyProvider()
		{
			propertyTable = new Hashtable();

            propertyDictionary = new System.Collections.Generic.Dictionary<int, Hashtable>();

			intVersion = 0;
		
		}

		public override IPropertyProvider Load()
		{
            // Read General ConfigurationSettings (AppID Group ID property assembly and class)
            strThemeID =
                ConfigurationManager.AppSettings
                ["propertyservice.themeid"];
			strApplicationID = 
				ConfigurationManager.AppSettings
				["propertyservice.applicationid"];
			strGroupID = 
				ConfigurationManager.AppSettings
				["propertyservice.groupid"];

            if(Version == 0)
			{
                // Attempt to convert themeid to int for later when adding to Property Dictionary
                #region Convert ThemeId
                try
                {
                    themeId = int.Parse(strThemeID);
                }
                catch
                {
                    OperationalEvent oe =
                            new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Error,
                            "An error has occurred attempting to convert the ThemeId to an integer: " + strThemeID);
                    Logger.Write(oe);
                    throw new TDException(
                        "Exception trying to load the Property Xml File. The ThemeId could not be converted to an integer: " + strThemeID,
                        true,
                        TDExceptionIdentifier.PSInvalidPropertyFile);
                }
                #endregion

				try
				{
					// Get unique provider settings ( filename
					string providerFile = (string)ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];

					// populate properties
					// open the xml file
					XmlNodeList xNodelist;
					XmlDocument xdLookup = new XmlDocument();
					xdLookup.Load(providerFile);
				
					// step through xml file and load all properties.
                    
                    // before first select the properties for this theme
                    xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@ThemeId=\"" +ThemeID+ "\"]");
                    foreach (XmlNode node in xNodelist)
                    {
                        AddProperty(node);
                    }
				
					// first select the properties for this application
					xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@AID=\""+ApplicationID+"\"]");
					foreach (XmlNode node in xNodelist)
					{
						AddProperty(node);
					}

					// Next select the properties for this GroupId
					xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@GID=\""+GroupID+"\"]");
					foreach (XmlNode node in xNodelist)
					{
						AddProperty(node);
					}
				
					// Next select the global properties
					xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@GID=\"\"]");
					foreach (XmlNode node in xNodelist)
					{
						AddProperty(node);
					}

					// find latest version from property table
					intVersion = GetVersion();

					return this;
				}
				catch (ArgumentException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Business,
						TDTraceLevel.Error,
						"An error has occurred manipulating the Properties Xml File");
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to manipulate the Property Xml File : " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidPropertyFile);
					
				}
				catch (TDException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Business,
						TDTraceLevel.Error,
						"An error has occurred manipulating the Properties Xml File");
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to manipulate the Property Xml File : " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidPropertyFile);
					
				}
			
			}

			// if changed version
			else if (IsNewVersion)
			{
				try
				{
					

					FilePropertyProvider newInstance  = new FilePropertyProvider();


					// Call Load in new instantiated object and return it
					return newInstance.Load();
				}
				catch (ArgumentException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Business,
						TDTraceLevel.Error,
						"An error has occurred manipulating the Properties Xml File");
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to manipulate the Property Xml File : " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidPropertyFile);
					
				}
				catch (TDException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Business,
						TDTraceLevel.Error,
						"An error has occurred manipulating the Properties Xml File");
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to manipulate the Property Xml File : " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidPropertyFile);
					
				}
				
			}
			else
			{
				return null;
			
			}	

			
			
		}

		public bool IsNewVersion
		{
			get
			{

				try
				{
					int latestVersion = GetVersion();

					if(latestVersion == Version)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				catch (TDException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Business,
						TDTraceLevel.Error,
						"An error has occurred manipulating the Properties Xml File");
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to manipulate the Property Xml File : " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidPropertyFile);
					
				}
			}
		}

		/// <summary>
		/// Adds a property to the propertyTable
		/// </summary>
		/// <param name="node"></param>
		private void AddProperty(XmlNode node)
		{
			string partnerId = "0";
			if (node.Attributes["PartnerId"] != null)
				if (node.Attributes["PartnerId"].InnerText.Length > 0)
					if (FilePropertyProvider.IsNumeric(node.Attributes["PartnerId"].InnerText))
						partnerId = node.Attributes["PartnerId"].InnerText;

			string key = partnerId + "." + node.Attributes["name"].InnerText;

			string strValue = node.InnerText;
			
			// if property already exists in the table dont add it.
			if(!propertyTable.Contains(key))
				propertyTable.Add(key, strValue); 

            // Properties now use a dictionary so populate this as well
            if (!propertyDictionary.ContainsKey(themeId))
                propertyDictionary.Add(themeId, new Hashtable());

            if (!propertyDictionary[themeId].Contains(key))
            {
                propertyDictionary[themeId].Add(key, strValue);
            }

		}

		/// <summary>
		/// Function to test if string value represents an integer
		/// </summary>
		/// <param name="toTest"></param>
		/// <returns></returns>
		private static bool IsNumeric(string toTest)
		{
			// Attempt the conversion and if it errors it's not an integer
			try
			{
				Convert.ToInt32(toTest, CultureInfo.InvariantCulture);
				return true;
			}
			catch (System.InvalidCastException)
			{
			}
			return false;
		}

		/// <summary>
		/// Reads the version from the currently saved properties file
		/// </summary>
		/// <returns></returns>
		private int GetVersion()
		{

			int latestVersion = 0;

			try
			{
				// Get unique provider settings ( filename
				string providerFile = (string)ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];

				XmlNode xnQuery;
				XmlDocument xdLookup = new XmlDocument();
				xdLookup.Load(providerFile);
				xnQuery = xdLookup.DocumentElement.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				latestVersion = Convert.ToInt32(xnQuery.InnerXml, CultureInfo.CurrentCulture.NumberFormat);
			}

			catch (NullReferenceException e)
			{
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"An error has occurred accessing the version property in the Properties Xml File : Null Reference");

				Logger.Write(oe);
				throw new TDException(
					"Exception trying to get the version property from property Xml file : "+e.Message, 
					false, 
					TDExceptionIdentifier.PSInvalidVersion);
			}
			catch (XPathException e)
			{
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"An error has occurred accessing the version property in the Properties Xml File : incorrect XPath");

				Logger.Write(oe);
				throw new TDException(
					"Exception trying to get the version property from property Xml file : "+e.Message, 
					false, 
					TDExceptionIdentifier.PSInvalidVersion);
			}
			catch (XmlException e)
			{
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"An error has occurred accessing the version property in the Properties Xml File");

				Logger.Write(oe);
				throw new TDException(
					"Exception trying to get the version property from property Xml file : "+e.Message, 
					false, 
					TDExceptionIdentifier.PSInvalidVersion);
					
			}
			catch (ArgumentException e)
			{
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"An error has occurred accessing the version property in the Properties Xml File");

				Logger.Write(oe);
				throw new TDException(
					"Exception trying to get the version property from property Xml file : "+e.Message, 
					false, 
					TDExceptionIdentifier.PSInvalidVersion);
					
			}

			return latestVersion;

		}

	}
}
