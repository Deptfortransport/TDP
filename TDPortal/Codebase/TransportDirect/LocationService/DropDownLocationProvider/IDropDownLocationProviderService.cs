// ************************************************ 
// NAME                 : IDropDownLocationProviderService.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : DropDownLocationProvider service interface
//                      : This interface provides the way to interact with core classes 
//                        providing the functionality of creating dropdown data javascript files and 
//                        also moniters change notifications to create new data files
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/IDropDownLocationProviderService.cs-arc  $
//
//   Rev 1.1   Jun 04 2010 13:23:02   apatel
//Updated
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.0   Jun 04 2010 11:27:34   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownLocation Provider service interface
    /// </summary>
    interface IDropDownLocationProviderService
    {
        /// <summary>
        /// This method returns a string array of Script names that have been generated 
        /// for the JavaScript data files. The PageId value indicates which mode 
        /// the “auto-suggest” drop down logic is being added to.
        /// <example>for PageId.FindTrainInput, the DropDownLocationType.Rail data script names are returned</example>
        /// Any PageId’s not supported by the provider will return an empty string array.
        /// The Script name refers to the data script added to the ScriptRepository
        /// </summary>
        /// <param name="pageId">PageId enum value</param>
        /// <returns>string array of Script names that have been generated for the JavaScript data files</returns>
        string[] GetDropDownLocationDataScriptName(PageId pageId);

        /// <summary>
        /// This method uses the enabled flags set during the class constructor to return 
        /// a Boolean indicating if the drop down locations should be added to the page’s input controls
        /// </summary>
        /// <param name="pageId">PageId enum value</param>
        /// <returns>true if the AutoSuggest functionality enabled for the page</returns>
        bool DropDownLocationEnabled(PageId pageId);
    }
}
