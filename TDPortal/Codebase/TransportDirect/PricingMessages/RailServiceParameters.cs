//********************************************************************************
//NAME         : TTBOParametersDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to pass CJP/TTBO service selection criteria 
//                between RBOGateway and 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/RailServiceParameters.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 08:54:56   mmodi
//Added additional parameters to allow validation of services and fares using the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:35:54   mturner
//Initial revision.
//
//   Rev 1.3   Dec 05 2005 17:57:04   RPhilpott
//Add flag to indicate if GD call needed becuase of connecting TOC restrictions.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.2   Apr 13 2005 13:35:34   RPhilpott
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
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Summary description for RailServiceParameters.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class RailServiceParameters
	{
		private TocDto[] includeTocs;
		private TocDto[] excludeTocs;
		private string[] includeTrainUids;
		private string[] excludeTrainUids;
		private TDLocation[] includeLocations;
		private TDLocation[] excludeLocations; 
		private string restrictionCodesToReapply;
		private TDDateTime adjustedDateTime = null;
		private bool changesAllowed = true;
		private bool connectingTocsToCheck = false;
        private bool crossLondonToCheck = false;
        private bool zonalIndicatorToCheck = false;
        private bool visitCRSToCheck = false;
        private string outputGL = string.Empty;

		private ArrayList errorResourceIds;

		public RailServiceParameters(TTBOParametersDto ttboParams, TDLocation[] includeLocs, TDLocation[] excludeLocs)
		{
			this.includeTocs = ttboParams.IncludeTocs;
			this.excludeTocs = ttboParams.ExcludeTocs;
			this.includeTrainUids = ttboParams.IncludeTrainUids;
			this.excludeTrainUids = ttboParams.ExcludeTrainUids;
			this.restrictionCodesToReapply = ttboParams.RestrictionCodesToReapply;
			this.adjustedDateTime = ttboParams.AdjustedDateTime;
			this.changesAllowed = ttboParams.ChangesAllowed;
			this.connectingTocsToCheck = ttboParams.ConnectingTocsToCheck;

            this.crossLondonToCheck = ttboParams.CrossLondonToCheck;
            this.zonalIndicatorToCheck = ttboParams.ZonalIndicatorToCheck;
            this.visitCRSToCheck = ttboParams.VisitCRSToCheck;
            this.outputGL = ttboParams.OutputGL;

			this.includeLocations = includeLocs;
			this.excludeLocations = excludeLocs;

			this.errorResourceIds = ttboParams.ErrorResourceIds;
		}

		/// <summary>
		/// UIDs of trains, at least one of which must 
		/// be included in each journey returned.
		/// </summary>
		public TocDto[] IncludeTocs
		{
			get { return (includeTocs != null ? includeTocs : new TocDto[0]); }
		}

		/// <summary>
		/// UIDs of trains, none of which must appear in journeys returned
		/// </summary>
		public TocDto[] ExcludeTocs
		{
			get { return (excludeTocs != null ? excludeTocs : new TocDto[0]); }
		}

		/// <summary>
		/// Array of locations, at least one of which each  
		/// must be passed through by each journey returned
		/// </summary>
		public string[] IncludeTrainUids
		{
			get { return (includeTrainUids != null ? includeTrainUids : new string[0]); }
		}

		/// <summary>
		/// Array of locations, through which returned journeys must NOT pass
		/// </summary>
		public string[] ExcludeTrainUids
		{
			get { return (excludeTrainUids != null ? excludeTrainUids : new string[0]); }
		}

		/// <summary>
		/// TOC's, at least one of which must appear in each journey returned
		/// </summary>
		public TDLocation[] IncludeLocations
		{
			get { return (includeLocations != null ? includeLocations : new TDLocation[0]); }
		}

		/// <summary>
		/// TOC's, none which must appear in journeys returned
		/// </summary>
		public TDLocation[] ExcludeLocations
		{
			get { return (excludeLocations != null ? excludeLocations : new TDLocation[0]); }
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
		public string[] ErrorResourceIds
		{
			get { return (string[])(this.errorResourceIds.ToArray(typeof(string))); } 
		}


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

	}
}
