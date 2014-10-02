//// ***********************************************
//// NAME           : ContentDatabaseConnectionStringHelper.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 09-Mar-2008
//// DESCRIPTION 	: Helper class that provides the connection string for the content database
//// ************************************************
////
////    Rev Devfactory Mar 09 2008 sbarker
////    Initial version

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace TD.ThemeInfrastructure
{
    /// <summary>
    /// Helper class to provide the connection string for the content database. This is stored
    /// in the configuration file.
    /// </summary>
    public static class ContentDatabaseConnectionStringHelper
    {
        #region Private Constants

        private const string contentDatabaseConfigurationKey = "contentDb.connectionstring";

        #endregion

        #region Public Static Methods

        public static string Get()
        {
            return ConfigurationManager.AppSettings[contentDatabaseConfigurationKey];
        }

        #endregion        
    }
}
