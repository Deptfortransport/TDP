/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.utils"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.utils"]){_4._hasResource["dojox.lang.utils"]=true;_4.provide("dojox.lang.utils");(function(){var _7={},du=_6.lang.utils;var _9=function(o){if(_4.isArray(o)){return _4._toArray(o);}if(!_4.isObject(o)||_4.isFunction(o)){return o;}return _4.delegate(o);};_4.mixin(du,{coerceType:function(_b,_c){switch(typeof _b){case "number":return Number(eval("("+_c+")"));case "string":return String(_c);case "boolean":return Boolean(eval("("+_c+")"));}return eval("("+_c+")");},updateWithObject:function(_d,_e,_f){if(!_e){return _d;}for(var x in _d){if(x in _e&&!(x in _7)){var t=_d[x];if(t&&typeof t=="object"){du.updateWithObject(t,_e[x],_f);}else{_d[x]=_f?du.coerceType(t,_e[x]):_9(_e[x]);}}}return _d;},updateWithPattern:function(_12,_13,_14,_15){if(!_13||!_14){return _12;}for(var x in _14){if(x in _13&&!(x in _7)){_12[x]=_15?du.coerceType(_14[x],_13[x]):_9(_13[x]);}}return _12;}});})();}}};});