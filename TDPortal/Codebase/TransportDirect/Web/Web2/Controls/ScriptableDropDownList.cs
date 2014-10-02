// *********************************************** 
// NAME                 : ScriptableDropDownList.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Control that inherits from DropDownList, but adds 
//						  support for JavaScript fucntionality
// ************************************************ 
// $Log:

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for ScriptableListBox.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ScriptableListBox runat=server></{0}:ScriptableListBox>")]
	public class ScriptableDropDownList: DropDownList, IScriptable
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
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, Action	);
			}
		}

	}
}
