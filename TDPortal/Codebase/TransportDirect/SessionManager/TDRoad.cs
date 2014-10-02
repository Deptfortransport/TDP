// *********************************************** 
// NAME			: TDRoad.cs
// AUTHOR		: Reza Bamshad
// DATE CREATED	: 18/01/2005 
// DESCRIPTION	: Implementation of the TDRoad class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDRoad.cs-arc  $
//
//   Rev 1.1   Oct 06 2010 13:44:02   mturner
//Updated regular expression used to validate road names.  This is to bring it more in line with the logic used by the CJP.
//Resolution for 5617: Avoid Roads regular expression too weak
//
//   Rev 1.0   Nov 08 2007 12:48:40   mturner
//Initial revision.
//
//   Rev 1.5   Nov 09 2005 15:01:22   RPhilpott
//Remove spurious reference to Windows.Forms.
//
//   Rev 1.4   Mar 18 2005 11:01:04   RAlavi
//Modifications related to door to door page for car costing
//
//   Rev 1.3   Mar 02 2005 15:27:28   RAlavi
//Changes for ambiguity
//
//   Rev 1.2   Feb 24 2005 11:37:32   PNorell
//Updated for favourite details.
//
//   Rev 1.1   Feb 21 2005 13:02:30   esevern
//added roadname property
//
//   Rev 1.0   Jan 28 2005 10:01:32   ralavi
//Initial revision.

using System;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.SessionManager
{

	public enum TDRoadStatus
	{
		Unspecified,
		Ambiguous,
		Valid
	}
	/// <summary>
	/// Summary description for TDRoad.
	/// </summary>
	[Serializable()]
	public class TDRoad
	{

		private TDRoadStatus roadStatus = TDRoadStatus.Unspecified;
        private const string pattern = @"^[ABM]{1}[0-9]+\s?(TOLL|\(M\))?,?$";

		private string roadName;

		public TDRoad()
		{
		}

		public TDRoad(string name)
		{
			if( ValidateRoadName(name) )
			{
				roadStatus = TDRoadStatus.Valid;
			}
			roadName = name;
		}


		public bool ValidateRoadName(string text)
		{
			Regex regexRoadname = new Regex(pattern,RegexOptions.IgnoreCase);
 			return regexRoadname.IsMatch(text);
		}

		public TDRoadStatus Status
		{
			get { return roadStatus;}
			set { roadStatus = value;}
		}

		/// <summary>
		/// Read/write property returning road name (eg. A1, M11 etc)
		/// </summary>
		public string RoadName 
		{
			get 
			{
				return roadName;
			}
			set 
			{
				roadName = value;
			}
		}
	
	}
}
