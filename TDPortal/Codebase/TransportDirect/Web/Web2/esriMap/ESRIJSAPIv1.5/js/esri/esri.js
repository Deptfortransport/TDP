/*
 COPYRIGHT 2009 ESRI

 TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
 Unpublished material - all rights reserved under the
 Copyright Laws of the United States and applicable international
 laws, treaties, and conventions.

 For additional information, contact:
 Environmental Systems Research Institute, Inc.
 Attn: Contracts and Legal Services Department
 380 New York Street
 Redlands, California, 92373
 USA

 email: contracts@esri.com
 */

dojo.registerModulePath("esri",djConfig.esriJSAPI+"js/esri");dojo.mixin(typeof esri=="undefined"?window.esri={}:esri,{version:1.5,config:{defaults:{screenDPI:96,map:{width:400,height:400,layerNamePrefix:"layer",graphicsLayerNamePrefix:"graphicsLayer",slider:{left:"30px",top:"30px",width:null,height:"200px"},sliderLabel:{tick:5,labels:null,style:"width:2em; font-family:Verdana; font-size:75%;"},sliderChangeImmediate:true,zoomSymbol:{color:[0,0,0,64],outline:{color:[255,0,0,255],width:1.5,style:"esriSLSSolid"},style:"esriSFSSolid"},zoomDuration:250,zoomRate:25,panDuration:250,panRate:25,logoLink:"http://www.esri.com/javascript"},io:{errorHandler:function(_1,io){dojo.publish("esri.Error",[_1]);},proxyUrl:null,alwaysUseProxy:false,postLength:2000,timeout:60000}}}});esriConfig=esri.config;(function(){var h=document.getElementsByTagName("head")[0],_4=[dojo.moduleUrl("esri","../../css/jsapi.css"),dojo.moduleUrl("esri","dijit/css/InfoWindow.css")],_5={rel:"stylesheet",type:"text/css",media:"all"};dojo.forEach(_4,function(_6){_5.href=_6;dojo.create("link",_5,h);});})();
