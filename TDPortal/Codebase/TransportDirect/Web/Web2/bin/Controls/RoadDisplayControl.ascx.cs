//*********************************************** 
// NAME                 : RoadDisplayControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 18/01/2005 
// DESCRIPTION  : New version for RoadDisplayControl. Allows user to enter the road name
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RoadDisplayControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:34   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:27:08   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   May 19 2005 13:52:46   ralavi
//Changes as the result of FXcop run.
//
//   Rev 1.3   Mar 02 2005 15:26:18   RAlavi
//Changes for ambiguity
//
//   Rev 1.2   Feb 21 2005 14:05:40   esevern
//checking for integration with input page
//
//   Rev 1.1   Feb 18 2005 17:13:24   esevern
//Car costing - interim working copy checked in for code integration
//
//   Rev 1.0   Jan 28 2005 09:54:34   ralavi
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;	
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using System.Globalization;

	
	/// <summary>
	///		Summary description for RoadDisplayControl.
	/// </summary>
	public partial class RoadDisplayControl : TDUserControl
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			
		}

		public void Populate(TDRoad road)
		{
			
			roadInstructLabel.Text = HttpUtility.HtmlDecode( road.RoadName);
		}
	
			

		#region Properties giving access to the controls

		/// <summary>
		/// Read-only property returning the textLocation control
		/// </summary>
		public Label RoadLabel
		{
			get
			{
				return roadInstructLabel;
			}
			set
			{
				roadInstructLabel = value;
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
