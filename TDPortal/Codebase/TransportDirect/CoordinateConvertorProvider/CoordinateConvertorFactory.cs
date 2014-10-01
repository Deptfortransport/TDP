// *********************************************** 
// NAME                 : CoordinateConvertorFactory.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION 	        : This class provides a single Get method that creates a 
//				          new instance of the CoordinateConvertor class for each call
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CoordinateConvertorProvider/CoordinateConvertorFactory.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:09:24   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Collections.Generic;
using System.Text;
    
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CoordinateConvertorProvider
{
    /// <summary>
    /// This class provides a single Get method that creates a new instance 
    /// of the CoordinateConvertor class for each call.
    /// Does not register for Change notification, so will not be reloaded after initial application start.
    /// </summary>
    public class CoordinateConvertorFactory : IServiceFactory
    {
        #region Private members

        private ICoordinateConvertor current;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor.
		/// </summary>
        public CoordinateConvertorFactory()
		{
            current = new CoordinateConvertor();
		}

		#endregion

        #region IServiceFactory Members
        /// <summary>
        /// Method used by the ServiceDiscovery to get the instance of the CoordinateConvertor.
        /// </summary>
        /// <returns>The current instance of the CoordinateConvertor.</returns>
        public object Get()
        {
            return current;
        }

        #endregion
    }
}
