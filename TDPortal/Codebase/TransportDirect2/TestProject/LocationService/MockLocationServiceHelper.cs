// *********************************************** 
// NAME             : MockLocationServiceHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Helper class to load test location service data
// ************************************************
// 
                

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.LocationService;
using TDP.Common;

namespace TDP.TestProject.Common.LocationService
{
    public class MockLocationServiceHelper
    {
        #region Helpers

        /// <summary>
        /// Temporary method to add some locations data.
        /// Currently Venues, GNAT, and Postcodes are commented out and not loaded
        /// so manually add items to satisfy test
        /// </summary>
        public static void PopulateLocationLists()
        {
            // Create venue list item
            List<TDPLocation> venuesList = new List<TDPLocation>();
            venuesList.Add(new TDPLocation("Olympic Park", TDPLocationType.Venue, TDPLocationType.Venue, "8100OPK"));
            venuesList.Add(new TDPLocation("Olympic Stadium", TDPLocationType.Venue, TDPLocationType.Venue, "8100STA"));
            TDPVenueLocationCache_Accessor.venuesList = venuesList;

            // Create venue location item(s)
            List<TDPLocation> venueLocations = new List<TDPLocation>();
            TDPVenueLocation tdpLocation = new TDPVenueLocation("Olympic Park", "Olympic Park", "N0077705",
                new List<string>(), new List<string>() { "8100OPK" }, string.Empty,
                TDPLocationType.Venue,
                new OSGridReference("537910,184993"), new OSGridReference("537910,184993"), false, true,
                123, 987, "8100OPK");

            tdpLocation.VenueMapUrl = "venueMapUrl";
            tdpLocation.VenueTravelNewsRegion = "L";
            tdpLocation.VenueWalkingRoutesUrl = "venueWalkingRoutesMapURL";
            tdpLocation.CycleToVenueDistance = 5;
            tdpLocation.VenueRiverServiceAvailable = RiverServiceAvailableTypeHelper.GetRiverServiceAvailableType("Yes");
            tdpLocation.VenueGroupID = "OPK";
            tdpLocation.VenueGroupName = "Olympic Park";

            venueLocations.Add(tdpLocation);

            tdpLocation = new TDPVenueLocation("Olympic Stadium", "Olympic Stadium", "N0077705",
                new List<string>(), new List<string>() { "8100STA" }, "8100OPK",
                TDPLocationType.Venue,
                new OSGridReference("537910,184993"), new OSGridReference("537910,184993"), false, true,
                123, 987, "8100STA");

            tdpLocation.VenueMapUrl = "venueMapUrl";
            tdpLocation.VenueTravelNewsRegion = "L";
            tdpLocation.VenueWalkingRoutesUrl = "venueWalkingRoutesMapURL";
            tdpLocation.CycleToVenueDistance = 5;
            tdpLocation.VenueRiverServiceAvailable = RiverServiceAvailableTypeHelper.GetRiverServiceAvailableType("Yes");
            tdpLocation.VenueGroupID = "OPK";
            tdpLocation.VenueGroupName = "Olympic Park";

            venueLocations.Add(tdpLocation);

            tdpLocation = new TDPVenueLocation("Hyde Park", "Hyde Park", "N0077705",
                new List<string>(), new List<string>() { "8100HYD" }, string.Empty,
                TDPLocationType.Venue,
                new OSGridReference("537910,184993"), new OSGridReference("537910,184993"), false, true,
                123, 987, "8100HYD");

            tdpLocation.VenueMapUrl = "venueMapUrl";
            tdpLocation.VenueTravelNewsRegion = "L";
            tdpLocation.VenueWalkingRoutesUrl = "venueWalkingRoutesMapURL";
            tdpLocation.CycleToVenueDistance = 5;
            tdpLocation.VenueRiverServiceAvailable = RiverServiceAvailableTypeHelper.GetRiverServiceAvailableType("Yes");
            tdpLocation.VenueGroupID = string.Empty;
            tdpLocation.VenueGroupName = string.Empty;

            venueLocations.Add(tdpLocation);

            TDPVenueLocationCache_Accessor.venueLocations = venueLocations;

            // Create car park item
            List<TDPVenueCarPark> carParks = new List<TDPVenueCarPark>();
            Dictionary<string, List<string>> venueCarParks = new Dictionary<string, List<string>>();

            TDPVenueCarPark venuePark = new TDPVenueCarPark();
            venuePark.ID = "OPK_EBB";
            venuePark.Name = "Olympic Park Ebbsfleet Blue Badge";
            venuePark.VenueNaPTAN = "8100OPK";
            venuePark.MapOfSiteUrl = "MapOfSiteUrl";
            venuePark.InterchangeDuration = 10;
            venuePark.CoachSpaces = 1000;
            venuePark.CarSpaces = 5000;
            venuePark.DisabledSpaces = 100;
            venuePark.BlueBadgeSpaces = 100;
            venuePark.DriveFromToid = "osgbDriveFromToid";
            venuePark.DriveToToid = "osgbDriveToToid";

            List<TDPParkAvailability> availability = new List<TDPParkAvailability>();
            TDPParkAvailability parkAvailability = new TDPParkAvailability(new DateTime(2012, 1, 1), new DateTime(2014, 12, 31), new TimeSpan(8, 0, 0), new TimeSpan(1, 0, 0), DaysOfWeek.Everyday);
            availability.Add(parkAvailability);
            venuePark.Availability = availability;

            carParks.Add(venuePark);

            List<string> carParkList = new List<string>();
            carParkList.Add(venuePark.ID);
            venueCarParks.Add(venuePark.VenueNaPTAN, carParkList);

            TDPVenueLocationCache_Accessor.carParks = carParks;
            TDPVenueLocationCache_Accessor.venueCarParks = venueCarParks;

            // Create cycle park item
            List<TDPVenueCyclePark> cycleParks = new List<TDPVenueCyclePark>();
            Dictionary<string, List<string>> venueCycleParks = new Dictionary<string, List<string>>();

            TDPVenueCyclePark venueParkCycle = new TDPVenueCyclePark();
            venueParkCycle.ID = "WEACY01";
            venueParkCycle.Name = "West Cycle Park";
            venueParkCycle.VenueNaPTAN = "8100OPK";
            venueParkCycle.CycleParkMapUrl = "CycleParkMapURL";
            venueParkCycle.StorageType = CycleStorageType.Loops;
            venueParkCycle.CycleToGridReference = new OSGridReference("537910,184993");
            venueParkCycle.CycleFromGridReference = new OSGridReference("537910,184993");
            venueParkCycle.NumberOfSpaces = 100;
            venueParkCycle.VenueGateEntranceNaPTAN = "VenueEntranceGate";
            venueParkCycle.WalkToGateDuration = new TimeSpan(0, 10, 0);
            venueParkCycle.VenueGateExitNaPTAN = "VenueExitGate";
            venueParkCycle.WalkFromGateDuration = new TimeSpan(0, 5, 0);

            availability = new List<TDPParkAvailability>();
            parkAvailability = new TDPParkAvailability(new DateTime(2012, 1, 1), new DateTime(2014, 12, 31), new TimeSpan(8, 0, 0), new TimeSpan(1, 0, 0), DaysOfWeek.Everyday);
            availability.Add(parkAvailability);
            venueParkCycle.Availability = availability;

            cycleParks.Add(venueParkCycle);

            List<string> cycleParkList = new List<string>();
            cycleParkList.Add(venueParkCycle.ID);
            venueCycleParks.Add(venueParkCycle.VenueNaPTAN, cycleParkList);

            TDPVenueLocationCache_Accessor.cycleParks = cycleParks;
            TDPVenueLocationCache_Accessor.venueCycleParks = venueCycleParks;

            // Create venue gate item
            Dictionary<string, List<TDPVenueGate>> venueGates = new Dictionary<string, List<TDPVenueGate>>();

            TDPVenueGate venueGate = new TDPVenueGate();
            venueGate.GateNaPTAN = "8100HYDg0";
            venueGate.GateName = "Hyde park gate entrance";
            venueGate.GateGridRef = new OSGridReference("312345,312345");
            venueGate.AvailableFrom = new DateTime(2012, 6, 1);
            venueGate.AvailableTo = new DateTime(2014, 12, 31);

            List<TDPVenueGate> venueGateList = new List<TDPVenueGate>();
            venueGateList.Add(venueGate);
            venueGates.Add(venueGate.GateNaPTAN, venueGateList);

            TDPVenueLocationCache_Accessor.venueGates = venueGates;

            // Create venue gate navigation path item
            Dictionary<string, List<TDPVenueGateNavigationPath>> venueGateNavigationPaths = new Dictionary<string, List<TDPVenueGateNavigationPath>>();

            TDPVenueGateNavigationPath venueGateNavigationPath = new TDPVenueGateNavigationPath();
            venueGateNavigationPath.GateNaPTAN = "8100HYDg0";
            venueGateNavigationPath.NavigationPathID = "8100HYD_Path1";
            venueGateNavigationPath.NavigationPathName = "Hyde park path 1";
            venueGateNavigationPath.FromNaPTAN = "8100HYDg0";
            venueGateNavigationPath.ToNaPTAN = "8100HYD";
            venueGateNavigationPath.TransferDistance = 2000;
            venueGateNavigationPath.TransferDuration = new TimeSpan(0, 25, 0);

            List<TDPVenueGateNavigationPath> venueGateNavigationPathList = new List<TDPVenueGateNavigationPath>();
            venueGateNavigationPathList.Add(venueGateNavigationPath);

            venueGateNavigationPath = new TDPVenueGateNavigationPath();
            venueGateNavigationPath.GateNaPTAN = "8100HYDg0";
            venueGateNavigationPath.NavigationPathID = "8100HYD_Path2";
            venueGateNavigationPath.NavigationPathName = "Hyde park path 2";
            venueGateNavigationPath.FromNaPTAN = "8100HYD";
            venueGateNavigationPath.ToNaPTAN = "8100HYDg0";
            venueGateNavigationPath.TransferDistance = 2000;
            venueGateNavigationPath.TransferDuration = new TimeSpan(0, 25, 0);

            venueGateNavigationPathList.Add(venueGateNavigationPath);

            venueGateNavigationPaths.Add(venueGateNavigationPath.GateNaPTAN, venueGateNavigationPathList);

            TDPVenueLocationCache_Accessor.venueGateNavigationPaths = venueGateNavigationPaths;

            // Create venue river service item
            Dictionary<string, List<TDPVenueRiverService>> venueRiverServices = new Dictionary<string, List<TDPVenueRiverService>>();

            TDPVenueRiverService riverService = new TDPVenueRiverService();
            riverService.VenueNaPTAN = "8100GRP";
            riverService.VenuePierNaPTAN = "8100GRPp1";
            riverService.RemotePierNaPTAN = "8100GRPp2";
            riverService.VenuePierName = "Venue pier name";
            riverService.RemotePierName = "Remote pier name";

            List<TDPVenueRiverService> riverServiceList = new List<TDPVenueRiverService>();
            riverServiceList.Add(riverService);
            venueRiverServices.Add(riverService.VenueNaPTAN, riverServiceList);

            TDPVenueLocationCache_Accessor.venueRiverServices = venueRiverServices;

            // Create venue pier navigation path item
            Dictionary<string, List<TDPPierVenueNavigationPath>> pierVenueNavigationPaths = new Dictionary<string, List<TDPPierVenueNavigationPath>>();

            TDPPierVenueNavigationPath pierVenueNavigationPath = new TDPPierVenueNavigationPath();
            pierVenueNavigationPath.VenueNaPTAN = "8100GRP";
            pierVenueNavigationPath.NavigationID = "8100GRPpath1";
            pierVenueNavigationPath.FromNaPTAN = "8100GRP";
            pierVenueNavigationPath.ToNaPTAN = "9300GNW1";
            pierVenueNavigationPath.DefaultDuration = new TimeSpan(0, 10, 0); ;
            pierVenueNavigationPath.Distance = 1000;

            pierVenueNavigationPath.AddTransferText("transferText", Language.English);
            pierVenueNavigationPath.AddTransferText("transferText", Language.Welsh);

            List<TDPPierVenueNavigationPath> pierVenueNavigationPathList = new List<TDPPierVenueNavigationPath>();
            pierVenueNavigationPathList.Add(pierVenueNavigationPath);

            pierVenueNavigationPath = new TDPPierVenueNavigationPath();
            pierVenueNavigationPath.VenueNaPTAN = "8100GRP";
            pierVenueNavigationPath.NavigationID = "8100GRPpath2";
            pierVenueNavigationPath.FromNaPTAN = "9300GNW1";
            pierVenueNavigationPath.ToNaPTAN = "8100GRP";
            pierVenueNavigationPath.DefaultDuration = new TimeSpan(0, 10, 0); ;
            pierVenueNavigationPath.Distance = 1000;

            pierVenueNavigationPath.AddTransferText("transferText", Language.English);
            pierVenueNavigationPath.AddTransferText("transferText", Language.Welsh);

            pierVenueNavigationPathList.Add(pierVenueNavigationPath);

            pierVenueNavigationPaths.Add(pierVenueNavigationPath.VenueNaPTAN, pierVenueNavigationPathList);

            TDPVenueLocationCache_Accessor.pierVenueNavigationPaths = pierVenueNavigationPaths;

            // Create venue access data item
            Dictionary<string, List<TDPVenueAccess>> venueAccessData = new Dictionary<string, List<TDPVenueAccess>>();
            List<TDPVenueAccess> venueAccessList = new List<TDPVenueAccess>();

            TDPVenueAccess tdpVenueAccess = new TDPVenueAccess("8100MIL", "venue name", new DateTime(2012, 01, 01), new DateTime(2014, 12, 31), new TimeSpan(0, 10, 0));
            TDPVenueAccessStation tdpVenueAccessStation = new TDPVenueAccessStation("9100EUS", "Euston");
            tdpVenueAccessStation.AddTransferText("Transfer text", Language.English, true);
            tdpVenueAccessStation.AddTransferText("Transfer text", Language.English, false);
            tdpVenueAccessStation.AddTransferText("Transfer text", Language.Welsh, true);
            tdpVenueAccessStation.AddTransferText("Transfer text", Language.Welsh, false);

            tdpVenueAccess.Stations.Add(tdpVenueAccessStation);

            venueAccessList.Add(tdpVenueAccess);

            venueAccessData.Add(tdpVenueAccess.VenueNaPTAN, venueAccessList);

            TDPVenueLocationCache_Accessor.venueAccessData = venueAccessData;

            // Create venue gate check constraint item
            Dictionary<string, List<TDPVenueGateCheckConstraint>> venueGateCheckConstraints = new Dictionary<string, List<TDPVenueGateCheckConstraint>>();

            TDPVenueGateCheckConstraint venueGateCheckConstraint = new TDPVenueGateCheckConstraint();
            venueGateCheckConstraint.GateNaPTAN = "8100HYDg0";
            venueGateCheckConstraint.CheckConstraintID = "8100HYDg0check1";
            venueGateCheckConstraint.CheckConstraintName = "CheckConstraintName";
            venueGateCheckConstraint.IsEntry = true;
            venueGateCheckConstraint.Process = "Security check";
            venueGateCheckConstraint.Congestion = "Queue";
            venueGateCheckConstraint.AverageDelay = new TimeSpan(0, 10, 0);

            List<TDPVenueGateCheckConstraint> venueGateCheckConstraintList = new List<TDPVenueGateCheckConstraint>();
            venueGateCheckConstraintList.Add(venueGateCheckConstraint);
            venueGateCheckConstraints.Add(venueGateCheckConstraint.GateNaPTAN, venueGateCheckConstraintList);

            TDPVenueLocationCache_Accessor.venueGateCheckConstraints = venueGateCheckConstraints;

            // Create gnat item
            List<TDPGNATLocation> gnatList = new List<TDPGNATLocation>();
            gnatList.Add(new TDPGNATLocation("Coach Stop", TDPLocationType.Station, "900010171",
                true, true, "123", "EN", 123, 123, TDPGNATLocationType.Coach));
            gnatList.Add(new TDPGNATLocation("Railway Stop", TDPLocationType.Station, "9100LESTER",
                true, true, "123", "EN", 123, 123, TDPGNATLocationType.Rail));
            TDPGNATLocationCache_Accessor.gnatList = gnatList;

            // Create admin area item
            List<TDPGNATAdminArea> adminAreaList = new List<TDPGNATAdminArea>();
            adminAreaList.Add(new TDPGNATAdminArea(82, 282, true, true));
            adminAreaList.Add(new TDPGNATAdminArea(82, 281, true, false));
            adminAreaList.Add(new TDPGNATAdminArea(82, 288, false, true));
            TDPGNATLocationCache_Accessor.adminAreaList = adminAreaList;

            // Create postcode item
            List<TDPLocation> postcodeLocations = new List<TDPLocation>();
            postcodeLocations.Add(new TDPLocation("AB101AL", "AB10 1AL", "Locality",
                new List<string>() { "123456789" }, new List<string>(), string.Empty,
                TDPLocationType.Postcode, TDPLocationType.Postcode,
                new OSGridReference("312345,323456"), new OSGridReference("312345,323456"),
                false, false, 123, 123, "AB101AL"));
            postcodeLocations.Add(new TDPLocation("NG91AL", "NG9 1AL", "Locality",
                new List<string>() { "123456789" }, new List<string>(), string.Empty,
                TDPLocationType.Postcode, TDPLocationType.Postcode,
                new OSGridReference("312345,323456"), new OSGridReference("312345,323456"),
                false, false, 123, 123, "NG91AL"));
            TDPLocationCache_Accessor.postcodeLocations = postcodeLocations;
        }

        #endregion
    }
}
