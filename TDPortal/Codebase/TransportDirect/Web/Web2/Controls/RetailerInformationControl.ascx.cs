// *********************************************** 
// NAME			: RetailerInformationControl.ascx
// AUTHOR		: Rachel Geraghty
// DATE CREATED	: 09/02/05
// DESCRIPTION	: Displays details about a single retailer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RetailerInformationControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:28   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.0   Jan 10 2006 15:27:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Apr 05 2005 14:19:48   rgeraghty
//FxCop and comment changes post code review
//Resolution for 1948: DEV CODE REVIEW: FR Retailer Information
//
//   Rev 1.4   Mar 08 2005 16:21:28   jgeorge
//Modifications after first QA
//
//   Rev 1.3   Mar 03 2005 18:00:04   rgeraghty
//FxCop changes made and Name field added
//Resolution for 1948: DEV CODE REVIEW: FR Retailer Information
//
//   Rev 1.2   Mar 01 2005 15:33:18   rgeraghty
//Updated PopulateControl method
//Resolution for 1948: DEV CODE REVIEW: FR Retailer Information
//
//   Rev 1.1   Feb 21 2005 17:24:48   rgeraghty
//Removed back button from control
//
//   Rev 1.0   Feb 10 2005 15:02:42   rgeraghty
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
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.Web.Support;
	

	/// <summary>
	///	Displays information about a Retailer
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerInformationControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		
		private Retailer retailer;

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public RetailerInformationControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetStaticLabelText();
			PopulateControl();			
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

		#region private methods

		/// <summary>
		/// Initialise static label text 
		/// </summary>
		private void SetStaticLabelText()
		{
			labelTitle.Text = GetResource("RetailerInformationControl.LabelTitleText");		
				
			labelNameHeader.Text = GetResource("RetailerInformationControl.LabelNameText");	
			
			labelTelephoneHeader.Text = GetResource("RetailerInformationControl.LabelTelephoneText");		

			labelUrlHeader.Text = GetResource("RetailerInformationControl.LabelURLText");			
		}

		/// <summary>
		/// Populate the control with values from the control's Retailer object
		/// If a property of the Retailer object is null/empty then the corresponding row
		/// of the control is hidden
		/// </summary>
		private void PopulateControl()
		{
			if (retailer !=null)
			{
				//set the name
				labelName.Text = retailer.Name;

				//set Company logo
				if ((retailer.IconUrl == null) || (retailer.IconUrl.Length ==0))
					imageCompanyLogo.Visible = false;
				else			
				{
					imageCompanyLogo.ImageUrl = retailer.IconUrl;					
					imageCompanyLogo.AlternateText = retailer.Name;
				}

				//set telephone number
				if ((retailer.PhoneNumber ==null) ||(retailer.PhoneNumber.Length ==0))
					telephoneRow.Visible = false;
				else
					labelTelephoneValue.Text = retailer.PhoneNumber;

				//set display URL
				if ((retailer.DisplayUrl== null ) ||(retailer.DisplayUrl.Length ==0))
					urlRow.Visible = false;
				else
					labelUrlValue.Text = retailer.DisplayUrl;
			}
		}

		#endregion

		#region public properties

		/// <summary>
		/// Gets/Sets the Retailer object used by the control for its display values
		/// </summary>
		public Retailer RetailerDetails
		{
			get {return retailer;}
			set {retailer=value;}
		}

		#endregion

	}
}
