/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.gfx.fx"],["require","dojox.gfx.matrix"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.gfx.fx"]){_4._hasResource["dojox.gfx.fx"]=true;_4.provide("dojox.gfx.fx");_4.require("dojox.gfx.matrix");(function(){var d=_4,g=_6.gfx,m=g.matrix;var _a=function(_b,_c){this.start=_b,this.end=_c;};d.extend(_a,{getValue:function(r){return (this.end-this.start)*r+this.start;}});var _e=function(_f,end,_11){this.start=_f,this.end=end;this.unit=_11;};d.extend(_e,{getValue:function(r){return (this.end-this.start)*r+this.start+this.unit;}});var _13=function(_14,end){this.start=_14,this.end=end;this.temp=new _4.Color();};d.extend(_13,{getValue:function(r){return d.blendColors(this.start,this.end,r,this.temp);}});var _17=function(_18){this.values=_18;this.length=_18.length;};d.extend(_17,{getValue:function(r){return this.values[Math.min(Math.floor(r*this.length),this.length-1)];}});var _1a=function(_1b,def){this.values=_1b;this.def=def?def:{};};d.extend(_1a,{getValue:function(r){var ret=_4.clone(this.def);for(var i in this.values){ret[i]=this.values[i].getValue(r);}return ret;}});var _20=function(_21,_22){this.stack=_21;this.original=_22;};d.extend(_20,{getValue:function(r){var ret=[];_4.forEach(this.stack,function(t){if(t instanceof m.Matrix2D){ret.push(t);return;}if(t.name=="original"&&this.original){ret.push(this.original);return;}if(!(t.name in m)){return;}var f=m[t.name];if(typeof f!="function"){ret.push(f);return;}var val=_4.map(t.start,function(v,i){return (t.end[i]-v)*r+v;}),_2a=f.apply(m,val);if(_2a instanceof m.Matrix2D){ret.push(_2a);}},this);return ret;}});var _2b=new d.Color(0,0,0,0);var _2c=function(_2d,obj,_2f,def){if(_2d.values){return new _17(_2d.values);}var _31,_32,end;if(_2d.start){_32=g.normalizeColor(_2d.start);}else{_32=_31=obj?(_2f?obj[_2f]:obj):def;}if(_2d.end){end=g.normalizeColor(_2d.end);}else{if(!_31){_31=obj?(_2f?obj[_2f]:obj):def;}end=_31;}return new _13(_32,end);};var _34=function(_35,obj,_37,def){if(_35.values){return new _17(_35.values);}var _39,_3a,end;if(_35.start){_3a=_35.start;}else{_3a=_39=obj?obj[_37]:def;}if(_35.end){end=_35.end;}else{if(typeof _39!="number"){_39=obj?obj[_37]:def;}end=_39;}return new _a(_3a,end);};g.fx.animateStroke=function(_3c){if(!_3c.easing){_3c.easing=d._defaultEasing;}var _3d=new d._Animation(_3c),_3e=_3c.shape,_3f;d.connect(_3d,"beforeBegin",_3d,function(){_3f=_3e.getStroke();var _40=_3c.color,_41={},_42,_43,end;if(_40){_41.color=_2c(_40,_3f,"color",_2b);}_40=_3c.style;if(_40&&_40.values){_41.style=new _17(_40.values);}_40=_3c.width;if(_40){_41.width=_34(_40,_3f,"width",1);}_40=_3c.cap;if(_40&&_40.values){_41.cap=new _17(_40.values);}_40=_3c.join;if(_40){if(_40.values){_41.join=new _17(_40.values);}else{_43=_40.start?_40.start:(_3f&&_3f.join||0);end=_40.end?_40.end:(_3f&&_3f.join||0);if(typeof _43=="number"&&typeof end=="number"){_41.join=new _a(_43,end);}}}this.curve=new _1a(_41,_3f);});d.connect(_3d,"onAnimate",_3e,"setStroke");return _3d;};g.fx.animateFill=function(_45){if(!_45.easing){_45.easing=d._defaultEasing;}var _46=new d._Animation(_45),_47=_45.shape,_48;d.connect(_46,"beforeBegin",_46,function(){_48=_47.getFill();var _49=_45.color,_4a={};if(_49){this.curve=_2c(_49,_48,"",_2b);}});d.connect(_46,"onAnimate",_47,"setFill");return _46;};g.fx.animateFont=function(_4b){if(!_4b.easing){_4b.easing=d._defaultEasing;}var _4c=new d._Animation(_4b),_4d=_4b.shape,_4e;d.connect(_4c,"beforeBegin",_4c,function(){_4e=_4d.getFont();var _4f=_4b.style,_50={},_51,_52,end;if(_4f&&_4f.values){_50.style=new _17(_4f.values);}_4f=_4b.variant;if(_4f&&_4f.values){_50.variant=new _17(_4f.values);}_4f=_4b.weight;if(_4f&&_4f.values){_50.weight=new _17(_4f.values);}_4f=_4b.family;if(_4f&&_4f.values){_50.family=new _17(_4f.values);}_4f=_4b.size;if(_4f&&_4f.unit){_52=parseFloat(_4f.start?_4f.start:(_4d.font&&_4d.font.size||"0"));end=parseFloat(_4f.end?_4f.end:(_4d.font&&_4d.font.size||"0"));_50.size=new _e(_52,end,_4f.unit);}this.curve=new _1a(_50,_4e);});d.connect(_4c,"onAnimate",_4d,"setFont");return _4c;};g.fx.animateTransform=function(_54){if(!_54.easing){_54.easing=d._defaultEasing;}var _55=new d._Animation(_54),_56=_54.shape,_57;d.connect(_55,"beforeBegin",_55,function(){_57=_56.getTransform();this.curve=new _20(_54.transform,_57);});d.connect(_55,"onAnimate",_56,"setTransform");return _55;};})();}}};});