// ***********************************************
// NAME 		: IKey.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: An interface that defines the ID property
// which must be included in every TypeKey class defined in
// the Key.cs file.  
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/IKey.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:41:08   mturner
//Initial revision.
//
//   Rev 1.1   Nov 03 2004 14:54:52   Schand
//// added Seasonal Notice board
//
//   Rev 1.0   Jul 03 2003 17:30:08   AWindley
//Initial Revision

using System;

namespace TransportDirect.UserPortal.Resource
{
	/// <summary>
	/// The Ikey interface is implemented by all type specific Key classes
	/// contained in Keys.cs
	/// </summary>
	
	public interface IKey 
	{
		// When implemented in Key classes this ID attribute is unique for each data type. 
		// This prevents keys of different types but the same name overwriting each other.
		string ID{get;}
	}
}
