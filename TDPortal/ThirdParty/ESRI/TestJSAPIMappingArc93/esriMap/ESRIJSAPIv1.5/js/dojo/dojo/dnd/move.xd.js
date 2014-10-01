/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.dnd.move"],["require","dojo.dnd.Mover"],["require","dojo.dnd.Moveable"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.dnd.move"]){_4._hasResource["dojo.dnd.move"]=true;_4.provide("dojo.dnd.move");_4.require("dojo.dnd.Mover");_4.require("dojo.dnd.Moveable");_4.declare("dojo.dnd.move.constrainedMoveable",_4.dnd.Moveable,{constraints:function(){},within:false,markupFactory:function(_7,_8){return new _4.dnd.move.constrainedMoveable(_8,_7);},constructor:function(_9,_a){if(!_a){_a={};}this.constraints=_a.constraints;this.within=_a.within;},onFirstMove:function(_b){var c=this.constraintBox=this.constraints.call(this,_b);c.r=c.l+c.w;c.b=c.t+c.h;if(this.within){var mb=_4.marginBox(_b.node);c.r-=mb.w;c.b-=mb.h;}},onMove:function(_e,_f){var c=this.constraintBox,s=_e.node.style;s.left=(_f.l<c.l?c.l:c.r<_f.l?c.r:_f.l)+"px";s.top=(_f.t<c.t?c.t:c.b<_f.t?c.b:_f.t)+"px";}});_4.declare("dojo.dnd.move.boxConstrainedMoveable",_4.dnd.move.constrainedMoveable,{box:{},markupFactory:function(_12,_13){return new _4.dnd.move.boxConstrainedMoveable(_13,_12);},constructor:function(_14,_15){var box=_15&&_15.box;this.constraints=function(){return box;};}});_4.declare("dojo.dnd.move.parentConstrainedMoveable",_4.dnd.move.constrainedMoveable,{area:"content",markupFactory:function(_17,_18){return new _4.dnd.move.parentConstrainedMoveable(_18,_17);},constructor:function(_19,_1a){var _1b=_1a&&_1a.area;this.constraints=function(){var n=this.node.parentNode,s=_4.getComputedStyle(n),mb=_4._getMarginBox(n,s);if(_1b=="margin"){return mb;}var t=_4._getMarginExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;if(_1b=="border"){return mb;}t=_4._getBorderExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;if(_1b=="padding"){return mb;}t=_4._getPadExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;return mb;};}});_4.dnd.move.constrainedMover=function(fun,_21){_4.deprecated("dojo.dnd.move.constrainedMover, use dojo.dnd.move.constrainedMoveable instead");var _22=function(_23,e,_25){_4.dnd.Mover.call(this,_23,e,_25);};_4.extend(_22,_4.dnd.Mover.prototype);_4.extend(_22,{onMouseMove:function(e){_4.dnd.autoScroll(e);var m=this.marginBox,c=this.constraintBox,l=m.l+e.pageX,t=m.t+e.pageY;l=l<c.l?c.l:c.r<l?c.r:l;t=t<c.t?c.t:c.b<t?c.b:t;this.host.onMove(this,{l:l,t:t});},onFirstMove:function(){_4.dnd.Mover.prototype.onFirstMove.call(this);var c=this.constraintBox=fun.call(this);c.r=c.l+c.w;c.b=c.t+c.h;if(_21){var mb=_4.marginBox(this.node);c.r-=mb.w;c.b-=mb.h;}}});return _22;};_4.dnd.move.boxConstrainedMover=function(box,_2e){_4.deprecated("dojo.dnd.move.boxConstrainedMover, use dojo.dnd.move.boxConstrainedMoveable instead");return _4.dnd.move.constrainedMover(function(){return box;},_2e);};_4.dnd.move.parentConstrainedMover=function(_2f,_30){_4.deprecated("dojo.dnd.move.parentConstrainedMover, use dojo.dnd.move.parentConstrainedMoveable instead");var fun=function(){var n=this.node.parentNode,s=_4.getComputedStyle(n),mb=_4._getMarginBox(n,s);if(_2f=="margin"){return mb;}var t=_4._getMarginExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;if(_2f=="border"){return mb;}t=_4._getBorderExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;if(_2f=="padding"){return mb;}t=_4._getPadExtents(n,s);mb.l+=t.l,mb.t+=t.t,mb.w-=t.w,mb.h-=t.h;return mb;};return _4.dnd.move.constrainedMover(fun,_30);};_4.dnd.constrainedMover=_4.dnd.move.constrainedMover;_4.dnd.boxConstrainedMover=_4.dnd.move.boxConstrainedMover;_4.dnd.parentConstrainedMover=_4.dnd.move.parentConstrainedMover;}}};});