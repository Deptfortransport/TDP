//// ***********************************************
//// NAME           : Theme.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 27-Feb-2008
//// DESCRIPTION 	: Models a theme, storing the name and Id
//// ************************************************
////    Rev Devfactory Jan 27 2008 14:00:00   sbarker
////    CCN 0427 - First version

using System;
using System.Collections.Generic;
using System.Text;

namespace TD.ThemeInfrastructure
{
    /// <summary>
    /// Immutable class to model a theme
    /// </summary>
    public class Theme
    {
        #region Private Fields

        private int id;
        private string name;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Standard constructor, allowing name and id to be set
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Theme(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the id of the theme
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Gets the name of the theme
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        #endregion
    }
}
