/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.rpc.JsonRPC"],["require","dojox.rpc.Service"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.rpc.JsonRPC"]){_4._hasResource["dojox.rpc.JsonRPC"]=true;_4.provide("dojox.rpc.JsonRPC");_4.require("dojox.rpc.Service");(function(){function _7(_8){return {serialize:function(_9,_a,_b,_c){var d={id:this._requestId++,method:_a.name,params:_b};if(_8){d.jsonrpc=_8;}return {data:_4.toJson(d),handleAs:"json",contentType:"application/json",transport:"POST"};},deserialize:function(_e){if("Error"==_e.name){_e=_4.fromJson(_e.responseText);}if(_e.error){var e=new Error(_e.error.message||_e.error);e._rpcErrorObject=_e.error;return e;}return _e.result;}};};_6.rpc.envelopeRegistry.register("JSON-RPC-1.0",function(str){return str=="JSON-RPC-1.0";},_4.mixin({namedParams:false},_7()));_6.rpc.envelopeRegistry.register("JSON-RPC-2.0",function(str){return str=="JSON-RPC-2.0";},_7("2.0"));})();}}};});