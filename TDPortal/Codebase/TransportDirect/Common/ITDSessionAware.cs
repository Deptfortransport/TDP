// *********************************************** 
// NAME                 : ITDSessionAware.cs 
// AUTHOR               : Peter Norell
// DATE CREATED         : 13/01/2005 
// DESCRIPTION			: Marker interface for things that are aware of session
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/ITDSessionAware.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:02   mturner
//Initial revision.
//
//   Rev 1.0   Jan 26 2005 15:24:54   PNorell
//Initial revision.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// A marker interface for being session aware.
	/// </summary>
	public interface ITDSessionAware
	{
		/// <summary>
		/// Gets/Sets if the session aware object considers itself to have changed or not
		/// </summary>
		bool IsDirty { get; set; }
	}
}
