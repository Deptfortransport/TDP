// *********************************************** 
// NAME             : CycleAlgorithmAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Helper class to create all the avilable penalty function
//                    in to CycleAlgorithm array
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleAlgorithmAssembler.cs-arc  $
//
//   Rev 1.1   Oct 15 2010 10:55:10   apatel
//Updated to accept multiple Cycle algorithm dlls (Doc Ref ATO687)
//Resolution for 5622: Update CTP to accept multiple function dlls (Doc Ref: ATO687)
//
//   Rev 1.0   Sep 29 2010 10:39:36   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                                          
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Assemby class containing methods to build cycle algoritm dto object suppored by cycle planner
    /// </summary>
    public class CycleAlgorithmAssembler
    {
        #region Private Fields
        private string PENALTYFUNCTION_DLL = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Dll";
        #endregion

        #region Public Methods
        /// <summary>
        /// Builds and returns various cycle algorithms (i.e. Quiet, Quickest, etc) dto objects and returns
        /// them so it can be used to build cycle journey planning request object
        /// </summary>
        /// <returns>Array of CycleAlgorithm objects</returns>
        public CycleAlgorithm[] GetCycleAlgorithms()
        {
            DataServices ds = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            ArrayList cycleAlgorithms = ds.GetList(DataServiceType.CycleJourneyType);

            List<CycleAlgorithm> cycleAlgorithmList = new List<CycleAlgorithm>();

            for (int i = 0; i < cycleAlgorithms.Count; i++)
            {
                DSDropItem cycleJourneyType = (DSDropItem)cycleAlgorithms[i];
                
                CycleAlgorithm algorithm = new CycleAlgorithm();
                
                algorithm.AlgorithmDescription = cycleJourneyType.ResourceID;
                algorithm.AlgorithmPenaltyFunction = cycleJourneyType.ItemValue;

                algorithm.AlgorithmDLL = Properties.Current[string.Format(PENALTYFUNCTION_DLL, cycleJourneyType.ItemValue)];

                algorithm.AlgorithmCall = string.Format("Call {0},{1}",
                    Properties.Current[string.Format(PENALTYFUNCTION_DLL, cycleJourneyType.ItemValue)], cycleJourneyType.ItemValue);

                cycleAlgorithmList.Add(algorithm);

            }

            return cycleAlgorithmList.ToArray();

        }
        #endregion
    }
}
