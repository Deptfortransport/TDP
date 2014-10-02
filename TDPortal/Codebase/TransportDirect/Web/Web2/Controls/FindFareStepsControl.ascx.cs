// ***************************************************************
// NAME                 : FindFareStepsControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 25/02/2008
// DESCRIPTION			: Find fare steps controled used to visually display the step in the process
//                      : of a search by price request
// ****************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareStepsControl.ascx.cs-arc  $
//
//   Rev 1.7   Jul 29 2011 15:08:08   mmodi
//Added ToolTip to images to show alt text in FireFox
//Resolution for 5715: CCN0635 - Search by price steps image content update
//
//   Rev 1.6   Mar 30 2010 14:24:32   mmodi
//Deleted commented out code
//
//   Rev 1.5   Mar 30 2010 14:20:04   mmodi
//When imagebutton is disabled, display a basic image instead
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Jan 26 2009 12:55:34   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Dec 17 2008 13:09:00   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:40   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 25 2008 17:00:00   mmodi
//Initial version
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.Web.Code;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class FindFareStepsControl : TDUserControl
    {
        #region Public Enums
        // Enum for FindFareStepsControl mode to display
        public enum FindFareStep
        {
            FindFareStep1,
            FindFareStep2,
            FindFareStep3,
            FindFareStep4
        }
        #endregion

        #region Private members

        private FindFareStep step;
        private bool printable = false;

        // Used for transition to steps
        private ITDSessionManager sessionManager;
        private FindCostBasedPageState pageState;
        private FindFareTicketSelectionAdapter ticketSelectionAdapter;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FindFareStepsControl()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}
		#endregion

        

        #region Page Load, PreRender
        /// <summary>
		/// Handler for the Init event. Sets up additional event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_Init(object sender, System.EventArgs e)
        {
        
            imagebuttonFindFareStep1.Click += new ImageClickEventHandler(imagebuttonFindFareStep1_Click);
            imagebuttonFindFareStep2.Click += new ImageClickEventHandler(imagebuttonFindFareStep2_Click);
            imagebuttonFindFareStep3.Click += new ImageClickEventHandler(imagebuttonFindFareStep3_Click);
            imagebuttonFindFareStep4.Click += new ImageClickEventHandler(imagebuttonFindFareStep4_Click);
        }

        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        /// <summary>
        /// Page PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadResources();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the text and images
        /// </summary>
        private void LoadResources()
        {
            // Default images to all be greyed out
            imagebuttonFindFareStep1.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep1, false);
            imagebuttonFindFareStep2.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep2, false);
            imagebuttonFindFareStep3.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep3, false);
            imagebuttonFindFareStep4.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep4, false);

            imageFindFareStep1.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep1, false);
            imageFindFareStep2.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep2, false);
            imageFindFareStep3.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep3, false);
            imageFindFareStep4.ImageUrl = GetFareStepImageUrl(FindFareStep.FindFareStep4, false);

            // Update the image and enable the appropriate buttons based on the step currently in
            switch (step)
            {
                case FindFareStep.FindFareStep2:
                    
                    imagebuttonFindFareStep2.ImageUrl = GetFareStepImageUrl(step, true);
                    imagebuttonFindFareStep3.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep3.Image.Blue.URL"));
                    
                    imageFindFareStep2.ImageUrl = GetFareStepImageUrl(step, true);
                    imageFindFareStep3.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep3.Image.Blue.URL"));

                    imagebuttonFindFareStep1.Visible = true;
                    imagebuttonFindFareStep2.Visible = false;
                    imagebuttonFindFareStep3.Visible = false;
                    imagebuttonFindFareStep4.Visible = false;

                    imageFindFareStep1.Visible = false;
                    imageFindFareStep2.Visible = true;
                    imageFindFareStep3.Visible = true;
                    imageFindFareStep4.Visible = true;

                    break;
                case FindFareStep.FindFareStep3:
                    
                    imagebuttonFindFareStep3.ImageUrl = GetFareStepImageUrl(step, true);
                    imagebuttonFindFareStep2.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep2.Image.Grey.URL"));
                    imagebuttonFindFareStep4.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep4.Image.Blue.URL"));

                    imageFindFareStep3.ImageUrl = GetFareStepImageUrl(step, true);
                    imageFindFareStep2.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep2.Image.Grey.URL"));
                    imageFindFareStep4.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep4.Image.Blue.URL"));

                    imagebuttonFindFareStep1.Visible = true;
                    imagebuttonFindFareStep2.Visible = true;
                    imagebuttonFindFareStep3.Visible = false;
                    imagebuttonFindFareStep4.Visible = false;

                    imageFindFareStep1.Visible = false;
                    imageFindFareStep2.Visible = false;
                    imageFindFareStep3.Visible = true;
                    imageFindFareStep4.Visible = true;

                    break;
                case FindFareStep.FindFareStep4:
                    
                    imagebuttonFindFareStep4.ImageUrl = GetFareStepImageUrl(step, true);
                    imagebuttonFindFareStep2.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep2.Image.Grey.URL"));

                    imageFindFareStep4.ImageUrl = GetFareStepImageUrl(step, true);
                    imageFindFareStep2.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("FindFareStepsControl.FindFareStep2.Image.Grey.URL"));

                    imagebuttonFindFareStep1.Visible = true;
                    imagebuttonFindFareStep2.Visible = true;
                    imagebuttonFindFareStep3.Visible = true;
                    imagebuttonFindFareStep4.Visible = false;

                    imageFindFareStep1.Visible = false;
                    imageFindFareStep2.Visible = false;
                    imageFindFareStep3.Visible = false;
                    imageFindFareStep4.Visible = true;

                    break;
                default:
                    
                    imagebuttonFindFareStep1.ImageUrl = GetFareStepImageUrl(step, true);

                    imageFindFareStep1.ImageUrl = GetFareStepImageUrl(step, true);

                    imagebuttonFindFareStep1.Visible = false;
                    imagebuttonFindFareStep2.Visible = false;
                    imagebuttonFindFareStep3.Visible = false;
                    imagebuttonFindFareStep4.Visible = false;

                    imageFindFareStep1.Visible = true;
                    imageFindFareStep2.Visible = true;
                    imageFindFareStep3.Visible = true;
                    imageFindFareStep4.Visible = true;

                    break;
            }

            
            imagebuttonFindFareStep1.AlternateText = GetResource("FindFareStepsControl.FindFareStep1.Image.AlternateText");
            imagebuttonFindFareStep2.AlternateText = GetResource("FindFareStepsControl.FindFareStep2.Image.AlternateText");
            imagebuttonFindFareStep3.AlternateText = GetResource("FindFareStepsControl.FindFareStep3.Image.AlternateText");
            imagebuttonFindFareStep4.AlternateText = GetResource("FindFareStepsControl.FindFareStep4.Image.AlternateText");

            imagebuttonFindFareStep1.ToolTip = imagebuttonFindFareStep1.AlternateText;
            imagebuttonFindFareStep2.ToolTip = imagebuttonFindFareStep2.AlternateText;
            imagebuttonFindFareStep3.ToolTip = imagebuttonFindFareStep3.AlternateText;
            imagebuttonFindFareStep4.ToolTip = imagebuttonFindFareStep4.AlternateText;
            
            imageFindFareStep1.AlternateText = GetResource("FindFareStepsControl.FindFareStep1.Image.AlternateText");
            imageFindFareStep2.AlternateText = GetResource("FindFareStepsControl.FindFareStep2.Image.AlternateText");
            imageFindFareStep3.AlternateText = GetResource("FindFareStepsControl.FindFareStep3.Image.AlternateText");
            imageFindFareStep4.AlternateText = GetResource("FindFareStepsControl.FindFareStep4.Image.AlternateText");

            imageFindFareStep1.ToolTip = imageFindFareStep1.AlternateText;
            imageFindFareStep2.ToolTip = imageFindFareStep2.AlternateText;
            imageFindFareStep3.ToolTip = imageFindFareStep3.AlternateText;
            imageFindFareStep4.ToolTip = imageFindFareStep4.AlternateText;
                       

            // disable all the image buttons in case of printer friendly page.
            if (printable)
            {
                imagebuttonFindFareStep1.Visible = false;
                imagebuttonFindFareStep2.Visible = false;
                imagebuttonFindFareStep3.Visible = false;
                imagebuttonFindFareStep4.Visible = false;

                imageFindFareStep1.Visible = true;
                imageFindFareStep2.Visible = true;
                imageFindFareStep3.Visible = true;
                imageFindFareStep4.Visible = true;
            }
        }

        private string GetFareStepImageUrl(FindFareStep step, bool active)
        {
            string imageString = "Image";
            string stepUrlResourceId = string.Empty;

            if (active)
            {
                imageString = "Image.Active";
            }

            stepUrlResourceId = string.Format("FindFareStepsControl.{0}.{1}.URL",step.ToString(),imageString);

            return ImageUrlHelper.GetAlteredImageUrl(GetResource(stepUrlResourceId));


        }

        
        #endregion

        #region Event handlers

       

        /// <summary>
        /// FindFareStep4 image button click event handler
        /// </summary>
        /// <param name="sender">FindFareSetp4 image button</param>
        /// <param name="e">Image click event arguments</param>
        protected void imagebuttonFindFareStep4_Click(object sender, ImageClickEventArgs e)
        {
            #region Go to Step 4
            if (pageState != null)
            {
                // If the User is viewing separate singles for Outward and Return then
                // show three buttons to allow them to buy either Outward or Return or both
                if (pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles)
                {
                    // Have to assume user wants to but both (outward, inward) single tickets
                    sessionManager.SetOneUseKey(SessionKey.FindFareBuyBothSingle, string.Empty);
                    sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
                }
                else
                {
                    // The User is viewing single or combined tickets
                    sessionManager.SetOneUseKey(SessionKey.FindFareBuySingleOrReturn, string.Empty);
                    sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
                }
            }
            #endregion
            
        }

        /// <summary>
        /// FindFareStep3 image button click event handler
        /// </summary>
        /// <param name="sender">FindFareSetp4 image button</param>
        /// <param name="e">Image click event arguments</param>
        protected void imagebuttonFindFareStep3_Click(object sender, ImageClickEventArgs e)
        {
            #region Go to Step 3
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneySummary;
            #endregion
            
        }

        /// <summary>
        /// FindFareStep2 image button click event handler
        /// </summary>
        /// <param name="sender">FindFareSetp4 image button</param>
        /// <param name="e">Image click event arguments</param>
        protected void imagebuttonFindFareStep2_Click(object sender, ImageClickEventArgs e)
        {
            #region Go to Step 2
            sessionManager.Session[SessionKey.Transferred] = false;
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneySummaryTicketSelectionBack;
            #endregion
            
        }

        /// <summary>
        /// FindFareStep1 image button click event handler
        /// </summary>
        /// <param name="sender">FindFareSetp4 image button</param>
        /// <param name="e">Image click event arguments</param>
        protected void imagebuttonFindFareStep1_Click(object sender, ImageClickEventArgs e)
        {
            #region Go to Step 1

            SetupSinglesOrReturns();

            if (ticketSelectionAdapter != null) // If ticketselection adapter set, use for convenience
            {
                ticketSelectionAdapter.BackToTravelDateSelection();
            }
            else
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareTicketSelectionBack;
            }
            #endregion
            
        }

        /// <summary>
        /// Sets the session values to the correct SelectedTravelDate, specific handling when user
        /// is viewing tickets in the two singles for a return (or normal return ) journey mode
        /// </summary>
        private void SetupSinglesOrReturns()
        {
            // If user was viewing singles (or returns) on date selection page and on singles (or returns) ticket selection page
            // switched to returns (or singles) using hyperlink, then switch the selected travel date back to singles (returns), 
            // so that next time they click "next" on the date selection page, they see singles (return) tickets first.
            // Note if no singles (return) tickets exist for the date, then the selected travel date will remain as return (single).

            if (pageState != null)
            {
                if ((pageState.SelectedTicketType == TicketType.Singles) && (pageState.SelectedReturnOutwardDate != null))
                {
                    TravelDate singlesTravelDate = pageState.SearchResult.GetTravelDate(
                        pageState.SelectedReturnOutwardDate,
                        pageState.SelectedReturnInwardDate,
                        pageState.SelectedTravelDate.TravelDate.TravelMode,
                        TicketType.Singles
                        );

                    if (singlesTravelDate != null)
                    {
                        pageState.SelectedTravelDateIndex =
                            pageState.FareDateTable.GetTravelDateIndex(singlesTravelDate, pageState.SearchResult);

                        pageState.SelectedTravelDate = new DisplayableTravelDate(singlesTravelDate, pageState.ShowChild, 0);
                    }
                }
                else if ((pageState.SelectedTicketType == TicketType.Return) && (pageState.SelectedSinglesOutwardDate != null))
                {
                    TravelDate returnsTravelDate = pageState.SearchResult.GetTravelDate(
                        pageState.SelectedSinglesOutwardDate,
                        pageState.SelectedSinglesInwardDate,
                        pageState.SelectedTravelDate.TravelDate.TravelMode,
                        TicketType.Return
                        );

                    if (returnsTravelDate != null)
                    {
                        pageState.SelectedTravelDateIndex =
                            pageState.FareDateTable.GetTravelDateIndex(returnsTravelDate, pageState.SearchResult);

                        pageState.SelectedTravelDate = new DisplayableTravelDate(returnsTravelDate, pageState.ShowChild, 0);
                    }
                }
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write property to set the printable mode of the control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        /// <summary>
        /// Read/write property to set the display mode of the control
        /// </summary>
        public FindFareStep Step
        {
            get { return step; }
            set { step = value; }
        }

        /// <summary>
        /// Read/write property. Ticket selection adapter to help in the transition to steps
        /// </summary>
        public FindFareTicketSelectionAdapter TicketSelectionAdapter
        {
            get { return ticketSelectionAdapter; }
            set { ticketSelectionAdapter = value; }
        }

        /// <summary>
        /// Read/write property. Page state to help with transition to steps
        /// </summary>
        public FindPageState PageState
        {
            get { return (FindPageState)pageState; }
            set
            {
                try
                {
                    pageState = (FindCostBasedPageState)value;
                }
                catch
                {
                    // Supplied PageState cannot be cast to FindCostBasedPageState
                    pageState = null;
                }
            }
        }

        /// <summary>
        /// Read/write property. Session manager to set transition to steps
        /// </summary>
        public ITDSessionManager SessionManager
        {
            get { return sessionManager; }
            set { sessionManager = value; }
        }

        #endregion
    }
}