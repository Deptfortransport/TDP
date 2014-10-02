// *********************************************** 
// NAME                 : AmendCostSearchDayControl.ascx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28/02/2005
// DESCRIPTION			: This control allows the user to select an outward and 
//					      possibly inward travel date
// ************************************************
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendCostSearchDayControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:02   mturner
//Initial revision.
//
//   Rev 1.8   Jun 29 2007 11:51:00   asinclair
//Updated to change the label text depending on which button is being shown
//Resolution for 4454: 9.6: Find a train (Cost) Incorrect text on Amend date tab.
//
//   Rev 1.7   Feb 23 2006 19:16:18   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:23:16   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Nov 03 2005 17:08:44   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.5.1.0   Oct 21 2005 10:47:36   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.5   Apr 26 2005 11:06:16   COwczarek
//Prevent user from selecting a return date < outward date by
//hiding next/prev buttons appropriately.
//Resolution for 2099: PT: Find A Fare singles ticket selection page needs a link to return ticket selection page
//
//   Rev 1.4   Apr 22 2005 12:28:28   COwczarek
//Add comments
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;

	/// <summary>
	///    This control allows the user to select an outward and 
	///    possibly inward travel date
	/// </summary>
	public partial class AmendCostSearchDayControl : TDUserControl, ILanguageHandlerIndependent
	{
	    
	    /// <summary>
	    /// Label containing instructional copy 
	    /// </summary>
		protected System.Web.UI.WebControls.Label infoLabel;
		
		/// <summary>
		/// Label containing "Outward" 
		/// </summary>
		protected System.Web.UI.WebControls.Label outwardLabel;
		
		/// <summary>
		/// Label containing "Inward"
		/// </summary>
		protected System.Web.UI.WebControls.Label inwardLabel;
		
		/// <summary>
		/// Label containing currently selected outward date
		/// </summary>
		protected System.Web.UI.WebControls.Label currentOutwardDateLabel;
		
		/// <summary>
		/// Button for incrementing current outward date
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton nextOutwardDayButton;
		
		/// <summary>
		/// Button for decrementing current outward date
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton prevInwardDayButton;
		
		/// <summary>
		/// Label containing currently selected inward date
		/// </summary>
		protected System.Web.UI.WebControls.Label currentInwardDateLabel;
		
		/// <summary>
		/// Button for incrementing current inward date
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton nextInwardDayButton;
		
		/// <summary>
		/// Button for decrementing current inward date
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton prevOutwardDayButton;

		/// <summary>
		/// The current return date
		/// </summary>
		private TDDateTime currentReturnDate;
		
		/// <summary>
		/// The current outward date
		/// </summary>
		private TDDateTime currentOutwardDate;
		
		/// <summary>
		/// The earliest outward date that can be selected by this control
		/// </summary>
		private TDDateTime minOutwardDate;
		
		/// <summary>
		/// The latest outward date that can be selected by this control
		/// </summary>
		private TDDateTime maxOutwardDate;
		
		/// <summary>
		/// The earliest return date that can be selected by this control
		/// </summary>
		private TDDateTime minReturnDate;

		/// <summary>
		/// The latest return date that can be selected by this control
		/// </summary>		
		private TDDateTime maxReturnDate;

		/// <summary>
		/// True if this control should display and allow modification of return date,
		/// false otherwise.
		/// </summary>
		private bool showReturnDate;
		
		/// <summary>
		/// Format of date shown by this control
		/// </summary>
		private const string TIME_FORMAT = "ddd dd MMM yy";

		/// <summary>
		/// Event fired when current outward date is incremented or decremented
		/// </summary>
		public event System.EventHandler OutwardDateChanged;
		
		/// <summary>
		/// Event fired when current return date is incremented or decremented
		/// </summary>
		public event System.EventHandler ReturnDateChanged;

		#region Private methods

		/// <summary>
		/// Returns true if outward date is not modifiable because the earliest and latest
		/// permissible dates are equal, false othwise.
		/// </summary>
		/// <returns>true if outward date is not modifiable, false otherwise</returns>
		private bool hideOutwardDates() 
		{
			return minOutwardDate.Equals(maxOutwardDate);
		}

		/// <summary>
		/// Returns true if return date is not modifiable because the earliest and latest
		/// permissible dates are equal or if the ShowReturnDate property is true.
		/// </summary>
		/// <returns>true if return date is not modifiable, false otherwise</returns>
		private bool hideReturnDates() 
		{
			if (showReturnDate) 
			{
				return minReturnDate.Equals(maxReturnDate);
			} 
			else 
			{
				return true;
			}
		}

		/// <summary>
		/// Returns a new TDDateTime object created from the supplied TDDateTime object
		/// but with the time component set to zero.
		/// </summary>
		/// <param name="dateTime">The object from which to extract date value</param>
		/// <returns>New object with time component zereod</returns>
        private TDDateTime removeTime(TDDateTime dateTime) 
        {
            return new TDDateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

		#endregion Private methods

		#region Properties

		/// <summary>
		/// Read/write property that is true if this control should display and allow 
		/// modification of return date, false otherwise.
		/// </summary>
		public bool ShowReturnDate 
		{
			get {return showReturnDate;}
			set {showReturnDate = value;}
		}

		/// <summary>
		/// Read/write property that is the current outward date 
		/// </summary>
		public TDDateTime CurrentOutwardDate 
		{
			get {return currentOutwardDate;}
			set {currentOutwardDate = removeTime(value);}
		}

		/// <summary>
		/// Read/write property that is the current inward date
		/// </summary>
		public TDDateTime CurrentReturnDate 
		{
			get {return currentReturnDate;}
			set {currentReturnDate = removeTime(value);}
		}

		/// <summary>
		/// Read/write property that is the earliest return date that can be selected by this
		/// control
		/// </summary>
		public TDDateTime MinReturnDate 
		{
			get {return minReturnDate;}
			set {minReturnDate = removeTime(value);}
		}

		/// <summary>
		/// Read/write property that is the latest return date that can be selected by this
		/// control
		/// </summary>
		public TDDateTime MaxReturnDate 
		{
			get {return maxReturnDate;}
			set {maxReturnDate = removeTime(value);}
		}

		/// <summary>
		/// Read/write property that is the earliest outward date that can be selected by this
		/// control
		/// </summary>
		public TDDateTime MinOutwardDate
		{
			get {return minOutwardDate;}
			set {minOutwardDate = removeTime(value);}
		}

		/// <summary>
		/// Read/write property that is the latest outward date that can be selected by this
		/// control
		/// </summary>
		public TDDateTime MaxOutwardDate
		{
			get {return maxOutwardDate;}
			set {maxOutwardDate = removeTime(value);}
		}

		/// <summary>
		/// Read only property that returns true if this control is showing dates that
		/// can be modified. If the min and max dates supplied are equal then there is no
		/// flexibility and dates cannot be modified in which case false is returned.
		/// </summary>
		public bool DatesAreFlexible
		{
			get 
			{
				if (!showReturnDate) 
				{
					return !minOutwardDate.Equals(maxOutwardDate);
				} 
				else 
				{
					return !(minReturnDate.Equals(maxReturnDate) &&
						minOutwardDate.Equals(maxOutwardDate));
				}
				
			}
		}

		#endregion Properties

		#region Protected methods

		/// <summary>
		/// Constructor. Sets a local resource manager.
		/// </summary>
		protected AmendCostSearchDayControl() 
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}

		#endregion Protected methods

		#region Event handlers

		/// <summary>
		/// Event handler called when page load event is fired
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Event handler called when pre-render event is fired
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
            if (this.Visible) 
            {
                infoLabel.Text = GetResource("AmendCostSearchDayControl.infoLabel.Text");

                outwardLabel.Text = GetResource("AmendCostSearchDayControl.outwardLabel.Text");
                inwardLabel.Text = GetResource("AmendCostSearchDayControl.inwardLabel.Text");

                nextOutwardDayButton.Text = GetResource("AmendCostSearchDayControl.nextDayButton.Text");
                nextInwardDayButton.Text = GetResource("AmendCostSearchDayControl.nextDayButton.Text");
                prevOutwardDayButton.Text = GetResource("AmendCostSearchDayControl.previousDayButton.Text");
                prevInwardDayButton.Text = GetResource("AmendCostSearchDayControl.previousDayButton.Text");

                if (currentOutwardDate != null) 
                {
                    currentOutwardDateLabel.Text = currentOutwardDate.ToString(TIME_FORMAT);
                }
                if (currentReturnDate != null) 
                {
                    currentInwardDateLabel.Text = currentReturnDate.ToString(TIME_FORMAT);
                }

                if (hideOutwardDates()) 
                {
                    HtmlTableRow outwardDateRow = (HtmlTableRow)FindControl("outwardDateRow");
                    outwardDateRow.Visible = false;
                }

                if (hideReturnDates()) 
                {
                    HtmlTableRow inwardDateRow = (HtmlTableRow)FindControl("inwardDateRow");
                    HtmlTableCell inwardLabelCell = (HtmlTableCell)FindControl("inwardLabelCell");
                    HtmlTableCell outwardLabelCell = (HtmlTableCell)FindControl("outwardLabelCell");
                    inwardDateRow.Visible = false;
                    outwardLabelCell.Visible = false;
                    inwardLabelCell.Visible = false;
                }

                if (!hideOutwardDates()) 
                {
                    // Only enable outward prev day button if its > min outward date
                    prevOutwardDayButton.Visible = currentOutwardDate > minOutwardDate;
                    // Only enable outward next day button if its < max outward date and < current 
                    // return date if the return date is showing
                    nextOutwardDayButton.Visible = ((!showReturnDate && currentOutwardDate < maxOutwardDate)) ||
                        ((showReturnDate && currentOutwardDate < maxOutwardDate) && currentOutwardDate < currentReturnDate);
                }

                if (!hideReturnDates()) 
                {
                    // Only enable inward prev day button if its > min inward date and > current outward date
                    prevInwardDayButton.Visible = (currentReturnDate > minReturnDate) && (currentReturnDate > currentOutwardDate);
                    // Only enable inward next day button if its < max inward date 
                    nextInwardDayButton.Visible = currentReturnDate < maxReturnDate;
                }

				if(prevOutwardDayButton.Visible && !nextOutwardDayButton.Visible)
				{
					infoLabel.Text = GetResource("AmendCostSearchDayControl.infoLabelPrevious.Text");
				}
				else if(!prevOutwardDayButton.Visible && nextOutwardDayButton.Visible)
				{
					infoLabel.Text = GetResource("AmendCostSearchDayControl.infoLabelNext.Text");
				}
            }
		}

		/// <summary>
		/// Event handler called when button to increment current selected outward day is clicked.
		/// The method fires the OutwardDateChanged event.
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void nextOutwardDayButton_Click(object sender, EventArgs e)
		{
			currentOutwardDate = currentOutwardDate.AddDays(1);
            if (OutwardDateChanged != null) 
            {
                OutwardDateChanged(this,null);
            }
		}

		/// <summary>
		/// Event handler called when button to decrement current selected outward day is clicked.
        /// The method fires the OutwardDateChanged event.
        /// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void prevOutwardDayButton_Click(object sender, EventArgs e)
		{
			currentOutwardDate = currentOutwardDate.AddDays(-1);
            if (OutwardDateChanged != null) 
            {
                OutwardDateChanged(this,null);
            }
		}

		/// <summary>
		/// Event handler called when button to decrement current selected return day is clicked.
        /// The method fires the ReturnDateChanged event.
        /// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void prevInwardDayButton_Click(object sender, EventArgs e)
		{
			currentReturnDate = currentReturnDate.AddDays(-1);
            if (ReturnDateChanged != null) 
            {
                ReturnDateChanged(this,null);
            }
		}

		/// <summary>
		/// Event handler called when button to increment current selected return day is clicked.
        /// The method fires the ReturnDateChanged event.
        /// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void nextInwardDayButton_Click(object sender, EventArgs e)
		{
			currentReturnDate = currentReturnDate.AddDays(1);
            if (ReturnDateChanged != null) 
            {
                ReturnDateChanged(this,null);
            }		
		}

		#endregion Event handlers

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			this.PreRender += new System.EventHandler(this.Page_PreRender);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.prevOutwardDayButton.Click += new EventHandler(this.prevOutwardDayButton_Click);
			this.nextOutwardDayButton.Click += new EventHandler(this.nextOutwardDayButton_Click);
			this.prevInwardDayButton.Click += new EventHandler(this.prevInwardDayButton_Click);
			this.nextInwardDayButton.Click += new EventHandler(this.nextInwardDayButton_Click);

		}
		#endregion

	}
}
