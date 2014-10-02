// *********************************************** 
// NAME                 : MapLocationIconsSelectControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Control allowing to select the icons to display on the map.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapLocationIconsSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Jan 13 2009 11:40:46   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 20 2008 16:00:48   mturner
//Fix for IR4993 - MapIcons control obscures the footer control in IE6
//
//   Rev 1.2   Mar 31 2008 13:22:04   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 22 2008 11:19:00 apatel
//  Changed image and imagebutton controls to tdimage and tdimagebutton controls.
//
//  Rev Devfactory Feb 04 2008 08:37:00 apatel
//  aspx page design updated changed layout of controls - CCN 0427
//
//   Rev 1.0   Nov 08 2007 13:16:30   mturner
//Initial revision.
//
//   Rev 1.37   Nov 07 2006 11:20:26   dsawe
//made chages for replacing asp:panel with asp:table.
//Resolution for 4242: IE 7 compatibility
//
//   Rev 1.37   Oct 31 2006 16:23:29  dsawe
//compatibility changes for IE7
//
//   Rev 1.36   Oct 06 2006 15:58:14   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.35.1.3   Sep 29 2006 15:31:34   esevern
//Added check for Car parking functionality switched on before setting image urls, text etc accordingly. Car park icon should not  be visible if car parking function switched off
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.35.1.2   Aug 30 2006 14:53:32   esevern
//changes to ensure car park icon is visible on maps by default and according to user selection
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.35.1.1   Aug 29 2006 09:59:18   esevern
//Added property for selected car park checkbox
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.35.1.0   Jul 31 2006 16:07:22   MModi
//Added Car Park map symbol to key
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.35   Apr 04 2006 14:20:28   build
//Automatically merged from stream0034
//
//   Rev 1.34.1.0   Mar 29 2006 17:53:22   RWilby
//Updated for new map symbols.
//Resolution for 3715: Map Symbols
//
//   Rev 1.34   Feb 23 2006 16:12:50   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.33   Jan 18 2006 18:16:48   jbroome
//Updated display of Transport symbols planner for Visit Planner
//Resolution for 3464: VisitPlanner: Transport symbols remain on map key after being unchecked
//
//   Rev 1.32   Nov 03 2005 17:02:24   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.31.1.0   Oct 24 2005 18:02:26   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.31   Sep 20 2004 15:20:46   asinclair
//IR 1592 - Travel mode overlays
//
//   Rev 1.30   Jun 02 2004 16:38:28   passuied
//working version
//
//   Rev 1.29   May 21 2004 15:49:42   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.28   Mar 04 2004 09:41:50   asinclair
//Removed the Map Symbols label - this is now on the page and not the control.  For Del 5.2
//
//   Rev 1.27   Mar 01 2004 11:41:10   asinclair
//Hide B&B for Del 5.2
//
//   Rev 1.26   Feb 20 2004 09:42:12   asinclair
//Changes for Del 5.2 
//
//   Rev 1.25   Dec 12 2003 18:54:28   kcheung
//Del 5.1
//
//   Rev 1.24   Nov 05 2003 14:50:46   kcheung
//Updated commenting
//
//   Rev 1.23   Oct 31 2003 11:01:42   passuied
//Resolution of SCR 44 and 45.
//Refresh of POI on map everytime action is done on map and scale is close enough to display something.
//Resolution for 44: pois overlaid on the output map are not changed on zoom
//Resolution for 45: point of interest category changed but existing items on map are not cleared
//
//   Rev 1.22   Oct 28 2003 10:24:42   kcheung
//Fixed spelling of "accommodation" for FXCOP
//
//   Rev 1.21   Oct 22 2003 13:11:22   passuied
//esthetic change
//
//   Rev 1.20   Oct 21 2003 16:44:38   kcheung
//FXCOP spelling change
//
//   Rev 1.19   Oct 20 2003 12:06:02   kcheung
//Cosmetic changes for FXCOP
//
//   Rev 1.18   Oct 13 2003 12:04:54   kcheung
//Fixed ALT text
//
//   Rev 1.17   Oct 03 2003 15:05:06   passuied
//fixed bug
//
//   Rev 1.16   Oct 02 2003 11:30:02   passuied
//implemented iconSelection storage in sessionmanager
//
//   Rev 1.15   Sep 30 2003 15:35:08   kcheung
//Added blue radio button
//
//   Rev 1.14   Sep 26 2003 15:24:40   kcheung
//Fixed EnableOKButton to ensure that selected and unselected cannot be displayed at the same time,.
//
//   Rev 1.13   Sep 25 2003 18:36:26   passuied
//Added missing headers


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;
    using TransportDirect.UserPortal.Web.Code;
    using TransportDirect.Common.DatabaseInfrastructure.Content;

	/// <summary>
	///	Control to allow selection of STOPS and POINTXs on the map.
	/// </summary>
	public partial  class MapLocationIconsSelectControl : TDUserControl
	{
		#region Labels

		#endregion

		#region Image Buttons

		#endregion

		#region Tables
		#endregion

		#region Checkboxes

		









		#endregion
		
		#region Images (used for the keys)
		#endregion

		#region Image Urls of radio and checked box button
		
		// Checkbox
		private string imageUnchecked = String.Empty;
		private string imageChecked = String.Empty;

		// Radio button
		private string imageSelected = String.Empty;
		private string imageUnselected = String.Empty;
		#endregion
		
		// Flag used to determine whether the Transport symbols 
		// sub menu should be displayed
		bool transportPanelVisible;

       
		

		#region Page Load
		/// <summary>
		/// Page Load method - populates and initialise all controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise Image Urls of radio and checked box image buttons
			imageUnchecked = Global.tdResourceManager.GetString(
				"JourneyPlanner.imageButtonCheckBoxUnchecked",
				TDCultureInfo.CurrentUICulture);

			imageChecked = Global.tdResourceManager.GetString(
				"JourneyPlanner.imageButtonCheckBoxChecked",
				TDCultureInfo.CurrentUICulture);

			imageSelected = Global.tdResourceManager.GetString(
				"JourneyPlanner.imageButtonBlueRadioButtonChecked",
				TDCultureInfo.CurrentUICulture);

			imageUnselected = Global.tdResourceManager.GetString(
				"JourneyPlanner.imageButtonBlueRadioButtonUnchecked",
				TDCultureInfo.CurrentUICulture);

			Image31.Visible = FindCarParkHelper.CarParkingAvailable;
			CPK.Visible = FindCarParkHelper.CarParkingAvailable;

            lblSelectedCategory.Text = Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.lblSelectedCategory",
                    TDCultureInfo.CurrentUICulture);
                
                // Initialise the OK image button URL
                buttonOk.Text = GetResource("JourneyMapControl.JourneyPlanner.imageShowOnMap.Text");

                // Set alternate texts

                commandTransport.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandTransport.AlternateText", TDCultureInfo.CurrentUICulture);

                commandAccommodation.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandAccomodation.AlternateText", TDCultureInfo.CurrentUICulture);

                commandAttractions.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandAttractions.AlternateText", TDCultureInfo.CurrentUICulture);

                commandSport.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandSport.AlternateText", TDCultureInfo.CurrentUICulture);

                commandEducation.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandEducation.AlternateText", TDCultureInfo.CurrentUICulture);

                commandInfrastructure.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandInfrastructure.AlternateText", TDCultureInfo.CurrentUICulture);

                commandHealth.AlternateText = Global.tdResourceManager.GetString(
                    "JourneyMapControl.commandHealth.AlternateText", TDCultureInfo.CurrentUICulture);


                // Populate controls

                labelOnlyView.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelOnlyView",
                    TDCultureInfo.CurrentUICulture);

                labelSymbolsKey.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelSymbolsKey",
                    TDCultureInfo.CurrentUICulture);

                labelTransport.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelTransport",
                    TDCultureInfo.CurrentUICulture);

                labelAccommodation.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelAccommodationTitle",
                    TDCultureInfo.CurrentUICulture);

                labelSport.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelSportTitle",
                    TDCultureInfo.CurrentUICulture);

                labelAttractions.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelAttractionsTitle",
                    TDCultureInfo.CurrentUICulture);

                labelHealth.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelHealthTitle",
                    TDCultureInfo.CurrentUICulture);

                labelEducation.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelEducationTitle",
                    TDCultureInfo.CurrentUICulture);

                labelPublicBuildings.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelInfrastructureTitle",
                    TDCultureInfo.CurrentUICulture);


                labelTransportTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelTransportTitle",
                    TDCultureInfo.CurrentUICulture);

                labelAccommodationTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelAccommodationTitle",
                    TDCultureInfo.CurrentUICulture);

                labelAttractionsTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelAttractionsTitle",
                    TDCultureInfo.CurrentUICulture);

                labelEducationTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelEducationTitle",
                    TDCultureInfo.CurrentUICulture);

                labelInfrastructureTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelInfrastructureTitle",
                    TDCultureInfo.CurrentUICulture);
                labelSportTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelSportTitle",
                    TDCultureInfo.CurrentUICulture);
                labelHealthTitle.Text =
                    Global.tdResourceManager.GetString(
                    "MapLocationIconsSelectControl.labelHealthTitle",
                    TDCultureInfo.CurrentUICulture);
                if (!IsPostBack)
                {

                    // Set the Transport to the checked image (default)
                    commandTransport.ImageUrl = imageChecked;

                    // Check all checkboxes in the Transport Table
                    UpdateCheckBoxes(tableTransport, true);

                    // Set the image radio button for Accomodation to selected
                    commandAccommodation.ImageUrl = imageUnselected; ;

                    // Set the Table of accomodation to be false
                    tableAccommodation.Visible = false;

                    // Set all other image radio buttons to unselected
                    commandAttractions.ImageUrl = imageUnselected;
                    commandSport.ImageUrl = imageUnselected;
                    commandEducation.ImageUrl = imageUnselected;
                    commandInfrastructure.ImageUrl = imageUnselected;
                    commandHealth.ImageUrl = imageUnselected;
                }
                    
                    // Populate the checklists
                    if (FindCarParkHelper.CarParkingAvailable)
                    {
                        PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransport");

                        PopulateKeyImages(
                            tableTransport,
                            "MapLocationIconsSelectControl.imageUrlListTransport",
                            "MapLocationIconsSelectControl.imageListTransportAlternateText");
                    }
                    else
                    {
                        PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransportAlt");

                        PopulateKeyImages(
                            tableTransport,
                            "MapLocationIconsSelectControl.imageUrlListTransportAlt",
                            "MapLocationIconsSelectControl.imageListTransportAlternateTextAlt");
                    }
                    PopulateCheckList(tableAttractions, "MapLocationIconsSelectControl.checklistAttractions");
                    PopulateCheckList(tableAccommodation, "MapLocationIconsSelectControl.checklistAccommodation");
                    PopulateCheckList(tableSport, "MapLocationIconsSelectControl.checklistSport");
                    PopulateCheckList(tableEducation, "MapLocationIconsSelectControl.checklistEducation");
                    PopulateCheckList(tableInfrastructure, "MapLocationIconsSelectControl.checklistInfrastructure");
                    PopulateCheckList(tableHealth, "MapLocationIconsSelectControl.checklistHealth");

                    // Populate the key images				
                    PopulateKeyImages(
                        tableAttractions,
                        "MapLocationIconsSelectControl.imageUrlListAttractions",
                        "MapLocationIconsSelectControl.imageListAttractionsAlternateText");

                    PopulateKeyImages(
                        tableAccommodation,
                        "MapLocationIconsSelectControl.imageUrlListAccomodation",
                        "MapLocationIconsSelectControl.imageListAccomodationAlternateText");

                    PopulateKeyImages(
                        tableSport,
                        "MapLocationIconsSelectControl.imageUrlListSport",
                        "MapLocationIconsSelectControl.imageListSportAlternateText");

                    PopulateKeyImages(
                        tableEducation,
                        "MapLocationIconsSelectControl.imageUrlListEducation",
                        "MapLocationIconsSelectControl.imageListEducationAlternateText");

                    PopulateKeyImages(
                        tableInfrastructure,
                        "MapLocationIconsSelectControl.imageUrlListInfrastructure",
                        "MapLocationIconsSelectControl.imageListInfrastructureAlternateText");

                    PopulateKeyImages(
                        tableHealth,
                        "MapLocationIconsSelectControl.imageUrlListHealth",
                        "MapLocationIconsSelectControl.imageListHealthAlternateText");
               
			
			// Set the visibility of the Transport types Table, 
			// based on the current image N.B. this is a dummy checkbox
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
			if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
			{
				transportPanelVisible = true;
			}
			else
			{
				transportPanelVisible = false;
			}
		}
		#endregion

		/// <summary>
		/// Pre Render method
		/// Set visibilty of transport menu Table
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e ) 
		{
			// Udpate visibility of transport sub menu
			// This could have changed since page load
			tableTransport.Visible = transportPanelVisible;
			base.OnPreRender( e );
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.commandTransport.Click += new System.Web.UI.ImageClickEventHandler(this.commandTransport_Click);
			this.commandAccommodation.Click += new System.Web.UI.ImageClickEventHandler(this.commandAccommodation_Click);
			this.commandSport.Click += new System.Web.UI.ImageClickEventHandler(this.commandSport_Click);
			this.commandAttractions.Click += new System.Web.UI.ImageClickEventHandler(this.commandAttractions_Click);
			this.commandHealth.Click += new System.Web.UI.ImageClickEventHandler(this.commandHealth_Click);
			this.commandEducation.Click += new System.Web.UI.ImageClickEventHandler(this.commandEducation_Click);
			this.commandInfrastructure.Click += new System.Web.UI.ImageClickEventHandler(this.commandInfrastructure_Click);

		}
		#endregion

		#region Event Handlers to handle image button clicks to expand panels

		/// <summary>
		/// Handler for the transport button
		/// </summary>
		private void commandTransport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// Update dummy check box image		
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
            if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
			{
				commandTransport.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(imageUnchecked);
				transportPanelVisible = false;
			}
			else
			{
				commandTransport.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(imageChecked);
				transportPanelVisible = true;
			}
		}

		/// <summary>
		/// Handler for the accomodation button
		/// </summary>
		private void commandAccommodation_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			
			
			commandAccommodation.ImageUrl = imageSelected;
			
			// no subcat to open
			tableAccommodation.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableAccommodation, true);

			// unselect all other buttons
			commandAttractions.ImageUrl = imageUnselected;
			commandEducation.ImageUrl = imageUnselected;
			commandInfrastructure.ImageUrl = imageUnselected;
			commandSport.ImageUrl = imageUnselected;
			commandHealth.ImageUrl = imageUnselected;

			// hide all other sub cat
			tableAttractions.Visible = false;
			tableEducation.Visible = false;
			tableInfrastructure.Visible = false;
			tableSport.Visible = false;
			tableHealth.Visible = false;
		}

		/// <summary>
		/// Handler for the accomodation button
		/// </summary>
		private void commandAttractions_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			commandAttractions.ImageUrl = imageSelected;
			tableAttractions.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableAttractions, true);

			// unselect all other buttons
			commandAccommodation.ImageUrl = imageUnselected;
			commandEducation.ImageUrl = imageUnselected;
			commandInfrastructure.ImageUrl = imageUnselected;
			commandSport.ImageUrl = imageUnselected;
			commandHealth.ImageUrl = imageUnselected;

			// hide all other sub cat
			tableAccommodation.Visible = false;
			tableEducation.Visible = false;
			tableInfrastructure.Visible = false;
			tableSport.Visible = false;
			tableHealth.Visible = false;
		}

		/// <summary>
		/// Handler for the accomodation button
		/// </summary>
		private void commandSport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			commandSport.ImageUrl = imageSelected;
			
			// open sub cat
			tableSport.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableSport, true);

			// unselect all other buttons
			commandAccommodation.ImageUrl = imageUnselected;
			commandEducation.ImageUrl = imageUnselected;
			commandInfrastructure.ImageUrl = imageUnselected;
			commandAttractions.ImageUrl = imageUnselected;
			commandHealth.ImageUrl = imageUnselected;

			// hide all other sub cat
			tableAttractions.Visible = false;
			tableEducation.Visible = false;
			tableInfrastructure.Visible = false;
			tableAccommodation.Visible = false;
			tableHealth.Visible = false;	
		}

		/// <summary>
		/// Handler for the education button
		/// </summary>
		private void commandEducation_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			commandEducation.ImageUrl = imageSelected;
			
			// open sub cat
			tableEducation.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableEducation, true);

			// unselect all other buttons
			commandAccommodation.ImageUrl = imageUnselected;
			commandAttractions.ImageUrl = imageUnselected;
			commandInfrastructure.ImageUrl = imageUnselected;
			commandSport.ImageUrl = imageUnselected;
			commandHealth.ImageUrl = imageUnselected;

			// hide all other sub cat
			tableAttractions.Visible = false;
			tableAccommodation.Visible = false;
			tableInfrastructure.Visible = false;
			tableSport.Visible = false;
			tableHealth.Visible = false;
		}

		/// <summary>
		/// Handler for the infrastructure button
		/// </summary>
		private void commandInfrastructure_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			commandInfrastructure.ImageUrl = imageSelected;
			
			// open sub cat
			tableInfrastructure.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableInfrastructure, true);

			// unselect all other buttons
			commandAccommodation.ImageUrl = imageUnselected;
			commandEducation.ImageUrl = imageUnselected;
			commandAttractions.ImageUrl = imageUnselected;
			commandSport.ImageUrl = imageUnselected;
			commandHealth.ImageUrl = imageUnselected;
	
			// hide all other sub cat
			tableAttractions.Visible = false;
			tableEducation.Visible = false;
			tableAccommodation.Visible = false;
			tableSport.Visible = false;
			tableHealth.Visible = false;
		}

		/// <summary>
		/// Handler for the health button
		/// </summary>
		private void commandHealth_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			commandHealth.ImageUrl = imageSelected;
			
			// open sub cat
			tableHealth.Visible = true;

			// Check all checkboxes in this Table
			UpdateCheckBoxes(tableHealth, true);

			// unselect all other buttons
			commandAccommodation.ImageUrl = imageUnselected;
			commandEducation.ImageUrl = imageUnselected;
			commandAttractions.ImageUrl = imageUnselected;
			commandSport.ImageUrl = imageUnselected;
			commandInfrastructure.ImageUrl = imageUnselected;

	
			// hide all other sub cat
			tableAttractions.Visible = false;
			tableEducation.Visible = false;
			tableAccommodation.Visible = false;
			tableSport.Visible = false;
			tableInfrastructure.Visible = false;
		}

		/// <summary>
		/// Checks of unchecks all the checkboxes in the given Table.
		/// </summary>
		/// <param name="Table">Table to update checkboxes for.</param>
		/// <param name="check">Check boxes in true, uncheck if false.</param>
		private void UpdateCheckBoxes(Table table, bool check)
		{
			foreach (System.Web.UI.Control control in table.Controls)
			{
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in control1.Controls)
					{

						if (control2 is CheckBox)
						{
							CheckBox checkbox = (CheckBox) control2;
							checkbox.Checked = check;
						}
					}
				}
			}
		}

		#endregion

		#region Methods to populate check lists key images in the table

		/// <summary>
		/// Takes a Table and a resource name (for Resourcing manager)
		/// and populates the images on that Table. (The images are the keys
		/// to the checkbox items). A 'pipe' seperate list of image urls
		/// must exist in the resource manager for the given resource.
		/// </summary>
		/// <param name="Table">Table to populate key images for.</param>
		/// <param name="resource">Name of the resource to populate from in
		/// Resource manager.</param>
		private void PopulateKeyImages(Table table, string resourceImages, string resourceAlternateText)
		{
			// Get the 'pipe' seperate list of image urls for this Table
			string[] imageUrls = Global.tdResourceManager.GetString(
				resourceImages, TDCultureInfo.CurrentUICulture).Split('|');

			string[] imageAlternateTexts = Global.tdResourceManager.GetString(
				resourceAlternateText, TDCultureInfo.CurrentUICulture).Split('|');

			// Find all the images in this Table and populate from the list
			int i = 0;
			foreach(System.Web.UI.Control control in table.Controls)
			{
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in control1.Controls)
					{

						// Check to see if the control is an image
						if(control2 is System.Web.UI.WebControls.Image)
						{
							// The control is an image so set the image url
                            // CCN 0427 change image control to tdimage control
							TDImage image =
								(TDImage)control2;
							if(i < imageUrls.Length)
							{
								image.ImageUrl = imageUrls[i];
								image.AlternateText = imageAlternateTexts[i++];
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Method to populate the checklist
		/// </summary>
		private void PopulateCheckList(Table table, string resource)
		{
			try
			{
				string[] labels = Global.tdResourceManager.GetString(
					resource,
					TDCultureInfo.CurrentUICulture).Split('|');

				int i=0;
				foreach (System.Web.UI.Control control in table.Controls)
				{
					foreach(System.Web.UI.Control control1 in control.Controls)
					{
						foreach(System.Web.UI.Control control2 in control1.Controls)
						{
							if (control2 is CheckBox)
							{
								CheckBox checkbox = (CheckBox) control2;
								if (i < labels.Length)
									checkbox.Text = labels[i++];
							}
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// If this occurs, then the checklist will not be populated.
			}
		}

		#endregion

		#region Property to return OK button

		/// <summary>
		/// Get Property, returns the OK image button
		/// </summary>
		public TDButton OK
		{
			get { return buttonOk; }
		}

		#endregion

		#region Methods to get selected Hashtable keys

		/// <summary>
		/// Returns an arraylist of all the Stop keys that have been selected.
		/// These are determined depending on what has been selected in
		/// the Transport checkboxes.
		/// </summary>
		/// <returns>ArrayList containing all selected keys</returns>
		public ArrayList GetSelectedStopKeys()
		{
			// Determine if the ImageButton checkbox for Transport has been selected.
			// If the Table is open then get the keys, otherwise return an empty
			// string array.
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
			if(commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
				return GetHashtableKeys(tableTransport);
			else
				return new ArrayList(0);
		}

		/// <summary>
		/// Returns true if the car park check box is selected.
		/// </summary>
		/// <returns>boolean</returns>
		public bool CarParksSelected 
		{
			get
			{
                // CCN 0427 used GetAlteredImageUrl to get the correct image url
				if(commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
					return CPK.Checked;
				else
					return false;
			}
		}

		/// <summary>
		/// Returns an arraylist of all the Stop keys that have been selected.
		/// These are determined depending on what has been selected in
		/// the Accomodation, Sport, Attractions, Education, Health, Public Checkboxes.
		/// The keys returned will belong to only one category (e.g. - this method
		/// will never return keys that belong both to Accomodation and Sports.)
		/// </summary>
		/// <returns>ArrayList containing all selected keys</returns>
		public ArrayList GetSelectedPointXKeys()
		{
			// Determine which Table is open
            // CCN 0427 used GetAlteredImageUrl to get the correct image urls
			if(commandAccommodation.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableAccommodation);

            else if (commandAttractions.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableAttractions);

            else if (commandSport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableSport);

            else if (commandEducation.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableEducation);

            else if (commandInfrastructure.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableInfrastructure);

            else if (commandHealth.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageSelected).ToLower())
				return GetHashtableKeys(tableHealth);

			else
				return new ArrayList(0); // no Tables open
		}

		/// <summary>
		/// Returns all the hashtable keys of all selected checkboxes in the given Table.
		/// </summary>
		/// <param name="Table">Table to get hashtable keys for.</param>
		/// <returns>ArrayList of selected keys.</returns>
		private ArrayList GetHashtableKeys(Table table)
		{
			ArrayList keys = new ArrayList();
			bool selectAll = false;
				
			// Loop through the controls in the given Table
			foreach(System.Web.UI.Control control in table.Controls)
			{	
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in control1.Controls)
					{

						// Check to see if the control is a checkbox
						if(control2 is CheckBox)
						{
							CheckBox checkBox = (CheckBox)control2;

							// Get the id of the checkbox
							string id = checkBox.ID;

							// Check to see if the id is a "Select All" option
							if(id.StartsWith("checkboxSelectAll"))
							{
								// Determine if "Select All" has been checked
								selectAll = checkBox.Checked;
							}
							else
							{
								// Check box is not "Select All". Determine if the associated
								// key should be added.
								if(selectAll || checkBox.Checked)
									keys.Add(id);
							}
						}
					}
				}
			}

			// Return all selected keys
			return keys;
		}

		#endregion

		/// <summary>
		/// Enable/Disables the OK button
		/// </summary>
		/// <param name="enable">True to enable, false to disable.</param>
		public void EnableOKButton(bool enable)
		{
			// Set both to false initially - otherwise it would be possible for
			// both buttons to show if there visibilities were not initialised correctly.
			buttonOk.Visible = false;
			//imageOK.Visible = false;
			panelOnlyView.Visible = false;
			
			buttonOk.Visible = enable;
			//imageOK.Visible = !enable;

			panelOnlyView.Visible = !enable;
				
			commandTransport.Visible = enable;

			labelTransportTitle.Visible = enable;
			commandAccommodation.Visible = enable;
			labelAccommodationTitle.Visible = enable;
			commandSport.Visible = enable;
			labelSportTitle.Visible = enable;
			commandAttractions.Visible = enable;
			labelAttractionsTitle.Visible = enable;
			commandHealth.Visible = enable;
			labelHealthTitle.Visible = enable;
			commandEducation.Visible = enable;
			labelEducationTitle.Visible = enable;
			commandInfrastructure.Visible = enable;
			labelInfrastructureTitle.Visible = enable;
			panelKeys.Visible = enable;
            panelKeysBox.Visible = enable;
		
		}

		/// <summary>
		/// Get property - indicates if the OK button is enabled.
		/// </summary>
		public bool IsEnabled
		{
			get
			{
				return buttonOk.Visible;
			}
		}

        public bool IsAdditionalIconsSelected
        {
            get
            {
                bool selected = false;

                if (commandHealth.ImageUrl == imageSelected)
                    selected = true;
                if (commandAccommodation.ImageUrl == imageSelected)
                    selected = true;
                if (commandEducation.ImageUrl == imageSelected)
                    selected = true;
                if (commandAttractions.ImageUrl == imageSelected)
                    selected = true;
                if (commandSport.ImageUrl == imageSelected)
                    selected = true;
                if (commandInfrastructure.ImageUrl == imageSelected)
                    selected = true;


                return selected;
			
			
			
            }
        }

        

		/// <summary>
		/// Uncheck all transport section checkboxes. For FindStationMap page
		/// </summary>
		public void UncheckTransportSection()
		{

			// UnCheck all checkboxes in the Transport Table
			UpdateCheckBoxes(tableTransport, false);

		}

		/// <summary>
		/// Uncheck all transport section checkboxes, then check airports, rail stations, and car parks
		/// This is the default map symbol selection for car journeys
		/// </summary>
		public void TransportSectionCar()
		{

			// UnCheck all checkboxes in the Transport Table
			UpdateCheckBoxes(tableTransport, false);
			RLY.Checked = true;
			AIR.Checked = true;
			MET.Checked = true;
			FER.Checked = true;
			CPK.Checked = true;

		}

		/// <summary>
		/// Check all transport section checkboxes, default for public journeys and Extension summaries. 
		/// </summary>
		public void CheckTransportSection()
		{

			// Check all checkboxes in the Transport Table
			UpdateCheckBoxes(tableTransport, true);

		}

		#region Methods to control icon selection for printer friendly pages

		/// <summary>
		/// Clears the current icon selection
		/// </summary>
		private void ClearIconSelection(ref bool[][] iconSelection)
		{
			for (int i=0 ; i< iconSelection.Length; i++)
			{
				for (int j=0; j< iconSelection[i].Length; j++)
				{
					iconSelection[i][j] = false;
				}
			}
		}

		/// <summary>
		/// Updates the current icon selection
		/// </summary>
		public void UpdateIconSelection (ref bool[][] iconSelection)
		{
			ClearIconSelection(ref iconSelection);
			
			if (tableTransport.Visible)
				RetrieveSelectionFromTable(tableTransport, ref iconSelection[0]);
			if (tableAccommodation.Visible)
				RetrieveSelectionFromTable(tableAccommodation, ref iconSelection[1]);
			if (tableSport.Visible)
				RetrieveSelectionFromTable(tableSport, ref iconSelection[2]);
			if (tableAttractions.Visible)
				RetrieveSelectionFromTable(tableAttractions, ref iconSelection[3]);
			if (tableHealth.Visible)
				RetrieveSelectionFromTable(tableHealth, ref iconSelection[4]);
			if (tableEducation.Visible)
				RetrieveSelectionFromTable(tableEducation, ref iconSelection[5]);
			if (tableInfrastructure.Visible)
				RetrieveSelectionFromTable(tableInfrastructure, ref iconSelection[6]);

			
		}

		/// <summary>
		/// Retrieves the current icon selection
		/// </summary>
		private void RetrieveSelectionFromTable(Table table, ref bool[] iconSelection)
		{
			int i=0;
			foreach (System.Web.UI.Control control in table.Controls)
			{
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in control1.Controls)
					{
						if (control2 is CheckBox)
						{
							
							CheckBox checkBox = (CheckBox)control2;
							
							iconSelection[i++] = checkBox.Checked;
							
						}
					}
				}
			}
		}

		#endregion




	}
}
