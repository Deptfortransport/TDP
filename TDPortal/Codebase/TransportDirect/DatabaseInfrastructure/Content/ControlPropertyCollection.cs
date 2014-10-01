//// ***********************************************
//// NAME           : ControlPropertyCollection.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 21-Jan-2008
//// DESCRIPTION 	: Contains control property names and values in a fashion that enables control looping
//// ************************************************
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
////    Rev Devfactory Jan 21 2008 12:00:00   sbarker
////    CCN 0427 - Contains control property names and values in a fashion that enables control looping

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Contains control property names and values in a fashion that enables control looping
    /// </summary>
    public class ControlPropertyCollection
    {
        #region Private Fields
        
        /// <summary>
        /// Field to store all controls, properties and values. Although the field looks
        /// nasty and complex, it must be this way in order to provide the required
        /// functionality. The outer dictionary is keyed on control name, and has
        /// an inner dictionary as its values. The inner dictionary is keyed on 
        /// property name. Thus, contentDictionary["txtTest"] will return a 
        /// dictionary of all property names and values available for a control 
        /// called txtTest. Going one step further, 
        /// contentDictionary["txtTest"]["Text"] will return the string value
        /// of the property Text for control txtTest.
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, string>> contentDictionary = new Dictionary<string, Dictionary<string, string>>();
        private readonly DateTime creationDateTime;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor in which all required data is set for the collection. The
        /// data is stored in a "double dictionary". See comments against the
        /// field contentDictionary for more information.
        /// </summary>
        /// <param name="contentDictionary">All data needed to set up the object. 
        /// See comments against the field contentDictionary for more 
        /// information as to why a "double dictionary" has been used. </param>
        internal ControlPropertyCollection(Dictionary<string, Dictionary<string, string>> contentDictionary)
        {
            this.contentDictionary = contentDictionary;
            creationDateTime = DateTime.Now;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of controls handled by the collection.
        /// </summary>
        /// <returns></returns>
        public int GetControlCount()
        {
            return contentDictionary.Keys.Count;
        }

        /// <summary>
        /// Gets the date and time the collection was created
        /// </summary>
        public DateTime CreationDateTime
        {
            get
            {
                return creationDateTime;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Given a control index in the dictionary, the name of that control is returned 
        /// </summary>
        /// <param name="controlIndex"></param>
        /// <returns></returns>
        public string GetControlName(int controlIndex)
        {
            string[] keys = new string[contentDictionary.Keys.Count];
            contentDictionary.Keys.CopyTo(keys, 0);
            return keys[controlIndex];
        }

        /// <summary>
        /// Given a control index in the dictionary, the name of all available properties in the collection 
        /// for the control are returned, as a string array.
        /// </summary>
        /// <param name="controlIndex"></param>
        /// <returns></returns>
        public string[] GetPropertyNames(int controlIndex)
        {
            string[] keys = new string[contentDictionary.Keys.Count];
            contentDictionary.Keys.CopyTo(keys, 0);
            string controlName = keys[controlIndex];
            //Make an array to return:
            string[] returnArray = new string[contentDictionary[controlName].Keys.Count];

            //Now copy the key names to the return array:
            contentDictionary[controlName].Keys.CopyTo(returnArray, 0);

            return returnArray;
        }
       

        /// <summary>
        /// Given a control name, the name of all available properties in the collection 
        /// for the control are returned, as a string array.
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string[] GetPropertyNames(string controlName)
        {
            //Make the input lower:
            controlName = controlName.ToLower();

            //If the inner dictionary contains nothing against the control, then
            //the count is zero:
            if (!contentDictionary.ContainsKey(controlName))
            {
                return new string[] { };
            }

            //Make an array to return:
            string[] returnArray = new string[contentDictionary[controlName].Keys.Count];

            //Now copy the key names to the return array:
            contentDictionary[controlName].Keys.CopyTo(returnArray, 0);

            return returnArray;
        }

        /// <summary>
        /// Given a control name and property name, the relevant value is
        /// returned. If a matching value cannot be found, a 
        /// ContentProviderException is thrown.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetPropertyValue(int controlIndex, string propertyName)
        {
            //Make the inputs lower:
            string[] keys = new string[contentDictionary.Keys.Count];
            contentDictionary.Keys.CopyTo(keys, 0);
            string controlName = keys[controlIndex];

            propertyName = propertyName.ToLower();

            try
            {
                return contentDictionary[controlName][propertyName];
            }
            catch (KeyNotFoundException)
            {
                string message = string.Format("Could not find a value for {0}.{1}", controlName, propertyName);
                return null;
            }                
        }

        /// <summary>
        /// Given a control name and property name, the relevant value is
        /// returned. If a matching value cannot be found, a 
        /// ContentProviderException is thrown.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetPropertyValue(string controlName, string propertyName)
        {
            //Make the inputs lower:
            controlName = controlName.ToLower();
            propertyName = propertyName.ToLower();

            try
            {
                return contentDictionary[controlName][propertyName];
            }
            catch (KeyNotFoundException)
            {
                string message = string.Format("Could not find a value for {0}.{1}", controlName, propertyName);
                return null;
            }                
        }
        #endregion
    }
}
