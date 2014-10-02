// *********************************************** 
// NAME                 : TLChecker
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Base class for traveline checkers
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/TLChecker.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.1   Feb 23 2009 12:38:46   mturner
//Improved code that formats TL response into stream for debugging purposes.
//
//   Rev 1.0.1.0   Feb 19 2009 15:42:18   mturner
//Changes to implement via proxy server functionality.
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:46   mturner
//Initial revision.
//
//   Rev 1.3   Nov 22 2004 13:00:26   passuied
//added more comments
//
//   Rev 1.2   Nov 17 2004 16:00:10   passuied
//changes for FxCop
//
//   Rev 1.1   Nov 17 2004 11:26:24   passuied
//First working version
//
//   Rev 1.0   Nov 15 2004 17:32:44   passuied
//Initial revision.


using System;
using System.Text;
using System.IO;
using System.Net;

using TransportDirect.Common;
using TransportDirect.ReportDataProvider.TransactionHelper;

namespace TransportDirect.ReportDataProvider.TravelineChecker
{
	/// <summary>
	/// Base class for traveline checkers
	/// </summary>
	public class TLChecker : ITLChecker
	{
		protected TLJourneyWebRequest jWRequest;
		protected string sUrl;
        protected string sProxy;
        protected bool bUseProxy;
        
		#region Public members
		/// <summary>
		/// Default Constructor
		/// </summary>
		public TLChecker()
		{
			jWRequest = null;
			sUrl = string.Empty;
		}

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="request">Journey Web Request</param>
		/// <param name="url">Traveline url</param>
		public TLChecker(TLJourneyWebRequest request, string url, bool useProxy, string proxy)
		{
			jWRequest = request;
			sUrl = url;
            bUseProxy = useProxy;
            sProxy = proxy;
		}

		/// <summary>
		/// Read-Write Property.JourneyWeb request to use for traveline checking
		/// </summary>
		public TLJourneyWebRequest JourneyWebRequest
		{
			get
			{
				return jWRequest;
			}
			set
			{
				jWRequest = value;
			}
		}

		/// <summary>
		/// Read-Write Property. Url of the traveline to test.
		/// </summary>
		public string TravelineUrl
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
        /// Read-Write Property. Url of the proxy server to use.
        /// </summary>
        public string ProxyUrl
        {
            get
            {
                return sProxy;
            }
            set
            {
                sProxy = value;
            }
        }

        /// <summary>
        /// Read-Write Property. Url of the traveline to test.
        /// </summary>
        public bool UseProxy
        {
            get
            {
                return bUseProxy;
            }
            set
            {
                bUseProxy = value;
            }
        }

		/// <summary>
		/// Check traveline and returns number of returned journeys
		/// </summary>
		/// <returns>returns the number of returned journeys</returns>
		public virtual int Check()
		{
			if (jWRequest == null || sUrl.Length == 0)
			{
				throw new TDException("Error in Check() method. JourneyWebRequest and Traveline Url both need to be set", false, TDExceptionIdentifier.Undefined);
			}

			byte[] received = null;
			try
			{
				WebClient wc = new WebClient();
				wc.Headers.Add("Content-Type: text/xml");
                if (this.bUseProxy == true)
                {
                    WebProxy proxyServer = new WebProxy(this.sProxy, true);
                    WebRequest.DefaultWebProxy = proxyServer;
                }
                
                received = wc.UploadData(sUrl, "POST", jWRequest.GetBytes() );
			}
			catch (Exception e)
			{
				throw new TDException(string.Format(Messages.TravelineRequestError, sUrl, e.Message), false, TDExceptionIdentifier.RDPTravelineCheckerError);
			}
			
			return GetReturnedJourneysCount(received);
		}

		#endregion

		#region Protected methods
		/// <summary>
		/// Extracts the number of returned journeys from the received data byte array
		/// </summary>
		/// <param name="receivedData">byte array. Received data to analyse</param>
		/// <returns>number of returned journeys</returns>
		protected int GetReturnedJourneysCount(byte[] receivedData)
		{
			// Debug : easily check content of result data.
			//FileStream fs = new FileStream(@"C:\temp\jwanswer.txt", FileMode.Append);
			//fs.Write(receivedData,0,receivedData.Length);
            //byte[] newLine = new byte[]{10,13};
            //fs.Write(newLine,0,2);
            //fs.Flush();
			//fs.Close();

			// Check if the response contains more than 0 <Journey> 
			// Either read it in as XML docu or just scan byte array for <Journey>
			byte[] seek = ASCIIEncoding.Default.GetBytes( "<Journey>" );
			int returnedJourneys = ScanArray( receivedData, seek, 0 );

			return returnedJourneys;
		}

		/// <summary>
		/// Convenience method. Determins if data starts with startsWith from offset
		/// </summary>
		/// <param name="data">data to analyse</param>
		/// <param name="startsWith">byte array to search</param>
		/// <param name="offset">offset to start from</param>
		/// <returns>True if the data array begins with the startsWith array</returns>
		protected bool StartsWith( byte[] data, byte[] startsWith, int offset ) 
		{
			int index = 0;
			// while index in data not greater than data
			// and stop if startWith length has been reached (i.e. data starts with <startWith>
			while( index + offset < data.Length && index < startsWith.Length )
			{
				if( data[index + offset] != startsWith[index ] )
				{
					return false;
				}
				index++;
			}
			return index == startsWith.Length;
		}
		

		/// <summary>
		/// Goes through byte array and returns the number of instances of seek byte[] 
		/// found in data byte[], starting from offset
		/// </summary>
		/// <param name="data">data to analyse</param>
		/// <param name="seek">snippet to search</param>
		/// <param name="offset">offset to start from</param>
		/// <returns>number of instances of seek in data</returns>
		protected int ScanArray(byte[] data, byte[] seek, int offset )
		{
			int total_found = 0;
			int index = 0;
			while( index < data.Length )
			{
				if( data[index] == seek[0] )
				{
					if(StartsWith( data, seek, index ) )
					{
						// snippet found. increment total and index
						total_found++;
						index += seek.Length;
						continue; // and skip normal increment
					}
				}
				// Increase index
				index++;
			}
			return total_found;
		}
		#endregion
	}
}
