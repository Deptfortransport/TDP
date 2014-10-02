// *********************************************** 
// NAME                 : TicketParser.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Converts Xml into Tickets
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;

namespace TransportDirect.Common.RailTicketType
{
    /// <summary>
    /// Ticket parser used for converting XML feed to ticket objects
    /// </summary>
    public class TicketParser
    {

        private static TicketParser instance = null;

        /// <summary>
        /// Constructor for ticket parser
        /// </summary>
        private TicketParser()
        {
        }

        /// <summary>
        /// Method for returning static ticket parser instance
        /// </summary>
        public static TicketParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TicketParser();
                }

                return instance;
            }
        }

        
        /// <summary>
        /// Converts an XML string to a Tickets class
        /// </summary>
        /// <param name="TicketsXML">Xml Document containing Ticket type information</param>
        /// <returns>Ticket business object containing ticket types</returns>
        public Tickets ConvertXMLToTickets(XmlDocument TicketsXML)
        {

            XmlNamespaceManager ns = new XmlNamespaceManager(TicketsXML.NameTable);
            ns.AddNamespace("def", "http://nationalrail.co.uk/xml/ticket");

            XmlNodeList nodelist = TicketsXML.SelectNodes("/def:TicketTypeDescriptionList/*", ns);

            Tickets rtnTickets = new Tickets();
            string ticketTypeCode = string.Empty;
            string ticketTypeName = string.Empty;
            string description = string.Empty;
            ApplicableTOCs applicableTocs = null;
            string tocAndConnections = string.Empty;
            ValidityCodes validityCodes = null;
            List<string> valcodes = new List<string>();
            string fareCategory = string.Empty;
            bool groupSave = false;
            string discounts = string.Empty;
            string availability = string.Empty;
            string retailing = string.Empty;
            string bookingDeadlines = string.Empty;
            string changesToTravelPlans = string.Empty;
            string refunds = string.Empty;
            string breaksOfJourney = string.Empty; ;
            string validity = string.Empty;
            string sleepers = string.Empty;
            string packages = string.Empty;
            string conditions = string.Empty;
            string easement = string.Empty;
            string internetonly = string.Empty;

            try
            {
                //Loop through the XML nodes creating Xml Tickets
                foreach (XmlNode node in nodelist)
                {
                    ticketTypeCode = string.Empty;
                    ticketTypeName = string.Empty;
                    description = string.Empty;
                    applicableTocs = null;
                    tocAndConnections = string.Empty;
                    validityCodes = null;
                    valcodes = new List<string>();
                    fareCategory = string.Empty;
                    groupSave = false;
                    discounts = string.Empty;
                    availability = string.Empty;
                    retailing = string.Empty;
                    bookingDeadlines = string.Empty;
                    changesToTravelPlans = string.Empty;
                    refunds = string.Empty;
                    breaksOfJourney = string.Empty; ;
                    validity = string.Empty;
                    sleepers = string.Empty;
                    packages = string.Empty;
                    conditions = string.Empty;
                    easement = string.Empty;
                    internetonly = string.Empty;

                    ticketTypeCode = node.SelectSingleNode("def:TicketTypeCode", ns).InnerXml;
                    ticketTypeName = node.SelectSingleNode("def:TicketTypeName", ns).InnerXml;
                    description = node.SelectSingleNode("def:Description", ns).InnerXml;
                    applicableTocs = new ApplicableTOCs();

                    if (node.SelectSingleNode("def:ApplicableTocs/def:AllTocs", ns) != null)
                    {
                        applicableTocs.AllTocs = Convert.ToBoolean(node.SelectSingleNode("def:ApplicableTocs/def:AllTocs", ns).InnerXml);
                    }

                    foreach (XmlNode tocNode in node.SelectNodes("def:ApplicableTocs/def:IncludedTocs/*", ns))
                    {
                        applicableTocs.IncludedTocs.Add(new IncludedTOCs("", tocNode.InnerXml));
                    }

                    foreach (XmlNode tocNode in node.SelectNodes("def:ApplicableTocs/def:ExcludedTocs/*", ns))
                    {
                        applicableTocs.ExcludedTocs.Add(new ExcludedTOCs("", tocNode.InnerXml));
                    }

                    if (node.SelectSingleNode("def:ApplicableTocs/def:TocAndConnections", ns) != null)
                    {
                        applicableTocs.TocsAndConnections = node.SelectSingleNode("def:ApplicableTocs/def:TocAndConnections", ns).InnerXml;
                    }

                    if (node.SelectSingleNode("def:ValidityCodes", ns) != null)
                    {
                        valcodes.Clear();
                        foreach (XmlNode valNode in node.SelectNodes("def:ValidityCodes/*", ns))
                        {
                            valcodes.Add(valNode.InnerXml);
                        }
                        validityCodes = new ValidityCodes(valcodes);
                    }

                    if (node.SelectSingleNode("def:Validity", ns) != null)
                    {
                        validity = node.SelectSingleNode("def:Validity", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Sleepers", ns) != null)
                    {
                        sleepers = node.SelectSingleNode("def:Sleepers", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:BreaksOfJourney", ns) != null)
                    {
                        breaksOfJourney = node.SelectSingleNode("def:BreaksOfJourney", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:FareCategory", ns) != null)
                    {
                        fareCategory = node.SelectSingleNode("def:FareCategory", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:GroupSave", ns) != null)
                    {
                        groupSave = Convert.ToBoolean(node.SelectSingleNode("def:GroupSave", ns).InnerXml);
                    }
                    if (node.SelectSingleNode("def:Discounts", ns) != null)
                    {
                        discounts = node.SelectSingleNode("def:Discounts", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Availability", ns) != null)
                    {
                        availability = node.SelectSingleNode("def:Availability", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Retailing", ns) != null)
                    {
                        retailing = node.SelectSingleNode("def:Retailing", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:BookingDeadlines", ns) != null)
                    {
                        bookingDeadlines = node.SelectSingleNode("def:BookingDeadlines", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:ChangesToTravelPlans", ns) != null)
                    {
                        changesToTravelPlans = node.SelectSingleNode("def:ChangesToTravelPlans", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Refunds", ns) != null)
                    {
                        refunds = node.SelectSingleNode("def:Refunds", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Packages", ns) != null)
                    {
                        packages = node.SelectSingleNode("def:Packages", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Conditions", ns) != null)
                    {
                        conditions = node.SelectSingleNode("def:Conditions", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:Easement", ns) != null)
                    {
                        easement = node.SelectSingleNode("def:Easement", ns).InnerXml;
                    }
                    if (node.SelectSingleNode("def:InternetOnly", ns) != null)
                    {
                        internetonly = node.SelectSingleNode("def:InternetOnly", ns).InnerXml;
                    }

                    rtnTickets.AllTickets.Add(new Ticket(ticketTypeCode, ticketTypeName, description, applicableTocs,
                        validityCodes, validity, sleepers, fareCategory,
                        groupSave, discounts, availability, retailing,
                        bookingDeadlines, refunds, breaksOfJourney,
                        changesToTravelPlans, packages, conditions, easement, internetonly));
                }
            }
            catch (Exception ex)
            {
                throw new TDException(ex.Message, false, TDExceptionIdentifier.XMLRTTTicketParser);
            }
            return rtnTickets;
        }
    }
}
