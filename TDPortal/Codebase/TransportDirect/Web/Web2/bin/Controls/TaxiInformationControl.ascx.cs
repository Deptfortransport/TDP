// *********************************************** 
// NAME                 : TaxiInformationControl.aspx.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 11/08/2005 
// DESCRIPTION			: A custom user control to display taxi information for a stop
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TaxiInformationControl.ascx.cs-arc  $
//
//   Rev 1.3   Dec 15 2008 09:57:34   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:06   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:52   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:27:38   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Sep 16 2005 18:40:46   kjosling
//Outsourced image urls and alt text to resource files
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.1   Sep 01 2005 11:42:44   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 12 2005 13:28:20   kjosling
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.AdditionalDataModule;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;

	/// <summary>
	///		A custom user control to display taxi information for a stop
	/// </summary>
	public partial class TaxiInformationControl : TDUserControl
	{

		#region Private attributes

		private StopTaxiInformation data;
		private bool isForAlternativeStops;

		private string imageIcon;
		private string imageAltText;

		#endregion

		#region Public attributes

		/// <summary>
		/// (Read-Write) Allows a parent control to check if the TaxiInformation control is being
		/// rendered for an alternative stop
		/// </summary>
		public bool IsForAlternativeStops
		{
			get
			{	return isForAlternativeStops;	}
			set
			{	isForAlternativeStops = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets the StopTaxiInformation object associated with this TaxiInformationControl
		/// </summary>
		public StopTaxiInformation Data
		{
			get{	return data;	}
			set{	data = value;	}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Used to populate the control on PreRender. This approach is 
		/// necessary because late binding will be used when the control creates 
		/// alternative stops
		/// </summary>
		private void displayControl()
		{
			if(data.InformationAvailable == false)
			{ 
				this.Visible = false;
				return;
			}

			this.comments.Text = data.Comment;
			this.accessibleText.Text = data.AccessibleText;

			//Check for operators and bind the first repeater
			if(data.Operators.Length == 0)
			{
				this.operators.Visible = false;
			}
			else
			{
				TaxiOperator[] listOfOperators = data.Operators;
				this.operators.DataSource = listOfOperators;
			}

			//Check for alternative stops and bind the second repeater 
			if(data.AlternativeStops.Length == 0)
			{
				this.alternativeStops.Visible = false;
			}
			else
			{
				StopTaxiInformation[] listOfAlternativeStops = data.AlternativeStops;
				this.alternativeStops.DataSource = listOfAlternativeStops;

				this.alternativeStops.Visible = true;
			}

			if(data.AccessibleOperatorPresent && !isForAlternativeStops)
			{
				accessibleIcon.Visible = true;
				accessibleText.Visible = true;

			}

			this.DataBind();
		}

		/// <summary>
		/// Renders the control
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			//Set image icon and text
			imageIcon = GetResource("TaxiInfoControl.AccessibleIcon.ImageURL");
			imageAltText = GetResource("TaxiInfoControl.AccessibleIcon.AltText");
			accessibleIcon.ImageUrl = imageIcon;
			accessibleIcon.AlternateText = imageAltText;
			
			if(data != null)
			{
				displayControl();
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
			this.operators.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.operators_ItemDataBound);
			this.alternativeStops.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.alternativeStops_ItemDataBound);

		}
		#endregion

		#region Event Handlers

		/// <summary>
		/// Occurs when the control displays a TaxiOperator record
		/// </summary>
		/// <param name="sender">An object reference to the Repeater control</param>
		/// <param name="e">RepeaterItemEventArgs used to obtain an object reference to the item</param>
		private void operators_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			//Check to see if the operator is accessible, and display the accessiblity icon against it
			//if it is
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				TaxiOperator nextOperator = (TaxiOperator)e.Item.DataItem;
				System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)e.Item.FindControl("isAccessible");
				if(!nextOperator.Accessible)
				{
					image.Visible = false;
				}
				else
				{
					image.ImageUrl = imageIcon;
					image.AlternateText = imageAltText;
				}
			}
		}

		/// <summary>
		/// Occurs when the control displays an AlternativeStop record
		/// </summary>
		/// <param name="sender">An object reference to the Repeater control</param>
		/// <param name="e">RepeaterItemEventArgs used to obtain an object reference to the item</param>
		private void alternativeStops_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			//Process the current StopTaxiInformation representing an alternative stop. Create a new
			//TaxiInformationControl and render it in the placeholder provided
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				StopTaxiInformation alternativeStop = (StopTaxiInformation)e.Item.DataItem;
				TaxiInformationControl newControl = (TaxiInformationControl)LoadControl("TaxiInformationControl.ascx");
				newControl.data = alternativeStop;
				newControl.IsForAlternativeStops = true;
				e.Item.FindControl("NewAlternativeStop").Controls.Add(newControl);
			}
		}

		#endregion
	}
}
