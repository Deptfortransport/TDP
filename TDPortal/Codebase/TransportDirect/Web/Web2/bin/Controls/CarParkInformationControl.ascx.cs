// *********************************************** 
// NAME                 : CarParkInformationControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/08/2006 
// DESCRIPTION			: Displays the data for a Car Park object
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarParkInformationControl.ascx.cs-arc  $
//
//   Rev 1.4   Jan 05 2009 09:30:06   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jul 24 2008 13:45:46   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.2   Mar 31 2008 13:19:40   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 17 2008 17:00:00   mmodi
//Updated presentation of information
//
//   Rev DevFactory   Feb 14 2008 13:00:00   mmodi
//Updated check for Park and ride car park
//
//  Rev DevFactory Feb 12 2008 15:30:00 mmodi
//Updated to not display information when not available
//
//  Rev DevFactory Feb 06 2008 22:02:00 apatel
//  Change to handler car parking opening/closing times as string instead of integer.
//
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:12:42   mturner
//Initial revision.
//
//   Rev 1.9   Nov 08 2006 17:36:46   mmodi
//InformationNote which displays the standard opening times note has been hidden
//Resolution for 4248: Del 9.1: Remove Opening time note on Car Park Information page
//
//   Rev 1.8   Oct 11 2006 12:11:18   mmodi
//Address now replaces "--" with a new line character
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4222: Car Parking: Formatting required to handle hyphen in car park data
//
//   Rev 1.7   Sep 12 2006 13:04:10   mmodi
//Amended operator name setting logic to detect empty urls
//Resolution for 4159: Car Parking: Car park information page displays operator name in purple
//Resolution for 4176: Car Parking: Information page does not display Operator URL
//
//   Rev 1.6   Sep 05 2006 14:21:04   mmodi
//Added check for when no Park and ride scheme is referenced
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Sep 01 2006 13:30:40   mmodi
//Amended various display issues
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 29 2006 18:28:48   esevern
//updated to correct park and ride scheme location and comments properties
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 16 2006 11:55:08   mmodi
//Updated to use CarPark.cs object
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;
    using TransportDirect.Common.PropertyService.Properties;

	/// <summary>
	///		Summary description for CarParkInformationControl.
	/// </summary>
	public partial class CarParkInformationControl : TDUserControl
	{
		#region Private attributes

		#region Car park labels


		#endregion

		#region Park and ride labels


		#endregion
		
		private CarPark data;


		#endregion

		#region Public attributes

		/// <summary>
		/// (Read-Write) Gets or sets the CarPark object associated with this CarParkInformationControl
		/// </summary>
		public CarPark Data
		{
			get{	return data;	}
			set{	data = value;	}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Used to populate the control.
		/// </summary>
		private void DisplayControl()
		{
			if (data.CarParkReference == "")
			{
				this.Visible = false;
				return;
			}

			
			// Display car park details
			this.carParkSummaryTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkSummaryTitle", TDCultureInfo.CurrentUICulture);
			this.carParkTelephone.Text = data.Telephone;
				
			DisplayAddress();

			DisplayMinimumCost();

			DisplayOperatorName();

            DisplayAdditionalData();
			
			DisplayParkAndRideDetails();

            DisplayAdditionalNotes();

			// Display notes at bottom of screen
			informationNote.Text = Global.tdResourceManager.GetString("CarParkInformationControl.informationNote", TDCultureInfo.CurrentUICulture);

			// IR4248 - Do not display the opening times "informationNote"
			informationNote.Visible = false;
		}

		/// <summary>
		/// Sets and displays the car park address
		/// Removes double hyphen in the address by replacing with a new line
		/// </summary>
		private void DisplayAddress()
		{
            this.carParkAddressTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkAddressTitle", TDCultureInfo.CurrentUICulture);
			string address = data.Address;
			this.carParkAddress.Text = address.Replace("--","<br/>");
			this.carParkPostcode.Text = data.Postcode;
		}

		/// <summary>
		/// Sets and displays the car park minimum cost
		/// </summary>
		private void DisplayMinimumCost()
		{
			// Only display minimum cost if available
			if (data.MinimumCost >= 0)
			{
				this.carParkMinimumCostTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkMinimumCostTitle", TDCultureInfo.CurrentUICulture);
				if (data.MinimumCost == 0)
					// Display as "Free"
					this.carParkMinimumCost.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkMinimumCostFree", TDCultureInfo.CurrentUICulture);
				else
				{
					// convert the cost (int) to pounds and pence
					decimal chargeItem;
					chargeItem = (decimal)data.MinimumCost / 100;
					this.carParkMinimumCost.Text = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", chargeItem);
				}

                this.minimumCostPanel.Visible = true;
			}
			else
			{
				this.carParkMinimumCostTitle.Visible = false;
				this.carParkMinimumCost.Visible = false;

                this.minimumCostPanel.Visible = false;
			}
		}

        /// <summary>
        /// Sets and dsplays the additional data labels/hyperlinks dependant on what is available
        /// </summary>
        private void DisplayAdditionalData()
        {
            if (data.CarParkAdditionalData == null)
            {
                openingTimePanel.Visible = false;
                numberOfSpacesPanel.Visible = false;
                numberOfDisabledSpacePanel.Visible = false;
                carParkTypePanel.Visible = false;
                carParkRestrictionsPanel.Visible = false;
                carParkAdvancedReservationsPanel.Visible = false;
                carParkPMSPAPanel.Visible = false;
                return;
            }
            
            // Determine if we should show any of the additional data
            bool showOpeningClosingTimes = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowOpeningClosingTimes"]);
            bool showParkPMSPALogo = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowIsSecureLogo"]);
            bool showRestrictions = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowRestrictions"]);
            bool showAdvancedReservations = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowAdvancedReservations"]);
            bool showTotalSpaces = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowTotalSpaces"]);
            bool showDisabledSpaces = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowDisabledSpaces"]);
            bool showCarParkType = bool.Parse(Properties.Current["FindCarParkResults.Information.ShowCarParkType"]);

            //opening and closing times
            // CCN 0426 Changed opening time and closing time as a string
            // if opening/closing time is 00:00:00 then opening time shouldn't displayed.
            if (((data.CarParkAdditionalData.OpeningTime != string.Empty) && (data.CarParkAdditionalData.ClosingTime != string.Empty) 
                    && (data.CarParkAdditionalData.OpeningTime.Trim() != "00:00:00") 
                    && (data.CarParkAdditionalData.ClosingTime.Trim() != "00:00:00")) && (showOpeningClosingTimes))
            {
                carParkOpeningTimesTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkOpeningTimesTitle", TDCultureInfo.CurrentUICulture);
                carParkOpeningTimes.Text = string.Format(Global.tdResourceManager.GetString("CarParkInformationControl.carParkOpeningTimes", TDCultureInfo.CurrentUICulture), data.CarParkAdditionalData.OpeningTime.Substring(0, 5), data.CarParkAdditionalData.ClosingTime.Substring(0, 5));
            }
            else
            {
                openingTimePanel.Visible = false;
            }

            //is Carpark PMSPA Accredited
            if ((data.CarParkAdditionalData.IsSecure) && (showParkPMSPALogo))
            {
                carParkPMSPALogo.AlternateText = Global.tdResourceManager.GetString("CarParkInformationControl.carParkPMSPAMarkAltText", TDCultureInfo.CurrentUICulture);
                carParkPMSPALogo.ImageUrl = Global.tdResourceManager.GetString("CarParkInformationControl.carParkPMSPAImageURL", TDCultureInfo.CurrentUICulture);
            }
            else
            {
                carParkPMSPAPanel.Visible = false;
            }

            //restrictions, based on height n width as restrictions data is of limited availability
            if (((data.CarParkAdditionalData.MaxWidth > 0) || (data.CarParkAdditionalData.MaxHeight > 0)) && (showRestrictions))
            {
                carParkRestrictionsTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.ParkingRestrictionsTitle", TDCultureInfo.CurrentUICulture);
                if (data.CarParkAdditionalData.MaxHeight > 0)
                    carParkRestrictionsHeight.Text = string.Format(Global.tdResourceManager.GetString("CarParkInformationControl.ParkingRestrictions.Height", TDCultureInfo.CurrentUICulture), data.CarParkAdditionalData.MaxHeight) + "<br />";
                else
                    carParkRestrictionsHeight.Visible = false;

                if (data.CarParkAdditionalData.MaxWidth > 0)
                    carParkRestrictionsWidth.Text = string.Format(Global.tdResourceManager.GetString("CarParkInformationControl.ParkingRestrictions.Width", TDCultureInfo.CurrentUICulture), data.CarParkAdditionalData.MaxWidth) + "<br />";
                else
                    carParkRestrictionsWidth.Visible = false;
            }
            else
            {
                carParkRestrictionsPanel.Visible = false;
            }

            //advanced reservations available
            if ((data.CarParkAdditionalData.IsAdvancedReservationsAvailable) && (showAdvancedReservations))
            {
                carParkAdvancedReservationsTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.AdvancedReservationsTitle", TDCultureInfo.CurrentUICulture);
                carParkAdvancedReservations.Text = Global.tdResourceManager.GetString("CarParkInformationControl.AdvancedReservationsYesText", TDCultureInfo.CurrentUICulture); 
            }
            else
            {
                carParkAdvancedReservationsPanel.Visible = false;
            }

            //data.CarParkAdditionalData.TotalSpaces;
            if ((data.CarParkAdditionalData.TotalSpaces > 0) && (showTotalSpaces))
            {
                carParkNumberOfSpacesTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.TotalSpacesTitle", TDCultureInfo.CurrentUICulture);
                carParkNumberOfSpaces.Text = Convert.ToString(data.CarParkAdditionalData.TotalSpaces);
            }
            else
            {
                numberOfSpacesPanel.Visible = false;
            }

            //data.CarParkAdditionalData.TotalDisabledSpaces - not known;
            if ((data.CarParkAdditionalData.TotalDisabledSpaces > 0) && (showDisabledSpaces))
            {
                carParkNumberOfDisabledSpacesTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.NumberOfDisabledSpacesTitle", TDCultureInfo.CurrentUICulture);
                carParkNumberOfDisabledSpaces.Text = Convert.ToString(data.CarParkAdditionalData.TotalDisabledSpaces);
            }
            else
            {
                numberOfDisabledSpacePanel.Visible = false;
            }

            //data.CarParkAdditionalData.Description
            if ((data.CarParkAdditionalData.CarParkTypeDescription != "") && (showCarParkType))
            {
                carParkTypeTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.CarParkTypeTitle", TDCultureInfo.CurrentUICulture);
                carParkType.Text = data.CarParkAdditionalData.CarParkTypeDescription;
            }
            else
            {
                carParkTypePanel.Visible = false;
            }
        }
        
        /// <summary>
		/// Sets and displays the operator label/hyperlink details dependent on data available
		/// </summary>
		private void DisplayOperatorName()
		{
			// hyperlink is launched in a new browser
			this.operatorNameHyperlink.Target = "_blank";

			// default is to use the hyperlink control and not the operator name label
			this.operatorName.Visible = false;

			if ((data.CarParkOperator.OperatorName != null) && (data.CarParkOperator.OperatorName != ""))
			{
				// hyperlink is displayed using operator name, 
                this.operatorNameHyperlink.Text = string.Format("{0} {1}", data.CarParkOperator.OperatorName, GetResource("ExternalLinks.OpensNewWindowText"));

				// car park url has priority over operator url
				if ((data.CarParkURL != null) && (data.CarParkURL != ""))
				{	
					this.operatorNameHyperlink.NavigateUrl = data.CarParkURL;
				}
				else if ((data.CarParkOperator.OperatorURL != null) && (data.CarParkOperator.OperatorURL != ""))
				{
					this.operatorNameHyperlink.NavigateUrl = data.CarParkOperator.OperatorURL;

				}
				else
				{
					// no urls available, so hide the hyperlink and only use the operator name label
					this.operatorNameHyperlink.Visible = false;
					this.operatorName.Visible = true;
					this.operatorName.Text = data.CarParkOperator.OperatorName;
				}
			}
			else  // operator name is not available so use the urls
			{
				// car park url has priority over operator url
				if ((data.CarParkURL != null) && (data.CarParkURL != ""))
				{
					this.operatorNameHyperlink.Text = data.CarParkURL;
					this.operatorNameHyperlink.NavigateUrl = data.CarParkURL;
				}
				else if ((data.CarParkOperator.OperatorURL != null) && (data.CarParkOperator.OperatorURL != ""))
				{
					this.operatorNameHyperlink.Text = data.CarParkOperator.OperatorURL;
					this.operatorNameHyperlink.NavigateUrl = data.CarParkOperator.OperatorURL;

				}
				else
				{
					// operator name, and both urls not available so hide the hyperlink control as well
					this.operatorNameHyperlink.Visible = false;
				}
			}
		}

		/// <summary>
		/// Displays the Park and Ride if car park is part of P&R scheme
		/// </summary>
		private void DisplayParkAndRideDetails()
		{
			if ((data.ParkAndRideIndicator.Trim().ToLower().Equals("true"))
                && (data.ParkAndRideScheme != null))
			{
				this.parkAndRideSummaryTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.parkAndRideSummaryTitle", TDCultureInfo.CurrentUICulture);
				// "This car park is part of the"
				this.parkAndRideText1.Text = Global.tdResourceManager.GetString("CarParkInformationControl.parkAndRideText1", TDCultureInfo.CurrentUICulture);
				// "park and ride scheme"
				this.parkAndRideText2.Text = Global.tdResourceManager.GetString("CarParkInformationControl.parkAndRideText2", TDCultureInfo.CurrentUICulture);
				this.parkAndRideLocation.Text = data.ParkAndRideScheme.SchemeLocation;
				this.parkAndRideComments.Text = data.ParkAndRideScheme.SchemeComments;

				this.ParkAndRidePanel.Visible = true;
			}
			else
			{
				this.ParkAndRidePanel.Visible = false;
			}
		}

        /// <summary>
        /// Displays the additional notes for the car park
        /// </summary>
        private void DisplayAdditionalNotes()
        {
            if (!string.IsNullOrEmpty(data.Notes))
            {
                this.carParkNotes.Text = data.Notes;
                this.carParkAdditionalNotesTitle.Text = Global.tdResourceManager.GetString("CarParkInformationControl.carParkAdditionalNotesTitle", TDCultureInfo.CurrentUICulture);

                this.additionalNotesPanel.Visible = true;
            }
            else
            {
                this.additionalNotesPanel.Visible = false;
            }
        }

		#endregion

		#region PreRender

		/// <summary>
		/// Renders the control
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			if(data != null)
			{
				DisplayControl();
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
