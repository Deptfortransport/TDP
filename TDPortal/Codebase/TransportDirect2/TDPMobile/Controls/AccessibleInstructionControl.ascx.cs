// *********************************************** 
// NAME                 : AccessibleInstructionControl.ascx.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 06/08/2013
// DESCRIPTION          : Control to display accessible instruction and booking information
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.UserPortal.JourneyControl;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.Retail;
using TDP.Common.ServiceDiscovery;

namespace TDP.UserPortal.TDPMobile.Controls
{
    public partial class AccessibleInstructionControl : System.Web.UI.UserControl
    {
        #region Private members

        private PublicJourneyDetail publicJourneyDetail;
        private bool showAccessibleAssistance;
        private bool showAccessibleStepFree;
        private DateTime startTime;

        #endregion

        #region Page events

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
        public void Initialise(PublicJourneyDetail publicJourneyDetail, bool showAccessibleAssistance, 
                            bool showAccessibleStepFree, DateTime startTime)
        {
            this.publicJourneyDetail = publicJourneyDetail;
            this.showAccessibleAssistance = showAccessibleAssistance;
            this.showAccessibleStepFree = showAccessibleStepFree;
            this.startTime = startTime;
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
                    TDPPageMobile page = (TDPPageMobile)Page;

                    OperatorCatalogue operatorCatalogue = (OperatorCatalogue)TDPServiceDiscovery.Current.Get<OperatorCatalogue>(ServiceDiscoveryKey.OperatorService);

                    foreach (ServiceDetail serviceDetail in publicJourneyDetail.Services)
                    {
                        #region Find operator

                        // Determine the operator parameters
                        string operatorCode = serviceDetail.OperatorCode;
                        string serviceNumber = serviceDetail.ServiceNumber;
                        TDPModeType mode = publicJourneyDetail.Mode;
                        string region = publicJourneyDetail.Region;

                        // Find the operator
                        ServiceOperator serviceOperator = operatorCatalogue.GetAccessibleOperator(
                            operatorCode, serviceNumber, region, mode, startTime);

                        #endregion

                        if (serviceOperator != null)
                        {
                            bool isToday = (startTime.Date == DateTime.Now.Date);

                            // Build up the text dependent on the user accessible options selected and
                            // the operator accessible attributes

                            #region Set instruction text

                            // Step free instruction (takes priority over assistance)
                            if (showAccessibleStepFree && serviceOperator.WheelchairBookingAvailable)
                            {
                                // "Wheelchair booked in advance"
                                accessInstruction.Text = isToday ?
                                    page.GetResourceMobile("JourneyDetailsControl.AccessibleBooking.Wheelchair.Today") :
                                    page.GetResourceMobile("JourneyDetailsControl.AccessibleBooking.Wheelchair.Advanced");


                            }
                            // Assistance instruction
                            else if (showAccessibleAssistance && serviceOperator.AssistanceBookingAvailable)
                            {
                                // "Assistance booked in advance"
                                accessInstruction.Text = isToday ?
                                    page.GetResourceMobile("JourneyDetailsControl.AccessibleBooking.Assistance.Today") :
                                    page.GetResourceMobile("JourneyDetailsControl.AccessibleBooking.Assistance.Advanced");
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
                                    accessLink.Text = page.GetResourceMobile(resource);
                                    accessLink.ToolTip = page.GetResourceMobile(resource);

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
                                    accessPhone.Text = string.Format(page.GetResourceMobile(resource),
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