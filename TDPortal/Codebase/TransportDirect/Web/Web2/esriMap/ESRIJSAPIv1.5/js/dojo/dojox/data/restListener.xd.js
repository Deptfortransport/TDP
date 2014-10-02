/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.restListener"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.restListener"]){_4._hasResource["dojox.data.restListener"]=true;_4.provide("dojox.data.restListener");_6.data.restListener=function(_7){var _8=_7.channel;var jr=_6.rpc.JsonRest;var _a=jr.getServiceAndId(_8).service;var _b=_6.json.ref.resolveJson(_7.result,{defaultId:_7.event=="put"&&_8,index:_6.rpc.Rest._index,idPrefix:_a.servicePath,idAttribute:jr.getIdAttribute(_a),schemas:jr.schemas,loader:jr._loader,assignAbsoluteIds:true});var _c=_6.rpc.Rest._index&&_6.rpc.Rest._index[_8];var _d="on"+_7.event.toLowerCase();var _e=_a&&_a._store;if(_c){if(_c[_d]){_c[_d](_b);return;}}if(_e){switch(_d){case "onpost":_e.onNew(_b);break;case "ondelete":_e.onDelete(_c);break;}}};}}};});