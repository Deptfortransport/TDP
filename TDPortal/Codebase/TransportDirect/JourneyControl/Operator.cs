// *********************************************** 
// NAME			: Operator.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 15/07/2005 
// DESCRIPTION	: Implementation of the Operator class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Operator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:54   mturner
//Initial revision.
//
//   Rev 1.0   Jul 18 2005 16:16:38   pcross
//Initial revision.
//

using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Defines and holds all data associated with a service operator (eg Virgin Trains, Arriva buses, etc).
	/// More precisely it is an operation of the operator since it is defined by operator *and* travel mode.
	/// </summary>
	public class Operator
	{
		#region Private Members
		
		private ModeType travelMode;
		private string code;
		private string name;
		private string url;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Empty object constructor
		/// </summary>
		public Operator()
		{}

		
		/// <summary>
		/// Constructor with all properties passed in
		/// </summary>
		public Operator(ModeType travelMode, string code, string name, string url)
		{
			// Populate the instance variables
			this.travelMode = travelMode;
			this.code = code;
			this.name = name;
			this.url = url;
		}

		/// <summary>
		/// Operator may have no mode
		/// </summary>
		public Operator(string code, string name, string url)
		{
			this.code = code;
			this.name = name;
			this.url = url;
		}


		#endregion

		#region Public Properties

		public ModeType TravelMode
		{
			get {return travelMode;}
			set {travelMode = value;}
		}

		public string Code
		{
			get {return code;}
			set {code = value;}
		}

		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		public string Url
		{
			get {return url;}
			set {url = value;}
		}

		#endregion
	}
}
