// *************************************************************** 
// NAME                 : ISingleWindow.cs 
// AUTHOR               : Peter Norell 
// DATE CREATED         : 30/08/2003 
// DESCRIPTION			: 
// **************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ISingleWindow.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:02   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:16:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:25:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Sep 30 2003 15:22:12   PNorell
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for ISingleWindow.
	/// </summary>
	public interface ISingleWindow
	{
		bool IsOpen { get; }
		void Open();
		void Close();
	}
}
