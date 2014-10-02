/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.wire.CompositeWire"],["require","dojox.wire._base"],["require","dojox.wire.Wire"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.wire.CompositeWire"]){_4._hasResource["dojox.wire.CompositeWire"]=true;_4.provide("dojox.wire.CompositeWire");_4.require("dojox.wire._base");_4.require("dojox.wire.Wire");_4.declare("dojox.wire.CompositeWire",_6.wire.Wire,{_wireClass:"dojox.wire.CompositeWire",constructor:function(_7){this._initializeChildren(this.children);},_getValue:function(_8){if(!_8||!this.children){return _8;}var _9=(_4.isArray(this.children)?[]:{});for(var c in this.children){_9[c]=this.children[c].getValue(_8);}return _9;},_setValue:function(_b,_c){if(!_b||!this.children){return _b;}for(var c in this.children){this.children[c].setValue(_c[c],_b);}return _b;},_initializeChildren:function(_e){if(!_e){return;}for(var c in _e){var _10=_e[c];_10.parent=this;if(!_6.wire.isWire(_10)){_e[c]=_6.wire.create(_10);}}}});}}};});