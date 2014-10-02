/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.action2d.Base"],["require","dojo.fx.easing"],["require","dojox.lang.functional.object"],["require","dojox.gfx.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.action2d.Base"]){_4._hasResource["dojox.charting.action2d.Base"]=true;_4.provide("dojox.charting.action2d.Base");_4.require("dojo.fx.easing");_4.require("dojox.lang.functional.object");_4.require("dojox.gfx.fx");(function(){var _7=400,_8=_4.fx.easing.backOut,df=_6.lang.functional;_4.declare("dojox.charting.action2d.Base",null,{overOutEvents:{onmouseover:1,onmouseout:1},constructor:function(_a,_b,_c){this.chart=_a;this.plot=_b?_b:"default";this.anim={};if(!_c){_c={};}this.duration=_c.duration?_c.duration:_7;this.easing=_c.easing?_c.easing:_8;},connect:function(){this.handle=this.chart.connectToPlot(this.plot,this,"process");},disconnect:function(){if(this.handle){_4.disconnect(this.handle);this.handle=null;}},reset:function(){},destroy:function(){if(this.handle){this.disconnect();}df.forIn(this.anim,function(o){df.forIn(o,function(_e){_e.action.stop(true);});});this.anim={};}});})();}}};});