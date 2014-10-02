/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._base.typematic"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._base.typematic"]){_4._hasResource["dijit._base.typematic"]=true;_4.provide("dijit._base.typematic");_5.typematic={_fireEventAndReload:function(){this._timer=null;this._callback(++this._count,this._node,this._evt);this._currentTimeout=(this._currentTimeout<0)?this._initialDelay:((this._subsequentDelay>1)?this._subsequentDelay:Math.round(this._currentTimeout*this._subsequentDelay));this._timer=setTimeout(_4.hitch(this,"_fireEventAndReload"),this._currentTimeout);},trigger:function(_7,_8,_9,_a,_b,_c,_d){if(_b!=this._obj){this.stop();this._initialDelay=_d||500;this._subsequentDelay=_c||0.9;this._obj=_b;this._evt=_7;this._node=_9;this._currentTimeout=-1;this._count=-1;this._callback=_4.hitch(_8,_a);this._fireEventAndReload();}},stop:function(){if(this._timer){clearTimeout(this._timer);this._timer=null;}if(this._obj){this._callback(-1,this._node,this._evt);this._obj=null;}},addKeyListener:function(_e,_f,_10,_11,_12,_13){if(_f.keyCode){_f.charOrCode=_f.keyCode;_4.deprecated("keyCode attribute parameter for dijit.typematic.addKeyListener is deprecated. Use charOrCode instead.","","2.0");}else{if(_f.charCode){_f.charOrCode=String.fromCharCode(_f.charCode);_4.deprecated("charCode attribute parameter for dijit.typematic.addKeyListener is deprecated. Use charOrCode instead.","","2.0");}}return [_4.connect(_e,"onkeypress",this,function(evt){if(evt.charOrCode==_f.charOrCode&&(_f.ctrlKey===undefined||_f.ctrlKey==evt.ctrlKey)&&(_f.altKey===undefined||_f.altKey==evt.ctrlKey)&&(_f.shiftKey===undefined||_f.shiftKey==evt.ctrlKey)){_4.stopEvent(evt);_5.typematic.trigger(_f,_10,_e,_11,_f,_12,_13);}else{if(_5.typematic._obj==_f){_5.typematic.stop();}}}),_4.connect(_e,"onkeyup",this,function(evt){if(_5.typematic._obj==_f){_5.typematic.stop();}})];},addMouseListener:function(_16,_17,_18,_19,_1a){var dc=_4.connect;return [dc(_16,"mousedown",this,function(evt){_4.stopEvent(evt);_5.typematic.trigger(evt,_17,_16,_18,_16,_19,_1a);}),dc(_16,"mouseup",this,function(evt){_4.stopEvent(evt);_5.typematic.stop();}),dc(_16,"mouseout",this,function(evt){_4.stopEvent(evt);_5.typematic.stop();}),dc(_16,"mousemove",this,function(evt){_4.stopEvent(evt);}),dc(_16,"dblclick",this,function(evt){_4.stopEvent(evt);if(_4.isIE){_5.typematic.trigger(evt,_17,_16,_18,_16,_19,_1a);setTimeout(_4.hitch(this,_5.typematic.stop),50);}})];},addListener:function(_21,_22,_23,_24,_25,_26,_27){return this.addKeyListener(_22,_23,_24,_25,_26,_27).concat(this.addMouseListener(_21,_24,_25,_26,_27));}};}}};});