/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.io.scriptFrame"],["require","dojo.io.script"],["require","dojo.io.iframe"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.io.scriptFrame"]){_4._hasResource["dojox.io.scriptFrame"]=true;_4.provide("dojox.io.scriptFrame");_4.require("dojo.io.script");_4.require("dojo.io.iframe");(function(){var _7=_4.io.script;_6.io.scriptFrame={_waiters:{},_loadedIds:{},_getWaiters:function(_8){return this._waiters[_8]||(this._waiters[_8]=[]);},_fixAttachUrl:function(_9){},_loaded:function(_a){var _b=this._getWaiters(_a);this._loadedIds[_a]=true;this._waiters[_a]=null;for(var i=0;i<_b.length;i++){var _d=_b[i];_d.frameDoc=_4.io.iframe.doc(_4.byId(_a));_7.attach(_d.id,_d.url,_d.frameDoc);}}};var _e=_7._canAttach;var _f=_6.io.scriptFrame;_7._canAttach=function(_10){var fId=_10.args.frameDoc;if(fId&&_4.isString(fId)){var _12=_4.byId(fId);var _13=_f._getWaiters(fId);if(!_12){_13.push(_10);_4.io.iframe.create(fId,_6._scopeName+".io.scriptFrame._loaded('"+fId+"');");}else{if(_f._loadedIds[fId]){_10.frameDoc=_4.io.iframe.doc(_12);this.attach(_10.id,_10.url,_10.frameDoc);}else{_13.push(_10);}}return false;}else{return _e.apply(this,arguments);}};})();}}};});