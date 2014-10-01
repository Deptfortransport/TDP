// *********************************************** 
// NAME                 : ICoordinateConvertor.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Interface class outlining methods available for the CoordinateConvertor
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CoordinateConvertorProvider/ICoordinateConvertor.cs-arc  $
//
//   Rev 1.1   Oct 01 2009 10:53:16   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.0   Jun 03 2009 11:09:24   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;

namespace TransportDirect.UserPortal.CoordinateConvertorProvider
{
    public interface ICoordinateConvertor
    {
        /// <summary>
        /// Converts an OSGR into a LatitudeLongitude by calling the CoordinateConvertor web service 
        /// </summary>
        LatitudeLongitude GetLatitudeLongitude(OSGridReference osgr);

        /// <summary>
        /// Converts the OSGRs into LatitudeLongitudes by calling the CoordinateConvertor web service
        /// </summary>
        LatitudeLongitude[] GetLatitudeLongitude(OSGridReference[] osgrs);

        /// <summary>
        /// Converts a LatitudeLongitude into an OSGR by calling the CoordinateConverter web service
        /// </summary>
        OSGridReference GetOSGridReference(LatitudeLongitude latlong);

        /// <summary>
        /// Converts the LatitudeLongitudes into OSGRs by calling the CoordinateConverter web service
        /// </summary>
        OSGridReference[] GetOSGridReference(LatitudeLongitude[] latlongs);
    }
}
