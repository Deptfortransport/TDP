//********************************************************************************
//NAME         : MandatoryReservations.cs
//AUTHOR       : Russell Wilby
//DATE CREATED : 2005-11-08
//DESCRIPTION  : Represents MandatoryReservations 
//               applying to a journey/fare combination. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/MandatoryReservations.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:12   mturner
//Initial revision.
//
//   Rev 1.1   Nov 14 2005 12:08:12   RWilby
//Bug fix for handling output errors from SBO.
//Resolution for 3003: NRS enhancement for non-compulsory reservations
//
//   Rev 1.0   Nov 11 2005 17:41:52   RWilby
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Enumeration for Mandatory Reservation Flags
	/// </summary>
	[Serializable]
	public enum MandatoryReservationFlag
	{
		Required, //Yes is required
		NotRequired, //Not required
		OutwardOnly, //Required for outward direction only
		ReturnOnly, //Required for return direction only
	}

	///<summary>
	/// Encapsulates the collection of MandatoryReservations 
	///	applying to a journey/fare combination.
	/// </summary>
	[Serializable]
	public class MandatoryReservations
	{	
		//Hashtable to store mandatory reservations
		//Key:ShortTicketCode
		//Value:MandatoryReservationFlags enum
		private Hashtable  results = new  Hashtable();

		private bool hasErrors = false;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="output">output</param>
		/// <param name="ticketTypeCodes">ticketTypeCodes</param>
		public MandatoryReservations(BusinessObjectOutput output, string[] ticketTypeCodes)
		{
			if	(output.ErrorCode.Length > 0)
			{
				// error has already been logged
				// Set hasErrors property to true
				hasErrors = true;

				//If we have errors set each ticketTypeCode to MandatoryReservationFlag.NotRequired
				//this will allow the tickets to be displayed in the UI but with the NRS_UNAVAILABLE error message
				foreach(string ticketTypeCode in ticketTypeCodes)
				{
					if(!results.ContainsKey(ticketTypeCode))
					{
						results.Add(ticketTypeCode,MandatoryReservationFlag.NotRequired);
					}
				}
			
			}
			else
			{

				//Convert mandatory reservation flags to char array ready for processing
				char[] mandatoryReservationFlags =  output.OutputBody.Substring(8).TrimEnd().ToCharArray();
			
				//Make sure both array are of equal size
				if (int.Equals(mandatoryReservationFlags.Length,ticketTypeCodes.Length))
				{
					for(int i = 0;i<mandatoryReservationFlags.Length;i++)
					{
						//Safety check to make sure doesn't already exist
						if(!results.ContainsKey(ticketTypeCodes[i]))
						{
							switch(mandatoryReservationFlags[i])
							{
								case 'N':
								case 'X':
									results.Add(ticketTypeCodes[i],MandatoryReservationFlag.NotRequired);
									break;
								case 'Y':
								case 'E':
									results.Add(ticketTypeCodes[i],MandatoryReservationFlag.Required);
									break;
								case 'O':
									results.Add(ticketTypeCodes[i],MandatoryReservationFlag.OutwardOnly);
									break;
								case 'R':
									results.Add(ticketTypeCodes[i],MandatoryReservationFlag.ReturnOnly);
									break;
							}
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Readonly property.
		/// MandatoryReservations. Keyed on TicketTypeCode
		/// </summary>
		public Hashtable Results
		{
			get{return results;}
		}

		/// <summary>
		/// Readonly property.
		/// HasErrors
		/// </summary>
		public bool HasErrors
		{
			get{return hasErrors;}
		}
	}
}
