// ***********************************************
// NAME 		: TestMockCJPFromFile.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 16/01/2006
// DESCRIPTION 	: stub CJP that will return a journey result read from an xml file
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TestStub/TestMockCJPFromFile.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:12:52   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:02   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:46   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:24:26   mturner
//Initial revision.
//
//   Rev 1.5   Mar 16 2006 11:45:20   tmollart
//Unit test fixes.
//
//   Rev 1.4   Mar 14 2006 15:19:26   tmollart
//Post merge fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Jan 23 2006 11:29:00   mdambrine
//add resource id to the error
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 17 2006 16:57:16   mdambrine
//Added the CJPmessages to the result
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 17 2006 16:39:00   halkatib
//Added overload for call cjp method
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 16 2006 17:07:30   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 16 2006 14:55:12   mdambrine
//Added header to file
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//

using System;
using System.Collections;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestMockCJPFromFile.
	/// </summary>
	public class TestMockCJPFromFile : ICJPManager, IServiceFactory
	{

		private ITDJourneyRequest request;				
		private string xmlFilenameOutward;
		private string xmlFilenameReturn;

		public TestMockCJPFromFile()
		{
			
		}

		#region properties
		public string FilenameOutward 
		{
			get 
			{
				return xmlFilenameOutward;
			}
			set 
			{
				xmlFilenameOutward = value;
			}
		}

		public string FilenameReturn
		{
			get 
			{
				return xmlFilenameReturn;
			}
			set 
			{
				xmlFilenameReturn = value;
			}
		}
		#endregion
		
		#region Public methods
		/// <summary>
		/// This method will create a response from a file that is passed through
		/// the propeties 
		/// </summary>
		/// <param name="request">request - will not be used</param>
		/// <param name="sessionId">sessionid - will not be used</param>
		/// <param name="userType">will not be used</param>
		/// <param name="referenceTransaction">will not be used</param>
		/// <param name="loggedOn">will not be used</param>
		/// <param name="language">will not be used</param>
		/// <param name="isExtension">will not be used</param>
		/// <returns>A journeyresult read from a file</returns>
		public ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			bool loggedOn,
			string language,
            bool isExtension)
		{
			this.request = request;			

			// Create a new instance of the CJP object
			CjpStub cjpObject ;
							
			cjpObject = new CjpStub(xmlFilenameOutward, 1);
			
			// Create a dummy request
			JourneyRequest testrequest = new JourneyRequest();

			// Get the first result (outward)
			JourneyResult result = (JourneyResult)cjpObject.JourneyPlan(testrequest);

			TDJourneyResult tdJourneyResult = new TDJourneyResult(
				1234,
				0101,
				DateTime.Now,
				DateTime.Now.AddMinutes(60),
				request.OutwardArriveBefore,
				request.ReturnArriveBefore,
                false);

			//add the outward journey
			tdJourneyResult.AddResult(result, 
									  true,
									  null,
									  null,
									  request.OriginLocation,
									  Request.DestinationLocation,
									  sessionId,
                                      false,
                                      -1);

			//add the cjp messages if they are found in result
			if (result.messages != null && result.messages.Length  > 0)
			{
				for(int i=0; i<result.messages.Length; i++)
				{
					Message cjpMessage = (Message) result.messages[i];
					if (cjpMessage.code != 100)
					{
						tdJourneyResult.AddMessageToArray(cjpMessage.description, "JourneyPlannerOutput.CJPInternalError", cjpMessage.code, 0, ErrorsType.Error);					
						tdJourneyResult.IsValid = false;
					}
				}
			}

			if (request.IsReturnRequired)
			{
				//the walkingtime is used here to indicate which test is currently running hence what
				//result need to be passed back. ReturnJourney				
				cjpObject = new CjpStub(xmlFilenameReturn , 1);
				

				testrequest = new JourneyRequest();

				// Get the second result (return)
				result = (JourneyResult)cjpObject.JourneyPlan(testrequest);

				//add the return journey
				tdJourneyResult.AddResult(result, 
					false,
					null,
					null,
					Request.DestinationLocation,
					request.OriginLocation,
					sessionId,
                    false,
                    -1);

				//add the cjp messages if they are found in result
				if (result.messages != null && result.messages.Length  > 0)
				{
					for(int i=0; i<result.messages.Length; i++)
					{
						Message cjpMessage = (Message) result.messages[i];
						if (cjpMessage.code != 100)
						{
							tdJourneyResult.AddMessageToArray(cjpMessage.description, string.Empty, cjpMessage.code, 0, ErrorsType.Error);
							tdJourneyResult.IsValid = false;
						}
					}
				}
			}

			return tdJourneyResult;
		}

		public ITDJourneyResult CallCJP( ITDJourneyRequest	request,
			string sessionId,
			int userType,
			bool referenceTransaction,
			int referenceNumber,
			int lastSequenceNumber,
			bool loggedOn,
			string language)
		{
			this.request = request;
			return new TDJourneyResult();
		}

		public ITDJourneyResult CallCJP( ITDJourneyRequest	request)
		{
			return CallCJP(request,null, 0, false, false, null, false);
		}

		
		public ITDJourneyRequest Request 
		{
			get { return request; }
		}


		public object Get()
		{
			return this;
		}
		#endregion
	
	}
}


