// ********************************************************************* 
// NAME                 : JourneyPlanRequestVerboseEventFileFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 08/09/2003 
// DESCRIPTION  : Defines a file formatter for formatting
// JourneyPlanRequestVerbose events using the Event Service
// ********************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanRequestVerboseEventFileFormatter.cs-arc  $
//
//   Rev 1.2   Jan 20 2013 16:25:48   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.1   Mar 14 2011 15:11:54   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.0   Nov 08 2007 12:23:48   mturner
//Initial revision.
//
//   Rev 1.13   Mar 31 2005 17:05:40   jmorrissey
//Fixed bug with logging PublicViaLocations
//
//   Rev 1.12   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.11   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.10   Sep 25 2003 18:50:50   RPhilpott
//Corrections
//
//   Rev 1.9   Sep 25 2003 17:38:08   RPhilpott
//Tidy up date/times into ISO format
//
//   Rev 1.8   Sep 25 2003 10:43:02   ALole
//Added various checks for nulls to allow the FileFormatter to work if passed an empty JourneyPlanReqeustVerboseEvent class.
//
//   Rev 1.7   Sep 24 2003 13:43:38   RPhilpott
//Fix - via and alternate locations can be null
//
//   Rev 1.6   Sep 24 2003 12:08:26   PScott
//Add code to break out fields and build output string for file
//
//   Rev 1.1   Sep 12 2003 12:47:14   geaton
//Added message to highlight ToString has not been implemented.
//
//   Rev 1.0   Sep 09 2003 13:37:44   RPhilpott
//Initial Revision
//
//   Rev 1.0   Sep 08 2003 12:49:12   geaton
//Initial Revision

using System;
using System.Globalization;
using System.Text;

using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Formats JourneyPlanRequestVerbose events for publishing by file.
	/// </summary>
	public class JourneyPlanRequestVerboseEventFileFormatter : IEventFormatter
	{	
		/// <summary>
		/// Default constructor.
		/// </summary>
		public JourneyPlanRequestVerboseEventFileFormatter()
		{
		}
		/// <summary>
		/// Formats tht given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string output = String.Empty;
			
			if(logEvent is JourneyPlanRequestVerboseEvent)
			{
				JourneyPlanRequestVerboseEvent e = (JourneyPlanRequestVerboseEvent)logEvent;
				
				// build output string from properties within the event
				StringBuilder outputString = new StringBuilder();
				outputString.Append("JourneyPlanRequestVerboseEvent:: JourneyPlan Request Id: ");
				outputString.Append(e.JourneyPlanRequestId.ToString());
				outputString.Append(" Modes:");
				// mode
				if ( e.JourneyRequestData.Modes != null )
				{
					for ( int i = 0; i < e.JourneyRequestData.Modes.Length; i++ )
					{
						switch(e.JourneyRequestData.Modes[i])
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
						}

					}
				}

				// return required
				outputString.Append(" Return Reqd: ");
				if (e.JourneyRequestData.IsReturnRequired == true)
				{
					outputString.Append("Y");
				}
				else
				{
					outputString.Append("N");
				}
				// outward arrive before
				if (e.JourneyRequestData.OutwardArriveBefore == true)
				{
					outputString.Append(" Outward Arrive Before: " );
				}
				else
				{
					outputString.Append(" Outward Start After: ");
				}
				// OutwardDateTime
				if ( e.JourneyRequestData.OutwardDateTime != null )
				{
					outputString.Append(e.JourneyRequestData.OutwardDateTime[0].ToString("u"));
				}

				if (e.JourneyRequestData.IsReturnRequired == true)
				{			
					// Return Arrive Before
					if (e.JourneyRequestData.ReturnArriveBefore == true)
					{
						outputString.Append("Return Arrive Before: ");
					}
					else
					{
						outputString.Append(" Return Start After: ");
						// ReturnDateTime
					}
					outputString.Append(e.JourneyRequestData.ReturnDateTime[0].ToString("u"));
				}
				// InterchangeSpeed
				outputString.Append(" Interchange Speed: ");
				outputString.Append(e.JourneyRequestData.InterchangeSpeed.ToString());
				//  WalkingSpeed
				outputString.Append(" Walking Speed: ");
				outputString.Append(e.JourneyRequestData.WalkingSpeed.ToString());
				//	MaxWalkingTime
				outputString.Append(" Max Walking Time: ");
				outputString.Append(e.JourneyRequestData.InterchangeSpeed.ToString());
				//	DrivingSpeed
				outputString.Append(" Driving Speed: ");
				outputString.Append(e.JourneyRequestData.DrivingSpeed.ToString());
				//		bool AvoidMotorways { get; set; }
                outputString.Append(" Avoid Motorways: ");
                outputString.Append(e.JourneyRequestData.AvoidMotorways ? "Y" : "N");

                outputString.Append(" Avoid Limited Access: ");
                outputString.Append(e.JourneyRequestData.BanUnknownLimitedAccess ? "Y" : "N");
                
                //	OriginLocation
				outputString.Append(" Origin Location:");
				if ( e.JourneyRequestData.OriginLocation != null )
				{
					outputString.Append(e.JourneyRequestData.OriginLocation.Description);
				}
				//	DestinationLocation { get; set; }
				outputString.Append(" Destination Location:");
				if ( e.JourneyRequestData.DestinationLocation != null )
				{
					outputString.Append(e.JourneyRequestData.DestinationLocation.Description);
				}
				// PublicViaLocation
				outputString.Append(" Public Via Location:");
				if (e.JourneyRequestData.PublicViaLocations == null)
				{
					outputString.Append("none");
				}
				else
				{
					outputString.Append(e.JourneyRequestData.PublicViaLocations[0] != null ? e.JourneyRequestData.PublicViaLocations[0].Description: "none");
				}
				//		TDLocation PrivateViaLocation { get; set; }
				outputString.Append(" Private Via Location:");
				outputString.Append(e.JourneyRequestData.PrivateViaLocation != null ? e.JourneyRequestData.PrivateViaLocation.Description: "none");
						//		string[] AvoidRoads { get; set; }
				outputString.Append(" Avoid Roads:");
				if ( e.JourneyRequestData.AvoidRoads != null )
				{
					foreach(string strRoad in e.JourneyRequestData.AvoidRoads)
					{
						outputString.Append(" ");
						outputString.Append(strRoad);
					}
				}
				//		TDLocation[] AlternateLocations { get; set; }
				outputString.Append(" Alternate Locations:");
		
				if	(e.JourneyRequestData.AlternateLocations != null)
				{
					foreach (TransportDirect.UserPortal.LocationService.TDLocation tdAltLoc in e.JourneyRequestData.AlternateLocations)
					{
						outputString.Append(" ");
						outputString.Append(tdAltLoc.Description);
					}
					
					//		bool AlternateLocationsFrom { get; set; }
					outputString.Append(" AlternateLocationsFrom: ");
					
					if  (e.JourneyRequestData.AlternateLocationsFrom == true)
					{
						outputString.Append("Y");
					}
					else
					{
						outputString.Append("N");
					}
				}
					//	PrivateAlgorithm 
				outputString.Append(" PrivateAlgorithm: ");
				outputString.Append(e.JourneyRequestData.PrivateAlgorithm.ToString());
				//	PublicAlgorithm { get; set; }
				outputString.Append(" PublicAlgorithm:");
				outputString.Append(e.JourneyRequestData.PublicAlgorithm.ToString());
				output = outputString.ToString();			
			}
			return output;
		}
	}
}