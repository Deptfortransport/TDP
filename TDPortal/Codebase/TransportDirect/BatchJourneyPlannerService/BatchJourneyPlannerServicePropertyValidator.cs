// *********************************************** 
// NAME                 : BatchJourneyPlannerServicePropertyValidator.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: The clue's in the name.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/BatchJourneyPlannerServicePropertyValidator.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:24   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;

namespace BatchJourneyPlannerService
{
    public class BatchJourneyPlannerServicePropertyValidator
    {
        protected IPropertyProvider properties;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="current">property provider</param>
        public BatchJourneyPlannerServicePropertyValidator(IPropertyProvider current)
        {
            properties = Properties.Current;
        }

        /// <summary>
        /// Validates the properties - errors added to the array
        /// </summary>
        /// <param name="errors">errors array</param>
        public void ValidateProperties(ArrayList errors)
        {
            // Validate all the propeties from Keys
            bool tempBool;
            DateTime tempDate;
            string dateStub = DateTime.Now.ToString("yyyy-MM-dd") + " ";
            int tempInt;

            //BatchProcessingServiceEnabled
            string property = properties[Keys.BatchProcessingServiceEnabled];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingServiceEnabled));
            }
            else if (!bool.TryParse(property, out tempBool))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingServiceEnabled, property, "true / false"));
            }
            else if (!tempBool)
            {
                errors.Add(String.Format(Messages.Service_Disabled, Keys.BatchProcessingServiceEnabled, property));
            }

            //BatchProcessingPeakStartTime 
            property = properties[Keys.BatchProcessingPeakStartTime];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingPeakStartTime));
            }
            else if (!DateTime.TryParse(dateStub + property, out tempDate))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingPeakStartTime, property, "time hh:mm"));
            }


            //BatchProcessingPeakEndTime
            property = properties[Keys.BatchProcessingPeakEndTime];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingPeakEndTime));
            }
            else if (!DateTime.TryParse(dateStub + property, out tempDate))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingPeakEndTime, property, "time hh:mm"));
            }

            //BatchProcessingInWindowIntervalSeconds
            property = properties[Keys.BatchProcessingInWindowIntervalSeconds];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingInWindowIntervalSeconds));
            }
            else if (!int.TryParse(property, out tempInt))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingInWindowIntervalSeconds, property, "integer"));
            }

            //BatchProcessingOutWindowIntervalSeconds
            property = properties[Keys.BatchProcessingOutWindowIntervalSeconds];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingOutWindowIntervalSeconds));
            }
            else if (!int.TryParse(property, out tempInt))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingOutWindowIntervalSeconds, property, "integer"));
            }

            //BatchProcessingInWindowConcurrentRequests
            property = properties[Keys.BatchProcessingInWindowConcurrentRequests];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingInWindowConcurrentRequests));
            }
            else if (!int.TryParse(property, out tempInt))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingInWindowConcurrentRequests, property, "integer"));
            }

            //BatchProcessingOutWindowConcurrentRequests
            property = properties[Keys.BatchProcessingOutWindowConcurrentRequests];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingOutWindowConcurrentRequests));
            }
            else if (!int.TryParse(property, out tempInt))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingOutWindowConcurrentRequests, property, "integer"));
            }

            //BatchProcessingWindowCount
            property = properties[Keys.BatchProcessingWindowCount];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingWindowCount));
            }
            else if (!int.TryParse(property, out tempInt))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingWindowCount, property, "integer"));
            }
            else
            {
                string propertyName;
                for (int i = 1; i <= tempInt; i++)
                {
                    //BatchProcessingWindow{0}Start
                    propertyName = string.Format(Keys.BatchProcessingWindowNStart, i.ToString());
                    property = properties[propertyName];
                    if (string.IsNullOrEmpty(property))
                    {
                        errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, propertyName));
                    }
                    else if (!DateTime.TryParse(dateStub + property, out tempDate))
                    {
                        errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, propertyName, property, "time hh:mm"));
                    }

                    //BatchProcessingWindow{0}End";
                    propertyName = string.Format(Keys.BatchProcessingWindowNEnd, i.ToString());
                    property = properties[propertyName];
                    if (string.IsNullOrEmpty(property))
                    {
                        errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, propertyName));
                    }
                    else if (!DateTime.TryParse(dateStub + property, out tempDate))
                    {
                        errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, propertyName, property, "time hh:mm"));
                    }

                }
            }

            //BatchProcessingRunningDays
            property = properties[Keys.BatchProcessingRunningDays];
            if (string.IsNullOrEmpty(property))
            {
                errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, Keys.BatchProcessingRunningDays));
            }
            else 
            {
                // semi colon separated list of day names
                string[] daysOfWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                string[] runningDays = property.Split(';');
                foreach (string day in runningDays)
                {
                    if(!(new List<string>(daysOfWeek).Contains(day)))
                    {
                        errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, Keys.BatchProcessingRunningDays, property, "day of the week"));
                    }
                }
            }
        }
    }
}
