// *********************************************** 
// NAME                 : RTTIEvent.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 24/01/2005 
// DESCRIPTION  	: Defines the class for incrementing RTTI counter.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RTTIEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:32   mturner
//Initial revision.
//
//   Rev 1.1   Mar 08 2005 16:27:20   schand
//Code Review error has been fixed.
//
//   Rev 1.0   Jan 25 2005 13:41:18   schand
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines the class for incrementing RTTI counter.
	/// The counter will increased each time when RTTI service is consumed.
	/// </summary>
	[Serializable]
	public class RTTIEvent : TDPCustomEvent 
	{	
		private DateTime startTime = DateTime.Now;
		private DateTime finishTime= DateTime.Now;
		private bool dataReceived = false;
		private static RTTIEventFileFormatter  fileFormatter = new RTTIEventFileFormatter();

		public RTTIEvent(DateTime start, DateTime finshed, bool recievedData): base(string.Empty, false)
		{	
			startTime = start;
			finishTime = finshed;
			dataReceived = recievedData ;
		}


		
		/// <summary>
		/// Read-Only property returns the time when the RTTI requested the external server. 
		/// </summary>
		public DateTime StartTime
		{
			get {return startTime;}
		}

		/// <summary>
		/// Read-Only property returns the time when the RTTI requested the external server. 
		/// </summary>
		public DateTime FinishTime
		{
			get {return finishTime;}
		}


		/// <summary>
		/// Read-Only property indicates whether data was received sucessfully or not?
		/// </summary>
		public bool DataReceived
		{
			get {return dataReceived;}
		}

		/// <summary>
		/// Read-Only property provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}


	}
}
