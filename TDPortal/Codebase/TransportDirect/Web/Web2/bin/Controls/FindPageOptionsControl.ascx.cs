// *********************************************** 
// NAME                 : FindPageOptionsControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 12/07/2004
// DESCRIPTION  : Control to display submit, clear, back and preferences buttons
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindPageOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Feb 10 2009 16:06:42   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:22   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:16:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.0   Jan 10 2006 15:24:50   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Nov 04 2005 11:35:18   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.15   Nov 01 2005 15:11:42   build
//Automatically merged from branch for stream2638
//
//   Rev 1.14.1.3   Oct 24 2005 19:32:08   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.14.1.2   Oct 12 2005 12:50:22   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.14.1.1.1.0   Oct 20 2005 09:21:26   asinclair
//Corrected Langstrings entrires
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.14.1.1   Oct 10 2005 19:12:58   asinclair
//Updated after CodeReview
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.14.1.0   Oct 10 2005 11:36:50   asinclair
//Updated for VisitPlanner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.14   May 19 2005 12:19:44   ralavi
//Modifications as the result of FXCop run
//
//   Rev 1.13   Apr 13 2005 12:11:16   Ralavi
//Uncommenting displayPanels
//
//   Rev 1.12   Mar 23 2005 11:18:30   RAlavi
//Modifications to the UI format
//
//   Rev 1.11   Mar 18 2005 10:33:48   RAlavi
//added code for allowClear
//
//   Rev 1.10   Mar 10 2005 11:08:14   tmollart
//Fixed bug which meant same image was displayed for showing preferences regardless of the current page.
//
//   Rev 1.9   Feb 25 2005 10:36:24   RAlavi
//Adding code for next button to work on the control
//
//   Rev 1.8   Feb 23 2005 16:32:58   RAlavi
//Changed for car costing
//
//   Rev 1.7   Feb 22 2005 10:02:14   RAlavi
//Checked in after modification relating to car costing
//
//   Rev 1.6   Aug 06 2004 14:48:18   esevern
//corrected show preferences alt text setting (was showing image url)
//
//   Rev 1.5   Jul 20 2004 15:24:58   jgeorge
//Removed properties from ViewState
//
//   Rev 1.4   Jul 20 2004 14:04:50   COwczarek
//Use CurrentUICutlure rather than CurrentCulture when accessing langstrings
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 19 2004 11:47:44   jgeorge
//Bug fix
//
//   Rev 1.2   Jul 14 2004 12:47:50   jgeorge
//Updated after testing
//
//   Rev 1.1   Jul 13 2004 10:59:42   jgeorge
//Interim check-in
//
//   Rev 1.0   Jul 13 2004 10:53:04   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common.ResourceManager;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using TransportDirect.Common;

    using TransportDirect.Web.Support;

    /// <summary>
    ///	Control to display submit, clear, back and preferences buttons
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class FindPageOptionsControl : TDUserControl
    {
        #region Controls

        
        protected PtPreferencesControl ptPreferencesControl;

        #endregion

        #region Constants/variables

        // Keys used to obtain strings from the resource file
        private const string BackImageTextKey = "FindPageOptionsControl.Back.Text";
        private const string ClearImageTextKey = "FindPageOptionsControl.Clear.Text";
        private const string SubmitImageTextKey = "FindPageOptionsControl.Submit.Text";

        private const string ShowAdvancedOptionsTextKey = "FindPageOptionsControl.ShowAdvancedOptions.Text";
        private const string HideAdancedOptionsTextKey = "FindPageOptionsControl.HideAdvancedOptions.Text";

        // Keys used to save/load elements in the viewstate
        private const int AllowBackKey = 2;
        private const int MaxViewStateIndex = 2;

        private bool allowBack;
        private bool allowClear = true;
        private bool allowNext = true;

        //Added for VisitPlanner
        private bool allowShowAdvancedOptions;
        private bool allowHideAdvancedOptions;

        #endregion

        #region Public events

        // The following lines declare objects that can be used as
        // keys in the EventHandlers table for the control.
        private static readonly object SubmitEventKey = new object();
        private static readonly object ClearEventKey = new object();
        private static readonly object BackEventKey = new object();

        private static readonly object ShowAdvancedOptionsKey = new object();
        private static readonly object HideAdvancedOptionsKey = new object();

        /// <summary>
        /// Occurs when the Submit button is clicked
        /// </summary>
        public event EventHandler Submit
        {
            add { this.Events.AddHandler(SubmitEventKey, value); }
            remove { this.Events.RemoveHandler(SubmitEventKey, value); }
        }

        /// <summary>
        /// Occurs when the Clear button is clicked
        /// </summary>
        public event EventHandler Clear
        {
            add { this.Events.AddHandler(ClearEventKey, value); }
            remove { this.Events.RemoveHandler(ClearEventKey, value); }
        }

        /// <summary>
        /// Occurs when the Back button is clicked
        /// </summary>
        public event EventHandler Back
        {
            add { this.Events.AddHandler(BackEventKey, value); }
            remove { this.Events.RemoveHandler(BackEventKey, value); }
        }

        /// <summary>
        /// Occurs when the 'Advanced options' button is clicked
        /// </summary>
        public event EventHandler ShowAdvancedOptions
        {
            add { this.Events.AddHandler(ShowAdvancedOptionsKey, value); }
            remove { this.Events.AddHandler(ShowAdvancedOptionsKey, value); }
        }

        /// <summary>
        /// Occurs when the 'Hide advanced options' button is clicked
        /// </summary>
        public event EventHandler HideAdvancedOptions
        {
            add { this.Events.AddHandler(HideAdvancedOptionsKey, value); }
            remove { this.Events.AddHandler(HideAdvancedOptionsKey, value); }

        }

        #endregion

        #region Page lifecycle event handlers

        /// <summary>
        /// Handler for the Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Load the appropriate image/test based on the parent page
            TDResourceManager rm = Global.tdResourceManager;
            commandBack.Text = rm.GetString(BackImageTextKey, TDCultureInfo.CurrentUICulture);
            commandClear.Text = rm.GetString(ClearImageTextKey, TDCultureInfo.CurrentUICulture);
            commandSubmit.Text = rm.GetString(SubmitImageTextKey, TDCultureInfo.CurrentUICulture);

            commandHideAdvancedOptions.Text = rm.GetString(HideAdancedOptionsTextKey, TDCultureInfo.CurrentUICulture);
            commandShowAdvancedOptions.Text = rm.GetString(ShowAdvancedOptionsTextKey, TDCultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Sets control visibility depending on properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            commandBack.Visible = allowBack;
            switch (PageId)
            {
                case PageId.JourneyPlannerAmbiguity:
                    commandClear.Visible = !allowClear;
                    break;

                default:
                    commandClear.Visible = allowClear;
                    break;
            }

            commandSubmit.Visible = allowNext;

            commandHideAdvancedOptions.Visible = allowHideAdvancedOptions;
            commandShowAdvancedOptions.Visible = allowShowAdvancedOptions;
        }


        /// <summary>
        /// Sets up the necessary event handlers.
        /// </summary>
        private void ExtraWiringEvents()
        {
            this.commandBack.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandClear.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandSubmit.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandHideAdvancedOptions.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandHideAdvancedOptions.Click += new EventHandler(this.commandHideAdvancedOptions_Click);
            this.commandShowAdvancedOptions.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandShowAdvancedOptions.Click += new EventHandler(this.commandShowAdvancedOptions_Click);
        }





        #endregion

        #region Properties

        /// <summary>
        ///Read-Write property.  Controls visibility of the back button
        /// </summary>
        public bool AllowBack
        {
            get { return allowBack; }
            set { allowBack = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the clear button
        /// </summary>
        public bool AllowClear
        {
            get { return allowClear; }
            set { allowClear = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the "Advanced options" button
        /// </summary>
        public bool AllowShowAdvancedOptions
        {
            get { return allowShowAdvancedOptions; }
            set { allowShowAdvancedOptions = value; }
        }

        /// <summary>
        /// Read-Write property. Controls visibility of the "Hide advanced options" button
        /// </summary>
        public bool AllowHideAdvancedOptions
        {
            get { return allowHideAdvancedOptions; }
            set { allowHideAdvancedOptions = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the Next button
        /// </summary>
        public bool AllowNext
        {
            get { return allowNext; }
            set { allowNext = value; }
        }


        #endregion

        #region Control event handler

        /// <summary>
        /// Handles the click event of the command buttons and raises the appropriate event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCommandButtonClick(object sender, EventArgs e)
        {
            EventHandler theDelegate = null;

            if (sender.Equals(commandBack))
                theDelegate = this.Events[BackEventKey] as EventHandler;
            else if (sender.Equals(commandClear))
                theDelegate = this.Events[ClearEventKey] as EventHandler;
            else if (sender.Equals(commandSubmit))
                theDelegate = this.Events[SubmitEventKey] as EventHandler;
            else if (sender.Equals(commandHideAdvancedOptions))
                theDelegate = this.Events[HideAdvancedOptionsKey] as EventHandler;
            else if (sender.Equals(commandShowAdvancedOptions))
                theDelegate = this.Events[ShowAdvancedOptionsKey] as EventHandler;

            if (theDelegate != null)
                theDelegate(this, EventArgs.Empty);

        }
        /// <summary>
        /// Event handler for the show advanced options click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandShowAdvancedOptions_Click(object sender, EventArgs e)
        {
            allowHideAdvancedOptions = true;
        }
        /// <summary>
        /// Event handler for the hide advanced options click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandHideAdvancedOptions_Click(object sender, EventArgs e)
        {
            allowShowAdvancedOptions = true;
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {

            ExtraWiringEvents();
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
