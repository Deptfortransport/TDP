/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.dtl.Context"],["require","dojox.dtl._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.dtl.Context"]){_4._hasResource["dojox.dtl.Context"]=true;_4.provide("dojox.dtl.Context");_4.require("dojox.dtl._base");_6.dtl.Context=_4.extend(function(_7){this._this={};_6.dtl._Context.call(this,_7);},_6.dtl._Context.prototype,{getKeys:function(){var _8=[];for(var _9 in this){if(this.hasOwnProperty(_9)&&_9!="_dicts"&&_9!="_this"){_8.push(_9);}}return _8;},extend:function(_a){return _4.delegate(this,_a);},filter:function(_b){var _c=new _6.dtl.Context();var _d=[];var i,_f;if(_b instanceof _6.dtl.Context){_d=_b.getKeys();}else{if(typeof _b=="object"){for(var key in _b){_d.push(key);}}else{for(i=0;_f=arguments[i];i++){if(typeof _f=="string"){_d.push(_f);}}}}for(i=0,key;key=_d[i];i++){_c[key]=this[key];}return _c;},setThis:function(_11){this._this=_11;},getThis:function(){return this._this;},hasKey:function(key){if(typeof this[key]!="undefined"){return true;}for(var i=0,_14;_14=this._dicts[i];i++){if(typeof _14[key]!="undefined"){return true;}}return false;}});}}};});