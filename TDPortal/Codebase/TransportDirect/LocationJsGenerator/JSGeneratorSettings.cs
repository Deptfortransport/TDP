// ************************************************ 
// NAME                 : JSGeneratorSettings.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Provides all settings used in generating location javascripts
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationJsGenerator/JSGeneratorSettings.cs-arc  $
//
//   Rev 1.0   Aug 28 2012 10:35:36   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Provides all settings used in generating location javascripts
    /// </summary>
    class JSGeneratorSettings
    {
        #region Private Fields

        private static List<string> commandLineArgs = null;

        /// <summary>
        /// Used to store a default version. 
        /// If no version supplied as an argument then datetime now is used
        /// </summary>
        private static string versionDefault = DateTime.Now.ToString("yyyyMMddHHmm");

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandLineArguments">Command line arguments</param>
        public JSGeneratorSettings(string[] commandLineArguments)
        {
            if ((commandLineArguments != null) && (commandLineArguments.Length > 0))
            {
                commandLineArgs = new List<string>(commandLineArguments);
            }
            else
            {
                commandLineArgs = new List<string>();
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// The location for writing the scripts to
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string ScriptFolder(JSGeneratorMode mode)
        {
            if (Properties.Current == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(Properties.Current["ScriptFolder." + mode]))
                return Properties.Current["ScriptFolder." + mode];

            if (!string.IsNullOrEmpty(Properties.Current["ScriptFolder"]))
                return Properties.Current["ScriptFolder"];

            throw new TDException("ScriptFolder has not been defined in properties", false, TDExceptionIdentifier.LJSGenMissingProperty);
        }

        /// <summary>
        /// The prefix name for the scripts
        /// </summary>
        public static string ScriptName(JSGeneratorMode mode)
        {
            if (Properties.Current == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(Properties.Current["ScriptName." + mode]))
                return Properties.Current["ScriptName." + mode];

            if (!string.IsNullOrEmpty(Properties.Current["ScriptName"]))
                return Properties.Current["ScriptName"];

            throw new TDException("ScriptName has not been defined in properties", false, TDExceptionIdentifier.LJSGenMissingProperty);
        }

        #endregion

        #region Public static Properties

        /// <summary>
        /// Read only property determining the alias file location
        /// </summary>
        public static string LocationsAliasFile
        {
            get
            {
                if (Properties.Current == null)
                    return string.Empty;

                return Properties.Current["LocationsAliasFile"];
            }
        }

        /// <summary>
        /// Read only property determining the connection string to be used to load the location data
        /// </summary>
        public static SqlHelperDatabase LocationsDatabase
        {
            get
            {
                if (Properties.Current == null)
                    return SqlHelperDatabase.DefaultDB;

                if (!string.IsNullOrEmpty(Properties.Current["LocationsDatabase"]))
                {
                    try
                    {
                        return (SqlHelperDatabase)Enum.Parse(typeof(SqlHelperDatabase), Properties.Current["LocationsDatabase"], true);
                    }
                    catch
                    {
                        throw new TDException("LocationsDatabase property is not regcognised, ensure value is a valid SqlHelperDatabase enumeration", false, TDExceptionIdentifier.LJSGenMissingProperty);
                    }
                }

                throw new TDException("LocationsDatabase has not been defined in properties", false, TDExceptionIdentifier.LJSGenMissingProperty);
            }
        }

        /// <summary>
        /// Read only property determining the stored procedure to be used to load the locations data
        /// </summary>
        public static string LocationsStoredProcedure
        {
            get
            {
                if (Properties.Current == null)
                    return string.Empty;

                if (!string.IsNullOrEmpty(Properties.Current["LocationsStoredProcedure"]))
                    return Properties.Current["LocationsStoredProcedure"];

                throw new TDException("LocationsStoredProcedure has not been defined in properties", false, TDExceptionIdentifier.LJSGenMissingProperty);
            }
        }

        /// <summary>
        /// Read only property determining the stored procedure to be used to load the location data
        /// </summary>
        public static string LocationStoredProcedure
        {
            get
            {
                if (Properties.Current == null)
                    return string.Empty;

                if (!string.IsNullOrEmpty(Properties.Current["LocationStoredProcedure"]))
                    return Properties.Current["LocationStoredProcedure"];

                throw new TDException("LocationStoredProcedure has not been defined in properties", false, TDExceptionIdentifier.LJSGenMissingProperty);
            }
        }

        /// <summary>
        /// Read only property determining the version string to use in generating the full name of the script files
        /// </summary>
        /// <remarks>
        /// If the version is provided through command line the property will return that otherwise the property 
        /// returns the date the script generated in yyyyMMdd format
        /// </remarks>
        public static string Version
        {
            get
            {
                string version = GetCommandLineValue("version");

                if (string.IsNullOrEmpty(version))
                    version = GetCommandLineValue("v");

                if (string.IsNullOrEmpty(version))
                {
                    return versionDefault;
                }

                return version;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a command line param value defined following a key i.e. /key keyvalue
        /// </summary>
        /// <param name="key">key for the value</param>
        /// <returns>command line param value</returns>
        private static string GetCommandLineValue(string key)
        {
            string keyValue = string.Empty;

            if (commandLineArgs != null)
            {
                // Check if key exists
                if (!string.IsNullOrEmpty(key.Trim())
                    && commandLineArgs.Count > 0 
                    && (commandLineArgs.Contains("/" + key) || commandLineArgs.Contains("\\" + key)) )
                {
                    int keyIndex = 0;
                    if (commandLineArgs.Contains("/" + key))
                        keyIndex = commandLineArgs.IndexOf("/" + key);
                    else if (commandLineArgs.Contains("\\" + key))
                        keyIndex = commandLineArgs.IndexOf("\\" + key);
                        
                    int valIndex = keyIndex + 1;
                    
                    // Retrieve the next val, checking if it is a val or key
                    if (valIndex < commandLineArgs.Count)
                    {
                        string val = commandLineArgs[valIndex];

                        if ((!val.StartsWith("/")) && (!val.StartsWith("\\")))
                        {
                            keyValue = val;
                        }
                    }
                }
            }

            return keyValue;
        }

        #endregion
    }
}
