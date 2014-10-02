/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.wire.TreeAdapter"],["require","dojox.wire.CompositeWire"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.wire.TreeAdapter"]){_4._hasResource["dojox.wire.TreeAdapter"]=true;_4.provide("dojox.wire.TreeAdapter");_4.require("dojox.wire.CompositeWire");_4.declare("dojox.wire.TreeAdapter",_6.wire.CompositeWire,{_wireClass:"dojox.wire.TreeAdapter",constructor:function(_7){this._initializeChildren(this.nodes);},_getValue:function(_8){if(!_8||!this.nodes){return _8;}var _9=_8;if(!_4.isArray(_9)){_9=[_9];}var _a=[];for(var i in _9){for(var i2 in this.nodes){_a=_a.concat(this._getNodes(_9[i],this.nodes[i2]));}}return _a;},_setValue:function(_d,_e){throw new Error("Unsupported API: "+this._wireClass+"._setValue");},_initializeChildren:function(_f){if(!_f){return;}for(var i in _f){var _11=_f[i];if(_11.node){_11.node.parent=this;if(!_6.wire.isWire(_11.node)){_11.node=_6.wire.create(_11.node);}}if(_11.title){_11.title.parent=this;if(!_6.wire.isWire(_11.title)){_11.title=_6.wire.create(_11.title);}}if(_11.children){this._initializeChildren(_11.children);}}},_getNodes:function(_12,_13){var _14=null;if(_13.node){_14=_13.node.getValue(_12);if(!_14){return [];}if(!_4.isArray(_14)){_14=[_14];}}else{_14=[_12];}var _15=[];for(var i in _14){_12=_14[i];var _17={};if(_13.title){_17.title=_13.title.getValue(_12);}else{_17.title=_12;}if(_13.children){var _18=[];for(var i2 in _13.children){_18=_18.concat(this._getNodes(_12,_13.children[i2]));}if(_18.length>0){_17.children=_18;}}_15.push(_17);}return _15;}});}}};});