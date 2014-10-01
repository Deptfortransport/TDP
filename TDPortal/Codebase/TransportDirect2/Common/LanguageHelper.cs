﻿// *********************************************** 
// NAME             : LanguageHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 01 Feb 2012
// DESCRIPTION  	: LanguageHelper class containing helper methods
// ************************************************


namespace TDP.Common
{
    /// <summary>
    /// LanguageHelper class containing helper methods
    /// </summary>
    public static class LanguageHelper
    {
        #region Public Static Properties

        /// <summary>
        /// Gets the default language for the application
        /// </summary>
        public static Language Default
        {
            get { return Language.English; }
        }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Parses the Culture name and returns a Language value. Throws exception for unrecognised langauge
        /// </summary>
        public static Language ParseLanguage(string cultureName)
        {
            switch (cultureName.ToLower())
            {
                case "en":
                    return Language.English;
                case "cy":
                    return Language.Welsh;
                default:
                    throw new TDPException(string.Format("Language {0} not handled", cultureName), false, TDPExceptionIdentifier.RMLanguageNotHandled);
            }
        }

        /// <summary>
        ///Retuns a language string for given language enum value
        /// </summary>
        public static string GetLanguageString(Language language)
        {
            switch (language)
            {
                case Language.Welsh:
                    return "cy";
                case Language.English:
                default:
                    return "en";
            }
        }

        #endregion
    }
}
