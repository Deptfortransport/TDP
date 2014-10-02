//********************************************************************************
//NAME         : FareRequest.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FareRequest class. Used for storing request information.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FareRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:28   mturner
//Initial revision.
//
//   Rev 1.8   Jun 13 2007 15:13:02   mmodi
//Added RequestID property
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.7   Jan 18 2006 18:16:28   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.6   Nov 11 2005 16:26:58   mguney
//Null checks included.
//
//   Rev 1.5   Nov 10 2005 13:13:56   mguney
//Message method implemented to be used for logging.
//
//   Rev 1.4   Nov 03 2005 13:59:18   RPhilpott
//Add new ctor.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 18 2005 16:48:56   mguney
//Changes made for Atkins interface.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:53:22   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:32   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:50   mguney
//Initial revision.

using System;
using System.Globalization;
using System.Text;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Class for storing request information.
	/// </summary>
	public class FareRequest
	{

		static readonly string nl = Environment.NewLine;

		#region Private variables	
		private string operatorCode;
		private TDNaptan originNaPTAN;
		private TDNaptan destinationNaPTAN;
		private TDDateTime outwardStartDateTime;
		private TDDateTime outwardEndDateTime;
		private TDDateTime returnStartDateTime;
		private TDDateTime returnEndDateTime;
		private string requestID;

		private CJPSessionInfo cjpRequestInfo;		

		#endregion

		#region Constructor

		/// <summary>
		/// Default empty constructor
		/// </summary>
		public FareRequest()
		{
		}

		/// <summary>
		/// Constructor setting all properties
		/// </summary>
		public FareRequest(string operatorCode, TDNaptan originNaPTAN, TDNaptan destinationNaPTAN,
			TDDateTime outwardStartDateTime,
			TDDateTime outwardEndDateTime,
			CJPSessionInfo cjpRequestInfo)
		{
			this.operatorCode = operatorCode;
			this.originNaPTAN = originNaPTAN;
			this.destinationNaPTAN = destinationNaPTAN;
			this.outwardStartDateTime = outwardStartDateTime;
			this.outwardEndDateTime = outwardEndDateTime;
			this.cjpRequestInfo = cjpRequestInfo;
			this.requestID = string.Empty;
		}
														

		#endregion

		#region Public properties		

		/// <summary>
		/// Operator code. [rw]
		/// </summary>
		public string OperatorCode
		{
			get {return operatorCode;}
			set {operatorCode = value;}
		}

		/// <summary>
		/// Origin naptan. [rw]
		/// </summary>
		public TDNaptan OriginNaPTAN
		{
			get {return originNaPTAN;}
			set {originNaPTAN = value;}
		}

		/// <summary>
		/// Destination naptan. [rw]
		/// </summary>
		public TDNaptan DestinationNaPTAN
		{
			get {return destinationNaPTAN;}
			set {destinationNaPTAN = value;}
		}

		/// <summary>
		/// Start date time for outward journey. [rw]
		/// </summary>
		public TDDateTime OutwardStartDateTime
		{
			get {return outwardStartDateTime;}
			set {outwardStartDateTime = value;}
		}

		/// <summary>
		/// End date time for outward journey. [rw]
		/// </summary>
		public TDDateTime OutwardEndDateTime
		{
			get {return outwardEndDateTime;}
			set {outwardEndDateTime = value;}
		}

		/// <summary>
		/// Start date time for return journey. Optional. [rw]
		/// </summary>
		public TDDateTime ReturnStartDateTime
		{
			get {return returnStartDateTime;}
			set {returnStartDateTime = value;}
		}

		/// <summary>
		/// End date time for return journey. Optional. [rw]
		/// </summary>
		public TDDateTime ReturnEndDateTime
		{
			get {return returnEndDateTime;}
			set {returnEndDateTime = value;}
		}

		/// <summary>
		/// CJP session info. [rw]
		/// </summary>
		public CJPSessionInfo CjpRequestInfo
		{
			get {return cjpRequestInfo;}
			set {cjpRequestInfo = value;}
		}
		
		/// <summary>
		/// CJP session info. [rw]
		/// </summary>
		public string RequestID
		{
			get {return requestID;}
			set {requestID = value;}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// String representation of the request.
		/// </summary>
		/// <returns></returns>
		public string Message()
		{
			StringBuilder request = new StringBuilder();
			request.Append(nl + "Fare Request:" + nl); 
			request.Append("Request ID: " + requestID + nl); 
			request.Append("Operator Code: " + operatorCode);
			if (originNaPTAN != null)
				request.Append("\tOrigin Naptan: " + originNaPTAN.Naptan);
			if (destinationNaPTAN != null)
				request.Append("\tDestination Naptan: " + destinationNaPTAN.Naptan);
			if (outwardStartDateTime != null)
				request.Append("\tOutward Start DateTime: " + outwardStartDateTime.ToString("dd/MM/yyyy HH:mm"));
			if (outwardEndDateTime != null)
				request.Append("\tOutward End DateTime: " + outwardEndDateTime.ToString("dd/MM/yyyy HH:mm"));
			if (returnStartDateTime != null)
				request.Append("\tReturn Start DateTime: " + returnStartDateTime.ToString("dd/MM/yyyy HH:mm"));
			if (returnEndDateTime != null)
				request.Append("\tReturn End DateTime: " + returnEndDateTime.ToString("dd/MM/yyyy HH:mm"));

			if (cjpRequestInfo != null)
			{
				request.Append(nl + "CJP Session (Request) Info:" + nl);
				request.Append("Is Logged On: " + cjpRequestInfo.IsLoggedOn.ToString());
				request.Append("\tUser Type: " + cjpRequestInfo.UserType.ToString(CultureInfo.CurrentCulture));
				request.Append("\tSessionId: " + cjpRequestInfo.SessionId);
			}

			return request.ToString();
		}


		#endregion

	}
}