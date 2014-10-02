// ************************************************ 
// NAME                 : DropDownLocationType.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : DropDownLocationType is an enum representing the different type of 
//                        location drop down data files which can be generated. This allows generation 
//                        of mode specific JavaScript data files for use on different pages 
//                        and helps maintain smaller file sizes for download
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocationType.cs-arc  $
//
//   Rev 1.0   Jun 04 2010 11:27:34   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// Enum which defines the different Drop Down Location Types available
    /// </summary>
    public enum DropDownLocationType
    {
        Rail
    }

}
