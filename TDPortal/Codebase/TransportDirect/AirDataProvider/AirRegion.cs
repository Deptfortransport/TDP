// *********************************************** 
// NAME			: AirRegion.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Represents a region used to define groups of airports
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirRegion.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:22   mturner
//Initial revision.
//
//   Rev 1.2   May 20 2004 11:32:30   jgeorge
//Added Serializable attribute so class can be used with Session Manager.
//
//   Rev 1.1   May 13 2004 09:28:14   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:50   jgeorge
//Initial revision.

using System;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Represents a region used to define groups of airports
	/// </summary>
	[Serializable]
	public class AirRegion
	{
		private int code;
		private string name;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="code"></param>
		/// <param name="name"></param>
		public AirRegion(int code, string name)
		{
			this.code = code;
			this.name = name;
		}

		/// <summary>
		/// Region code
		/// </summary>
		public int Code 
		{
			get { return code; }
		}

		/// <summary>
		/// Region name
		/// </summary>
		public string Name 
		{
			get { return name; }
		}
	}
}
