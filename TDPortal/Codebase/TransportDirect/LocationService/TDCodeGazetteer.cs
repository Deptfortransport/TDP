// *********************************************** 
// NAME                 : TDCodeGazetteer.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 17/01/2005
// DESCRIPTION  : Real Implementation of ITDCodeGazetteer interface
// Accesses and formats results from ESRI code gazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDCodeGazetteer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:22   mturner
//Initial revision.
//
//   Rev 1.6   Mar 07 2006 15:51:12   CRees
//Resolution for IR3496 - Vantives 3964483 and 3865878. Amends call to GAZ to add mode directive to allow gaz to pre-filter results.
//
//   Rev 1.5   Mar 01 2005 17:08:34   passuied
//comments addition after code review
//
//   Rev 1.4   Feb 10 2005 13:52:10   passuied
//fixed minor problem to get maxreturnedRecords from properties
//
//   Rev 1.3   Feb 01 2005 14:51:04   passuied
//added logging for code gazetteer
//
//   Rev 1.2   Jan 26 2005 18:06:50   passuied
//tidying up
//
//   Rev 1.1   Jan 19 2005 12:07:12   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.0   Jan 18 2005 17:38:36   passuied
//Initial revision.

using System;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	///  Real Implementation of ITDCodeGazetteer interface
	/// Accesses and formats results from ESRI code gazetteer
	/// </summary>
	public class TDCodeGazetteer : ITDCodeGazetteer
	{
		private string gazetteerId = string.Empty;
		private int maxReturnedRecords = 0;
		private const int maxLength = -1;
		private const int minLength = -1;
		
		[NonSerialized()]
		private GazopsWeb.GazopsWeb gazopsWeb;

		private XmlGazetteerHandler xmlHandler;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TDCodeGazetteer()
		{
			
			gazetteerId = Properties.Current["locationservice.codegazetteerid"];
			if (gazetteerId == null || gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					"locationservice.codegazetteerid not set or set to a wrong format");


				Logger.Write(oe);
				throw new TDException("locationservice.codegazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSCodeGazetteerIDInvalid);
			}


			// Get MaxReturnedRecords Property from PropertyService
			
			try
			{
				maxReturnedRecords = Convert.ToInt32(
					Properties.Current["locationservice.maxreturnedrecords"],
					CultureInfo.CurrentCulture.NumberFormat);
			}
			catch (FormatException)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					"locationservice.maxreturnedrecords not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.maxreturnedrecords not set or set to a wrong format", true, TDExceptionIdentifier.LSCodeGazetteerMaxReturnPropertyInvalid);
			}
			
			


			xmlHandler = new XmlGazetteerHandler();
		}

		/// <summary>
		/// Read-only property that returns the GazopsWeb instance
		/// </summary>
		private GazopsWeb.GazopsWeb GazopsWeb
		{
			get 
			{
				if( gazopsWeb == null )
				{
					gazopsWeb = new GazopsWeb.GazopsWeb();
					gazopsWeb.Url = Properties.Current["locationservice.gazopsweburl"];
					if (gazopsWeb.Url == string.Empty)
					{
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,  TDTraceLevel.Error, "Unable to access the GazopsWeb Url property");
						Logger.Write(oe);
						throw new TDException ("Unable to access the GazopsWeb Url property", true, TDExceptionIdentifier.LSGazopURLUnavailable);
					}
				}
				return gazopsWeb;
			}
		}


		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		public TDCodeDetail[] FindCode( 
			string code)
		{
			Logging(DateTime.Now);
			string result = GazopsWeb.StreetMatch(code, false, gazetteerId, string.Empty, maxReturnedRecords, string.Empty);

			// read result with modetype filter set to all modeTypes!
			return xmlHandler.ReadCodeGazResult(result, new TDModeType[]{TDModeType.Air, TDModeType.Bus, TDModeType.Coach, TDModeType.Ferry, TDModeType.Metro, TDModeType.Rail, TDModeType.Undefined});
		}

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		public TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes)
		{

			// IR 3496 start CR - fix for Vantives: 3865878 and 3964483
			
			bool modeSelected = false;
			bool otherModeSelected = false;
			string modeSelection = String.Empty;
			
			// first parse the array to see if there are any modes selected. 
			// If so, create filter string for later.
			foreach (TDModeType modeInProgress in modeTypes)
			{ 
				//	Valid modes are Undefined, Rail, Bus, Coach, Air, Ferry, Metro
				if (modeInProgress != TDModeType.Undefined) 
				{
					switch (modeInProgress)
					{
						case TDModeType.Air:
							if (modeSelected) 
							{
								modeSelection = modeSelection + ",'Air'";
							}
							else
							{
								modeSelection = "'Air'";
								modeSelected = true;
							}
							break;
						case TDModeType.Bus:
							if (modeSelected) 
							{
								modeSelection = modeSelection + ",'Bus'";
							}
							else
							{
								modeSelection = "'Bus'";
								modeSelected = true;
							}
							break;
						case TDModeType.Coach:
							if (modeSelected) 
							{
								modeSelection = modeSelection + ",'Coach'";
							}
							else
							{
								modeSelection = "'Coach'";
								modeSelected = true;
							}
							break;
						case TDModeType.Ferry:
							if (modeSelected) 
							{
								modeSelection = modeSelection + ",'Ferry'";
							}
							else
							{
								modeSelection = "'Ferry'";
								modeSelected = true;
							}
							break;
						case TDModeType.Rail:
							if (modeSelected) 
							{
								modeSelection = modeSelection + ",'Rail'";
							}
							else
							{
								modeSelection = "'Rail'";
								modeSelected = true;
							}
							break;
						default:
							// 'Other' includes Tram, Metro and Underground.
							if (!otherModeSelected) 
							{
								if (modeSelected) 
								{
									modeSelection = modeSelection + ",'Other'";
									otherModeSelected=true;
								}
								else
								{
									modeSelection = "'Other'";
									modeSelected = true;
									otherModeSelected=true;
								}
							}
							break;
					} //switch
				}//if
			}//foreach
			//now check to see if any modes were selected, for which we need to add the directive.
			if (modeSelected) 
			{
				text = text + "[//GAZOPS_DIRECTIVE=MODE:" + modeSelection + "]";
			}

		// end CR IR 3496

			Logging(DateTime.Now);
			string result = GazopsWeb.PlaceNameMatch(text, fuzzy, maxLength, minLength, gazetteerId, string.Empty, maxReturnedRecords, string.Empty);

			return xmlHandler.ReadCodeGazResult(result, modeTypes);
		}

		#region Logging method
		/// <summary>
		/// Method called for logging purposes
		/// </summary>
		/// <param name="submitted">DateTime the event to be logged was submitted.</param>
		protected void Logging(DateTime submitted)
		{
			GazetteerEvent ge =  new GazetteerEvent(GazetteerEventCategory.GazetteerCode, submitted, string.Empty, false);
			Logger.Write(ge);
		}
		#endregion


	}
}
