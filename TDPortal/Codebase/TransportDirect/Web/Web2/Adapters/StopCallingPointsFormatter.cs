// *********************************************** 
// NAME                 : StopCallingPointsFormatter.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Provide methods to formate stop information in to calling points
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/StopCallingPointsFormatter.cs-arc  $
//
//   Rev 1.5   Jun 15 2010 12:48:48   apatel
//Updated to add new  "Cancelled" attribute to the DBSEvent object
//Resolution for 5554: Departure Board service detail page cancelled train issue
//
//   Rev 1.4   Jan 08 2010 09:20:50   apatel
//Updated real time status to show more like mobile emulator.
//Resolution for 5356: Stop Information Departure board don't display "On time" and "Cancelled" status
//
//   Rev 1.3   Nov 22 2009 15:57:56   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.2   Nov 18 2009 16:00:28   pghumra
//Code fixes for TDP release patch 10.8.2.3 - Stop Information page
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.1   Sep 14 2009 15:24:10   apatel
//Stop Service detail calling points formatter class
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
	/// Responsible for formatting individual 
	/// lines of a public transport schedule	 	
	/// </summary>
    public class StopCallingPointsFormatter : TDWebAdapter
    {
        private DBSStopEvent detail;

		/// <summary>
		/// Constructor that also sets value of PublicJourneyDetail property.
		/// </summary>
		/// <param name="detail">The DBSStopEvent from which calling points are to be obtained</param>
        public StopCallingPointsFormatter(DBSStopEvent detail)
		{
			this.detail = detail;
			this.LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
        /// The DBSStopEvent for the current leg.
		/// </summary>
        public DBSStopEvent JourneyDetail
		{
			get { return detail; }
			set { detail = value; }
		}

		/// <summary>
		/// Gets an array of CallingPointLines for the 
		/// specified portion of the current journey leg.
		/// </summary>
		/// <param name="type">Portion of the leg required</param>
		/// <returns>Array of CallingPointLines for the specified portion</returns>
		public CallingPointLine[] GetCallingPointLines(CallingPointControlType type)
		{

			List<CallingPointLine> callingPointLinesList = new List<CallingPointLine>();
            		
			switch (type)
			{

				case (CallingPointControlType.Before):
				{
                    if (detail.Stop.ActivityType != DBSActivityType.Depart)
                    {
                        callingPointLinesList.Add(FormatCallingPointLine(detail.Departure));
                    }
					foreach (DBSEvent dbsEvent in detail.PreviousIntermediates)
					{
                        callingPointLinesList.Add(FormatCallingPointLine(dbsEvent));
							
					}
					
					
					break;
				}

				case (CallingPointControlType.Leg):
				{
                    callingPointLinesList.Add(FormatCallingPointLine(detail.Stop));
                    
					break;
				}

				case (CallingPointControlType.After):
				{
                    foreach (DBSEvent dbsEvent in detail.OnwardIntermediates)
                    {

                        callingPointLinesList.Add(FormatCallingPointLine(dbsEvent));

                    }
                    if (detail.Stop.ActivityType != DBSActivityType.Arrive)
                    {
                        callingPointLinesList.Add(FormatCallingPointLine(detail.Arrival));
                    }
                    break;
				}
			}

            return callingPointLinesList.ToArray();
		}

        

		/// <summary>
		/// Creates an individual CallingPointLine for a single calling point stop event.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		private CallingPointLine FormatCallingPointLine(DBSEvent dbsEvent)
		{

			string stationName = dbsEvent.Stop.Name;

			string arrivalTime;
			string departureTime;

			if	(dbsEvent.ActivityType == DBSActivityType.Depart && dbsEvent.ArriveTime == DateTime.MinValue)
			{
				arrivalTime = GetResource("CallingPoint.StartsText");
			}
			else if (dbsEvent.ArriveTime == null || dbsEvent.ArriveTime == DateTime.MinValue)
			{
				arrivalTime = GetResource("CallingPoint.NoTimeText");
			}
			else
			{
                arrivalTime = string.Format("{0}<br/>({1})", dbsEvent.ArriveTime.ToString("HH:mm"), GetRealTimeInfo(dbsEvent, false));
			}

			if	(dbsEvent.ActivityType == DBSActivityType.Arrive && dbsEvent.DepartTime == DateTime.MinValue)
			{
				departureTime = GetResource("CallingPoint.TerminatesText");
			}
            else if (dbsEvent.DepartTime == null || dbsEvent.DepartTime == DateTime.MinValue)
			{
				departureTime = GetResource("CallingPoint.NoTimeText");
			}
			else
			{
                departureTime = string.Format("{0}<br/>({1})", dbsEvent.DepartTime.ToString("HH:mm"), GetRealTimeInfo(dbsEvent, true));
			}


			bool significant = (dbsEvent.ActivityType == DBSActivityType.Arrive
                                || dbsEvent.ActivityType == DBSActivityType.Depart);

			return new CallingPointLine(stationName, arrivalTime, departureTime, significant);
		}

        /// <summary>
        /// Gets the real time info text
        /// </summary>
        /// <param name="stopEvent">DBS stop event</param>
        /// <returns>real time information text</returns>
        private string GetRealTimeInfo(DBSEvent stopEvent, bool showDeparture)
        {
            string realTimeText = GetResource("langStrings","StopInformationDepartureBoardResultControl.RealTimeText.NoReport.Text");

            if (stopEvent.RealTime != null)
            {
                if (stopEvent.RealTime.DepartTime != DateTime.MinValue
                    || stopEvent.RealTime.ArriveTime != DateTime.MinValue)
                {
                    if (showDeparture)
                    {
                        if (stopEvent.RealTime.DepartTime <= stopEvent.DepartTime.AddMinutes(1))
                        {
                            realTimeText = GetResource("langStrings", "StopInformationDepartureBoardResultControl.RealTimeText.OnTime.Text");
                        }
                        else
                        {
                            realTimeText = stopEvent.RealTime.DepartTime.ToString("HH:mm");
                        }
                    }
                    else
                    {
                        if (stopEvent.RealTime.ArriveTime <= stopEvent.ArriveTime.AddMinutes(1) 
                            && stopEvent.RealTime.ArriveTime >= stopEvent.ArriveTime.AddMinutes(-1))
                        {
                            realTimeText = GetResource("langStrings", "StopInformationDepartureBoardResultControl.RealTimeText.OnTime.Text");
                        }
                        else
                        {
                            realTimeText = stopEvent.RealTime.ArriveTime.ToString("HH:mm");
                        }
                    }

                }

                if (detail.Mode == DepartureBoardType.Train && detail is TrainStopEvent)
                {
                    if (stopEvent.Cancelled)
                    {
                        realTimeText = GetResource("langStrings", "StopInformationDepartureBoardResultControl.RealTimeText.Cancelled.Text");
                    }
                }

            }

            return realTimeText;
        }
    }
}
