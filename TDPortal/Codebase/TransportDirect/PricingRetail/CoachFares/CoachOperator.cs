//********************************************************************************
//NAME         : CoachOperator.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 12/10/2005
//DESCRIPTION  : Implementation of CoachOperator class which stores information for coach operators.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.1   Oct 14 2005 09:55:54   mguney
//FareProviderURL changed to FareProviderUrl
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 13 2005 09:35:24   mguney
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// CoachOperator class.
	/// </summary>
	public class CoachOperator
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="interfaceType">Route or Journey.</param>
		/// <param name="operatorCode">operatorCode</param>
		/// <param name="operatorName">operatorName</param>
		/// <param name="fareProviderURL">URL for the web services. Only for Route type coach operators.</param>
		public CoachOperator(CoachFaresInterfaceType interfaceType,string operatorCode,
			string operatorName,string fareProviderUrl)
		{
			this.interfaceType = interfaceType;
			this.operatorCode = operatorCode;
			this.operatorName = operatorName;
			this.fareProviderUrl = fareProviderUrl;
		}

		#region Private variables
		private CoachFaresInterfaceType interfaceType;
		private string operatorCode;
		private string operatorName;
		private string fareProviderUrl;
		#endregion

		#region Public properties
		
		/// <summary>
		/// Interface type. Route or Journey. [r]
		/// </summary>
		public CoachFaresInterfaceType InterfaceType
		{
			get {return interfaceType;}
		}

		/// <summary>
		/// Operator code. [r]
		/// </summary>
		public string OperatorCode
		{
			get {return operatorCode;}
		}

		/// <summary>
		/// Operator name. [r]
		/// </summary>
		public string OperatorName
		{
			get {return operatorName;}
		}

		/// <summary>
		/// URL for the web services. Only for Route type coach operators. [r]
		/// </summary>
		public string FareProviderUrl
		{
			get {return fareProviderUrl;} 
		}

		#endregion


	}
}
