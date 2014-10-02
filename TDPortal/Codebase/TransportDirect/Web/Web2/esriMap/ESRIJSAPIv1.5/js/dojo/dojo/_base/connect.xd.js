/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo._base.connect"],["require","dojo._base.lang"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base.connect"]){_4._hasResource["dojo._base.connect"]=true;_4.provide("dojo._base.connect");_4.require("dojo._base.lang");_4._listener={getDispatcher:function(){return function(){var ap=Array.prototype,c=arguments.callee,ls=c._listeners,t=c.target;var r=t&&t.apply(this,arguments);var _c;_c=[].concat(ls);for(var i in _c){if(!(i in ap)){_c[i].apply(this,arguments);}}return r;};},add:function(_e,_f,_10){_e=_e||_4.global;var f=_e[_f];if(!f||!f._listeners){var d=_4._listener.getDispatcher();d.target=f;d._listeners=[];f=_e[_f]=d;}return f._listeners.push(_10);},remove:function(_13,_14,_15){var f=(_13||_4.global)[_14];if(f&&f._listeners&&_15--){delete f._listeners[_15];}}};_4.connect=function(obj,_18,_19,_1a,_1b){var a=arguments,_1d=[],i=0;_1d.push(_4.isString(a[0])?null:a[i++],a[i++]);var a1=a[i+1];_1d.push(_4.isString(a1)||_4.isFunction(a1)?a[i++]:null,a[i++]);for(var l=a.length;i<l;i++){_1d.push(a[i]);}return _4._connect.apply(this,_1d);};_4._connect=function(obj,_22,_23,_24){var l=_4._listener,h=l.add(obj,_22,_4.hitch(_23,_24));return [obj,_22,h,l];};_4.disconnect=function(_27){if(_27&&_27[0]!==undefined){_4._disconnect.apply(this,_27);delete _27[0];}};_4._disconnect=function(obj,_29,_2a,_2b){_2b.remove(obj,_29,_2a);};_4._topics={};_4.subscribe=function(_2c,_2d,_2e){return [_2c,_4._listener.add(_4._topics,_2c,_4.hitch(_2d,_2e))];};_4.unsubscribe=function(_2f){if(_2f){_4._listener.remove(_4._topics,_2f[0],_2f[1]);}};_4.publish=function(_30,_31){var f=_4._topics[_30];if(f){f.apply(this,_31||[]);}};_4.connectPublisher=function(_33,obj,_35){var pf=function(){_4.publish(_33,arguments);};return (_35)?_4.connect(obj,_35,pf):_4.connect(obj,pf);};}}};});