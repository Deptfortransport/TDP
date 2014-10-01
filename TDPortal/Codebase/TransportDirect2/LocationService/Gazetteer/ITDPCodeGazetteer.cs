// *********************************************** 
// NAME             : ITDPCodeGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Feb 2014
// DESCRIPTION  	: Interface for TDPCodeGazetteer
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Interface for TDPCodeGazetteer
    /// </summary>
    public interface ITDPCodeGazetteer
    {
        /// <summary>
        /// Method that identifies a given code as a valid code or not. 
        /// </summary>
        /// <param name="code">Code to identify.</param>
        /// <returns>If valid, all associated codes will be returned in a an array of CodeDetail objects.
        /// Otherwise, the array will be empty</returns>
        CodeDetail[] FindCode(string code);

        /// <summary>
        /// Method that takes a given text entry and searches for associated codes.
        /// </summary>
        /// <param name="text">Text to find</param>
        /// <param name="fuzzy">Indicate if user is unsure of spelling</param>
        /// <param name="modeTypes">Requested mode types-associated codes to be returned.</param>
        /// <returns>If codes are found, all matching ones will be returned in an array of CodeDetail objects.
        /// Otherwise, the array will be empty.</returns>
        CodeDetail[] FindText(string text, bool fuzzy, TDPModeType[] modeTypes);
    }
}
