// ********************************************************
// NAME 		: SeasonalNoticeBoardControl.ascx.cs
// AUTHOR 		: Sanjeev Chand.
// DATE CREATED : 26/10/2004
// DESCRIPTION 	: Control will display seasonal notice board data. 
// *********************************************************

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for SeasonalNoticeBoardControl.
	///		This control 
	/// </summary>
	public partial  class SeasonalNoticeBoardControl : TDUserControl
	{

		//protected System.Web.UI.WebControls.Repeater RepeaterNews;
		
		

		
		#region "Control member fields"
		private DataTable  _DataSource = null;
		private bool _HasData = false;
		private bool _Visible = true;
		private string _lblRegionText = "";
		private string _lblTransportModeText = "";		
		private string _lblInformationText = "";
		private string _lblEffectedDatesText = "";
		private string _lblLastUpdatedText = ""; 
		private string _lblNoticeHeadingText = "";
		private string _NoDataFoundMessageText = "";
		private string _TableSummaryText = "";
		protected System.Web.UI.WebControls.Label lblRegion;
		protected System.Web.UI.WebControls.Label lblTransportMode;
		protected System.Web.UI.WebControls.Label lblInformation;
		protected System.Web.UI.WebControls.Label lblEffectedDates;
		protected System.Web.UI.WebControls.Label lblLastUpdated;
		private bool _NoticeHeaderVisible =true ;

		#endregion
		
		
		#region "Table Header Text Properties"

		/// <summary>
		///		public string RegionText will get/set the column header 
		/// </summary>
		public string RegionText
		{
			get
			{
				return (string) _lblRegionText;
			}
			set 
			{
				_lblRegionText = (string) value;
			}
		}
		

			
		
		/// <summary>
		///		public string TransportModeText will get/set the column header 
		/// </summary>
		public string TransportModeText
		{
			get
			{
				return (string) _lblTransportModeText;
			}
			set 
			{
				_lblTransportModeText = (string) value;
			}
		}

		
		




		/// <summary>
		///		public string InformationText will get/set the column header 
		/// </summary>
		public string InformationText
		{
			get
			{
				return (string) _lblInformationText;
			}

			set 
			{
				_lblInformationText = (string) value;
			}
		}
		
		

		/// <summary>
		///		public string EffectedDatesText will get/set the column header 
		/// </summary>
		public string EffectedDatesText
		{
			get
			{
				return (string) _lblEffectedDatesText;
			}

			set 
			{
				_lblEffectedDatesText = (string) value;
			}
		}


		/// <summary>
		///		public string LastUpdatedText will get/set the column header 
		/// </summary>

		public string LastUpdatedText
		{
			get
			{
				return (string) _lblLastUpdatedText;
			}


			set 
			{
				_lblLastUpdatedText = (string) value;
			}
		}


		#endregion
		
		#region "Control properties"

		/// <summary>
		///		public bool NoticeHeaderVisible will make  Notice Header Visible or not
		/// </summary>
		
		public bool NoticeHeaderVisible
		{
			get
			{
				return (bool) _NoticeHeaderVisible;
			}

			set 
			{
				_NoticeHeaderVisible = (bool) value;
			}
		}

		/// <summary>
		///		public string NoticeHeadingText will get/set the heading text
		/// </summary>
		public string NoticeHeadingText
		{
			get
			{
				return (string) _lblNoticeHeadingText;
			}

			set 
			{
				_lblNoticeHeadingText = (string) value;
			}
		}
		

		/// <summary>
		///		public string NoticeHeadingText will get/set the heading text
		/// </summary>
		public string NoDataFoundMessage
		{
			get
			{
				return (string) _NoDataFoundMessageText;
			}

			set 
			{
				_NoDataFoundMessageText = (string) value;
			}
		}
		
		/// <summary>
		///		TableSummaryText property will get/set the table summary property for WAI purpose
		/// </summary>
		public string TableSummaryText
		{
			get
			{
				return (string) _TableSummaryText;
			}
			set
			{
				_TableSummaryText  = (string) value;

			}
		}

		


		/// <summary>
		///		DataSource property will get/set the data for this control
		/// </summary>
		public DataTable  DataSource
		{
			get
			{
				return (DataTable) _DataSource;
			}
			set
			{
				_DataSource = (DataTable) value;
			}
		}


		/// <summary>
		///		ReadOnly property Hasdata to indicate whether control has data or not
		/// </summary>
		public bool HasData
		{
			get
			{
				return (bool) _HasData;
			}
			
		}
		


		/// <summary>
		///		ReadOnly property Visible to set/get visible property
		/// </summary>		
		public bool IsVisible
		{
			get
			{
				return (bool) _Visible;
			}
			set
			{
				_Visible = (bool) value;
			}
		}
		

			
		#endregion
		
		#region "Control methods"
		/// <summary>
		///		public void DisplayData() responsible for binding data to the control.
		/// </summary>		
		/// 
		public void DisplayData()
		{

			try
			{	
				this.lblHeadingText.Text  =  NoticeHeadingText.ToString(); 
				//this.repeaterSeasonalNews.Visible =  this.IsVisible ;
				this.lblHeadingText.Visible = NoticeHeaderVisible;
 
				if (this.DataSource != null ) 
				{
					this.seasonalNoticeBoardPanel.Visible  = true;
					this.repeaterSeasonalNews.DataSource = this.DataSource;
					this.repeaterSeasonalNews.DataBind();  
				}
				else
				{
					this.lblHeadingText.Visible = false;
					this.seasonalNoticeBoardPanel.Visible = false; 
				}
				
			}
			catch(Exception ex )
			{
				throw ex ;
			}
			finally
			{
				if (this.repeaterSeasonalNews.Items.Count > 0) 
				{
					_HasData = true;
				}
				else 
				{	
					_HasData = false;					
				}  
				AdjustControl();
			}		
			
		}

		
		// <summary>
		//		public void AdjustControl() will adjust the control to show/hide the data.
		// </summary>
				protected void AdjustControl()
				{		
					if (this.HasData)
					{
						this.seasonalNoticeBoardPanel.Visible = true; 						
						this.lblSeasonalNews_NoData.Visible = false; 
					}
					else
					{
						this.seasonalNoticeBoardPanel.Visible = false;						
						this.lblSeasonalNews_NoData.Visible = true; 
						this.lblSeasonalNews_NoData.Text = NoDataFoundMessage.ToString();
					}
					
						
		 
				}
				
        
		#endregion


		//System.Data.DataSet 
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
