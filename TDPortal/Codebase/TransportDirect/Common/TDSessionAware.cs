// *********************************************** 
// NAME                 : TDSessionAware.cs 
// AUTHOR               : Peter Norell
// DATE CREATED         : 11/03/2005 
// DESCRIPTION			: A base class for session aware items
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDSessionAware.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:06   mturner
//Initial revision.
//
//   Rev 1.1   Apr 11 2005 15:47:04   COwczarek
//Class now implement the ITDSessionAware interface
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.0   Mar 11 2005 10:42:40   PNorell
//Initial revision.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// A base class for session aware items
	/// </summary>
	/// 
	[Serializable()]
	public class TDSessionAware : ITDSessionAware
	{

		/// <summary>
		/// Dirty flag, default to true as it needs to be saved after it has been instantiated.
		/// </summary>
		private bool dirty = true;

		/// <summary>
		/// Constructor - currently does nothing
		/// </summary>
		public TDSessionAware()
		{
		}

		/// <summary>
		/// Get/sets if this object has been changed since last save or not
		/// </summary>
		public bool IsDirty 
		{
			get { return dirty; }
			set { dirty = value; }
		}

	}
}
