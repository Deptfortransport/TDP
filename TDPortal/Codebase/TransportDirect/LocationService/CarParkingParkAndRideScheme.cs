// *********************************************** 
// NAME			: CarParkingParkAndRideScheme.cs
// AUTHOR		: Esther Severn
// DATE CREATED	: 25/08/2006 
// DESCRIPTION	: Park and Ride scheme class for
//				  car parking.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkingParkAndRideScheme.cs-arc  $
//
//   Rev 1.2   Mar 10 2008 15:18:46   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.0   Aug 29 2006 10:01:38   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CarParkingParkAndRideScheme.
	/// </summary>
	[Serializable()]
	public class CarParkingParkAndRideScheme
	{

		private readonly string location = string.Empty;
		private readonly string schemeURL = string.Empty;
		private readonly string comments = string.Empty;
		private readonly int locationEasting;
		private readonly int locationNorthing;
		private readonly string transferFrequency = string.Empty;
		private readonly string transferFrom = string.Empty;
		private readonly string transferTo = string.Empty;

		/// <summary>
		/// Default constructor
		/// </summary>
		public CarParkingParkAndRideScheme()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="schemeURL"></param>
		/// <param name="comments"></param>
		/// <param name="locationEasting"></param>
		/// <param name="locationNorthing"></param>
		/// <param name="transferFrequency"></param>
		/// <param name="transferFrom"></param>
		/// <param name="transferTo"></param>
		public CarParkingParkAndRideScheme(string location, string schemeURL, string comments,
			int locationEasting, int locationNorthing, string transferFrequency, 
			string transferFrom, string transferTo)
		{
			this.location = location;
			this.schemeURL = schemeURL;
			this.comments = comments;
			this.locationEasting = locationEasting;
			this.locationNorthing = locationNorthing;
			this.transferFrequency = transferFrequency; 
			this.transferFrom = transferFrom;
			this.transferTo = transferTo;
		}


		#region Public properties

		/// <summary>
		/// Read only property, returning the park and ride scheme location.
		/// </summary>
		public string SchemeLocation
		{
			get { return location; }
		}
		
		/// <summary>
		/// Read only property, returning the park and ride scheme url.
		/// </summary>
		public string SchemeURL
		{
			get { return schemeURL; }
		}

		/// <summary>
		/// Read only property, returning comment details.
		/// </summary>
		public string SchemeComments
		{
			get { return comments; }
		}

		/// <summary>
		/// Read only property, returning the transfer frequency details.
		/// </summary>
		public string TransferFrequency
		{
			get { return transferFrequency; }
		}

		/// <summary>
		/// Read only property, returning the transfer to details.
		/// </summary>
		public string TransferFromDetails
		{
			get { return transferFrom; }
		}

		/// <summary>
		/// Read only property, returning the transfer from details.
		/// </summary>
		public string TransferToDetails
		{
			get { return transferTo; }
		}

		/// <summary>
		/// Read only property, returning the scheme easting.
		/// </summary>
		public int Easting
		{
			get { return locationEasting; }
		}

		/// <summary>
		/// Read only property, returning the scheme northing.
		/// </summary>
		public int Northing
		{
			get { return locationNorthing; }
		}

		#endregion
	}
}
