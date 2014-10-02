/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._base.place"],["require","dojo.AdapterRegistry"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._base.place"]){_4._hasResource["dijit._base.place"]=true;_4.provide("dijit._base.place");_4.require("dojo.AdapterRegistry");_5.getViewport=function(){var _7=(_4.doc.compatMode=="BackCompat")?_4.body():_4.doc.documentElement;var _8=_4._docScroll();return {w:_7.clientWidth,h:_7.clientHeight,l:_8.x,t:_8.y};};_5.placeOnScreen=function(_9,_a,_b,_c){var _d=_4.map(_b,function(_e){var c={corner:_e,pos:{x:_a.x,y:_a.y}};if(_c){c.pos.x+=_e.charAt(1)=="L"?_c.x:-_c.x;c.pos.y+=_e.charAt(0)=="T"?_c.y:-_c.y;}return c;});return _5._place(_9,_d);};_5._place=function(_10,_11,_12){var _13=_5.getViewport();if(!_10.parentNode||String(_10.parentNode.tagName).toLowerCase()!="body"){_4.body().appendChild(_10);}var _14=null;_4.some(_11,function(_15){var _16=_15.corner;var pos=_15.pos;if(_12){_12(_10,_15.aroundCorner,_16);}var _18=_10.style;var _19=_18.display;var _1a=_18.visibility;_18.visibility="hidden";_18.display="";var mb=_4.marginBox(_10);_18.display=_19;_18.visibility=_1a;var _1c=(_16.charAt(1)=="L"?pos.x:Math.max(_13.l,pos.x-mb.w)),_1d=(_16.charAt(0)=="T"?pos.y:Math.max(_13.t,pos.y-mb.h)),_1e=(_16.charAt(1)=="L"?Math.min(_13.l+_13.w,_1c+mb.w):pos.x),_1f=(_16.charAt(0)=="T"?Math.min(_13.t+_13.h,_1d+mb.h):pos.y),_20=_1e-_1c,_21=_1f-_1d,_22=(mb.w-_20)+(mb.h-_21);if(_14==null||_22<_14.overflow){_14={corner:_16,aroundCorner:_15.aroundCorner,x:_1c,y:_1d,w:_20,h:_21,overflow:_22};}return !_22;});_10.style.left=_14.x+"px";_10.style.top=_14.y+"px";if(_14.overflow&&_12){_12(_10,_14.aroundCorner,_14.corner);}return _14;};_5.placeOnScreenAroundNode=function(_23,_24,_25,_26){_24=_4.byId(_24);var _27=_24.style.display;_24.style.display="";var _28=_24.offsetWidth;var _29=_24.offsetHeight;var _2a=_4.coords(_24,true);_24.style.display=_27;return _5._placeOnScreenAroundRect(_23,_2a.x,_2a.y,_28,_29,_25,_26);};_5.placeOnScreenAroundRectangle=function(_2b,_2c,_2d,_2e){return _5._placeOnScreenAroundRect(_2b,_2c.x,_2c.y,_2c.width,_2c.height,_2d,_2e);};_5._placeOnScreenAroundRect=function(_2f,x,y,_32,_33,_34,_35){var _36=[];for(var _37 in _34){_36.push({aroundCorner:_37,corner:_34[_37],pos:{x:x+(_37.charAt(1)=="L"?0:_32),y:y+(_37.charAt(0)=="T"?0:_33)}});}return _5._place(_2f,_36,_35);};_5.placementRegistry=new _4.AdapterRegistry();_5.placementRegistry.register("node",function(n,x){return typeof x=="object"&&typeof x.offsetWidth!="undefined"&&typeof x.offsetHeight!="undefined";},_5.placeOnScreenAroundNode);_5.placementRegistry.register("rect",function(n,x){return typeof x=="object"&&"x" in x&&"y" in x&&"width" in x&&"height" in x;},_5.placeOnScreenAroundRectangle);_5.placeOnScreenAroundElement=function(_3c,_3d,_3e,_3f){return _5.placementRegistry.match.apply(_5.placementRegistry,arguments);};}}};});