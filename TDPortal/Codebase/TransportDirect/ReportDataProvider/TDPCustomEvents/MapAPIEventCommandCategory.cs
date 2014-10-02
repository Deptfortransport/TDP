// NAME             : MapAPIEvent.cs
// AUTHOR           : Amit Patel
// DATE CREATED     : 12/11/2009 
// DESCRIPTION      : Enumeration containing command classifiers for <c>MapAPIEvent</c>.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapAPIEventCommandCategory.cs-arc  $ 
//
//   Rev 1.1   Mar 19 2010 13:00:40   mmodi
//Added file Headers
//
// Added header as original author had failed to add

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Enumeration containing command classifiers for <c>MapAPIEvent</c>.
    /// </summary>
    public enum MapAPIEventCommandCategory : int
    {
        UseAsStartPoint = 1,
        UseAsViaPoint,
        UseAsEndPoint,
        ShowCarParkInformation,
        ShowStopsInformation
    }
}
