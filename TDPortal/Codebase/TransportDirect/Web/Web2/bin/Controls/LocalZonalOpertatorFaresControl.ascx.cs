// *********************************************** 
// NAME                 : LocalZonalOperatorFaresControl.ascx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 13/03/2007
// DESCRIPTION			: Displays the external local zonal operators fares link
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocalZonalOpertatorFaresControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 17 2008 11:55:24   build
//Automatically merged from branch for stream0093
//
//   Rev 1.2.1.0   Aug 06 2008 11:18:20   apatel
//added text "(opens new window)" to hyperlinks opening in new window.
//Resolution for 5096: ArrivalBoard and DepartureBoard , and related sites -  labels missing "Opens new window" text
//
//   Rev 1.2   Mar 31 2008 13:21:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:00   mturner
//Initial revision.
//
//   Rev 1.3   Apr 03 2007 10:20:28   dsawe
//updated for local zonal services phase 2 & 3
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2   Mar 21 2007 15:18:26   dsawe
//added for printer friendly pages
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.1   Mar 14 2007 18:38:38   dsawe
//Updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0   Mar 14 2007 09:52:00   asinclair
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.ZonalServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.JourneyPlanning.CJPInterface;
	
	
	/// <summary>
	///		Summary description for LocalZonalOpertatorFaresControl.
	/// </summary>
	public partial class LocalZonalOpertatorFaresControl : TDPrintableUserControl
	{
		protected System.Web.UI.WebControls.Label labelCheckFor;
		protected System.Web.UI.WebControls.HyperLink operatorFaresHyperLink;
		private PublicJourneyDetail journeyDetail = null;
		private ArrayList linkArray = new ArrayList();
		private bool isEmpty = true;

		protected void Page_Load(object sender, System.EventArgs e)
		{ 
			// Set the resource manager
			this.LocalResourceManager  = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.operatorFaresLinkRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.opertorFaresLinkRepeater_ItemCreated);

		}
		#endregion
		
		protected override void OnPreRender( EventArgs e )
		{
			if(linkArray != null)
			{
				operatorFaresLinkRepeater.DataSource = linkArray;
				operatorFaresLinkRepeater.DataBind();
			}
			if(!isEmpty)
			{
				base.OnPreRender (e);
			}
			
		}

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


		private bool BuildLocalOperatorFaresLink()
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
			// For each unique operator code gathered, couple with mode, and regionId -> find Url (if exists)
			ModeType mode = journeyDetail.Mode;
			string OperatorFaresLinkUrl = string.Empty;

            string region = journeyDetail.Region;

			// Get NetworkMapLinks instance from service discovery
			IOperatorCatalogue currentOperatorCatalogue = OperatorCatalogue.Current;
			foreach (string operatorCode in operatorArray)
			{
				// Get the URL based on the current mode and operator
				OperatorFaresLinkUrl = currentOperatorCatalogue.GetZonalOperatorFaresLinks(mode.ToString(), operatorCode, region);

                // If there is a link returned then add to the linkArray (if unique)
				if (OperatorFaresLinkUrl.Length > 0)
					AddUniqueArrayListEntry(OperatorFaresLinkUrl, linkArray);
			}

			// Check if we have got any Urls returned to display
			if (linkArray.Count == 0)
				itemsAdded = false;
			else
				itemsAdded = true;

			return itemsAdded;
		}

		private void opertorFaresLinkRepeater_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			// Each repeater item corresponds to a url
			// Update the controls in the repeater accordingly
			Label labelTitle = (Label)e.Item.FindControl("labelCheckFor");
			HyperLink linkOperatorFares = (HyperLink)e.Item.FindControl("operatorFaresHyperLink");

			// Set the label title text
			labelTitle.Text = GetResource("LocalZonalFares.Label.CheckFor");
            linkOperatorFares.Text = string.Format("{0} {1}", GetResource("LocalZonalOperatorFaresLinks.Hyperlink.Text"), GetResource("langStrings", "ExternalLinks.OpensNewWindowText")); 
			
			// If in printer friendly mode then don't show as hyperlink
			if (!this.PrinterFriendly)
			{
				// Set the hyperlink control properties
				linkOperatorFares.NavigateUrl = e.Item.DataItem.ToString();
				linkOperatorFares.ToolTip = GetResource("OperatorLinks.Hyperlink.Title");
				linkOperatorFares.Target = "_blank";
			}
			else
			{
				// Use the label to output and hide the hyperlink
				labelTitle.Text += linkOperatorFares.Text;				
				linkOperatorFares.Visible = false;
			}

		}


		
		
		#region properties

		public PublicJourneyDetail JourneyDetail
		{
			get {return journeyDetail;}
			set 
			{
				journeyDetail = value;
				if(BuildLocalOperatorFaresLink())
				{
					isEmpty = false;
				}
			}
		}

		/// <summary>
		/// (read-only) Returns true if the control is to render links, false othewise
		/// </summary>
		public bool IsEmpty
		{
			get{ return isEmpty; }
		}

		#endregion
	
	}


}
