/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.io.xhrWindowNamePlugin"],["require","dojox.io.xhrPlugins"],["require","dojox.io.windowName"],["require","dojox.io.httpParse"],["require","dojox.secure.capability"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.io.xhrWindowNamePlugin"]){_4._hasResource["dojox.io.xhrWindowNamePlugin"]=true;_4.provide("dojox.io.xhrWindowNamePlugin");_4.require("dojox.io.xhrPlugins");_4.require("dojox.io.windowName");_4.require("dojox.io.httpParse");_4.require("dojox.secure.capability");_6.io.xhrWindowNamePlugin=function(_7,_8,_9){_6.io.xhrPlugins.register("windowName",function(_a,_b){return _b.sync!==true&&(_a=="GET"||_a=="POST"||_8)&&(_b.url.substring(0,_7.length)==_7);},function(_c,_d,_e){var _f=_6.io.windowName.send;var dfd=(_8?_8(_f,true):_f)(_c,_d,_e);dfd.addCallback(function(_11){var _12=dfd.ioArgs;_12.xhr={getResponseHeader:function(_13){return _4.queryToObject(_12.hash.match(/[^#]*$/)[0])[_13];}};if(_12.handleAs=="json"){if(!_9){_6.secure.capability.validate(_11,["Date"],{});}return _4.fromJson(_11);}return _4._contentHandlers[_12.handleAs||"text"]({responseText:_11});});return dfd;});};}}};});