// *********************************************** 
// NAME                 : VehicleFeaturesControl.ascx.cs 
// AUTHOR               : Rob Greenwood 
// DATE CREATED         : 23/06/2004
// DESCRIPTION			: Control to display the series of on-board
//						  facilities for a given vehicle type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/RailVehicleFeaturesIconMapper.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:28   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:17:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 19 2005 20:35:42   RPhilpott
//Fix cases where multiple first class features present.
//Resolution for 2653: Del 8 DN062 Multiple First Class icons
//
//   Rev 1.2   Jul 18 2005 15:14:18   RPhilpott
//Add description property to VehicleFeaturesIcon class.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 11 2005 11:08:52   rgreenwood
//Removed faulty code
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 08 2005 11:09:22   rgreenwood
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for RailVehicleFeaturesIconMapper.
	/// </summary>
	public class RailVehicleFeaturesIconMapper
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public ArrayList GetIcons(int[] Icons)
		{

			ArrayList featureIcons = new ArrayList();

			if (Icons != null)
			{
			
				bool feature21Present = false;
				bool feature22Present = false;
				bool feature30Present = false;
				bool feature31Present = false;
				bool feature32Present = false;

				for (int i=0; i < Icons.Length; i++)
				{
					//get the number from the Icons array
					if (Icons.GetValue(i) != null)
					{
						int iconIndex = (int)Icons.GetValue(i);

						// simple cases first -- 1:1 relationship between features and icons ...
						
						switch (iconIndex) 
						{
							case 0:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.0", "RailVehicleFeaturesIconMapper.AltTextToolTip.0", "RailVehicleFeaturesIconMapper.AltTextToolTip.0", "RailVehicleFeaturesIconMapper.AltTextToolTip.0"));
								break;
							case 1:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.1", "RailVehicleFeaturesIconMapper.AltTextToolTip.1", "RailVehicleFeaturesIconMapper.AltTextToolTip.1", "RailVehicleFeaturesIconMapper.AltTextToolTip.1"));
								break;
							case 2:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.2", "RailVehicleFeaturesIconMapper.AltTextToolTip.2", "RailVehicleFeaturesIconMapper.AltTextToolTip.2", "RailVehicleFeaturesIconMapper.AltTextToolTip.2"));
								break;
							case 3:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.3", "RailVehicleFeaturesIconMapper.AltTextToolTip.3", "RailVehicleFeaturesIconMapper.AltTextToolTip.3", "RailVehicleFeaturesIconMapper.AltTextToolTip.3"));
								break;
							case 4:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.4", "RailVehicleFeaturesIconMapper.AltTextToolTip.4", "RailVehicleFeaturesIconMapper.AltTextToolTip.4", "RailVehicleFeaturesIconMapper.AltTextToolTip.4"));
								break;
							case 5:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.5", "RailVehicleFeaturesIconMapper.AltTextToolTip.5", "RailVehicleFeaturesIconMapper.AltTextToolTip.5", "RailVehicleFeaturesIconMapper.AltTextToolTip.5"));
								break;
							case 10:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.10", "RailVehicleFeaturesIconMapper.AltTextToolTip.10", "RailVehicleFeaturesIconMapper.AltTextToolTip.10", "RailVehicleFeaturesIconMapper.AltTextToolTip.10"));
								break;
							case 11:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.11", "RailVehicleFeaturesIconMapper.AltTextToolTip.11", "RailVehicleFeaturesIconMapper.AltTextToolTip.11", "RailVehicleFeaturesIconMapper.AltTextToolTip.11"));
								break;
							case 12:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.12", "RailVehicleFeaturesIconMapper.AltTextToolTip.12", "RailVehicleFeaturesIconMapper.AltTextToolTip.12", "RailVehicleFeaturesIconMapper.AltTextToolTip.12"));
								break;
							case 13:
								featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.13", "RailVehicleFeaturesIconMapper.AltTextToolTip.13", "RailVehicleFeaturesIconMapper.AltTextToolTip.13", "RailVehicleFeaturesIconMapper.AltTextToolTip.13"));
								break;

							// flags for more complex cases to be handled later ...
							case 21:
								feature21Present = true;								
								break;
							case 22:
								feature22Present = true;								
								break;
							case 30:
								feature30Present = true;								
								break;
							case 31:
								feature31Present = true;								
								break;
							case 32:
								feature32Present = true;								
								break;

							default :
								//do nothing
								break;

						}
						
					}
				}

				// now do the more complex cases -- to avoid "1st" symbol appearing twice ...

				// if 20 or 21 present, the seating "1st" sysmbol will be displayed and 
				//  we should not use the combined 1st+berth symbols for 30 or 31 ...

				if	(feature21Present)
				{
					featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.21", "RailVehicleFeaturesIconMapper.AltTextToolTip.21", "RailVehicleFeaturesIconMapper.AltTextToolTip.21", "RailVehicleFeaturesIconMapper.AltTextToolTip.21"));

					if	(feature30Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30"));
					}
					else if (feature31Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31"));
					}
				}
				else if	(feature22Present)
				{
					featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.22", "RailVehicleFeaturesIconMapper.AltTextToolTip.22", "RailVehicleFeaturesIconMapper.AltTextToolTip.22", "RailVehicleFeaturesIconMapper.AltTextToolTip.22"));

					if	(feature30Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30"));
					}
					else if (feature31Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31"));
					}
				}
				else  // neither 21 nor 22 present, so we can use "combined" symbols for 30 or 31 ...
				{
					if	(feature30Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30", "RailVehicleFeaturesIconMapper.AltTextToolTip.30"));
					}
					else if (feature31Present)
					{
						featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31", "RailVehicleFeaturesIconMapper.AltTextToolTip.31"));
					}
				}


				if	(feature32Present)		// mutually exclusive with 30 and 31
				{
					featureIcons.Add(new VehicleFeatureIcon("RailVehicleFeaturesIconMapper.ImageURL.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.32", "RailVehicleFeaturesIconMapper.AltTextToolTip.32"));
				}
			}
			
			return featureIcons;
		}

		
	}
}
