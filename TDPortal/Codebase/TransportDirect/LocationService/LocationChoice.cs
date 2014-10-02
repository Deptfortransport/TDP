// *********************************************** 
// NAME                 : LocationChoice.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Classes related to a location choice offer to user, or picked by him.
// Also :	LocationChoiceList class : list of LocationChoice objects
//			LocationChoiceComparer class : Compare 2 LocationChoice to sort them
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocationChoice.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:10   mturner
//Initial revision.
//
//   Rev 1.9   Apr 13 2005 09:10:08   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.8   Apr 07 2005 16:23:52   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.7   Apr 27 2004 13:44:36   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.6   Mar 08 2004 16:56:54   PNorell
//Added convinient ToString() method for debugging.
//
//   Rev 1.5   Oct 08 2003 10:44:48   passuied
//implemented detection of ADMINAREA choice to avoid exception when trying to get location details
//
//   Rev 1.4   Oct 03 2003 13:38:38   PNorell
//Updated the new exception identifier.
//
//   Rev 1.3   Oct 02 2003 17:50:12   COwczarek
//id parameter passed in TDException constructor set to -1 to enable compilation after introduction of new TDException constructor which takes an enum type for id. This is a temporary fix - the constructor taking an
//id of type long will be removed.
//
//   Rev 1.2   Sep 22 2003 17:31:22   passuied
//made all objects serializable
//
//   Rev 1.1   Sep 09 2003 17:23:48   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:30   passuied
//Initial Revision


using System;
using System.Collections;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for LocationChoice.
	/// </summary>
	[Serializable()]
	public class LocationChoice
	{

		#region Private members declaration
		private string stringDescription = string.Empty;
		private bool boolHasChildren = false;
		private string stringPickListCriteria = string.Empty;
		private string stringPickListValue = string.Empty;
		private OSGridReference gridReference = null;
		private string stringNaptan = string.Empty;
		private double doubleScore = 0;
		private string stringLocality = string.Empty;
		private string stringExchangePointType = string.Empty;
		private bool boolIsAdminArea = false;
		private double dblPartPostcodeMaxX = 0;
		private double dblPartPostcodeMaxY = 0;
		private double dblPartPostcodeMinX = 0;
		private double dblPartPostcodeMinY = 0;
		#endregion

		#region Constructor
		public LocationChoice(string description, bool hasChildren, string pickListCriteria, string pickListValue, OSGridReference gridReference, string naptan, double score, string locality, string exchangePointType , bool isAdminArea)
		{
			stringDescription = description;
			boolHasChildren = hasChildren;
			stringPickListCriteria = pickListCriteria;
			stringPickListValue = pickListValue;
			this.gridReference = gridReference;
			stringNaptan = naptan;
			doubleScore = score;
			stringLocality = locality;
			stringExchangePointType = exchangePointType;
			boolIsAdminArea = isAdminArea;
			dblPartPostcodeMaxX = 0;
			dblPartPostcodeMaxY = 0;
			dblPartPostcodeMinX = 0;
			dblPartPostcodeMinY = 0;
		}
		
		public LocationChoice(string description, bool hasChildren, string pickListCriteria, string pickListValue, OSGridReference gridReference, string naptan, double score, string locality, string exchangePointType , bool isAdminArea, double partPostcodeMaxX, double partPostcodeMaxY, double partPostcodeMinX, double partPostcodeMinY)
		{
			stringDescription = description;
			boolHasChildren = hasChildren;
			stringPickListCriteria = pickListCriteria;
			stringPickListValue = pickListValue;
			this.gridReference = gridReference;
			stringNaptan = naptan;
			doubleScore = score;
			stringLocality = locality;
			stringExchangePointType = exchangePointType;
			boolIsAdminArea = isAdminArea;
			dblPartPostcodeMaxX = partPostcodeMaxX;
			dblPartPostcodeMaxY = partPostcodeMaxY;
			dblPartPostcodeMinX = partPostcodeMinX;
			dblPartPostcodeMinY = partPostcodeMinY;
		}
		#endregion

		#region Public properties
		
		/// <summary>
		/// Read-only property. gets the choice description
		/// </summary>
		public string Description
		{
			get
			{
				return stringDescription;
			}
		}

		/// <summary>
		/// Read-only property. Indicates if choice has children
		/// </summary>
		public bool HasChilden
		{
			get
			{
				return boolHasChildren;
			}
		}

		/// <summary>
		/// Read-only property. PickList criteria
		/// </summary>
		public string PicklistCriteria
		{
			get
			{
				return stringPickListCriteria;
			}
		}

		/// <summary>
		/// Read-only property. PickList value
		/// </summary>
		public string PicklistValue
		{
			get
			{
				return stringPickListValue;
			}
		}

		/// <summary>
		/// Read-only property. gridReference
		/// </summary>
		public OSGridReference OSGridReference
		{
			get
			{
				return gridReference;
			}
		}

		/// <summary>
		/// Read-only property. Naptan
		/// </summary>
		public string Naptan
		{	
			get
			{
				return stringNaptan;
			}
		}

		/// <summary>
		/// Read-only property. choice's score
		/// </summary>
		public double Score
		{
			get
			{
				return doubleScore;
			}
		}

		/// <summary>
		/// Read-only property. Locality
		/// </summary>
		public string Locality
		{
			get
			{
				return stringLocality;
			}
		}

		/// <summary>
		/// Read-only property. ExchangePoint type
		/// </summary>
		public string ExchangePointType
		{
			get
			{
				return stringExchangePointType;
			}
		}

		public bool IsAdminArea
		{
			get
			{
				return boolIsAdminArea;
			}

		}

		/// <summary>
		/// Read-only property. Maximum X coordinate for postcode area
		/// </summary>
		public double PartPostcodeMaxX
		{
			get
			{
				return dblPartPostcodeMaxX;
			}

		}

		/// <summary>
		/// Read-only property. Maximum Y coordinate for postcode area
		/// </summary>
		public double PartPostcodeMaxY
		{
			get
			{
				return dblPartPostcodeMaxY;
			}

		}

		/// <summary>
		/// Read-only property. Minimum X coordinate for postcode area
		/// </summary>
		public double PartPostcodeMinX
		{
			get
			{
				return dblPartPostcodeMinX;
			}

		}

		/// <summary>
		/// Read-only property. Minimum Y coordinate for postcode area
		/// </summary>
		public double PartPostcodeMinY
		{
			get
			{
				return dblPartPostcodeMinY;
			}

		}

		#endregion
	
		public override string ToString()
		{
			return "[desc = "+stringDescription+" : hasChildren = "+boolHasChildren+" : picklistCriteria = "+PicklistCriteria+" : picklistValue = "+PicklistValue+" : gridReference = "+gridReference +" : Naptan = "+stringNaptan +" : score = "+doubleScore+" : locality = "+Locality+" : exchangepoint type = "+ExchangePointType;
		}

	}

	[Serializable()]
	public class LocationChoiceList : ArrayList
	{
		private int intLevel = 0;
		private bool isVague = false;
		private bool isSingleWordAddress = false;

		/// <summary>
		/// Bool that indicates the return from the gazetteer location search is vague
		/// i.e. too many results
		/// </summary>
		public bool IsVague
		{
			get {return isVague;}
			set {isVague = value;}
		}

		/// <summary>
		/// Bool that indicates the address was a single word search
		/// </summary>
		public bool IsSingleWordAddress
		{
			get{return isSingleWordAddress;}
			set{isSingleWordAddress = value;}
		}

		public LocationChoiceList(int level)
		{
			intLevel = level;
		}
	
		public LocationChoiceList()
		{
		}

		/// <summary>
		/// read-only property. LocationChoiceList level
		/// </summary>
		public int Level
		{
			get
			{
				return intLevel;
			}
		}

		/// <summary>
		/// Overriden LocationChoiceList Add method
		/// </summary>
		/// <param name="choice">choice to add</param>
		/// <returns>returns the index where the choice has been added</returns>
		public override int Add (object choice)
		{
			if (!(choice is LocationChoice))
				throw new TDException(
					"Can't add the object of type different to LocationChoice",						false,
					TDExceptionIdentifier.LSWrongType);

			return base.Add(choice);
		}

		/// <summary>
		/// Overriden LocationChoiceList Insert method
		/// </summary>
		/// <param name="index">index where to insert the locationChoice in the list</param>
		/// <param name="choice">choice to insert</param>
		public override void Insert (int index, object choice)
		{
			if (!(choice is LocationChoice))
				throw new TDException(
					"Can't insert the object of type different to LocationChoice",						false,
					TDExceptionIdentifier.LSWrongType);

			base.Insert(index, choice);
		}

		/// <summary>
		/// overriden indexer 
		/// </summary>
		public override object this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (!(value is LocationChoice))
					throw new TDException(
						"Can't insert the object of type different to LocationChoice",		
						false,
						TDExceptionIdentifier.LSWrongType);
			}
		}

		/// <summary>
		/// Overriden Remove method
		/// </summary>
		/// <param name="choice">choice to remove</param>
		public override void Remove(object choice)
		{
			if (!(choice is LocationChoice))
				throw new TDException(
					"Can't remove the object of type different to LocationChoice",
					false,
					TDExceptionIdentifier.LSWrongType);

			base.Remove(choice);
		}
	}

	/// <summary>
	/// Class used to compare 2 location choices
	/// </summary>
	[Serializable()]
	public class LocationChoiceComparer : IComparer
	{

		/// <summary>
		/// Compares 2 locations choices
		/// </summary>
		/// <param name="x">choice x</param>
		/// <param name="y">choice y</param>
		/// <returns>returns 0 if choicex==choicey. returns int lower than 0 if choicex lower than choicey. returns int greater than 0 if choicex greater than choicey</returns>
		public int Compare (object x, object y)
		{
			if (!(x is LocationChoice) || !(y is LocationChoice))
			{
				throw new TDException("Can't compare those object. At least one of them is not a LocationChoice object", false, TDExceptionIdentifier.LSWrongType);
			}
			
			LocationChoice xLocation = (LocationChoice)x;
			LocationChoice yLocation = (LocationChoice)y;

			if (xLocation.Score > yLocation.Score)
				return -1;
			else if (xLocation.Score < yLocation.Score)
				return 1;
			else
			{
				if (xLocation.Description.CompareTo( yLocation.Description)< 0)
					return -1;
				else 
					if (xLocation.Description.CompareTo(yLocation.Description)>0)
					return 1;
				else
					return 0;
			}

		}
	}
}
