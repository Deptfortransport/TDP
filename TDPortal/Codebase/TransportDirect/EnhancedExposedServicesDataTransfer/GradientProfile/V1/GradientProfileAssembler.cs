// *********************************************** 
// NAME             : GradientProfileAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Assembler class containing methods for converting between Domain and Data Transfer objects
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/GradientProfileAssembler.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:04   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.CyclePlannerControl;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// Assembler class containing methods for converting between Domain and Data Transfer objects
    /// </summary>
    public class GradientProfileAssembler
    {
        #region Public Methods
        
        #region Request

        /// <summary>
        /// Method uses parameters from a dto request object to create a domain request object
        /// </summary>
        public static ITDGradientProfileRequest CreateTDGradientProfileRequest(GradientProfileRequest gradientProfileRequest)
        {
            // At a minimum, the request needs to contain at least one polylineGroup to get gradient profile
            if ((gradientProfileRequest != null) && (gradientProfileRequest.PolylineGroups != null) && (gradientProfileRequest.PolylineGroups.Length > 0))
            {
                ITDGradientProfileRequest tdGradientProfileRequest = new TDGradientProfileRequest() ;
                
                #region Set Result Settings
                if (gradientProfileRequest.Settings != null)
                {
                    tdGradientProfileRequest.Resolution = gradientProfileRequest.Settings.Resolution;
                    tdGradientProfileRequest.EastingNorthingSeperator = Convert.ToChar(gradientProfileRequest.Settings.EastingNorthingSeperator);
                    tdGradientProfileRequest.PointSeperator = Convert.ToChar(gradientProfileRequest.Settings.PointSeperator);
                }
                #endregion

                #region Set Polyline Groups

                Dictionary<int,TDPolyline[]> tdPolylineGroups = new Dictionary<int,TDPolyline[]>();

                foreach (PolylineGroup group in gradientProfileRequest.PolylineGroups)
                {
                    List<TDPolyline> polylines = new List<TDPolyline>();

                    foreach (Polyline polyline in group.Polylines)
                    {
                        polylines.Add(new TDPolyline(polyline.ID, 
                            GetPolylineOSGRs(polyline.PolylineGridReferences,tdGradientProfileRequest.PointSeperator, tdGradientProfileRequest.EastingNorthingSeperator), 
                            polyline.InterpolateGradient));
                    }

                    tdPolylineGroups.Add(group.ID, polylines.ToArray());
                }

                tdGradientProfileRequest.TDPolylines = tdPolylineGroups;

                #endregion


                return tdGradientProfileRequest;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Result
        /// <summary>
        /// Method which creates a GradientProfileResult dto object from the TD Gradient Profile result object
        /// </summary>
        /// <param name="tdGradientProfileRequest"></param>
        /// <param name="tdGradientProfileResult"></param>
        /// <param name="rm"></param>
        /// <param name="errorMessage"></param>
        /// <param name="tdExId"></param>
        /// <returns></returns>
        public static GradientProfileResult CreateGradientProfileResultDT(ITDGradientProfileRequest tdGradientProfileRequest,
            ITDGradientProfileResult tdGradientProfileResult, TDResourceManager rm,
            string errorMessage, TransportDirect.Common.TDExceptionIdentifier tdExId)
        {
            // The return dto object
            GradientProfileResult GradientProfileDTO = new GradientProfileResult();

            ArrayList errorMessageDT = new ArrayList();
            ArrayList warningMessageDT = new ArrayList();

            // If theres a result object, then create the outward and/or return car journey
            if (tdGradientProfileResult != null)
            {
                if (tdGradientProfileResult.IsValid)
                {

                    GradientProfileDTO.Resolution = tdGradientProfileResult.Resolution;

                    List<HeightPointGroup> heightPointGroups = new List<HeightPointGroup>();
                    foreach (KeyValuePair<int, TDHeightPoint[]> group in tdGradientProfileResult.TDHeightPoints)
                    {
                        HeightPointGroup heightPointGroup = new HeightPointGroup();
                        heightPointGroup.ID = group.Key;

                        List<HeightPoint> heightPoints = new List<HeightPoint>();
                        foreach (TDHeightPoint tdHeightPoint in group.Value)
                        {
                            heightPoints.Add(new HeightPoint(tdHeightPoint.ID, tdHeightPoint.Height));
                        }

                        heightPointGroup.HeightPoints = heightPoints.ToArray();

                        heightPointGroups.Add(heightPointGroup);

                    }

                    GradientProfileDTO.HeightGroups = heightPointGroups.ToArray();
                   
                }

                #region Add errors

                // Add any warnings or error messages
                if (tdGradientProfileResult.Messages.Length != 0)
                {
                    foreach (CyclePlannerMessage gradientProfilerMessage in tdGradientProfileResult.Messages)
                    {
                        if (gradientProfilerMessage.Type == ErrorsType.Warning)
                        {
                            warningMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(gradientProfilerMessage.MessageResourceId), (int)TDExceptionIdentifier.JPFailedToPlanJourney));
                        }
                        else if (gradientProfilerMessage.Type == ErrorsType.Error)
                        {
                            errorMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(gradientProfilerMessage.MessageResourceId), (int)TDExceptionIdentifier.JPCJPErrorsOccured));
                        }
                    }
                }

                #endregion
            }

            #region Assign warnings and errors

            // If an error message has been supplied, assign that to the error messages list
            if (!string.IsNullOrEmpty(errorMessage))
            {
                #region Add errors

                errorMessageDT.Add(CommonAssembler.CreateMessageDT(errorMessage, (int)tdExId));

                #endregion
            }

            // Convert and assign warning and error messages
            if (warningMessageDT.Count > 0)
            {
                GradientProfileDTO.UserWarnings = (Message[])warningMessageDT.ToArray(typeof(Message));
            }

            if (errorMessageDT.Count > 0)
            {
                GradientProfileDTO.ErrorMessages = (Message[])errorMessageDT.ToArray(typeof(Message));
            }

            #endregion

            #region Create CompletionStatus object

            if (tdGradientProfileResult != null)
            {
                bool requestOK = true;

                if ((tdGradientProfileResult.Messages != null) && (tdGradientProfileResult.Messages.Length > 0))
                {
                    requestOK = false;
                }


                if (requestOK)
                {
                    GradientProfileDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(true, 0, string.Empty);
                }
                else
                {
                    GradientProfileDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanAllJourneys,
                        "Currently unable to obtain gradient profile using the request parameters supplied.");
                }

            }
            else
            {
                // This error should not be reached, there should always be a result journey, otherwise exception 
                // would have been thrown to user by calling class
                // Added for completeness.
                GradientProfileDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanJourney,
                    "Currently unable to obtain gradient profile using the request parameters supplied.");
            }

            #endregion

            return GradientProfileDTO;
        }
        #endregion

        #endregion

        #region Private Methods

        #region Request
        /// <summary>
        /// Returns the OSGR coordinates array from the polyline string in the request
        /// </summary>
        /// <returns></returns>
        private static TransportDirect.UserPortal.LocationService.OSGridReference[] GetPolylineOSGRs(string polyline, char pointSeparator, char eastingNorthingSeparator)
        {
            List<TransportDirect.UserPortal.LocationService.OSGridReference> gridRefereneces = new List<TransportDirect.UserPortal.LocationService.OSGridReference>();

            foreach (string polylineGridReference in polyline.Split(pointSeparator))
            {
                string[] gridReference = polylineGridReference.Split(eastingNorthingSeparator);

                int easting = int.Parse(gridReference[0]);
                int northing = int.Parse(gridReference[1]);

                gridRefereneces.Add(new TransportDirect.UserPortal.LocationService.OSGridReference(easting, northing));
            }

            return gridRefereneces.ToArray();
        }
        #endregion

        #endregion
    }
}
