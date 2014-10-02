// *********************************************** 
// NAME			: ParkAndRideScheme.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 07/08/2006 
// DESCRIPTION	: Class which holds data about Park and Ride Scheme specific to a car park
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ParkAndRideScheme.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:18   mturner
//Initial revision.
//
//   Rev 1.0   Aug 08 2006 10:07:36   mmodi
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Displays and stores information about a Park and Ride Scheme
	/// </summary>
	[Serializable()]
	public class ParkAndRideScheme
	{
		private readonly string parkAndRideLocation;
		private readonly string parkAndRideURL;
		private readonly string parkAndRideComments;
		private readonly string parkAndRideLocationEasting;
		private readonly string parkAndRideLocationNorthing;
		private readonly string transferFrequency;
		private readonly string transferFrom;
		private readonly string transferTo;

		#region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
		public ParkAndRideScheme()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parkAndRideLocation">Park and ride location scheme name</param>
		/// <param name="parkAndRideURL">Park and ride scheme url</param>
		/// <param name="parkAndRideComments">Comments for Park and ride scheme</param>
		/// <param name="parkAndRideLocationEasting">Park and ride easting</param>
		/// <param name="parkAndRideLocationNorthing">Park and ride northing</param>
		/// <param name="transferFrequency">Transfer frequency</param>
		/// <param name="transferFrom">Transfer from</param>
		/// <param name="transferTo">Transfer to</param>
		public ParkAndRideScheme(string parkAndRideLocation, string parkAndRideURL, string parkAndRideComments,
				string parkAndRideLocationEasting, string parkAndRideLocationNorthing,
				string transferFrequency, string transferFrom, string transferTo)
		{
			this.parkAndRideLocation = parkAndRideLocation;
			this.parkAndRideURL = parkAndRideURL;
			this.parkAndRideComments = parkAndRideComments;
			this.parkAndRideLocationEasting = parkAndRideLocationEasting;
			this.parkAndRideLocationNorthing = parkAndRideLocationNorthing;
			this.transferFrequency = transferFrequency;
			this.transferFrom = transferFrom;
			this.transferTo = transferTo;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property. Get the park and ride location name
		/// </summary>
		public string ParkAndRideLocation
		{
			get { return parkAndRideLocation; }
		}

		/// <summary>
		/// Read only property. Get the park and ride URL link
		/// </summary>
		public string ParkAndRideURL
		{
			get { return parkAndRideURL; }
		}

		/// <summary>
		/// Read only property. Get any park and ride comments
		/// </summary>
		public string ParkAndRideComments
		{
			get { return parkAndRideComments; }
		}

		/// <summary>
		/// Read only property. Get the park and ride easting value
		/// </summary>
		public string ParkAndRideLocationEasting
		{
			get { return parkAndRideLocationEasting; }
		}

		/// <summary>
		/// Read only property. Get the park and ride northing value
		/// </summary>
		public string ParkAndRideLocationNorthing
		{
			get { return parkAndRideLocationNorthing; }
		}

		/// <summary>
		/// Read only property. Get the transfer frequency value
		/// </summary>
		public string TransferFrequency
		{
			get { return transferFrequency; }
		}

		/// <summary>
		/// Read only property. Get the transfer from value
		/// </summary>
		public string TransferFrom
		{
			get { return transferFrom; }
		}
		
		/// <summary>
		/// Read only property. Get the transfer to value
		/// </summary>
		public string TransferTo
		{
			get { return transferTo; }
		}
		
		#endregion

	}
}
