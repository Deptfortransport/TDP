// *********************************************** 
// NAME			: Upgrade.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 25/01/05
// DESCRIPTION	: Implementation of the Upgrade class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Upgrade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.3   Apr 01 2005 12:13:18   jgeorge
//Added [Serializable] attribute, since Upgrade objects are used within Ticket objects, which are used within the PricingRetailOptionsState object held in session state
//
//   Rev 1.2   Mar 30 2005 16:54:48   RPhilpott
//Add upgrade information to time-based tickets.
//
//   Rev 1.1   Mar 14 2005 16:23:52   jmorrissey
//Changed Url to Code. The code holds a code to describe the type of upgrade.
//
//The url for an upgrades page is now held in the propertirs database.
//
//   Rev 1.0   Jan 25 2005 14:34:38   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Contains Upgrade details for a Ticket
	/// </summary>
	[Serializable]
	public class Upgrade
	{
		private float cost;
		private string description;
		private string code;

		public Upgrade(string code, string description, float cost)
		{
			this.code = code;
			this.description = description;
			this.cost = cost;
		}

		public Upgrade(string code, string description, int costPence)
		{
			this.code = code;
			this.description = description;
			this.cost = ((float)costPence) / 100 ;
		}

		/// <summary>
		/// read/write property for cost field
		/// </summary>
		public float Cost
		{
			get
			{
				return cost;
			}
			set 
			{
				cost = value;
			}
		}

		/// <summary>
		/// read/write property for description field
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
			set 
			{
				description = value;
			}
		}
		/// <summary>
		/// read/write property for code field
		/// </summary>
		public string Code
		{
			get
			{
				return code;
			}
			set 
			{
				code = value;
			}
		}

	}		
}
