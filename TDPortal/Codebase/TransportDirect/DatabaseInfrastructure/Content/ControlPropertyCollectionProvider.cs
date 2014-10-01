//// ***********************************************
//// NAME           : ControlPropertyCollectionProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 21-Jan-2008
//// DESCRIPTION 	: Acts as a manager for ControlPropertyCollections, determining when a collection has expired
//// ************************************************
////    Rev Devfactory Jan 21 2008 12:00:00   sbarker
////    CCN 0427 - Acts as a manager for ControlPropertyCollections, determining when a collection has expired

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Serves non-expired ControlPropertyCollections
    /// </summary>
    internal sealed class ControlPropertyCollectionProvider
    {
        #region Private Fields

        /// <summary>
        /// Private collection used to stored active ControlPropertyCollections
        /// </summary>
        private Dictionary<LanguageThemeName, ControlPropertyCollection> dictionary = new Dictionary<LanguageThemeName, ControlPropertyCollection>();
        private readonly object dictionaryLock = new object();

        #endregion

        #region Default Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        internal ControlPropertyCollectionProvider()
        {
            //No implementation
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Determines whether a control collection for a specified language and
        /// request uri (related to partner) exists. If it does, the required 
        /// collection is returned as out parameter.
        /// </summary>
        /// <param name="key">Represents the language and theme name to allow the correct
        /// values to be loaded</param>
        /// <param name="controlPropertyCollection">An out parameter that contains the 
        /// required collection if the method return is true.</param>
        /// <returns>Returns true if a collection for the required language exists. 
        /// False otherwise.</returns>
        internal bool CanGetControlPropertyCollection(LanguageThemeName key, out ControlPropertyCollection controlPropertyCollection)
        {
            lock (dictionaryLock)
            {
                controlPropertyCollection = null;
                
                if (dictionary.ContainsKey(key))
                {
                    controlPropertyCollection = dictionary[key];
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Allows a control collection to be added to the internal collection
        /// </summary>
        /// <param name="key">The language and theme of the collection being added</param>
        /// <param name="controlPropertyCollection">The collection to add</param>
        internal void SetControlPropertyCollection(LanguageThemeName key, ControlPropertyCollection controlPropertyCollection)
        {
            lock (dictionaryLock)
            {
                dictionary[key] = controlPropertyCollection;
            }
        }

        #endregion
    }
}
