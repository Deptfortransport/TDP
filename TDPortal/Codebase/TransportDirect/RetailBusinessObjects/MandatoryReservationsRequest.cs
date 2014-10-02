//********************************************************************************
//NAME         : MandatoryReservationsRequest.cs
//AUTHOR       : Russell Wilby
//DATE CREATED : 2005-11-08
//DESCRIPTION  :BO input object used to request all 
//				  reservations for a journey from the SBO.  
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/MandatoryReservationsRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:12   mturner
//Initial revision.
//
//   Rev 1.0   Nov 11 2005 17:42:14   RWilby
//Initial revision.
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// BO input object used to request all mandatory reservations for a journey from the SBO
	/// </summary>
	[Serializable]
	public class MandatoryReservationsRequest : BusinessObjectInput
	{
		/// <summary>
		/// Output parameter fixed length (bytes)
		/// </summary>
		private const int OUTPUT_LENGTH = 258;

		private string[] ticketTypeCodes;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="interfaceVersion">interfaceVersion</param>
		/// <param name="fares">ticketTypeCodes</param>
		public MandatoryReservationsRequest(string interfaceVersion,string[] ticketTypeCodes) 
			: base("SU", "MT", interfaceVersion )
		{
			//Set ticketTypeCodes
			this.ticketTypeCodes = ticketTypeCodes;
			
			//Create HeaderInputParameter for Mandatory Reservations
			HeaderInputParameter inputParameter = new HeaderInputParameter(BuildRequestHeader());
			
			//Call base class method to add HeaderInputParameter
			AddInputParameter(inputParameter, 0);

			//Create HeaderOutputParameter for Mandatory Reservations
			HeaderOutputParameter outputParameter = new HeaderOutputParameter(OUTPUT_LENGTH);
			
			//Call base class method to add HeaderOutputParameter
			AddOutputParameter(outputParameter, 0);
		}


		/// <summary>
		/// Creates HeaderOutputParameter string for Mandatory Reservations
		/// </summary>
		/// <param name="fares">FareDataDto[]</param>
		/// <returns>HeaderOutputParameter string for Mandatory Reservations</returns>
		private string BuildRequestHeader() 
		{
			StringBuilder sb = new StringBuilder();

			if(ticketTypeCodes.Length > 0)
			{	
				//Section-ID. Length:8
				sb.Append("HEADER  "); 
				//Number-Types. Lenght: 3
				sb.Append(ticketTypeCodes.Length.ToString().PadLeft(3,'0')); 

				//Add Ticket-Type-codes
				foreach(string ticketTypeCode in ticketTypeCodes)
				{
					sb.Append(ticketTypeCode.PadRight(3,' '));
				}
			
				//Pad Ticket-Type-codes to 750.
				string ticketTypeCodePadding = new string(' ', (750 - (ticketTypeCodes.Length*3)));
				sb.Append(ticketTypeCodePadding);

				//Add Effective-Date. CCYYMMDD
				sb.Append(DateTime.Now.ToString("yyyyMMdd")); 
			}
			
			return sb.ToString();
		}

		/// <summary>
		/// TicketTypeCodes.
		/// Readonly property.
		/// </summary>
		public string[] TicketTypeCodes
		{
			get{return ticketTypeCodes;}
		}

	}
}
