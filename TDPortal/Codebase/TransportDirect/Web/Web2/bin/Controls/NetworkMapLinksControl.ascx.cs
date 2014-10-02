// *********************************************** 
// NAME                 : NetworkMapLinksControl.ascx
// AUTHOR               : Paul Cross
// DATE CREATED         : 11/07/2005
// DESCRIPTION			: Shows a hyperlink to a defined external url for a web page
//						  showing the map associated with the given mode and operator.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/NetworkMapLinksControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 17 2008 11:55:24   build
//Automatically merged from branch for stream0093
//
//   Rev 1.2.1.0   Aug 06 2008 10:11:30   apatel
//added text for newwork map hyperlink opening in new window
//Resolution for 5096: ArrivalBoard and DepartureBoard , and related sites -  labels missing "Opens new window" text
//
//   Rev 1.2   Mar 31 2008 13:22:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:44   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:26:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Aug 03 2005 16:32:44   pcross
//Minor change to tooltip
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jul 26 2005 18:18:36   pcross
//Minor correction after unit test run
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 25 2005 21:03:10   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 25 2005 15:26:34   pcross
//Updated to handle possibility of multiple services resulting in multiple netowrk map links
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 18 2005 16:24:44   pcross
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Globalization;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.Common.ServiceDiscovery;

	[System.Runtime.InteropServices.ComVisible(false)]

	/// <summary>
	///		NetworkMapLinksControl shows a link (url) to an external website.
	///		The url is gained from the ExternalLinks table for a given operator and mode of travel
	/// </summary>
	public partial class NetworkMapLinksControl : TDPrintableUserControl
	{

		private PublicJourneyDetail journeyDetail;
		private ArrayList linkArray = new ArrayList();
		private string linkIntro;
		private string linkNetworkMapText;
		private string linkNetworkMapToolTip;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set the resource manager
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
		/// Event handler for the prerender event.
		/// Uses the property values set for the control to lookup the associated url and display.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{
			// Build an array of urls - if there are any returned then bind to repeater,
			// else make the control invisible
			if (BuildLinkArray())
			{
				GetResourceStrings();
				networkLinkRepeater.DataSource = linkArray;
				networkLinkRepeater.DataBind();
			}
			else
				this.Visible = false;
			
		}

		#region Properties

		public PublicJourneyDetail JourneyDetail
		{
			get {return journeyDetail;}
			set {journeyDetail = value;}
		}

		#endregion

		#region Private Methods
		
		/// <summary>
		/// For each service in the leg, builds an array of network map link Urls
		/// </summary>
		private bool BuildLinkArray()
		{
			// Note whether array items are added
			bool itemsAdded = false;
			
			// For each service in the leg, add the operator code to an operatorArray
			// (where the operator code has not already been added)
			ArrayList operatorArray = new ArrayList();

			// Check for services
			if (journeyDetail.Services != null)
			{
				foreach (JourneyControl.ServiceDetails service in journeyDetail.Services)
					if (service.OperatorCode != null)
						AddUniqueArrayListEntry(service.OperatorCode, operatorArray);
			}
			else
			{
				return itemsAdded;
			}
			
			// Check if we have got any service details
			if (operatorArray.Count == 0)
				return itemsAdded;

			// For each unique operator code gathered, couple with mode and find Url (if exists)
			ModeType mode = journeyDetail.Mode;
			string nwMapLinkUrl = string.Empty;

			// Get NetworkMapLinks instance from service discovery
			INetworkMapLinks nwMapLinks = NetworkMapLinks.Current;

			foreach (string operatorCode in operatorArray)
			{
				// Get the URL based on the current mode and operator
				nwMapLinkUrl = nwMapLinks.GetURL(mode, operatorCode);

				// If there is a link returned then add to the linkArray (if unique)
				if (nwMapLinkUrl.Length > 0)
					AddUniqueArrayListEntry(nwMapLinkUrl, linkArray);
			}

			// Check if we have got any Urls returned to display
			if (linkArray.Count == 0)
				itemsAdded = false;
			else
				itemsAdded = true;

			return itemsAdded;
		}

		/// <summary>
		/// Adds a text entry to an ArrayList array but only when it doesn't already exist.
		/// </summary>
		/// <param name="newEntry"></param>
		/// <param name="theArray"></param>
		private static void AddUniqueArrayListEntry(string newEntry, ArrayList theArray)
		{
			bool alreadyEntered = false;
			
			// Compare existing values with value to be entered.
			foreach (string existingEntry in theArray)
			{
				if (existingEntry == newEntry)
				{
					alreadyEntered = true;
					break;
				}

			}

			// Add the entry if not there already
			if (alreadyEntered)
			{
				return;
			}
			else
			{
				theArray.Add(newEntry);
			}
		}

		/// <summary>
		/// Get the resource strings that may be used multiple times.
		/// </summary>
		private void GetResourceStrings()
		{
			linkIntro = GetResource("NetworkMapLinks.LinkIntro");
			linkNetworkMapText = GetResource("NetworkMapLinks.Hyperlink.Text");
			linkNetworkMapToolTip = GetResource("NetworkMapLinks.Hyperlink.Title");
		}

		/// <summary>
		/// Wire up events to handlers
		/// </summary>
		private void AddEventHandlers()
		{
			networkLinkRepeater.ItemCreated += new RepeaterItemEventHandler(networkLinkRepeater_ItemCreated);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Event handler for repeater ItemCreated event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkLinkRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			// Each repeater item corresponds to a url
			// Update the controls in the repeater accordingly
			Label labelTitle = (Label)e.Item.FindControl("labelTitle");
			HyperLink linkNetworkMap = (HyperLink)e.Item.FindControl("linkNetworkMap");

			// Set the label title text
			labelTitle.Text = linkIntro;

			string stringMode = journeyDetail.Mode.ToString().ToLower(CultureInfo.InvariantCulture);	// string version of the mode property

			// If in printer friendly mode then don't show as hyperlink
			if (!this.PrinterFriendly)
			{
				// Set the hyperlink control properties
				linkNetworkMap.NavigateUrl = e.Item.DataItem.ToString();
                linkNetworkMap.Text = string.Format("{0} {1}", String.Format(CultureInfo.InvariantCulture, linkNetworkMapText, stringMode), GetResource("langStrings", "ExternalLinks.OpensNewWindowText")); 
				linkNetworkMap.ToolTip = linkNetworkMapToolTip;
				linkNetworkMap.Target = "_blank";
			}
			else
			{
				// Use the label to output and hide the hyperlink
				labelTitle.Text += String.Format(CultureInfo.InvariantCulture, linkNetworkMapText, stringMode);
				linkNetworkMap.Visible = false;
			}
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			AddEventHandlers();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
