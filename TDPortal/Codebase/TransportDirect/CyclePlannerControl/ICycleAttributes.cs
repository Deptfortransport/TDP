// *********************************************** 
// NAME			: ICycleAttributes.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/09/2008
// DESCRIPTION	: Definition of the CycleAttributes class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ICycleAttributes.cs-arc  $
//
//   Rev 1.1   Sep 29 2010 11:26:14   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.0   Oct 10 2008 15:40:50   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public interface ICycleAttributes
    {
        /// <summary>
        /// Returns an array of all CycleAttribute objects
        /// </summary>
        /// <returns>CycleAttribute[]</returns>
        CycleAttribute[] GetCycleAttributes();

        /// <summary>
        /// Returns an array of CycleAttribute for the specified CycleAttributeGroup
        /// </summary>
        /// <param name="cycleAttributeType">CycleAttributeType</param>
        /// <param name="cycleAttributeGroup">CycleAttributeGroup</param>
        /// <returns>CycleAttribute[]</returns>
        CycleAttribute[] GetCycleAttributes(CycleAttributeType cycleAttributeType, CycleAttributeGroup cycleAttributeGroup);

        /// <summary>
        /// Returns an array of CycleAttributes for the specified CycleAttributeGroup. Attributes returned will 
        /// be those attributes which have been defined as specific Cycle Infrastructure. 
        /// Can be used to determine when the Cycle icon is displayed
        /// </summary>
        /// <param name="cycleAttributeGroup">CycleAttributeGroup</param>
        /// <returns>CycleAttribute[]</returns>
        CycleAttribute[] GetCycleInfrastructureAttributes(CycleAttributeGroup cycleAttributeGroup);

        /// <summary>
        /// Returns an array of CycleAttributes for the specified CycleAttributeGroup. Attributes returned will 
        /// be those attributes which have been defined as Recommended cycle route attributes. 
        /// Can be used to determine when the Cycle (recommended route) icon is displayed
        /// </summary>
        /// <param name="cycleAttributeGroup">CycleAttributeGroup</param>
        /// <returns>CycleAttribute[]</returns>
        CycleAttribute[] GetCycleRecommendedAttributes(CycleAttributeGroup cycleAttributeGroup);
    }
}
