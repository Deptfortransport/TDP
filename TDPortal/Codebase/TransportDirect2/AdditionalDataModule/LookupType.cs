// *********************************************** 
// NAME             : LookupType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2014
// DESCRIPTION  	: LookupType class to specify enumeration values to AdditionalData database table
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TDP.UserPortal.AdditionalDataModule
{
    /// <summary>
    /// LookupType class to specify enumeration values to AdditionalData database table
    /// Enum type class for the various Types we can lookup given a NaPTAN
    /// A enum isn't used because:
    /// 1. Some of the values have spaces in them
    /// 2. It is convenient to hang to values of the key - type name and category name
    /// </summary>
    [Serializable]
    public class LookupType
    {
        #region Public static variables

        public static Hashtable lookupTypes = new Hashtable();

        public static LookupType NLC_Code = new LookupType("NLC Code", "Lookup");
        public static LookupType CRS_Code = new LookupType("CRS Code", "Lookup");

        #endregion

        #region Private variables

        private string type = string.Empty;
        private string category = string.Empty;

        #endregion

        #region Private constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        private LookupType(string type, string category)
        {
            this.type = type;
            this.category = category;

            // Add the newly created LookupType to the HashTable for easy retrieval
            lookupTypes.Add(type, this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// ToString method, returns Type 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return type;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. Type
        /// </summary>
        public string Type
        {
            get { return type; }
        }

        /// <summary>
        /// Read only. Categorhy
        /// </summary>
        public string Category
        {
            get { return category; }
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Finds LookupType for a given string type, returns Null if not found
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static LookupType FindLookupType(string type)
        {
            if (lookupTypes.ContainsKey(type))
            {
                return (LookupType)lookupTypes[type];
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}