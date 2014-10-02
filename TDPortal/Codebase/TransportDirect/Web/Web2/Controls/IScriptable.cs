// *********************************************** 
// NAME                 : IScriptable.cs 
// AUTHOR               : James Broome
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Interface to be fulfilled by all Scriptable controls
//						  to ensure a consistent behaviour.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/IScriptable.cs-arc  $
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
//   Rev 1.0   Apr 30 2004 13:39:48   jbroome
//Initial revision.


using System;using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Interface to be fulfilled by all Scriptable controls, to ensure a consistent behaviour.
	/// When EnableClientScript is set to true the control should emit a JavaScript related attribute
	/// corresponding to its Action property. The ScriptName property indicates to the page the name 
	/// of the script to register
	/// </summary>
	public interface IScriptable
	{
		/// <summary>
		/// Property specifying the associated JavaScript name.
		/// </summary>
		string ScriptName { get; set; }

		/// <summary>
		/// Property specifying the value of the JavaScript related attribute for the control
		/// </summary>
		string Action { get; set; }

		/// <summary>
		/// Used to inform the control if it should emit its JavaScript related attribute
		/// </summary>
		/// <returns></returns>
		bool EnableClientScript { get; set; }
	}
}

