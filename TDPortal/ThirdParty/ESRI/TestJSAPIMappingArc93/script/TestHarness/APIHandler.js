
var ESRIUKTDPAPI = {
  useAsStartPoint:function(x,y,text){ 
    //  summary:
    //  UC 1.4 - User Defined Location - Use as start of journey
    //  replace inner function code here to link to external code
    alert('Start Point: \n'+x + ' \n'+ y+'\n '+text); 
  },    
  useAsViaPoint:function(x,y,text){ 
    //  summary:
    //  UC 1.4 - User Defined Location - Use as journey via pt
    //  replace inner function code here to link to external code
    alert('Via Point: \n'+x + ' \n'+ y+'\n '+text); 
  },        
  useAsEndPoint:function(x,y,text){ 
    //  summary:
    //  UC 1.4 - User Defined Location - Use as end of journey
    //  replace inner function code here to link to external code  
    alert('End Point: \n'+x + ' \n'+ y+'\n '+text); 
  },
  showCarParkInformation:function(id){
    //  summary:
    //  UC 1.1 - Client-side graphics - Car Parks Layer
    //  replace inner function code here to link to external code 
    alert('Car Park Id: '+id);
  },
  showStopsInformation:function(id){
    //  summary:
    //  UC 1.1 - Client-side graphics - Stops Layer
    //  replace inner function code here to link to external code 
    alert('Stops id: '+id);
  },
  selectNearbyPointResult:function(data){ 
    //  summary:
    //    UC 1.4 - Select nearby point - raw data sent to harness
    //    data consisits of an array of click Points, point x etc...
    //    replace inner function code here to link to external code  
    //    example link to external handler
    alert(dojo.toJson(data,true));      
  },
  miscError:function(/*Object*/error){
    //alert(error.message);
  },
  queryError:function(/*Object*/error,/*response*/response){
    //alert(dojo.toJson(error));
  },
  mapError:function(/*Object*/error,/*response*/response){
    alert(dojo.toJson(error)+dojo.toJson(response));
  },
  mapGraphicsCount: function(/*Number*/num){
    //  summary:
    //    Called on each response from the Query Extent method
    //  params:
    //    num (number) : number of graphics returned (-1 = number of graphics exceeded)
    if(num==-1){ alert('Too many graphics at this scale'); }
  },
  onMapExtentChange: function(/*esri.geometry.Extent*/extent,/*boolean*/levelChange,/*integer*/scale,/*string*/ovURL){
    //  summary:
    //    Event fired when the map extent is changed (pan, zoom etc)
    //  params:
    //    extent = esri.geometry.Extent - object with properties xmax, xmin, ymax, ymin
    //      reference: http://resources.esri.com/help/9.3/arcgisserver/apis/javascript/arcgis/help/jsapi_start.htm#jsapi/extent.htm
    //    levelChange = boolean - indicates if the map scale level has changed
    //    scale = integer - value reflecting scale of map e.g. 50000
    //    ovUrl = string - representing the new url fo the overview image
    
    
    
    //  This is an example of using the map parameters to update another element on the page
    //  here we use the overview url to show the overview map & parameters
     //  only on the basic map test page
     if(location.href.indexOf('Test_BasicMap.aspx')>-1){
          var id = 'myOvImage';
          var elm = dojo.byId(id);
          if(!elm){
            elm = dojo.create('img',{id:id,style:'z-index:1000;position:absolute;left:780px;top:28px;border:1px solid gray;'},document.body);
          }
          elm.src = ovURL;
          
          var _id = "mySpan";
          elm = dojo.byId(_id);
          if(!elm){
            elm = dojo.create('span',{id:_id,style:'z-index:1000;position:absolute;left:780px;border:1px solid gray;'},document.body);
          }
          elm.innerHTML = 'levelChange = '+levelChange+', scale = '+scale;
     }
  },
  onMapInitialiseComplete:function(/*ESRIUK.Dijits.Map*/map){
    //  summary:
    //    Event fired when the map and map components have completed their loading cycles
    //   map:
    //    The map in question
    
    //alert(map.id);
  }
};
