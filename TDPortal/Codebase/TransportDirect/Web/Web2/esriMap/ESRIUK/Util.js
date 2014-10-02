/**
*   @copyright: ESRI (UK) 2009
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: July 2009
*   @description:
*     Static class to access variety of util functions throughout the application
*     This class can be accessed throughout the application via:
*       //  reference class/file
*       dojo.require('ESRIUK.Util');
*       //  get handle on instance of util
*       var util = ESRIUK.util();
*       //  call it's methods
*       util.isString(unknown);
*   @note:
*     One section below is attributed to Douglas Crockford
*     http://javascript.crockford.com/remedial.html
*     No copyright notice provided with this code
*
*   @updates:
*
*/
dojo.provide("ESRIUK.Util");
dojo.declare("ESRIUK.Util", null, (function(){
  /**
  *   @class: ESRIUK.Util
  *
  *   @capability: This class is capable of logging
  *     Picked up in url = ?debug=[0|1|2|3|4|5]
  *     0 = no logging (or just no debug parameter)
  *     5 = all logging
  *     logging to be viewerd in firebug console
  */

  //  START: Externally sourced functions
  /**
  *   @functions:
  *     typeOf isEmpty, isEmpty,String.prototype.entityify, String.prototype.quote, 
  *     String.prototype.supplant, String.prototype.trim
  *   @description: Remedial JavaScript functions taken from Douglas Crockford
  *   @copyright: No copyright defined - therefore none propogated
  *   @source: http://javascript.crockford.com/remedial.html
  */
  var typeOf=function(value){var s=typeof value;if(s==='object'){if(value){if(typeof value.length==='number'&&!(value.propertyIsEnumerable('length'))&&typeof value.splice==='function'){s='array';}}else{s='null';}}return s;};
  var isEmpty=function(o){var i,v;if(typeOf(o)==='object'){for(i in o){v=o[i];if(v!==undefined&&typeOf(v)!=='function'){return false;}}}return true;};
  String.prototype.entityify=function(){return this.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;");};
  String.prototype.quote=function(){var c,i,l=this.length,o='"';for(i=0;i<l;i+=1){c=this.charAt(i);if(c>=' '){if(c==='\\'||c==='"'){o+='\\';}o+=c;}else{switch(c){case'\b':o+='\\b';break;case'\f':o+='\\f';break;case'\n':o+='\\n';break;case'\r':o+='\\r';break;case'\t':o+='\\t';break;default:c=c.charCodeAt();o+='\\u00'+Math.floor(c/16).toString(16)+(c%16).toString(16);}}}return o+'"';};
  String.prototype.supplant=function(o){return this.replace(/{([^{}]*)}/g,function(a,b){var r=o[b];return typeof r==='string'||typeof r==='number'?r:a;});};
  String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g,"");}; 
  //  END: Externally sourced functions
  String.prototype.camelCase=function(){var s=trim(this);return(/\S[A-Z]/.test(s))?s.replace(/(.)([A-Z])/g,function(t,a,b){return a+' '+b.toLowerCase();}):s.replace(/( )([a-z])/g,function(t,a,b){return b.toUpperCase();});};
  
  return {
  _logLevel:0,
  degrees:"<span class='degrees'>&deg;</span>",
  isValid:false,
  jsonESRITemplate:{
    result:{
      geometryType:"",
      displayFieldName:"",
      fieldAliases:null,
      spatialReference:null,
      features:[]
    }
  },
  //  START: CONSTRUCTOR
  constructor:function(){ 
    this.isValid = true;
    //  prevent console errors in IE
    if(this.isNothing(window.console)){
      this.setupDummyConsole();
    }
    //  read url debug param
    this._getURLParams();
    if(this._logLevel<1){
      //  bind to empty function
      var fcn = function(){ return false; };
      this.logMessage = this.logDebug = this.logInfo = this.logWarning = this.logError = fcn;
    }else{
      //  bind stubs
      this.log = this.logMessage;
      this.debug = this.logDebug;
      this.info = this.logInfo;
      this.warn = this.logWarning;
      this.error = this.logError;
    }
  },
  //  END: CONSTRUCTOR
  //  START: TYPE METHODS
  isNothing:function(){
    if(arguments.length<1){ return true; }
    var isNothing = function(obj){ return obj === undefined || obj === null; };
    for(var i=0;i<arguments.length;i++){
      if(!isNothing(arguments[0])){ return false; }
    }
    return true;
  },
  isBoolean:function(){
    if(arguments.length<1){ return false; }    
    var isBool = function(obj){ return typeOf(obj)==='boolean'; };
    for(var i=0;i<arguments.length;i++){
      if(!isBool(arguments[0])){ return false; }
    }
    return true;
  },
  isObject:function(){
    if(arguments.length<1){ return false; }    
    var isObject = function(obj){ return typeOf(obj)==='object'; };
    for(var i=0;i<arguments.length;i++){
      if(!isObject(arguments[0])){ return false; }
    }
    return true;
  },    
  isString:function(){
    if(arguments.length<1){ return false; }    
    var isString = function(obj){ return typeOf(obj)==='string'; };
    for(var i=0;i<arguments.length;i++){
      if(!isString(arguments[0])){ return false; }
    }
    return true;
  },
  isArray:function(){
    if(arguments.length<1){ return false; }
    var isArray = function(obj){ return typeOf(obj)==='array'; };
    for(var i=0;i<arguments.length;i++){
      if(!isArray(arguments[0])){ return false; }
    }
    return true;
  },
  isNumber:function(){
    if(arguments.length<1){ return false; }    
    var that = this;
    var isNumber = function(obj){
      if(!that.isNothing(obj)){
        if(typeof obj==="string"){
          if(isNaN(obj)){ return false; }
          else{ return true; }
        }else if(typeof obj==="number"){
          return true;
        }else{
          return false;
        }
      }else{
        return false;
      }
    };
    for(var i=0;i<arguments.length;i++){
      if(!isNumber(arguments[i])){ return false; }
    }
    return true;
  },    
  isStringPopulated:function(/*object*/obj){ return this.isString(obj)&&obj.length>0; },
  isArrayPopulated:function(/*object*/obj){ return this.isArray(obj)&&obj.length>0; },
  parseBool:function(/*object*/obj){
    if(this.isBoolean(obj)){
      return obj;
    }else{
      if(this.isString(obj)){
        return (obj.toLowerCase()==="true");
      }else{
        return false;
      }
    }
  },
  parseNumber:function(/*object*/obj){
    if(this.isNumber(obj)){
      return obj*1;
    }else{
      this.warn(this.declaredClass,arguments.callee.nom,' Could not parse number ',obj);
    }
  },
  isValidXY:function(/*object*/obj){
    if(this.isObject(obj)&&this.isNumber(obj.x,obj.y)){
      return true;
    }
    return false;
  },
  isValidWH:function(obj){
    if(this.isNumber(obj.w,obj.h)){
      return true;
    }
    return false;
  },
  isValidExt:function(/*object*/obj){
    if(this.isObject(obj)&&this.isNumber(obj.xmin,obj.ymin,obj.xmax,obj.ymax)){
      return true;
    }
    return false;
  },
  validateXY:function(/*object*/obj){
    if(this.isString(obj.x)){ obj.x = parseFloat(obj.x); }
    if(this.isString(obj.y)){ obj.y = parseFloat(obj.y); }
    return obj;
  },
  validateExt:function(/*Object*/ext){
    if(typeof ext.xmin === 'string'){ ext.xmin = parseFloat(ext.xmin); }
    if(typeof ext.ymin === 'string'){ ext.ymin = parseFloat(ext.ymin); }
    if(typeof ext.xmax === 'string'){ ext.xmax = parseFloat(ext.xmax); }
    if(typeof ext.ymax === 'string'){ ext.ymax = parseFloat(ext.ymax); }
    return ext;
  },
  //  END: TYPE METHODS
  getElmGutterVertical:function(/*DOMElement*/elm){
    var returnVar = 0;
    returnVar += dojo.style(elm,'paddingLeft');
    returnVar += dojo.style(elm,'paddingRight');
    return returnVar;
  },
  getElmGutterHorizontal:function(/*DOMElement*/elm){
    var returnVar = 0;
    returnVar += dojo.style(elm,'paddingTop');
    returnVar += dojo.style(elm,'paddingBottom');
    return returnVar;
  },
  getST_GeometryExtent:function(/*extent*/ext){
    var x = ext.toJson();
    return x.xmin+' '+x.ymin+', '+x.xmin+' '+x.ymax+', '+x.xmax+' '+x.ymax+', '+x.xmax+' '+x.ymin+','+x.xmin+' '+x.ymin;
  },
  getESRIJsonExtent:function(/*extent*/ext){
    var returnVar = ext.toJson();
    returnVar.spatialReference = "";
    return returnVar;
  },
  getValidJSONResponse:function(data){
    var response = this.jsonESRITemplate.result;
    try{
      var rData = data.result || undefined;
      if(typeof rData ==="string"){
        rData = dojo.fromJson(data.result);
      }else if(typeof rData === 'object' && typeof rData.features ==='object' && typeof rData.features.length === "number"){
        response = rData;
        if(typeof rData.displayFieldName !== 'string'){
          response.displayFieldName = '';
        }
        if(typeof rData.geometryType!=='string'){
          response.geometryType = '';
        }
        if(typeof rData.fieldAliases !== 'object'){
          response.fieldAliases = null;
        }
        if(typeof rData.spatialReference !== 'object'){
          response.spatialReference = null;
        }
      }else{
        console.warn('defrasdit.dijits.getValidJSONResponse - ',data);
      }
    }catch(err){
      console.warn('defrasdit.dijits.getValidJSONResponse - ',err," - ",data);
    }
    return response;
  },
  getValidGraphic:function(obj){
    var graphic = null;
    var x = obj.x || null;
    var y = obj.y || null;
    var paths = obj.paths || null;
    var rings = obj.rings || null;
    var spRef = obj.spatialReference;
    if (x && y) {
      graphic = new esri.geometry.Point([x, y], new esri.SpatialReference(spRef));
    } else if (paths) {
      graphic = new esri.geometry.Polyline(obj);
    } else if (rings) {
      graphic = new esri.geometry.Polygon(obj);
    }
    return graphic;
  },
  getPtExtent:function(pt){
    return new esri.geometry.Extent(pt.x,pt.y,pt.x,pt.y,pt.spatialReference);      
  },
  formatConnect:function(conn,name){
    return {handle:conn,name:name};
  },
  arguments2Array:function(obj){
    return Array.prototype.slice.call(obj);
  },
  //  START: LOGGING
  setupDummyConsole:function(){
    window.console={
      log:function(){ return false; },
      debug:function(){ return false; },
      warn:function(){ return false; },
      info:function(){ return false; },
      error:function(){ return false; }
    };
  },
  logMessage:function(){ if(this._logLevel>4){ console.log(arguments); } },
  logDebug:function(){ if(this._logLevel>3){ console.debug(arguments); } },
  logInfo:function(){ if(this._logLevel>2){ console.info(arguments); } },
  logWarning:function(){ if(this._logLevel>1){ console.warn(arguments); } },
  logError:function(){ if(this._logLevel>0){ console.error(Array.prototype.slice.call(arguments)); } },
  //  stubs
  log:function(){ return false; },  //  log level 5
  debug:function(){ return false; },  //  log level 4
  info:function(){ return false; },  //  log level 3
  warn:function(){ return false; },  //  log level 2
  error:function(){ return false; },  //  log level 1
  _getURLParams:function(){
    try{
      if(this.isStringPopulated(window.location.search)){
        // convert our query string into an object (use slice to strip the leading "?")
        var params = dojo.queryToObject(window.location.search.slice(1));
        if(this.isNumber(params.debug)){ this._setDebugLevel(parseInt(params.debug)); }
      }
    }catch(err){
      this.log(this.declaredClass,arguments.callee.nom,err,window.location);
    }
  },
  _setDebugLevel:function(obj){
    if(obj>-1&&obj<6){
      this._logLevel = obj*1;
    }
  }
  //  END: LOGGING    
}
})());
ESRIUK._util = null;
ESRIUK.util = function(){
  if(ESRIUK._util===null){
    ESRIUK._util = new ESRIUK.Util();
  }
  return ESRIUK._util;
};
