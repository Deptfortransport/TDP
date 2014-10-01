/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.dnd.Mover"],["require","dojo.dnd.common"],["require","dojo.dnd.autoscroll"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.dnd.Mover"]){_4._hasResource["dojo.dnd.Mover"]=true;_4.provide("dojo.dnd.Mover");_4.require("dojo.dnd.common");_4.require("dojo.dnd.autoscroll");_4.declare("dojo.dnd.Mover",null,{constructor:function(_7,e,_9){this.node=_4.byId(_7);this.marginBox={l:e.pageX,t:e.pageY};this.mouseButton=e.button;var h=this.host=_9,d=_7.ownerDocument,_c=_4.connect(d,"onmousemove",this,"onFirstMove");this.events=[_4.connect(d,"onmousemove",this,"onMouseMove"),_4.connect(d,"onmouseup",this,"onMouseUp"),_4.connect(d,"ondragstart",_4.stopEvent),_4.connect(d.body,"onselectstart",_4.stopEvent),_c];if(h&&h.onMoveStart){h.onMoveStart(this);}},onMouseMove:function(e){_4.dnd.autoScroll(e);var m=this.marginBox;this.host.onMove(this,{l:m.l+e.pageX,t:m.t+e.pageY});_4.stopEvent(e);},onMouseUp:function(e){if(_4.isWebKit&&_4.dnd._isMac&&this.mouseButton==2?e.button==0:this.mouseButton==e.button){this.destroy();}_4.stopEvent(e);},onFirstMove:function(){var s=this.node.style,l,t,h=this.host;switch(s.position){case "relative":case "absolute":l=Math.round(parseFloat(s.left));t=Math.round(parseFloat(s.top));break;default:s.position="absolute";var m=_4.marginBox(this.node);var b=_4.doc.body;var bs=_4.getComputedStyle(b);var bm=_4._getMarginBox(b,bs);var bc=_4._getContentBox(b,bs);l=m.l-(bc.l-bm.l);t=m.t-(bc.t-bm.t);break;}this.marginBox.l=l-this.marginBox.l;this.marginBox.t=t-this.marginBox.t;if(h&&h.onFirstMove){h.onFirstMove(this);}_4.disconnect(this.events.pop());},destroy:function(){_4.forEach(this.events,_4.disconnect);var h=this.host;if(h&&h.onMoveStop){h.onMoveStop(this);}this.events=this.node=this.host=null;}});}}};});