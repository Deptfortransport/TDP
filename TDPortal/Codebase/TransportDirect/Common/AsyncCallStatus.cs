// *********************************************** 
// NAME         : AsyncCallStatus.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/AsyncCallStatus.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:00   mturner
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:01:04   jgeorge
//Initial revision.
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Possible results of executing a generic asynchronous call
	/// </summary>
	public enum AsyncCallStatus
	{
		/// <summary>
		/// No value has currently been specified
		/// </summary>
		None = 0,

		/// <summary>
		/// The call is currently ongoing.
		/// </summary>
		InProgress,

		/// <summary>
		/// The call has completed. This does not necessarily mean that results have been found. In some
		/// cases, calls may use the NoResults status, but this cannot be relied upon.
		/// </summary>
		CompletedOK,

		/// <summary>
		/// The call exceeded the maximum time allowed and was given up.
		/// </summary>
		TimedOut,

		/// <summary>
		/// There was an error validating the parameters. The asynchronous part of the call was not begun.
		/// </summary>
		ValidationError,

		/// <summary>
		/// The call has completed, and no results have been returned.
		/// </summary>
		NoResults
	}
}
