// *********************************************** 
// NAME             : LocationChoiceList.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: List of LocationChoice objects
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// List of LocationChoice objects
    /// </summary>
    [Serializable()]
    public class LocationChoiceList : List<LocationChoice>
    {
        #region Private members

        private int level = 0;
        private bool isVague = false;
        private bool isSingleWordAddress = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationChoiceList()
        {
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
        /// Read-only property. LocationChoiceList level
        /// </summary>
        public int Level
        {
            get { return level; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Set LocationChoiceList level
        /// </summary>
        /// <param name="level"></param>
        public LocationChoiceList(int level)
        {
            this.level = level;
        }

        #endregion

        #region List override methods

        // MMODI - COMMENTED OUT UNTIL CONFIRM NOT NEEDED

        ///// <summary>
        ///// Overriden LocationChoiceList Add method
        ///// </summary>
        ///// <param name="choice">choice to add</param>
        ///// <returns>returns the index where the choice has been added</returns>
        //public override void Add(object choice)
        //{
        //    if (!(choice is LocationChoice))
        //        throw new TDPException(
        //            "Can't add the object of type different to LocationChoice", false,
        //            TDPExceptionIdentifier.LSWrongType);

        //    base.Add((LocationChoice)choice);
        //}

        ///// <summary>
        ///// Overriden LocationChoiceList Insert method
        ///// </summary>
        ///// <param name="index">index where to insert the locationChoice in the list</param>
        ///// <param name="choice">choice to insert</param>
        //public override void Insert(int index, object choice)
        //{
        //    if (!(choice is LocationChoice))
        //        throw new TDPException(
        //            "Can't insert the object of type different to LocationChoice", false,
        //            TDPExceptionIdentifier.LSWrongType);

        //    base.Insert(index, (LocationChoice)choice);
        //}

        ///// <summary>
        ///// Overriden indexer 
        ///// </summary>
        //public override LocationChoice this[int index]
        //{
        //    get { return base[index]; }
        //    set
        //    {
        //        if (!(value is LocationChoice))
        //            throw new TDPException(
        //                "Can't insert the object of type different to LocationChoice", false,
        //                TDPExceptionIdentifier.LSWrongType);
        //    }
        //}

        ///// <summary>
        ///// Overriden Remove method
        ///// </summary>
        ///// <param name="choice">choice to remove</param>
        //public override void Remove(object choice)
        //{
        //    if (!(choice is LocationChoice))
        //        throw new TDPException(
        //            "Can't remove the object of type different to LocationChoice", false,
        //            TDPExceptionIdentifier.LSWrongType);

        //    base.Remove((LocationChoice)choice);
        //}

        #endregion
    }
}