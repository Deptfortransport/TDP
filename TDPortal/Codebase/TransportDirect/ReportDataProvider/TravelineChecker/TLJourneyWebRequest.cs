// *********************************************** 
// NAME                 : JourneyWebRequest.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Class gathering information to send a request to a traveline
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/TLJourneyWebRequest.cs-arc  $ 
//
//   Rev 1.5   Dec 14 2011 11:48:42   MTurner
//Emergency fix for Christmas handling bug
//Resolution for 5775: Emergency fix for Traveline Checker Christmas Bug
//
//   Rev 1.4   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.3.1.0   Feb 23 2009 12:41:36   mturner
//Updated for tech refresh - Added support for Geocode journeys as previously only NaPTANs cld be used.
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.3   Apr 21 2008 13:51:34   jfrank
//Added check for string.empty to account for the setting being added to the config file but no value being set.
//Resolution for 4860: TI Check Transaction - Make the request ID configurable
//
//   Rev 1.2   Apr 17 2008 09:56:24   jfrank
//Code change to make the request id used by the traveline checker transaction configurable.
//Resolution for 4860: TI Check Transaction - Make the request ID configurable
//
//   Rev 1.1   Dec 12 2007 15:35:56   mmodi
//Amended get date method for christmas day date problem
//
//   Rev 1.0   Nov 08 2007 12:40:46   mturner
//Initial revision.
//
//   Rev 1.2   Nov 29 2004 13:51:12   passuied
//fixed webRequest creation when more than one naptanID
//
//   Rev 1.1   Nov 18 2004 13:17:44   passuied
//minor change
//
//   Rev 1.0   Nov 15 2004 17:32:46   passuied
//Initial revision.


using System;
using System.Text;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.TravelineChecker
{
	/// <summary>
	/// Class gathering information to send a request to a traveline
	/// </summary>
	[Serializable]
	public class TLJourneyWebRequest
	{
		private const string version = "2.1";
		private const string arrDep = "depart";
		private const string range = "<Range><Sequence>8</Sequence></Range>";
		private const string algorithm = "default";
		private const string walkspeed = "normal";
		private const int maxWalkDistance = 2400;
		private const string interchangeSpeed = "normal";
		private const string includeIntermediateStops = "none";
		private const string modes = "<Modes Exclude=\"false\"><Mode Mode=\"rail\"></Mode><Mode Mode=\"bus\"></Mode><Mode Mode=\"coach\"></Mode><Mode Mode=\"metro\"></Mode><Mode Mode=\"underground\"></Mode><Mode Mode=\"tram\"></Mode><Mode Mode=\"ferry\"></Mode><Mode Mode=\"air\"></Mode></Modes>";

        private string requestID = "1111-1111-1111-1212-0000-1";

		private Place plOrigin;
		private Place plDestination;

		public TLJourneyWebRequest()
		{
            if (Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"] != null &&
                Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"] != string.Empty)
            {
                this.requestID = Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"];
            }

			plOrigin = null;
			plDestination = null;
		}

		public TLJourneyWebRequest(TDLocation origin, TDLocation destination)
		{
            if (Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"] != null &&
                Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"] != string.Empty)
            {
                this.requestID = Properties.Current["TransactionInjector.TravelineChecker.JourneyRequestID"];
            }
            OriginLocation = origin;
			DestinationLocation = destination;
		}

		public TDLocation OriginLocation
		{
			set
			{
				string[] naptanIds = new string[value.NaPTANs.Length];
				for (int i= 0; i< value.NaPTANs.Length; i++)
				{
					naptanIds[i] = value.NaPTANs[i].Naptan;
				}

                plOrigin = new Place(value.Description, naptanIds, value.GridReference.Easting.ToString(), value.GridReference.Northing.ToString(), GetRequestDateTime());
			}
		}

		public TDLocation DestinationLocation
		{
			set
			{
				string[] naptanIds = new string[value.NaPTANs.Length];
				for (int i= 0; i< value.NaPTANs.Length; i++)
				{
					naptanIds[i] = value.NaPTANs[i].Naptan;
				}

				plDestination = new Place(value.Description, naptanIds, value.GridReference.Easting.ToString(), value.GridReference.Northing.ToString(), DateTime.Now.AddYears(-1));
			}
		}


		/// <summary>
		/// Determins the Journey Request date time, but setting within 9 days from now
		/// and making sure the planned journey is between tuesday to Thursday
		/// </summary>
		/// <returns></returns>
		private DateTime GetRequestDateTime()
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddHours( 9 -  dt.Hour );
			dt = dt.AddMinutes( - dt.Minute );
			dt = dt.AddMilliseconds( - dt.Millisecond);

			// Roll forward 9 days
			dt = dt.AddDays(9.0);

            // Put date validation in loop to ensure when we add days it 
            // doesnt land on a weekend day or Public holiday again.
            bool validDate = false;
            while (!validDate)
            {
                validDate = true;

                // Not allowed to Transaction Inject on a Fri, Sat, Sun, Mon
                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        dt = dt.AddDays(4);
                        break;
                    case DayOfWeek.Monday:
                        dt = dt.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        dt = dt.AddDays(3);
                        break;
                    case DayOfWeek.Sunday:
                        dt = dt.AddDays(2);
                        break;
                }

                // Not allowed to Transaction Inject on Christmas Day, Boxing Day, New Years Day or 
                // their substitute bank holidays
                if (dt.Day == 25 && dt.Month == 12)
                {
                    dt = dt.AddDays(2);
                    validDate = false;
                }
                if (dt.Day == 26 && dt.Month == 12)
                {
                    dt = dt.AddDays(1);
                    validDate = false;
                }
                if (dt.Day == 1 && dt.Month == 1)
                {
                    dt = dt.AddDays(1);
                    validDate = false;
                }
                if ((dt.Day == 27 || dt.Day == 28) && dt.Month == 12 && dt.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dt = dt.AddDays(1);
                    validDate = false;
                }

            }

			return dt;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<JourneyWeb Version=\""+version+"\"><Request>");
			sb.Append("<JourneysRequest RequestID=\""+requestID+"\">");
			sb.Append("<Origin>"+plOrigin.ToString()+"</Origin>");
			sb.Append("<Destination>"+plDestination.ToString()+"</Destination>");
			sb.Append("<ArrDep>"+arrDep+"</ArrDep>");
			sb.Append(range);
			sb.Append("<Algorithm>"+algorithm+"</Algorithm>");
			sb.Append("<WalkSpeed>"+walkspeed+"</WalkSpeed>");
			sb.Append("<MaxWalkDistance>"+maxWalkDistance+"</MaxWalkDistance>");
			sb.Append("<InterchangeSpeed>"+interchangeSpeed+"</InterchangeSpeed>");
			sb.Append("<IncludeIntermediateStops>"+includeIntermediateStops+"</IncludeIntermediateStops>");
			sb.Append(modes);
			sb.Append("</JourneysRequest></Request></JourneyWeb>");
			return sb.ToString();
		}

		public byte[] GetBytes()
		{
			return ASCIIEncoding.Default.GetBytes(this.ToString());
		}


	}


	[Serializable]
	public class Place
	{
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffff";

		string   givenName;
		string[] naptanID;
        string   easting;
        string   northing;

        DateTime journeyTime;

		public Place(string name, string[] naptanID, string easting, string northing, DateTime journeyTime)
		{
			this.givenName = name;
			this.naptanID = naptanID;
			this.journeyTime = journeyTime;
            this.easting = easting;
            this.northing = northing;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

            if (naptanID.Length == 0)
            {
                sb.Append("<Place><ID>");
                sb.Append("<Geocode><Easting>" + easting + "</Easting><Northing>" + northing + "</Northing></Geocode>");
                sb.Append("</ID>");
                if (journeyTime > DateTime.Now)
                {
                    sb.Append("<JourneyTime>" + journeyTime.ToString(dateTimeFormat) + "</JourneyTime>");
                }
                sb.Append("</Place>");
                sb.Append("<GivenName>" + givenName + "</GivenName>");
            }
            else
            {
                foreach (string naptan in naptanID)
                {
                    sb.Append("<Place><ID>");
                    sb.Append("<NaPTANID>" + naptan + "</NaPTANID>");
                    sb.Append("</ID>");
                    if (journeyTime > DateTime.Now)
                    {
                        sb.Append("<JourneyTime>" + journeyTime.ToString(dateTimeFormat) + "</JourneyTime>");
                    }
                    sb.Append("</Place>");
                }
                sb.Append("<GivenName>" + givenName + "</GivenName>");
            }
			return sb.ToString();
		}
	}
}
