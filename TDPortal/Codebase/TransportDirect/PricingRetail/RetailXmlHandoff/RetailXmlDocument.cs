// *********************************************** 
// NAME                 : RetailXmlDocument.cs 
// AUTHOR               : Andrew Toner
// DATE CREATED         : 20/10/2003 
// DESCRIPTION			: Responsible for generating the XML document describing journey and
//                        pricing parameters that are posted to a retailer website
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RetailXmlHandoff/RetailXmlDocument.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:14   mturner
//Initial revision.
//
//   Rev 1.21   Nov 17 2005 12:36:16   mguney
//OpenReturn situation handled in GenerateXml method.
//Resolution for 3091: DN040 - Find a Fare - server error buying ticket for return coach journey
//
//   Rev 1.20   Aug 19 2005 14:06:14   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.19.1.0   Aug 16 2005 11:20:54   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.19   Apr 09 2005 16:35:20   jgeorge
//Bug fix to ensure that code only attempts to add inward journey when inward journey legs are present
//
//   Rev 1.18   Mar 31 2005 16:29:04   jgeorge
//Bug fix
//
//   Rev 1.17   Mar 30 2005 15:38:40   jgeorge
//Updates to handoff, as well as FxCop changes
//
//   Rev 1.16   Mar 17 2005 17:43:58   jgeorge
//Correction to commenting
//
//   Rev 1.15   Mar 08 2005 17:13:56   jgeorge
//Changed to remove use of Itinerary object
//
//   Rev 1.14   May 18 2004 11:47:46   COwczarek
//In journey legs, replace mode of rail replacement bus with mode of rail
//Resolution for 892: Retailer handoff should replace rail replacment legs with rail legs (DEL 6.0)
//
//   Rev 1.13   Apr 02 2004 12:09:16   ESevern
//Removed users email from xml generation
//
//   Rev 1.12   Nov 28 2003 15:50:30   COwczarek
//Use cached XML schema (from service discovery)
//Resolution for 451: Retail Handoff does not need to read XML schema for each request
//
//   Rev 1.11   Nov 21 2003 17:00:54   acaunt
//Journey leg service details are only added to the XML if a service is present. Retested for a walking leg.
//
//   Rev 1.10   Nov 18 2003 10:12:10   acaunt
//CRS and NLC codes added to location nodes
//Resolution for 234: Retailer XML should include NLC and CRS codes
//
//   Rev 1.9   Nov 17 2003 16:08:30   acaunt
//XML Declaration added
//Resolution for 240: XML Declaration not present in Retailer XML
//
//   Rev 1.8   Nov 14 2003 17:23:24   COwczarek
//SCR#216 Retailer XML validation failure if discounts selected
//
//   Rev 1.7   Nov 12 2003 11:06:10   COwczarek
//FxCop fixes applied. Comments added. Exception handling fixed. Fixes to comply with XML schema.
//
//   Rev 1.6   Nov 07 2003 17:30:52   COwczarek
//FxCop fixes
//
//   Rev 1.5   Nov 04 2003 12:07:50   COwczarek
//Interchange speed now passed in as int and converted to appropriate string for output
//
//   Rev 1.4   Oct 31 2003 12:34:50   COwczarek
//Work in progress
//
//   Rev 1.3   Oct 30 2003 18:48:40   acaunt
//Update Via Point
//
//   Rev 1.2   Oct 30 2003 17:27:40   acaunt
//Document now correctly structured and all required fields are included
//
//   Rev 1.1   Oct 30 2003 11:55:48   COwczarek
//Fix to time representation
//
//   Rev 1.0   Oct 23 2003 16:35:04   AToner
//Initial Revision
//
//   Rev 1.1   Oct 23 2003 16:03:42   AToner
//Changes from testing
//
//   Rev 1.0   Oct 23 2003 13:45:02   AToner
//Initial Revision
//
//   Rev 1.3   Oct 23 2003 12:09:06   AToner
//Created GenerateXml function
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Data;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.AdditionalDataModule;
using CJP =  TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff
{	
    /// <summary>
    /// Responsible for generating the XML document describing journey and
    //  pricing parameters that are posted to a retailer website
    /// </summary>
	public sealed class RetailXmlDocument
	{
		// const values for the JourneyWeb2.1 Origin and Destination NaPTAN values;
		private const string ORIGIN_NAPTAN = "Origin";
		private const string DESTINATION_NAPTAN = "Destination";

		// Set up the reference to the additionalDataModule for the CRS and NLC lookups
		private static IAdditionalData additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

        /// <summary>
        /// Constructor.
        /// </summary>
		private RetailXmlDocument( )
		{
		}

        #region Public methods
        
        /// <summary>
        /// Generates the XML document using the supplied parameters
        /// </summary>
        /// <param name="outwardJourney">The outward journey from the original itinerary</param>
        /// <param name="inwardJourney">The inward journey from the original itinerary, or null if none</param>
        /// <param name="itineraryType">The type of the original itinerary</param>
        /// <param name="overrideItineraryType">The itinerary type overridden by the user</param>
        /// <param name="isReturn">True if user selected return retailer, false if outward retailer</param>
        /// <param name="interchangeSpeed">0=average, 2=slow, 3=fast</param>
        /// <param name="noChanges">true if no changes, false otherwise</param>
        /// <param name="discounts">Discounts selected from fares page</param>
        /// <param name="requestedVia">A via location</param>
        /// <param name="adultPassengers">The number of adults travelling</param>
        /// <param name="childPassengers">The number of children travelling</param>
        /// <returns>The XML document</returns>
		public static IXPathNavigable GenerateXml( PublicJourneyDetail[] outwardJourneyLegs, PublicJourneyDetail[] inwardJourneyLegs, 
			ItineraryType itineraryType, ItineraryType overrideItineraryType, bool isReturn,
			int interchangeSpeed, bool noChanges, Discounts discounts, TDLocation requestedVia, 
			int adultPassengers, int childPassengers)
		{
			IPropertyProvider pp = Properties.Current;

			string version = pp["PricingRetail.RetailXmlDocument.Version"];

			XmlDataDocument result = new XmlDataDocument( );

			XmlElement rootElement = result.CreateElement("Root");
			rootElement.SetAttribute( "Version", version );

			// Add XML declaration
			
			XmlDeclaration xmldecl;
			xmldecl = result.CreateXmlDeclaration("1.0","UTF-8","");
			XmlElement doc = result.DocumentElement;
			result.InsertBefore(xmldecl, doc);
			
			// Create and populate user preferences
			rootElement.AppendChild(AddUserPreferences(result, interchangeSpeed, noChanges));
			
			// If the user has specified a return and has asked for a return, generate both the outbound and return legs with details			
			// If ItineraryType is Return and inwardJourneyLegs.Length = 0 then this is a OpenReturn ticket.
			if ( (itineraryType == ItineraryType.Return) && (overrideItineraryType == ItineraryType.Return) && (inwardJourneyLegs != null) 
				&& (inwardJourneyLegs.Length > 0)) 
			{
				rootElement.AppendChild( AddXmlJourney( result, "Outward_Journey", outwardJourneyLegs, requestedVia ) );

				rootElement.AppendChild( AddXmlJourney( result, "Return_Journey", inwardJourneyLegs, requestedVia ) );
			}
			// Otherwise specify for the outward journey the leg they have selected
			else 
			{
				rootElement.AppendChild( AddXmlJourney( result, "Outward_Journey", outwardJourneyLegs, requestedVia ) );

				// And add a Return_Option element with the appropriate flag
				rootElement.AppendChild(AddReturnJourneyFlag( result, overrideItineraryType == ItineraryType.Return));
			}

			// Create and populate the Passenger Details
			rootElement.AppendChild(AddPassengerDetail(result, discounts, adultPassengers, childPassengers));
			result.AppendChild( rootElement );
			
			// Validate newly created document against schema
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(RetailXmlSchema.Current.Schema);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
            
			XmlReader vr = XmlReader.Create(new StringReader(result.OuterXml),settings);
            		
			while(vr.Read());
			vr.Close();
			
			return (IXPathNavigable)result;
		}
		
		#endregion Public methods
		
		#region Private methods
		
        /// <summary>
        /// Called by the validation handler if an error occurs during validation.
        /// An operational event is logged and a TDException is thrown.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="args">Event arguments</param>
		private static void ValidationHandler(object sender, ValidationEventArgs args)
		{
		    string message = "Error occurred validating handoff XML: "+args.Message +". Severity=" + 
		        args.Severity;
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
				    message,args.Exception));
            throw new TDException(message,args.Exception,true,
                    TDExceptionIdentifier.PRHErrorValidatingHandoffXml);				
		}

        /// <summary>
        /// Creates an outward or return journey element
        /// </summary>
        /// <param name="doc">The enclosing document</param>
        /// <param name="JourneyName">The name of the enclosing outward or return journey element</param>
        /// <param name="publicJourney">The journey details</param>
        /// <param name="viaLocation">Details for the requested vias element</param>
        /// <returns>The created outward or return journey element</returns>
		private static XmlElement AddXmlJourney( XmlDocument doc, string JourneyName, PublicJourneyDetail[] publicJourneyLegs, TDLocation viaLocation)
		{
			XmlElement journey;
			XmlElement journeyLegs, legStart, legEnd, mode;

			journey = doc.CreateElement( JourneyName );

			if( publicJourneyLegs != null && publicJourneyLegs.Length != 0) 
			{
				foreach( PublicJourneyDetail detail in publicJourneyLegs )
				{
					// Create the JourneyLegs element
					journeyLegs = doc.CreateElement( "Journey_Legs" );
					// Create the Leg Start Element
					legStart = AddLegNode(doc, "Leg_Start", detail.LegStart.Location);
					// Create the Leg End Element
					legEnd = AddLegNode(doc, "Leg_End", detail.LegEnd.Location);

					// Create the Mode element
                    // Retailers treat rail replacement bus legs as rail legs so
                    // change rail replacement bus legs to rail
					mode = doc.CreateElement( "Mode" );
                    if (detail.Mode == CJP.ModeType.RailReplacementBus) 
                    {
                        mode.SetAttribute( "Mode", CJP.ModeType.Rail.ToString().ToLower(CultureInfo.InvariantCulture));
                    } 
                    else 
                    {
                        mode.SetAttribute( "Mode", detail.Mode.ToString().ToLower(CultureInfo.InvariantCulture));
                    }

					// Add all the created elements to the journey legs element.
					journeyLegs.SetAttribute( "Date", detail.LegStart.DepartureDateTime.GetDateTime().ToString("u",CultureInfo.InvariantCulture).Substring( 0, 10 ) );
					journeyLegs.SetAttribute( "Time", detail.LegStart.DepartureDateTime.GetDateTime().ToLongTimeString() );
					
					// If we have any service details add the information
					if (detail.Services != null && detail.Services.Length != 0) 
					{
						journeyLegs.SetAttribute( "OperatorCode", detail.Services[0].OperatorCode );
						if (detail.Services[0].ServiceNumber != null && detail.Services[0].ServiceNumber.Length != 0)
						{
							journeyLegs.SetAttribute( "ServiceNumber", detail.Services[0].ServiceNumber );
						}
						if (detail.Mode == CJP.ModeType.Coach && (detail.Services[0].ServiceNumber != null && detail.Services[0].ServiceNumber.Length != 0))
						{
							journeyLegs.SetAttribute( "Direction", detail.Services[0].Direction );
						}
					}
					journeyLegs.AppendChild( legStart );
					journeyLegs.AppendChild( legEnd );
					journeyLegs.AppendChild( mode );

					// Create the requested vias element if applicable
					if( detail.IncludesVia && viaLocation != null)
						journeyLegs.AppendChild(AddRequestedVia(doc, detail, viaLocation));

					// Append the journey leg to the journey.
					journey.AppendChild( journeyLegs );
				}
			}

			return journey;
		}

        /// <summary>
        /// Creates a return journey element and nested return journey flag element
        /// </summary>
        /// <param name="doc">The enclosing document</param>
        /// <param name="openReturn">The value of the return journey flag element</param>
        /// <returns>The created return journey element</returns>
		private static XmlElement AddReturnJourneyFlag(XmlDocument doc, bool openReturn)
		{
			XmlElement result = doc.CreateElement("Return_Journey");
			XmlElement returnOption = doc.CreateElement("Return_Option");
			returnOption.SetAttribute("Return_Flag", openReturn.ToString().ToLower(CultureInfo.InvariantCulture));
			result.AppendChild(returnOption);
			return result;
		}

		/// <summary>
		/// Creates a user preferences element and sets the attributes to the supplied values
		/// </summary>
		/// <param name="doc">The enclosing document</param>
		/// <param name="interchangeSpeed">0=average, 2=slow, 3=fast</param>
		/// <param name="noChanges">true if no changes, false otherwise</param>
		/// <returns>The created user preferences element</returns>
		private static XmlElement AddUserPreferences(XmlDocument doc, int interchangeSpeed, bool noChanges)
		{
            string interchangeSpeedText;
            switch (interchangeSpeed) 
            {
                case 0:
                    interchangeSpeedText = "Average";
                    break;
                case 2:
                    interchangeSpeedText = "Slow";
                    break;
                case 3:
                    interchangeSpeedText = "Fast";
                    break;
                default:
                    interchangeSpeedText = string.Empty;
                    break;

            };
            XmlElement userPreferences = doc.CreateElement( "User_Preferences" );
			userPreferences.SetAttribute( "Interchange_Speed", interchangeSpeedText );
			userPreferences.SetAttribute( "No_Changes", noChanges ? "Y" : "N" );	
			return userPreferences;
		}

        /// <summary>
        /// Creates a passenger details element and sets the attributes to the supplied values
        /// </summary>
        /// <param name="doc">The enclosing document</param>
        /// <param name="discounts">Discounts selected from fares page</param>
        /// <param name="adultPassengers">Number of adults travelling</param>
        /// <param name="childPassengers">Number of children travelling</param>
        /// <returns>The created passenger details element</returns>
		private static XmlElement AddPassengerDetail(XmlDocument doc, Discounts discounts, int adultPassengers, int childPassengers)
		{
			XmlElement passengerDetail = doc.CreateElement( "Passenger_Detail" );
            if (discounts != null) 
            {
                if (discounts.RailDiscount != null && discounts.RailDiscount.Length != 0)
                {
                    XmlElement railDiscount = doc.CreateElement("Discount");
                    railDiscount.SetAttribute("Discount_Card_Type", discounts.RailDiscount);
                    railDiscount.SetAttribute("Number_of_Discounts", "1");
                    passengerDetail.AppendChild(railDiscount);
                }
                if (discounts.CoachDiscount != null && discounts.CoachDiscount.Length != 0)
                {
                    XmlElement coachDiscount = doc.CreateElement("Discount");
                    coachDiscount.SetAttribute("Discount_Card_Type", discounts.CoachDiscount);
                    coachDiscount.SetAttribute("Number_of_Discounts", "1");
                    passengerDetail.AppendChild(coachDiscount);
                }
            }
			passengerDetail.SetAttribute( "Number_of_Adults", adultPassengers.ToString( CultureInfo.InvariantCulture ) );
			passengerDetail.SetAttribute( "Number_of_Children", childPassengers.ToString( CultureInfo.InvariantCulture ) );	
			return passengerDetail;
		}

		/// <summary>
		/// Creates the element and populates the attributes for a Leg_Start or Leg_End node
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="nodeName"></param>
		/// <param name="location"></param>
		/// <returns></returns>
		private static XmlElement AddLegNode(XmlDocument doc, string nodeName, TDLocation location)
		{
			XmlElement legNode = doc.CreateElement(nodeName);
			string naptan = MapNaPTAN(location.NaPTANs[0].Naptan);
			legNode.SetAttribute( "NaPTAN", naptan );
			// Add CRS and NLC if appropriate
			if (naptan.Length != 0) 
			{
				legNode.SetAttribute( "CRS", additionalData.LookupFromNaPTAN(LookupType.CRS_Code, naptan));
				legNode.SetAttribute( "NLC",  additionalData.LookupFromNaPTAN(LookupType.NLC_Code, naptan));
			}
			return legNode;
		}

        /// <summary>
        /// Creates a Requested Vias element and sets the attributes using the supplied values
        /// </summary>
        /// <param name="doc">The enclosing document</param>
        /// <param name="detail">Journey details with which the via location is associated</param>
        /// <param name="viaLocation">The via location</param>
        /// <returns>The created requested vias element</returns>
		private static XmlElement AddRequestedVia(XmlDocument doc, PublicJourneyDetail detail, TDLocation viaLocation)
		{
			XmlElement requestedVia = doc.CreateElement("Requested_Vias");
			string naptan = MapNaPTAN(viaLocation.NaPTANs[0].Naptan);
			requestedVia.SetAttribute("NaPTAN", naptan);
			// Add CRS and NLC if appropriate
			if (naptan.Length != 0) 
			{
				requestedVia.SetAttribute( "CRS", additionalData.LookupFromNaPTAN(LookupType.CRS_Code, naptan));
				requestedVia.SetAttribute( "NLC",  additionalData.LookupFromNaPTAN(LookupType.NLC_Code, naptan));
			}

			// The via is an interchange location if it coincides with either the boarding or alighting location of the journey detail
			// otherwise it is a calling at location
			TDLocation boardLocation = detail.LegStart.Location;
			TDLocation alightLocation = detail.LegEnd.Location;
			bool isInterchange = viaLocation.IsMatchingNaPTANGroup(boardLocation) || viaLocation.IsMatchingNaPTANGroup(alightLocation);
			if (isInterchange) 
			{
				requestedVia.SetAttribute("Type","I");
			} 
			else 
			{
				requestedVia.SetAttribute("Type","C");
			}
			return requestedVia;
		}
		
		/// <summary>
		/// Converts all non standard naptans to a common value
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		private static string MapNaPTAN(string naptan)
		{
			if (naptan == null || naptan.Length == 0 || naptan.Equals(ORIGIN_NAPTAN) || naptan.Equals(DESTINATION_NAPTAN))
			{
				return string.Empty;
			}
			else
			{
				return naptan;
			}
		}
		#endregion Private methods
		
	}


}





















