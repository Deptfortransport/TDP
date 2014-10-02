//********************************************************************************
//NAME         : TTBOParametersDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to capture CJP/TTBO service selection criteria 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/TTBOParametersDto.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:05:02   mmodi
//New method to process the output of the RBO GL call prior to planning/validating journeys, with the new parameters set to allow validation of the services and fares using the RBO MR call.
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:35:56   mturner
//Initial revision.
//
//   Rev 1.8   May 24 2007 12:08:42   asinclair
//Correct substring from the GC response is now being used for getting the mins value
//Resolution for 4426: 9.6 - Error when 'First Inclusive' ticket select on Find a Fare (Train)
//
//   Rev 1.7   Dec 08 2005 14:20:44   RPhilpott
//Refine use of connectingTocs flag -- it needs to be set if and only if restriction codes count  == 1 and there are no codes in the reapply list. 
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on Weekender fare
//
//   Rev 1.6   Dec 05 2005 17:36:42   RPhilpott
//Add flag to indicate if GD call needed becuase of connecting TOC restrictions.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.5   Nov 29 2005 17:25:02   RPhilpott
//Do not use include UID's if there are moer than 20 of them, because the CJP can't cope -- let GD call filter these trains instead. 
//Resolution for 3204: DN039 - Ticket not available in Find a Fare
//
//   Rev 1.4   Apr 27 2005 11:01:42   RPhilpott
//Correct handling of "route include TOCs" when no "ticket type TOCs" are present.in GC output.
//Resolution for 2350: Del 7 - PT - include/exclude TOC's not always set correctly
//
//   Rev 1.3   Apr 13 2005 20:00:50   RPhilpott
//Corrections to interpretation of RBO GC output (changes is Y or space, not N or space).
//Resolution for 2171: PT: incorrect parameters used when selecing journeys
//
//   Rev 1.2   Apr 13 2005 13:35:54   RPhilpott
//Return errors.
//Resolution for 2072: PT: NRS error messages.
//
//   Rev 1.1   Apr 08 2005 13:55:44   RPhilpott
//Change restrictionCodesToReapply from string[] to single string.
//
//   Rev 1.0   Mar 01 2005 18:47:44   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object to capture CJP/TTBO service selection criteria.
	/// </summary>
	[Serializable()]
	public class TTBOParametersDto
    {
        #region Private members

        private ArrayList includeTocs = new ArrayList();
		private ArrayList excludeTocs = new ArrayList();
		
		private ArrayList includeTrainUids = new ArrayList();
		private ArrayList excludeTrainUids = new ArrayList();
		private ArrayList includeCrsLocations = new ArrayList();
		private ArrayList excludeCrsLocations = new ArrayList();

		private string restrictionCodesToReapply = String.Empty;
		
		private bool connectingTocsToCheck = false;

        private string outputGL = string.Empty;
        private bool crossLondonToCheck = false;
        private bool zonalIndicatorToCheck = false;
        private bool visitCRSToCheck = false;

		private TDDateTime adjustedDateTime = null;
		private bool changesAllowed = true;

		private ArrayList errorResourceIds = new ArrayList();

		private const int MAX_INCLUDE_UIDS = 20;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TTBOParametersDto()
		{
        }

        #endregion

        #region Public properties

        /// <summary>
		/// UIDs of trains, at least one of which must 
		/// be included in each journey returned.
		/// </summary>
		public string[] IncludeTrainUids
		{
			get { return (string[])(this.includeTrainUids.ToArray(typeof(string))); } 
		}

		/// <summary>
		/// UIDs of trains, none of which must appear in journeys returned
		/// </summary>
		public string[] ExcludeTrainUids
		{
			get { return (string[])(this.excludeTrainUids.ToArray(typeof(string))); } 
		}
		
		/// <summary>
		/// Array of locations, at least one of which each  
		/// must be passed through by each journey returned
		/// </summary>
		public string[] IncludeCrsLocations
		{
			get { return (string[])(this.includeCrsLocations.ToArray(typeof(string))); } 
		}

		/// <summary>
		/// Array of locations, through which returned journeys must NOT pass
		/// </summary>
		public string[] ExcludeCrsLocations
		{
			get { return (string[])(this.excludeCrsLocations.ToArray(typeof(string))); } 
		}
		
		/// <summary>
		/// TOC's, at least one of which must appear in each journey returned
		/// </summary>
		public TocDto[] IncludeTocs
		{
			get { return (TocDto[])(this.includeTocs.ToArray(typeof(TocDto))); } 
		}

		/// <summary>
		/// TOC's, none which must appear in journeys returned
		/// </summary>
		public TocDto[] ExcludeTocs
		{
			get { return (TocDto[])(this.excludeTocs.ToArray(typeof(TocDto))); } 
		}

		/// <summary>
		/// Restriction codes that need to be rechecked 
		/// (using RBO GD call) for all returned trains
		/// </summary>
		public string RestrictionCodesToReapply
		{
			get { return restrictionCodesToReapply; } 
		}

		/// <summary>
		/// Indication that TOC's need to be rechecked  
		/// (using RBO GD call) for all returned trains
		/// </summary>
		public bool ConnectingTocsToCheck
		{
			get { return connectingTocsToCheck; } 
		}

		/// <summary>
		/// Outward departure time, modified to avoid trains
		/// leaving before end of restricted time period
		/// </summary>
		public TDDateTime AdjustedDateTime
		{
			get { return this.adjustedDateTime; } 
		}
				
		/// <summary>
		/// Is changing trains permitted?
		/// </summary>
		public bool ChangesAllowed
		{
			get { return this.changesAllowed; } 
		}

        /// <summary>
        /// Did RBO GL call contain London check flag. 
        /// If yes, then post timetable validation call to RBO MR is required
        /// </summary>
        public bool CrossLondonToCheck
        {
            get { return crossLondonToCheck; }
        }

        /// <summary>
        /// Did RBO GL call contain a Zonal indicator flag.
        /// If flag is yes, then post timetable validation call to RBO MR is required
        /// </summary>
        public bool ZonalIndicatorToCheck
        {
            get { return zonalIndicatorToCheck; }
        }

        /// <summary>
        /// Did RBO GL call contain Visit CRS locations. 
        /// If yes, then post timetable validation call to RBO MR is required
        /// </summary>
        public bool VisitCRSToCheck
        {
            get { return visitCRSToCheck; }
        }

        /// <summary>
        /// The raw output from the RBO GL request (to be passed in to any future RBO MR calls)
        /// </summary>
        public string OutputGL
        {
            get { return outputGL; }
        }

		/// <summary>
		/// Resource ids for text of message(s) to be displayed
		/// to user as a result of any errors during processing
		/// </summary>
		public ArrayList ErrorResourceIds
		{
			get { return this.errorResourceIds; }
        }

        #endregion

        #region Public methods

        /// <summary>
		/// Add resource id for an error msg, but only if this
		/// one is not already present in the msg array ...
		/// </summary>
		public void AddErrorMessage(string resourceId) 
		{
			foreach (string rid in errorResourceIds)
			{
				if	(rid.Equals(resourceId))
				{
					return;
				}
			}

			errorResourceIds.Add(resourceId);
		}

		/// <summary>
		/// Populate those properties that are derived 
		/// from the results of the RBO GC call
		/// </summary>
		public void PopulateFromGCOutput(string gcOutput, TDDateTime inputDateTime)
		{

			int adjustedHrs = Int32.Parse(gcOutput.Substring(0, 2));
			int adjustedMin = Int32.Parse(gcOutput.Substring(2, 2));

			DateTime originalDateTime = inputDateTime.GetDateTime();

			if	(gcOutput.Substring(5, 1).Equals("Y"))	// adjusted time is next day ...
			{
				originalDateTime += new TimeSpan(1, 0, 0, 0);
			}

			adjustedDateTime = new TDDateTime(originalDateTime.Year, originalDateTime.Month, 
													originalDateTime.Day, adjustedHrs, adjustedMin, 0);

			changesAllowed = !(gcOutput.Substring(4, 1).Equals("N"));

			bool excludeTrains = gcOutput.Substring(6, 1).Equals("N");

			int trainCount = Int32.Parse(gcOutput.Substring(7, 4));

			// if there are more than 20 include UIDs the TTBO can't
			//  cope, so we ignore the list in this case and let the
			//  post-timetable GD call filter out the train ...

			if	(!(excludeTrains || trainCount > MAX_INCLUDE_UIDS))
			{
				for (int i = 0; i < trainCount; i++) 
				{
					if	(excludeTrains) 
					{
						excludeTrainUids.Add(gcOutput.Substring(11 + (i * 6), 6));
					}
					else
					{
						includeTrainUids.Add(gcOutput.Substring(11 + (i * 6), 6));
					}
				}
			}
			
			int rcCount = Int32.Parse(gcOutput.Substring(371, 4));

			StringBuilder sb = new StringBuilder(20);

			for (int i = 0; i < rcCount; i++) 
			{
				sb.Append(gcOutput.Substring(375 + (i * 2), 2).PadRight(2, ' '));
			}

			restrictionCodesToReapply = sb.ToString();

			// The RE GC returns a restriction code count of 1 but no actual 
			//  restriction codes if we need to call RE GD post-timetable
			//  to recheck the restrictions for connecting TOC's.
		
			if	(rcCount == 1 && restrictionCodesToReapply.Equals("  "))
			{
				restrictionCodesToReapply = string.Empty;
				connectingTocsToCheck = true;
			}

			bool emptyTicketList = false;

			ArrayList ticketTocs		= new ArrayList();
			ArrayList includeRouteTocs  = new ArrayList();
			ArrayList excludeRouteTocs  = new ArrayList();

			for (int i = 0; i < 40; i++) 
			{
				string tocEntry = gcOutput.Substring(415 + (i * 4), 4);

				if	(tocEntry.Substring(0, 2).Equals("  ") && tocEntry.Substring(3, 1).Equals("C"))
				{
					emptyTicketList = true;
				}
				else if (tocEntry.Substring(3, 1).Equals("C") || tocEntry.Substring(3, 1).Equals("M"))
				{
					ticketTocs.Add(tocEntry.Substring(0, 2));
				}

				if (tocEntry.Substring(2, 1).Equals("I"))
				{
					includeRouteTocs.Add(tocEntry.Substring(0, 2));
				}
				else if (tocEntry.Substring(2, 1).Equals("E"))
				{
					excludeRouteTocs.Add(tocEntry.Substring(0, 2));
				}
			}

			if	(ticketTocs.Count == 0)
			{
				emptyTicketList = true;
			}

			if	(emptyTicketList)
			{
				foreach (string toc in includeRouteTocs)
				{
					includeTocs.Add(new TocDto(toc));
				}

				foreach (string toc in excludeRouteTocs)
				{
					excludeTocs.Add(new TocDto(toc));
				}
				
				return;
			}

			bool includesOnly = false;
			bool excludesOnly = false;

			foreach (string tktToc in ticketTocs)
			{
				if	(tktToc.Length > 0 && !tktToc.Equals("  "))  
				{
					foreach (string incToc in includeRouteTocs)
					{
						if	(incToc.Equals(tktToc))
						{
							includesOnly = true;
							break;
						}
					}

					foreach (string excToc in excludeRouteTocs)
					{
						if	(excToc.Equals(tktToc))
						{
							excludesOnly = true;
							break;
						}
					}
				}
				
				if (includesOnly) 
				{
					break;
				}
			}

			if	(includesOnly)
			{
				foreach (string tktToc in ticketTocs)
				{
					foreach (string incToc in includeRouteTocs)
					{
						if	(incToc.Equals(tktToc))
						{
							includeTocs.Add(new TocDto(incToc));
						}
					}
				}
			}
			else if	(excludesOnly)
			{
				foreach (string tktToc in ticketTocs)
				{
					foreach (string excToc in excludeRouteTocs)
					{
						if	(excToc.Equals(tktToc))
						{
							excludeTocs.Add(new TocDto(excToc));
						}
					}
				}
			}
			else
			{
				foreach (string tktToc in ticketTocs)
				{
					includeTocs.Add(new TocDto(tktToc));
				}
			}
		}

		/// <summary>
		/// Populate those properties that are derived 
		/// from the results of the RBO GN call
		/// </summary>
		public void PopulateFromGNOutput(string gnOutput)
		{
			for (int i = 0; i < 10; i++) 
			{
				string crs = gnOutput.Substring(i * 4, 3);
 
				if	(!crs.Equals("   "))
				{
					if	(gnOutput.Substring(3 + (i * 4), 1).Equals("I"))
					{
						includeCrsLocations.Add(crs);
					} 
					else if	(gnOutput.Substring(3 + (i * 4), 1).Equals("E"))
					{
						excludeCrsLocations.Add(crs);
					}
				}
			}
		}

        /// <summary>
        /// Populate those properties that are derived 
        /// from the results of the RBO GL call
        /// </summary>
        public void PopulateFromGLOutput(string glOutput)
        {
            // Identify if the GL output indicates
            // a) cross London check = Y
            // b) zonal indicator = Y
            // c) visit crs contains entries.
            // Any of these indicates we should perform an RBO MR call after services are found

            this.outputGL = glOutput;

            int currentIndex = 0;

            #region VISIT-CRS

            // Check Visit-CRS list

            int crsCount = 0;
            Int32.TryParse(glOutput.Substring(currentIndex, 4), out crsCount); // VISIT-CRS-COUNT
            currentIndex += 4;

            string crsList = glOutput.Substring(currentIndex, (3 * 20)); // VISIT-CRS
            currentIndex += (3 * 20);

            // Check there are CRS locations specified
            if ((crsCount > 0) && (!string.IsNullOrEmpty(crsList.Trim())))
            {
                // VISIT-CRS values exist, flag that restrict using MR should be called after services returned
                visitCRSToCheck = true;
            }
            
            #endregion

            #region ROUTE-ENTRY

            // Get the CRS locations to include/exclude for the fare route code
            string routeCount = glOutput.Substring(currentIndex, 4);  // ROUTE-COUNT
            currentIndex += 4;

            // There should only ever be 1 route code as the request was made for a single fare

            for (int i = 0; i < 99; i++) // ROUTE-ENTRY
            {
                // The route code the following CRS locations apply to
                string routeCode = glOutput.Substring(currentIndex, 5);  // RT-ROUTE-CODE
                currentIndex += 5;

                string routeXLondonMarker = glOutput.Substring(currentIndex, 1); // RT-XLONDON-MARKER
                currentIndex += 1;

                // Go through each CRS location and add to the correct array
                for (int j = 0; j < 10; j++)  // RT-INC-EXC-CRS
                {
                    string crs = glOutput.Substring(currentIndex, 3);  // RT-ROUTE-CRS
                    currentIndex += 3;

                    string includeExclude = glOutput.Substring(currentIndex, 1);  // RT-INC-EXC-IND
                    currentIndex += 1;

                    if (!crs.Equals("   "))
                    {
                        if (includeExclude.Equals("I"))
                        {
                            includeCrsLocations.Add(crs);
                        }
                        else if (includeExclude.Equals("E"))
                        {
                            excludeCrsLocations.Add(crs);
                        }
                    }
                }
            }

            #endregion

            #region LONDON-CHECK-FLAG

            string londonCheckFlag = glOutput.Substring(currentIndex, 1);  // LONDON-CHECK-FLAG
            currentIndex += 1;

            if (londonCheckFlag.Equals("Y"))
            {
                crossLondonToCheck = true;
            }

            string londonCRS = glOutput.Substring(currentIndex, (3 * 20));  // LONDON-CRS
            currentIndex += (3 * 20);

            #endregion

            #region TT-TOCS

            // Not concerned about the TT-TOCS as this info will have been obtained by the GC call

            currentIndex += 2; // TT-TOC-COUNT
            currentIndex += (2 + 1) * 15; // TT-TOC, TT-TOC-INC-EXC, repeated 15 times

            #endregion

            #region RT-ROUTE-TOCS

            // Not concerned about the RT-ROUTE-TOCS as this info will have been obtained by the GC call

            currentIndex += (2 + 1) * 10 * 99; // RT-ROUTE-TOC, RT-INC-EXC-TOC, repeated 10 times, and repeated 99 times

            #endregion

            #region ZONAL-INDICATOR

            string zonalIndicator = glOutput.Substring(currentIndex, 1);  // ZONAL-INDICATOR
            currentIndex += 1;

            if (zonalIndicator.Equals("Y"))
            {
                zonalIndicatorToCheck = true;
            }

            #endregion

        }

		/// <summary>
		/// Returns string summarising contents, 
		/// for testing/debugging use only.
		/// </summary>
		public override string ToString()
		{

			StringBuilder sb = new StringBuilder();		

			sb.Append("\r\nInclude TOCs: ");

			foreach (TocDto t in includeTocs)
			{
				sb.Append(t.Code + ", ");
			}

			sb.Append("\r\nExclude TOCs: ");

			foreach (TocDto t in excludeTocs)
			{
				sb.Append(t.Code + ", ");
			}

			sb.Append("\r\nInclude UIDs: ");

			foreach (string uid in includeTrainUids)
			{
				sb.Append(uid + ", ");
			}

			sb.Append("\r\nExclude UIDs: ");

			foreach (string uid in excludeTrainUids)
			{
				sb.Append(uid + ", ");
			}

			sb.Append("\r\nInclude locations: ");

			foreach (string crs in includeCrsLocations)
			{
				sb.Append(crs + ", ");
			}

			sb.Append("\r\nExclude locations: ");

			foreach (string crs in excludeCrsLocations)
			{
				sb.Append(crs + ", ");
			}

			sb.Append("\r\nRestrictions: ");

			sb.Append(restrictionCodesToReapply + ", ");

			sb.Append("\r\n");

			if	(adjustedDateTime != null)
			{
				sb.Append("Adjusted date/time: " + adjustedDateTime.ToString("yyyy-MM-dd HH:mm") + " ");
			}
			
			sb.Append("changes allowed = " + changesAllowed);

			return sb.ToString();
        }

        #endregion
    }
}
