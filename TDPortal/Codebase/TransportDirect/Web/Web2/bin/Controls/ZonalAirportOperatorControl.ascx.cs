// ****************************************
// Name:			ZonalAirportOperatorControl
//Author:			Darshan Sawe
//Date Created:		15-March-2007
//Description:		Displays Zonal Airport Operators in Location Information Page


	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.JourneyControl;

	namespace TransportDirect.UserPortal.Web.Controls
	{

	/// <summary>
	///		Summary description for ZonalAirportOperatorControl.
	/// </summary>
	public partial class ZonalAirportOperatorControl : TDUserControl
	{
		
		private string airportnaptan = String.Empty;								//Stores the currently selected Airport
		private bool isEmpty = true;										//Determines if any links are to be rendered
		private ServiceOperator[] arrayAirOperatorLinks;

		private IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

		#region Public Properties
		/// <summary>
		/// (read-write) Gets or Sets the naptan for this control
		/// </summary>
		public string AirportNaptan
		{
			get{ return airportnaptan; }
			set
			{ 
				airportnaptan = value;	
				GetAirportOperatorLinks();
			}
		}

		/// <summary>
		/// (read-only) Returns true if the control is to render links, false othewise
		/// </summary>
		public bool IsEmpty
		{
			get{ return isEmpty;		}
		}

		#endregion 

		private void GetAirportOperatorLinks()
		{
			ArrayList tempAirOperators = new ArrayList();
			ArrayList tempAirOperatorLinks = new ArrayList();
			ServiceOperator objMatchedZonalOperator;

			IOperatorCatalogue currentOperatorCatalogue = OperatorCatalogue.Current;

			tempAirOperators = airData.GetLocalZonalAirportOperators(airportnaptan);
			if(tempAirOperators != null)
			{
				foreach (OperatorKey operatorkey in tempAirOperators)
				{
					objMatchedZonalOperator = currentOperatorCatalogue.GetZonalOperatorLinks("Air", operatorkey.operatorcode, "Air");
					if(objMatchedZonalOperator == null)
					{
						objMatchedZonalOperator = new ServiceOperator();
					}
					objMatchedZonalOperator.Name = operatorkey.operatorname;
					tempAirOperatorLinks.Add(objMatchedZonalOperator);

				}
			}
			arrayAirOperatorLinks = (ServiceOperator[])tempAirOperatorLinks.ToArray(typeof(ServiceOperator));
			if(arrayAirOperatorLinks != null && arrayAirOperatorLinks.Length > 0)
			{
				isEmpty = false;
			}

		}

		// <summary>
		/// Runs before the control is rendered to the client
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			if(arrayAirOperatorLinks != null)
			{
				ZonalAirportOperatorRepeater.DataSource = arrayAirOperatorLinks;
				ZonalAirportOperatorRepeater.DataBind();
			}
			if(!IsEmpty)
			{
				base.OnPreRender(e);
			}
		}

		private void zonalAirOperatorRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				ServiceOperator airOperatorlink = (ServiceOperator)e.Item.DataItem;
				HyperLink link = (HyperLink)e.Item.FindControl("zonalAirportOperatorHyperLink");
				Label label = (Label) e.Item.FindControl("zonalAirportOperatorLabel");
				if(airOperatorlink.Url != null && airOperatorlink.Url.Length > 0)
				{
					link.Target = "_blank";
					link.NavigateUrl = airOperatorlink.Url;
                    link.Text = string.Format("{0} {1}", airOperatorlink.Name, GetResource("ExternalLinks.OpensNewWindowImage"));
					link.Visible = true;
				}
				else
				{
					label.Text = airOperatorlink.Name;
					label.Visible = true;
				}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Airport airport;

			ZonalAirportOperatorRepeater.ItemDataBound += new RepeaterItemEventHandler(zonalAirOperatorRepeater_ItemDataBound);
			airport = airData.GetAirportFromNaptan(airportnaptan);
			if(airport != null)
			{
				comments.Text = airport.Name;
				comments.Text += GetResource("LocationInformation.OperatorsComment.Text");
			}
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

		}
		#endregion
	}
}
