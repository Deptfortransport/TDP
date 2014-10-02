/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.Chart2D"],["require","dojox.gfx"],["require","dojox.lang.functional"],["require","dojox.lang.functional.fold"],["require","dojox.lang.functional.reversed"],["require","dojox.charting.Theme"],["require","dojox.charting.Series"],["require","dojox.charting.axis2d.Default"],["require","dojox.charting.plot2d.Default"],["require","dojox.charting.plot2d.Lines"],["require","dojox.charting.plot2d.Areas"],["require","dojox.charting.plot2d.Markers"],["require","dojox.charting.plot2d.MarkersOnly"],["require","dojox.charting.plot2d.Scatter"],["require","dojox.charting.plot2d.Stacked"],["require","dojox.charting.plot2d.StackedLines"],["require","dojox.charting.plot2d.StackedAreas"],["require","dojox.charting.plot2d.Columns"],["require","dojox.charting.plot2d.StackedColumns"],["require","dojox.charting.plot2d.ClusteredColumns"],["require","dojox.charting.plot2d.Bars"],["require","dojox.charting.plot2d.StackedBars"],["require","dojox.charting.plot2d.ClusteredBars"],["require","dojox.charting.plot2d.Grid"],["require","dojox.charting.plot2d.Pie"],["require","dojox.charting.plot2d.Bubble"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.Chart2D"]){_4._hasResource["dojox.charting.Chart2D"]=true;_4.provide("dojox.charting.Chart2D");_4.require("dojox.gfx");_4.require("dojox.lang.functional");_4.require("dojox.lang.functional.fold");_4.require("dojox.lang.functional.reversed");_4.require("dojox.charting.Theme");_4.require("dojox.charting.Series");_4.require("dojox.charting.axis2d.Default");_4.require("dojox.charting.plot2d.Default");_4.require("dojox.charting.plot2d.Lines");_4.require("dojox.charting.plot2d.Areas");_4.require("dojox.charting.plot2d.Markers");_4.require("dojox.charting.plot2d.MarkersOnly");_4.require("dojox.charting.plot2d.Scatter");_4.require("dojox.charting.plot2d.Stacked");_4.require("dojox.charting.plot2d.StackedLines");_4.require("dojox.charting.plot2d.StackedAreas");_4.require("dojox.charting.plot2d.Columns");_4.require("dojox.charting.plot2d.StackedColumns");_4.require("dojox.charting.plot2d.ClusteredColumns");_4.require("dojox.charting.plot2d.Bars");_4.require("dojox.charting.plot2d.StackedBars");_4.require("dojox.charting.plot2d.ClusteredBars");_4.require("dojox.charting.plot2d.Grid");_4.require("dojox.charting.plot2d.Pie");_4.require("dojox.charting.plot2d.Bubble");(function(){var df=_6.lang.functional,dc=_6.charting,_9=df.lambda("item.clear()"),_a=df.lambda("item.purgeGroup()"),_b=df.lambda("item.destroy()"),_c=df.lambda("item.dirty = false"),_d=df.lambda("item.dirty = true");_4.declare("dojox.charting.Chart2D",null,{constructor:function(_e,_f){if(!_f){_f={};}this.margins=_f.margins?_f.margins:{l:10,t:10,r:10,b:10};this.stroke=_f.stroke;this.fill=_f.fill;this.theme=null;this.axes={};this.stack=[];this.plots={};this.series=[];this.runs={};this.dirty=true;this.coords=null;this.node=_4.byId(_e);var box=_4.marginBox(_e);this.surface=_6.gfx.createSurface(this.node,box.w,box.h);},destroy:function(){_4.forEach(this.series,_b);_4.forEach(this.stack,_b);df.forIn(this.axes,_b);this.surface.destroy();},getCoords:function(){if(!this.coords){this.coords=_4.coords(this.node,true);}return this.coords;},setTheme:function(_11){this.theme=_11._clone();this.dirty=true;return this;},addAxis:function(_12,_13){var _14;if(!_13||!("type" in _13)){_14=new dc.axis2d.Default(this,_13);}else{_14=typeof _13.type=="string"?new dc.axis2d[_13.type](this,_13):new _13.type(this,_13);}_14.name=_12;_14.dirty=true;if(_12 in this.axes){this.axes[_12].destroy();}this.axes[_12]=_14;this.dirty=true;return this;},getAxis:function(_15){return this.axes[_15];},removeAxis:function(_16){if(_16 in this.axes){this.axes[_16].destroy();delete this.axes[_16];this.dirty=true;}return this;},addPlot:function(_17,_18){var _19;if(!_18||!("type" in _18)){_19=new dc.plot2d.Default(this,_18);}else{_19=typeof _18.type=="string"?new dc.plot2d[_18.type](this,_18):new _18.type(this,_18);}_19.name=_17;_19.dirty=true;if(_17 in this.plots){this.stack[this.plots[_17]].destroy();this.stack[this.plots[_17]]=_19;}else{this.plots[_17]=this.stack.length;this.stack.push(_19);}this.dirty=true;return this;},removePlot:function(_1a){if(_1a in this.plots){var _1b=this.plots[_1a];delete this.plots[_1a];this.stack[_1b].destroy();this.stack.splice(_1b,1);df.forIn(this.plots,function(idx,_1d,_1e){if(idx>_1b){_1e[_1d]=idx-1;}});this.dirty=true;}return this;},addSeries:function(_1f,_20,_21){var run=new dc.Series(this,_20,_21);if(_1f in this.runs){this.series[this.runs[_1f]].destroy();this.series[this.runs[_1f]]=run;}else{this.runs[_1f]=this.series.length;this.series.push(run);}run.name=_1f;this.dirty=true;if(!("ymin" in run)&&"min" in run){run.ymin=run.min;}if(!("ymax" in run)&&"max" in run){run.ymax=run.max;}return this;},removeSeries:function(_23){if(_23 in this.runs){var _24=this.runs[_23],_25=this.series[_24].plot;delete this.runs[_23];this.series[_24].destroy();this.series.splice(_24,1);df.forIn(this.runs,function(idx,_27,_28){if(idx>_24){_28[_27]=idx-1;}});this.dirty=true;}return this;},updateSeries:function(_29,_2a){if(_29 in this.runs){var run=this.series[this.runs[_29]];run.data=_2a;run.dirty=true;this._invalidateDependentPlots(run.plot,false);this._invalidateDependentPlots(run.plot,true);}return this;},resize:function(_2c,_2d){var box;switch(arguments.length){case 0:box=_4.marginBox(this.node);break;case 1:box=_2c;break;default:box={w:_2c,h:_2d};break;}_4.marginBox(this.node,box);this.surface.setDimensions(box.w,box.h);this.dirty=true;this.coords=null;return this.render();},getGeometry:function(){var ret={};df.forIn(this.axes,function(_30){if(_30.initialized()){ret[_30.name]={name:_30.name,vertical:_30.vertical,scaler:_30.scaler,ticks:_30.ticks};}});return ret;},setAxisWindow:function(_31,_32,_33){var _34=this.axes[_31];if(_34){_34.setWindow(_32,_33);}return this;},setWindow:function(sx,sy,dx,dy){if(!("plotArea" in this)){this.calculateGeometry();}df.forIn(this.axes,function(_39){var _3a,_3b,_3c=_39.getScaler().bounds,s=_3c.span/(_3c.upper-_3c.lower);if(_39.vertical){_3a=sy;_3b=dy/s/_3a;}else{_3a=sx;_3b=dx/s/_3a;}_39.setWindow(_3a,_3b);});return this;},calculateGeometry:function(){if(this.dirty){return this.fullGeometry();}_4.forEach(this.stack,function(_3e){if(_3e.dirty||(_3e.hAxis&&this.axes[_3e.hAxis].dirty)||(_3e.vAxis&&this.axes[_3e.vAxis].dirty)){_3e.calculateAxes(this.plotArea);}},this);return this;},fullGeometry:function(){this._makeDirty();_4.forEach(this.stack,_9);if(!this.theme){this.setTheme(new _6.charting.Theme(_6.charting._def));}_4.forEach(this.series,function(run){if(!(run.plot in this.plots)){var _40=new dc.plot2d.Default(this,{});_40.name=run.plot;this.plots[run.plot]=this.stack.length;this.stack.push(_40);}this.stack[this.plots[run.plot]].addSeries(run);},this);_4.forEach(this.stack,function(_41){if(_41.hAxis){_41.setAxis(this.axes[_41.hAxis]);}if(_41.vAxis){_41.setAxis(this.axes[_41.vAxis]);}},this);var dim=this.dim=this.surface.getDimensions();dim.width=_6.gfx.normalizedLength(dim.width);dim.height=_6.gfx.normalizedLength(dim.height);df.forIn(this.axes,_9);_4.forEach(this.stack,function(_43){_43.calculateAxes(dim);});var _44=this.offsets={l:0,r:0,t:0,b:0};df.forIn(this.axes,function(_45){df.forIn(_45.getOffsets(),function(o,i){_44[i]+=o;});});df.forIn(this.margins,function(o,i){_44[i]+=o;});this.plotArea={width:dim.width-_44.l-_44.r,height:dim.height-_44.t-_44.b};df.forIn(this.axes,_9);_4.forEach(this.stack,function(_4a){_4a.calculateAxes(this.plotArea);},this);return this;},render:function(){if(this.theme){this.theme.clear();}if(this.dirty){return this.fullRender();}this.calculateGeometry();df.forEachRev(this.stack,function(_4b){_4b.render(this.dim,this.offsets);},this);df.forIn(this.axes,function(_4c){_4c.render(this.dim,this.offsets);},this);this._makeClean();if(this.surface.render){this.surface.render();}return this;},fullRender:function(){this.fullGeometry();var _4d=this.offsets,dim=this.dim;var _4f=df.foldl(this.stack,"z + plot.getRequiredColors()",0);this.theme.defineColors({num:_4f,cache:false});_4.forEach(this.series,_a);df.forIn(this.axes,_a);_4.forEach(this.stack,_a);this.surface.clear();var t=this.theme,_51=t.plotarea&&t.plotarea.fill,_52=t.plotarea&&t.plotarea.stroke;if(_51){this.surface.createRect({x:_4d.l,y:_4d.t,width:dim.width-_4d.l-_4d.r,height:dim.height-_4d.t-_4d.b}).setFill(_51);}if(_52){this.surface.createRect({x:_4d.l,y:_4d.t,width:dim.width-_4d.l-_4d.r-1,height:dim.height-_4d.t-_4d.b-1}).setStroke(_52);}df.foldr(this.stack,function(z,_54){return _54.render(dim,_4d),0;},0);_51=this.fill?this.fill:(t.chart&&t.chart.fill);_52=this.stroke?this.stroke:(t.chart&&t.chart.stroke);if(_51=="inherit"){var _55=this.node,_51=new _4.Color(_4.style(_55,"backgroundColor"));while(_51.a==0&&_55!=document.documentElement){_51=new _4.Color(_4.style(_55,"backgroundColor"));_55=_55.parentNode;}}if(_51){if(_4d.l){this.surface.createRect({width:_4d.l,height:dim.height+1}).setFill(_51);}if(_4d.r){this.surface.createRect({x:dim.width-_4d.r,width:_4d.r+1,height:dim.height+1}).setFill(_51);}if(_4d.t){this.surface.createRect({width:dim.width+1,height:_4d.t}).setFill(_51);}if(_4d.b){this.surface.createRect({y:dim.height-_4d.b,width:dim.width+1,height:_4d.b+2}).setFill(_51);}}if(_52){this.surface.createRect({width:dim.width-1,height:dim.height-1}).setStroke(_52);}df.forIn(this.axes,function(_56){_56.render(dim,_4d);});this._makeClean();if(this.surface.render){this.surface.render();}return this;},connectToPlot:function(_57,_58,_59){return _57 in this.plots?this.stack[this.plots[_57]].connect(_58,_59):null;},_makeClean:function(){_4.forEach(this.axes,_c);_4.forEach(this.stack,_c);_4.forEach(this.series,_c);this.dirty=false;},_makeDirty:function(){_4.forEach(this.axes,_d);_4.forEach(this.stack,_d);_4.forEach(this.series,_d);this.dirty=true;},_invalidateDependentPlots:function(_5a,_5b){if(_5a in this.plots){var _5c=this.stack[this.plots[_5a]],_5d,_5e=_5b?"vAxis":"hAxis";if(_5c[_5e]){_5d=this.axes[_5c[_5e]];if(_5d.dependOnData()){_5d.dirty=true;_4.forEach(this.stack,function(p){if(p[_5e]&&p[_5e]==_5c[_5e]){p.dirty=true;}});}}else{_5c.dirty=true;}}}});})();}}};});