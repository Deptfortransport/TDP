// *********************************************** 
// NAME                 : ScriptableHyperlink.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 16/05/2005
// DESCRIPTION			: Control that inherits from hyperlink, but adds 
//						  support for JavaScript functionality 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ScriptableHyperlink.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:46   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 16:13:42   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.1   Feb 10 2006 15:04:46   build
//Automatically merged from branch for stream3180
//
//   Rev 1.0.1.0   Dec 14 2005 16:07:58   RGriffith
//Added a NavigateUrl property to add an Href attribute if the value input = #. This prevents the .net framework resolving the url and adding extra data to the link
//
//   Rev 1.0   May 20 2005 09:47:20   rgeraghty
//Initial revision.


using System;
using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// The ScriptableHyperlink control inherits from hyperlink, but adds support 
	/// for JavaScript functionality.	
	/// </summary>

	[ToolboxData("<{0}:ScriptableHyperlink runat=\"server\"></{0}:ScriptableHyperlink>")]
	public class ScriptableHyperlink : HyperLink, IScriptable
	{
		private string scriptName = string.Empty;
		private string action = string.Empty;
		private string navigateURL = string.Empty;
	
		#region IScriptable implementation

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
		/// Implementation of Hyperlink.NavigateURL
		/// </summary>
		public new string NavigateUrl
		{
			get {return base.NavigateUrl;}
			set
			{
				// If the hyperlink is merely a # symbol then add an Href attribute rather than let the 
				// .Net framework resolve the URL locally to add extra unwanted data to the link
				if (value == "#")
				{
					this.Attributes.Add("href", value);
				}
				// Else set the hyperlink.NavigateUrl as normal
				else
				{
					base.NavigateUrl = value;
				}
			}
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
				writer.AddAttribute("onclick",Action);
			}
		}

		#endregion
	
	}
}
