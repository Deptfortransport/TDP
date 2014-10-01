//// ***********************************************
//// NAME           : LanguageThemeName.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : Feb-2008
//// DESCRIPTION 	: Key to allow language and theme to be retrieved as one
//// ************************************************
////
////    Rev Devfactory Feb 2008 12:00:00   sbarker
////    CCN 0427 - First version

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Used as a key to store language and theme concurrently
    /// </summary>
    internal class LanguageThemeName
    {
        #region Private Fields

        private readonly Language language;
        private readonly string themeName;

        #endregion

        #region Constructor

        internal LanguageThemeName(Language language, string themeName)
        {
            this.language = language;
            this.themeName = themeName;
        }

        #endregion

        #region Internal Fields

        /// <summary>
        /// Gets the language
        /// </summary>
        internal Language Language
        {
            get
            {
                return language;
            }
        }

        /// <summary>
        /// Gets the theme name
        /// </summary>
        internal string ThemeName
        {
            get
            {
                return themeName;
            }
        }

        #endregion

        #region Public Override Methods
        
        public override int GetHashCode()
        {
            //We simple build a string of language and theme name, and return 
            //that:
            string key = string.Format("{0}|{1}", Language.ToString(), ThemeName);
            return key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            LanguageThemeName incoming = obj as LanguageThemeName;

            //Check for the right type incoming:
            if (incoming == null)
            {
                return false;
            }

            //Check each condition in turn. This is not the 
            //most compact way to do this, but it is the most readable!
            if (incoming.language != this.language)
            {
                return false;
            }

            if (incoming.themeName != this.themeName)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
