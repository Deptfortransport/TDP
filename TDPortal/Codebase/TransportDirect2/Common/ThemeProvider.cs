// *********************************************** 
// NAME             : ThemeProvider.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Jun 2013
// DESCRIPTION  	: ThemeProvider class, basic implementation to return default theme information,
//                  : Future enhancement to replicate functionality as in TDP
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common
{
    /// <summary>
    /// ThemeProvider class
    /// </summary>
    public class ThemeProvider
    {
        #region Private Constants

        private const string defaultThemeName = "TransportDirect";
        private const int defaultThemeId = 1;

        #endregion

        #region Private Static Fields

        private static readonly object instanceLock = new object();
        private static ThemeProvider instance = null;

        #endregion

        #region Public Static Fields

        /// <summary>
        /// Gets the sinlgeton instance
        /// </summary>
        public static ThemeProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ThemeProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Singleton private constructor
        /// </summary>
        private ThemeProvider()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method returns the default theme id
        /// </summary>
        /// <returns></returns>
        public int GetDefaultThemeId()
        {
            return defaultThemeId;
        }

        /// <summary>
        /// This method returns the default theme name
        /// </summary>
        /// <returns></returns>
        public string GetDefaultThemeName()
        {
            return defaultThemeName;
        }

        #endregion
    }
}
