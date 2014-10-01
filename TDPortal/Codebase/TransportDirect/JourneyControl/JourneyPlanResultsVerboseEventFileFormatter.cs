// ********************************************************************* 
// NAME                 : JourneyPlanResultsEventFileFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 08/09/2003 
// DESCRIPTION  : Defines a file formatter for formatting
// JourneyPlanResults events using the Event Service
// ********************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanResultsVerboseEventFileFormatter.cs-arc  $
//
//   Rev 1.1   Jan 20 2013 16:25:50   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:23:50   mturner
//Initial revision.
//
//   Rev 1.5   Aug 24 2005 16:05:10   RPhilpott
//Get rid eof warnings from deprecated properties.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.4   Sep 17 2004 15:13:00   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.3   Apr 19 2004 10:59:48   COwczarek
//Add new case to handle rail replacement bus mode in AsString method
//Resolution for 697: Bus replacement change
//
//   Rev 1.2   Feb 19 2004 17:05:32   COwczarek
//Refactored PublicJourneyDetail into new class hierarchy representing different journey leg types (timed, continuous and frequency based)
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.1   Sep 25 2003 18:51:12   RPhilpott
//Restructure to match data being formatted
//
//   Rev 1.0   Sep 24 2003 18:22:40   geaton
//Initial Revision
//
//   Rev 1.4   Sep 24 2003 12:08:24   PScott
//Add code to break out fields and build output string for file
//
//   Rev 1.1   Sep 12 2003 12:47:14   geaton
//Added message to highlight ToString has not been implemented.
//
//   Rev 1.0   Sep 09 2003 13:37:46   RPhilpott
//Initial Revision
//
//   Rev 1.0   Sep 08 2003 12:49:20   geaton
//Initial Revision

using System;
using System.Globalization;
using System.Text;


using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Formats JourneyPlanResults events for publishing by file.
	/// </summary>
	public class JourneyPlanResultsVerboseEventFileFormatter : IEventFormatter
	{	
		/// <summary>
		/// Default constructor.
		/// </summary>
		public JourneyPlanResultsVerboseEventFileFormatter()
		{
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			StringBuilder outputString = new StringBuilder();

			if  (logEvent is JourneyPlanResultsVerboseEvent)
			{
				JourneyPlanResultsVerboseEvent e = (JourneyPlanResultsVerboseEvent)logEvent;

				outputString.Append("JourneyPlanResultsEvent:: JourneyPlan Request Id: ");
				outputString.Append(e.JourneyPlanRequestId);
				outputString.Append(" CJP Messages: ");

				foreach (CJPMessage strMessage in e.JourneyResultsData.CJPMessages)
				{
					outputString.Append(" ");
					outputString.Append(strMessage.MessageResourceId);
					outputString.Append(strMessage.MessageText);
				}

				outputString.Append(" Valid: ");
				outputString.Append(e.JourneyResultsData.IsValid ? "Y" : "N");

				outputString.Append(" JourneyReferenceNumber: ");
				outputString.Append(e.JourneyResultsData.JourneyReferenceNumber.ToString());
				outputString.Append(" LastReferenceSequence: ");
				outputString.Append(e.JourneyResultsData.LastReferenceSequence.ToString());
				outputString.Append(" OutwardPublicJourneyCount: ");
				outputString.Append(e.JourneyResultsData.OutwardPublicJourneyCount.ToString());
				outputString.Append(" ReturnPublicJourneyCount: ");
				outputString.Append(e.JourneyResultsData.ReturnPublicJourneyCount.ToString());
				outputString.Append(" OutwardRoadJourneyCount: ");
				outputString.Append(e.JourneyResultsData.OutwardRoadJourneyCount.ToString());
				outputString.Append(" returnRoadJourneyCount: ");
				outputString.Append(e.JourneyResultsData.ReturnRoadJourneyCount.ToString());

				outputString.Append(" OutwardJourneys:");

				foreach (JourneySummaryLine sumLine in e.JourneyResultsData.OutwardJourneySummary(true))
				{
					outputString.Append(" arrival:");
					outputString.Append(sumLine.ArrivalDateTime.ToString("u"));
					outputString.Append(" departure: ");
					outputString.Append(sumLine.DepartureDateTime.ToString("u"));
					outputString.Append(" DisplayNumber: ");
					outputString.Append(sumLine.DisplayNumber);
					outputString.Append(" IterchangeCount: ");
					outputString.Append(sumLine.InterchangeCount.ToString());
					outputString.Append(" JourneyIndex: ");
					outputString.Append(sumLine.JourneyIndex.ToString());
					outputString.Append(" Modes:");

					for (int modeIndex = 0; modeIndex < sumLine.Modes.Length; modeIndex++)
					{
						switch (sumLine.Modes[modeIndex])
						{
							case ModeType.Air :
								outputString.Append(" air");
								break;
							case ModeType.Bus :
								outputString.Append(" bus");
								break;
							case ModeType.Coach:
								outputString.Append(" coach");
								break;
							case ModeType.Ferry:
								outputString.Append(" ferry");
								break;
							case ModeType.Metro:
								outputString.Append(" metro");
								break;
							case ModeType.Rail:
								outputString.Append(" rail");
								break;
                            case ModeType.Telecabine:
                                outputString.Append(" telecabine");
                                break;
							case ModeType.Tram:
								outputString.Append(" tram");
								break;
							case ModeType.Underground:
								outputString.Append(" underground");
								break;
                            case ModeType.RailReplacementBus:
                                outputString.Append(" rail replacement bus");
                                break;
                        }
					}
					outputString.Append(" RoadMiles: ");
					outputString.Append(sumLine.RoadMiles.ToString());
					outputString.Append(" type: ");
					outputString.Append(sumLine.Type.ToString());

					if	(sumLine.Type == TDJourneyType.PublicOriginal) 
					{
						outputString.Append(FormatDetails(e.JourneyResultsData.OutwardPublicJourney(sumLine.JourneyIndex)));
					}
					else if	(sumLine.Type == TDJourneyType.RoadCongested) 
					{
						outputString.Append(FormatDetails(e.JourneyResultsData.OutwardRoadJourney()));
					}
				}
				
				outputString.Append(" ReturnJourneys:");
						
				foreach (JourneySummaryLine sumLine in e.JourneyResultsData.ReturnJourneySummary(true))
				{
					outputString.Append(" arrival:");
					outputString.Append(sumLine.ArrivalDateTime.ToString("u"));
					outputString.Append(" departure: ");
					outputString.Append(sumLine.DepartureDateTime.ToString("u"));
					outputString.Append(" DisplayNumber: ");
					outputString.Append(sumLine.DisplayNumber);
					outputString.Append(" IterchangeCount: ");
					outputString.Append(sumLine.InterchangeCount.ToString());
					outputString.Append(" JourneyIndex: ");
					outputString.Append(sumLine.JourneyIndex.ToString());
					outputString.Append(" Modes:");

					for (int modeIndex = 0; modeIndex < sumLine.Modes.Length; modeIndex++)
					{
						switch(sumLine.Modes[modeIndex])
						{
							case ModeType.Air :
								outputString.Append(" air");
								break;
							case ModeType.Bus :
								outputString.Append(" bus");
								break;
							case ModeType.Coach:
								outputString.Append(" coach");
								break;
							case ModeType.Ferry:
								outputString.Append(" ferry");
								break;
							case ModeType.Metro:
								outputString.Append(" metro");
								break;
							case ModeType.Rail:
								outputString.Append(" rail");
								break;
                            case ModeType.Telecabine:
                                outputString.Append(" telecabine");
                                break;
							case ModeType.Tram:
								outputString.Append(" tram");
								break;
							case ModeType.Underground:
								outputString.Append(" underground");
								break;
                            case ModeType.RailReplacementBus:
                                outputString.Append(" rail replacement bus");
                                break;
                        }
					}
					outputString.Append(" RoadMiles: ");
					outputString.Append(sumLine.RoadMiles.ToString());
					outputString.Append(" type: ");
					outputString.Append(sumLine.Type.ToString());

					if	(sumLine.Type == TDJourneyType.PublicOriginal) 
					{
						outputString.Append(FormatDetails(e.JourneyResultsData.ReturnPublicJourney(sumLine.JourneyIndex)));
					}
					else if	(sumLine.Type == TDJourneyType.RoadCongested) 
					{
						outputString.Append(FormatDetails(e.JourneyResultsData.ReturnRoadJourney()));
					}
	
				}
			}

			return outputString.ToString();
		}
		

		private string FormatDetails(PublicJourney journey)
		{
			
			StringBuilder outputString = new StringBuilder();
			
			foreach (PublicJourneyDetail pubLine in journey.Details)
			{
				outputString.Append(" ArriveDateTime: ");
				outputString.Append(pubLine.LegEnd.ArrivalDateTime.ToString("u"));
				outputString.Append(" DepartDateTime: ");
				outputString.Append(pubLine.LegStart.DepartureDateTime.ToString("u"));
				outputString.Append(" DestinationLocation: ");
				if	(pubLine.Destination != null)
				{
					outputString.Append(pubLine.Destination.Location.Description);
				}
				else
				{
					outputString.Append(string.Empty);
				}
				outputString.Append(" Duration: ");
				outputString.Append(pubLine.Duration.ToString());
				outputString.Append(" EndLocation: ");
				outputString.Append(pubLine.LegEnd.Location.Description);
				outputString.Append(" IsValidated: ");
				outputString.Append(pubLine.IsValidated.ToString());
				outputString.Append(" Mode: ");
				outputString.Append(pubLine.Mode.ToString());

                if (pubLine is PublicJourneyFrequencyDetail) {
                    PublicJourneyFrequencyDetail frequencyDetail = pubLine as PublicJourneyFrequencyDetail;
                    outputString.Append(" Frequency: ");
                    outputString.Append(frequencyDetail.Frequency);
                    outputString.Append(" Min frequency: ");
                    outputString.Append(frequencyDetail.MinFrequency);
                    outputString.Append(" Max frequency: ");
                    outputString.Append(frequencyDetail.MaxFrequency);
                    outputString.Append(" Max duration: ");
                    outputString.Append(frequencyDetail.MaxDuration);
                }

                if (pubLine.Services != null)
				{
					outputString.Append("services : ");

					foreach (ServiceDetails servLine in pubLine.Services)
					{
						outputString.Append(" ");
						outputString.Append(servLine.ServiceNumber);
					}
				}
				outputString.Append(" StartLocation: ");
				outputString.Append(pubLine.LegStart.Location.Description);
			}			

			return outputString.ToString();
		}	


		private string FormatDetails(RoadJourney journey)
		{
			StringBuilder outputString = new StringBuilder();

			foreach (RoadJourneyDetail detailLine in journey.Details)
			{
				outputString.Append(" Angle: ");
				outputString.Append(detailLine.Angle.ToString());
				outputString.Append(" CongestionLevel : ");
				outputString.Append(detailLine.CongestionLevel.ToString());
				outputString.Append(" Direction: ");
				outputString.Append(detailLine.Direction.ToString());
				outputString.Append(" Distance: ");
				outputString.Append(detailLine.Distance.ToString());
				outputString.Append(" Duration: ");
				outputString.Append(detailLine.Duration.ToString());
				outputString.Append("  StopOver: ");
				outputString.Append(detailLine.IsStopOver ? " Y" : " N");
				outputString.Append(" RoadName: ");
				outputString.Append(detailLine.RoadName);
				outputString.Append(" RoadNumber: ");
				outputString.Append(detailLine.RoadNumber);
				outputString.Append(" RoundAbout: ");
				outputString.Append(detailLine.Roundabout ? " Y" : " N");
				outputString.Append(" ThroughRoute: ");
				outputString.Append(detailLine.ThroughRoute ? " Y" : " N");
				outputString.Append(" Toid: ");
				outputString.Append(detailLine.Toid);
				outputString.Append(" TurnCount: ");
				outputString.Append(detailLine.TurnCount.ToString());
				outputString.Append(" ");
			}

			return outputString.ToString();
		}

	}
}

