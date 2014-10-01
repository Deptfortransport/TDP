using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDP.TestProject.CommandAndControl
{
    [TestClass]
    public class TDPDashboardTest
    {
        [TestMethod]
        public void DashboardWebsiteTest()
        {
            // NB1: Unfortunately code coverage will not pick this up as it tests the website directly

            // NB2: To run this test you will need to add an application to the default website that points at the TDPDashboard project

            // Call out using web classes
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/TDPDashboard/Default.aspx");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader srResponse = new StreamReader(responseStream);
            string theResponse = srResponse.ReadToEnd();

            // Check for the titles
            if (!theResponse.Contains("Dashboard"))
            {
                Assert.Fail("Dashboard main page not returned 1");
            }

            if (!theResponse.Contains("Latest Monitoring Data"))
            {
                Assert.Fail("Dashboard main page not returned 2");
            }

            if (!theResponse.Contains("Historical Monitoring Data"))
            {
                Assert.Fail("Dashboard main page not returned 3");
            }


            request = (HttpWebRequest)WebRequest.Create("http://localhost/TDPDashboard/TDPChecksumMonitoringResults/List.aspx");
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            srResponse = new StreamReader(responseStream);
            theResponse = srResponse.ReadToEnd();

            if (!theResponse.Contains("ChecksumMonitoringResults"))
            {
                Assert.Fail("ChecksumMonitoringResults page not returned");
            }


            request = (HttpWebRequest)WebRequest.Create("http://localhost/TDPDashboard/TDPDatabaseMonitoringResults/List.aspx");
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            srResponse = new StreamReader(responseStream);
            theResponse = srResponse.ReadToEnd();

            if (!theResponse.Contains("DatabaseMonitoringResults"))
            {
                Assert.Fail("DatabaseMonitoringResults page not returned");
            }


            request = (HttpWebRequest)WebRequest.Create("http://localhost/TDPDashboard/TDPFileMonitoringResults/List.aspx");
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            srResponse = new StreamReader(responseStream);
            theResponse = srResponse.ReadToEnd();

            if (!theResponse.Contains("FileMonitoringResults"))
            {
                Assert.Fail("FileMonitoringResults page not returned");
            }


            request = (HttpWebRequest)WebRequest.Create("http://localhost/TDPDashboard/TDPWMIMonitoringResults/List.aspx");
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            srResponse = new StreamReader(responseStream);
            theResponse = srResponse.ReadToEnd();

            if (!theResponse.Contains("WMIMonitoringResults"))
            {
                Assert.Fail("WMIMonitoringResults page not returned");
            }
        }
    }
}
