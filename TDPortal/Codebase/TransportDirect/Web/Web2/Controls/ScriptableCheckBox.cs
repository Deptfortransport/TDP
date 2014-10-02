// *********************************************** 
// NAME                 : ScriptableCheckBox.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Control that inherits from CheckBox, but adds 
//						  support for JavaScript functionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ScriptableCheckBox.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:44   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:27:18   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 20 2004 12:09:36   jgeorge
//IR1256
//
//   Rev 1.2   Aug 02 2004 11:10:08   jbroome
//IR11252 - Now implements IPostBackDataHandler interface to handle post back data changes correctly. 
//
//   Rev 1.1   Jun 09 2004 16:57:42   jgeorge
//Modified rendering method

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for ScriptableCheckBox.
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ScriptableCheckBox runat=\"server\"></{0}:ScriptableCheckBox>")]
	public class ScriptableCheckBox : CheckBox, IScriptable, IPostBackDataHandler
	{
		private string scriptName = string.Empty;
		private string action = string.Empty;
		private bool enableClientScript = false;
		private string thisValue = string.Empty;
		private string cssClassEnabled = string.Empty;
		private string cssClassDisabled = string.Empty;

		#region IScriptable implementation

		/// <summary>
		/// Implementation of IScriptable.ScriptName
		/// </summary>
		public string ScriptName
		{
			get {return this.scriptName;}
			set {this.scriptName = value;}
		}

		/// <summary>
		/// Implementation of IScriptable.Action
		/// </summary>
		public string Action
		{
			get {return this.action;}
			set {this.action = value;}
		}

		/// <summary>
		/// Implementation of IScriptable.EnableClientScript
		/// The informatin is simply stored in the ViewState.
		/// </summary>
		public bool EnableClientScript 
		{
			get {return this.enableClientScript;}
			set {this.enableClientScript = value;}
		}

		#endregion

		#region Additional properties

		/// <summary>
		/// The standard checkbox doesn't allow use of the HTML "value" attribute, so this
		/// is included here.
		/// </summary>
		public string Value
		{
			get {return this.thisValue;}
			set {this.thisValue = value;}
		}

		/// <summary>
		/// Sets the Css Class that will be used for the checkbox when it is enabled
		/// </summary>
		public string CssClassEnabled
		{
			get {return this.cssClassEnabled;}
			set {this.cssClassEnabled = value;}
		}

		/// <summary>
		/// Sets the Css class that will be used for the checkbox when it is disabled
		/// </summary>
		public string CssClassDisabled
		{
			get {return this.cssClassDisabled;}
			set {this.cssClassDisabled = value;}
		}
		#endregion

		#region Overridden methods

		/// <summary>
		/// Initialization steps that are required to create and set up an 
		/// instance of the control.
		/// </summary>
		/// <param name="e">EventArgs object that contains the event data</param>
		protected override void OnInit(EventArgs e)
		{
			if (Page != null)
				Page.RegisterRequiresPostBack(this);
			base.OnInit(e);
		}

		/// <summary>
		/// Renders the control. Checkboxes are rendered as a checkbox and then a label, both with a Class attribute
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			// Render the checkbox
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Value);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Enabled ? CssClassEnabled : CssClassDisabled);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			if (this.Checked)
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");

			if (!this.Enabled)
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");

			if (EnableClientScript && ScriptName != string.Empty && Action != string.Empty) 
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Action	);

			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();

			// Render the label
			writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Enabled ? CssClassEnabled : CssClassDisabled);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		/// <summary>
		/// Loads the properties from the view state
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				
				ScriptName = myState[1] == null ? string.Empty : (string)myState[1];
				Action = myState[2] == null ? string.Empty : (string)myState[2];
				EnableClientScript = myState[3] == null ? false : (bool)myState[3];
				Value = myState[4] == null ? string.Empty : (string)myState[4];
				CssClassEnabled = myState[5] == null ? string.Empty : (string)myState[5];
				CssClassDisabled = myState[6] == null ? string.Empty : (string)myState[6];
			}
		}

		/// <summary>
		/// Saves the properties in the ViewState
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			return new object[] { base.SaveViewState(), ScriptName, Action, EnableClientScript, Value, CssClassEnabled, CssClassDisabled };
		}

		/// <summary>
		/// Processes post back data for an ASP.NET server control
		/// </summary>
		/// <param name="postDataKey">The key identifier for the control</param>
		/// <param name="postCollection">The collection of all incoming name values</param>
		/// <returns>True if the server control's state changes as a result of the post back; otherwise false</returns>
		new public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			bool oldValue = Checked;
			bool newValue = (postCollection[postDataKey] != null);
			if (oldValue != newValue)
			{
				Checked = newValue;
				return true;
			}
			else return false;
		}

		/// <summary>
		/// Change events for the server control that implements the  
		/// IPostBackDataHandler interface are raised from this method.
		/// </summary>
		new public virtual void RaisePostDataChangedEvent() 
		{
			this.OnCheckedChanged(EventArgs.Empty);
		}

		#endregion
	}
}
