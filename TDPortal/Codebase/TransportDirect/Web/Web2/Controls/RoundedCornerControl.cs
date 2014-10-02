// *********************************************** 
// NAME                 : RoundedCornerControl.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 25/11/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RoundedCornerControl.cs-arc  $
//
//   Rev 1.3   Oct 03 2008 11:58:22   mturner
//Updated for XHTML compliance
//
//   Rev 1.2.1.0   Sep 15 2008 11:09:36   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:22:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:36   mturner
//Initial revision.
//
//   Rev 1.0   Nov 25 2005 15:02:58   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Options for corners to be rounded by the RoundedCornerControl
	/// </summary>
	[Flags]
	public enum RoundedCornerOption
	{
		/// <summary>No corners will be rounded</summary>
		None			= 0x0000,

		/// <summary>The top left corner will be rounded</summary>
		TopLeft			= 0x0001,

		/// <summary>The top right corner will be rounded</summary>
		TopRight		= 0x0002,

		/// <summary>The bottom left corner will be rounded</summary>
		BottomLeft		= 0x0004,

		/// <summary>The bottom right corner will be rounded</summary>
		BottomRight		= 0x0008,

		/// <summary>The top left and top right corners will be rounded</summary>
		Top				= TopLeft | TopRight,

		/// <summary>The bottom left and bottom right corners will be rounded</summary>
		Bottom			= BottomLeft | BottomRight,

		/// <summary>The top left and bottom left corners will be rounded</summary>
		Left			= TopLeft | BottomLeft,

		/// <summary>The top right and bottom right corners will be rounded</summary>
		Right			= TopRight | BottomRight,

		/// <summary>All corners will be rounded</summary>
		All				= Top | Bottom
	}

	/// <summary>
	/// Provides a panel control which will render with rounded corners if Javascript is enabled.
	/// </summary>
	/// <remarks>
	/// <p>This control inherits from Panel to make life simple - it means we don't need a Designer,
	/// and all the basic stuff is taken care of. However, it renders its own HTML - see the 
	/// <see cref="RoundedCornerControl.Render">Render</see> method for more information.
	/// method for more information.</p>
	/// <p>This control requires that an associated Css class is defined in a stylesheet. As a 
	/// minimum, the class should set the padding-top and padding-bottom values to 5px, to 
	/// prevent resizing when Javascript is disabled.</p>
	/// </remarks>
	[DefaultProperty("Corners"), ToolboxData("<{0}:RoundedCornerControl runat=server></{0}:RoundedCornerControl>"), ComVisible(false)]
	public class RoundedCornerControl : Panel
	{
		const string ScriptName = "Nifty";
		const string StartupScript = "<script language=\"javascript\" type=\"text/javascript\"> if(NiftyCheck()) Rounded( \"div.{0}\", \"{1}\", \"{2}\", \"{3}\", \"smooth\"); </script>";

		RoundedCornerOption corners;
		Color outerColour;
		Color innerColour;

		/// <summary>
		/// Use to specify the corners that will be rounded.
		/// </summary>
		public RoundedCornerOption Corners
		{
			get { return corners; }
			set { corners = value; }
		}

		/// <summary>
		/// Gets or sets the colour that will be used for the outer part of the rounded corner.
		/// </summary>
		public Color OuterColour
		{
			get { return outerColour; }
			set { outerColour = value; }
		}

		/// <summary>
		/// Gets or sets the colour that will be used for the inner part of the rounded corner.
		/// </summary>
		public Color InnerColour
		{
			get { return innerColour; }
			set { innerColour = value; }
		}

		/// <summary>
		/// Returns the string used to pass to the nifty method, specifying which corners to round.
		/// </summary>
		/// <returns></returns>
		private string GetNiftyCornersValue()
		{
			if (corners == RoundedCornerOption.None)
				return string.Empty;
			else if (corners == RoundedCornerOption.All)
				return "all";
			else if (corners == RoundedCornerOption.Top)
				return "top";
			else if (corners == RoundedCornerOption.Bottom)
				return "bottom";
			else
			{
				string result = string.Empty;
				if ((corners & RoundedCornerOption.TopLeft) == RoundedCornerOption.TopLeft)
					result += "tl,";
				if ((corners & RoundedCornerOption.TopRight) == RoundedCornerOption.TopRight)
					result += "tr,";
				if ((corners & RoundedCornerOption.BottomLeft) == RoundedCornerOption.BottomLeft)
					result += "bl,";
				if ((corners & RoundedCornerOption.BottomRight) == RoundedCornerOption.BottomRight)
					result += "br,";
				if (result.Length > 0)
					result = result.Substring(0, result.Length - 1);
				return result;
			}
		}

		/// <summary>
		/// Converts a colour into its corresponding RGB hex value. E.g. white is #ffffff and
		/// black is #0
		/// </summary>
		/// <param name="color">The colour to convert.</param>
		/// <returns>The hex string for the colour.</returns>
		private string GetHexValue(Color color)
		{
			// Get the rgb value (need to discard the alpha value)
			int rgb = color.ToArgb() & 0xffffff;

			// Convert to hex and return
			return "#" + rgb.ToString("x");
		}

		/// <summary>
		/// Adds the appropriate scripts to the page if necessary.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// Add the appropriate javascript to the page to invoke the Nifty corners functionality

			// Only bother adding the javascript if:
			// - some corners will be rounded;
			// - the control is on a TDPage
			// - JavaScript is enabled.
			TDPage thePage = this.Page as TDPage;

			if (Corners != RoundedCornerOption.None && thePage != null && thePage.IsJavascriptEnabled)
			{
				// Register the Nifty.js script, that actually does the work.
				if (!thePage.ClientScript.IsClientScriptBlockRegistered(ScriptName))
				{
					ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
					thePage.ClientScript.RegisterClientScriptBlock(typeof(RoundedCornerControl), ScriptName, repository.GetScript(ScriptName, thePage.JavascriptDom));
				}

				// Attach the startup script using the script name and CssClass as key.
				// This means that if more than one instance of the control is used on a page
				// with the same CssClass, they will only be initialised once.
				string startupKey = ScriptName + this.CssClass;
				if (!thePage.ClientScript.IsStartupScriptRegistered(startupKey))
				{
					string script = string.Format(StartupScript, this.CssClass, GetNiftyCornersValue(), GetHexValue(OuterColour), GetHexValue(InnerColour) );
					thePage.ClientScript.RegisterStartupScript(typeof(RoundedCornerControl),startupKey, script);
				}
			}
		}

		/// <summary>
		/// Overridden render method, to ensure that this control is always rendered as a DIV
		/// element.
		/// </summary>
		/// <param name="writer"></param>
		/// <remarks>
		/// Note that this is very restrictive, in that it will only use the following properties
		/// in the rendered HTML:
		/// <list type="bullet">
		///		<item>Id</item>
		///		<item>CssClass</item>
		/// </list>
		/// Any other properties are ignored, meaning that all styles should be specified using the
		/// CSS Class.
		/// </remarks>
		protected override void Render(HtmlTextWriter writer)
		{
			// Opening tag
			writer.WriteBeginTag("div");

			// Id
			writer.WriteAttribute("id", ClientID);

			// Css class
			writer.WriteAttribute("class", CssClass);

			// Close the opening tag
			writer.Write(HtmlTextWriter.TagRightChar);
			
			// Add child elements
			RenderChildren(writer);

			// End tag
			writer.WriteEndTag("div");
		}

	}
}
