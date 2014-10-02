// *************************************************** 
// NAME                 : ReceivedOperationalEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 30/10/2003 
// DESCRIPTION  : A class for wrapping 
// Operational Events as a custom event. Used by the
// Event Receiver to log 'received' Operational Events
// as opposed to Operational Events it generates.
// *************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ReceivedOperationalEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:28   mturner
//Initial revision.
//
//   Rev 1.1   Nov 14 2003 11:47:32   geaton
//Added file formatter.
//
//   Rev 1.0   Oct 30 2003 12:22:26   geaton
//Initial Revision

using System;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	/// <summary>
	/// Defines a wrapper class for storing a reference to an
	/// Operational Event.
	/// </summary>
	[Serializable]
	public class ReceivedOperationalEvent : TDPCustomEvent	
	{
		private static ReceivedOperationalEventFileFormatter fileFormatter = new ReceivedOperationalEventFileFormatter();

		/// <summary>
		/// Gets the wrapped Operational Event.
		/// </summary>
		private OperationalEvent operationalEvent;
		public OperationalEvent WrappedOperationalEvent
		{
			get {return operationalEvent;}
		}

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="operationalEvent">
		/// A reference to this event is stored in the class.
		/// </param>
		/// <remarks>
		/// It is only necessary to store a reference to the operational event, 
		/// rather than a clone, since the Operational Event properties
		/// cannot be changed once set on constrution.
		/// </remarks>
		public ReceivedOperationalEvent(OperationalEvent operationalEvent) : base("NoSession", false)
		{
			this.operationalEvent = operationalEvent;
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}

}

