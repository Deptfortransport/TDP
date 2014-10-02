//********************************************************************************
//NAME         : LookupLocationGroupResult.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-26
//DESCRIPTION  : Extracts all Group locations relating to a given NLC 
//				 from the result of an LBO call.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupLocationGroupResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:10   mturner
//Initial revision.
//
//   Rev 1.2   Nov 24 2005 18:20:58   RPhilpott
//1) Use NLC lookups, not GRP.
//
//2) Generalise to get stations in a group as well as v.v.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Apr 25 2005 21:12:36   RPhilpott
//Change LBO lookups to get all groups to which a location belongs, and remove associated redundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.0   Apr 25 2005 21:10:52   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Extracts all Group locations relating to a 
	/// given NCL code from the result of an LBO call.
	/// </summary>
	[Serializable]
	public class LookupLocationGroupResult
	{

		private const int HEADER_LENGTH = 13;
		private const int NLC_LENGTH  = 9;
		private const int CRS_LENGTH  = 3;

		private ArrayList groupLocations = new ArrayList();
		private bool fatalError = false;

		public LocationDto[] GroupLocations
		{
			get { return ((LocationDto[])groupLocations.ToArray(typeof(LocationDto))); }
		}

		public bool FatalError
		{
			get { return fatalError; }
		}

		public LookupLocationGroupResult(BusinessObjectOutput output, string inputNlc, bool fromIndividualToGroup) 
		{
			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				fatalError = true;
				return;
			}

			if	(output.RecordDetails.Length < 2)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Not enough entries in output header - NLC = " + inputNlc));
				fatalError = true;
				return;
			}

			if	(output.RecordDetails[1].RecordsRemaining > 0)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Not all group locations returned for NLC " + inputNlc));
			}

			int entryLength = NLC_LENGTH + (fromIndividualToGroup ? 0 : CRS_LENGTH);

			string entry = string.Empty;
			string nlc	 = string.Empty;
			string crs	 = string.Empty;

			for (int i = 0; i < output.RecordDetails[1].RecordOutput; i++)
			{
				entry = output.OutputBody.Substring(HEADER_LENGTH + (i * entryLength), entryLength);

				nlc = entry.Substring(3, 4);		// strip off leading "70 " and trailing "00"

				if	(fromIndividualToGroup)
				{
					crs = null;
				}
				else
				{
					crs = entry.Substring(9, 3);
				}

				if	(!nlc.Equals(inputNlc))
				{	
					groupLocations.Add(new LocationDto(crs, nlc));
				}
			}
		}


	}
}

