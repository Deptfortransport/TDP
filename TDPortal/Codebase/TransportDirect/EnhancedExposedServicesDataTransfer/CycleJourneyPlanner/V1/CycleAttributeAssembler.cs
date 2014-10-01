// *********************************************** 
// NAME             : CycleAttributeAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Assembler class containing methods to return cycle attribute dto objects
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleAttributeAssembler.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:38   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ServiceDiscovery;

using CyclePlannerControl = TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Assembler class containing methods to return cycle attribute dto objects
    /// </summary>
    public class CycleAttributeAssembler
    {
        #region Public Methods
        /// <summary>
        /// Calls the CycleAttribute service's GetCycleAttributes method to return all the cycle attributes
        /// </summary>
        /// <returns></returns>
        public CycleAttribute[] GetCycleAttributes()
        {
            // Get the cycle attributes we're allowed to display from service discovery
            CyclePlannerControl.ICycleAttributes cycleAttributesService = (CyclePlannerControl.ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

            List<CycleAttribute> cycleAttributeList = new List<CycleAttribute>();

            foreach (CyclePlannerControl.CycleAttribute attribute in cycleAttributesService.GetCycleAttributes())
            {
                CycleAttribute cycleAttribute = new CycleAttribute();

                cycleAttribute.AttributeId = attribute.CycleAttributeId;

                cycleAttribute.AttributeDescription = GetCycleAttributeDescription(attribute.CycleAttributeResourceName);

                cycleAttribute.AttributeType = attribute.CycleAttributeType.ToString();

                cycleAttribute.AttributeGroup = attribute.CycleAttributeGroup.ToString();

                cycleAttribute.AttributeInteger = (uint)attribute.CycleAttributeMask;

                cycleAttribute.AttributeMask = String.Format("{0:X}", cycleAttribute.AttributeInteger);

                cycleAttributeList.Add(cycleAttribute);
            }

            return cycleAttributeList.ToArray();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the description for the specified Cycle Attribute resource name
        /// </summary>
        /// <param name="cycleAttributeResourceName">Cycle attribute resource name</param>
        /// <returns></returns>
        private string GetCycleAttributeDescription(string cycleAttributeResourceName)
        {
            TDResourceManager rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

            return rm.GetString(cycleAttributeResourceName);
        }
        #endregion 
    }
}
