/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.axis2d.Default"],["require","dojox.charting.scaler.linear"],["require","dojox.charting.axis2d.common"],["require","dojox.charting.axis2d.Base"],["require","dojo.colors"],["require","dojo.string"],["require","dojox.gfx"],["require","dojox.lang.functional"],["require","dojox.lang.utils"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.axis2d.Default"]){_4._hasResource["dojox.charting.axis2d.Default"]=true;_4.provide("dojox.charting.axis2d.Default");_4.require("dojox.charting.scaler.linear");_4.require("dojox.charting.axis2d.common");_4.require("dojox.charting.axis2d.Base");_4.require("dojo.colors");_4.require("dojo.string");_4.require("dojox.gfx");_4.require("dojox.lang.functional");_4.require("dojox.lang.utils");(function(){var dc=_6.charting,df=_6.lang.functional,du=_6.lang.utils,g=_6.gfx,_b=dc.scaler.linear,_c=4;_4.declare("dojox.charting.axis2d.Default",_6.charting.axis2d.Base,{defaultParams:{vertical:false,fixUpper:"none",fixLower:"none",natural:false,leftBottom:true,includeZero:false,fixed:true,majorLabels:true,minorTicks:true,minorLabels:true,microTicks:false,htmlLabels:true},optionalParams:{min:0,max:1,from:0,to:1,majorTickStep:4,minorTickStep:2,microTickStep:1,labels:[],labelFunc:null,maxLabelSize:0,stroke:{},majorTick:{},minorTick:{},microTick:{},font:"",fontColor:""},constructor:function(_d,_e){this.opt=_4.delegate(this.defaultParams,_e);du.updateWithPattern(this.opt,_e,this.optionalParams);},dependOnData:function(){return !("min" in this.opt)||!("max" in this.opt);},clear:function(){delete this.scaler;delete this.ticks;this.dirty=true;return this;},initialized:function(){return "scaler" in this&&!(this.dirty&&this.dependOnData());},setWindow:function(_f,_10){this.scale=_f;this.offset=_10;return this.clear();},getWindowScale:function(){return "scale" in this?this.scale:1;},getWindowOffset:function(){return "offset" in this?this.offset:0;},calculate:function(min,max,_13,_14){if(this.initialized()){return this;}this.labels="labels" in this.opt?this.opt.labels:_14;this.scaler=_b.buildScaler(min,max,_13,this.opt);if("scale" in this){this.opt.from=this.scaler.bounds.lower+this.offset;this.opt.to=(this.scaler.bounds.upper-this.scaler.bounds.lower)/this.scale+this.opt.from;if(!isFinite(this.opt.from)||isNaN(this.opt.from)||!isFinite(this.opt.to)||isNaN(this.opt.to)||this.opt.to-this.opt.from>=this.scaler.bounds.upper-this.scaler.bounds.lower){delete this.opt.from;delete this.opt.to;delete this.scale;delete this.offset;}else{if(this.opt.from<this.scaler.bounds.lower){this.opt.to+=this.scaler.bounds.lower-this.opt.from;this.opt.from=this.scaler.bounds.lower;}else{if(this.opt.to>this.scaler.bounds.upper){this.opt.from+=this.scaler.bounds.upper-this.opt.to;this.opt.to=this.scaler.bounds.upper;}}this.offset=this.opt.from-this.scaler.bounds.lower;}this.scaler=_b.buildScaler(min,max,_13,this.opt);if(this.scale==1&&this.offset==0){delete this.scale;delete this.offset;}}var _15=0,ta=this.chart.theme.axis,_17="font" in this.opt?this.opt.font:ta.font,_18=_17?g.normalizedLength(g.splitFontString(_17).size):0;if(this.vertical){if(_18){_15=_18+_c;}}else{if(_18){var _19,i;if(this.opt.labelFunc&&this.opt.maxLabelSize){_19=this.opt.maxLabelSize;}else{if(this.labels){_19=df.foldl(df.map(this.labels,function(_1b){return _6.gfx._base._getTextBox(_1b.text,{font:_17}).w;}),"Math.max(a, b)",0);}else{var _1c=Math.ceil(Math.log(Math.max(Math.abs(this.scaler.bounds.from),Math.abs(this.scaler.bounds.to)))/Math.LN10),t=[];if(this.scaler.bounds.from<0||this.scaler.bounds.to<0){t.push("-");}t.push(_4.string.rep("9",_1c));var _1e=Math.floor(Math.log(this.scaler.bounds.to-this.scaler.bounds.from)/Math.LN10);if(_1e>0){t.push(".");for(i=0;i<_1e;++i){t.push("9");}}_19=_6.gfx._base._getTextBox(t.join(""),{font:_17}).w;}}_15=_19+_c;}}this.scaler.minMinorStep=_15;this.ticks=_b.buildTicks(this.scaler,this.opt);return this;},getScaler:function(){return this.scaler;},getTicks:function(){return this.ticks;},getOffsets:function(){var _1f={l:0,r:0,t:0,b:0},_20,a,b,c,d,gtb=_6.gfx._base._getTextBox,gl=dc.scaler.common.getNumericLabel,_27=0,ta=this.chart.theme.axis,_29="font" in this.opt?this.opt.font:ta.font,_2a="majorTick" in this.opt?this.opt.majorTick:ta.majorTick,_2b="minorTick" in this.opt?this.opt.minorTick:ta.minorTick,_2c=_29?g.normalizedLength(g.splitFontString(_29).size):0,s=this.scaler;if(!s){return _1f;}if(this.vertical){if(_2c){if(this.opt.labelFunc&&this.opt.maxLabelSize){_20=this.opt.maxLabelSize;}else{if(this.labels){_20=df.foldl(df.map(this.labels,function(_2e){return _6.gfx._base._getTextBox(_2e.text,{font:_29}).w;}),"Math.max(a, b)",0);}else{a=gtb(gl(s.major.start,s.major.prec,this.opt),{font:_29}).w;b=gtb(gl(s.major.start+s.major.count*s.major.tick,s.major.prec,this.opt),{font:_29}).w;c=gtb(gl(s.minor.start,s.minor.prec,this.opt),{font:_29}).w;d=gtb(gl(s.minor.start+s.minor.count*s.minor.tick,s.minor.prec,this.opt),{font:_29}).w;_20=Math.max(a,b,c,d);}}_27=_20+_c;}_27+=_c+Math.max(_2a.length,_2b.length);_1f[this.opt.leftBottom?"l":"r"]=_27;_1f.t=_1f.b=_2c/2;}else{if(_2c){_27=_2c+_c;}_27+=_c+Math.max(_2a.length,_2b.length);_1f[this.opt.leftBottom?"b":"t"]=_27;if(_2c){if(this.opt.labelFunc&&this.opt.maxLabelSize){_20=this.opt.maxLabelSize;}else{if(this.labels){_20=df.foldl(df.map(this.labels,function(_2f){return _6.gfx._base._getTextBox(_2f.text,{font:_29}).w;}),"Math.max(a, b)",0);}else{a=gtb(gl(s.major.start,s.major.prec,this.opt),{font:_29}).w;b=gtb(gl(s.major.start+s.major.count*s.major.tick,s.major.prec,this.opt),{font:_29}).w;c=gtb(gl(s.minor.start,s.minor.prec,this.opt),{font:_29}).w;d=gtb(gl(s.minor.start+s.minor.count*s.minor.tick,s.minor.prec,this.opt),{font:_29}).w;_20=Math.max(a,b,c,d);}}_1f.l=_1f.r=_20/2;}}return _1f;},render:function(dim,_31){if(!this.dirty){return this;}var _32,_33,_34,_35,_36,_37,ta=this.chart.theme.axis,_39="stroke" in this.opt?this.opt.stroke:ta.stroke,_3a="majorTick" in this.opt?this.opt.majorTick:ta.majorTick,_3b="minorTick" in this.opt?this.opt.minorTick:ta.minorTick,_3c="microTick" in this.opt?this.opt.microTick:ta.minorTick,_3d="font" in this.opt?this.opt.font:ta.font,_3e="fontColor" in this.opt?this.opt.fontColor:ta.fontColor,_3f=Math.max(_3a.length,_3b.length),_40=_3d?g.normalizedLength(g.splitFontString(_3d).size):0;if(this.vertical){_32={y:dim.height-_31.b};_33={y:_31.t};_34={x:0,y:-1};if(this.opt.leftBottom){_32.x=_33.x=_31.l;_35={x:-1,y:0};_37="end";}else{_32.x=_33.x=dim.width-_31.r;_35={x:1,y:0};_37="start";}_36={x:_35.x*(_3f+_c),y:_40*0.4};}else{_32={x:_31.l};_33={x:dim.width-_31.r};_34={x:1,y:0};_37="middle";if(this.opt.leftBottom){_32.y=_33.y=dim.height-_31.b;_35={x:0,y:1};_36={y:_3f+_c+_40};}else{_32.y=_33.y=_31.t;_35={x:0,y:-1};_36={y:-_3f-_c};}_36.x=0;}this.cleanGroup();try{var s=this.group,c=this.scaler,t=this.ticks,_44,f=_b.getTransformerFromModel(this.scaler),_46=_6.gfx.renderer=="canvas",_47=_46||this.opt.htmlLabels&&!_4.isIE&&!_4.isOpera?"html":"gfx",dx=_35.x*_3a.length,dy=_35.y*_3a.length;s.createLine({x1:_32.x,y1:_32.y,x2:_33.x,y2:_33.y}).setStroke(_39);_4.forEach(t.major,function(_4a){var _4b=f(_4a.value),_4c,x=_32.x+_34.x*_4b,y=_32.y+_34.y*_4b;s.createLine({x1:x,y1:y,x2:x+dx,y2:y+dy}).setStroke(_3a);if(_4a.label){_4c=dc.axis2d.common.createText[_47](this.chart,s,x+_36.x,y+_36.y,_37,_4a.label,_3d,_3e);if(_47=="html"){this.htmlElements.push(_4c);}}},this);dx=_35.x*_3b.length;dy=_35.y*_3b.length;_44=c.minMinorStep<=c.minor.tick*c.bounds.scale;_4.forEach(t.minor,function(_4f){var _50=f(_4f.value),_51,x=_32.x+_34.x*_50,y=_32.y+_34.y*_50;s.createLine({x1:x,y1:y,x2:x+dx,y2:y+dy}).setStroke(_3b);if(_44&&_4f.label){_51=dc.axis2d.common.createText[_47](this.chart,s,x+_36.x,y+_36.y,_37,_4f.label,_3d,_3e);if(_47=="html"){this.htmlElements.push(_51);}}},this);dx=_35.x*_3c.length;dy=_35.y*_3c.length;_4.forEach(t.micro,function(_54){var _55=f(_54.value),_56,x=_32.x+_34.x*_55,y=_32.y+_34.y*_55;s.createLine({x1:x,y1:y,x2:x+dx,y2:y+dy}).setStroke(_3c);},this);}catch(e){}this.dirty=false;return this;}});})();}}};});