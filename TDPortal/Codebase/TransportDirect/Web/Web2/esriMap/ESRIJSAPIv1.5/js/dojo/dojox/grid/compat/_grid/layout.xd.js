/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.grid.compat._grid.layout"],["require","dojox.grid.compat._grid.cell"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.grid.compat._grid.layout"]){_4._hasResource["dojox.grid.compat._grid.layout"]=true;_4.provide("dojox.grid.compat._grid.layout");_4.require("dojox.grid.compat._grid.cell");_4.declare("dojox.grid.layout",null,{constructor:function(_7){this.grid=_7;},cells:[],structure:null,defaultWidth:"6em",setStructure:function(_8){this.fieldIndex=0;this.cells=[];var s=this.structure=[];for(var i=0,_b,_c;(_b=_8[i]);i++){s.push(this.addViewDef(_b));}this.cellCount=this.cells.length;},addViewDef:function(_d){this._defaultCellProps=_d.defaultCell||{};return _4.mixin({},_d,{rows:this.addRowsDef(_d.rows||_d.cells)});},addRowsDef:function(_e){var _f=[];for(var i=0,row;_e&&(row=_e[i]);i++){_f.push(this.addRowDef(i,row));}return _f;},addRowDef:function(_12,_13){var _14=[];for(var i=0,def,_17;(def=_13[i]);i++){_17=this.addCellDef(_12,i,def);_14.push(_17);this.cells.push(_17);}return _14;},addCellDef:function(_18,_19,_1a){var w=0;if(_1a.colSpan>1){w=0;}else{if(!isNaN(_1a.width)){w=_1a.width+"em";}else{w=_1a.width||this.defaultWidth;}}var _1c=_1a.field!=undefined?_1a.field:(_1a.get?-1:this.fieldIndex);if((_1a.field!=undefined)||!_1a.get){this.fieldIndex=(_1a.field>-1?_1a.field:this.fieldIndex)+1;}return new _6.grid.cell(_4.mixin({},this._defaultCellProps,_1a,{grid:this.grid,subrow:_18,layoutIndex:_19,index:this.cells.length,fieldIndex:_1c,unitWidth:w}));}});}}};});