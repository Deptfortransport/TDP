/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.wire.TextAdapter"],["require","dojox.wire.CompositeWire"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.wire.TextAdapter"]){_4._hasResource["dojox.wire.TextAdapter"]=true;_4.provide("dojox.wire.TextAdapter");_4.require("dojox.wire.CompositeWire");_4.declare("dojox.wire.TextAdapter",_6.wire.CompositeWire,{_wireClass:"dojox.wire.TextAdapter",constructor:function(_7){this._initializeChildren(this.segments);if(!this.delimiter){this.delimiter="";}},_getValue:function(_8){if(!_8||!this.segments){return _8;}var _9="";for(var i in this.segments){var _b=this.segments[i].getValue(_8);_9=this._addSegment(_9,_b);}return _9;},_setValue:function(_c,_d){throw new Error("Unsupported API: "+this._wireClass+"._setValue");},_addSegment:function(_e,_f){if(!_f){return _e;}else{if(!_e){return _f;}else{return _e+this.delimiter+_f;}}}});}}};});