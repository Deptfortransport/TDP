//********************************************************************************
//NAME         : PricingResultsHelper.cs
//AUTHOR       : James Broome
//DATE CREATED : 08/03/2005
//DESCRIPTION  : Implementation of PricingResultsHelper class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/PricingResultsHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:36   mturner
//Initial revision.
//
//   Rev 1.4   Dec 08 2005 09:55:16   mguney
//Changes made for code review.
//Resolution for 3345: Coach Fare Code Review - CR026_IR_2818 Coach Fares.doc
//
//   Rev 1.3   Nov 01 2005 17:20:44   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 22 2005 15:55:48   mguney
//Set converted fare method removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 21 2005 10:28:46   mguney
//Associated IR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 10:27:12   mguney
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:34:10   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for PricingResultsHelper.
	/// </summary>
	public class PricingResultsHelper
	{		
		/// <summary>
		/// Constructor
		/// </summary>
		public PricingResultsHelper()
		{
		}

		
		/// <summary>
		/// Public SetFare method
		/// FareAmount needs converting to TD float format, before calling SetTDFare
		/// If fareAmount is of type int, then convert to TD float format and call SetFare()
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="fare"></param>
		public void SetFare(Ticket ticket, bool isAdult, bool isDiscounted, int fareAmount)
		{
			//convert fare.
			float fareAmountConverted = ConvertFare(fareAmount);

			if (isDiscounted)
			{
				if (isAdult) 
				{
					ticket.DiscountedAdultFare = fareAmountConverted;
				}
				else 
				{
					ticket.DiscountedChildFare = fareAmountConverted;
				}
			}
			else
			{
				if (isAdult)
				{
					ticket.AdultFare = fareAmountConverted;
				}
				else
				{
					ticket.ChildFare = fareAmountConverted;
				}
			}
		}		

		/// <summary>
		/// Converts the fare provided by the CJP (cents) into a TDP fare (pounds).
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		public float ConvertFare(int fare)
		{
			return (float)(fare/100.00);
		}

	}
}
