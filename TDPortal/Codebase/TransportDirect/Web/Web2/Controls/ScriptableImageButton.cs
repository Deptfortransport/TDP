// *********************************************** 
// NAME                 : ScriptableImageButton.cs 
// AUTHOR               : James Broome
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Control that inherits from ImageButton, but adds 
//						  support for JavaScript fucntionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ScriptableImageButton.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:46   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:27:20   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Apr 30 2004 13:39:02   jbroome
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for ScriptableImageButton.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ScriptableImageButton runat=server></{0}:ScriptableImageButton>")]
	public class ScriptableImageButton : ImageButton, IScriptable
	{
		private string scriptName = string.Empty;
		private string action = string.Empty;

		/// <summary>
		/// Implementation of IScriptable.ScriptName
		/// </summary>
		public string ScriptName
		{
			get {return this.scriptName;}
			set {this.scriptName = (value == null) ? string.Empty : value;}
		}

		/// <summary>
		/// Implementation of IScriptable.Action
		/// </summary>
		public string Action
		{
			get {return this.action;}
			set {this.action = (value == null) ? string.Empty : value;}
		}

		/// <summary>
		/// Implementation of IScriptable.EnableClientScript
		/// The informatin is simply stored in the ViewState.
		/// </summary>
		public bool EnableClientScript 
		{
			get 
			{
				object o = ViewState["EnableClientScript"];
				return((o == null) ? false : (bool)o);
			}
			set {ViewState["EnableClientScript"] = value;}
		}

		/// <summary>
		/// Override the base class method so that if appropriate an onclick attribute is written
		/// </summary>
		/// <param name="writer"></param>
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{      
			base.AddAttributesToRender(writer);   
			if (EnableClientScript && ScriptName != string.Empty && Action != string.Empty) 
			{
				writer.AddAttribute("onclick", Action	);
			}
		}

	}
}
