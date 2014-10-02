//********************************************************************************
//NAME         : Supplement.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-02
//DESCRIPTION  : Represents an individual supplement 
//               applying to a journey/fare combination. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Supplement.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:24   mturner
//Initial revision.
//
//   Rev 1.4   Nov 16 2005 12:17:08   RPhilpott
//Add extra checks for class and type. 
//Resolution for 3081: DN040: Displayable supplements handling
//
//   Rev 1.3   Apr 29 2005 20:45:38   RPhilpott
//Fix typos in logging.
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.2   Apr 07 2005 20:52:32   RPhilpott
//Corrections to Supplement and Availability checking.
//
//   Rev 1.1   Mar 22 2005 20:50:10   RPhilpott
//Supplement filtering using DataServices
//
//   Rev 1.0   Mar 22 2005 16:30:42   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Represents an individual supplement 
	/// applying to a journey/fare combination. 
	/// </summary>
	[Serializable]
	public class Supplement
	{
	
		private int legNumber = 0;
		private bool returnDirection = false; 
		private string supplementCode = string.Empty;
		
		private bool seat		= false;
		private bool berth		= false;
		private bool bicycle	= false;
		private bool chargeable = false;
		
		private bool filterChecked = false;
		private bool isDisplayable = false;

		// additional data -- only added if in displayabale list
		private int cost = 0;
		private string description = string.Empty;
		private SupplementType supplementType  = SupplementType.Unknown;
		private int supplementClass = 0;


		private static IList displayableSupplements = null;
		
		static Supplement()
		{
			if	(displayableSupplements == null)
			{
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				displayableSupplements = ds.GetList(DataServiceType.DisplayableSupplements);
			}
		}

		public Supplement(string supplement)
		{
			returnDirection = (supplement.ToCharArray(0, 1)[0] == 'R');
			legNumber = Int32.Parse(supplement.Substring(1, 2));
			supplementCode = supplement.Substring(3, 3);
			chargeable = (supplement.ToCharArray(7, 1)[0] == '1');

			char accom = supplement.ToCharArray(6, 1)[0];

			switch (accom)
			{
				case 'S':
					seat = true;
					break;
		
				case 'B':
					berth = true;
					break;
		
				case 'C':
					bicycle = true;
					break;
			}
		}

		
		public int LegNumber 
		{
			get { return legNumber; }
		}

		public bool ReturnDirection 
		{
			get { return returnDirection; }
		}

		public string SupplementCode 
		{
			get { return supplementCode; } 
		}

		public bool Seat
		{
			get { return seat; }
		}
		
		public bool Berth
		{
			get { return berth; }
		}

		public bool Bicycle
		{
			get { return bicycle; }
		}

		public bool Chargeable
		{
			get { return chargeable; }
		}
		
		public int Cost
		{
			get { return cost; }
		}

		public string Description
		{
			get { return description; }
		}

		public int SupplementClass
		{
			get { return supplementClass; }
		}

		public SupplementType SupplementType
		{
			get { return supplementType; }
		}

		public void AddAdditionalData(string additionalData)
		{
			cost = Int32.Parse(additionalData.Substring(71, 5));
			description = additionalData.Substring(36, 20);	
			
			string type = additionalData.Substring(68, 3);
 		
			switch (type)
			{
				case "SEA":
					supplementType = SupplementType.Seat;
					break;

				case "SLE":
					supplementType = SupplementType.Sleeper;
					break;

				case "BIK":
					supplementType = SupplementType.Bicycle;
					break;

				case "EXA":
					supplementType = SupplementType.NonAccomodation;
					break;

				case "QUO":
					supplementType = SupplementType.SeatQuota;
					break;
				
				default:
					supplementType = SupplementType.Unknown;
					break;
			}

			string suppClass = additionalData.Substring(110, 1);

			switch (suppClass)
			{
				case "1":
					supplementClass = 1;
					break;

				case "2":
					supplementClass = 2;
					break;
			}
		}

		public bool IsDisplayable
		{
			// no locking needed here because a given instance is  
			//  only ever accessed on a single thread ...

			get
			{
				if	(!filterChecked)
				{
					isDisplayable = displayableSupplements.Contains(supplementCode);
					filterChecked = true;
				}	
				return isDisplayable;
			}
		}

		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("LegNumber = ").Append(legNumber.ToString()).Append(", ");
			sb.Append("Code = ").Append(supplementCode.ToString()).Append(", ");
			sb.Append("Description = ").Append(description.ToString()).Append(", ");
			sb.Append("Seat = ").Append(seat.ToString()).Append(", ");
			sb.Append("Berth = ").Append(berth.ToString()).Append(", ");
			sb.Append("Bicycle = ").Append(bicycle.ToString()).Append(", ");
			sb.Append("Chargeable = ").Append(chargeable.ToString()).Append(", ");
			sb.Append("Cost = ").Append(cost.ToString()).Append(", ");
			sb.Append("Type = ").Append(supplementType.ToString()).Append(", ");
			sb.Append("Class = ").Append(supplementClass.ToString()).Append(", ");
			sb.Append("ReturnDirection = ").Append(returnDirection.ToString());

			return sb.ToString();
		}
	}

	public enum SupplementType 
	{
		Unknown,
		Seat,
		Sleeper,
		Bicycle,
		NonAccomodation,
		SeatQuota
	}

}
