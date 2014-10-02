// *********************************************** 
// NAME			: CallingPointsFormatter.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-11
// DESCRIPTION	: Responsible for formatting individual 
//                lines of a public transport schedule	  
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CallingPointsFormatter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:58:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:12   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.1   Jan 30 2006 12:15:14   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3.1.0   Jan 10 2006 15:17:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 16 2005 17:53:32   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Aug 02 2005 19:40:32   RPhilpott
//Handle passing points properly.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 19:57:58   RPhilpott
//Development of ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:09:48   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Responsible for formatting individual 
	/// lines of a public transport schedule	 	
	/// </summary>
	public class CallingPointsFormatter : TDWebAdapter
	{

		private PublicJourneyDetail detail;

		/// <summary>
		/// Constructor that also sets value of PublicJourneyDetail property.
		/// </summary>
		/// <param name="detail">The PublicJourneyDetail from which calling points are to be obtained</param>
		public CallingPointsFormatter(PublicJourneyDetail detail)
		{
			this.detail = detail;
			this.LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
		/// The PublicJourneyDetail for the current leg.
		/// </summary>
		public PublicJourneyDetail JourneyDetail
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

			CallingPointLine[] callingPointLines = null;
			
			switch (type)
			{

				case (CallingPointControlType.Before):
				{
					int arraySize = 0;

					if	(detail.Origin.Type != PublicJourneyCallingPointType.OriginAndBoard)
					{
						arraySize = 1;

						foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesBefore())
						{
							if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
							{
								arraySize++;
							}
						}
					}

					callingPointLines = new CallingPointLine[arraySize];

					if	(detail.Origin.Type != PublicJourneyCallingPointType.OriginAndBoard)
					{
						callingPointLines[0] = FormatCallingPointLine(detail.Origin);

						int i = 1;

						foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesBefore())
						{
							if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
							{
								callingPointLines[i] = FormatCallingPointLine(pjcp);
								i++;
							}
						}
					}
					
					break;
				}

				case (CallingPointControlType.Leg):
				{
					
					int arraySize = 2;

					foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesLeg())
					{
						if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
						{
							arraySize++;
						}
					}
				
					callingPointLines = new CallingPointLine[arraySize];

					callingPointLines[0] = FormatCallingPointLine(detail.LegStart);

					int i = 1;

					foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesLeg())
					{
						if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
						{
							callingPointLines[i] = FormatCallingPointLine(pjcp);
							i++;
						}
					}

					callingPointLines[arraySize - 1] = FormatCallingPointLine(detail.LegEnd);
				
					break;
				}

				case (CallingPointControlType.After):
				{
					int arraySize = 0;

					if	(detail.Destination.Type != PublicJourneyCallingPointType.DestinationAndAlight)
					{
						arraySize = 1;

						foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesAfter())
						{
							if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
							{
								arraySize++;
							}
						}
					}

					callingPointLines = new CallingPointLine[arraySize];

					if	(detail.Destination.Type != PublicJourneyCallingPointType.DestinationAndAlight)
					{
						int i = 0;

						foreach (PublicJourneyCallingPoint pjcp in detail.GetIntermediatesAfter())
						{
							if	(pjcp.Type != PublicJourneyCallingPointType.PassingPoint)
							{
								callingPointLines[i] = FormatCallingPointLine(pjcp);
								i++;
							}
						}

						callingPointLines[arraySize - 1] = FormatCallingPointLine(detail.Destination);
					}
					
					break;
				}
			}

			return callingPointLines;
		}


		/// <summary>
		/// Creates an individual CallingPointLine for a single calling point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		private CallingPointLine FormatCallingPointLine(PublicJourneyCallingPoint point)
		{

			string stationName = point.Location.Description;

			string arrivalTime;
			string departureTime;

			if	(point.Type == PublicJourneyCallingPointType.Origin 
					|| point.Type == PublicJourneyCallingPointType.OriginAndBoard)
			{
				arrivalTime = GetResource("CallingPoint.StartsText");
			}
			else if (point.ArrivalDateTime == null || point.ArrivalDateTime.GetDateTime() == DateTime.MinValue)
			{
				arrivalTime = GetResource("CallingPoint.NoTimeText");
			}
			else
			{
				arrivalTime = point.ArrivalDateTime.ToString("HH:mm");
			}

			if	(point.Type == PublicJourneyCallingPointType.Destination 
					|| point.Type == PublicJourneyCallingPointType.DestinationAndAlight)
			{
				departureTime = GetResource("CallingPoint.TerminatesText");
			}
			else if (point.DepartureDateTime == null || point.DepartureDateTime.GetDateTime() == DateTime.MinValue)
			{
				departureTime = GetResource("CallingPoint.NoTimeText");
			}
			else
			{
				departureTime = point.DepartureDateTime.ToString("HH:mm");
			}


			bool significant = (point.Type == PublicJourneyCallingPointType.OriginAndBoard 
								 || point.Type == PublicJourneyCallingPointType.Board
								 || point.Type == PublicJourneyCallingPointType.DestinationAndAlight
								 || point.Type == PublicJourneyCallingPointType.Alight);

			return new CallingPointLine(stationName, arrivalTime, departureTime, significant);
		}

	}
}
