/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo._base._loader.loader_debug"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base._loader.loader_debug"]){_4._hasResource["dojo._base._loader.loader_debug"]=true;_4.provide("dojo._base._loader.loader_debug");_4.nonDebugProvide=_4.provide;_4.provide=function(_7){var _8=_4["_xdDebugQueue"];if(_8&&_8.length>0&&_7==_8["currentResourceName"]){if(_4.isAIR){window.setTimeout(function(){_4._xdDebugFileLoaded(_7);},1);}else{window.setTimeout(_4._scopeName+"._xdDebugFileLoaded('"+_7+"')",1);}}return _4.nonDebugProvide.apply(_4,arguments);};_4._xdDebugFileLoaded=function(_9){if(!this._xdDebugScopeChecked){if(_4._scopeName!="dojo"){window.dojo=window[_4.config.scopeMap[0][1]];window.dijit=window[_4.config.scopeMap[1][1]];window.dojox=window[_4.config.scopeMap[2][1]];}this._xdDebugScopeChecked=true;}var _a=this._xdDebugQueue;if(_9&&_9==_a.currentResourceName){_a.shift();}if(_a.length==0){_4._xdWatchInFlight();}if(_a.length==0){_a.currentResourceName=null;for(var _b in this._xdInFlight){if(this._xdInFlight[_b]===true){return;}}this._xdNotifyLoaded();}else{if(_9==_a.currentResourceName){_a.currentResourceName=_a[0].resourceName;var _c=document.createElement("script");_c.type="text/javascript";_c.src=_a[0].resourcePath;document.getElementsByTagName("head")[0].appendChild(_c);}}};}}};});