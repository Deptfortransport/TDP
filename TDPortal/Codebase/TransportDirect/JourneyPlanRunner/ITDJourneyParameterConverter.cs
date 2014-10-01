// ****************************************************************
// NAME         : ITDJourneyParameterConverter.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-07
// DESCRIPTION  : A common interface to allow the conversion from 
// TDJourneyParameters to TDJourneyRequest for each sub class of TDJourneyParameters
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/ITDJourneyParameterConverter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:42   mturner
//Initial revision.
//
//   Rev 1.1   Jun 21 2005 14:55:52   asinclair
//Added comments
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:21:10   asinclair
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for ITDJourneyParameterConverter.
	/// </summary>
	public interface ITDJourneyParameterConverter
	{
		ITDJourneyRequest Convert (TDJourneyParameters parameters, TDDateTime outwardDateTime, TDDateTime returnDateTime);
	}
}
