// ***********************************************
// NAME 		: DuplicateTiplocProviderFactory.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: Service Factory for the DuplicateTiplocProvider
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DuplicateTiplocProviderFactory.cs-arc  $
//
//   Rev 1.0   Jul 01 2010 12:49:06   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
    public class DuplicateTiplocProviderFactory: IServiceFactory
	{
		#region private members

		/// <summary>
        /// Singleton instance of DuplicateTiplocProvider
		/// </summary>
		DuplicateTiplocProvider current;

		#endregion

		/// <summary>
		/// Constructor.
		/// </summary>
        public DuplicateTiplocProviderFactory()
		{
            current = new DuplicateTiplocProvider(TransportDirect.Common.DatabaseInfrastructure.SqlHelperDatabase.TransientPortalDB); 
		}

		#region public methods
		/// <summary>
		/// Method used to get an instance of the Duplicate Tiploc provider. 
		/// </summary>
		/// <returns>An instance of DuplicateTiplocProvider</returns>
		public object Get()
		{
			return current;
		}
		#endregion

	}
}
