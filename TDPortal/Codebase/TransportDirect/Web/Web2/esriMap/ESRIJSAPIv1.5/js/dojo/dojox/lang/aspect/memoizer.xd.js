/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.aspect.memoizer"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.aspect.memoizer"]){_4._hasResource["dojox.lang.aspect.memoizer"]=true;_4.provide("dojox.lang.aspect.memoizer");(function(){var _7=_6.lang.aspect;var _8={around:function(_9){var _a=_7.getContext(),_b=_a.joinPoint,_c=_a.instance,t,u,_f;if((t=_c.__memoizerCache)&&(t=t[_b.targetName])&&(_9 in t)){return t[_9];}var _f=_7.proceed.apply(null,arguments);if(!(t=_c.__memoizerCache)){t=_c.__memoizerCache={};}if(!(u=t[_b.targetName])){u=t[_b.targetName]={};}return u[_9]=_f;}};var _10=function(_11){return {around:function(){var ctx=_7.getContext(),_13=ctx.joinPoint,_14=ctx.instance,t,u,ret,key=_11.apply(_14,arguments);if((t=_14.__memoizerCache)&&(t=t[_13.targetName])&&(key in t)){return t[key];}var ret=_7.proceed.apply(null,arguments);if(!(t=_14.__memoizerCache)){t=_14.__memoizerCache={};}if(!(u=t[_13.targetName])){u=t[_13.targetName]={};}return u[key]=ret;}};};_7.memoizer=function(_19){return arguments.length==0?_8:_10(_19);};})();}}};});