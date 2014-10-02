//********************************************************************************
//NAME         : PricingResultsHelper.cs
//AUTHOR       : James Broome
//DATE CREATED : 08/03/2005
//DESCRIPTION  : Implementation of PricingResultsHelper class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/PricingResultsHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:06   mturner
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:34:10   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Summary description for PricingResultsHelper.
	/// </summary>
	public class PricingResultsHelper
	{
		// Key for indicating no associated discount card
		public string NO_DISCOUNT = string.Empty;
		// Default values of valid child ages
		public int DEFAULT_MIN = 0;
		public int DEFAULT_MAX = 18;

		/// <summary>
		/// Constructor
		/// </summary>
		public PricingResultsHelper()
		{
		}

		/// <summary>
		/// Map the flexibility of the fare onto our ticket
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		public Flexibility ConvertFlexibility(Fare fare)
		{
			switch (fare.fareRestrictionType)
			{
				case FareType.NotFlexible:
					return Flexibility.NoFlexibility;					
				case FareType.LimitedFlexibility:
					return Flexibility.LimitedFlexibility;					
				default:
					return Flexibility.FullyFlexible;					
			}
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
			SetConvertedFare(ticket, isAdult, isDiscounted, ConvertFare(fareAmount));
		}

		/// <summary>
		/// Set the appropriate fare field of a Ticket, based on parameters
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="fare"></param>
		public void SetConvertedFare(Ticket ticket, bool isAdult, bool isDiscounted, float fareAmount)
		{
			if (isDiscounted)
			{
				if (isAdult) 
				{
					ticket.DiscountedAdultFare = fareAmount;
				}
				else 
				{
					ticket.DiscountedChildFare = fareAmount;
				}
			}
			else
			{
				if (isAdult)
				{
					ticket.AdultFare = fareAmount;
				}
				else
				{
					ticket.ChildFare = fareAmount;
				}
			}
		}

		/// <summary>
		/// Converts the fare provided by the CJP into a TDP fare
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		public float ConvertFare(int fare)
		{
			return (float)(fare/100.00);
		}

		/// <summary>
		/// Parse the childAgeRange string to convert it into min and max child ages
		/// It assumes that the childAgeRange is of the form "[minAge]-[maxAge]"
		/// If this fails then the default age range (0-18) is used
		/// </summary>
		/// <param name="childAgeRange"></param>
		/// <returns></returns>
		public int[] GenerateChildAges(string childAgeRange)
		{
			int[] ages = new int[2];
			try 
			{
				string[] agesStrings = childAgeRange.Split(new Char[]{'-'});
		
				if (ages.Length ==2) 
				{
					ages[0] = int.Parse(agesStrings[0]);
					ages[1]= int.Parse(agesStrings[1]);
				}
			}
			catch (Exception)
			{
				ages[0] = DEFAULT_MIN;
				ages[1] = DEFAULT_MAX;
			}
			return ages;
		}

	}
}
