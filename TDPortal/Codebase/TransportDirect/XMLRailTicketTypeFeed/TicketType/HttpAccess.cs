// *********************************************** 
// NAME                 : HttpAccess.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Used for accessing Xml from an HTTP source
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

namespace TransportDirect.Common.RailTicketType
{
    public class HttpAccess
    {
        private static HttpAccess instance = null;

        /// <summary>
        /// Constructor
        /// </summary>
        private HttpAccess()
        {
        }

        /// <summary>
        /// Returns the singleton instance of the HttpAccess object
        /// </summary>
        public static HttpAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpAccess();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the XML file from an HTTP source
        /// </summary>
        /// <param name="HttpPath">The HTTP Source</param>
        /// <returns>An Xml Document containing the Xml from the Http Source</returns>
        public XmlDocument GetXMLFromHttp(string HttpPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpPath);
                return doc;
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
               TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.HttpAccess - " + ex.Message));

                throw new TDException("TransportDirect.Common.RailTicketType.HttpAccess  - " + ex.Message, true, TDExceptionIdentifier.XMLRTTTicketParser);
            }
        }
    }
}
