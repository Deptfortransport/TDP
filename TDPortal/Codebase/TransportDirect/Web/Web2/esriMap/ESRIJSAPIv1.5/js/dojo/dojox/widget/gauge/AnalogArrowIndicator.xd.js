/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.widget.gauge.AnalogArrowIndicator"],["require","dojox.widget.AnalogGauge"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.widget.gauge.AnalogArrowIndicator"]){_4._hasResource["dojox.widget.gauge.AnalogArrowIndicator"]=true;_4.provide("dojox.widget.gauge.AnalogArrowIndicator");_4.require("dojox.widget.AnalogGauge");_4.experimental("dojox.widget.gauge.AnalogArrowIndicator");_4.declare("dojox.widget.gauge.AnalogArrowIndicator",[_6.widget.gauge.AnalogLineIndicator],{_getShapes:function(){if(!this._gauge){return null;}var x=Math.floor(this.width/2);var _8=this.width*5;var _9=(this.width&1);var _a=[];var _b=[{x:-x,y:0},{x:-x,y:-this.length+_8},{x:-2*x,y:-this.length+_8},{x:0,y:-this.length},{x:2*x+_9,y:-this.length+_8},{x:x+_9,y:-this.length+_8},{x:x+_9,y:0},{x:-x,y:0}];_a[0]=this._gauge.surface.createPolyline(_b).setStroke({color:this.color}).setFill(this.color);_a[1]=this._gauge.surface.createLine({x1:-x,y1:0,x2:-x,y2:-this.length+_8}).setStroke({color:this.highlight});_a[2]=this._gauge.surface.createLine({x1:-x-3,y1:-this.length+_8,x2:0,y2:-this.length}).setStroke({color:this.highlight});_a[3]=this._gauge.surface.createCircle({cx:0,cy:0,r:this.width}).setStroke({color:this.color}).setFill(this.color);return _a;}});}}};});