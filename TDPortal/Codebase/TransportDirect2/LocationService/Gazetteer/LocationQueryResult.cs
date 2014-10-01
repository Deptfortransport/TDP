// *********************************************** 
// NAME             : LocationQueryResult.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Class used to hold the gazetteer query results
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Class used to hold the gazetteer query results
    /// </summary>
    [Serializable()]
    public class LocationQueryResult
    {
        #region Private members

        private LocationChoiceList choiceList;
        private LocationChoice parentChoice;
        private bool isVague = false;
        private bool isSingleWordAddress = false;
        private string pickListUsed = string.Empty;
        private string queryReference = string.Empty;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pickListUsed"></param>
        public LocationQueryResult(string pickListUsed)
        {
            this.choiceList = new LocationChoiceList();
            this.pickListUsed = pickListUsed;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Bool that indicates the return from the gazetteer location search is vague
        /// i.e. too many results
        /// </summary>
        public bool IsVague
        {
            get { return isVague; }
            set { isVague = value; }
        }

        /// <summary>
        /// Bool that indicates the address was a single word search
        /// </summary>
        public bool IsSingleWordAddress
        {
            get { return isSingleWordAddress; }
            set { isSingleWordAddress = value; }
        }


        /// <summary>
        /// Read-Write property. get/set the query reference
        /// </summary>
        public string QueryReference
        {
            get { return queryReference; }
            set { queryReference = value; }
        }

        /// <summary>
        /// Choice used to create this result (for hierarchic searches)
        /// </summary>
        public LocationChoice ParentChoice
        {
            get { return parentChoice; }
            set { parentChoice = value; }
        }

        /// <summary>
        /// Read-Write property. get/set the locationChoice list
        /// </summary>
        public LocationChoiceList LocationChoiceList
        {
            get { return choiceList; }
            set { choiceList = value; }
        }

        /// <summary>
        /// Read-only property. get pickList string used 
        /// </summary>
        public string PickListUsed
        {
            get { return pickListUsed; }
        }

        #endregion
    }
}