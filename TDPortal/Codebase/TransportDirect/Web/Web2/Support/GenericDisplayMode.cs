// *************************************************************************** 
// NAME                 : GenericDisplayMode.cs
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 09/07/2004 
// DESCRIPTION			: This enumerator holds values for the display behaviour
// of user controls
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Support/GenericDisplayMode.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:27:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:52   mturner
//Initial revision.
//
//   Rev 1.1   Jul 13 2004 09:57:50   jgeorge
//Removed "Empty" value
//
//   Rev 1.0   Jul 09 2004 12:03:04   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.Web.Support
{
	/// <summary>
	/// Summary description for GenericDisplayMode.
	/// </summary>
	public enum GenericDisplayMode
	{
		Normal, // for initial text input
		ReadOnly, // for display of a valid value
		Ambiguity // errors highlighted and input modifiable
	}
}
