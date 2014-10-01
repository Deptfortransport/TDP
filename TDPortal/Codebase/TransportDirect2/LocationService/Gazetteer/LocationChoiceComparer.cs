// *********************************************** 
// NAME             : LocationChoiceComparer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Class used to compare location choices
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Class used to compare location choices
    /// </summary>
    public class LocationChoiceComparer : IComparer
    {
        /// <summary>
        /// Compares 2 locations choices
        /// </summary>
        /// <param name="x">choice x</param>
        /// <param name="y">choice y</param>
        /// <returns>returns 0 if choicex==choicey. returns int lower than 0 if choicex lower than choicey. returns int greater than 0 if choicex greater than choicey</returns>
        public int Compare(object x, object y)
        {
            if (!(x is LocationChoice) || !(y is LocationChoice))
            {
                throw new TDPException("Can't compare those object. At least one of them is not a LocationChoice object", false, TDPExceptionIdentifier.LSWrongType);
            }

            LocationChoice xLocation = (LocationChoice)x;
            LocationChoice yLocation = (LocationChoice)y;

            if (xLocation.Score > yLocation.Score)
                return -1;
            else if (xLocation.Score < yLocation.Score)
                return 1;
            else
            {
                if (xLocation.Description.CompareTo(yLocation.Description) < 0)
                    return -1;
                else
                    if (xLocation.Description.CompareTo(yLocation.Description) > 0)
                        return 1;
                    else
                        return 0;
            }
        }
    }
}