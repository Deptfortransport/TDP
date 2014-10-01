dojo.require("dojo.parser");
dojo.require("dijit.Menu");
dojo.require("dijit.Dialog");
dojo.require("dijit.Tooltip");
dojo.require("dijit.form.Form");
dojo.require("dijit.form.Button");
dojo.require("dijit.form.CheckBox");
dojo.require("dijit.form.TextBox");
dojo.require("dijit.form.FilteringSelect");
dojo.require("dijit.layout.ContentPane");
dojo.require("dijit.layout.BorderContainer");

var ESRIUKTestHarness = {
  mapDijit: "esriukTDPMap",
  alerts:false,
  hasLayerList:false,
  layerState:null,
  //destroyMap: function(){
    //dijit.byId(ESRIUKTestHarness.mapDijit).destroyRecursive();
  //},
/*   addNewMap: function(){
    //  attempt to find previous element
    var elm = document.getElementById(ESRIUKTestHarness.mapDijit);
    //  check if element has been destroyed by the above method
    if(elm===null){
        //  create empty div to use as base for new map object
        var div = document.createElement('div');
        //  get reference to the container element in which to place map
        var container = document.getElementById('dijit_layout_ContentPane_1');
        //  add empty div to container
        container.appendChild(div);
        //  create new map
        var map = new ESRIUK.Dijits.Map({
                //  set map initialisation parameters
                id:ESRIUKTestHarness.mapDijit,
                param_Width:776,
                param_Height:540
            },
            div  // use empty div as base element for new map object
        );
        //  must call startup method
        map.startup();
    }else{
        console.warn(' element has not been destroyed before a call to recreate is made ');
    }
  }, */
  setActiveTool: function(str){
    dijit.byId(ESRIUKTestHarness.mapDijit).setActiveTool(str);
  },
  setScale: function() {
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToScale(dijit.byId("setScaleBox").attr("value"));
  },
  setZoom: function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToLevel(dijit.byId("setZoomBox").attr("value")-1);
  },
  zoomToLocation: function() {
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToXY(
      dijit.byId('setXBox').attr('value'),
      dijit.byId('setYBox').attr('value')
    );
  },
  zoomToLocationAndScale:function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToScaleAndPoint(
        dijit.byId('setXBox').attr('value'),
        dijit.byId('setYBox').attr('value'),
        dijit.byId("setScaleBox").attr("value")
    );
  },
  zoomToLocationAndZoom: function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToLevelAndPoint(
        dijit.byId('setXBox').attr('value'),
        dijit.byId('setYBox').attr('value'),
        dijit.byId("setZoomBox").attr("value")-1
    );
  },
  zoomToLocationAndScaleText: function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToScaleAndPoint(
        dijit.byId('setXBox').attr('value'),
        dijit.byId('setYBox').attr('value'),
        dijit.byId("setScaleBox").attr("value"),
        dijit.byId("setLocationTextBox").attr("value")
    );
  },
  zoomToLocationAndLevelText: function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToLevelAndPoint(
        dijit.byId('setXBox').attr('value'),
        dijit.byId('setYBox').attr('value'),
        dijit.byId("setZoomBox").attr("value")-1,
        dijit.byId("setLocationTextBox").attr("value")
    );
  },
  zoomToEnvelope: function() {
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToExtent(
      dijit.byId('setMinXBox').attr('value'),
      dijit.byId('setMinYBox').attr('value'),
      dijit.byId('setMaxXBox').attr('value'),
      dijit.byId('setMaxYBox').attr('value'),
      true  //  fit extent into map view
    );
  },
  findJunctionPoint: function(){
     var pt = dijit.byId(ESRIUKTestHarness.mapDijit).findJunctionPoint(
        dijit.byId('junctionToidA').attr('value'),
        dijit.byId('junctionToidB').attr('value'),
        dijit.byId('junctionToidLevel').attr('value'),
        true
     );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(pt,true));},2000);
    }
  },
  findITNNodePoint: function(){
     var pt = dijit.byId(ESRIUKTestHarness.mapDijit).findITNNodePoint(
        dijit.byId('itnToid').attr('value'),
        dijit.byId('itnLevel').attr('value'),
        true
     );
      if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(pt,true));},2000);
    }
  },
  zoomToPTRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).zoomToRoute(
        dijit.byId('ptRouteSessionID').attr('value'),
        dijit.byId('ptRouteRouteNumber').attr('value'),
        'PT'
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  addPTRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).addRoute(
        dijit.byId('ptRouteSessionID').attr('value'),
        dijit.byId('ptRouteRouteNumber').attr('value'),
        "PT"
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  clearPTRoutes:function(){
    var bool = dijit.byId(ESRIUKTestHarness.mapDijit).clearRoutes('PT');
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert('Success: '+bool);},2000);    
    }
  },
  zoomToRoadRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).zoomToRoute(
        dijit.byId('roadRouteSessionID').attr('value'),
        dijit.byId('roadRouteRouteNumber').attr('value'),
        'Road'
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  addRoadRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).addRoute(
        dijit.byId('roadRouteSessionID').attr('value'),
        dijit.byId('roadRouteRouteNumber').attr('value'),
        "Road"
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  clearRoadRoutes:function(){
    var bool = dijit.byId(ESRIUKTestHarness.mapDijit).clearRoutes('Road');
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert('Success: '+bool);},2000);    
    }
  },  
  zoomToCycleRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).zoomToRoute(
        dijit.byId('cycleRouteSessionID').attr('value'),
        dijit.byId('cycleRouteRouteNumber').attr('value'),
        'Cycle'
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  addCycleRoute:function(){
    var extent = dijit.byId(ESRIUKTestHarness.mapDijit).addRoute(
        dijit.byId('cycleRouteSessionID').attr('value'),
        dijit.byId('cycleRouteRouteNumber').attr('value'),
        "Cycle"
    );
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert(dojo.toJson(extent,true));},2000);    
    }
  },
  clearCycleRoutes:function(){
    var bool = dijit.byId(ESRIUKTestHarness.mapDijit).clearRoutes('Cycle');
     if(ESRIUKTestHarness.alerts){
        setTimeout(function(){alert('Success: '+bool);},2000);    
    }
  },   
  
  getPrintImage:function(){
    var height = dijit.byId('printImageHeight').attr('value')*1;
    var width = dijit.byId('printImageWidth').attr('value')*1;
    var url = dijit.byId(ESRIUKTestHarness.mapDijit).getPrintImage(
            width,
            height,
            dijit.byId('printImageDPI').attr('value')
        );

    if(typeof url==='string'){
        window.open(url,null,'width='+(width+25)+',height='+(height+50)+',left=0,top=100,screenX=0,screenY=100');
        return;
        
        
        //  Caused IE to crash !!
        var diag = new dijit.Dialog({
            title:"Print Page",
            content:'<a title="Click on image to open it in a new window." href="'+url+'"><img src="'+url+'" alt="Print image" style="width:'+width+'px;height:'+height+'px;" /></a>',
            style:{
                width:(width+25)+"px",
                height:(height+50)+"px"
            }
        }).show();
    }    
  },
  getNumberOfCycleImages:function(){
    var val = dijit.byId(ESRIUKTestHarness.mapDijit).getNumberOfCycleImages(
        dijit.byId('cyclePrintScale').attr('value')
    );
    setTimeout(function(){alert(dojo.toJson(val,true));},2000);    
  },
  getCyclePrintDetails:function(){
    var obj = dijit.byId(ESRIUKTestHarness.mapDijit).getCyclePrintDetails(
        dijit.byId('cyclePrintScale').attr('value')
    );
    setTimeout(function(){alert(dojo.toJson(obj,true));},2000);    
  },
  setTravelNewsFilter: function(){
    var obj = this.getValues();
    dijit.byId(ESRIUKTestHarness.mapDijit).setTravelNews({
        transportType:obj.tType,
        incidentType:obj.incidentType,
        severity:obj.severity,
        timePeriod:obj.timePeriod,
        datetime:obj.travelNewsDate        
    });
    return false;
  },
  addMarker: function(){
      var type = '', infoWindow = false, content = '', main = false;
      if(dijit.byId("markerStart").attr('checked')){
        type = 'start';
      }else if(dijit.byId("markerVia").attr('checked')){
        type = 'via';
      }else if(dijit.byId("markerEnd").attr('checked')){
        type = 'end';
      }else if(dijit.byId("markerSymbol").attr('checked')){
        type = 'symbol';
      }
      if(dijit.byId('setMarkerAddInfoWindow').attr('checked')){
        infoWindow = true;
        content = dijit.byId('setMarkerContent').attr('value'); 
      }
      if(dijit.byId('setMarkerMain').attr('checked')){
        main = true;
      }
      dijit.byId(ESRIUKTestHarness.mapDijit).addStartEndPt({
          x: dijit.byId('setMarkerXBox').attr('value'),
          y: dijit.byId('setMarkerYBox').attr('value'),
          type: type, infoWindowRequired: infoWindow, content: content, main: main,
          label: dijit.byId('setMarkerTextBox').attr('value'),
          symbolKey: type==='symbol'? dijit.byId('setMarkerSymbolType').attr('value') : ''
      });
  },
  clearPoints:function(){
      dijit.byId(ESRIUKTestHarness.mapDijit).clearPoints();
  },
  getLayerList: function(){
        if(ESRIUKTestHarness.hasLayerList){ return; }
        var list = dijit.byId(ESRIUKTestHarness.mapDijit).getLayerList(),
            mainContainer = dojo.byId('layerListContent'),
            container = null;      
            
        while(mainContainer.firstChild){ mainContainer.removeChild(mainContainer.firstChild); }

        //  add carparks layer
        var div = mainContainer.appendChild(document.createElement('div'));
        var obj = {id: "Carparks", name: "Carparks", onClick:ESRIUKTestHarness.toggleLayer};
        if(list.carparkLayerVisible){ obj = dojo.mixin(obj,{checked:'checked'}); }
        new dijit.form.CheckBox(obj, dojo.create('input',null,div));    
        dojo.create('label',{innerHTML:"Carparks"},div);   
        //  add stops layer list
        var stops = list.stops;
        container = dojo.create('div',{innerHTML:'Stops Layers',style:'padding:2px;border:1px solid #CCCCCC; margin:2px'},mainContainer);
        for(var i=0;i<stops.length;i++){
            var item = stops[i];
            var div = container.appendChild(document.createElement('div'));
            var obj = {name: "Stops"+item.name, onClick:ESRIUKTestHarness.toggleLayer};
            if(item.visible){ obj = dojo.mixin(obj,{checked:'checked'}); }
            new dijit.form.CheckBox(obj, dojo.create('input',null,div));    
            dojo.create('label',{innerHTML:item.name},div);   
        } 
        //  add pointX layer list     
        var ptX = list.pointX;
        container = dojo.create('div',{innerHTML:'PointX Layers',style:'padding:2px;border:1px solid #CCCCCC; margin:2px'},mainContainer);
        for(var i=0;i<ptX.length;i++){
            var item = ptX[i];
            var div = container.appendChild(document.createElement('div'));
            var obj = {name: "PointX"+item.name, onClick:ESRIUKTestHarness.toggleLayer};
            if(item.visible){ obj = dojo.mixin(obj,{checked:'checked'}); }
            new dijit.form.CheckBox(obj, dojo.create('input',null,div));
            dojo.create('label',{innerHTML:item.name},div);          
        }
        ESRIUKTestHarness.hasLayerList = true;
        ESRIUKTestHarness.layerState = list;
        
        if(dojo.isIE){
            dojo.style(mainContainer,{'overflow':'scroll'});
        }
        
  },
  setLayerList: function(){
    dijit.byId(ESRIUKTestHarness.mapDijit).setLayerState(ESRIUKTestHarness.layerState);
  },
  toggleLayer: function(elm){
    if(this.name.indexOf('PointX')>-1){
        var name = this.name.split('PointX')[1];
        for(var i=0;i<ESRIUKTestHarness.layerState.pointX.length;i++){
            if(name==ESRIUKTestHarness.layerState.pointX[i].name){
                ESRIUKTestHarness.layerState.pointX[i].visible = this.checked;
                return;
            }
        }
    }else if(this.name.indexOf('Stops')>-1){
        var name = this.name.split('Stops')[1];
        for(var i=0;i<ESRIUKTestHarness.layerState.stops.length;i++){
            if(name==ESRIUKTestHarness.layerState.stops[i].name){
                ESRIUKTestHarness.layerState.stops[i].visible = this.checked;
                return;
            }
        }
    }else if(this.name=="Carparks"){
        ESRIUKTestHarness.layerState.carparkLayerVisible = this.checked;
    }
    //dijit.byId(ESRIUKTestHarness.mapDijit).toggleLayer(this.name,this.checked); 
  },
  
  /*  START: Unused Functions */
  zoomPrevious: function() {
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToPreviousExtent();
  },
  zoomFullExtent: function() {
    dijit.byId(ESRIUKTestHarness.mapDijit).zoomToFullExtent();
  },
  identify: function() {
    var id = dijit.byId(ESRIUKTestHarness.mapDijit).isIdentify();
    dijit.byId(ESRIUKTestHarness.mapDijit).setIdentify((id ? false : true));
  }
  /*  END: Unused Functions */  
};
