/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.analytics.plugins.mouseClick"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.analytics.plugins.mouseClick"]){_4._hasResource["dojox.analytics.plugins.mouseClick"]=true;_4.provide("dojox.analytics.plugins.mouseClick");_6.analytics.plugins.mouseClick=new (function(){this.addData=_4.hitch(_6.analytics,"addData","mouseClick");this.onClick=function(e){this.addData(this.trimEvent(e));};_4.connect(_4.doc,"onclick",this,"onClick");this.trimEvent=function(e){var t={};for(var i in e){switch(i){case "target":case "originalTarget":case "explicitOriginalTarget":var _b=["id","className","nodeName","localName","href","spellcheck","lang"];t[i]={};for(var j=0;j<_b.length;j++){if(e[i][_b[j]]){if(_b[j]=="text"||_b[j]=="textContent"){if((e[i]["localName"]!="HTML")&&(e[i]["localName"]!="BODY")){t[i][_b[j]]=e[i][_b[j]].substr(0,50);}}else{t[i][_b[j]]=e[i][_b[j]];}}}break;case "clientX":case "clientY":case "pageX":case "pageY":case "screenX":case "screenY":t[i]=e[i];break;}}return t;};})();}}};});