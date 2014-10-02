// *********************************************** 
// NAME                 : D2DPageOptionsControl.ascx.cs
// AUTHOR               : David Lane
// DATE CREATED         : 09/01/2013
// DESCRIPTION  : Control to display submit, clear, back and preferences buttons
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DPageOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.1   Jan 17 2013 09:45:50   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:33:58   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    ///	Control to display submit, clear, back and preferences buttons
    /// </summary>
    public partial class D2DPageOptionsControl : TDUserControl
    {
        #region Constants/variables

        private bool allowNext = false;
        private bool allowBack = false;
        private bool allowClear = false;
        private bool allowSave = false;

        #endregion

        #region Public events

        // The following lines declare objects that can be used as
        // keys in the EventHandlers table for the control.
        private static readonly object NextEventKey = new object();
        private static readonly object BackEventKey = new object();
        private static readonly object ClearEventKey = new object();
        private static readonly object SaveEventKey = new object();
                
        /// <summary>
        /// Occurs when the Next button is clicked
        /// </summary>
        public event EventHandler Next
        {
            add { this.Events.AddHandler(NextEventKey, value); }
            remove { this.Events.RemoveHandler(NextEventKey, value); }
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
        /// Occurs when the Clear button is clicked
        /// </summary>
        public event EventHandler Clear
        {
            add { this.Events.AddHandler(ClearEventKey, value); }
            remove { this.Events.RemoveHandler(ClearEventKey, value); }
        }

        /// <summary>
        /// Occurs when the Save button is clicked
        /// </summary>
        public event EventHandler Save
        {
            add { this.Events.AddHandler(SaveEventKey, value); }
            remove { this.Events.RemoveHandler(SaveEventKey, value); }
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
        }

        /// <summary>
        /// Sets control visibility depending on properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            // Load the appropriate image/test based on the parent page
            commandNext.Text = GetResource("FindPageOptionsControl.Submit.Text");
            commandBack.Text = GetResource("FindPageOptionsControl.Back.Text");
            commandClear.Text = GetResource("FindPageOptionsControl.Clear.Text");
            commandSave.Text = GetResource("FindPageOptionsControl.Save.Text");

            SetupVisibility();
        }


        /// <summary>
        /// Sets up the necessary event handlers.
        /// </summary>
        private void ExtraWiringEvents()
        {
            this.commandNext.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandBack.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandClear.Click += new EventHandler(this.OnCommandButtonClick);
            this.commandSave.Click += new EventHandler(this.OnCommandButtonClick);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Updates the visibility and the styling
        /// </summary>
        private void SetupVisibility()
        {
            commandNext.Visible = allowNext;
            commandBack.Visible = allowBack;
            commandClear.Visible = allowClear;
            commandSave.Visible = allowSave;

            if (!allowSave)
            {
                divBack.Visible = true;
                divSave.Visible = false;
                divClear.Visible = true;
                divNext.Visible = true;

                divBack.Attributes["class"] = "w33 l";
                divClear.Attributes["class"] = "w33 c";
                divNext.Attributes["class"] = "w33 r";
            }
            else if (allowSave && !allowBack)
            {
                divBack.Visible = false;
                divSave.Visible = true;
                divClear.Visible = true;
                divNext.Visible = true;

                divSave.Attributes["class"] = "w33 l";
                divClear.Attributes["class"] = "w33 c";
                divNext.Attributes["class"] = "w33 r";
            }
            else
            {
                divBack.Visible = true;
                divSave.Visible = true;
                divClear.Visible = true;
                divNext.Visible = true;

                divBack.Attributes["class"] = "w25 l";
                divSave.Attributes["class"] = "w25 c";
                divClear.Attributes["class"] = "w25 c";
                divNext.Attributes["class"] = "w25 r";
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///Read-Write property.  Controls visibility of the Next button
        /// </summary>
        public bool AllowNext
        {
            get { return allowNext; }
            set { allowNext = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the Back button
        /// </summary>
        public bool AllowBack
        {
            get { return allowBack; }
            set { allowBack = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the Clear button
        /// </summary>
        public bool AllowClear
        {
            get { return allowClear; }
            set { allowClear = value; }
        }

        /// <summary>
        ///Read-Write property.  Controls visibility of the Save button
        /// </summary>
        public bool AllowSave
        {
            get { return allowSave; }
            set { allowSave = value; }
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
                        
            if (sender.Equals(commandNext))
                theDelegate = this.Events[NextEventKey] as EventHandler;
            else if (sender.Equals(commandBack))
                theDelegate = this.Events[BackEventKey] as EventHandler;
            else if (sender.Equals(commandClear))
                theDelegate = this.Events[ClearEventKey] as EventHandler;
            else if (sender.Equals(commandSave))
                theDelegate = this.Events[SaveEventKey] as EventHandler;

            if (theDelegate != null)
                theDelegate(this, EventArgs.Empty);

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
