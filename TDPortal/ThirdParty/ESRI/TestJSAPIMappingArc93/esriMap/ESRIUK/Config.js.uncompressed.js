/**
*   @copyright: ESRI (UK) 2009
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: July 2009
*   @description:
*     Static class to hold all config settings used throughout the application
*     This class can be accessed throughout the application via:
*       //  reference class/file
*       dojo.require('ESRIUK.Config');
*       //  get handle on instance of config
*       var config = ESRIUK.config();
*       //  access it's properties
*       (config.mapServiceTypeEnum.dynamic == 'dynamic') returns true
*     
*       eg:  config.mapServices[0].baseMap //  returns boolean property of first defined mapService settings
*
*   @updates:
*
*/
dojo.provide("ESRIUK.Config");


dojo.declare("ESRIUK.Config", null, (function(){
var util;
//  IGNORE: enumeration of map service types
var mapServiceTypeEnum = { tiled:'tiled', dynamic: 'dynamic', image: 'image', ims93: 'arcims93', wms: 'wms', imsserver: 'imsserver' };
//  change when in a live environment
var liveEnvironment = true;
//  START: Specific settings for TDP Map service
var esriukTDPSettings = {
    //  IGNORE: is this map service the main (base) map service - DO NOT CHANGE
    baseMap:true,
    //  IGNORE: does this map service allow graphic additions - DO NOT CHANGE
    addGraphics:true,
    //  Location of proxy JavaScript Ajax Class
    url: "/TransportDirectMapWebService/TransportDirectMapWebService.ashx?proxy",
    //  Name of proxy JavaScript Ajax Class
    checkString:'TransportDirectMapWebService',
    //  Milliseconds to wait for initial request
    timeout:25000,
    //  IGNORE: type of map service - use custom imsserver type in this case
    type: mapServiceTypeEnum.imsserver,
    //  string constants
    errorStrings:{
      //  no valid map was retrieved on 'initMap'
      no_map:' Unable to retrieve a map from the server.',
      //  no image url is returned by a getImage request to map web service
      no_imageUrl:' Result does not contain valid imageUrl. ',
      //  no status code returned by map web service
      no_errorStatusCode:' Result does not contain valid errorStatusCode. ',
      //  no valid result object returned by map web service
      no_result:' No valid result was returned. ',
      //  no valid http response returned by map web service
      no_response:' No valid response was returned. ',
      //  invalid status code returned by map web service
      invalid_status:' Invalid status code returned. ',
      //  invalid image url returned by map web service
      invalid_url:' Invalid image url returned. '
    },
    //  used for toggling layer visibilities
    layers:{
      carparks:'Carparks',
      roadIncidents:'Road Incidents',
      publicIncidents:'Public Incidents',
      pointX:'PointX',  // prefix only
      stops:'Stops'  //  prefix only
    },
    //  image url regular expression check
    imgUrlPattern:/^http:\/\/.+(\.png|\.jpg|\.gif)$/
  };
//  END: Specific settings for TDP Map service
//  IGNORE: web service type enumeration
var webServiceTypeEnum = { geom: 'geometry', feat: 'feature', query:'query' };  
var esriukTDPQuery = {
    //  Location of proxy JavaScript Ajax Class
    url: "/TransportDirectQueryWebService/TransportDirectQueryWebService.ashx?proxy",
    //  Name of proxy JavaScript Ajax Class
    checkString:'TransportDirectQueryWebService',
    //  Milliseconds to wait for initial request
    timeout:25000,  
    //  IGNORE: type of web service
    type:webServiceTypeEnum.query,
    //  **  IMPORTANT **
    //  Must match array in web.config of TransportDirectQueryWebService
    //  Array of returned fields from a 'getTravelNews' query
    //  <Incidents returnedFields='' />
    roadIncidentQueryFields:[  
      'Headline','Incident Type','Severity','Detail',
      'Start Date','End Date','Last Updated',
      'Planned','Type'
    ],
    //  these two must match the type & planned field names above
    roadIncidentTypeField:'Type',
    roadIncidentPlannedField:'Planned'
};
//  array of required css files - added dynamically
var requiredCSSFiles = [
  djConfig.esriJSAPI+"js/dojo/dijit/themes/tundra/tundra.css",
  djConfig.modulePaths.ESRIUK+"/css/ESRIUK.Custom.css"];


return {
  events:{
    mapLoad:'esriukMap/onLoad',
    useAsStartPoint:function(x,y,text){
      try{ ESRIUKTDPAPI.useAsStartPoint(x,y,text); }catch(err){}
    },    
    useAsViaPoint:function(x,y,text){ 
      try{ ESRIUKTDPAPI.useAsViaPoint(x,y,text); }catch(err){}
    },        
    useAsEndPoint:function(x,y,text){ 
      try{ ESRIUKTDPAPI.useAsEndPoint(x,y,text); }catch(err){}
    },
    showCarParkInformation:function(id){
      try{ ESRIUKTDPAPI.showCarParkInformation(id); }catch(err){}
    },
    showStopsInformation:function(id){
      try{ ESRIUKTDPAPI.showStopsInformation(id); }catch(err){}
    },
    selectNearbyPointResult:function(data){ 
      try{ ESRIUKTDPAPI.selectNearbyPointResult(data); }catch(err){}
    },
    error:function(error){
      try{ this.miscError(error); }catch(err){}
    },
    miscError:function(error){
      //util.error(dojo.toJson(error));
      try{ ESRIUKTDPAPI.miscError(error); }catch(err){}
    },
    queryError:function(error,response){
      //util.error(dojo.toJson(error),dojo.toJson(response));
      try{ ESRIUKTDPAPI.queryError(error,response); }catch(err){}
    },
    mapError:function(error,response){
      //util.error(dojo.toJson(error),dojo.toJson(response));
      try{ ESRIUKTDPAPI.mapError(error,response); }catch(err){}
    },
    mapGraphicsCount: function(num){
      try{ ESRIUKTDPAPI.mapGraphicsCount(num); }catch(err){}
    },
    onMapExtentChange: function(extent,levelChange,scale,ovURL){
      try{ ESRIUKTDPAPI.onMapExtentChange(extent,levelChange,scale,ovURL); }catch(err){}
    },
    onMapInitialiseComplete: function(map){
      //util.debug(map);
      try{ ESRIUKTDPAPI.onMapInitialiseComplete(map); }catch(err){}
    }
  },
  map:{
    //  British National Grid (well-known ID)
    wkid: 27700,     
    //  set map to respond to page resize events ??
    useResizeHandler:false,
    //  START: Zoom Slider Dimensions
    //  set properties for zoom slider
    slider: { left: "23px", top: "4px", width: null, height: "150px"},
    //  END: Zoom Slider Dimensions
    //  padding around slider low levels red box
    sliderBoxPadding: 3,
    //  level to start slider box
    sliderBoxStart:5,
    //  overlap of slider in IE
    sliderIEOverlap: 45,
    //  START: Map Scale Levels
    //  scale slider levels - generated from a dummy cached map service
    lods:[
      {level : 0, resolution : 2381.25476250953, scale : 9000000},
      {level : 1, resolution : 1270.00254000508, scale : 4800000}, 
      {level : 2, resolution : 635.00127000254, scale : 2400000}, 
      {level : 3, resolution : 317.50063500127, scale : 1200000}, 
      {level : 4, resolution : 190.500381000762, scale : 720000}, 
      {level : 5, resolution : 95.250190500381, scale : 360000}, 
      {level : 6, resolution : 47.6250952501905, scale : 180000}, 
      {level : 7, resolution : 21.1667090000847, scale : 80000}, 
      {level : 8, resolution : 10.5833545000423, scale : 40000}, 
      {level : 9, resolution : 5.29167725002117, scale : 20000},
      {level : 10, resolution : 2.11667090000847, scale : 8000}, 
      {level : 11, resolution : 1.05833545000423, scale : 4000}, 
      {level : 12, resolution : 0.529167725002117, scale : 2000}
    ],
    //  END: Map Scale Levels  
    //  START: Default Map Dimensions
    defaultDimensions:{ height: 554, width: 804 }
    //  END: Default Map Dimensions
  },
  statics:{
    //  START: CSS class names
    cssInfoWindowClassName:'esriuk-infowindow',  
    cssInfoWindowContentClassName:'esriuk-infowindow-content',
    cssSelectedToolClass:'selected',
    cssDefaultToolClass:'default',
    cssToolItemClass:'toolbarPanelItem',
    cssTravelNewsField:'travelNewsField',
    cssTravelNewsBody:'travelNewsBody',
    //  END: CSS class names    
    //  START: String constants
    //    Can be replaced by welsh string constants
    travelNewsEnum:{
      //  Travel news types (used in infoWindow titles)
      pRoad:'Roadworks',
      pRail:'Rail Engineering',
      upRoad:'Road incident',
      upRail:'PT incident' 
    },
    planAJourneyText:{
      //  Used for plan a journey links
      addPointPrefix:'Plan a journey',
      addPointSuffixStart:' from here',
      addPointSuffixVia:' via here',
      addPointSuffixEnd:' to here',
      //  Used for info windows when adding a new point
      addPointNoTitle:'No information set for this point',
      addPointTitle:'Add Point',
      addPointDefaultText:'New Point'
    },
    misc:{
      //  car park information link
      infoPageCarParks:'For more information about this car park click here',
      //  stop information link
      infoPageStops:'For more information about this stop click here',
      parkandride:'park and ride ',
      carpark:'car park',
      loading:'Loading...',
      //  OK & Cancel buttons
      ok:'OK',
      cancel:'Cancel',
      //  text for miscellaneous point
      pt:'Point'
    },
    errors:{
      //  not used 
      noNearbyPointsFound:'No nearby points were found.'
    },
    mapNavPanel:{
      //  Used for tooltips for navigation buttons
      altSuffix:' navigation image',      
      panNorth:'Pan North',
      panEast:'Pan East',
      panSouth:'Pan South',
      panWest:'Pan West',
      fullExtent:'Restore original view'
    },
    mapToolbarPanel:{
      //  Used for tooltips for toolbar items
      altSuffix:' tool image',
      panMap:'Pan Map',
      userDefined:'Add A User Defined Location',
      selectNearby:'Select A Nearby Point'
    },
    //  END: String Constants
    //  START: Info window dimensions
    dims:{
      addPointSize:{x:250,y:65},
      calloutGeneric:{x:290,y:100},
      calloutCarParks:{x:290,y:90},
      calloutStops:{x:330,y:90},
      calloutTravelNews:{x:300,y:250},
      calloutPointX:{x:290,y:175},
      ieBuffer:20
    }
    //  END: Info window dimensions
  },
  query:{
    maxLOD:0,
    wait:1500
  },
  graphics:{
    point:((function(){ return new esri.symbol.SimpleMarkerSymbol(
        esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE, //  marker symbol 'circle|diamond|star' etc...
        12, //  marker width
        new esri.symbol.SimpleLineSymbol(
          esri.symbol.SimpleLineSymbol.STYLE_SOLID, //  line style type 'solid|dashed|dotted' etc..
          new dojo.Color([45, 0, 79, 0.0]), //  line colour: r, g, b, transparency
          1 //  line width
        ),
        new dojo.Color([180, 79, 255, 0.0])  //  fill colour: r, g, b, transparency
      );
    })()),
    //  START: User Defined Point
    //    reference: 
    //    http://resources.esri.com/help/9.3/arcgisserver/apis/javascript/arcgis/help/jsapi_start.htm#jsapi/simplemarkersymbol.htm
    pointMisc:((function(){ return new esri.symbol.SimpleMarkerSymbol(
        esri.symbol.SimpleMarkerSymbol.STYLE_CIRCLE, //  marker symbol 'circle|diamond|star' etc...
        12, //  marker width
        new esri.symbol.SimpleLineSymbol(
          esri.symbol.SimpleLineSymbol.STYLE_SOLID, //  line style type 'solid|dashed|dotted' etc..
          new dojo.Color([0,0,0, 0.8]), //  line colour: r, g, b, transparency
          2 //  line width
        ),
        new dojo.Color([180, 79, 255, 0.5])  //  fill colour: r, g, b, transparency
      );
    })()),
    //  END: User Defined Point
    //  START: Main Point
    //    reference: 
    //    http://resources.esri.com/help/9.3/arcgisserver/apis/javascript/arcgis/help/jsapi_start.htm#jsapi/simplemarkersymbol.htm    
    pointMain:((function(){ return new esri.symbol.SimpleMarkerSymbol(
        esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE, //  marker symbol 'circle|diamond|star' etc...
        16, //  marker width
        new esri.symbol.SimpleLineSymbol(
          esri.symbol.SimpleLineSymbol.STYLE_SOLID, //  line style type 'solid|dashed|dotted' etc..
          new dojo.Color([0,0,0, 1]), //  line colour: r, g, b, transparency
          2 //  line width
        ),
        new dojo.Color([3, 0, 223, 1])  //  fill colour: r, g, b, transparency
      );
    })()),
    //  END: Main Point
    typeEnum:{carParks:'CarParks',travelNews:'TravelNews',stops:'Stops',pointX:'PointX',serverGraphic:'ServerGraphic'}
  },
  toolBar:{
    typeEnum:{defaultTool:'default',userDefinedLocation:'userDefined',selectNearbyPoint:'selectNearby'}
  },
  filter:{
    standard:{roadIncidentsVisible:false,publicIncidentsVisible:false,incidentType:"all",severity:"all",timePeriod:"current",datetime:"3/9/2009 17:7:54"}
  },
  style:{
    wholePageMessage:' Whole page map needs the following CSS: html,body{padding:0px;margin:0px;width:100%;height:100%;overflow:hidden} '
  },
  requiredCSSFiles:requiredCSSFiles,
  //  add ArcGIS Server Map Services here
  mapServiceTypeEnum:mapServiceTypeEnum,
  mapServices: [ esriukTDPSettings
    //  Examples:
    //  { baseMap:true, url: "http://[YOUR_SERVER]/arcgis/rest/services/[YOUR_SERVICE]/MapServer", wkid: 27700, type: this.mapServiceTypeEnum.tiled },
    //  { baseMap:false, url:"http://[YOUR_SERVER]/arcgis/rest/services/[YOUR_SERVICE]/MapServer", wkid: 27700, type: this.mapServiceTypeEnum.dynamic }
  ],
  //  add web services / rest services here
  webServiceTypeEnum:webServiceTypeEnum,
  webServices: [ esriukTDPQuery
    //  Examples:
    //  { name: this.webServiceTypeEnum.geom, geometype: this.webServiceTypeEnum.geom, url:'http://[YOUR_SERVER]/ArcGIS/rest/services/Geometry/GeometryServer'},
    //  { name: 'featureQueryService1', type: this.webServiceTypeEnum.feat, url:'http://[YOUR_SERVER]/[YOUR_SERVICE]'}
  ],
  live:liveEnvironment,
  
  mixinStatics:function(){
  
    var arr = ['travelNewsEnum','planAJourneyText','misc','errors','mapNavPanel','mapToolbarPanel'];
    //  acquire localisation
    dojo.requireLocalization("ESRIUK", "Config", null, "ROOT,cy,en,en-gb,en-us");

    //  get handle on localisation settigns
    var loc = dojo.i18n.getLocalization("ESRIUK", "Config"),
        settings = this.statics;
        
    for(var i=0;i<arr.length;i++){
      dojo.mixin(settings[arr[i]],loc[arr[i]])
    }
  },
  
  constructor:function(){
    //  mixin input arguments
    dojo.mixin(this,arguments[0]);
    if(dojo.locale==='cy'){    
      this.mixinStatics();
    }
    //  assumed
    //  dojo.require("ESRIUK.Util");
    //util = ESRIUK.util();
  }
};
})());
// summary: the config singleton variable
ESRIUK._config = null;
ESRIUK.config = function(obj){
  //  summary: 
  //    returns the current Config, creates one if it is not created yet
  //  
  if(ESRIUK._config===null){
    ESRIUK._config = typeof obj==="object"? new ESRIUK.Config(obj) : new ESRIUK.Config();
  }
  return ESRIUK._config;
};
