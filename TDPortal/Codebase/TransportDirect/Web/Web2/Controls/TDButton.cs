// *********************************************** 
// NAME                 : TDButton.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 08/09/2005
// DESCRIPTION			: A web custom control for a TD specific button inheriting from System.Web.UI.WebControls.Button
//						  The control will replace the ImageButton buttons currently in use to give a lighter web page.
//						  The developer can tailor the look of the button by setting the CssClass and / or the CssClassMouseOver
//						  properties to a button style in the setup.css stylesheet (new definitions can be added as required).
//						  If these properties are not set, a default style will be used (this should be the norm).
// NOTES				:
/*
* USING THE BUTTON CONTROL ON A PAGE
*
*   TO ADD THE CONTROL
*   The control cannot be added by drag and drop. Edit the HTML directly by adding the following lines in the appropriate places:
*   <%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
*   <link href="/web2/Styles/setup.css" type="text/css" rel="stylesheet">
*   <cc1:TDButton id="TDButton1" runat="server" text="TDButton"></cc1:TDButton>
*
*   SETTING BUTTON PROPERTIES
*   Accept the default TD button styles for the button or tailor the styles by setting the CssClass and CssClassMouseOver properties.
*   These can be set at design time:
*   - in the design view, using the control property sheet (you can then see the button style at design time);
*   - in the HTML view by editting the HTML tags directly;
*   - in the code behind, in the OnInit or OnLoad of the host page.
*
*
* STYLESHEET NOTES
*
*	There are 3 default styles for the generic TransportDirect button:
*	.TDButtonDefault			- default state
*	.TDButtonDefault:hover		- state on hover for browsers that support the hover style
*	.TDButtonDefaultMouseOver	- state on hover for browsers that don't support the hover style but are running Javascript
* 
*	The TDButtonDefault:hover and TDButtonDefaultMouseOver styles are identical as they represent the same 'hover over' style
*	but for different browser capabilities.
*
*   The TDButton control picks up the default styles at runtime from the properties table therefore the script DUD0260_AddTDButtonDefaultStyles.sql
*   must have been run.
* 
*	TO ADD A STYLE
*	The default styles can be overridden by the developer when adding the TDButton control to the page.
*	Set the exposed CssClass and CssClassMouseOver properties in the page OnInit methods (click events should also go in here)
*	TDButtonStyle1 and TDButtonStyle2 have been added as examples of replacement styles. If you are adding a style to be
*	applied on hover remember to include a "MouseOver" style in addition to the ":hover" style to cover all browser capabilities.
*
*   Note that a style can be overriden at the design by specifying a style attribute eg if a button needs to be a little
*   bigger than the default size you can add style="font-size:1.2em" (say). This overrides both normal and hover styles.
*/
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDButton.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:08   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.0   Jan 10 2006 15:27:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Oct 17 2005 16:32:26   rhopkins
//TD089 ES020 Image Button Replacement - Improve robustness of handling Action attribute
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.7   Oct 10 2005 18:22:16   rhopkins
//Add IScriptable interface to allow for Clientside Javascript.
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.6   Oct 06 2005 10:12:52   RGriffith
//Minor Alteration to Font tag for standard "quotation" marks 
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.5   Oct 05 2005 17:05:14   RGriffith
//Update to add a font tag to give standard default size to all buttons
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.4   Sep 13 2005 18:24:46   pcross
//Update after code review update!
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//
//   Rev 1.3   Sep 13 2005 17:29:10   pcross
//Update after code review
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//
//   Rev 1.2   Sep 09 2005 14:25:42   pcross
//FxCop updates
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//
//   Rev 1.1   Sep 09 2005 08:33:00   pcross
//Updates to notes on use
//Resolution for 2766: DEL 8 Stream: Replacement of imagebuttons with new custom control
//
//   Rev 1.0   Sep 08 2005 13:47:28   pcross
//Initial revision.

using System;
using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for TDButton.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	[DefaultProperty("Text"), ToolboxData("<{0}:TDButton runat=server></{0}:TDButton>")]
	public class TDButton : System.Web.UI.WebControls.Button, IScriptable
	{
		#region Private member variables

		// Mouse-over style to be used by java script event.
		private string cssClassMouseOver;
		private string scriptName = string.Empty;
		private string action = string.Empty;

		#endregion

		#region Initialisation


		/// <summary>
		/// Class initialisation. Default styles are assigned (if they haven't been assigned
		/// in the HTML code using this class)
		/// </summary>
		protected override void OnInit(EventArgs e)
		{

			// Set default styles from settings in the properties table

			// The style classes set here must be included in a linked stylesheet on the
			// control's host page
			
			if (this.CssClass == null || this.CssClass.Length == 0)
				CssClass = Properties.Current["WebControlLibrary.TDButton.DefaultStyle"].ToString();

			if (this.cssClassMouseOver == null || this.cssClassMouseOver.Length == 0)
				cssClassMouseOver = Properties.Current["WebControlLibrary.TDButton.DefaultMouseOverStyle"].ToString();

			base.OnInit (e);
		}



		#endregion

		#region Overrides

		/// <summary>
		/// OnPreRender event handler.
		/// Assign JavaScript event atributes.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// Assign javascript event attributes
			Attributes.Add("onmouseover",String.Format(CultureInfo.InvariantCulture, "this.className='{0}'",cssClassMouseOver));
			Attributes.Add("onmouseout",String.Format(CultureInfo.InvariantCulture, "this.className='{0}'",CssClass));

			if (EnableClientScript && ScriptName != string.Empty && Action != string.Empty) 
			{
				Attributes.Add("onclick", Action);
			}

			base.OnPreRender (e);
		}

		/// <summary>
		/// Render event handler.
		/// Adds a font tag to make button sizes consistent.
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<font size=\"2\">");
			base.Render (writer);
			writer.Write("</font>");
		}

		#endregion

		#region Properties

		/// <summary>
		/// Mouse-over style to be used by java script event.
		/// Read/write property.
		/// </summary>
		[Category("Appearance")]
		public string CssClassMouseOver
		{
			get {return cssClassMouseOver;}
			set {cssClassMouseOver = value;}
		}

		#endregion

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

			set
			{
				if (value == null)
				{
					this.action = string.Empty;
				}
				else
				{
					// Ensure that the JavaScript is properly terminated so that
					// the base class can append extra code if necessary
					if (value.EndsWith(";"))
					{
						this.action = value;
					}
					else
					{
						this.action = value + ";";
					}
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

		#endregion
	}
}
