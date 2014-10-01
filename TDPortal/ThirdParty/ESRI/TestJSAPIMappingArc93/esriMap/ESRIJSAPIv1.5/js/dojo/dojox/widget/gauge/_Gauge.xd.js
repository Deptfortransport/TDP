/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.widget.gauge._Gauge"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dijit._Container"],["require","dijit._Contained"],["require","dijit.Tooltip"],["require","dojo.fx.easing"],["require","dojox.gfx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.widget.gauge._Gauge"]){_4._hasResource["dojox.widget.gauge._Gauge"]=true;_4.provide("dojox.widget.gauge._Gauge");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dijit._Container");_4.require("dijit._Contained");_4.require("dijit.Tooltip");_4.require("dojo.fx.easing");_4.require("dojox.gfx");_4.experimental("dojox.widget.gauge._Gauge");_4.declare("dojox.widget.gauge._Gauge",[_5._Widget,_5._Templated,_5._Container],{width:0,height:0,background:null,min:0,max:0,image:null,useRangeStyles:0,useTooltip:true,majorTicks:null,minorTicks:null,_defaultIndicator:null,defaultColors:[[0,84,170,1],[68,119,187,1],[102,153,204,1],[153,187,238,1],[153,204,255,1],[204,238,255,1],[221,238,255,1]],min:null,max:null,surface:null,hideValues:false,gaugeContent:undefined,templateString:"<div>\r\n\t<div class=\"dojoxGaugeContent\" dojoAttachPoint=\"gaugeContent\"></div>\r\n\t<div dojoAttachPoint=\"containerNode\"></div>\r\n\t<div dojoAttachPoint=\"mouseNode\"></div>\r\n</div>\r\n",_backgroundDefault:{color:"#E0E0E0"},_rangeData:null,_indicatorData:null,_drag:null,_img:null,_overOverlay:false,_lastHover:"",startup:function(){if(this.image===null){this.image={};}this.connect(this.gaugeContent,"onmousemove",this.handleMouseMove);this.connect(this.gaugeContent,"onmouseover",this.handleMouseOver);this.connect(this.gaugeContent,"onmouseout",this.handleMouseOut);this.connect(this.gaugeContent,"onmouseup",this.handleMouseUp);if(!_4.isArray(this.ranges)){this.ranges=[];}if(!_4.isArray(this.indicators)){this.indicators=[];}var _7=[],_8=[];var i;if(this.hasChildren()){var _a=this.getChildren();for(i=0;i<_a.length;i++){if(/dojox\.widget\..*Indicator/.test(_a[i].declaredClass)){_8.push(_a[i]);continue;}switch(_a[i].declaredClass){case "dojox.widget.gauge.Range":_7.push(_a[i]);break;}}this.ranges=this.ranges.concat(_7);this.indicators=this.indicators.concat(_8);}if(!this.background){this.background=this._backgroundDefault;}this.background=this.background.color||this.background;if(!this.surface){this.createSurface();}this.addRanges(this.ranges);if(this.minorTicks&&this.minorTicks.interval){this.setMinorTicks(this.minorTicks);}if(this.majorTicks&&this.majorTicks.interval){this.setMajorTicks(this.majorTicks);}for(i=0;i<this.indicators.length;i++){this.addIndicator(this.indicators[i]);}},_setTicks:function(_b,_c,_d){var i;if(_b&&_4.isArray(_b._ticks)){for(i=0;i<_b._ticks.length;i++){this.removeIndicator(_b._ticks[i]);}}var t={length:_c.length,offset:_c.offset,noChange:true};if(_c.color){t.color=_c.color;}if(_c.font){t.font=_c.font;}_c._ticks=[];for(i=this.min;i<=this.max;i+=_c.interval){t.value=i;if(_d){t.label=""+i;}_c._ticks.push(this.addIndicator(t));}return _c;},setMinorTicks:function(_10){this.minorTicks=this._setTicks(this.minorTicks,_10,false);},setMajorTicks:function(_11){this.majorTicks=this._setTicks(this.majorTicks,_11,true);},postCreate:function(){if(this.hideValues){_4.style(this.containerNode,"display","none");}_4.style(this.mouseNode,"width","0");_4.style(this.mouseNode,"height","0");_4.style(this.mouseNode,"position","absolute");_4.style(this.mouseNode,"z-index","100");if(this.useTooltip){_5.showTooltip("test",this.mouseNode);_5.hideTooltip(this.mouseNode);}},createSurface:function(){this.gaugeContent.style.width=this.width+"px";this.gaugeContent.style.height=this.height+"px";this.surface=_6.gfx.createSurface(this.gaugeContent,this.width,this.height);this._background=this.surface.createRect({x:0,y:0,width:this.width,height:this.height});this._background.setFill(this.background);if(this.image.url){this._img=this.surface.createImage({width:this.image.width||this.width,height:this.image.height||this.height,src:this.image.url});if(this.image.overlay){this._img.getEventSource().setAttribute("overlay",true);}if(this.image.x||this.image.y){this._img.setTransform({dx:this.image.x||0,dy:this.image.y||0});}}},setBackground:function(_12){if(!_12){_12=this._backgroundDefault;}this.background=_12.color||_12;this._background.setFill(this.background);},addRange:function(_13){this.addRanges([_13]);},addRanges:function(_14){if(!this._rangeData){this._rangeData=[];}var _15;for(var i=0;i<_14.length;i++){_15=_14[i];if((this.min===null)||(_15.low<this.min)){this.min=_15.low;}if((this.max===null)||(_15.high>this.max)){this.max=_15.high;}if(!_15.color){var _17=this._rangeData.length%this.defaultColors.length;if(_6.gfx.svg&&this.useRangeStyles>0){_17=(this._rangeData.length%this.useRangeStyles)+1;_15.color={style:"dojoxGaugeRange"+_17};}else{_17=this._rangeData.length%this.defaultColors.length;_15.color=this.defaultColors[_17];}}this._rangeData[this._rangeData.length]=_15;}this.draw();},addIndicator:function(_18){_18._gauge=this;if(!_18.declaredClass){_18=new this._defaultIndicator(_18);}if(!_18.hideValue){this.containerNode.appendChild(_18.domNode);}if(!this._indicatorData){this._indicatorData=[];}this._indicatorData[this._indicatorData.length]=_18;_18.draw();return _18;},removeIndicator:function(_19){for(var i=0;i<this._indicatorData.length;i++){if(this._indicatorData[i]===_19){this._indicatorData.splice(i,1);_19.remove();break;}}},moveIndicatorToFront:function(_1b){if(_1b.shapes){for(var i=0;i<_1b.shapes.length;i++){_1b.shapes[i].moveToFront();}}},drawText:function(txt,x,y,_20,_21,_22,_23){var t=this.surface.createText({x:x,y:y,text:txt,align:_20});t.setFill(_22);t.setFont(_23);return t;},removeText:function(t){this.surface.rawNode.removeChild(t);},updateTooltip:function(txt,e){if(this._lastHover!=txt){if(txt!==""){_5.hideTooltip(this.mouseNode);_5.showTooltip(txt,this.mouseNode);}else{_5.hideTooltip(this.mouseNode);}this._lastHover=txt;}},handleMouseOver:function(_28){var _29=_28.target.getAttribute("hover");if(_28.target.getAttribute("overlay")){this._overOverlay=true;var r=this.getRangeUnderMouse(_28);if(r&&r.hover){_29=r.hover;}}if(this.useTooltip&&!this._drag){if(_29){this.updateTooltip(_29,_28);}else{this.updateTooltip("",_28);}}},handleMouseOut:function(_2b){if(_2b.target.getAttribute("overlay")){this._overOverlay=false;}if(this.useTooltip&&this.mouseNode){_5.hideTooltip(this.mouseNode);}},handleMouseDown:function(_2c){for(var i=0;i<this._indicatorData.length;i++){var _2e=this._indicatorData[i].shapes;for(var s=0;s<_2e.length;s++){if(_2e[s].getEventSource()==_2c.target){this._drag=this._indicatorData[i];s=_2e.length;i=this._indicatorData.length;}}}_4.stopEvent(_2c);},handleMouseUp:function(_30){this._drag=null;_4.stopEvent(_30);},handleMouseMove:function(_31){if(_31){_4.style(this.mouseNode,"left",_31.pageX+1+"px");_4.style(this.mouseNode,"top",_31.pageY+1+"px");}if(this._drag){this._dragIndicator(this,_31);}else{if(this.useTooltip&&this._overOverlay){var r=this.getRangeUnderMouse(_31);if(r&&r.hover){this.updateTooltip(r.hover,_31);}else{this.updateTooltip("",_31);}}}}});_4.declare("dojox.widget.gauge.Range",[_5._Widget,_5._Contained],{low:0,high:0,hover:"",color:null,size:0,startup:function(){this.color=this.color.color||this.color;}});_4.declare("dojox.widget.gauge._Indicator",[_5._Widget,_5._Contained,_5._Templated],{value:0,type:"",color:"black",label:"",font:{family:"sans-serif",size:"12px"},length:0,width:0,offset:0,hover:"",front:false,easing:_4._defaultEasing,duration:1000,hideValue:false,noChange:false,_gauge:null,title:"",templateString:"<div class=\"dojoxGaugeIndicatorDiv\">\r\n\t<label class=\"dojoxGaugeIndicatorLabel\" for=\"${title}\">${title}:</label>\r\n\t<input class=\"dojoxGaugeIndicatorInput\" name=\"${title}\" size=\"5\" value=\"${value}\" dojoAttachPoint=\"valueNode\" dojoAttachEvent=\"onchange:_update\"></input>\r\n</div>\r\n",startup:function(){if(this.onDragMove){this.onDragMove=_4.hitch(this.onDragMove);}},postCreate:function(){if(this.title===""){_4.style(this.domNode,"display","none");}if(_4.isString(this.easing)){this.easing=_4.getObject(this.easing);}},_update:function(_33){var _34=this.valueNode.value;if(_34===""){this.value=null;}else{this.value=Number(_34);this.hover=this.title+": "+_34;}if(this._gauge){this.draw();this.valueNode.value=this.value;if((this.title=="Target"||this.front)&&this._gauge.moveIndicator){this._gauge.moveIndicatorToFront(this);}}},update:function(_35){if(!this.noChange){this.valueNode.value=_35;this._update();}},onDragMove:function(){this.value=Math.floor(this.value);this.valueNode.value=this.value;this.hover=this.title+": "+this.value;},draw:function(_36){},remove:function(){for(var i=0;i<this.shapes.length;i++){this._gauge.surface.remove(this.shapes[i]);}if(this.text){this._gauge.surface.remove(this.text);}}});}}};});