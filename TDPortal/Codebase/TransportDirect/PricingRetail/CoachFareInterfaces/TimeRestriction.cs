//********************************************************************************
//NAME         : TimeRestriction.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of TimeRestriction class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/TimeRestriction.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:32   mturner
//Initial revision.
//
//   Rev 1.7   May 25 2007 16:22:12   build
//Automatically merged from branch for stream4401
//
//   Rev 1.6   Nov 03 2005 17:37:26   RWilby
//Added Serializable attribute to class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 13 2005 14:55:24   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 12 2005 14:43:20   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 12 2005 14:38:46   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 13:43:44   mguney
//Default constructor included.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:04:18   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:10   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Time restriction details for coach fares. Start and end time are not TDDateTime because this class is
	/// passed to web services methods.
	/// </summary>
	[Serializable]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info")]
	public class TimeRestriction
	{

		/// <summary>
		/// Default constructor.
		/// </summary>		
		public TimeRestriction()
		{
			
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="startTime">Start time</param>
		/// <param name="endTime">End time</param>
		public TimeRestriction(DateTime startTime, DateTime endTime)
		{
			this.startTime = startTime;
			this.endTime = endTime;
		}

		#region Private variables
		private DateTime startTime;
		private DateTime endTime;
		#endregion

		#region Public properties
		/// <summary>
		/// Start time for restriction. [rw]
		/// </summary>
		public DateTime StartTime
		{
			get {return startTime;}
			set {startTime = value;}
		}
		
		/// <summary>
		/// End time for restriction. [rw]
		/// </summary>
		public DateTime EndTime
		{
			get {return endTime;}
			set {endTime = value;}
		}
		#endregion

		/// <summary>
		/// Compares with another TimeRestriction
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public bool Equals(TimeRestriction timeRestriction)
		{
			return (startTime.CompareTo(timeRestriction.startTime) == 0) && 
				(endTime.CompareTo(timeRestriction.endTime) == 0);
		}

	}
}