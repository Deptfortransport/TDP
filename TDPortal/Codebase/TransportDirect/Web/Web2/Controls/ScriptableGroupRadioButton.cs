// *********************************************** 
// NAME                 : ScriptableGroupRadioButton.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 11/01/2005
// DESCRIPTION			: Control that inherits from radiobutton, but adds 
//						  support for JavaScript functionality and extended 
//						  ability to be used in groups.	
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ScriptableGroupRadioButton.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:44   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 16:13:38   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.3   Jan 05 2006 17:45:56   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Dec 16 2005 12:30:10   jbroome
//Exposed radio button value as public property
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Jan 18 2005 16:18:28   rgeraghty
//Updated with viewstate changes
//
//   Rev 1.0   Jan 11 2005 14:16:52   rgeraghty
//Initial revision.


using System;
using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// The ScriptableGroupRadioButton control inherits from radiobutton, but adds support 
	/// for JavaScript functionality and extended ability to be used in groups.	
	/// </summary>

	[ToolboxData("<{0}:ScriptableGroupRadioButton runat=\"server\"></{0}:ScriptableGroupRadioButton>")]
	public class ScriptableGroupRadioButton : RadioButton, IScriptable, IPostBackDataHandler
	{
		private string scriptName = string.Empty;
		private string action = string.Empty;
		private bool enableClientScript = false;
	
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

		/// <summary>
		/// Read only property
		/// Returns the value attribute of the radiobutton
		/// </summary>
		public string GetValue
		{
			get {return Attributes["value"];}
		}


		#endregion

		#region Additional properties


		/// <summary>
		/// Returns the value of the radiobutton
		/// concatenated with the unique id of the 
		/// control. This ensures a unique value per page.
		/// </summary>
		private string UniqueValue
		{
			get
			{
				string val = Attributes["value"];
				if(val == null)
					val = UniqueID;
				else
					val = UniqueID + "_" + val;
				return val;
			}
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
			// Render the radiobutton

			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");			
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.GroupName);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.UniqueValue);

			if(this.Checked)
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			if(!this.Enabled)
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");

			if (EnableClientScript && ScriptName != string.Empty && Action != string.Empty) 
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Action	);

			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();		
		}

		/// <summary>
		/// Processes post back data for an ASP.NET server control
		/// </summary>
		/// <param name="postDataKey">The key identifier for the control</param>
		/// <param name="postCollection">The collection of all incoming name values</param>
		/// <returns>True if the server control's state changes as a result of the post back; otherwise false</returns>
		new public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			bool result = false;
			string value = postCollection[GroupName];
			if((value != null) && (value == UniqueValue))
			{
				if(!Checked)
				{
					Checked = true;
					result = true;
				}
			}
			else
			{
				if(Checked)
					Checked = false;
			}
			return result;

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
			}
		}

		/// <summary>
		/// Saves the properties in the ViewState
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			return new object[] { base.SaveViewState(), ScriptName, Action, EnableClientScript };
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
