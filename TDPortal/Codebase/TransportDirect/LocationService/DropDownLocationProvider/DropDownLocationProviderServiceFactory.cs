// ************************************************ 
// NAME                 : DropDownLocationProviderServiceFactory.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : Implementation of IServiceFactory for the DropDownLocationProviderService
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocationProviderServiceFactory.cs-arc  $
//
//   Rev 1.3   Jun 14 2010 16:43:44   apatel
//Updated comments
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 14 2010 11:54:56   apatel
//Updated
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Jun 04 2010 13:22:42   apatel
//Updated
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.0   Jun 04 2010 11:27:32   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownLocationProviderServiceFactory class
    /// </summary>
    public class DropDownLocationProviderServiceFactory : IServiceFactory
    {
        #region Private Fields
        private DropDownLocationProviderService current;
        #endregion

        #region Constructor
        /// <summary>
        /// Standard constructor. Initialises the DropDownLocationProviderService
		/// </summary>
        public DropDownLocationProviderServiceFactory()
		{
            current = new DropDownLocationProviderService();
		}
        #endregion

        #region IServiceFactory Members
        /// <summary>
        /// Gets the current DropDownLocationProviderService object
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            return current;
        }

        #endregion
    }
}
