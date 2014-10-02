// *********************************************** 
// NAME                 : XmlGazetteerHandler.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 18/01/2005
// DESCRIPTION  : Handler for Xml that is returned from ESRI Gazetteer.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/XmlGazetteerHandler.cs-arc  $
//
//   Rev 1.1   Aug 28 2012 10:19:56   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.0   Nov 08 2007 12:25:26   mturner
//Initial revision.
//
//   Rev 1.10   Jun 22 2005 17:18:30   NMoorhouse
//IR2537 - Handle 'Too Vague' locations
//Resolution for 2537: Mobile - No codes found for 'London' rail locations - location too vague
//
//   Rev 1.9   Apr 11 2005 13:55:48   rscott
//Fix for IR2042 - changes made to ReadCoordValue( )
//
//   Rev 1.8   Apr 07 2005 16:23:52   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.7   Mar 01 2005 17:08:38   passuied
//comments addition after code review
//
//   Rev 1.6   Feb 14 2005 15:39:18   PNorell
//Updated with the latest requirements for IR1905
//Resolution for 1905: Cumbria causes error when issued as a locality
//
//   Rev 1.5   Jan 26 2005 18:07:00   passuied
//tidying up
//
//   Rev 1.4   Jan 25 2005 16:25:56   passuied
//sorry Peter! Put the Serializable attribute back ;)
//
//   Rev 1.3   Jan 25 2005 13:42:22   passuied
//now handles empty xml tags
//
//   Rev 1.1   Jan 19 2005 12:07:16   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.0   Jan 18 2005 17:38:52   passuied
//Initial revision.

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;



namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Handler for Xml that is returned from ESRI Gazetteer.
	/// </summary>
	[Serializable]
	public class XmlGazetteerHandler
	{
		#region Constants
		private const string operationResultName			= "OPERATION_RESULT";
		private const string cacheIdName					= "CACHE_ID";
		private const string addressMatchName				= "ADDRESS_MATCH";
		private const string gazopsNorthingName				= "GAZOPS_NORTHING";
		private const string gazopsEastingName				= "GAZOPS_EASTING";
		private const string nodeNName						= "N";
		private const string nodeVName						= "V";
		private const string addLineName					= "ADDLINE";
		private const string pickListEntryName				= "PICK_LIST_ENTRY";
		private const string pickListName					= "PICK_LIST";
		private const string criteriaFieldName				= "CRITERIA_FIELD";
		private const string identityName					= "IDENTITY";
		private const string descriptionName				= "DESCRIPTION";
		private const string matchRecordsName				= "MATCH_RECORDS";
		private const string scoreName						= "SCORE";
		private const string finalName						= "FINAL";
		private const string noMatchFound					= "No Match Found";
		private const string success						= "success";
		private const string matches						= "matches";
		private const string unsupportedOperation			= "Unsupported Operation";
		private const string gazopsScoreName				= "GAZOPS_SCORE";
		private const string nationalGazetteerIdName		= "NATIONAL_GAZETTEER_ID";
		private const string exchangePointIdName			= "EXCHANGE_POINT_ID";
		private const string exchangePointTypeName			= "EXCHANGE_POINT_TYPE";
		private const string naptanName						= "NAPTAN";
		private const string eastingName					= "EASTING";
		private const string northingName					= "NORTHING";
		private const string postcodeName					= "POSTCODE";
		private const string adminArea						= "ADMINAREA";
		private const string atcoCode						= "ATCO_CODE";
		private const string fields							= "FIELDS";
		private const string maxX							= "GAZOPS_MAXX";
		private const string minX							= "GAZOPS_MINX";
		private const string maxY							= "GAZOPS_MAXY";
		private const string minY							= "GAZOPS_MINY";
		private const string gazopsUniqueId					= "GAZOPS_UNIQUE_ID";
		private const string gazopsVagueName				= "GAZOPS_VAGUE";

		// Extra constants for code Gazetteer
		private const string codeDescriptionName			= "CODEDESCRIPTION";
		private const string naptanCodeName					= "NAPTANCODE";
		private const string codeTypeName					= "CODETYPE";
		private const string modeTypeName					= "MODETYPE";
		private const string localityName					= "LOCALITYNAME";
		private const string regionName						= "REGIONNAME";
		private const string matchValueName					= "MATCHVALUE";

		// Picklist entry Code Type values
		private const string IATAValue						= "IATA";
		private const string CRSValue						= "CRS";
		private const string SMSValue						= "SMS";
		private const string postcodeValue					= "Postcode";
		
		private const string railValue						= "Rail";
		private const string busValue						= "Bus";
		private const string coachValue						= "Coach";
		private const string airValue						= "Air";
		private const string metroValue						= "Metro";
		private const string ferryValue						= "Ferry";





		#endregion

		private bool userLoggedOn;
		private string sessionID;
		private int minScore;

		public XmlGazetteerHandler()
		{
			this.userLoggedOn = false;
			this.sessionID = string.Empty;
			this.minScore = 0;
		}

		public XmlGazetteerHandler(bool userLoggedOn, string sessionID, int minScore)
		{
			this.userLoggedOn = userLoggedOn;
			this.sessionID = sessionID;
			this.minScore = minScore;
		}

		
		#region Public Methods
		/// <summary>
		/// Read Xml Result from DrillDownQuery Webmethod - Final call
		/// </summary>
		/// <param name="xml">Xml string containing the result</param>
		/// <param name="pickListUsed">pickList entry used representing the user choices</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public LocationQueryResult ReadFinalDrillDownResult (string xml, string pickListUsed)
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"Final DrillDownQuery Result = " + xml);
				Logger.Write(log);
			}

			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			LocationQueryResult queryResult = new LocationQueryResult(pickListUsed);

			string operationResult = string.Empty;
			string cacheId = string.Empty;
			
			while (reader.Read())
			{
				// if it's the beginning of a node
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							if (IsText(reader))
								operationResult = reader.Value;
							else if (operationResult != success)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSFinalDrillDownInvalid);
							}
							break;
						}
						case cacheIdName:
							reader.Read();
							if (IsText(reader))
								queryResult.QueryReference = reader.Value;
							break;
						case addressMatchName:
						{
							LocationChoice lc = ReadAddressMatch(reader);
							if( !lc.IsAdminArea )
								queryResult.LocationChoiceList.Add(lc);
							break;
						}
						case pickListEntryName :
						{
							OperationalEvent oe = new OperationalEvent(
								TDEventCategory.Business,
								sessionID,
								TDTraceLevel.Error,
								"Final result must not have a PickList entry");

							Logger.Write(oe);
							throw new TDException(
								"Final Result must not have Pick List Entry",
								true, 
								TDExceptionIdentifier.LSPickListExists);
						}
					}
				}
			}
			return queryResult;
		}


		/// <summary>
		/// Read Xml Result from DrillDownQuery Webmethod
		/// </summary>
		/// <param name="xml">Xml string containing the result</param>
		/// <param name="pickListUsed">pickList entry used representing the user choices</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public LocationQueryResult ReadDrillDownResult (string xml, string pickListUsed)
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"DrillDownQuery Result = " + xml);
				Logger.Write(log);
			}

			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			LocationQueryResult queryResult = new LocationQueryResult(pickListUsed);
		
			string operationResult = string.Empty;
			string cacheId = string.Empty;
			bool noMatchWasFound = false;

			while (reader.Read())
			{

				// if it's the beginning of a node
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							if (IsText(reader))
								operationResult = reader.Value;
							if (operationResult == noMatchFound)
								noMatchWasFound = true;
							else if (operationResult != success)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSDrillDownInvalid);
							}
							break;
						}
						case gazopsVagueName :
						{
							if (noMatchWasFound)
							{
								queryResult.IsVague = true;
								return queryResult;
							}
							break;
						}
						case cacheIdName:
							reader.Read();
							if (IsText(reader))
								queryResult.QueryReference = reader.Value;
							break;
						case addressMatchName:
						{
							LocationChoice lc = ReadAddressMatch(reader);
							if( !lc.IsAdminArea )
								queryResult.LocationChoiceList.Add(lc);
							break;
						}
						case pickListEntryName :
						{
							LocationChoice lc = ReadPickListEntry(reader, false);
							if( !lc.IsAdminArea )
								queryResult.LocationChoiceList.Add(lc);
							break;
						}
					}
				}
			}
		
			if (!noMatchWasFound)
			{
				LocationChoiceList list = queryResult.LocationChoiceList;
				StandardizeLocationChoiceList(ref list);
			}
			return queryResult;
		}
	


		/// <summary>
		/// Read Xml Result from PostcodeMatch Webmethod
		/// </summary>
		/// <param name="xml">Xml string containing the result</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public LocationQueryResult ReadPostcodeMatchResult (string xml)
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"PostcodeMatch Result = " + xml);
				Logger.Write(log);
			}
			
			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			LocationQueryResult queryResult = new LocationQueryResult(string.Empty);
			
			string operationResult = string.Empty;
			int easting=-1;
			int northing=-1;
			string postcode = string.Empty;
			double partPostcodeMaxX = 0;
			double partPostcodeMaxY = 0;
			double partPostcodeMinX = 0;
			double partPostcodeMinY = 0;
			
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							if (IsText(reader))
								operationResult = reader.Value;
							if (operationResult == noMatchFound)
								return queryResult;
							else if (operationResult != success)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSPostCodeMatchInvalid);
							}
							break;
						}
						case postcodeName:
						{
							reader.Read();
							if (IsText(reader))
								postcode = reader.Value;
							break;
						}
						case gazopsEastingName:
						{
							reader.Read();
							if (IsText(reader))
								easting = Convert.ToInt32(TrimCoord(reader.Value), CultureInfo.CurrentCulture.NumberFormat);
							break;

						}
						case gazopsNorthingName:
						{
							reader.Read();
							if (IsText(reader))
								northing = Convert.ToInt32(TrimCoord(reader.Value), CultureInfo.CurrentCulture.NumberFormat);
							break;

						}
							// For Partial Postcode area max and min coords:
						case fields:
						{
							if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
							{
								string strFields = reader.ReadOuterXml();
								partPostcodeMaxX = ReadCoordValue(maxX, strFields);
								partPostcodeMaxY = ReadCoordValue(maxY, strFields);
								partPostcodeMinX = ReadCoordValue(minX, strFields);
								partPostcodeMinY = ReadCoordValue(minY, strFields);
							}
							break;
						}
					}
				}

			}
			OSGridReference gridReference = new OSGridReference(easting,northing);
			LocationChoice choice = new LocationChoice(
				postcode,
				false,
				string.Empty,
				string.Empty,
				gridReference,
				string.Empty,
				0,
				string.Empty,
				string.Empty,
				false,
				partPostcodeMaxX,
				partPostcodeMaxY,
				partPostcodeMinX,
				partPostcodeMinY);

			queryResult.LocationChoiceList.Add(choice);

			return queryResult;
			
		}


		/// <summary>
		/// Read Xml Result from PlacenameMatch Webmethod
		/// </summary>
		/// <param name="xml">Xml string containing the result</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public LocationQueryResult ReadPlacenameMatchResult (string xml)
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"PlacenameMatch Result = " + xml);
				Logger.Write(log);
			}

			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			LocationQueryResult queryResult = new LocationQueryResult(string.Empty);
			
			string operationResult = string.Empty;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							if (IsText(reader))
								operationResult = reader.Value;
							if (operationResult == noMatchFound)
								return queryResult;
							else if (operationResult != matches)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSPlaceNameMatchInvalid);
							}
							break;
						}
						
						case pickListEntryName:
						{
							queryResult.LocationChoiceList.Add(ReadPickListEntry(reader, true));
							break;
						}
					}
				}

			}
			LocationChoiceList list = queryResult.LocationChoiceList;
			StandardizeLocationChoiceList(ref list);
			return queryResult;
		}


		/// <summary>
		/// Read Xml Result from FetchRecord Webmethod
		/// </summary>
		/// <param name="xml">Xml string containing the result</param>
		/// <returns>Returns an array of TDNaptan objects</returns>
		public TDNaptan[] ReadFetchRecord(string xml)
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"FetchRecord Result = " + xml);
				Logger.Write(log);
			}

			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);
			
			ArrayList naptans = new ArrayList();
			string operationResult = string.Empty;

			bool isValid = false;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							
							if (IsText(reader))
							{
								operationResult = reader.Value;
							}
							else if (operationResult != unsupportedOperation)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSFetchRecordInvalid);
							}
							break;
						}

						case matchRecordsName :
						{
							isValid = true;
							break;

						}

						case naptanName :
						{
							reader.Read();
							
							if (IsText(reader))
							{
								operationResult = reader.Value;
							}
						
							TDNaptan naptan = new TDNaptan();
							naptan.Naptan = reader.Value;
							naptans.Add(naptan);
							break;
						}

						case northingName :
						{
							reader.Read();
							
							if (IsText(reader))
							{
								operationResult = reader.Value;
							}

							TDNaptan naptan = (TDNaptan)naptans[naptans.Count - 1];
							
							if	(naptan.GridReference == null) 
							{
								naptan.GridReference = new OSGridReference();
							}

							naptan.GridReference.Northing = Int32.Parse(reader.Value);
							break;
						}

						case eastingName :
						{
							reader.Read();
							
							if (IsText(reader))
							{
								operationResult = reader.Value;
							}

							TDNaptan naptan = (TDNaptan)naptans[naptans.Count - 1];

							if	(naptan.GridReference == null) 
							{
								naptan.GridReference = new OSGridReference();
							}
							
							naptan.GridReference.Easting = Int32.Parse(reader.Value);
							break;
						}

					}
				}
			}

			if (!isValid)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"Bad format Xml : Missing <MATCH_RECORDS> tag");

				Logger.Write(oe);
				throw new TDException(
					"Bad format Xml : Missing <MATCH_RECORDS> tag",
					false, 
					TDExceptionIdentifier.LSFetchRecordNoMatch);

			}
			return (TDNaptan[])naptans.ToArray(typeof(TDNaptan));

		}

		

		
		/// <summary>
		/// Method that builds a TDCodeDetail[] from the xml string 
		/// returned by ESRI
		/// </summary>
		/// <param name="xml">string containing xml</param>
		/// <param name="modeTypes">Mode of transport array to select</param>
		/// <returns></returns>
		public TDCodeDetail[] ReadCodeGazResult (string xml, TDModeType[] modeTypes)
		{
			bool noMatchWasFound = false;

			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"CodeGaz Result = " + xml);
				Logger.Write(log);
			}

			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			ArrayList listDetail = new ArrayList();


			string operationResult = string.Empty;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case operationResultName :
						{
							reader.Read();
							if (IsText(reader))
								operationResult = reader.Value;
							if (operationResult == noMatchFound)
							{
								noMatchWasFound = true;
							}
							else if (operationResult != matches)
							{
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Error,
									"Operation result not valid");

								Logger.Write(oe);
								throw new TDException(
									"Operation result not valid",
									true, 
									TDExceptionIdentifier.LSPlaceNameMatchInvalid);
							}
							break;
						}
						
						case pickListEntryName:
						{
							TDCodeDetail detail = ReadCodePickListEntry(reader);
							ArrayList listModes = new ArrayList(modeTypes);

							// only add the detail to list if modeType is in requested list!
							if (listModes.Contains(detail.ModeType))
								listDetail.Add(detail);
							break;
						}

						case gazopsVagueName:
						{
							if (noMatchWasFound)
							{
								OperationalEvent logoe = new OperationalEvent(
									TDEventCategory.Business,
									sessionID,
									TDTraceLevel.Verbose,
									"Place Name too vague");

								Logger.Write(logoe);
								throw new TooVagueException(
									"Place Name too vague",
									true, 
									TDExceptionIdentifier.LSPlaceNameMatchTooVague);
							}
							break;
						}
					}
				}
			}
			if (noMatchWasFound)
			{
				return new TDCodeDetail[0];
			}

			return (TDCodeDetail[])listDetail.ToArray(typeof(TDCodeDetail));

		}

		#endregion

		#region private methods

		/// <summary>
		/// Method that reads infor from a Picklist entry Xml node
		/// and creates either a LocationChoice or a TDCodeDetail object,
		/// depending on ginven GazetteerType
		/// </summary>
		/// <param name="type">Defines which type of object to get</param>
		/// <param name="reader">XmlTextReader object containing the next picklist entry to read</param>
		/// <param name="final">used to build a LocationChoice. Indicates if entry is final</param>
		/// <param name="target">reference of the object to build. </param>
		/// <returns>true if object built successfully, false otherwise</returns>
		private bool BuildGazObjectFromPickListEntry ( Type  type, XmlTextReader reader, bool final, ref object target)
		{
			string identity = string.Empty;
			string criteria = string.Empty;
			string description = string.Empty;
			string exchangePointType = string.Empty;
			string exchangePointId = string.Empty;
			string nationalGazetteerId = string.Empty;
			int easting = -1;
			int northing = -1;
			double score = 0;

			// special fields for code gaz
			string code = string.Empty;
			string codeType = string.Empty;
			string locality = string.Empty;
			string region = string.Empty;
			string modeType = string.Empty;
			string naptanCode = string.Empty;


			while (reader.Read() && !IsEndElement(reader, pickListEntryName))
			{
				if (!IsEndElement(reader, reader.Name))
				{
				
					switch (reader.Name)
					{
						case criteriaFieldName:
						{
							reader.Read();
							if (IsText(reader))
								criteria = reader.Value;
							break;
						}
						case finalName:
						{
							reader.Read();
							if (IsText(reader))
								final = Convert.ToBoolean(reader.Value);
							break;
						}
						case identityName:
						{
							reader.Read();
							if (IsText(reader))
							{
								identity = reader.Value;
							}
							break;
						}
						case scoreName:
						{
							reader.Read();
							if (IsText(reader))
								score = Convert.ToDouble(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
							break;

						}
						case descriptionName:
						{
							reader.Read();
							if (IsText(reader))
								description = reader.Value;
							break;

						}
						case gazopsEastingName:
						{
							reader.Read();
							if (IsText(reader))
								easting = Convert.ToInt32(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
							break;

						}
						case gazopsNorthingName:
						{
							reader.Read();
							if (IsText(reader))
								northing = Convert.ToInt32(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
							break;

						}
						case nationalGazetteerIdName:
						{
							reader.Read();
							if (IsText(reader))
								nationalGazetteerId = reader.Value;
							break;

						}
						case exchangePointIdName:
						case naptanName:
						case atcoCode:
						{
							reader.Read();
							if (IsText(reader))
								exchangePointId = reader.Value;
							break;

						}
						case exchangePointTypeName:
						{
							reader.Read();
							if (IsText(reader))
								exchangePointType = reader.Value;
							break;

						}

							// added for code gaz
						case codeTypeName:
						{
							reader.Read();
							if (IsText(reader))
								codeType = reader.Value;
							break;

						}
						case naptanCodeName:
						{
							reader.Read();
							if (IsText(reader))
								naptanCode = reader.Value;
							break;

						}
						case modeTypeName:
						{
							reader.Read();
							if (IsText(reader))
								modeType = reader.Value;
							break;

						}
						case localityName:
						{
							reader.Read();
							if (IsText(reader))
								locality = reader.Value;
							break;

						}
						case regionName:
						{
							reader.Read();
							if (IsText(reader))
								region = reader.Value;
							break;

						}
						case matchValueName:
						{
							reader.Read();
							if (IsText(reader))
								code = reader.Value;
							break;

						}


					}

				}
			}
			
			if ( type == typeof(LocationChoice))
			{
				// Create new LocationChoice
				LocationChoice choice = new LocationChoice(
					description,
					!final,
					criteria,
					identity,
					new OSGridReference(easting, northing),
					exchangePointId,
					score,
					nationalGazetteerId,
					exchangePointType,
					(criteria == adminArea));

				target = choice;
				return true;
			}
			if (type == typeof(TDCodeDetail))
			{
				TDCodeDetail detail = new TDCodeDetail();
				detail.Description = description;
				detail.Code = code;
				detail.CodeType = GetTDCodeType(codeType);
				detail.Description = description;
				detail.Easting = easting;
				detail.Northing = northing;
				detail.Locality = locality;
				detail.Region = region;
				detail.ModeType = GetTDModeType(modeType);
				detail.NaptanId = naptanCode;

				target = detail;
				return true;
			}
			return false;
			
		}

		/// <summary>
		/// Method used to build a Location Choice need in TDGazetteer classes.
		/// </summary>
		/// <param name="reader">XmlTextReader reader containing the xml Picklist entryto analyse</param>
		/// <param name="final">specifies if the pickList entry is final</param>
		/// <returns></returns>
		private LocationChoice ReadPickListEntry(XmlTextReader reader, bool final)
		{
			object choice = null;

			BuildGazObjectFromPickListEntry(typeof(LocationChoice), reader, final, ref choice);

            if (TDTraceSwitch.TraceVerbose)
            {
                try
                {
                    LocationChoice lc = (LocationChoice)choice;

                    OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        string.Format("ReadPickListEntry Location Desc[{0}] Value[{1}] Naptan[{2}] Locality[{3}]", lc.Description, lc.PicklistValue, lc.Naptan, lc.Locality));
                    Logger.Write(log);
                }
                catch
                {
                    // Ignore exception, just verbose logging 
                    OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "ReadPickListEntry Location");
                    Logger.Write(log);
                }
            }

			return (LocationChoice)choice;

		}

		/// <summary>
		/// Method used to build a TDCodeDetail object, needed by TDCodeGazetteer class.
		/// </summary>
		/// <param name="reader">Reader containing the xml Picklist entry to analyse</param>
		/// <returns>new TDCodeDetail object</returns>
		private TDCodeDetail ReadCodePickListEntry(XmlTextReader reader)
		{
			object  detail = null;

			BuildGazObjectFromPickListEntry(typeof(TDCodeDetail), reader, false, ref detail);

            if (TDTraceSwitch.TraceVerbose)
            {
                try
                {
                    TDCodeDetail d = (TDCodeDetail)detail;

                    OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        string.Format("ReadPickListEntry Code Desc[{0}] Code[{1}] Naptan[{2}] Locality[{3}]", d.Description, d.Code, d.NaptanId, d.Locality));
                    Logger.Write(log);
                }
                catch
                {
                    // Ignore exception, just verbose logging 
                    OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "ReadPickListEntry Code");
                    Logger.Write(log);
                }
            }

			return (TDCodeDetail)detail;
		}

		/// <summary>
		/// Method that builds a new LocationChoice object from an XmlTextReader containing xml AddressMatch.
		/// </summary>
		/// <param name="reader">Reader containing the xml address match information</param>
		/// <returns>new LocationChoice object</returns>
		private LocationChoice ReadAddressMatch (XmlTextReader reader)
		{
			string identity = string.Empty;
			string criteria = string.Empty;
			string exchangePointType = string.Empty;
			string exchangePointId = string.Empty;
			string exchangePointUniqueId = string.Empty;
			string nationalGazetteerId = string.Empty;
			double score = 0;

			string address = string.Empty;
			int northingValue = -1;
			int eastingValue = -1;

			if( TDTraceSwitch.TraceVerbose )
			{
				OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"ReadAddressMatch");
				Logger.Write(log);
			}

			while (reader.Read() && !IsEndElement(reader, addressMatchName))
			{
				if (!IsEndElement(reader, reader.Name))
				{
				
					switch (reader.Name)
					{
						case addLineName :
						{
							reader.Read();
							if (IsText(reader))
							{
								address += reader.Value +",";
								reader.Read();
							}
							break;
						}
						
					}
					switch (reader.Value)
					{
						case gazopsEastingName :
						{
							eastingValue = Convert.ToInt32(ReadValue(reader),
								CultureInfo.CurrentCulture.NumberFormat);
							break;
						}
						case gazopsNorthingName :
						{
							northingValue = Convert.ToInt32(ReadValue(reader),
								CultureInfo.CurrentCulture.NumberFormat);
							break;
						}
						case nationalGazetteerIdName :
						{
							nationalGazetteerId = ReadValue(reader);
							break;
						}
							// Added for ESRI 5.2 change in Mainstation Gazetteer
						case exchangePointIdName:
						case naptanName:
						case atcoCode:
						{
							exchangePointId = ReadValue(reader);
							break;
						}

						case gazopsUniqueId:
						{
							exchangePointUniqueId = ReadValue(reader);
							break;
						}

						case exchangePointTypeName:
						{
							exchangePointType = ReadValue(reader);
							break;
						}
	
					}
				}
			}
			
			if( address[ address.Length - 1] == ',' )
			{
				address = address.Substring(0,address.Length-1); // remove the coma at the end of the string
			}			

			OSGridReference gridReference = new OSGridReference(eastingValue, northingValue);

			// FetchRecord returns a UniqueId that we want to use in preference to exchangepointId
			if	(exchangePointUniqueId != null && exchangePointUniqueId.Length > 0)
			{
				exchangePointId = exchangePointUniqueId;
			}

			// Create new LocationChoice
			LocationChoice choice = new LocationChoice(
				address,
				false,
				criteria,
				identity,
				gridReference,
				exchangePointId,
				score,
				nationalGazetteerId,
				exchangePointType,
				(criteria == adminArea));
			return choice;
		}

		/// <summary>
		/// Trims any decimal places from returned coordinates to 
		/// ensure compatability with OSGridReference.
		/// </summary>
		private string TrimCoord(string coord)
		{
			while (coord.IndexOf(".")> 0)
			{
				coord = coord.Remove(coord.Length-1,1);
			}
			return coord;
		}

		/// <summary>
		/// Extracts max and min x and y values from xml string to 
		/// determine the best fit scale to use when displaying the map.
		/// </summary>
		private double ReadCoordValue(string strCoord, string xml)
		{
			bool setValue = false;
			double coord = 0;
			StringReader sr = new StringReader(xml);	
			XmlTextReader reader = new XmlTextReader(sr);

			while (reader.Read())
			{
				if ( reader.NodeType == XmlNodeType.Element && reader.Name == nodeNName )
				{
					// Next element (contains the actual value)
					reader.Read();
					string data = reader.Value;
					if ( data == strCoord)
					{
						setValue = true;
					}
				}
				else if (reader.NodeType == XmlNodeType.Element && reader.Name == nodeVName )
				{
					if (setValue)
					{
						// Next element (contains the actual value)
						reader.Read();
						setValue = false;
						coord =  Convert.ToDouble(reader.Value);
						break;
					}
				}
			}
			return coord;
		}

		/// <summary>
		/// Method that sorts in ascending order a given choice list.
		/// </summary>
		/// <param name="choiceList">given choice list as reference</param>
		private void StandardizeLocationChoiceList(ref LocationChoiceList choiceList)
		{
			// Don't remove any entries if there is only one ...

			if	(choiceList.Count < 2)
			{
				return;
			}
			
			for (int i=0; i < choiceList.Count; i++)
			{
				LocationChoice choice = (LocationChoice)choiceList[i];
				if (choice.Score < minScore)
				{
					choiceList.RemoveRange(i, choiceList.Count-i);
					break;
				}
			}
			
		}

		/// <summary>
		/// Method that converts a string into the associated enumeration TDCodeType
		/// </summary>
		/// <param name="type">string value to identify</param>
		/// <returns>TDCodeType value of given string.</returns>
		private TDCodeType GetTDCodeType( string type)
		{
			switch (type)
			{
				case CRSValue:
					return TDCodeType.CRS;
				case SMSValue:
					return TDCodeType.SMS;
				case IATAValue:
					return TDCodeType.IATA;
				case postcodeValue:
					return TDCodeType.Postcode;
				default :
				{
					Logger.Write(new OperationalEvent(
						TDEventCategory.ThirdParty,
						TDTraceLevel.Error,
						"Found Undefined CodeType in ESRI gazetteer")
						);


					throw new TDException(
						"Found Undefined CodeType in ESRI gazetteer",
						true,
						TDExceptionIdentifier.LSCodeGazetteerUndefinedCodeType);

				}
			}
		}

		/// <summary>
		/// Method that converts a string into the associated enumeration TDModeType
		/// </summary>
		/// <param name="type">string value to identify</param>
		/// <returns>TDModeType value of given string.</returns>
		private TDModeType GetTDModeType( string type)
		{
			switch (type)
			{
				case railValue:
					return TDModeType.Rail;
				case busValue:
					return TDModeType.Bus;
				case coachValue:
					return TDModeType.Coach;
				case airValue:
					return TDModeType.Air;
				case metroValue:
					return TDModeType.Metro;
				case ferryValue:
					return TDModeType.Ferry;
				default:
					return TDModeType.Undefined;
			}
		}


		#endregion

		#region Xml Helper
		private bool IsEndElement (XmlTextReader reader, string name)
		{
			if (reader.Name == name 
				&& (reader.NodeType == XmlNodeType.EndElement
					|| reader.IsEmptyElement) ) // detects if the current element is empty
												// equals to an end element!
				return true;
			else
				return false;
		}

		private bool IsText (XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.Text)
				return true;
			else
				return false;
		}

		private string ReadValue(XmlTextReader reader)
		{
			// find the next <V> node
			while ( 
				!(reader.Name == nodeVName && reader.NodeType == XmlNodeType.Element)
				)
			{
				reader.Read();
			}

			reader.Read();
			if (IsText(reader))
				return reader.Value;
			else
				return string.Empty;
		}
		#endregion

	}
}
