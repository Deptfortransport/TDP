/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.embed.flashVars"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.embed.flashVars"]){_4._hasResource["dojox.embed.flashVars"]=true;_4.provide("dojox.embed.flashVars");_4.mixin(_6.embed.flashVars,{serialize:function(n,o){var _9=function(_a){if(typeof _a=="string"){_a=_a.replace(/;/g,"_sc_");_a=_a.replace(/\./g,"_pr_");_a=_a.replace(/\:/g,"_cl_");}return _a;};var df=_6.embed.flashVars.serialize;var _c="";if(_4.isArray(o)){for(var i=0;i<o.length;i++){_c+=df(n+"."+i,_9(o[i]))+";";}return _c.replace(/;{2,}/g,";");}else{if(_4.isObject(o)){for(var nm in o){_c+=df(n+"."+nm,_9(o[nm]))+";";}return _c.replace(/;{2,}/g,";");}}return n+":"+o;}});}}};});