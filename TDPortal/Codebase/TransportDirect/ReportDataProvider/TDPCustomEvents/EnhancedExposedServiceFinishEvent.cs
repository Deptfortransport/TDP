// *********************************************** 
// NAME                 : EnhancedExposedServiceFinishEvent.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 17/11/2005
// DESCRIPTION  : Logging Finish Event for Enhanced Exposed Web Services Framework
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/EnhancedExposedServiceFinishEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:20   mturner
//Initial revision.
//
//   Rev 1.3   Feb 08 2006 12:09:28   mdambrine
//Added the serialisable attribute
//
//   Rev 1.2   Nov 25 2005 14:37:20   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 22 2005 16:23:12   rgreenwood
//Changed using statement to match changed namespace for EnhancedExposedServices
//
//   Rev 1.0   Nov 21 2005 13:13:34   rgreenwood
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


using System;
using TransportDirect.EnhancedExposedServices.Common;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents


{
	/// <summary>
	/// Summary description for EnhancedExposedServiceFinishEvent.
	/// </summary>
	[Serializable]
	public class EnhancedExposedServiceFinishEvent : EnhancedExposedServiceEvent
	{
		public EnhancedExposedServiceFinishEvent(bool successful, ExposedServiceContext serviceRefContext)
		{
			//Set DateTime property
			this.EventTime = DateTime.Now;
			
			//Set service ref
			this.ServiceReferenceContext = serviceRefContext;

			//set succesful
			this.Successful = successful;
		}
	}
}
