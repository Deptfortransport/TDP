// *********************************************** 
// NAME                 : AccessibleInstructionControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 03/12/2012
// DESCRIPTION          : Control to display accessible instruction and booking information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AccessibleInstructionControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Jan 25 2013 16:32:44   mmodi
//Open accessible linnk in new window
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Dec 10 2012 12:11:50   mmodi
//Get accessible opertor info using journey leg datetime
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 05 2012 14:27:18   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to display accessible instruction and booking information
    /// </summary>
    public partial class AccessibleInstructionControl : TDPrintableUserControl
    {
        #region Private members

        private PublicJourneyDetail publicJourneyDetail;
        private bool showAccessibleAssistance;
        private bool showAccessibleStepFree;

        #endregion

        #region Page_Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupAccessibleInstruction();

            SetVisibility();
        }

        #endregion

        #region Initialise

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(PublicJourneyDetail publicJourneyDetail, bool showAccessibleAssistance, bool showAccessibleStepFree)
        {
            this.publicJourneyDetail = publicJourneyDetail;
            this.showAccessibleAssistance = showAccessibleAssistance;
            this.showAccessibleStepFree = showAccessibleStepFree;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Displays the accessible information
        /// </summary>
        private void SetupAccessibleInstruction()
        {
            if (publicJourneyDetail != null)
            {
                if (publicJourneyDetail.Services != null)
                {
                    IOperatorCatalogue operatorCatalogue = OperatorCatalogue.Current;

                    foreach (ServiceDetails serviceDetail in publicJourneyDetail.Services)
                    {
                        #region Find operator

                        // Determine the operator parameters
                        string operatorCode = serviceDetail.OperatorCode;
                        string serviceNumber = serviceDetail.ServiceNumber;
                        ModeType mode = publicJourneyDetail.Mode;
                        string region = publicJourneyDetail.Region;
                        
                        // Find the operator
                        ServiceOperator serviceOperator = operatorCatalogue.GetAccessibleOperator(
                            operatorCode, serviceNumber, region, mode, publicJourneyDetail.StartTime);

                        #endregion

                        if (serviceOperator != null)
                        {
                            bool isToday = (publicJourneyDetail.StartTime.GetDateTime().Date == DateTime.Now.Date);

                            // Build up the text dependent on the user accessible options selected and
                            // the operator accessible attributes

                            #region Set instruction text

                            // Step free instruction (takes priority over assistance)
                            if (showAccessibleStepFree && serviceOperator.WheelchairBookingAvailable)
                            {
                                // "Wheelchair booked in advance"
                                accessInstruction.Text = isToday ?
                                    GetResource("JourneyDetailsControl.AccessibleBooking.Wheelchair.Today") :
                                    GetResource("JourneyDetailsControl.AccessibleBooking.Wheelchair.Advanced");

                                
                            }
                            // Assistance instruction
                            else if (showAccessibleAssistance && serviceOperator.AssistanceBookingAvailable)
                            {
                                // "Assistance booked in advance"
                                accessInstruction.Text = isToday ?
                                    GetResource("JourneyDetailsControl.AccessibleBooking.Assistance.Today") :
                                    GetResource("JourneyDetailsControl.AccessibleBooking.Assistance.Advanced");
                            }

                            #endregion

                            if (!string.IsNullOrEmpty(accessInstruction.Text))
                            {
                                accessInstruction.Visible = true;

                                #region Set link/phonenumber text

                                if (!string.IsNullOrEmpty(serviceOperator.BookingUrl))
                                {
                                    string resource = showAccessibleStepFree ?
                                        "JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Link" :
                                        "JourneyDetailsControl.AccessibleBooking.Assistance.Book.Link";

                                    // "Find out how to book" link
                                    accessLink.Text = string.Format(
                                        GetResource(resource), " " + GetResource("ExternalLinks.OpensNewWindowImage"));
                                    accessLink.ToolTip = string.Format(
                                        GetResource(resource), string.Empty);

                                    accessLink.Target = "_blank";
                                    accessLink.NavigateUrl = serviceOperator.BookingUrl;
                                    accessLink.Visible = true;
                                }
                                else if (!string.IsNullOrEmpty(serviceOperator.BookingPhoneNumber))
                                {
                                    string resource = showAccessibleStepFree ?
                                        "JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Phone" :
                                        "JourneyDetailsControl.AccessibleBooking.Assistance.Book.Phone";

                                    // "Book by phone"
                                    accessPhone.Text = string.Format(GetResource(resource),
                                        serviceOperator.BookingPhoneNumber);
                                    accessPhone.Visible = true;
                                }

                                #endregion
                            }

                            // Accessible details set for first found operator,
                            // break out. May need to rework logic if there are multiple services 
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the visibility of this control
        /// </summary>
        private void SetVisibility()
        {
            if ((!string.IsNullOrEmpty(accessInstruction.Text))
                || (!string.IsNullOrEmpty(accessLink.Text))
                || (!string.IsNullOrEmpty(accessPhone.Text)))
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }

        #endregion

    }
}