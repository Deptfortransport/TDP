//********************************************************************************
//NAME         : SupplementDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to pass back details of all  
//                supplements applicable to a specific journey 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/SupplementDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:54   mturner
//Initial revision.
//
//   Rev 1.1   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.0   Mar 01 2005 18:47:44   RPhilpott
//Initial revision.

using System;
using System.Collections;

using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object to pass back details of a 
	///  supplement (applicable to a specific journey) 	
	/// </summary>
	[Serializable()]
	public class SupplementDto
	{
		private string code;
		private string description;
		private int cost;
		
		public SupplementDto(string code, string description, int cost)
		{
			this.cost = cost;
			this.code = code;
			this.description = description;
		}

		/// <summary>
		/// The code for this supplement
		/// </summary>
		public string Code
		{
			get { return code; }
			set { code = value; }
		}
		
		/// <summary>
		/// The description of this supplement
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// The cost of this supplement (in pence)
		/// </summary>
		public int Cost
		{
			get { return cost; }
			set { cost = value; }
		}


	}
}
