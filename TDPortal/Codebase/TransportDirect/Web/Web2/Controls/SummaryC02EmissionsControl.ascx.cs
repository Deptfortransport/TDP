// *********************************************** 
// NAME                 : SummaryCO2EmissionsControl.ascx.cs 
// AUTHOR               : Darshan Sawe
// DATE CREATED         : 29/11/2006
// DESCRIPTION          : Control that displays the summary CO2 emissions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SummaryC02EmissionsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jul 28 2011 16:19:26   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:23:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:02   mturner
//Initial revision.
//
//   Rev 1.5   Jul 10 2007 13:17:02   jfrank
//Update to get the kg units string from the langstrings resourse file.
//Resolution for 4463: Incorrect symbol for kilogram used, should be kg  not Kg
//
//   Rev 1.4   Dec 04 2006 14:16:54   mmodi
//Added alt text for icon
//Resolution for 4240: CO2 Emissions
//Resolution for 4288: CO2: Alt text not shown for icons on Tickets/costs page

//   Rev 1.0   Nov 19 2006 10:25:52   dsawe
//Initial revision.


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.JourneyControl;

	/// <summary>
	///		Control to display C02 Emission for entire car journey
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class SummaryC02EmissionsControl : TDUserControl
	{
		protected HyperlinkPostbackControl hyperlinkJourneyEmission;

		private decimal fuelCost;

		/// <summary>
		/// PreRender event handler.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Initialise static controls.
			InitialiseStaticControls();
		}

		/// <summary>
		/// Sets up control labels.
		/// </summary>
		private void InitialiseStaticControls()
		{
			string CO2amount;
			labelCO2Emission.Text = GetResource("CO2EmissionControl.labelEmission");
			imageco2Emission.ImageUrl = GetResource("CO2EmissionControl.imagePollutingCar.imageurl");
			imageco2Emission.AlternateText = " ";
			
			// Get the car journey params - needed by Emissions helper
			TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			// Set up the emissions helper used to calculate the CO2 Emissions
			JourneyEmissionsHelper emissionsHelper = new JourneyEmissionsHelper(journeyParams, fuelCost);

			CO2amount = Convert.ToString(emissionsHelper.GetEmissions());
			labelCO2Amount.Text = CO2amount + GetResource("JourneyEmissionsDashboard.emissionsTitle.Text");
				
			hyperlinkJourneyEmission.Text = GetResource("CO2EmissionControl.JourneyEmissionLink");

		}

		/// <summary>
		/// Method handles the click event from the hyperlinkJourneyEmission
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hyperlinkJourneyEmission_link_Clicked(object sender, EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Reset the journey emissions page state, to clear it of any previous values
            TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

			// Navigate to the Journey Accessibility page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;
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
			hyperlinkJourneyEmission.link_Clicked += new EventHandler(hyperlinkJourneyEmission_link_Clicked);
		}
		#endregion

		#region Public property

		/// <summary>
		/// Set and get property for the car journey - fuel cost
		/// </summary>
		public decimal FuelCost
		{
			get { return fuelCost; }
			set { fuelCost = value; }
		}

		#endregion
	}
}
