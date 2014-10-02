// *********************************************** 
// NAME			: PartnerFeatures.cs
// AUTHOR		: Manuel Dambrine
// DATE CREATED	: 27/09/2005
// DESCRIPTION	: Represents an PartnerFeatures
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/PartnerFeatures.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:44   mturner
//Initial revision.
//
//   Rev 1.4   Oct 11 2005 18:53:20   COwczarek
//Fix IsLiveTravelEnabled property
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2808: Del8 White Labelling - Page Header & Footer
//
//   Rev 1.3   Oct 07 2005 11:23:10   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.2   Oct 04 2005 15:48:52   mdambrine
//Was not calling the partner id when looking up the properties
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.1   Oct 04 2005 15:45:52   COwczarek
//Add PrintableHeaderUrl property
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2808: Del8 White Labelling - Page Header & Footer
//
//   Rev 1.0   Sep 30 2005 15:37:44   mdambrine
//Initial revision.


using System;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Represents an PartnerFeatures class
	/// </summary>
	[Serializable]
	[System.Runtime.InteropServices.ComVisible(false)]
	public class PartnerFeatures
	{
		private int partnerId;	
		[NonSerializedAttribute] 
		System.IFormatProvider formatProvider;
		
		#region Constructors
		/// <summary>
		/// Constructor for the partnerfeatures class
		/// </summary>
		/// <param name="partnerId"></param>
        public PartnerFeatures(int partnerId)
        {
            this.partnerId = partnerId;

			formatProvider = new System.Globalization.CultureInfo("en-GB", false);

        }
		
		#endregion	

		#region Properties		
	
		public string HeaderUrl 
		{
			get 
			{ 
				return Properties.Current["PartnerConfiguration.HeaderUrl", partnerId]; 
			}
			
		}

        public string PrintableHeaderUrl 
        {
            get 
            { 
                return Properties.Current["PartnerConfiguration.PrintableHeaderUrl", partnerId]; 
            }
			
        }

		public string FooterUrl 
		{
			get 
			{ 
				return Properties.Current["PartnerConfiguration.FooterUrl", partnerId];
			}
			
		}

		public bool IsLocationMapsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsLocationMapsEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsNetworkMapsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsNetworkMapsEnabled", partnerId], formatProvider);
			}
		
		}

		public bool IsTrafficMapsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTrafficMapsEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsHomeEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsHomeEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsFindATrainEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindATrainEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsFindAFlightEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindAFlightEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsFindACoachEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindACoachEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsFindACarEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindACarEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsCityToCityEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsCityToCityEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsStationAirportEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsStationAirportEnabled", partnerId], formatProvider); 
			}
			
		}

		public bool IsDoorToDoorEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsDoorToDoorEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsMobileEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsMobileEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsTravelNewsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTravelNewsEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsDepartureBoardsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsDepartureBoardsEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsPublicTransportEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsPublicTransportEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsPrivateTransportEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsPrivateTransportEnabled", partnerId], formatProvider);
			}
			
		}
		
		public bool IsQuickPlannersEnabled 
		{
			get 
			{ 
				 return (IsFindATrainEnabled ||
						 IsFindAFlightEnabled ||
		 				 IsFindACoachEnabled ||
						 IsFindACarEnabled ||
						 IsCityToCityEnabled ||
						 IsStationAirportEnabled);				
			}
			
		}

		public bool IsMapsEnabled 
		{
			get 
			{ 
				return (IsLocationMapsEnabled ||
						IsNetworkMapsEnabled ||
						IsTrafficMapsEnabled);
				
			}
			
		}

		public bool IsLiveTravelEnabled 
		{
			get 
			{ 
				return (IsTravelNewsEnabled ||
						IsDepartureBoardsEnabled);
				
			}
			
		}

		public bool IsExtendEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsExtendEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsMapSymbolsEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsMapSymbolsEnabled", partnerId], formatProvider);
			}
			
		}

		public bool IsTicketCostEnabled 
		{
			get 
			{ 
				return Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTicketCostEnabled", partnerId], formatProvider);
			}
			
		}
		
		#endregion

	}
}
