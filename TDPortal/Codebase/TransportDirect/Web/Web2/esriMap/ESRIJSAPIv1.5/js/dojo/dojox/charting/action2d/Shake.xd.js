/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.action2d.Shake"],["require","dojox.charting.action2d.Base"],["require","dojox.gfx.matrix"],["require","dojo.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.action2d.Shake"]){_4._hasResource["dojox.charting.action2d.Shake"]=true;_4.provide("dojox.charting.action2d.Shake");_4.require("dojox.charting.action2d.Base");_4.require("dojox.gfx.matrix");_4.require("dojo.fx");(function(){var _7=3,m=_6.gfx.matrix,gf=_6.gfx.fx;_4.declare("dojox.charting.action2d.Shake",_6.charting.action2d.Base,{defaultParams:{duration:400,easing:_4.fx.easing.backOut,shiftX:_7,shiftY:_7},optionalParams:{},constructor:function(_a,_b,_c){if(!_c){_c={};}this.shiftX=typeof _c.shiftX=="number"?_c.shiftX:_7;this.shiftY=typeof _c.shiftY=="number"?_c.shiftY:_7;this.connect();},process:function(o){if(!o.shape||!(o.type in this.overOutEvents)){return;}var _e=o.run.name,_f=o.index,_10=[],_11,_12=o.type=="onmouseover"?this.shiftX:-this.shiftX,_13=o.type=="onmouseover"?this.shiftY:-this.shiftY;if(_e in this.anim){_11=this.anim[_e][_f];}else{this.anim[_e]={};}if(_11){_11.action.stop(true);}else{this.anim[_e][_f]=_11={};}var _14={shape:o.shape,duration:this.duration,easing:this.easing,transform:[{name:"translate",start:[this.shiftX,this.shiftY],end:[0,0]},m.identity]};if(o.shape){_10.push(gf.animateTransform(_14));}if(o.oultine){_14.shape=o.outline;_10.push(gf.animateTransform(_14));}if(o.shadow){_14.shape=o.shadow;_10.push(gf.animateTransform(_14));}if(!_10.length){delete this.anim[_e][_f];return;}_11.action=_4.fx.combine(_10);if(o.type=="onmouseout"){_4.connect(_11.action,"onEnd",this,function(){if(this.anim[_e]){delete this.anim[_e][_f];}});}_11.action.play();}});})();}}};});