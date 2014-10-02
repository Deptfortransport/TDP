//********************************************************************************
//NAME         : FaresInterface.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FaresInterface abstract class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FaresInterface.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.4   Oct 31 2005 11:42:40   mguney
//COACHFARESRESPONSETIMEOUTMILLISECS constant changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 13 2005 14:53:42   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:13:34   mguney
//Casting applied when getting the timeout.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:52   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:58   mguney
//Initial revision.

using System;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Base class for different types of coach fare interfaces.
	/// </summary>
	public abstract class FaresInterface : IFaresInterface
	{
		//Property key for timeout
		private const string COACHFARESRESPONSETIMEOUTMILLISECS = 
			"PricingRetail.CoachFaresInterfaces.CoachFaresResponseTimeoutMilliSecs";				

		/// <summary>
		/// Gets the time out value in milliseconds from the properties table.
		/// </summary>
		/// <returns>Timeout in milliseconds</returns>
		protected int GetTimeout()
		{
			int timeOut = Convert.ToInt32((string)Properties.Current[COACHFARESRESPONSETIMEOUTMILLISECS],
				CultureInfo.CurrentCulture.NumberFormat);
			return timeOut;
		}

		#region IFaresInterface Members

		/// <summary>
		/// The method for getting the coach fares from the providers.
		/// </summary>
		/// <param name="fareRequest">The request information</param>
		/// <returns>FareResult</returns>		
		public abstract FareResult GetCoachFares(FareRequest fareRequest);		

		#endregion
	}
}