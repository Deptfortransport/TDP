// *********************************************** 
// NAME			: AirOperator.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Represents an air operator (airline)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:20   mturner
//Initial revision.
//
//   Rev 1.2   May 20 2004 11:32:26   jgeorge
//Added Serializable attribute so class can be used with Session Manager.
//
//   Rev 1.1   May 13 2004 09:28:12   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:48   jgeorge
//Initial revision.

using System;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Represents an air operator (airline)
	/// </summary>
	[Serializable]
	public class AirOperator
	{
		private string iataCode;
		private string name;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="iataCode"></param>
		/// <param name="name"></param>
		public AirOperator(string iataCode, string name)
		{
			this.iataCode = iataCode;
			this.name = name;
		}

		/// <summary>
		/// Operator IATA Code
		/// </summary>
		public string IATACode 
		{
			get { return iataCode; }
		}
		/// <summary>
		/// Operator name
		/// </summary>
		public string Name 
		{
			get { return name; }
		}
	}
}
