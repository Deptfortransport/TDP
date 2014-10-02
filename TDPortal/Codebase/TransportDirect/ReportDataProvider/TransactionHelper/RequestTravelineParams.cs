// *********************************************** 
// NAME                 : RequestTravelineParams
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Parameters for Traveline Checker
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestTravelineParams.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.0   Feb 19 2009 15:45:28   mturner
//Changes to implement via proxy server functionality.
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:39:44   mturner
//Initial revision.
//
//   Rev 1.1   Nov 17 2004 11:04:58   passuied
//Added RequestTravelineParams
//
//   Rev 1.0   Nov 15 2004 17:44:14   passuied
//Initial revision.


using System;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	[Serializable]
	public class Traveline
	{
		string sUrl = string.Empty;
        bool useProxy = false;
        string proxyName = string.Empty;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Traveline ()
		{
		}

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="url"></param>
		/// <param name="isAim"></param>
		public Traveline (string url, bool viaProxy, string proxy)
		{
			this.sUrl = url;
            this.useProxy = viaProxy;
            this.proxyName = proxy;
		}

		/// <summary>
		/// Read-Write property. Traveline Url
		/// </summary>
		public string Url
		{
			get
			{
				return sUrl;
			}
			set
			{
				sUrl = value;
			}
		}

        /// <summary>
        /// Read-Write Property. Indicates whether this traveline is reached via a proxy or not
        /// </summary>
        public bool UseProxy
        {
            get
            {
                return useProxy;
            }
            set
            {
                useProxy = value;
            }
        }

        /// <summary>
        /// Read-Write property. The name of the proxy server to be used.
        /// </summary>
        public string ProxyName
        {
            get
            {
                return proxyName;
            }
            set
            {
                proxyName = value;
            }
        }
	
	}

	/// <summary>
	/// Parameters for travelineChecker components.
	/// </summary>
	[Serializable]
	public struct RequestTravelineParams
	{
		public TDLocation OriginLocation;
		public TDLocation DestinationLocation;
		public Traveline[] Travelines;

        
	}

}
