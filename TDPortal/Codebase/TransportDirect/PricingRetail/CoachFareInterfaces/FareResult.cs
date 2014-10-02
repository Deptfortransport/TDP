//********************************************************************************
//NAME         : FareResult.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FareResult class. Class for storing internal fare result information.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FareResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:28   mturner
//Initial revision.
//
//   Rev 1.2   Oct 13 2005 14:53:28   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:38   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:54   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Class for storing fare result information.
	/// </summary>
	public class FareResult
	{		

		#region Private variables
		private FareErrorStatus errorStatus;
		private CoachFare[] fares;
		private string requestId;
		#endregion

		#region Public properties

		/// <summary>
		/// Request id for reference. [rw]
		/// </summary>
		public string RequestId
		{
			get {return requestId;}
			set {requestId = value;}
		}

		/// <summary>
		/// Error status. [rw]
		/// </summary>
		public FareErrorStatus ErrorStatus
		{
			get {return errorStatus;}
			set {errorStatus = value;}
		}

		/// <summary>
		/// Array of returned coach fares. [rw]
		/// </summary>
		public CoachFare[] Fares
		{
			get {return fares;}
			set {fares = value;}
		}
		#endregion

	}
}