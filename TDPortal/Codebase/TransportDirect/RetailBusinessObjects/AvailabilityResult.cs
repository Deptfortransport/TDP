//********************************************************************************
//NAME         : AvailabilityResult.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Encapsulates the collection of availabilities  
//				 resulting from a single availabity request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/AvailabilityResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.8   Dec 08 2005 16:35:16   RPhilpott
//Treat overbook places as being available.
//Resolution for 3352: DN040: Overbooked places should be considered available
//
//   Rev 1.7   Nov 24 2005 14:31:06   RPhilpott
//Restore specific handling of E00202
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.5   Nov 09 2005 12:31:48   build
//Automatically merged from branch for stream2818
//
//   Rev 1.4.1.0   Nov 02 2005 17:40:06   rhopkins
//Added FareKey property to facilitate collation of results for multiple Fares.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Apr 23 2005 12:39:50   RPhilpott
//Allow for RVBO reinitialisation after NRS comms error.
//Resolution for 2301: PT - RVBO discontinues comms with NRS after error
//
//   Rev 1.3   Apr 07 2005 20:52:32   RPhilpott
//Corrections to Supplement and Availability checking.
//
//   Rev 1.2   Apr 01 2005 10:28:40   RPhilpott
//Correction to NRS Error detection
//
//   Rev 1.1   Mar 31 2005 18:44:24   RPhilpott
//Changes to handling of RVBO calls
//
//   Rev 1.0   Mar 22 2005 16:30:40   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Encapsulates the collection of availabilities  
	///	 resulting from a single availabity request.
	/// </summary>
	[Serializable]
	public class AvailabilityResult
	{
		private const string NRS_ERROR = "V080";
		private const string NRS_LEG_NOT_FOUND = "NRS Error:E00202";
		private const int PRODUCT_LENGTH = 19; 

		private ArrayList productAvailabilityList = new ArrayList();
		
		private bool nrsTrainNotFound = false;
		private bool nrsServiceError = false;
		private bool availabilityExistsForFare = false;

		public ArrayList ProductAvailabilityList
		{
			get { return productAvailabilityList; }
		}

		public bool TrainNotFoundByNrs
		{
			get { return nrsTrainNotFound; }
		}

		public bool NrsServiceError
		{
			get { return nrsServiceError; }
		}

		public bool AvailabilityExistsForFare
		{
			get { return availabilityExistsForFare; }
		}


		public AvailabilityResult(BusinessObjectOutput output) 
		{
			if	(output.ErrorCode.Length > 0)
			{
				if	(output.ErrorCode.Equals(NRS_ERROR) && output.RecordDetails[0].RecordLength > 0)
				{
					TDTraceLevel level;

					if	(output.OutputBody.StartsWith(NRS_LEG_NOT_FOUND))
					{
						level = TDTraceLevel.Info;
						nrsTrainNotFound = true;			// no inventory for supplied UID on this date
					}
					else
					{
						level = TDTraceLevel.Error;
						nrsServiceError = true;				// some other error				
					}

					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, level, "NRS returned error text: " + output.OutputBody));
					return;
				}
				else
				{
					// already logged by BusinessObject class ...
					nrsServiceError = true;						
					return;
				}
			}

			// no errors, so extract results for individual products ...
			
			int productCount = output.RecordDetails[0].RecordLength / PRODUCT_LENGTH;

			for (int i = 0; i < productCount; i++)
			{
				string productDetail = output.OutputBody.Substring(i * PRODUCT_LENGTH, PRODUCT_LENGTH);

				if	(productDetail.Equals(new String(' ', PRODUCT_LENGTH)))
				{
					break;
				}

				ProductAvailability detail = new ProductAvailability(productDetail);
				
				productAvailabilityList.Add(detail);
	
				if	(detail.PlacesAvailable > 0 || detail.OverbookAvailable > 0)
				{
					availabilityExistsForFare = true;
				}
			}
		}

	}

	/// <summary>
	/// Encapsulates availability of a single product. 
	/// </summary>
	[Serializable]
	public class ProductAvailability
	{
		private string product;
		private string fareKey;

		private int placesAvailable;
		private int overbookAvailable;


		public ProductAvailability(string productDetail)
		{

			this.product = productDetail.Substring(0, 14);
			this.fareKey = productDetail.Substring(0, 3) + productDetail.Substring(6, 5);  // TicketType + RouteCode

			try
			{
				this.placesAvailable = Int32.Parse(productDetail.Substring(15, 2));
			}
			catch
			{
				this.placesAvailable = 0;
			}

			try
			{
				this.overbookAvailable = Int32.Parse(productDetail.Substring(17, 2));
			}
			catch
			{
				this.overbookAvailable = 0;
			}

		}

		/// <summary>
		/// Fare identifier from result (TicketType + RouteCode)
		/// </summary>
		public string FareKey
		{
			get { return fareKey; }
		}

		/// <summary>
		/// Product identifier from result
		/// </summary>
		public string Product
		{
			get { return product; }
		}

		/// <summary>
		/// Number of counted/identified places available for Product
		/// </summary>
		public int PlacesAvailable
		{
			get { return placesAvailable; }
		}
		
		/// <summary>
		/// Number of overbooked places available for Product
		/// </summary>
		public int OverbookAvailable
		{
			get { return overbookAvailable; }
		}
	}
}