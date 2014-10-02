/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.layout.dnd.Avatar"],["require","dojo.dnd.common"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.layout.dnd.Avatar"]){_4._hasResource["dojox.layout.dnd.Avatar"]=true;_4.provide("dojox.layout.dnd.Avatar");_4.require("dojo.dnd.common");_6.layout.dnd.Avatar=function(_7,_8){this.manager=_7;this.construct(_8);};_4.extend(_6.layout.dnd.Avatar,{construct:function(_9){var _a=this.manager.source;var _b=(_a.creator)?_a._normalizedCreator(_a.getItem(this.manager.nodes[0].id).data,"avatar").node:this.manager.nodes[0].cloneNode(true);_b.id=_4.dnd.getUniqueId();_4.addClass(_b,"dojoDndAvatar");_b.style.position="absolute";_b.style.zIndex=1999;_b.style.margin="0px";_b.style.width=_4.marginBox(_a.node).w+"px";_4.style(_b,"opacity",_9);this.node=_b;},destroy:function(){_4.destroy(this.node);this.node=false;},update:function(){_4[(this.manager.canDropFlag?"add":"remove")+"Class"](this.node,"dojoDndAvatarCanDrop");},_generateText:function(){}});}}};});