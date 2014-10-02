// *********************************************** 
// NAME                 : ServiceOperationControl.cs
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2005-07-18
// DESCRIPTION			: Control to display formatted details
//                        of operation of a public transport schedule  
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ServiceOperationControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 12 2009 11:13:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:52   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:17:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:27:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 16 2005 17:52:50   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 20:01:04   RPhilpott
//Development ofd ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:08:10   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	/// Control to display formatted details of
	/// operation of a public transport schedule. 
	/// </summary>
	public partial class ServiceOperationControl : TDPrintableUserControl
	{
		protected OperatorLinkControl operatorLinkControl;

		private PublicJourneyDetail journeyDetail; 

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ServiceOperationControl()
		{
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
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
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{

			RailServiceOperationsAdaptor opsAdapter = new RailServiceOperationsAdaptor();
			serviceValidityText.Text = opsAdapter.GetServiceValidityText(journeyDetail);
			
			if	(serviceValidityText.Text.Length == 0)
			{
				serviceValidityText.Visible = false;
			}

			operatorLinkControl.Visible = false;
			operatorText.Visible = false;

			// note that this assumes only one service present 
			//  (true at present, but not necessarily for ever) ...

			if	(journeyDetail.Services != null && journeyDetail.Services.Length > 0) 
			{
				if	(journeyDetail.Services[0].OperatorCode.Length > 0 
					|| journeyDetail.Services[0].OperatorName.Length > 0)
				{
					operatorLinkControl.OperatorCode = journeyDetail.Services[0].OperatorCode; 
					operatorLinkControl.OperatorName = journeyDetail.Services[0].OperatorName; 
					operatorLinkControl.TravelMode	 = journeyDetail.Mode;
                    operatorLinkControl.Region = journeyDetail.Region;
					operatorText.Text = GetResource("ServiceDetails.Operator");

					operatorLinkControl.PrinterFriendly = this.PrinterFriendly;
					operatorLinkControl.Visible = true;
					operatorText.Visible = true;
				}
			}


			if	(!serviceValidityText.Visible && !operatorText.Visible)
			{
				headingLabel.Visible = false;
				this.Visible = false;
			}
			else
			{
				string modeString = GetResource("TransportMode." + journeyDetail.Mode.ToString());
				headingLabel.Text = string.Format(GetResource("ServiceDetails.ServiceOperation.Heading"), modeString);
			}
            
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the PublicJourneyDetail for this journey leg.
		/// </summary>
		public PublicJourneyDetail JourneyDetail
		{
			get { return journeyDetail; }
			set { journeyDetail = value; }
		}
		#endregion

	}
}
