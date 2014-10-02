var walkitLinks = null;

// adds function to be run when page loaded
dojo.addOnLoad(function() {

    var map = null;
    var arr = dijit.findWidgets(document.body);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    if (map) {

        var util = ESRIUK.util();

        // core map component
        var mapCore = map.attachMap._map;

        // add esri mapbase onLoad event handler
        // map.attachMap is a reference ESRI api MapBase object
        // map.attachMap._connects is an array containing list of event handlers
        // we adding our method to same array to be called when map gets loaded
        map.attachMap._connects.push(util.formatConnect(dojo.connect(map, "onLoad", null, mapOnLoad), "customMapOnLoad"));


    }
});

// function to be called when ESRI base map objects finish loading
function mapOnLoad(event) {
    var map = null;
    var arr = dijit.findWidgets(document.body);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    if (map) {

        var util = ESRIUK.util();

        // core map component
        var mapCore = map.attachMap._map;
        
        // Add handler for ESRI's core map onClick event
        map.attachMap._connects.push(util.formatConnect(dojo.connect(mapCore.graphics, "onClick", null, AddWalkitLink), "customOnGraphicsClick"));
    }
}

// Handler for ESRI's core map onClick event
// This method checks if the click event is called by maps graphic objects like pushpin symbol
function AddWalkitLink(event) {

    var util = ESRIUK.util();
    
    var map = null;
    var arr = dijit.findWidgets(document.body);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    if (map) {
        // check if the event got graphic object and it got attributes
        // only then call custom draw callout function
        if (util.isObject(event.graphic)) {
            var graphic = event.graphic;
            if (!util.isNothing(graphic.attributes)) {
                
                // Custom draw callout function responsible for showing information window
                walkitDrawCallout(event, map.attachMap.getMap(), graphic, graphic.geometry);
            }
        }
    }
}

function walkitDrawCallout(event, map, graphic, geom) {
    //  summary:
    //    injects walkit link in to the info window popup on overlays

    var config = ESRIUK._config; // reference to esri config
    var util = ESRIUK.util(); // reference to ESRI Util object
    
    var   attr = graphic.attributes,
          arr = attr.values
    //  specific feature class infoWindow
    types = config.graphics.typeEnum,
    //  extract type attribute - if it exists
          type = util.isStringPopulated(attr.type) ? attr.type : 'Generic';

    // Gets walkit link based on the event click raised
    var walkitLink = getWalkitLink(event);
    
    // only add walkit link if the graphics type is carpark or stop
    if (walkitLink && walkitLink != "") {
        switch (type) {
            //  CarParks Feature  
            case types.carParks:
            //  Stops (both) Feature

            case types.stops:
                AddWalkitLinkInWindow(walkitLink, map, event, false);
                break;
        }
    }
  
    
}


// Adds walkit link to existing information window for stop or carpark symbol
function AddWalkitLinkInWindow(walkitLink, map, event, createNew) {

    if (createNew) {
        // if window needs creating add code here
    }
    else {
        var win = map.infoWindow; // reference to map information window

        if (win._content && win._content.children.length > 0) {
            var wincontent = win._content.children[0];  // reference to the information window content
            
            // Create a hyper link using walkitlink object in information window content
            dojo.create("a", { href: walkitLink.url, innerHTML: walkitLink.description, target: '_blank' }, wincontent, "last");
        }
    }
}

// Initialises the walkit links 
// This is called when page loads by rendering a script on page by serverside
function initWalkitLinks(links) {
    walkitLinks = links;

}

// Gets the walkit link object from the array of walkit links initialise by server side using initWalkitLinks method
function getWalkitLink(event) {
    var osgr = event.graphic.geometry; //reference to the map's point osgr coordinates

    var walkitLink = null;

    if (osgr) {
        var easting = osgr.x;
        var northing = osgr.y;


        for (var i = 0; i < walkitLinks.length; i++) {
            var link = walkitLinks[i];

            if (link) {
                // if the link's easting or northing are within car/stop's easting or northing by margin of 10
                // return the walkit link as current link object in the list
                if ((link.easting < easting + 10 && link.easting > easting - 10)
                && (link.northing < northing + 10 && link.northing > northing - 10)) {
                    walkitLink = link;
                    break;
                }
            }
        }
    }

    return walkitLink;
}

function getEsriMap() {

    var map = null;
    var arr = dijit.findWidgets(document.body);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    return map;
}