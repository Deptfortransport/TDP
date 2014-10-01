// *********************************************** 
// NAME			: EnvironmentalBenefitsHelper.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 23/09/2009
// DESCRIPTION	: Class to provided helper methods for the environmental benefits calculator
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefitsHelper.cs-arc  $
//
//   Rev 1.1   Oct 11 2009 12:52:56   mmodi
//Method to remove toid prefix
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 13:58:48   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class to provided helper methods for the environmental benefits calculator
    /// </summary>
    public static class EnvironmentalBenefitsHelper
    {
        /// <summary>
        /// Checks the road number and determines its EBCRoadCategory type.
        /// This method does not determine if the road is a EBCRoadCategory.MotorwayHigh
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        public static EBCRoadCategory GetEBCRoadCategory(string roadNumber)
        {
            EBCRoadCategory ebcRoadCategory = EBCRoadCategory.None;

            if (string.IsNullOrEmpty(roadNumber))
            {
                ebcRoadCategory = EBCRoadCategory.RoadOther;
            }
            else
            {
                roadNumber = roadNumber.Trim().ToLower();

                if ((roadNumber.StartsWith("m")) | (roadNumber.EndsWith("(m)")))
                {
                    ebcRoadCategory = EBCRoadCategory.MotorwayStandard;
                }
                else if (roadNumber.StartsWith("a"))
                {
                    ebcRoadCategory = EBCRoadCategory.RoadStandardA;
                }
                else
                {
                    ebcRoadCategory = EBCRoadCategory.RoadOther;
                }
            }

            return ebcRoadCategory;
        }

        /// <summary>
        /// Removes the specified prefix from all toids in the array
        /// </summary>
        /// <param name="toids"></param>
        /// <param name="toidPrefix"></param>
        /// <returns></returns>
        public static string[] RemoveToidPrefix(string[] toids, string toidPrefix)
        {
            if ((toids != null) && (toids.Length > 0))
            {
                string[] formattedToids = new string[toids.Length];

                for (int i = 0; i < toids.Length; i++)
                {
                    string toid = toids[i];

                    if (toidPrefix.Length > 0 && toid.StartsWith(toidPrefix))
                    {
                        toid = toid.Substring(toidPrefix.Length);
                    }

                    formattedToids[i] = toid;
                }

                return formattedToids;
            }
            else
            {
                return new string[0];
            }
        }
    }
}
