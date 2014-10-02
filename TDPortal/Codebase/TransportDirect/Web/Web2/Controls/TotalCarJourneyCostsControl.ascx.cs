// *********************************************** 
// NAME                 : TotalCarJourneyCostsControl.ascx.cs 
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 13/01/2003
// DESCRIPTION          : Control that displays total costs for car journeys
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TotalCarJourneyCostsControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:23:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:16   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:27:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   May 20 2005 11:14:06   tmollart
//Removed redundant code and added new property to allow access to penceLabel control.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.3   Apr 19 2005 19:38:26   esevern
//fxcop change
//
//   Rev 1.2   Mar 14 2005 12:41:36   esevern
//formatting of table cells now using jpstd.css
//
//   Rev 1.1   Mar 02 2005 10:23:30   esevern
//corrected namespace and base class
//
//   Rev 1.0   Jan 13 2005 10:25:52   rgreenwood
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Web.Support;

	/// <summary>
	///		Contol to display total costs of the entire car journey
	///		(outward and return).  If the journey is outward only, the
	///		value is the same as the total cost of the single journey
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class TotalCarJourneyCostsControl : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			totalJourneyCostLabel.Text = GetResource("TotalCarJourneyCostsControl.totalJourneyCostLabel");
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
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Read only property returning reference to the label 
		/// used to display the total cost description text
		/// </summary>
		public Label TotalCostLabel 
		{
			get { return totalJourneyCostLabel;	}
		}

		/// <summary>
		/// Read Only. Returns reference to pounds label.
		/// </summary>
		public Label PoundsLabel
		{
			get { return poundsLabel; }
		}

		/// <summary>
		/// Read Only. Returns reference to pence label.
		/// </summary>
		public Label PenceLabel
		{
			get { return penceLabel; }
		}

		#endregion
	}
}
