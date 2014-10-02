//******************************************************************************
//NAME			: TDLinkButton.cs
//AUTHOR		: Marcus Tillett
//DATE CREATED	: 17/11/2005
//DESCRIPTION	: Extended LinkButton control (to be used instead of LinkButton)
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDLinkButton.cs-arc  $
//
//   Rev 1.4   Nov 15 2009 11:08:18   mmodi
//Updated to allow javascript to be attached to the hyperlink
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Oct 13 2008 11:17:46   pscott
//Updated for xhtml compliance
//
//   Rev 1.2.1.0   Sep 15 2008 11:09:34   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:08   mturner
//Initial revision.
//
//   Rev 1.0   Nov 29 2005 18:35:52   AViitanen
//Initial revision.

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Extended LinkButton control (to be used instead of LinkButton)
	/// </summary>
	[ToolboxData("<{0}:TDLinkButton runat=server>LinkButton</{0}:TDLinkButton>")]
	public class TDLinkButton : LinkButton
	{
        private string clientSideJavascript = string.Empty;

		/// <summary>
		/// Empty constructor
		/// </summary>
		public TDLinkButton()
		{
		}
		/// <summary>
		/// Modified version of the AddAttributesToRender from the LinkButton
		/// control to render Href as "#" and JavaScript logic to Onclick attribute
		/// </summary>
		/// <param name="writer">HtmlTextWriter</param>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Enabled && (this.Page != null))
			{
				if (!string.IsNullOrEmpty(clientSideJavascript))
                {
                    this.OnClientClick = clientSideJavascript;
                }
                else
                {
                    string onClickScript = "";

                    if (this.CausesValidation && (this.Page.Validators.Count > 0))
                    {
                        onClickScript = GetClientValidatedPostback(this);
                    }
                    else
                    {
                        onClickScript = this.Page.ClientScript.GetPostBackEventReference(this, "");
                    }
                    //ensure that false is returned from the onclick JavaScript
                    onClickScript += ";return false";

                    //add the Onclick attribute, with the appropriate JavaScript
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClickScript);
                }
			}
			//call the base method to add other attributes
			//this also renders another Href (which is ignored by the brwoser)
			base.AddAttributesToRender(writer);
		}
		/// <summary>
		/// Copy of the internal method in the System.Web.UI.Util class
		/// </summary>
		/// <param name="control">Current control</param>
		/// <returns>JavaScript function</returns>
		protected virtual string GetClientValidatedPostback(Control control)
		{
			string controlID = control.Page.ClientScript.GetPostBackEventReference(control,"");
			return ("{if (typeof(Page_ClientValidate) != 'function' ||  " +
				"Page_ClientValidate()) " + controlID + "} ");
		}

        /// <summary>
        /// Adds OnClientClick javascript to fire when the LinkButton is clicked. 
        /// This will prevent a page postback if set.
        /// </summary>
        public string ClientSideJavascript
        {
            get { return clientSideJavascript; }
            set { clientSideJavascript = value; }
        }
	}
}
