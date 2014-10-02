/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.functional.object"],["require","dojox.lang.functional.lambda"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.functional.object"]){_4._hasResource["dojox.lang.functional.object"]=true;_4.provide("dojox.lang.functional.object");_4.require("dojox.lang.functional.lambda");(function(){var d=_4,df=_6.lang.functional,_9={};d.mixin(df,{keys:function(_a){var t=[];for(var i in _a){if(!(i in _9)){t.push(i);}}return t;},values:function(_d){var t=[];for(var i in _d){if(!(i in _9)){t.push(_d[i]);}}return t;},filterIn:function(obj,f,o){o=o||d.global;f=df.lambda(f);var t={},v,i;for(i in obj){if(!(i in _9)){v=obj[i];if(f.call(o,v,i,obj)){t[i]=v;}}}return t;},forIn:function(obj,f,o){o=o||d.global;f=df.lambda(f);for(var i in obj){if(!(i in _9)){f.call(o,obj[i],i,obj);}}return o;},mapIn:function(obj,f,o){o=o||d.global;f=df.lambda(f);var t={},i;for(i in obj){if(!(i in _9)){t[i]=f.call(o,obj[i],i,obj);}}return t;}});})();}}};});