// *********************************************** 
// NAME                 : RoadSelectControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 18/01/2005 
// DESCRIPTION  : Allows User to modify the road name if it does not start with an alphabetic character and
//				  it is not followed by one digit.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RoadSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:22:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:36   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:27:10   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   May 19 2005 13:53:48   ralavi
//Changed as the result of FXCop run.
//
//   Rev 1.3   Apr 06 2005 12:58:30   Ralavi
//Allocated a limit of 12 characters in the text box
//
//   Rev 1.2   Mar 30 2005 17:28:24   RAlavi
//No change.
//
//   Rev 1.1   Feb 21 2005 14:05:46   esevern
//checking for integration with input page
//
//   Rev 1.0   Jan 28 2005 09:55:44   ralavi
//Initial revision.

	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.SessionManager;
	using System.Globalization;

	namespace TransportDirect.UserPortal.Web.Controls
	{

	/// <summary>
	///		Summary description for RoadSelectControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class RoadSelectControl : TDUserControl
	{

		#region declaration


		private bool textIsValid;
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			TDRoad road = new TDRoad();

			if (this.RoadTextBox.Text != null)
			{
				if (road.ValidateRoadName(this.RoadTextBox.Text))
				{
					road.Status = TDRoadStatus.Valid;
				}
				else
				{
					road.Status = TDRoadStatus.Ambiguous;
					textIsValid = false;
				}
			}
		}
		
		#region Properties giving access to the controls
		
		/// <summary>
		/// Read and write property returning the textLocation control
		/// </summary>
		public TextBox RoadTextBox
		{
			get
			{
				return boxTextRoad;
			}
			set
			{
				boxTextRoad=value;
			}
		}



		///<summary>
		/// Read-only property to check that returns true if text is valid
		///</summary>
		public bool TextIsValid
		{
			get
			{
				return textIsValid;
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
