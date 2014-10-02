// *********************************************** 
// NAME             : SocketClient.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 09 Dec 2013
// DESCRIPTION  	: SocketClient class responsible for establishing communication with RTTI server on specific port
//                  : 
//                  : This SocketClient class has been moved to this project from
//                  : TransportDirect.UserPortal.DepartureBoardService.RTTIManager
//                  : 
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using System.Globalization;
using System.Text;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.WebService.DepartureBoardWebService.RDManager
{
    /// <summary>
    /// SocketClient class responsible for establishing communication with RTTI server on specific port
    /// </summary>
    public class SocketClient
    {
        #region Private Variables

        private TcpClient myclient;
        private NetworkStream networkStream;
        private StreamWriter streamWriter;
        private int portNumber;
        private string serverName;
        private int iRetryCount;
        private int iMillisecondsTimeout;
        private int iNetworkStreamTimeout;
        private string socketResp;
        const int maxCount = 1024;
        private ArrayList retryAttempt = new ArrayList();

        #endregion

        #region Class contructor

        /// <summary>
        /// Constructor for SocketClient class 
        /// It initialises socket port number and server address/name
        /// </summary>
        public SocketClient()
        {
            // getting and assigning socket port
            portNumber = int.Parse(Properties.Current["DepartureBoards.EnquiryPorts.Service.SocketPort"].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture.NumberFormat);
            // getting and assigning server name 
            serverName = (string)Properties.Current["DepartureBoards.EnquiryPorts.Service.SocketServer"];

            // getting retry count 
            iRetryCount = int.Parse(Properties.Current["DepartureBoards.EnquiryPorts.Service.RetryCount"].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture.NumberFormat);

            // getting retry waiting wait time 
            iMillisecondsTimeout = int.Parse(Properties.Current["DepartureBoards.EnquiryPorts.Service.MilliSecondWaitTime"].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture.NumberFormat);

            // getting timeout for network stream 
            iNetworkStreamTimeout = int.Parse(Properties.Current["DepartureBoards.EnquiryPorts.Service.NetworkStreamTimeout"].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture.NumberFormat);
        }

        #endregion
        
        #region Public Members

        /// <summary>
        /// Property to return socket response 
        /// </summary>
        public string SocketResponse
        {
            get { return (string)socketResp; }
        }

        /// <summary>
        /// This method is responsible for sending the xml request to RTTI server. 
        /// It will then get the response and set it to public property SocketResponse.
        /// </summary>
        /// <param name="xmlRequest"></param>
        /// <returns>Returns boolean to indicate whether sending/getting request/response was sucessfull or not </returns>
        public bool SendRequest(string xmlRequest)
        {
            byte[] serverbuff = null;
            StringBuilder responseXmlSB = new StringBuilder();
            string responseXml = string.Empty;
            string errorMsg = string.Empty;
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

            //date variables to hold the start and end times of the event
            DateTime startDateTime;
            DateTime endDateTime;

            int numberOfRetries = 0;

            // starting listener
            // initialising response string 
            socketResp = string.Empty;

            try
            {
                // checking if Request string is blank
                if (xmlRequest == null || xmlRequest.Length == 0)
                {
                    // throw exception RTTINoXmlRequestFound
                    errorMsg = "SocketClient: No Xml request Found.";
                    throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTINoXmlRequestFound);
                }

                for (int i = 0; i < iRetryCount; i++)
                {
                    numberOfRetries = i;
                    // comminicating server on given port 
                    try
                    {
                        myclient = new TcpClient(serverName, portNumber);
                        break;
                    }
                    catch (SocketException sEx)
                    {	// checking for connection refused error
                        // if refused retry it 
                        if (sEx.NativeErrorCode == 10061)
                        {
                            System.Threading.Thread.Sleep(iMillisecondsTimeout);
                            if (i != iRetryCount - 1)
                            {
                                continue;
                            }
                            else
                            {
                                errorMsg = sEx.Message;
                                throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTISocketCommunicationError);
                            }
                        }
                        else
                        {
                            // throw exception RTTINoXmlRequestFound
                            StringBuilder socketMessage = new StringBuilder();
                            socketMessage.Append("SocketClient: Socket exception occurred: ");
                            socketMessage.Append(sEx.Message.ToString(CultureInfo.InvariantCulture));
                            socketMessage.Append(" Please check server is running.");

                            errorMsg = socketMessage.ToString();
                            throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTISocketCommunicationError);
                        }
                    }
                }

                // checking if we have got tcpclient object
                if (myclient == null)
                {
                    errorMsg = "Unable create an instance for tcpclient";
                    throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTISocketCommunicationError);
                }

                // get the start time for logging
                startDateTime = DateTime.Now;

                // Get the network stream 
                networkStream = myclient.GetStream();

                //Set read and write timeouts to prevent blocking indefinately in event of
                //network issues - USD11256657
                networkStream.ReadTimeout = iNetworkStreamTimeout;
                networkStream.WriteTimeout = iNetworkStreamTimeout;

                // get networkStream into stream reader for sending request 
                streamWriter = new StreamWriter(networkStream);

                // if connection has establish then try sending the request 
                //get a Network stream from the server
                streamWriter.WriteLine(xmlRequest);
                streamWriter.Flush();

                // setting the encoding type 
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                // instantiating the buffer with specified number of bytes
                serverbuff = new Byte[maxCount];

                int numberOfBytesRead = 0;

                try
                {
                    // now read the response
                    if (networkStream.CanRead)
                    {
                        // read till end of stream or exception occurs
                        while (networkStream.CanRead && (numberOfBytesRead = networkStream.Read(serverbuff, 0, serverbuff.Length)) > 0)
                        {
                            responseXmlSB.Append(enc.GetString(serverbuff, 0, numberOfBytesRead));
                        }

                        responseXml = responseXmlSB.ToString();
                    }
                }
                catch (IOException ioEx)
                {
                    errorMsg = ioEx.Message.ToString();
                    responseXml = string.Empty;
                }
                finally
                {
                    //get the end time
                    endDateTime = DateTime.Now;
                    bool loginSuccessful;

                    StringBuilder message = new StringBuilder();

                    if (responseXml.Length == 0)
                    {
                        message.Append("Request failed");
                        loginSuccessful = false;
                    }
                    else
                    {
                        message.Append("Request successful");
                        loginSuccessful = true;
                    }

                    message.Append("\r");
                    message.Append("Time started: ");
                    message.Append(startDateTime.ToString(dateTimeFormat, CultureInfo.CurrentCulture.DateTimeFormat));
                    message.Append("\r");
                    message.Append("Time finished: ");
                    message.Append(endDateTime.ToString(dateTimeFormat, CultureInfo.CurrentCulture.DateTimeFormat));
                    message.Append("\r");
                    message.Append("Number of retries: ");
                    message.Append(numberOfRetries.ToString(CultureInfo.CurrentCulture.NumberFormat));

                    Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose, message.ToString()));

                    // Here we got a response, either successful or unsuccessful					
                    // So, register end time here
                    // Log the following:
                    // Time started, Time end, retries and successfull or not
                    RTTIInternalEvent rEvent = new RTTIInternalEvent(startDateTime, endDateTime, numberOfRetries, loginSuccessful);
                    Logger.Write(rEvent);
                }

                // checking responseXmlString
                if (responseXml == null || responseXml.Length == 0)
                {
                    // throw exception RTTINoXmlRequestFound
                    errorMsg += " SocketClient: No Xml response Found.";
                    throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTIUnabletoReadSocketData);
                }
                // assigning socket response
                socketResp = responseXml;
            }
            catch (SocketException sEx)
            {
                errorMsg = "SocketClient: Socket exception occurred " + sEx.Message;
                throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTISocketCommunicationError);
            }
            catch (TDException tdex)
            {
                errorMsg = "SocketClient: Communication error occurred " + tdex.Message;
                throw new TDException(errorMsg, false, TDExceptionIdentifier.RTTISocketCommunicationError);
            }
            finally
            {
                CloseStreams();
                serverbuff = null;
            }

            return true;

        }

        #endregion
        
        #region Private Members

        /// <summary>
        /// Close all stream objects
        /// </summary>
        private void CloseStreams()
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
            }

            if (networkStream != null)
            {
                networkStream.Close();
            }

            if (myclient != null)
            {
                myclient.Close();
            }
        }

        #endregion
    }
}