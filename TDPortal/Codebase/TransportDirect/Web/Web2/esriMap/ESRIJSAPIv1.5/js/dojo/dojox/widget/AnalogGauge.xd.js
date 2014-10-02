/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.widget.AnalogGauge"],["require","dojox.gfx"],["require","dojox.widget.gauge._Gauge"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.widget.AnalogGauge"]){_4._hasResource["dojox.widget.AnalogGauge"]=true;_4.provide("dojox.widget.AnalogGauge");_4.require("dojox.gfx");_4.require("dojox.widget.gauge._Gauge");_4.experimental("dojox.widget.AnalogGauge");_4.declare("dojox.widget.gauge.AnalogLineIndicator",[_6.widget.gauge._Indicator],{_getShapes:function(){var _7=[];_7[0]=this._gauge.surface.createLine({x1:0,y1:-this.offset,x2:0,y2:-this.length-this.offset}).setStroke({color:this.color,width:this.width});return _7;},draw:function(_8){if(this.shapes){this._move(_8);}else{if(this.text){this._gauge.surface.rawNode.removeChild(this.text);this.text=null;}var v=this.value;if(v<this._gauge.min){v=this._gauge.min;}if(v>this._gauge.max){v=this._gauge.max;}var a=this._gauge._getAngle(v);this.color=this.color||"#000000";this.length=this.length||this._gauge.radius;this.width=this.width||1;this.offset=this.offset||0;this.highlight=this.highlight||"#D0D0D0";this.shapes=this._getShapes(this._gauge,this);if(this.shapes){for(var s=0;s<this.shapes.length;s++){this.shapes[s].setTransform([{dx:this._gauge.cx,dy:this._gauge.cy},_6.gfx.matrix.rotateg(a)]);if(this.hover){this.shapes[s].getEventSource().setAttribute("hover",this.hover);}if(this.onDragMove&&!this.noChange){this._gauge.connect(this.shapes[s].getEventSource(),"onmousedown",this._gauge.handleMouseDown);this.shapes[s].getEventSource().style.cursor="pointer";}}}if(this.label){var _c=this.length+this.offset;var x=this._gauge.cx+(_c+5)*Math.sin(this._gauge._getRadians(a));var y=this._gauge.cy-(_c+5)*Math.cos(this._gauge._getRadians(a));var _f="start";if(a<=-10){_f="end";}if(a>-10&&a<10){_f="middle";}var _10="bottom";if((a<-90)||(a>90)){_10="top";}this.text=this._gauge.drawText(""+this.label,x,y,_f,_10,this.color,this.font);}this.currentValue=this.value;}},_move:function(_11){var v=this.value;if(v<this._gauge.min){v=this._gauge.min;}if(v>this._gauge.max){v=this._gauge.max;}var c=this.currentValue;if(_11){var _14=this._gauge._getAngle(v);for(var i in this.shapes){this.shapes[i].setTransform([{dx:this._gauge.cx,dy:this._gauge.cy},_6.gfx.matrix.rotateg(_14)]);if(this.hover){this.shapes[i].getEventSource().setAttribute("hover",this.hover);}}}else{if(c!=v){var _16=new _4._Animation({curve:[c,v],duration:this.duration,easing:this.easing});_4.connect(_16,"onAnimate",_4.hitch(this,function(_17){for(var i in this.shapes){this.shapes[i].setTransform([{dx:this._gauge.cx,dy:this._gauge.cy},_6.gfx.matrix.rotateg(this._gauge._getAngle(_17))]);if(this.hover){this.shapes[i].getEventSource().setAttribute("hover",this.hover);}}this.currentValue=_17;}));_16.play();}}}});_4.declare("dojox.widget.AnalogGauge",_6.widget.gauge._Gauge,{startAngle:-90,endAngle:90,cx:0,cy:0,radius:0,_defaultIndicator:_6.widget.gauge.AnalogLineIndicator,startup:function(){if(this.getChildren){_4.forEach(this.getChildren(),function(_19){_19.startup();});}this.startAngle=Number(this.startAngle);this.endAngle=Number(this.endAngle);this.cx=Number(this.cx);if(!this.cx){this.cx=this.width/2;}this.cy=Number(this.cy);if(!this.cy){this.cy=this.height/2;}this.radius=Number(this.radius);if(!this.radius){this.radius=Math.min(this.cx,this.cy)-25;}this._oppositeMiddle=(this.startAngle+this.endAngle)/2+180;this.inherited(arguments);},_getAngle:function(_1a){return (_1a-this.min)/(this.max-this.min)*(this.endAngle-this.startAngle)+this.startAngle;},_getValueForAngle:function(_1b){if(_1b>this._oppositeMiddle){_1b-=360;}return (_1b-this.startAngle)*(this.max-this.min)/(this.endAngle-this.startAngle)+this.min;},_getRadians:function(_1c){return _1c*Math.PI/180;},_getDegrees:function(_1d){return _1d*180/Math.PI;},draw:function(){var i;if(this._rangeData){for(i=0;i<this._rangeData.length;i++){this.drawRange(this._rangeData[i]);}if(this._img&&this.image.overlay){this._img.moveToFront();}}if(this._indicatorData){for(i=0;i<this._indicatorData.length;i++){this._indicatorData[i].draw();}}},drawRange:function(_1f){var _20;if(_1f.shape){this.surface.remove(_1f.shape);_1f.shape=null;}var a1;var a2;if((_1f.low==this.min)&&(_1f.high==this.max)&&((this.endAngle-this.startAngle)==360)){_20=this.surface.createCircle({cx:this.cx,cy:this.cy,r:this.radius});}else{a1=this._getRadians(this._getAngle(_1f.low));a2=this._getRadians(this._getAngle(_1f.high));var x1=this.cx+this.radius*Math.sin(a1);var y1=this.cy-this.radius*Math.cos(a1);var x2=this.cx+this.radius*Math.sin(a2);var y2=this.cy-this.radius*Math.cos(a2);var big=0;if((a2-a1)>Math.PI){big=1;}_20=this.surface.createPath();if(_1f.size){_20.moveTo(this.cx+(this.radius-_1f.size)*Math.sin(a1),this.cy-(this.radius-_1f.size)*Math.cos(a1));}else{_20.moveTo(this.cx,this.cy);}_20.lineTo(x1,y1);_20.arcTo(this.radius,this.radius,0,big,1,x2,y2);if(_1f.size){_20.lineTo(this.cx+(this.radius-_1f.size)*Math.sin(a2),this.cy-(this.radius-_1f.size)*Math.cos(a2));_20.arcTo((this.radius-_1f.size),(this.radius-_1f.size),0,big,0,this.cx+(this.radius-_1f.size)*Math.sin(a1),this.cy-(this.radius-_1f.size)*Math.cos(a1));}_20.closePath();}if(_4.isArray(_1f.color)||_4.isString(_1f.color)){_20.setStroke({color:_1f.color});_20.setFill(_1f.color);}else{if(_1f.color.type){a1=this._getRadians(this._getAngle(_1f.low));a2=this._getRadians(this._getAngle(_1f.high));_1f.color.x1=this.cx+(this.radius*Math.sin(a1))/2;_1f.color.x2=this.cx+(this.radius*Math.sin(a2))/2;_1f.color.y1=this.cy-(this.radius*Math.cos(a1))/2;_1f.color.y2=this.cy-(this.radius*Math.cos(a2))/2;_20.setFill(_1f.color);_20.setStroke({color:_1f.color.colors[0].color});}else{_20.setStroke({color:"green"});_20.setFill("green");_20.getEventSource().setAttribute("class",_1f.color.style);}}if(_1f.hover){_20.getEventSource().setAttribute("hover",_1f.hover);}_1f.shape=_20;},getRangeUnderMouse:function(_28){var _29=null;var pos=_4.coords(this.gaugeContent);var x=_28.clientX-pos.x;var y=_28.clientY-pos.y;var r=Math.sqrt((y-this.cy)*(y-this.cy)+(x-this.cx)*(x-this.cx));if(r<this.radius){var _2e=this._getDegrees(Math.atan2(y-this.cy,x-this.cx)+Math.PI/2);var _2f=this._getValueForAngle(_2e);if(this._rangeData){for(var i=0;(i<this._rangeData.length)&&!_29;i++){if((Number(this._rangeData[i].low)<=_2f)&&(Number(this._rangeData[i].high)>=_2f)){_29=this._rangeData[i];}}}}return _29;},_dragIndicator:function(_31,_32){var pos=_4.coords(_31.gaugeContent);var x=_32.clientX-pos.x;var y=_32.clientY-pos.y;var _36=_31._getDegrees(Math.atan2(y-_31.cy,x-_31.cx)+Math.PI/2);var _37=_31._getValueForAngle(_36);if(_37<_31.min){_37=_31.min;}if(_37>_31.max){_37=_31.max;}_31._drag.value=_37;_31._drag.currentValue=_37;_31._drag.onDragMove(_31._drag);_31._drag.draw(true);_4.stopEvent(_32);}});}}};});