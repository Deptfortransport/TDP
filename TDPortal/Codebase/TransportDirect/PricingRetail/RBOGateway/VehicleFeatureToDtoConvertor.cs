//********************************************************************************
//NAME         : VehicleFeatureToDtoConverter.cs
//AUTHOR       : Rob Greenwood
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Class to obtain vehicle features from the CJP. Also builds strings
//				 representing the vehicle features for a specific leg.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/VehicleFeatureToDtoConvertor.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.0   Jul 08 2005 10:48:28   rgreenwood
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//


using System;
using System.Collections;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.RetailBusinessObjects;
using TransportDirect.UserPortal.PricingRetail.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using System.Xml.Serialization;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Summary description for VehicleFeatureToDtoConvertor.
	/// </summary>
	public class VehicleFeatureToDtoConvertor
	{
		private string reservations = " ";
		private string seatingClass = " ";
		private string sleeperClass = " ";
		private string catering = string.Empty;

		private ModeType mode;

		private const int iCompulsary = 10;
		private const int iRecommended = 11;
		private const int iAvailable = 12;
		private const int iSeatingClassB = 21;
		private const int iSeatingClassS = 20;
		private const int iSeatingClassF = 22;
		private const int iSleeperClassB = 30;
		private const int iSleeperClassS = 32;
		private const int iSleeperClassF = 31;
		private const int iCateringC = 0;
		private const int iCateringF = 1;
		private const int iCateringH = 2;
		private const int iCateringR = 3;
		private const int iCateringT = 4;

		private string sCompulsary = "A";
		private string sRecommended = "R";
		private string sAvailable = "S";
		private string sSeatingClassB = "B";
		private string sSeatingClassS = "S";
		private string sSeatingClassF = "F";
		private string sSleeperClassB = "B";
		private string sSleeperClassS = "S";
		private string sSleeperClassF = "F";
		private string sCateringC = "C";
		private string sCateringF = "F";
		private string sCateringH = "H";
		private string sCateringR = "R";
		private string sCateringT = "T";

		#region Constructor
		public VehicleFeatureToDtoConvertor(int[] features)
		{

			for (int i=0; i<features.Length; i++)
			{
				//Set reservation
				if (features[i] == (int)iCompulsary)
				{
					Reservations = sCompulsary;
				}
				else if (features[i] == (int)iRecommended)
				{
					Reservations = sRecommended;
				}
				else if (features[i] == iAvailable)
				{
					Reservations = sAvailable;
				}

				//Set seatingClass
				if (features[i] == iSeatingClassB)
				{
					SeatingClass = sSeatingClassB;
				}
				else if (features[i] == iSeatingClassF)
				{
					SeatingClass = sSeatingClassF;
				}
				else if (features[i] == iSeatingClassS)
				{
					SeatingClass = sSeatingClassS;
				}

				//Set sleeperClass
				if (features[i] == iSleeperClassB)
				{
					SleeperClass = sSleeperClassB;
				}
				else if (features[i] == iSleeperClassS)
				{
					SleeperClass = sSleeperClassS;
				}
				else if (features[i] == iSleeperClassF)
				{
					SleeperClass = sSleeperClassF;
				}
			

				//Set catering
				switch (features[i])
				{
					case iCateringC:
						catering = String.Concat(catering,sCateringC);
						break;
					case iCateringF:
						catering = String.Concat(catering,sCateringF);
						break;
					case iCateringH:
						catering = String.Concat(catering,sCateringH);
						break;
					case iCateringR:
						catering = String.Concat(catering,sCateringR);
						break;
					case iCateringT:
						catering = String.Concat(catering,sCateringT);
						break;
				}
			}
			
		
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets and sets the mode.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public ModeType Mode
		{
			get { return mode; }
			set { mode = value; }
		}


		/// <summary>
		/// Gets and sets the Reservations  
		/// </summary>
		public string Reservations
		{
			get { return reservations; }
			set { reservations = value; }
		}

		/// <summary>
		/// Gets and sets the SeatingClass  
		/// </summary>
		public string SeatingClass
		{
			get { return seatingClass; }
						set { seatingClass = value; }
		}

		/// <summary>
		/// Gets and sets the SleeperClass  
		/// </summary>
		public string SleeperClass
		{
			get { return sleeperClass; }
			set { sleeperClass = value; }
		}

		/// <summary>
		/// Gets and sets the Catering  
		/// </summary>
		public string Catering
		{
			get { return catering; }
			set { catering = value; }
		}
		#endregion




	}
}

