// *********************************************** 
// NAME                 : PublicJourneyDetail.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourneyDetail Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourneyDetail.cs-arc  
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class PublicJourneyDetail
	{

		private ModeType mode;
		private string modeText;
		private PublicJourneyCallingPoint legStart;
		private PublicJourneyCallingPoint legEnd;
		private PublicJourneyCallingPoint origin;
		private PublicJourneyCallingPoint destination;
		private PublicJourneyCallingPoint[] intermediatesBefore;
		private PublicJourneyCallingPoint[] intermediatesLeg;
		private PublicJourneyCallingPoint[] intermediatesAfter;
		private ServiceDetails[] services;
		private int[] vehicleFeatures;
		private string[] vehicleFeaturesText;
		private string[] displayNotes;
		private string instructionText;
		private LegType legType;
		private int minFrequency;
		private int maxFrequency;
		private int maxDuration;
		private int duration;
		private string durationText;
		private string frequencyText;
		private string maxDurationText;

		/// <summary>
		/// constructor
		/// </summary>
		public PublicJourneyDetail()
		{
			
		}

		/// <summary>
		/// The type of leg, e.g. Frequency based
		/// </summary>
		public LegType Type
		{
			get
			{
				return legType;
			}
			set
			{
				legType = value;
			}
		}		

		/// <summary>
		/// The mode of the leg, e.g. Rail
		/// </summary>
		public ModeType Mode
		{
			get
			{
				return mode;
			}
			set
			{
				mode = value;
			}
		}

		/// <summary>
		/// Same as Mode but represented as language sensitive text
		/// </summary>
		public string ModeText
		{
			get
			{
				return modeText;
			}
			set
			{
				modeText = value;
			}
		}

		/// <summary>
		/// The duration of the leg in minutes. This is the average (typical) duration if Type is Frequency.
		/// </summary>
		public int Duration
		{
			get
			{
				return duration;
			}
			set
			{
				duration = value;
			}
		}

		/// <summary>
		/// Language sensitive text that provides the duration of the leg in minutes. 
		/// The format is “Duration: <x> minutes” or “Typical Duration: <x> minutes” if Type is Frequency.
		/// </summary>
		public string DurationText
		{
			get
			{
				return durationText;
			}
			set
			{
				durationText = value;
			}
		}

		/// <summary>
		/// Language sensitive text that provides journey instructions for the leg. See below for examples.
		/// </summary>
		public string InstructionText
		{
			get
			{
				return instructionText;
			}
			set
			{
				instructionText = value;
			}
		}

		/// <summary>
		/// Defines where the vehicle is boarded.
		/// </summary>
		public PublicJourneyCallingPoint LegStart
		{
			get
			{
				return legStart;
			}
			set
			{
				legStart = value;
			}
		}

		/// <summary>
		/// Defines where the vehicle is alighted.
		/// </summary>
		public PublicJourneyCallingPoint LegEnd
		{
			get
			{
				return legEnd;
			}
			set
			{
				legEnd = value;
			}
		}

		/// <summary>
		/// Defines the origin of the service
		/// </summary>
		public PublicJourneyCallingPoint Origin
		{
			get
			{
				return origin;
			}
			set
			{
				origin = value;
			}
		}

		/// <summary>
		/// Defines the destination of the service
		/// </summary>
		public PublicJourneyCallingPoint Destination
		{
			get
			{
				return destination;
			}
			set
			{
				destination = value;
			}
		}

		/// <summary>
		/// Station stops preceding boarding point for leg. Defined only if ModeType is Rail.
		/// </summary>
		public PublicJourneyCallingPoint[] IntermediatesBefore
		{
			get
			{
				return intermediatesBefore;
			}
			set
			{
				intermediatesBefore = value;
			}
		}

		/// <summary>
		/// Station stops for leg. Defined only if ModeType is Rail.
		/// </summary>
		public PublicJourneyCallingPoint[] IntermediatesLeg
		{
			get
			{
				return intermediatesLeg;
			}
			set
			{
				intermediatesLeg = value;
			}
		}

		/// <summary>
		/// Station stops succeeding alight point for leg. Defined only if ModeType is Rail.
		/// </summary>
		public PublicJourneyCallingPoint[] IntermediatesAfter
		{
			get
			{
				return intermediatesAfter;
			}
			set
			{
				intermediatesAfter = value;
			}
		}

		/// <summary>
		/// Defines the service details. Not present if Type is Continuous.
		/// </summary>
		public ServiceDetails[] Services
		{
			get
			{
				return services;
			}
			set
			{
				services = value;
			}
		}

		/// <summary>
		/// A list of on board facilities for a service. Defined only if ModeType is Rail. See table below.
		/// </summary>
		public int[] VehicleFeatures
		{
			get
			{
				return vehicleFeatures;
			}
			set
			{
				vehicleFeatures = value;
			}
		}

		/// <summary>
		/// Same as VehicleFeatures but represented as language sensitive text
		/// </summary>
		public string[] VehicleFeaturesText
		{
			get
			{
				return vehicleFeaturesText;
			}
			set
			{
				vehicleFeaturesText = value;
			}
		}

		/// <summary>
		/// These notes contain important user information such as contact and booking details
		/// Present only if ModeType is Drt The language used may vary depending on which 
		/// Traveline region they are obtained from.
		/// </summary>
		public string[] DisplayNotes
		{
			get
			{
				return displayNotes;
			}
			set
			{
				displayNotes = value;
			}
		}
		
		/// <summary>
		/// Minimum frequency in minutes. Defined only if Type is Frequency,
		/// </summary>
		public int MinFrequency
		{
			get
			{
				return minFrequency;
			}
			set
			{
				minFrequency = value;
			}
		}

		/// <summary>
		/// Maximum frequency in minutes. Defined only if Type is Frequency,
		/// </summary>
		public int MaxFrequency
		{
			get
			{
				return maxFrequency;
			}
			set
			{
				maxFrequency = value;
			}
		}		
	
		/// <summary>
		/// Language sensitive text that provides the frequency range in minutes. 
		/// The format is “Frequency: Every <min frequency> to <max frequency> minutes”. 
		/// Defined only if Type is Frequency. If min and max are equal, the format is “Frequency: 
		/// Every <n> minutes”
		/// </summary>
		public string FrequencyText
		{
			get
			{
				return frequencyText;
			}
			set
			{
				frequencyText = value;
			}
		}	

		/// <summary>
		/// Maximum leg duration in minutes. Defined only if Type is Frequency,
		/// </summary>
		public int MaxDuration
		{
			get
			{
				return maxDuration;
			}
			set
			{
				maxDuration = value;
			}
		}

		/// <summary>
		/// Language sensitive text that provides the maximum duration in minutes. 
		/// The format is: “Maximum duration: <x> minutes”. Defined only if Type is Frequency,
		/// </summary>
		public string MaxDurationText
		{
			get
			{
				return maxDurationText;
			}
			set
			{
				maxDurationText = value;
			}
		}
	}// END CLASS DEFINITION PublicJourneyDetail

} // TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
