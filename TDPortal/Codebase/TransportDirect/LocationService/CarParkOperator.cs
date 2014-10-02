// *********************************************** 
// NAME			: CarParkOperatort.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 07/08/2006 
// DESCRIPTION	: Class which holds information about an operartor for a car park
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.0   Aug 08 2006 10:05:40   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Stores information about an operator for a car park
	/// </summary>
	[Serializable()]
	public class CarParkOperator
	{
		private readonly string operatorCode;
		private readonly string operatorName;
		private readonly string operatorURL;
		private readonly string operatorTsAndCs;
		private readonly string operatorEmail;

		#region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
		public CarParkOperator()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="operatorCode">Operator code</param>
		/// <param name="operatorName">Operator name for car park</param>
		/// <param name="operatorURL">Operator url for car park</param>
		/// <param name="operatorTsAndCs">Terms and conditions url</param>
		/// <param name="operatorEmail">Operator email address</param>
		public CarParkOperator(string operatorCode, string operatorName, string operatorURL,
				string operatorTsAndCs, string operatorEmail)
		{
			this.operatorCode = operatorCode;
			this.operatorName = operatorName;
			this.operatorURL = operatorURL;
			this.operatorTsAndCs = operatorTsAndCs;
			this.operatorEmail = operatorEmail;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property. Get the operator code
		/// </summary>
		public string OperatorCode
		{
			get { return operatorCode; }
		}
		
		/// <summary>
		/// Read only property. Get the operator name
		/// </summary>
		public string OperatorName
		{
			get { return operatorName; }
		}

		/// <summary>
		/// Read only property. Get the operator url
		/// </summary>
		public string OperatorURL
		{
			get { return operatorURL; }
		}

		/// <summary>
		/// Read only property. Get the operator terms and condtions URL link
		/// </summary>
		public string OperatorTsAndCs
		{
			get { return operatorTsAndCs; }
		}

		/// <summary>
		/// Read only property. Get the operator email address
		/// </summary>
		public string OperatorEmail
		{
			get { return operatorEmail; }
		}

		#endregion
	}
}
