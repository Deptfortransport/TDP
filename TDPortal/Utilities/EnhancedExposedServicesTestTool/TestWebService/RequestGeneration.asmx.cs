using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace TestWebService
{
    /// <summary>
    /// Summary description for RequestGeneration
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class RequestGeneration : System.Web.Services.WebService
    {

        [WebMethod]
        public string GenerateRequest(string transactionId)
        {
            string SoapEnv = "succeeded at " + DateTime.Now;

            return SoapEnv;
        }
    }
}
