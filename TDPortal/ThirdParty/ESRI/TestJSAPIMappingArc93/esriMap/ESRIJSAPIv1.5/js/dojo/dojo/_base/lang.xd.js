/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo._base.lang"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base.lang"]){_4._hasResource["dojo._base.lang"]=true;_4.provide("dojo._base.lang");_4.isString=function(it){return !!arguments.length&&it!=null&&(typeof it=="string"||it instanceof String);};_4.isArray=function(it){return it&&(it instanceof Array||typeof it=="array");};_4.isFunction=(function(){var _9=function(it){var t=typeof it;return it&&(t=="function"||it instanceof Function);};return _4.isSafari?function(it){if(typeof it=="function"&&it=="[object NodeList]"){return false;}return _9(it);}:_9;})();_4.isObject=function(it){return it!==undefined&&(it===null||typeof it=="object"||_4.isArray(it)||_4.isFunction(it));};_4.isArrayLike=function(it){var d=_4;return it&&it!==undefined&&!d.isString(it)&&!d.isFunction(it)&&!(it.tagName&&it.tagName.toLowerCase()=="form")&&(d.isArray(it)||isFinite(it.length));};_4.isAlien=function(it){return it&&!_4.isFunction(it)&&/\{\s*\[native code\]\s*\}/.test(String(it));};_4.extend=function(_11,_12){for(var i=1,l=arguments.length;i<l;i++){_4._mixin(_11.prototype,arguments[i]);}return _11;};_4._hitchArgs=function(_15,_16){var pre=_4._toArray(arguments,2);var _18=_4.isString(_16);return function(){var _19=_4._toArray(arguments);var f=_18?(_15||_4.global)[_16]:_16;return f&&f.apply(_15||this,pre.concat(_19));};};_4.hitch=function(_1b,_1c){if(arguments.length>2){return _4._hitchArgs.apply(_4,arguments);}if(!_1c){_1c=_1b;_1b=null;}if(_4.isString(_1c)){_1b=_1b||_4.global;if(!_1b[_1c]){throw (["dojo.hitch: scope[\"",_1c,"\"] is null (scope=\"",_1b,"\")"].join(""));}return function(){return _1b[_1c].apply(_1b,arguments||[]);};}return !_1b?_1c:function(){return _1c.apply(_1b,arguments||[]);};};_4.delegate=_4._delegate=(function(){function TMP(){};return function(obj,_1f){TMP.prototype=obj;var tmp=new TMP();if(_1f){_4._mixin(tmp,_1f);}return tmp;};})();(function(){var _21=function(obj,_23,_24){return (_24||[]).concat(Array.prototype.slice.call(obj,_23||0));};var _25=function(obj,_27,_28){var arr=_28||[];for(var x=_27||0;x<obj.length;x++){arr.push(obj[x]);}return arr;};_4._toArray=_4.isIE?function(obj){return ((obj.item)?_25:_21).apply(this,arguments);}:_21;})();_4.partial=function(_2c){var arr=[null];return _4.hitch.apply(_4,arr.concat(_4._toArray(arguments)));};_4.clone=function(o){if(!o){return o;}if(_4.isArray(o)){var r=[];for(var i=0;i<o.length;++i){r.push(_4.clone(o[i]));}return r;}if(!_4.isObject(o)){return o;}if(o.nodeType&&o.cloneNode){return o.cloneNode(true);}if(o instanceof Date){return new Date(o.getTime());}r=new o.constructor();for(i in o){if(!(i in r)||r[i]!=o[i]){r[i]=_4.clone(o[i]);}}return r;};_4.trim=String.prototype.trim?function(str){return str.trim();}:function(str){return str.replace(/^\s\s*/,"").replace(/\s\s*$/,"");};}}};});