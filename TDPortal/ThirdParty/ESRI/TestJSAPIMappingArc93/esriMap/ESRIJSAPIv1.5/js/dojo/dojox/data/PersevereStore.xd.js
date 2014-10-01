/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.PersevereStore"],["require","dojox.data.JsonQueryRestStore"],["require","dojox.rpc.Client"],["require","dojox.io.xhrScriptPlugin"],["require","dojox.io.xhrPlugins"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.PersevereStore"]){_4._hasResource["dojox.data.PersevereStore"]=true;_4.provide("dojox.data.PersevereStore");_4.require("dojox.data.JsonQueryRestStore");_4.require("dojox.rpc.Client");_6.json.ref.serializeFunctions=true;_4.declare("dojox.data.PersevereStore",_6.data.JsonQueryRestStore,{useFullIdInQueries:true,jsonQueryPagination:false});_6.data.PersevereStore.getStores=function(_7,_8){_7=(_7&&(_7.match(/\/$/)?_7:(_7+"/")))||"/";if(_7.match(/^\w*:\/\//)){_4.require("dojox.io.xhrScriptPlugin");_6.io.xhrScriptPlugin(_7,"callback",_6.io.xhrPlugins.fullHttpAdapter);}var _9=_4.xhr;_4.xhr=function(_a,_b){(_b.headers=_b.headers||{})["Server-Methods"]=false;return _9.apply(_4,arguments);};var _c=_6.rpc.Rest(_7,true);_6.rpc._sync=_8;var _d=_c("Class/");var _e;var _f={};var _10=0;_d.addCallback(function(_11){_6.json.ref.resolveJson(_11,{index:_6.rpc.Rest._index,idPrefix:"/Class/",assignAbsoluteIds:true});function _12(_13){if(_13["extends"]&&_13["extends"].prototype){if(!_13.prototype||!_13.prototype.isPrototypeOf(_13["extends"].prototype)){_12(_13["extends"]);_6.rpc.Rest._index[_13.prototype.__id]=_13.prototype=_4.mixin(_4.delegate(_13["extends"].prototype),_13.prototype);}}};function _14(_15,_16){if(_15&&_16){for(var j in _15){var _18=_15[j];if(_18.runAt=="server"&&!_16[j]){_16[j]=(function(_19){return function(){var _1a=_4.rawXhrPost({url:this.__id,postData:_4.toJson({method:_19,id:_10++,params:_4._toArray(arguments)}),handleAs:"json"});_1a.addCallback(function(_1b){return _1b.error?new Error(_1b.error):_1b.result;});return _1a;};})(j);}}}};for(var i in _11){if(typeof _11[i]=="object"){var _1d=_11[i];_12(_1d);_14(_1d.methods,_1d.prototype=_1d.prototype||{});_14(_1d.staticMethods,_1d);_f[_11[i].id]=new _6.data.PersevereStore({target:new _4._Url(_7,_11[i].id)+"",schema:_1d});}}return (_e=_f);});_4.xhr=_9;return _8?_e:_d;};_6.data.PersevereStore.addProxy=function(){_4.require("dojox.io.xhrPlugins");_6.io.xhrPlugins.addProxy("/proxy/");};}}};});