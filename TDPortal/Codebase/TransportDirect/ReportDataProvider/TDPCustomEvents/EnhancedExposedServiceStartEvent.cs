// *********************************************** 
// NAME                 : EnhancedExposedServiceStartEvent.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 17/11/2005
// DESCRIPTION  : Logging Start Event for Enhanced Exposed Web Services Framework
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/EnhancedExposedServiceStartEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:20   mturner
//Initial revision.
//
//   Rev 1.3   Feb 08 2006 12:09:28   mdambrine
//Added the serialisable attribute
//
//   Rev 1.2   Nov 25 2005 14:21:48   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//
//   Rev 1.1   Nov 22 2005 16:23:10   rgreenwood
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
	/// Summary description for EnhancedExposedServiceStartEvent.
	/// </summary>
	[Serializable]
	public class EnhancedExposedServiceStartEvent : EnhancedExposedServiceEvent
	{
		public EnhancedExposedServiceStartEvent(bool successful, ExposedServiceContext serviceRefContext)
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
