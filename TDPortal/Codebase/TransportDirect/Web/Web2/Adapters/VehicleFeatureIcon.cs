// *********************************************** 
// NAME                 : VehicleFeatureIcon.ascx.cs 
// AUTHOR               : Rob Greenwood 
// DATE CREATED         : 23/06/2004
// DESCRIPTION			: Control to display the series of on-board
//						  facilities for a given vehicle type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/VehicleFeatureIcon.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:32   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:17:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jul 18 2005 15:14:18   RPhilpott
//Add description property to VehicleFeaturesIcon class.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 08 2005 11:14:00   rgreenwood
//Initial revision.
//Resolution for 1950: ACP TTBO Update process freezes if cant contact TTBO server
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//


using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for VehicleFeatureIcon.
	/// </summary>
	public class VehicleFeatureIcon
	{
		#region Variables
		private string imageUrlResource;
		private string toolTipResource;
		private string altTextResource;
		private string description;
		#endregion

		#region Constructor
		public VehicleFeatureIcon()
		{
		}

		public VehicleFeatureIcon(string url, string toolTip, string altText, string description)
		{
			this.imageUrlResource = url;
			this.toolTipResource = toolTip;
			this.altTextResource = altText;
			this.description = description;

		}
		#endregion

		#region Properties

		public string ImageUrlResource
		{
			get 
			{
				return imageUrlResource;
			}
			set
			{
				imageUrlResource = value;
			}
		}

		public string ToolTipResource
		{
			get 
			{
				return toolTipResource;
			}
			set
			{
				toolTipResource = value;
			}
		}
		
		public string AltTextResource
		{
			get 
			{
				return altTextResource;
			}
			set
			{
				altTextResource = value;
			}
		}

		public string Description
		{
			get 
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		#endregion
	}
}
