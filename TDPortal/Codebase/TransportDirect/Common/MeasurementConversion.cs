 // ********************************************************************* 
// NAME                 : MeasurementConversion.cs 
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 25/01/2005 
// DESCRIPTION			: This is a generic class that is used to 
//						  convert units of measurement from Imperial to Metric
//                        and vice versa.
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/MeasurementConversion.cs-arc  $ 
//
//   Rev 1.2   Jan 09 2013 11:34:04   mmodi
//Updated with conversion method to return double values
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Nov 11 2010 16:11:04   apatel
//Updated convert function so it doesn't return empty string and put an error handling code.
//Resolution for 5637: Gradient profiler UI fails with error
//
//   Rev 1.0   Nov 08 2007 12:19:04   mturner
//Initial revision.
//
//   Rev 1.7   Aug 29 2007 15:33:42   mmodi
//Added MetersPerLitreToLitresPer100Kilometers
//Resolution for 4488: Car journey details: MPG value not changed when units dropdown changed
//
//   Rev 1.6   Jun 28 2007 14:22:38   mmodi
//Added MetersPerLitre to MilesPerGallon conversion
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.5   Apr 29 2005 17:11:48   Ralavi
//Changed conversion factors for milesPerGallon to MetersPerLitre and LitresPer100Kilometers to MetersPerLitre.
//
//   Rev 1.4   Apr 13 2005 12:16:44   Ralavi
//Adding conversion methods to new units
//
//   Rev 1.3   Apr 01 2005 13:35:44   Ralavi
//Adding new methods for fuel consumption conversion
//
//   Rev 1.2   Feb 01 2005 10:22:36   rgreenwood
//Initialised formatResult = null
//
//   Rev 1.1   Jan 31 2005 15:51:56   rgreenwood
//moved variables into Convert method, and removed private static keywords, at the request of Peter Norell.
//
//   Rev 1.0   Jan 26 2005 15:51:32   ralavi
//Initial revision.

using System;
using System.Collections;
using System.Globalization;



namespace TransportDirect.Common
{
	/// <summary>
	/// Enumeration type
	/// </summary>
	public enum ConversionType
	{
		MilesToKilometres,
		KilometresToMiles,
		GallonsToLitres,
		LitresToGallons,
		MetresToMileage,
		MileageToMetres,
		MilesPerGallonToMetersPerLitre,
		MetersPerLitreToMilesPerGallon,
		LitresPer100KilometersToMetersPerLitre,
		MetersPerLitreToLitresPer100Kilometers,
		TenthOfPencePerLitre
	}
	/// <summary>
	/// Summary description for MeasurementConversion.
	/// </summary>
	public class MeasurementConversion
	{
		
        /// <summary>
        /// Converts value into a formatted string (#,###.##)
        /// </summary>
        /// <param name="unitToConvert"></param>
        /// <param name="type"></param>
        /// <returns></returns>
		public static string Convert(double unitToConvert, ConversionType type)
		{
		    double result;
			string formatResult = null;

            try
            {
                result = MeasurementConversion.ConvertValue(unitToConvert, type);

                switch (type)
                {
                    case ConversionType.MilesToKilometres:

                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.KilometresToMiles:

                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.MetresToMileage:

                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.MileageToMetres:

                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.LitresToGallons:

                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.GallonsToLitres:
                        
                        formatResult = result.ToString("#,###.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.MilesPerGallonToMetersPerLitre:

                        formatResult = result.ToString("####.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.MetersPerLitreToMilesPerGallon:

                        formatResult = result.ToString("####.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.LitresPer100KilometersToMetersPerLitre:

                        formatResult = result.ToString("####.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.MetersPerLitreToLitresPer100Kilometers:

                        formatResult = result.ToString("####.##", NumberFormatInfo.CurrentInfo);
                        break;

                    case ConversionType.TenthOfPencePerLitre:

                        formatResult = result.ToString("####.##", NumberFormatInfo.CurrentInfo);
                        break;
                }
            }
            catch
            {
                formatResult = "0.0";
            }

            if (string.IsNullOrEmpty(formatResult.Trim()))
            {
                formatResult = "0.0";
            }
			
			return formatResult;
		}

        /// <summary>
        /// Converts value, not truncating any value
        /// </summary>
        /// <param name="unitToConvert"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static double ConvertValue(double unitToConvert, ConversionType type)
        {

            double conversionFactor;
            double result = 0;

            try
            {
                // Should retrieve the conversion factor from the Properties Service.

                switch (type)
                {
                    case ConversionType.MilesToKilometres:

                        conversionFactor = 0.6214;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.KilometresToMiles:

                        conversionFactor = 1.6090;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.MetresToMileage:

                        conversionFactor = 1609;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.MileageToMetres:

                        conversionFactor = 0.0006214;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.LitresToGallons:

                        conversionFactor = 4.5460;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.GallonsToLitres:
                    
                        conversionFactor = 0.2200;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.MilesPerGallonToMetersPerLitre:

                        conversionFactor = 354;
                        result = unitToConvert * conversionFactor;
                        break;

                    case ConversionType.MetersPerLitreToMilesPerGallon:

                        conversionFactor = 354;
                        result = unitToConvert / conversionFactor;
                        break;

                    case ConversionType.LitresPer100KilometersToMetersPerLitre:

                        conversionFactor = 100000;
                        result = conversionFactor / unitToConvert;
                        break;

                    case ConversionType.MetersPerLitreToLitresPer100Kilometers:

                        conversionFactor = 100000;
                        result = conversionFactor / unitToConvert;
                        break;

                    case ConversionType.TenthOfPencePerLitre:

                        conversionFactor = 10;
                        result = unitToConvert * conversionFactor;
                        break;
                }
            }
            catch
            {
                result = 0;
            }

            return result;
        }
	
	}
}
