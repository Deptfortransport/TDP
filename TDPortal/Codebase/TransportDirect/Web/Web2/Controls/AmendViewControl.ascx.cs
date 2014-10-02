// *********************************************** 
// NAME                 : AmendViewControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 17/03/2005
// DESCRIPTION			: Time based /cost based control pane for the AmendSaveSend control.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendViewControl.ascx.cs-arc  $
//
//   Rev 1.3   May 08 2008 12:10:28   dgath
//Added two lines of code to Page_Load so that drop down selected value would not be lost on page load.
//Resolution for 4879: "View other Results" drop down selection for search by cost journeys has no effect.
//
//   Rev 1.2   Mar 31 2008 13:19:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:20   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.0   Jan 10 2006 15:23:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Nov 03 2005 17:08:50   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.4.1.0   Oct 24 2005 14:06:04   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.4   May 09 2005 13:08:04   rgeraghty
//Corrected submit button alt text
//
//   Rev 1.3   Apr 05 2005 12:15:18   rgeraghty
//Com visibility set to false (FxCop)
//
//   Rev 1.2   Mar 30 2005 10:03:20   COwczarek
//Provide get/set property for drop down selection rather than
//expose drop down control as property
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.1   Mar 29 2005 10:24:54   COwczarek
//Work in progress
//
//   Rev 1.0   Mar 18 2005 14:54:42   rgeraghty
//Initial revision.
namespace TransportDirect.UserPortal.Web.Controls
{
	#region using
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	#endregion
	
    /// <summary>
    /// Enumeration to identify selection from partition drop list
    /// </summary>
    public enum PartitionType {DoorToDoor, QuickPlanner, FindAFare};

	/// <summary>
	///	Enables a user to switch between time based and cost based journey results.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]	
    public partial class AmendViewControl : TDUserControl, ILanguageHandlerIndependent
    {

		
        private FindAMode timeBasedFindAMode;
        private TDSessionPartition currentPartition;
        private bool partitionSelectionAvailable;

        /// <summary>
        /// Constructor
        /// </summary>
        public AmendViewControl()
        {
            //use the find a fare resource manager for this control
            this.LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
        }


        #region Event Handlers
        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitialiseStaticControls();

            int dropListPartitionSelectedIndex = dropListPartition.SelectedIndex;
            PopulateDropDowns();
            dropListPartition.SelectedIndex = dropListPartitionSelectedIndex;
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
        /// Populates the drop down lists with the allowed values from DataServices
        /// </summary>
        private void PopulateDropDowns()
        {
            IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            if (this.timeBasedFindAMode == FindAMode.None)
            {
                populator.LoadListControl(DataServiceType.PartitionDoorDrop,dropListPartition);
            }
            else
            {
                populator.LoadListControl(DataServiceType.PartitionQuickDrop,dropListPartition);
            }
						
        }
		
        /// <summary>
        /// Initialise the static page controls
        /// </summary>
        private void InitialiseStaticControls() 
        {
            submitButton.Text = GetResource("AmendViewControl.submitButton.Text" );
			
            //set the label texts 
            labelInfo.Text =GetResource("AmendViewControl.labelInfo");
            labelPreference.Text =GetResource("AmendViewControl.labelPreference");
            labelResults.Text =GetResource("AmendViewControl.labelResults");
								
        }

        #endregion

        #region properties


        /// <summary>
        /// Read/write property - The currently set partition for the session
        /// </summary>
        /// <returns>Partition</returns>
        public TDSessionPartition CurrentPartition
        {
            get { return currentPartition; }
            set {currentPartition = value; }
        }

        /// <summary>
        /// Read/write property - True if partition selection is selectable false otherwise. 
        /// </summary>
        /// <returns>Boolean</returns>
        public bool PartitionSelectionAvailable
        {
            get { return partitionSelectionAvailable; }
            set {partitionSelectionAvailable = value; }
        }


        /// <summary>
        /// Read/write property - The Find A mode used to plan the time based journey
        /// </summary>
        /// <returns>FindAMode</returns>
        public FindAMode TimeBasedFindAMode
        {
            get { return timeBasedFindAMode; }
            set { timeBasedFindAMode = value;}
        }

        /// <summary>
        /// Read/write property - Get or set the selection in the partition selection drop list
        /// </summary>
        public PartitionType PartitionSelected 
        {
            get
            {
                if (dropListPartition.SelectedIndex == 0) 
                {
                    return PartitionType.FindAFare;
                } 
                else
                {
                    if (this.timeBasedFindAMode == FindAMode.None)
                    {
                        return PartitionType.DoorToDoor;
                    } 
                    else 
                    {
                        return PartitionType.QuickPlanner;
                    }
                }                        

            }
            set 
            {
                switch (value) 
                {
                    case PartitionType.FindAFare:
                        dropListPartition.SelectedIndex = 0;
                        break;
                    default:
                        dropListPartition.SelectedIndex = 1;
                        break;
                }
            }
        }

        /// <summary>
        /// Exposes the Submit image button so that the page containing the control can handle its events
        /// </summary>
        /// <returns>The control's Submit image button</returns>
        public TDButton SubmitButton
        {
            get{ return this.submitButton;}
        }

        #endregion

    }
}
