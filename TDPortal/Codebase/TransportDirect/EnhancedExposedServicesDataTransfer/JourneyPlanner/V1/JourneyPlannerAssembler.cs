// *********************************************** 
// NAME                 : JourneyPlannerAssembler.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner JourneyPlannerAssembler Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/JourneyPlannerAssembler.cs-arc  $ 
//
//   Rev 1.2   Mar 05 2013 11:10:28   mmodi
//Updated public journey result creator to use culture specific resource manager methods to allow welsh text to be returned
//Resolution for 5893: Enhanced Exposed Services Journey Planner Service does not provide Welsh journey details
//
//   Rev 1.1   Jan 23 2013 11:50:24   mmodi
//Updated to support Telecabine
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:22:28   mturner
//Initial revision.
//
//   Rev 1.10   Jul 12 2006 16:23:34   rphilpott
//Select best journeys to return to client when number requestd from CJP is more than asked for by client.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.9   Feb 01 2006 16:25:42   halkatib
//Changes made as part of code review.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Jan 26 2006 18:10:14   halkatib
//Implemented fixes for the commonassembler and the journeyplannerassembler
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Jan 26 2006 09:42:28   halkatib
//Added missing origin and destination calls to the createpublicjourneydetails method
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jan 25 2006 16:30:00   halkatib
//Added fixes to problems shown by unit  testing.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 20 2006 12:00:48   halkatib
//Fixed foreach bug in result method
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 20 2006 10:48:22   halkatib
//Fixed bugs revealed by unit test
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 13 2006 17:04:36   halkatib
//Added functionality for all the methods in the class.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 12 2006 16:46:48   halkatib
//Added functionality for the empty methods.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 06 2006 15:59:20   halkatib
//Applied changes required by wsdl documents for IR3407
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:04:16   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using System.Text;
using System.Collections;
using TransportDirect.Common.ResourceManager;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;
using journeyControl = TransportDirect.UserPortal.JourneyControl;
using System.Globalization;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{

	public sealed class JourneyPlannerAssembler
	{
		#region public methods	
		/// <summary>
		/// Method uses parameters from a domain request object to create a dto request object
		/// </summary>
		/// <param name="journeyRequest"></param>
		/// <returns></returns>
		public static ITDJourneyRequest CreateTDJourneyRequest(PublicJourneyRequest journeyRequest)
		{
			if (journeyRequest != null)
			{
				ITDJourneyRequest tdJourneyRequestDTO = new TDJourneyRequest();
				tdJourneyRequestDTO.Modes = new cjpInterface.ModeType[] {
                    cjpInterface.ModeType.Air, cjpInterface.ModeType.Bus, cjpInterface.ModeType.Coach, 
                    cjpInterface.ModeType.Ferry, cjpInterface.ModeType.Metro, cjpInterface.ModeType.Rail, 
                    cjpInterface.ModeType.Tram, cjpInterface.ModeType.Underground};
                    // Modes will need to add Telecabine in future

				tdJourneyRequestDTO.IsReturnRequired = journeyRequest.IsReturnRequired;
				tdJourneyRequestDTO.OutwardArriveBefore = journeyRequest.OutwardArriveBefore;
				tdJourneyRequestDTO.ReturnArriveBefore = journeyRequest.ReturnArriveBefore;
			
				//convert datetime parameters into a TDDateTime parameter
				TDDateTime outwardTDDateTime = new TDDateTime(journeyRequest.OutwardDateTime);
				tdJourneyRequestDTO.OutwardDateTime = new TDDateTime[]{outwardTDDateTime};
				TDDateTime returnTDDateTime = new TDDateTime(journeyRequest.ReturnDateTime);
				tdJourneyRequestDTO.ReturnDateTime = new TDDateTime[]{returnTDDateTime};
			
				tdJourneyRequestDTO.InterchangeSpeed = journeyRequest.InterchangeSpeed;
				tdJourneyRequestDTO.WalkingSpeed = journeyRequest.WalkingSpeed;
				tdJourneyRequestDTO.MaxWalkingTime = journeyRequest.MaxWalkingTime;
				tdJourneyRequestDTO.PrivateAlgorithm = cjpInterface.PrivateAlgorithmType.Cheapest;
				tdJourneyRequestDTO.PublicAlgorithm = cjpInterface.PublicAlgorithmType.Default;
				tdJourneyRequestDTO.VehicleType = cjpInterface.VehicleType.Bicycle;
				tdJourneyRequestDTO.Sequence = journeyRequest.Sequence;
			
				return tdJourneyRequestDTO;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Method converts an ITDourneyRequest object to a PublicJourneyResult object.
		/// </summary>
		/// <param name="journeyResult"></param>
		/// <param name="rm"></param>
		/// <param name="outwardArriveBefore"></param>
		/// <param name="returnArriveBefore"></param>
		/// <returns></returns>
		public static PublicJourneyResult CreatePublicJourneyResultDT(ITDJourneyResult journeyResult,
																TDResourceManager  rm,
																bool outwardArriveBefore,
																bool returnArriveBefore,
																int originalSequence)
		{
			if (journeyResult != null)
			{
				PublicJourneyResult publicJourneyResultDTO = new PublicJourneyResult();
				if (journeyResult.IsValid)
				{
					
					ArrayList messageDTOList = new ArrayList();
					ArrayList publicJourneyDTOList = new ArrayList();

					JourneySummaryLine[] jslOutwardArray = journeyResult.OutwardJourneySummary(outwardArriveBefore);
					JourneySummaryLine[] jslReturnArray  = journeyResult.ReturnJourneySummary(returnArriveBefore);

					if	(jslOutwardArray.Length > originalSequence) 
					{
						JourneySummaryLine[] newOutwardArray = new JourneySummaryLine[originalSequence];

						for (int i = 0; i < originalSequence; i++) 
						{
							newOutwardArray[i] = jslOutwardArray[i];
						}

						jslOutwardArray = newOutwardArray;
					}

					if	(jslReturnArray.Length > originalSequence) 
					{
						JourneySummaryLine[] newReturnArray = new JourneySummaryLine[originalSequence];

						for (int i = 0; i < originalSequence; i++) 
						{
							newReturnArray[i] = jslReturnArray[i];
						}

						jslReturnArray = newReturnArray;
					}

					if (journeyResult.CJPMessages.Length != 0)
					{	
						foreach (CJPMessage cjpMessage in journeyResult.CJPMessages)
						{					
							if (cjpMessage.Type == ErrorsType.Warning)
							{
								messageDTOList.Add(rm.GetString(cjpMessage.MessageResourceId)); 											
							}					
						}
						//convert the array to a string and cast the converted array to get a string array
						publicJourneyResultDTO.UserWarnings = (string[])messageDTOList.ToArray(typeof(string));
					}
					 
					if (jslOutwardArray != null)
					{
						foreach (JourneySummaryLine jsl in jslOutwardArray)
						{
							publicJourneyDTOList.Add(
								CreatePublicJourneyDT(journeyResult.OutwardPublicJourney(jsl.JourneyIndex), 
								jsl, rm) );					
						}
						publicJourneyResultDTO.OutwardPublicJourneys = (PublicJourney[])publicJourneyDTOList.ToArray(typeof(PublicJourney));
					}

					if (jslReturnArray != null)
					{
						publicJourneyDTOList.Clear();
						foreach (JourneySummaryLine jsl in jslReturnArray)
						{					
							publicJourneyDTOList.Add(
								CreatePublicJourneyDT(journeyResult.ReturnPublicJourney(jsl.JourneyIndex), 
								jsl, rm) );					
						}
						publicJourneyResultDTO.ReturnPublicJourneys = (PublicJourney[])publicJourneyDTOList.ToArray(typeof(PublicJourney));
					}
				}
				return publicJourneyResultDTO;
			}
			else 
			{
				return null;
			}
		}

		/// <summary>
		/// Method create a PublicJourney DTO and assignes the properties
		/// </summary>
		/// <param name="publicJourneyResult"></param>
		/// <param name="summaryLine"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		public static PublicJourney CreatePublicJourneyDT(journeyControl.PublicJourney publicJourneyResult,
													JourneySummaryLine summaryLine,
													TDResourceManager rm)
		{
			PublicJourney publicJourneyDTO = new PublicJourney();
			publicJourneyDTO.Details = CreatePublicJourneyDetailsDT(publicJourneyResult.Details, rm);
			publicJourneyDTO.Summary = CreateJourneySummaryDT(summaryLine, rm);
			return publicJourneyDTO;
		}

		/// <summary>
		/// Method creates an array of PublicJourneyDetail DTOs.
		/// </summary>
		/// <param name="details"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		public static PublicJourneyDetail[] CreatePublicJourneyDetailsDT(journeyControl.PublicJourneyDetail[] details,
																   TDResourceManager rm)
		{
			if (details != null)
			{
				ArrayList publicJourneyDetailDTOArrayList = new ArrayList();
				foreach (journeyControl.PublicJourneyDetail pjd in details)
				{
					StringBuilder sbResourceString = new StringBuilder("TransportMode.");					
					PublicJourneyDetail pjdDTOArrayElement = new PublicJourneyDetail();
					pjdDTOArrayElement.Duration = pjd.Duration;	
					if (pjd is PublicJourneyTimedDetail)
					{
						pjdDTOArrayElement.Type = LegType.Timed;
						pjdDTOArrayElement.DurationText = GetDurationText(pjdDTOArrayElement.Duration,rm);										
					}
					else if (pjd is PublicJourneyFrequencyDetail)
					{
						pjdDTOArrayElement.Type = LegType.Frequency;
						PublicJourneyFrequencyDetail pjfd = (PublicJourneyFrequencyDetail)pjd;
						pjdDTOArrayElement.MinFrequency = pjfd.MinFrequency;
						pjdDTOArrayElement.MaxFrequency = pjfd.MaxFrequency;                    
						if (pjdDTOArrayElement.MaxFrequency == pjdDTOArrayElement.MinFrequency)
						{
                            pjdDTOArrayElement.FrequencyText = string.Format(rm.GetString("JourneyDetails.labelFrequencyTextEqual", TDCultureInfo.CurrentUICulture, true),
								pjdDTOArrayElement.MaxFrequency);
						}
						else
						{
                            pjdDTOArrayElement.FrequencyText = string.Format(rm.GetString("JourneyDetails.labelFrequencyTextNotEqual", TDCultureInfo.CurrentUICulture, true),
								pjdDTOArrayElement.MinFrequency,pjdDTOArrayElement.MaxFrequency);
						}					
						pjdDTOArrayElement.DurationText = GetTypicalDurationTextFrequency(pjdDTOArrayElement.Duration,rm);
						pjdDTOArrayElement.MaxDuration = pjfd.MaxDuration;
						pjdDTOArrayElement.MaxDurationText = GetMaxDurationTextFrequency(pjdDTOArrayElement.MaxDuration,rm);

					}
					else if (pjd is PublicJourneyContinuousDetail)
					{
						pjdDTOArrayElement.Type = LegType.Continuous;				
						pjdDTOArrayElement.DurationText = GetDurationText(pjdDTOArrayElement.Duration,rm);
					}
					string mode = pjd.Mode.ToString();

                    // The following Telecabine check must be removed in the future
                    if (pjd.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Telecabine)
                        mode = ModeType.Tram.ToString();

					pjdDTOArrayElement.Mode = (ModeType)Enum.Parse(typeof(ModeType),mode);				
				
					string modeResourceID = sbResourceString.Append(pjdDTOArrayElement.Mode.ToString()).ToString();
                    pjdDTOArrayElement.ModeText = rm.GetString(modeResourceID, TDCultureInfo.CurrentUICulture, true);
				
					pjdDTOArrayElement.InstructionText = CreateInstructionText(pjd, rm);
					pjdDTOArrayElement.Origin = CreatePublicJourneyCallingPointDT(pjd.Origin);
					pjdDTOArrayElement.Destination = CreatePublicJourneyCallingPointDT(pjd.Destination);
					pjdDTOArrayElement.LegStart = CreatePublicJourneyCallingPointDT(pjd.LegStart);
					pjdDTOArrayElement.LegEnd = CreatePublicJourneyCallingPointDT(pjd.LegEnd);
					pjdDTOArrayElement.IntermediatesBefore = CreatePublicJourneyCallingPointDT(pjd.GetIntermediatesBefore());
					if (pjdDTOArrayElement.IntermediatesBefore.Length == 0) 
					{
						pjdDTOArrayElement.IntermediatesBefore = null;
					}
					pjdDTOArrayElement.IntermediatesLeg = CreatePublicJourneyCallingPointDT(pjd.GetIntermediatesLeg());
					if (pjdDTOArrayElement.IntermediatesLeg.Length == 0)
					{
						pjdDTOArrayElement.IntermediatesLeg = null;
					}
					pjdDTOArrayElement.IntermediatesAfter = CreatePublicJourneyCallingPointDT(pjd.GetIntermediatesAfter());
					if (pjdDTOArrayElement.IntermediatesAfter.Length == 0)
					{
						pjdDTOArrayElement.IntermediatesAfter = null;
					}
					pjdDTOArrayElement.Services = CreateServicesDetailsDT(pjd.Services);
					pjdDTOArrayElement.VehicleFeatures = pjd.GetVehicleFeatures();
					if (pjdDTOArrayElement.VehicleFeatures.Length != 0)
					{
						string[] vehicleFeaturesArray = new string[pjdDTOArrayElement.VehicleFeatures.Length];
						int count = 0;
						foreach (int number in pjdDTOArrayElement.VehicleFeatures)
						{
							sbResourceString = new StringBuilder("VehicleFeature.");
							string vehicleFeatureResourceID = sbResourceString.Append(number.ToString()).ToString();
                            vehicleFeaturesArray[count] = rm.GetString(vehicleFeatureResourceID, TDCultureInfo.CurrentUICulture, true);
							count++;
						}
						pjdDTOArrayElement.VehicleFeaturesText = vehicleFeaturesArray;						
					}
					else 
					{
						pjdDTOArrayElement.VehicleFeatures = null;
					}
					if (pjd.GetDisplayNotes() == null || pjd.GetDisplayNotes().Length == 0)
						pjdDTOArrayElement.DisplayNotes = null;						
					else
						pjdDTOArrayElement.DisplayNotes = pjd.GetDisplayNotes();					
						

					//add array element to the arraylist
					publicJourneyDetailDTOArrayList.Add(pjdDTOArrayElement);
				}

				return (PublicJourneyDetail[])publicJourneyDetailDTOArrayList.ToArray(typeof(PublicJourneyDetail));
			}
			else 
			{
				return null;
			}
		}

		/// <summary>
		/// This method creates a PublicJourneyCallingPoint DTO object
		/// </summary>
		/// <param name="callingPoint"></param>
		/// <returns></returns>
		public static PublicJourneyCallingPoint CreatePublicJourneyCallingPointDT(journeyControl.PublicJourneyCallingPoint callingPoint)
		{
			if (callingPoint != null) 
			{
				PublicJourneyCallingPoint pjcpDTO = new PublicJourneyCallingPoint();
				string type = callingPoint.Type.ToString();
				pjcpDTO.Type = (PublicJourneyCallingPointType)Enum.Parse(typeof(PublicJourneyCallingPointType),type);			
				pjcpDTO.Location = CreateResponseLocationDT(callingPoint.Location);
				if (callingPoint.ArrivalDateTime != null)
				{
					pjcpDTO.ArrivalDateTime = callingPoint.ArrivalDateTime.GetDateTime();
				}
				if(callingPoint.DepartureDateTime != null)
				{
					pjcpDTO.DepartureDateTime = callingPoint.DepartureDateTime.GetDateTime();			
				}			
				return pjcpDTO;
			} 
			else 
			{
				return null;
			}
		}
		
		/// <summary>
		/// This method creates an array of PublicJourneyCallingPoint DTO objects
		/// </summary>
		/// <param name="callingPoint"></param>
		/// <returns></returns>
		public static PublicJourneyCallingPoint[] CreatePublicJourneyCallingPointDT(journeyControl.PublicJourneyCallingPoint[] callingPoint)
		{
			if (callingPoint != null) 
			{
				int count = 0;
				PublicJourneyCallingPoint[] pjcpDTOArray = new PublicJourneyCallingPoint[callingPoint.Length];
				foreach(journeyControl.PublicJourneyCallingPoint pjcp in callingPoint)
				{
					pjcpDTOArray[count] = CreatePublicJourneyCallingPointDT(pjcp);
					count++;
				}
				return pjcpDTOArray;
			} 
			else 
			{
				return null;
			}
		}

		/// <summary>
		/// Creates a ResponseLocation DTO object from the supplied domain location.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public static ResponseLocation CreateResponseLocationDT(TDLocation location)
		{
			if (location != null)
			{
				ResponseLocation responseLocationDTO = new ResponseLocation();
				responseLocationDTO.GridReference = CommonAssembler.CreateOSGridReferenceDT(location.GridReference);
				//there is only one naptan in the naptans array so only pass first array element
				responseLocationDTO.Naptan = CommonAssembler.CreateNaptanDT(location.NaPTANs[0]);
				responseLocationDTO.Description = location.Description;
				responseLocationDTO.Locality = location.Locality;
			
				return responseLocationDTO;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Creates an array of service details dtos
		/// </summary>
		/// <param name="details"></param>
		/// <returns></returns>
		public static ServiceDetails[] CreateServicesDetailsDT(journeyControl.ServiceDetails[] details)
		{
			if (details == null || details.Length == 0)
			{
				return null;				
			}
			else
			{
				ServiceDetails[] sdDTOArray = new ServiceDetails[details.Length];
				int count = 0;
				foreach(journeyControl.ServiceDetails sd in details)
				{
					ServiceDetails sdDTOElement = new ServiceDetails();
					sdDTOElement.OperatorCode = sd.OperatorCode;
					sdDTOElement.OperatorName = sd.OperatorName;
					sdDTOElement.ServiceNumber = sd.ServiceNumber;
					sdDTOElement.DestinationBoard = sd.DestinationBoard;

					sdDTOArray[count] = sdDTOElement;
					count++;
				}
				return sdDTOArray;				
			}
		}

		/// <summary>
		/// Method creates a journeysummary dto using the summary domain object
		/// </summary>
		/// <param name="summaryLine"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		public static JourneySummary CreateJourneySummaryDT(journeyControl.JourneySummaryLine summaryLine,
			TDResourceManager rm)
		{
			if (summaryLine != null)
			{
				JourneySummary journeySummaryDTO = new JourneySummary();
				journeySummaryDTO.OriginDescription = summaryLine.OriginDescription;
				journeySummaryDTO.DestinationDescription = summaryLine.DestinationDescription;
			
				if (summaryLine.Modes != null)
				{
					ModeType[] modeDTOArray = new ModeType[summaryLine.Modes.Length];			
					string[] modeTextDTOArray = new string[summaryLine.Modes.Length];
					int count = 0;
					foreach (TransportDirect.JourneyPlanning.CJPInterface.ModeType mode in summaryLine.Modes)
					{	
						StringBuilder sbResourceString = new StringBuilder("TransportMode.");

                        // The following Telecabine check must be removed in the future
                        if (mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Telecabine)
                        {
                            modeDTOArray[count] = ModeType.Tram;
                            modeTextDTOArray[count] = rm.GetString(sbResourceString.Append(ModeType.Tram.ToString()).ToString(), TDCultureInfo.CurrentUICulture, true);
                        }
                        else
                        {
                            modeDTOArray[count] = (ModeType)Enum.Parse(typeof(ModeType), mode.ToString());
                            modeTextDTOArray[count] = rm.GetString(sbResourceString.Append(mode.ToString()).ToString(), TDCultureInfo.CurrentUICulture, true);
                        }
						count++;
					}
					journeySummaryDTO.Modes = modeDTOArray;
					journeySummaryDTO.ModesText = modeTextDTOArray;
				}
				journeySummaryDTO.InterchangeCount = summaryLine.InterchangeCount;
				journeySummaryDTO.DepartureDateTime = summaryLine.DepartureDateTime.GetDateTime();
				journeySummaryDTO.ArrivalDateTime = summaryLine.ArrivalDateTime.GetDateTime();
				return journeySummaryDTO;
			}
			else 
			{
				return null; 
			}
		}

		/// <summary>
		/// Generates the language specific instruction text detailing the service
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		public static string CreateInstructionText(journeyControl.PublicJourneyDetail journeyDetail,
											 TDResourceManager rm)
		{
			if (journeyDetail != null)
			{
				StringBuilder instructionText = new StringBuilder();
				StringBuilder destinationText = new StringBuilder();				
				//string destinationText = string.Empty;

                string upperTakeText = rm.GetString("JourneyDetailsControl.labelUpperTakeString", TDCultureInfo.CurrentUICulture, true);
                string towardsText = rm.GetString("JourneyDetailsControl.labelTowardsString", TDCultureInfo.CurrentUICulture, true);
                string orText = rm.GetString("JourneyDetailsControl.labelOrString", TDCultureInfo.CurrentUICulture, true);
                string lowerTakeText = rm.GetString("JourneyDetailsControl.labelLowerTakeString", TDCultureInfo.CurrentUICulture, true);

				if (journeyDetail.Mode == cjpInterface.ModeType.Walk)
				{
					// Get resource strings
                    string walkToText = rm.GetString("JourneyDetailsTableControl.WalkTo", TDCultureInfo.CurrentUICulture, true);
					instructionText.Append(walkToText);
					instructionText.Append(" ");
					instructionText.Append(journeyDetail.LegEnd.Location.Description);

					// return the "Walk to" string from resourcing manager.
					return instructionText.ToString();				
				}
				else if (journeyDetail.Services == null) 
				{
					// destination text
					destinationText.Append(upperTakeText);
					destinationText.Append(" ");
					destinationText.Append(journeyDetail.Mode.ToString());
					
					if (journeyDetail.Destination != null  
						&& journeyDetail.Destination.Location != null  
						&& journeyDetail.Destination.Location.Description != null
						&& journeyDetail.Destination.Location.Description.Length != 0)					
					{
						destinationText.Append(String.Format(" {0} {1}",towardsText,journeyDetail.Destination.Location.Description));												
					}				
					return instructionText.Append(destinationText.ToString()).ToString();

				} 
				else
				{
					//SetServiceResourceStrings(rm);
					int count = 0;
					foreach (journeyControl.ServiceDetails sd in journeyDetail.Services)
					{
						destinationText = new StringBuilder();

						// Build the desription for the service
				
						// takeLabel
						if (count == 0)	// the description of the 1st service in a leg starts with a capital letter
							instructionText.Append(string.Format("{0} ",upperTakeText));													
						else
							instructionText.Append(string.Format(" {0} {1} ", orText, lowerTakeText));	// subsequent service suggestions separated by "or"							

						// operator link
						if (sd != null) // null check
						{						
							if (sd.OperatorName != null && sd.OperatorName.Length != 0)
								instructionText.Append(sd.OperatorName);
							else
								instructionText.Append(journeyDetail.Mode.ToString());

							if((sd.ServiceNumber != null
								&& (sd.ServiceNumber.Length != 0)))
							{
								instructionText.Append("/" + sd.ServiceNumber);
							}						
							// destination text
							if(sd.DestinationBoard != null 
								&& sd.DestinationBoard.Length != 0)
							{
								destinationText.Append(string.Format(" {0} {1}", towardsText, sd.DestinationBoard));								
							}
							else if (journeyDetail.Destination != null  
								&& journeyDetail.Destination.Location != null  
								&& journeyDetail.Destination.Location.Description != null
								&& journeyDetail.Destination.Location.Description.Length != 0)
							{
								destinationText.Append(String.Format(" {0} {1}", towardsText, journeyDetail.Destination.Location.Description));								
							}				
							instructionText.Append(destinationText);
						}
						else
						{
							// destination text
							destinationText.Append(journeyDetail.Mode.ToString());

							if (journeyDetail.Destination != null  
								&& journeyDetail.Destination.Location != null  
								&& journeyDetail.Destination.Location.Description != null
								&& journeyDetail.Destination.Location.Description.Length != 0)	
							{
								destinationText.Append(string.Format(" {0} {1}", towardsText, journeyDetail.Destination.Location.Description));
							}				
							instructionText.Append(destinationText.ToString());
						}
						count++;
					}
				}				
				return instructionText.ToString();
			}
			else
			{
				return string.Empty;
			}
		}		
		
		
		#endregion 

		#region Private methods
		/// <summary>
		/// Method returns the duration in a formated string using 
		/// the provided duration in seconds. 
		/// </summary>
		/// <param name="durationInSeconds"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		private static string GetDurationText(long durationInSeconds, TDResourceManager rm)
		{
            string minsText = rm.GetString("JourneyDetailsDurationText.minutesString", TDCultureInfo.CurrentUICulture, true);
            string minText = rm.GetString("JourneyDetailsDurationText.minuteString", TDCultureInfo.CurrentUICulture, true);
            string secondsText = rm.GetString("JourneyDetailsDurationText.secondsString", TDCultureInfo.CurrentUICulture, true);
            string hoursText = rm.GetString("JourneyDetailsDurationText.hoursString", TDCultureInfo.CurrentUICulture, true);
            string hourText = rm.GetString("JourneyDetailsDurationText.hourString", TDCultureInfo.CurrentUICulture, true);
            string duration = rm.GetString("JourneyDetailsDurationText.duration", TDCultureInfo.CurrentUICulture, true);

			StringBuilder durationText = new StringBuilder(duration);
	
			// Get the minutes
			double durationInMinutes = (double)durationInSeconds / (double)60.0;
			
			// Check to see if seconds is less than 30 seconds.
			if( durationInMinutes / 60.0  < 1.00 &&
				durationInMinutes % 60.0 < 0.5 )
			{
				return durationText.Append(string.Format("< 30 {0}", secondsText)).ToString();				
			}
			else
			{
				// Round to the nearest minute
				durationInMinutes = Round(durationInMinutes);

				// Calculate the number of hours in the minute
				int hours = (int)durationInMinutes / 60;

				// Get the minutes (afer the hours has been subracted so always < 60)
				int minutes = (int)durationInMinutes % 60;

				// If greater than 1 hour - retrieve "hours", if 1 or less, retrieve "hour"
				string hourString = hours > 1 ? hoursText : hourText;

				// If greater than 1 minute - retrive "minutes", if 1 or less, retrieve "minute"
				string minuteString = minutes > 1 ? minsText : minText;
        
				if(hours > 0)
					durationText.Append(string.Format("{0} {1} ", hours, hourString));					

				durationText.Append(string.Format("{0} {1}", minutes, minuteString));						

				return durationText.ToString();
			}
		}
		
		
		/// <summary>
		/// Method returns the maximum duration in a formated string using 
		/// the povided duration in minutes. 
		/// </summary>
		/// <param name="durationInMinutes"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		private static string GetMaxDurationTextFrequency(int durationInMinutes, TDResourceManager rm)
		{
            string minsText = rm.GetString("JourneyDetailsDurationText.minutesString", TDCultureInfo.CurrentUICulture, true);
            string minText = rm.GetString("JourneyDetailsDurationText.minuteString", TDCultureInfo.CurrentUICulture, true);
            string maxDurationText = rm.GetString("JourneyDetailsDurationText.maxDuration", TDCultureInfo.CurrentUICulture, true);

			StringBuilder durationText = new StringBuilder();
			durationText.Append(string.Format("{0}: {1} {2}", maxDurationText, durationInMinutes, 
				(durationInMinutes > 1 ? minsText : minText) ));

			return durationText.ToString();
		}

		/// <summary>
		/// Method returns the typical duration in a formated string using 
		/// the povided duration in minutes. 
		/// </summary>
		/// <param name="durationInMinutes"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		private static string GetTypicalDurationTextFrequency(int durationInMinutes, TDResourceManager rm)
		{
            string minsText = rm.GetString("JourneyDetailsDurationText.minutesString", TDCultureInfo.CurrentUICulture, true);
            string minText = rm.GetString("JourneyDetailsDurationText.minuteString", TDCultureInfo.CurrentUICulture, true);
            string typicalDurationText = rm.GetString("JourneyDetailsDurationText.typicalDuration", TDCultureInfo.CurrentUICulture, true);
				
			StringBuilder durationText = new StringBuilder();
			durationText.Append(string.Format("{0}: {1} {2}", typicalDurationText, durationInMinutes, 
				(durationInMinutes > 1 ? minsText : minText) ));

			return durationText.ToString();
		}

		/// <summary>
		/// Method round up or down the value provided depending on decimal value
		/// </summary>
		/// <param name="valueToRound"></param>
		/// <returns></returns>
		private static int Round(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if  (remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}

	
		#endregion
	
	}

} 
