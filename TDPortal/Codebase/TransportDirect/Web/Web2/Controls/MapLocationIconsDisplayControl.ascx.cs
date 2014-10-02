// *********************************************** 
// NAME                 : MapLocationIconsDisplay.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/09/2003 
// DESCRIPTION  : Control displaying the selected icons to display on the map.
// Used for the printer friendly map pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapLocationIconsDisplayControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Nov 17 2009 17:18:14   mmodi
//Corrected check for if symbols table should be visible
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 11 2009 18:33:06   mmodi
//Updated for use by new mapping changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:22:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:28   mturner
//Initial revision.
//
//   Rev 1.12   Nov 07 2006 11:24:52   dsawe
//made changes in the code behind for replacing asp:panel with asp:table
//Resolution for 4242: IE 7 compatibility
//
//   Rev 1.12   Nov 02 2006 17.10.00   dsawe
//change for IE 7 compatibility
//
//   Rev 1.11   Oct 06 2006 15:40:00   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.10.1.1   Sep 29 2006 15:30:20   esevern
//Added check for car parking functionality switched off and setting strings/image urls accordingly
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.10.1.0   Jul 31 2006 16:10:54   MModi
//Added Car Park map symbol to key
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.10   Apr 24 2006 13:49:42   mtillett
//Use different text for printable page
//Resolution for 3964: Map Symbols: keys truncated when Printer friendly page is printed
//
//   Rev 1.9   Apr 04 2006 14:22:54   build
//Automatically merged from stream0034
//
//   Rev 1.8.1.0   Mar 29 2006 17:57:00   RWilby
//Updated for new map symbols.
//Resolution for 3715: Map Symbols
//
//   Rev 1.8   Feb 23 2006 19:16:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.1   Jan 30 2006 14:41:16   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7.1.0   Jan 10 2006 15:26:22   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Mar 01 2004 11:41:12   asinclair
//Hide B&B for Del 5.2
//
//   Rev 1.6   Feb 20 2004 09:40:30   asinclair
//Del 5.2 - Removed the 'Select all' option
//
//   Rev 1.5   Jan 14 2004 17:28:48   asinclair
//Updated to fix IR 91 - Now the map key alt text is displayed
//
//   Rev 1.4   Nov 05 2003 14:29:50   kcheung
//Fixed commenting
//
//   Rev 1.3   Oct 22 2003 19:23:10   passuied
//changes for printable output map page
//
//   Rev 1.2   Oct 02 2003 16:53:58   passuied
//updated	
//
//   Rev 1.1   Oct 02 2003 12:04:20   passuied
//almost working version of icon display


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Collections;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web.UserSupport;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	/// Control displaying the selected icons to display on the map
	/// </summary>
	/// 
	public partial  class MapLocationIconsDisplayControl : TDUserControl
	{
        /// <summary>
		/// Page Load Method. Sets up the control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			CPK.Visible = FindCarParkHelper.CarParkingAvailable;
			Image31.Visible = FindCarParkHelper.CarParkingAvailable;

			// Populate controls (using same strings as LocationIconsSelectControl)
			labelLocationTitle.Text = 
				Global.tdResourceManager.GetString(
				"MapLocationIconsSelectControl.labelLocationTitle",
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

			// Populate the labels
			if(FindCarParkHelper.CarParkingAvailable)
			{
				PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransport.Print");
				
				PopulateKeyImages(tableTransport, "MapLocationIconsSelectControl.imageUrlListTransport",
					"MapLocationIconsSelectControl.imageListTransportAlternateText");
			}
			else 
			{
				PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransport.PrintAlt");
				
				PopulateKeyImages(tableTransport, "MapLocationIconsSelectControl.imageUrlListTransportAlt",
					"MapLocationIconsSelectControl.imageListTransportAlternateTextAlt");
			}
			PopulateCheckList(tableAttractions, "MapLocationIconsSelectControl.checklistAttractions");
			PopulateCheckList(tableAccommodation, "MapLocationIconsSelectControl.checklistAccommodation");
			PopulateCheckList(tableSport, "MapLocationIconsSelectControl.checklistSport");	
			PopulateCheckList(tableEducation, "MapLocationIconsSelectControl.checklistEducation");
			PopulateCheckList(tableInfrastructure, "MapLocationIconsSelectControl.checklistInfrastructure");
			PopulateCheckList(tableHealth, "MapLocationIconsSelectControl.checklistHealth");

			// Populate the key images
			PopulateKeyImages(tableAttractions, "MapLocationIconsSelectControl.imageUrlListAttractions",
				"MapLocationIconsSelectControl.imageListAttractionsAlternateText");
			PopulateKeyImages(tableAccommodation, "MapLocationIconsSelectControl.imageUrlListAccomodation",
				"MapLocationIconsSelectControl.imageListAccomodationAlternateText");
			PopulateKeyImages(tableSport, "MapLocationIconsSelectControl.imageUrlListSport",
				"MapLocationIconsSelectControl.imageListSportAlternateText");
			PopulateKeyImages(tableEducation, "MapLocationIconsSelectControl.imageUrlListEducation",
				"MapLocationIconsSelectControl.imageListEducationAlternateText");
			PopulateKeyImages(tableInfrastructure, "MapLocationIconsSelectControl.imageUrlListInfrastructure",
				"MapLocationIconsSelectControl.imageListInfrastructureAlternateText");
			PopulateKeyImages(tableHealth, "MapLocationIconsSelectControl.imageUrlListHealth",
				"MapLocationIconsSelectControl.imageListHealthAlternateText");
		}

        /// <summary>
		/// Page PreRender method. Sets up the control.
		/// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SetVisibleDivs();
        }

		/// <summary>
		/// Method to populate the Tables so that the current
		/// selected icons are shown.
		/// </summary>
		/// <param name="iconSelection">Selected icons 2-d array</param>
		public void Populate(bool[][] iconSelection)
		{
			DisplaySelectedIconsInTable(tableTransport, iconSelection[0]);
			DisplaySelectedIconsInTable(tableAccommodation, iconSelection[1]);
			DisplaySelectedIconsInTable(tableSport, iconSelection[2]);
			DisplaySelectedIconsInTable(tableAttractions, iconSelection[3]);
			DisplaySelectedIconsInTable(tableHealth, iconSelection[4]);
			DisplaySelectedIconsInTable(tableEducation, iconSelection[5]);
			DisplaySelectedIconsInTable(tableInfrastructure, iconSelection[6]);


		}

		/// <summary>
		/// Takes a Table and a resource name (for Resourcing manager)
		/// and populates the images on that Table. (The images are the keys
		/// to the checkbox items). A 'pipe' seperate list of image urls
		/// must exist in the resource manager for the given resource.
		/// </summary>
		/// <param name="Table">Table to populate key images for.</param>
		/// <param name="resource">Name of the resource to populate from in
		/// Resource manager.</param>
		private void PopulateKeyImages(Table table, string resource, string resourceAlternateText)
		{
			// Get the 'pipe' seperate list of image urls for this Table
			string[] imageUrls = Global.tdResourceManager.GetString(
				resource, TDCultureInfo.CurrentUICulture).Split('|');

			string[] imageAlternateTexts = Global.tdResourceManager.GetString(
				resourceAlternateText, TDCultureInfo.CurrentUICulture).Split('|');

			// Find all the images in this Table and populate from the list
			int i = 0;
			foreach(System.Web.UI.WebControls.TableRow control in table.Controls)
			{
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in  control1.Controls)
					{
						// Check to see if the control is an image
							if(control2 is System.Web.UI.WebControls.Image)
							{
								// The control is an image so set the image url
								System.Web.UI.WebControls.Image image =
									(System.Web.UI.WebControls.Image)control2;
								if(i < imageUrls.Length)
									image.ImageUrl = imageUrls[i];
								image.AlternateText = imageAlternateTexts[i++];
							}
						
					}
				}
			}
		}

		/// <summary>
		/// Populates the labels inside the Tables.
		/// </summary>
		/// <param name="Table">Table to populate</param>
		/// <param name="resource">Resource manager key for the given Table.</param>
		private void PopulateCheckList(Table table, string resource)
		{
			bool firstRow = true;
			try
			{
				string[] labels = Global.tdResourceManager.GetString(
					resource,
					TDCultureInfo.CurrentUICulture).Split('|');

				int i=0;
				foreach (System.Web.UI.WebControls.TableRow control in table.Controls)
				{
					foreach(System.Web.UI.Control control1 in control.Controls)
					{
						foreach(System.Web.UI.Control control2 in  control1.Controls)
						{
								if (control2 is Label && !firstRow)
								{
									Label label = (Label) control2;
									if (i < labels.Length)
										label.Text = labels[(i++)];
								}
								else
								{ 
									firstRow = false;
								}
							
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// If this happens, the checklist will not be populated.
			}
		}

		

		/// <summary>
		/// Displays the selected icons in the given Table.
		/// </summary>
		/// <param name="Table">Table to show.</param>
		/// <param name="iconsSelected">The icons that are currently
		/// selected for the given Table.</param>
		private void DisplaySelectedIconsInTable(Table table, bool[] iconsSelected)
		{
			int i = 0;
			bool firstRow = true;

			// if one of the icon is visible the Table must be visible
            foreach (bool selected in iconsSelected)
            {
                if (selected)
                {
                    table.Visible = true;
                    break;
                }
            }
			
			if (table.Visible)
				boolIsEmpty = false;
			foreach (System.Web.UI.Control control in table.Controls)
			{
				if (control is TableRow && !firstRow)
				{
					control.Visible = iconsSelected[i++];
				}
				else
				{ 
					firstRow = false;
				}
				 
			}
		}

        /// <summary>
        /// Method which sets the visibility of the symbol divs based on the table visibilities
        /// </summary>
        private void SetVisibleDivs()
        {
            divTransport.Visible = tableTransport.Visible;
            divAccommodation.Visible = tableAccommodation.Visible;
            divAttractions.Visible = tableAttractions.Visible;
            divEducation.Visible = tableEducation.Visible;
            divHealth.Visible = tableHealth.Visible;
            divInfrastructure.Visible = tableInfrastructure.Visible;
            divSport.Visible = tableSport.Visible;
        }

		private bool boolIsEmpty = true;
		
		/// <summary>
		/// Get property. Indicates if icons are selected and need to be displayed
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return boolIsEmpty;
			}
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

		}
		#endregion
	}
}
