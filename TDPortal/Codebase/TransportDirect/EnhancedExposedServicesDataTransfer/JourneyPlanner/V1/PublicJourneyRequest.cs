// *********************************************** 
// NAME                 : PublicJourneyRequest.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourneyRequest Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourneyRequest.cs-arc  
//

using System;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class PublicJourneyRequest 
	{
		private bool isReturnRequired;
		private bool outwardArriveBefore;
		private bool returnArriveBefore;
		private bool returnArriveBeforeSpecified;
		private DateTime outwardDateTime;
		private DateTime returnDateTime;
		private bool returnDateTimeSpecified;
		private int interchangeSpeed;
		private int walkingSpeed;
		private int maxWalkingTime;
		private RequestLocation originLocation;
		private RequestLocation destinationLocation;
		private int sequence;		

		/// <summary>
		/// constructor
		/// </summary>
		public PublicJourneyRequest()
		{
			
		}
		
		/// <summary>
		/// Specifies whether a return journey should be planned. True if required, false otherwise.
		/// </summary>
		public bool IsReturnRequired
		{
			get
			{
				return isReturnRequired;

			}
			set
			{
				isReturnRequired = value;
			}
		}
    
		/// <summary>
		/// Specifies whether the outward journey should arrive on or leave on the specified time. 
		/// If true, journeys will be planned to arrive on or before the specified time. If false, 
		/// journeys will be planned to leave on or after the specified time.
		/// </summary>
		public bool OutwardArriveBefore
		{
			get
			{
				return outwardArriveBefore;

			}
			set
			{
				outwardArriveBefore = value;
			}
		}
    
		/// <summary>
		/// Same as for OutwardArriveBefore except this applies to the return journey plan. 
		/// Ignored if IsReturnRequired is false
		/// </summary>
		public bool ReturnArriveBefore
		{
			get
			{
				return returnArriveBefore;

			}
			set
			{
				returnArriveBefore = value;
			}
		}
    
		/// <summary>
		/// autogenerate by the framework
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ReturnArriveBeforeSpecified
		{
			get
			{
				return returnArriveBeforeSpecified;

			}
			set
			{
				returnArriveBeforeSpecified = value;
			}
		}
    
		/// <summary>
		/// The time the outward journey should leave at or arrive by.
		/// </summary>
		public System.DateTime OutwardDateTime
		{
			get
			{
				return outwardDateTime;

			}
			set
			{
				outwardDateTime = value;
			}
		}
    
		/// <summary>
		/// The time the return journey should leave at or arrive by. Ignored if IsReturnRequired is false.
		/// </summary>
		public System.DateTime ReturnDateTime
		{
			get
			{
				return returnDateTime;

			}
			set
			{
				returnDateTime = value;
			}
		}
    
		/// <summary>
		/// autogenerated by the framework
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ReturnDateTimeSpecified
		{
			get
			{
				return returnDateTimeSpecified;

			}
			set
			{
				returnDateTimeSpecified = value;
			}
		}

		/// <summary>
		/// Specifies how quickly changes can be made between vehicles. Range is �3 to +3.
		/// </summary>
		public int InterchangeSpeed
		{
			get
			{
				return interchangeSpeed;

			}
			set
			{
				interchangeSpeed = value;
			}
		}
    
		/// <summary>
		/// Specifies walking speed in metres per minute (e.g. slow = 40, average = 80, fast = 120)
		/// </summary>
		public int WalkingSpeed
		{
			get
			{
				return walkingSpeed;

			}
			set
			{
				walkingSpeed = value;
			}
		}
    
		/// <summary>
		/// Specifies maximum walking time in minutes. Range is 5 to 30 minutes.
		/// </summary>
		public int MaxWalkingTime
		{
			get
			{
				return maxWalkingTime;

			}
			set
			{
				maxWalkingTime = value;
			}
		}
    
		/// <summary>
		/// Specifies the origin of the outward journey.
		/// </summary>
		public RequestLocation OriginLocation
		{
			get
			{
				return originLocation;

			}
			set
			{
				originLocation = value;
			}
		}
    
		/// <summary>
		/// Specifies the destination of the outward journey.
		/// </summary>
		public RequestLocation DestinationLocation
		{
			get
			{
				return destinationLocation;

			}
			set
			{
				destinationLocation = value;
			}
		}
    
		/// <summary>
		/// Specifies the maximum number of journeys to return. The value must be in the 
		/// range 1..5.The number of journeys returned might be less than that requested.
		/// </summary>
		public int Sequence
		{
			get
			{
				return sequence;

			}
			set
			{
				sequence = value;
			}
		}
	}

}