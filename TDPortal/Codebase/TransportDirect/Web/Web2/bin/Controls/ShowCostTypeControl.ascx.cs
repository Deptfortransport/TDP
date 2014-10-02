// *********************************************** 
// NAME                 : ShowCostTypeControl.ascx.cs 
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 21/12/2003
// DESCRIPTION          : UI Control that allows user to
//						  view either Fuel Costs or Total
//						  costs on the Results page.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ShowCostTypeControl.ascx.cs-arc  $ 
//
//   Rev 1.4   May 20 2008 13:05:40   RBRODDLE
//Updated following code review
//
//   Rev 1.3   May 20 2008 09:44:56   RBRODDLE
//Correction for Car Costs Drop Down not working - DfT 24hr USD 2590211
//Resolution for 4986: 24Hr USD - Car Fuel Costs / Total Costs selection drop not working
//
//   Rev 1.2   Mar 31 2008 13:23:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:54   mturner
//Initial revision.
//
//   Rev 1.9   Apr 03 2006 16:28:44   AViitanen
//Added a condition for setupresources for Refine tickets page. 
//Resolution for 3746: DN068 Extend: Faulty position of Help button in login/register control when on Tickets/costs page
//
//   Rev 1.8   Mar 14 2006 10:30:16   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.7   Feb 23 2006 19:16:52   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.2.2   Mar 07 2006 15:07:42   RGriffith
//Comments added for tickets/costs
//
//   Rev 1.6.2.1   Mar 06 2006 17:35:40   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.6.2.0   Mar 01 2006 13:22:28   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.6.1.0   Jan 10 2006 15:27:30   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Apr 22 2005 17:59:44   esevern
//for return car journeys, when either outward or return car cost are change (between fuel/all costs) the other controls display is also changed
//Resolution for 2236: Del 7 - Car Costing - Fuel Costs/Total Costs toggles
//
//   Rev 1.5   Apr 04 2005 17:03:52   esevern
//amendments to comply with FXCop
//
//   Rev 1.4   Mar 23 2005 16:10:18   esevern
//check page id - if printable, hide drop down/buttons etc
//
//   Rev 1.3   Feb 11 2005 15:37:44   rgreenwood
//Work in progress
//
//   Rev 1.2   Jan 11 2005 10:26:44   rgreenwood
//Work in Progress
//
//   Rev 1.1   Jan 06 2005 13:17:44   rgreenwood
//Initial revision

#region namespaces

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
#endregion

#region TD Namespaces

	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web.UserSupport;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
#endregion

	/// <summary>
	///	Control providing choice of fuel types via a drop down list
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ShowCostTypeControl : TDPrintableUserControl
	{

		#region Controls & Variables

		//Populator to load the strings for the DropDown list
		private DataServices populator;
		
		//Controls

		#endregion

		/// <summary>
		/// Sets text for control and calls populate method for drop down list.
		/// Checks if the calling page is printable - if so, hides
		/// the drop down list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (this.PrinterFriendly)
			{
				// hide the fuel type drop list 
				dropFuelCosts.Visible = false;
				labelShow.Visible = false;
			}
			else 
			{					
				//Set up resources for controls
                int fuelCostsVal = dropFuelCosts.SelectedIndex;
				SetUpResources();
                dropFuelCosts.SelectedIndex = fuelCostsVal;
			}
		}

		/// <summary>
		/// Set up resources for the controls - uses Dataservices, langstrings to 
		/// add the drop down list items, and sets label text
		/// </summary>
		private void SetUpResources()
		{

			dropFuelCosts.Visible = true;
			labelShow.Visible = true;
			
			labelShow.Text = Global.tdResourceManager.GetString
				("ShowCostTypeControl.labelShow", TDCultureInfo.CurrentUICulture);

			//Populates the drop down list control with the allowed values from DataServices
			populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			populator.LoadListControl(DataServiceType.FuelCostsDrop, dropFuelCosts, Global.tdResourceManager);

		}



		#region Properties

		/// <summary>
		/// Read only property returning the selected index in the dropdown list
		/// </summary>
		public int ShowSelectedCost
		{
			get { return (int)dropFuelCosts.SelectedIndex; }
		}

		/// <summary>
		/// Method used to set the selected item displayed in the fuel costs
		/// drop down list. When car costings are displayed for a return private
		/// journey, and the user swaps between fuel and all costs, both controls
		/// (outward and return) should display the same selection
		/// </summary>
		/// <param name="showAll">bool true if 'all costs' should be selected in fuel costs dropdown</param>
		public void SetSelectedCostItem(bool showAll)
		{
			if(showAll) 
			{
				dropFuelCosts.SelectedIndex = 1;
			}
			else 
			{
				dropFuelCosts.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Property to make the drop down fuel list accessible
		/// </summary>
		public DropDownList DropDownFuelCosts
		{
			get { return dropFuelCosts; }
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
