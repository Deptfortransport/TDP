using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.CyclePlannerControl;
using System.Xml.Serialization;
using System.IO;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1.Test
{
    public class TestMockCyclePlannerFromFile : ICyclePlannerManager
    {
        private ITDCyclePlannerRequest request;				
		private string xmlFilenameOutward;
		
		public TestMockCyclePlannerFromFile()
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

        public ITDCyclePlannerRequest Request
        {
            get { return request; }
        }
        #endregion
		
		#region Public methods
		
		public object Get()
		{
			return this;
		}
		#endregion
	
	
        #region ICyclePlannerManager Members

        public ITDCyclePlannerResult  CallCyclePlanner(ITDCyclePlannerRequest request, string sessionId, int userType, bool referenceTransaction, bool loggedOn, string language, string polylinesTransformXslt)
        {
            this.request = request;
            return new TDCyclePlannerResult();
        }

        public ITDCyclePlannerResult  CallCyclePlanner(ITDCyclePlannerRequest request, string sessionId, int userType, bool referenceTransaction, int referenceNumber, int lastSequenceNumber, bool loggedOn, string language, string polylinesTransformXslt)
        {
            this.request = request;
            return new TDCyclePlannerResult();
        }

        public ITDCyclePlannerResult  CallCyclePlanner(ITDCyclePlannerRequest request, string sessionId, int userType, bool referenceTransaction, bool loggedOn, string language, string polylinesTransformXslt, bool eesRequest)
        {
            this.request = request;

            TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.JourneyResult result 
                = GetCycleJourney(xmlFilenameOutward); // (JourneyResult)cjpObject.JourneyPlan(testrequest);


            TDCyclePlannerResult tdJourneyResult = new TDCyclePlannerResult(
                                                                1234,
                                                                0101,
                                                                DateTime.Now,
                                                                DateTime.Now.AddMinutes(60),
                                                                request.OutwardArriveBefore,
                                                                false);

            //add the outward journey
            tdJourneyResult.AddResult(result,
                                      true,
                                      null,
                                      request.OriginLocation,
                                      Request.DestinationLocation,
                                      sessionId,
                                      1,
                                      string.Empty,
                                      false,
                                      request.ResultSettings,
                                      true);

            //add the cjp messages if they are found in result
            if (result.messages != null && result.messages.Length > 0)
            {
                for (int i = 0; i < result.messages.Length; i++)
                {
                    TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.Message cpMessage 
                        = (TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.Message)result.messages[i];
                    if (cpMessage.code != 100)
                    {
                        tdJourneyResult.AddMessageToArray(cpMessage.description, "JourneyPlannerOutput.CJPInternalError", cpMessage.code, 0, ErrorsType.Error);
                        tdJourneyResult.IsValid = false;
                    }
                }
            }

            return tdJourneyResult;
        }

        private TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.JourneyResult GetCycleJourney(string xmlFilenameOutward)
        {
            // Instantiate a new instance of the XmlSerializer/
            XmlSerializer xs = new XmlSerializer(typeof(TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.JourneyResult));

            // Open a filestream pointing to the next filename as specified
            // in the filenames array.
            FileStream fileStream =
                new FileStream(xmlFilenameOutward, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Recreate a JourneyResult object by deserialising the file.
            TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.JourneyResult result 
                = (TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.JourneyResult)xs.Deserialize(fileStream);

            return result;
        }

        #endregion

    }
}
