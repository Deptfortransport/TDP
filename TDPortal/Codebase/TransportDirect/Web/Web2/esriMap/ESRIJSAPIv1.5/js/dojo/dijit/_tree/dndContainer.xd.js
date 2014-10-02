/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._tree.dndContainer"],["require","dojo.dnd.common"],["require","dojo.dnd.Container"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._tree.dndContainer"]){_4._hasResource["dijit._tree.dndContainer"]=true;_4.provide("dijit._tree.dndContainer");_4.require("dojo.dnd.common");_4.require("dojo.dnd.Container");_4.declare("dijit._tree.dndContainer",null,{constructor:function(_7,_8){this.tree=_7;this.node=_7.domNode;_4.mixin(this,_8);this.map={};this.current=null;this.containerState="";_4.addClass(this.node,"dojoDndContainer");if(!(_8&&_8._skipStartup)){this.startup();}this.events=[_4.connect(this.node,"onmouseenter",this,"onOverEvent"),_4.connect(this.node,"onmouseleave",this,"onOutEvent"),_4.connect(this.tree,"_onNodeMouseEnter",this,"onMouseOver"),_4.connect(this.tree,"_onNodeMouseLeave",this,"onMouseOut"),_4.connect(this.node,"ondragstart",_4,"stopEvent"),_4.connect(this.node,"onselectstart",_4,"stopEvent")];},getItem:function(_9){return this.selection[_9];},destroy:function(){_4.forEach(this.events,_4.disconnect);this.node=this.parent=null;},onMouseOver:function(_a,_b){this.current=_a.rowNode;this.currentWidget=_a;},onMouseOut:function(_c,_d){this.current=null;this.currentWidget=null;},_changeState:function(_e,_f){var _10="dojoDnd"+_e;var _11=_e.toLowerCase()+"State";_4.removeClass(this.node,_10+this[_11]);_4.addClass(this.node,_10+_f);this[_11]=_f;},_addItemClass:function(_12,_13){_4.addClass(_12,"dojoDndItem"+_13);},_removeItemClass:function(_14,_15){_4.removeClass(_14,"dojoDndItem"+_15);},onOverEvent:function(){this._changeState("Container","Over");},onOutEvent:function(){this._changeState("Container","");}});}}};});