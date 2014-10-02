// *********************************************** 
// NAME                 : StopInformationDepartureBoardResultControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information departure board result control displays departure board result grid
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationDepartureBoardResultControl.ascx.cs-arc  $
//
//   Rev 1.12   Mar 26 2010 12:00:22   RHopkins
//Reduce number of calls made to CJP when showing Departure Boards and Stop Events
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.11   Feb 22 2010 11:58:38   rhopkins
//Pass Operator Code in Stop Event requests
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.10   Jan 08 2010 09:20:24   apatel
//Updated real time status to show more like mobile emulator.
//Resolution for 5356: Stop Information Departure board don't display "On time" and "Cancelled" status
//
//   Rev 1.9   Jan 07 2010 09:52:42   apatel
//Resolved the issue with departure board not showing status "On time" and "Cancelled" for Train
//Resolution for 5356: Stop Information Departure board don't display "On time" and "Cancelled" status
//
//   Rev 1.8   Nov 22 2009 15:57:54   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.7   Nov 18 2009 16:00:28   pghumra
//Code fixes for TDP release patch 10.8.2.3 - Stop Information page
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.6   Oct 27 2009 09:53:22   apatel
//Stop Information Departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Oct 23 2009 13:25:24   apatel
//Added check for the Real Time to be null
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Oct 23 2009 09:05:04   apatel
//Stop info departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 15 2009 14:49:02   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Oct 06 2009 14:41:40   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:36   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.ScreenFlow;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information departure board result control
    /// </summary>
    public partial class StopInformationDepartureBoardResultControl : TDUserControl
    {
        #region Private Fields
        private bool showDeparture = true;
        private TDStopType stopType = TDStopType.Unknown;
        private DBSResult result = null;
        private String detailBaseUrl;

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindDBSResult();
        }

        #endregion
        
        #region Control Events
        /// <summary>
        /// DepBoardServicegrid row data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DepBoardServiceGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (stopType == TDStopType.Rail)
            {
                if (e.Row.Cells.Count > 0)
                {
                    e.Row.Cells[0].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label labelServiceNumberHeading = e.Row.FindControl("labelServiceNumberHeading") as Label;
                Label labelServiceDetailHeading = e.Row.FindControl("labelServiceDetailHeading") as Label;
                Label ServiceDetailLinkHeading = e.Row.FindControl("ServiceDetailLinkHeading") as Label;
                Label labelRealTimeInfoHeading = e.Row.FindControl("labelRealTimeInfoHeading") as Label;

                labelServiceNumberHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceNumberHeading.train.Text");

                if (stopType == TDStopType.Rail)
                {
                    labelServiceNumberHeading.Visible = false;
                }

                if (stopType == TDStopType.Bus)
                {
                    labelServiceNumberHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceNumberHeading.bus.Text");
                }

                if (showDeparture)
                {
                    labelServiceDetailHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceDetailHeading.departure.Text");
                    ServiceDetailLinkHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceNumberHeading.departure.Text");

                }
                else
                {
                    labelServiceDetailHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceDetailHeading.arrival.Text");
                    ServiceDetailLinkHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelServiceNumberHeading.arrival.Text");

                }

                labelRealTimeInfoHeading.Text = GetResource("StopInformationDepartureBoardResultControl.labelRealTimeInfoHeading.Text");

            }

            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label labelServiceNumber = e.Row.FindControl("labelServiceNumber") as Label;
                Label labelServiceDetail = e.Row.FindControl("labelServiceDetail") as Label;
                HyperLink ServiceDetailLink = e.Row.FindControl("ServiceDetailLink") as HyperLink;
                Label labelRealTimeInfo = e.Row.FindControl("labelRealTimeInfo") as Label;

                DBSStopEvent stopEvent = e.Row.DataItem as DBSStopEvent;

                labelServiceNumber.Text = stopEvent.Service.ServiceNumber;

                if (stopType == TDStopType.Rail)
                {
                    labelServiceNumber.Visible = false;
                }

                labelServiceDetail.Text = stopEvent.Stop.Stop.Name;

                if (!showDeparture)
                {
                    labelServiceDetail.Text = stopEvent.Departure == null ? string.Empty : stopEvent.Departure.Stop.Name;
                    ServiceDetailLink.Text = stopEvent.Stop.ArriveTime.ToString("HH:mm");


                }
                else
                {
                    labelServiceDetail.Text = stopEvent.Arrival == null ? string.Empty : stopEvent.Arrival.Stop.Name;
                    ServiceDetailLink.Text = stopEvent.Stop.DepartTime.ToString("HH:mm");
                }

                // Get the appropriate time and encode it so that it can be passed in the query string
                DateTime serviceTime = showDeparture ? stopEvent.Stop.DepartTime : stopEvent.Stop.ArriveTime;
                string encodedServiceTime = HttpUtility.UrlEncode(serviceTime.ToString());
                string encodedServiceNumber = HttpUtility.UrlEncode(stopEvent.Service.ServiceNumber);

                // Build the link for the Stop Details page
                StringBuilder linkString = new StringBuilder(100);
                linkString.Append(detailBaseUrl);
                linkString.Append("?servicetime=");
                linkString.Append(encodedServiceTime);
                linkString.Append("&servicenumber=");
                linkString.Append(encodedServiceNumber);
                linkString.Append("&operatorcode=");
                linkString.Append(stopEvent.Service.OperatorCode);

                ServiceDetailLink.NavigateUrl = linkString.ToString();	  

                labelRealTimeInfo.Text = GetRealTimeInfo(stopEvent);
            }
        }


        #endregion

        #region Public Methods
                
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="result"></param>
        /// <param name="stopType"></param>
        /// <param name="showDeparture"></param>
        public void Initialise(DBSResult result, TDStopType stopType, bool showDeparture)
        {

            this.showDeparture = showDeparture;

            this.stopType = stopType;

            this.result = result;

            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.StopServiceDetails);

            detailBaseUrl = @"~/" + pageTransferDetails.PageUrl;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the real time info text
        /// </summary>
        /// <param name="stopEvent">DBS stop event</param>
        /// <returns>real time information text</returns>
        private string GetRealTimeInfo(DBSStopEvent stopEvent)
        {
            string realTimeText = GetResource("StopInformationDepartureBoardResultControl.RealTimeText.NoReport.Text");

            if (stopEvent.Stop.RealTime != null)
            {
                if (stopEvent.Stop.RealTime.DepartTime != DateTime.MinValue
                    || stopEvent.Stop.RealTime.ArriveTime != DateTime.MinValue)
                {
                    if (showDeparture)
                    {
                        if (stopEvent.Stop.RealTime.DepartTime <= stopEvent.Stop.DepartTime.AddMinutes(1))
                        {
                            realTimeText = GetResource("StopInformationDepartureBoardResultControl.RealTimeText.OnTime.Text");
                        }
                        else
                        {
                            realTimeText = stopEvent.Stop.RealTime.DepartTime.ToString("HH:mm");
                        }
                    }
                    else
                    {
                        if (stopEvent.Stop.RealTime.ArriveTime <= stopEvent.Stop.ArriveTime.AddMinutes(1)
                            && stopEvent.Stop.RealTime.ArriveTime >= stopEvent.Stop.ArriveTime.AddMinutes(-1))
                        {
                            realTimeText = GetResource("StopInformationDepartureBoardResultControl.RealTimeText.OnTime.Text");
                        }
                        else
                        {
                            realTimeText = stopEvent.Stop.RealTime.ArriveTime.ToString("HH:mm");
                        }
                    }

                }

                if (stopEvent.Mode == DepartureBoardType.Train && stopEvent is TrainStopEvent)
                {
                    if (((TrainStopEvent)stopEvent).Cancelled)
                    {
                        realTimeText = GetResource("StopInformationDepartureBoardResultControl.RealTimeText.Cancelled.Text");
                    }
                }
            }

            return realTimeText;
        }

        /// <summary>
        /// Binds DBS result to the DepBoardServiceGrid gridview
        /// </summary>
        private void BindDBSResult()
        {
            bool resultFound = false;

            if (result != null)
            {
                if (result.StopEvents != null)
                {
                    if (result.StopEvents.Length > 0)
                    {
                        DepBoardServiceGrid.DataSource = result.StopEvents;
                        DepBoardServiceGrid.DataBind();
                        resultFound = true;
                    }
                }
            }

            if (!resultFound)
            {
                string emptyDataText = GetResource("StopInformationDepartureBoardControl.departureResults.EmptyDataText");

                if (!showDeparture)
                {
                    emptyDataText = GetResource("StopInformationDepartureBoardControl.arrivalResults.EmptyDataText");
                }

                DepBoardServiceGrid.EmptyDataText = emptyDataText;
            }

        }

        #endregion
    }
}