/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.fx._base"],["require","dojo.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.fx._base"]){_4._hasResource["dojox.fx._base"]=true;_4.provide("dojox.fx._base");_4.require("dojo.fx");_4.mixin(_6.fx,{anim:_4.anim,animateProperty:_4.animateProperty,fadeTo:_4._fade,fadeIn:_4.fadeIn,fadeOut:_4.fadeOut,combine:_4.fx.combine,chain:_4.fx.chain,slideTo:_4.fx.slideTo,wipeIn:_4.fx.wipeIn,wipeOut:_4.fx.wipeOut});_6.fx.sizeTo=function(_7){var _8=_7.node=_4.byId(_7.node);var _9=_7.method||"chain";if(!_7.duration){_7.duration=500;}if(_9=="chain"){_7.duration=Math.floor(_7.duration/2);}var _a,_b,_c,_d,_e,_f=null;var _10=(function(n){return function(){var cs=_4.getComputedStyle(n);var pos=cs.position;_a=(pos=="absolute"?n.offsetTop:parseInt(cs.top)||0);_c=(pos=="absolute"?n.offsetLeft:parseInt(cs.left)||0);_e=parseInt(cs.width);_f=parseInt(cs.height);_d=_c-Math.floor((_7.width-_e)/2);_b=_a-Math.floor((_7.height-_f)/2);if(pos!="absolute"&&pos!="relative"){var ret=_4.coords(n,true);_a=ret.y;_c=ret.x;n.style.position="absolute";n.style.top=_a+"px";n.style.left=_c+"px";}};})(_8);_10();var _15=_4.animateProperty(_4.mixin({properties:{height:{start:_f,end:_7.height||0,unit:"px"},top:{start:_a,end:_b}}},_7));var _16=_4.animateProperty(_4.mixin({properties:{width:{start:_e,end:_7.width||0,unit:"px"},left:{start:_c,end:_d}}},_7));var _17=_4.fx[(_7.method=="combine"?"combine":"chain")]([_15,_16]);_4.connect(_17,"beforeBegin",_17,_10);return _17;};_6.fx.slideBy=function(_18){var _19=_18.node=_4.byId(_18.node);var top=null;var _1b=null;var _1c=(function(n){return function(){var cs=_4.getComputedStyle(n);var pos=cs.position;top=(pos=="absolute"?n.offsetTop:parseInt(cs.top)||0);_1b=(pos=="absolute"?n.offsetLeft:parseInt(cs.left)||0);if(pos!="absolute"&&pos!="relative"){var ret=_4.coords(n,true);top=ret.y;_1b=ret.x;n.style.position="absolute";n.style.top=top+"px";n.style.left=_1b+"px";}};})(_19);_1c();var _21=_4.animateProperty(_4.mixin({properties:{top:top+(_18.top||0),left:_1b+(_18.left||0)}},_18));_4.connect(_21,"beforeBegin",_21,_1c);return _21;};_6.fx.crossFade=function(_22){if(_4.isArray(_22.nodes)){var _23=_22.nodes[0]=_4.byId(_22.nodes[0]);var op1=_4.style(_23,"opacity");var _25=_22.nodes[1]=_4.byId(_22.nodes[1]);var op2=_4.style(_25,"opacity");var _27=_4.fx.combine([_4[(op1==0?"fadeIn":"fadeOut")](_4.mixin({node:_23},_22)),_4[(op1==0?"fadeOut":"fadeIn")](_4.mixin({node:_25},_22))]);return _27;}else{return false;}};_6.fx.highlight=function(_28){var _29=_28.node=_4.byId(_28.node);_28.duration=_28.duration||400;var _2a=_28.color||"#ffff99";var _2b=_4.style(_29,"backgroundColor");var _2c=(_2b=="transparent"||_2b=="rgba(0, 0, 0, 0)")?_2b:false;var _2d=_4.animateProperty(_4.mixin({properties:{backgroundColor:{start:_2a,end:_2b}}},_28));if(_2c){_4.connect(_2d,"onEnd",_2d,function(){_29.style.backgroundColor=_2c;});}return _2d;};_6.fx.wipeTo=function(_2e){_2e.node=_4.byId(_2e.node);var _2f=_2e.node,s=_2f.style;var dir=(_2e.width?"width":"height");var _32=_2e[dir];var _33={};_33[dir]={start:function(){s.overflow="hidden";if(s.visibility=="hidden"||s.display=="none"){s[dir]="1px";s.display="";s.visibility="";return 1;}else{var now=_4.style(_2f,dir);return Math.max(now,1);}},end:_32,unit:"px"};var _35=_4.animateProperty(_4.mixin({properties:_33},_2e));return _35;};}}};});