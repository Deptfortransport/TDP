// *********************************************** 
// NAME                 : FareNoFareInformationControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 18/01/2005
// DESCRIPTION			: A custom control to display a list of 
//						  possible fares for a PricingUnit
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FareNoFareInformationControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:24   mturner
//Initial revision.
//
//   Rev 1.2   May 02 2007 13:49:46   asinclair
//Control now only used for Coach Journeys with no fare info, - code no longer need removed
//
//   Rev 1.1   Apr 13 2007 13:36:12   mmodi
//Updated to use resource strings and to fire click event
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.JourneyControl;

	/// <summary>
	///		Summary description for FareNoFareInformationControl.
	/// </summary>
	public partial class FareNoFareInformationControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl otherFaresLinkControl;

		#endregion

		#region Private variables

		private ItineraryAdapter itineraryAdapter;
		private PublicJourneyDetail publicJourneyDetail;
		private bool noThroughFares;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public FareNoFareInformationControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(this.Visible)
			{
				SetLabelText();
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Populate the fares controls dependant on the user's selected view
		/// </summary>
		private void SetLabelText()
		{
			labelMode.Text = publicJourneyDetail.Mode.ToString() + " " + GetResource("FareNoFareInformationControl.Fares");
			labelView.Visible = false;
			StringBuilder legDescription = new StringBuilder();
			legDescription.Append(publicJourneyDetail.LegStart.Location.Description);
			legDescription.Append(" ");
			legDescription.Append(GetResource("JourneyFareHeadingControl.Seperator"));
			legDescription.Append(" ");
			legDescription.Append(publicJourneyDetail.LegEnd.Location.Description);
			labelRoute.Text = legDescription.ToString();

		}

		#endregion 

		#region Event handlers

		/// <summary>
		/// Event raised when the user clicks the single/return HyperlinkPostbackControl.
		/// </summary>
		public event EventHandler OtherFaresClicked;

		/// <summary>
		/// Fired when the other fares button is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void otherFaresLinkControl_link_Clicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);	

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
			this.EnableViewState = false;
			this.otherFaresLinkControl.link_Clicked +=new EventHandler(otherFaresLinkControl_link_Clicked);
		}
		#endregion
	
		#region Public properties
		/// <summary>
		/// Read only property - Exposes the find cheaper image hyperlink control so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's find cheaper hyperlink control</returns>
		public HyperlinkPostbackControl ViewOtherFareLink
		{
			get{ return this.otherFaresLinkControl;}
		} 

		public ItineraryAdapter ItineraryAdapter
		{
			get{ return this.itineraryAdapter;}
			set{itineraryAdapter = value;}
		}

		public Label LabelRoute
		{
			get {return labelRoute;}
			set {labelRoute = value;}
		}

		public PublicJourneyDetail NoFareJourneyDetail
		{
			get {return publicJourneyDetail;}
			set {publicJourneyDetail = value;}
		}

		public Label LabelFareInformation
		{
			get {return labelFareInformation;}
			set {labelFareInformation = value;}
		}

		public bool NoThroughFares
		{
			get {return noThroughFares;}
			set {noThroughFares = value;}
		}
		#endregion
	}
}
