/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.plot2d.Bars"],["require","dojox.charting.plot2d.common"],["require","dojox.charting.plot2d.Base"],["require","dojox.lang.utils"],["require","dojox.lang.functional"],["require","dojox.lang.functional.reversed"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.plot2d.Bars"]){_4._hasResource["dojox.charting.plot2d.Bars"]=true;_4.provide("dojox.charting.plot2d.Bars");_4.require("dojox.charting.plot2d.common");_4.require("dojox.charting.plot2d.Base");_4.require("dojox.lang.utils");_4.require("dojox.lang.functional");_4.require("dojox.lang.functional.reversed");(function(){var df=_6.lang.functional,du=_6.lang.utils,dc=_6.charting.plot2d.common,_a=df.lambda("item.purgeGroup()");_4.declare("dojox.charting.plot2d.Bars",_6.charting.plot2d.Base,{defaultParams:{hAxis:"x",vAxis:"y",gap:0,shadows:null},optionalParams:{minBarSize:1,maxBarSize:1},constructor:function(_b,_c){this.opt=_4.clone(this.defaultParams);du.updateWithObject(this.opt,_c);du.updateWithPattern(this.opt,_c,this.optionalParams);this.series=[];this.hAxis=this.opt.hAxis;this.vAxis=this.opt.vAxis;},calculateAxes:function(_d){var _e=dc.collectSimpleStats(this.series),t;_e.hmin-=0.5;_e.hmax+=0.5;t=_e.hmin,_e.hmin=_e.vmin,_e.vmin=t;t=_e.hmax,_e.hmax=_e.vmax,_e.vmax=t;this._calc(_d,_e);return this;},render:function(dim,_11){this.dirty=this.isDirty();if(this.dirty){_4.forEach(this.series,_a);this.cleanGroup();var s=this.group;df.forEachRev(this.series,function(_13){_13.cleanGroup(s);});}var t=this.chart.theme,_15,_16,_17,f,gap,_1a,ht=this._hScaler.scaler.getTransformerFromModel(this._hScaler),vt=this._vScaler.scaler.getTransformerFromModel(this._vScaler),_1d=Math.max(0,this._hScaler.bounds.lower),_1e=ht(_1d),_1f=this.events();f=dc.calculateBarSize(this._vScaler.bounds.scale,this.opt);gap=f.gap;_1a=f.size;this.resetEvents();for(var i=this.series.length-1;i>=0;--i){var run=this.series[i];if(!this.dirty&&!run.dirty){continue;}run.cleanGroup();var s=run.group;if(!run.fill||!run.stroke){_15=run.dyn.color=new _4.Color(t.next("color"));}_16=run.stroke?run.stroke:dc.augmentStroke(t.series.stroke,_15);_17=run.fill?run.fill:dc.augmentFill(t.series.fill,_15);for(var j=0;j<run.data.length;++j){var v=run.data[j],hv=ht(v),_25=hv-_1e,w=Math.abs(_25);if(w>=1&&_1a>=1){var _27=s.createRect({x:_11.l+(v<_1d?hv:_1e),y:dim.height-_11.b-vt(j+1.5)+gap,width:w,height:_1a}).setFill(_17).setStroke(_16);run.dyn.fill=_27.getFill();run.dyn.stroke=_27.getStroke();if(_1f){var o={element:"bar",index:j,run:run,plot:this,hAxis:this.hAxis||null,vAxis:this.vAxis||null,shape:_27,x:v,y:j+1.5};this._connectEvents(_27,o);}}}run.dirty=false;}this.dirty=false;return this;}});})();}}};});