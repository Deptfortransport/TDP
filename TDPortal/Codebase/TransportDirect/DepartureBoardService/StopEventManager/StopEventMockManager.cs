// *********************************************** 
// NAME                 : StopEventMockManager.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 05/01/2005
// DESCRIPTION  : Mock class for the StopEventManager. For test purposes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/StopEventMockManager.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:26   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.1   Jul 20 2005 15:13:42   Schand
//Hardcoded file path has been moved to config file.
//
//   Rev 1.0   Feb 28 2005 16:23:58   passuied
//Initial revision.
//
//   Rev 1.12   Feb 24 2005 14:19:56   passuied
//Changes for FxCop
//
//   Rev 1.11   Feb 22 2005 11:46:44   passuied
//made the access to test data relative rather than absolute. Causes prob on SI test
//
//   Rev 1.10   Feb 02 2005 10:19:24   passuied
//removed type from results after design review
//
//   Rev 1.9   Jan 27 2005 13:24:10   SWillcock
//Closed XmlTextReader in ReadXml method
//
//   Rev 1.8   Jan 27 2005 11:01:10   passuied
//added ability to show callings stops in mock SE manager + updated test scripts
//
//   Rev 1.7   Jan 26 2005 13:48:12   passuied
//added extra functionality to mock SE manager to query for arrival/departures separately
//
//   Rev 1.6   Jan 26 2005 10:50:26   passuied
//enhanced the stopevent mock manager and updated test scripts
//
//   Rev 1.5   Jan 19 2005 14:33:22   SWillcock
//Added mock class
//
//   Rev 1.4   Jan 14 2005 10:21:36   passuied
//changes in interface
//
//   Rev 1.3   Jan 11 2005 16:33:36   passuied
//changes regarding calling stops
//
//   Rev 1.2   Jan 11 2005 13:42:22   passuied
//backed up version
//
//   Rev 1.1   Jan 07 2005 14:39:58   passuied
//changes in interface
//
//   Rev 1.0   Jan 05 2005 16:52:26   passuied
//Initial revision.

using System;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Web;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Mock class for the StopEventManager. For test purposes
	/// </summary>
	public class StopEventMockManager : IServiceFactory, IStopEventManager
	{

		private static string xmlFolder; 

		private DBSResult ReadXml( string from, string to, bool showDepartures, bool showCallingStops, int range )
		{
			XmlDocument xdLookup = new XmlDocument();
			xdLookup.Load(xmlFolder +"lookup.xml");
			char aOrd = showDepartures? 'd': 'a';
			string cs = showCallingStops? "c": string.Empty;
			string query = from.ToLower() + to.ToLower() + aOrd + cs;
			XmlNode xnQuery = xdLookup.DocumentElement.SelectSingleNode("//query[@fromto=\"" +query +"\"]");
			if (xnQuery== null)
			{
				// build empty dbsresult with error message
				DBSResult res = new DBSResult();
				DBSMessage msg = new DBSMessage();
				msg.Code = (int)DBSMessageIdentifier.CJPReturnedMessages;
				msg.Description = "No Real time info found";
				res.Messages = new DBSMessage[]{msg};

				return res;
			}
			else
			{

				XmlSerializer ser = new XmlSerializer(typeof(DBSStopEvent));
				XmlTextReader xr = new XmlTextReader(xmlFolder +xnQuery.InnerText);
				DBSStopEvent se = (DBSStopEvent)ser.Deserialize(xr);
				xr.Close();

				DBSResult res = new DBSResult();

				res.StopEvents = new DBSStopEvent[range];

				for (int i=0; i<range; i++)
				{
					res.StopEvents[i] = se;
				}

			
				return res;
			}
		}

		public StopEventMockManager()
		{	string replaceVal = @"file:\";
			string stopEventMockXmlPath  =string.Empty ;

			xmlFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			xmlFolder = xmlFolder.Replace(replaceVal, "");

            stopEventMockXmlPath = System.Configuration.ConfigurationManager.AppSettings["StopEventMockXmlFilesPath"];
   
			xmlFolder = System.IO.Path.Combine(xmlFolder , stopEventMockXmlPath);
			
			if (xmlFolder ==null || xmlFolder.Length == 0)
				throw new Exception("No Path found for StopEventMockManager"); 			
			 
		}

		/// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="token">Authentications token</param>
		/// <param name="originNaptan">naptanId of Origin</param>
		/// <param name="destinationNaptan">naptanId of Destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="ServiceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardTrip(
												DBSLocation originLocation,
												DBSLocation destinationLocation,
                                                string operatorCode,
                                                string serviceNumber,
												DBSTimeRequest time,
												DBSRangeType rangeType,
												int range,
												bool showDepartures,
												bool showCallingStops)
		{
			if (destinationLocation != null)
				return ReadXml(originLocation.Code, destinationLocation.Code, showDepartures, showCallingStops, range);
			else
				return ReadXml(originLocation.Code, string.Empty, showDepartures, showCallingStops, range);

		}

		/// <summary>
		/// Stop level request method for DepartureBoard Information
		/// </summary>
		/// <param name="token">Authentication token</param>
		/// <param name="stopNaptan">NaptanId of the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardStop(
												DBSLocation stopLocation,
                                                string operatorCode,
                                                string serviceNumber,
												bool showDepartures,
												bool showCallingStops)
		{
			int range = Convert.ToInt32(Properties.Current[Keys.DefaultRangeKey], CultureInfo.InvariantCulture.NumberFormat);
			return ReadXml(stopLocation.Code, string.Empty, showDepartures, showCallingStops, range);
			
		}


		/// <summary>
		/// Implementation of ServiceFactory. Return this object.
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return this;
		}
	}


	public class StopEventMockManagerManyJourneys : IServiceFactory, IStopEventManager
	{

		private DBSResult tripResult;
		private DBSResult stopResult;

		public StopEventMockManagerManyJourneys()
		{
			// building fake tripResult
			tripResult = new DBSResult();
			tripResult.StopEvents = new DBSStopEvent[20];

			for(int i = 0; i < 20; i++)
			{
				tripResult.StopEvents[i] = GetDummyStopEventForPagingTests(i);
			}

			// building fake stop Result
			stopResult = new DBSResult();

			stopResult.StopEvents = new DBSStopEvent[20];
			
			for(int i = 0; i < 20; i++)
			{
				stopResult.StopEvents[i] = GetDummyStopEventForPagingTests(i);
			}

		}

		private DBSStopEvent GetDummyStopEventForPagingTests(int idNumber)
		{
			DBSStopEvent stopEvent = new DBSStopEvent();

			stopEvent.CallingStopStatus = CallingStopStatus.HasCallingStops;

			stopEvent.Service = new DBSService();
			stopEvent.Service.ServiceNumber = "A6";
			stopEvent.Service.OperatorCode = "NX";
			stopEvent.Service.OperatorName = "National Express";

			stopEvent.Departure = new DBSEvent();
			stopEvent.Departure.Stop = new DBSStop();
			stopEvent.Departure.Stop.Name = "Victoria coach station : " + idNumber.ToString();
			stopEvent.Departure.Stop.NaptanId = "9000VCTR";
			
			stopEvent.Departure.ActivityType = DBSActivityType.Depart;
			stopEvent.Departure.DepartTime = DateTime.Now;
			stopEvent.Departure.RealTime = new DBSRealTime();
			stopEvent.Departure.RealTime.DepartTimeType = DBSRealTimeType.Estimated;
			stopEvent.Departure.RealTime.DepartTime = DateTime.Now.AddMinutes(5);

			stopEvent.Arrival = new DBSEvent();

			stopEvent.Arrival.Stop = new DBSStop();
			stopEvent.Arrival.Stop.Name = "Stansted Airport coach station : " + idNumber.ToString();
			stopEvent.Arrival.Stop.NaptanId = "9000STND";
			
			
			stopEvent.Arrival.ActivityType = DBSActivityType.Arrive;
			stopEvent.Arrival.ArriveTime = DateTime.Now.AddHours(2);
			stopEvent.Arrival.RealTime = new DBSRealTime();
			stopEvent.Arrival.RealTime.ArriveTimeType = DBSRealTimeType.Estimated;
			stopEvent.Arrival.RealTime.ArriveTime = stopEvent.Arrival.ArriveTime.AddMinutes(5);

			stopEvent.Stop = new DBSEvent();

			stopEvent.Stop.Stop = new DBSStop();
			stopEvent.Stop.Stop.Name = "Golders Green coach station";
			stopEvent.Stop.Stop.NaptanId = "9000GLDRSG";
			

			stopEvent.Stop.ActivityType = DBSActivityType.Depart;
			stopEvent.Stop.DepartTime = DateTime.Now.AddHours(1);
			stopEvent.Stop.RealTime = new DBSRealTime();
			stopEvent.Stop.RealTime.DepartTimeType = DBSRealTimeType.Estimated;
			stopEvent.Stop.RealTime.DepartTime = stopEvent.Stop.DepartTime.AddMinutes(5);

			return stopEvent;
		}

		/// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="token">Authentications token</param>
		/// <param name="originNaptan">naptanId of Origin</param>
		/// <param name="destinationNaptan">naptanId of Destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="ServiceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardTrip(
			DBSLocation originLocation,
			DBSLocation destinationLocation,
            string operatorCode,
            string serviceNumber,
			DBSTimeRequest time,
			DBSRangeType rangeType,
			int range,
			bool showDepartures,
			bool showCallingStops)
		{
			return tripResult;	
		}

		/// <summary>
		/// Stop level request method for DepartureBoard Information
		/// </summary>
		/// <param name="token">Authentication token</param>
		/// <param name="stopNaptan">NaptanId of the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardStop(
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops)
		{
			return stopResult;
		}


		/// <summary>
		/// Implementation of ServiceFactory. Return this object.
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return this;
		}
	}


}
