// *********************************************** 
// NAME                 : CarJourneyCostsControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/02/2005
// DESCRIPTION          : UI Control to display a 
//						  breakdown of the journey costs
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarJourneyCostsControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:32   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:23:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Apr 25 2005 17:03:18   jgeorge
//Modifications to cope with extend journey
//Resolution for 2297: Del 7 - Car Costing - Zero cost for car journey which is part of multi-mode extended journey
//
//   Rev 1.3   Apr 04 2005 17:04:02   esevern
//amendments to comply with FXCop
//
//   Rev 1.2   Mar 14 2005 12:29:10   esevern
//removed redundant method CalculateTotalJourneyCost
//
//   Rev 1.1   Mar 04 2005 15:45:44   esevern
//car costing integration
//
//   Rev 1.0   Feb 28 2005 12:13:08   esevern
//Initial revision.
//   Rev 1.0   Feb 09 2005 12:17:44   esevern
//Initial revision

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
	///		Summary description for CarJourneyCostsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CarJourneyCostsControl : TDUserControl
	{

		// Controls
		protected TransportDirect.UserPortal.Web.Controls.CarJourneyItemisedCostsControl carJourneyItemisedCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.TotalCarJourneyCostsControl totalCarJourneyCostsControl;
		
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


		#region properties
		
		/// <summary>
		/// Read only property returning reference to TotalCarJourneyCostsControl
		/// </summary>
		public TotalCarJourneyCostsControl TotalCarCostsControl
		{
			get 
			{
				return totalCarJourneyCostsControl;
			}
		}

		/// <summary>
		/// Read only property returning reference to CarJourneyItemisedCostsControl
		/// </summary>
		public CarJourneyItemisedCostsControl ItemisedCarCostsControl 
		{
			get 
			{
				return carJourneyItemisedCostsControl;
			}
		}


		#endregion
	}
}
