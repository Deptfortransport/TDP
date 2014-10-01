// *********************************************** 
// NAME                 : ExternalLinksKeys.cs
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Defines the different ExternalLinks Keys.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/ExternalLinksKeys.cs-arc  $
//
//   Rev 1.1   Jan 20 2013 16:25:48   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:23:24   mturner
//Initial revision.
//
//   Rev 1.2   Aug 16 2005 15:37:52   jgeorge
//Automatically merged for stream2559
//
//   Rev 1.1.1.1   Jun 29 2005 10:49:20   jbroome
//Added keys for accessibility links
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.1.1.0   Jun 15 2005 13:18:56   jbroome
//Creating branch
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.1   Jun 14 2005 18:58:42   rgeraghty
//Updated to include IR association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:56:16   rgeraghty
//Initial revision.

using System;

namespace TransportDirect.UserPortal.ExternalLinkService
{
	/// <summary>
	/// Definitions of the different ExternalLinks Keys.
	/// </summary>
	public class ExternalLinksKeys
	{
		public const string AccessibilityInformation_Car = "AccessibilityInformation.Car";
		public const string AccessibilityInformation_Air = "AccessibilityInformation.Air";
		public const string AccessibilityInformation_Bus = "AccessibilityInformation.Bus";
		public const string AccessibilityInformation_Coach = "AccessibilityInformation.Coach";
		public const string AccessibilityInformation_Cycle = "AccessibilityInformation.Cycle";
		public const string AccessibilityInformation_Drt = "AccessibilityInformation.Drt";
		public const string AccessibilityInformation_Ferry = "AccessibilityInformation.Ferry";
		public const string AccessibilityInformation_Metro = "AccessibilityInformation.Metro";
		public const string AccessibilityInformation_Rail = "AccessibilityInformation.Rail";
		public const string AccessibilityInformation_Taxi = "AccessibilityInformation.Taxi";
        public const string AccessibilityInformation_Telecabine = "AccessibilityInformation.Telecabine";
        public const string AccessibilityInformation_Tram = "AccessibilityInformation.Tram";
		public const string AccessibilityInformation_Underground = "AccessibilityInformation.Underground";
		public const string AccessibilityInformation_RailReplacementBus = "AccessibilityInformation.RailReplacementBus";
		public const string AccessibilityInformation_BeforeTravel = "AccessibilityInformation.BeforeTravel";
	}
}
