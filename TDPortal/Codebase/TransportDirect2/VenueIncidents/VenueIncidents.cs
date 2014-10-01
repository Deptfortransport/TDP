using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.TravelNews;
using TDP.UserPortal.TravelNews.SessionData;
using TDP.UserPortal.TravelNews.TravelNewsData;
using TDP.Common.PropertyManager;
using TDP.Common;

namespace TDP.VenueIncidents
{
    [XmlRoot(Namespace = "http://www.transportdirect.info/VenueIncidents")]
    public class VenueIncidents
    {
        // Attributes which are serialised into xml
        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation = "http://www.transportdirect.info VenueIncidents.xsd";

        public DateTime GenerationDateTime { get; set; }
        public int DataVersion { get; set; }
        [XmlElement("Venue")]
        public List<Venue> Venues { get; set; }

        private ITravelNewsHandler travelNewsHandler;
        private string outputFileLocationKey = @"VenueIncidents.OutputFile.Location";
        private string outputFileLocation;

        /// <summary>
        /// Constructor set the travel news handler to a default configuration
        /// </summary>
        public VenueIncidents()
        {
            travelNewsHandler = TDPServiceDiscovery.Current.Get<ITravelNewsHandler>(ServiceDiscoveryKey.TravelNews);
        }

        /// <summary>
        /// Constructor allows the travel news handler to be specified
        /// </summary>
        /// <param name="overrideHandler"></param>
        public VenueIncidents(ITravelNewsHandler overrideHandler)
        {
            travelNewsHandler = overrideHandler;
        }

        /// <summary>
        /// Allows the output file location to be overridden, if not set, get will use property from db
        /// </summary>
        [XmlIgnore]
        public string OutputFileLocation
        {
            get
            {
                if (outputFileLocation == null)
                {
                    outputFileLocation = TDP.Common.PropertyManager.Properties.Current[outputFileLocationKey];
                }

                return outputFileLocation;
            }

            set
            {
                this.outputFileLocation = value;
            }
        }

        /// <summary>
        /// Generates the travel news alert xml file
        /// </summary>
        /// <param name="filename">the filename to save the file to</param>
        public void GenerateTravelNewsAlertFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            TravelNewsState travelNewsState = new TravelNewsState();
            
            travelNewsState.SetDefaultState();

            // Ensure travel news state is as per Mobile requirements
            // - PT only
            // - Today only
            // - All regions
            // - Venue incidents
            // - Active incidents only
            travelNewsState.SelectedTransport = TransportType.PublicTransport;
            travelNewsState.SelectedDate = DateTime.Now.Date;
            travelNewsState.SelectedRegion = TravelNewsRegion.All.ToString();
            travelNewsState.SelectedVenuesFlag = true;
            travelNewsState.SearchNaptans = GetVenueNaptans();
            travelNewsState.SelectedIncidentActive = IncidentActiveStatus.Active.ToString();

            TravelNewsItem[] items = travelNewsHandler.GetDetails(travelNewsState);

            // Debug
            Console.WriteLine();

            Console.WriteLine(String.Format("Date:{0} Transport:{1} Region:{2} Active:{3}",
                travelNewsState.SelectedDate,
                travelNewsState.SelectedTransport,
                travelNewsState.SelectedRegion,
                travelNewsState.SelectedIncidentActive
                ));
            Console.WriteLine(String.Format("Found {0} active news items affecting venues", 
                items.Length
                ));
            Console.WriteLine();


            // Set up the venues list to output in the xml
            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            List<TDPLocation> locations = locationService.GetTDPVenueLocations();

            Venues = new List<Venue>();

            foreach (TDPLocation location in locations)
            {
                Venues.Add(new Venue(location.Naptan.FirstOrDefault()));
            }

            
            // Update venue (and their direct children) active incidents flag
            foreach (TravelNewsItem item in items)
            {
                SetVenueIncidents(item);
            }

            // Update the children of parent venues with an active incident 
            // are also marked as having an active incident
            IEnumerable<Venue> parentVenues = (from v in Venues
                                             join l in locations on v.VenueNaPTAN equals l.Naptan.FirstOrDefault()
                                             where v.ActiveIncident == true && String.IsNullOrEmpty(l.Parent)
                                             select v);

            foreach (Venue parentVenue in parentVenues)
            {
                IEnumerable<Venue> childVenues = (from v in Venues
                                                  join l in locations on v.VenueNaPTAN equals l.Naptan.FirstOrDefault()
                                                  where l.Parent == parentVenue.VenueNaPTAN
                                                  select v);

                foreach (Venue childVenue in childVenues)
                {
                    childVenue.ActiveIncident = true;
                }
            }

            GenerationDateTime = DateTime.Now;
            DataVersion = 1;

            // Debug
            string debugText = string.Empty;
            int count = 0;
            foreach (Venue venue in Venues)
            {
                debugText = string.Format("{0} {1}", venue.VenueNaPTAN, venue.ActiveIncident);
                debugText = debugText.PadRight(18);

                count++;

                if (count <= 3)
                    Console.Write(debugText);
                else
                {
                    Console.WriteLine(debugText);
                    count = 0;
                }
            }
            Console.WriteLine();

            try
            {
                using (FileStream file = File.Create(OutputFileLocation))
                {
                    XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings { Encoding = Encoding.UTF8 });

                    xmlSerializer.Serialize(writer, this);

                    Console.WriteLine(String.Format("Venue Incident file created {0}", OutputFileLocation));
                }
            }
            catch (Exception e)
            {
                throw new TDPException("Unable to create Incident file", e, false, TDPExceptionIdentifier.DLDataLoaderUnexpectedException);
            }
        }

        /// <summary>
        /// Returns a full list of venue naptans to show the travel news for
        /// </summary>
        /// <returns></returns>
        public List<string> GetVenueNaptans()
        {
            List<string> naptans = new List<string>();

            try
            {
                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
                List<TDPLocation> venues = locationService.GetTDPVenueLocations();

                foreach (TDPLocation venue in venues)
                {
                    naptans.Add(venue.Naptan.FirstOrDefault());
                }

            }
            catch
            {
                // Ignore any exceptions
            }

            return naptans;
        }

        /// <summary>
        /// Recursive function that set's a venue incident flag based on this incident and any child incidents
        /// </summary>
        /// <param name="travelNewsItem">The incident to be processed</param>
        private void SetVenueIncidents(TravelNewsItem travelNewsItem)
        {
            IEnumerable<Venue> affectedVenues = Venues.AsQueryable().Join(travelNewsItem.OlympicVenuesAffected, v => v.VenueNaPTAN, i => i, (v, i) => v);

            foreach (Venue venue in affectedVenues)
            {
                venue.ActiveIncident = true;
            }

            // get any child incidents for this item
            TravelNewsItem[] childIncidents = travelNewsHandler.GetChildrenDetailsByUid(travelNewsItem.Uid);

            foreach (TravelNewsItem item in childIncidents)
            {
                SetVenueIncidents(item);
            }
        }
    }

    public class Venue
    {
        public string VenueNaPTAN { get; set; }
        public bool ActiveIncident { get; set; }
        public string VenueTravelNewsURL { get; set; }

        private Venue()
        {
            throw new NotImplementedException();
        }

        public Venue(string venueNaPTAN)
        {
            string uriFormat = TDP.Common.PropertyManager.Properties.Current["VenueIncidents.IncidentLandingPage.Location"];
            this.VenueNaPTAN = venueNaPTAN;
            this.ActiveIncident = false;
            this.VenueTravelNewsURL = string.Format(uriFormat, venueNaPTAN);
        }
    }
}