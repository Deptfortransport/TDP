/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.axis2d.common"],["require","dojox.gfx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.axis2d.common"]){_4._hasResource["dojox.charting.axis2d.common"]=true;_4.provide("dojox.charting.axis2d.common");_4.require("dojox.gfx");(function(){var g=_6.gfx;function _8(s){s.marginLeft="0px";s.marginTop="0px";s.marginRight="0px";s.marginBottom="0px";s.paddingLeft="0px";s.paddingTop="0px";s.paddingRight="0px";s.paddingBottom="0px";s.borderLeftWidth="0px";s.borderTopWidth="0px";s.borderRightWidth="0px";s.borderBottomWidth="0px";};_4.mixin(_6.charting.axis2d.common,{createText:{gfx:function(_a,_b,x,y,_e,_f,_10,_11){return _b.createText({x:x,y:y,text:_f,align:_e}).setFont(_10).setFill(_11);},html:function(_12,_13,x,y,_16,_17,_18,_19){var p=_4.doc.createElement("div"),s=p.style;_8(s);s.font=_18;p.innerHTML=String(_17).replace(/\s/g,"&nbsp;");s.color=_19;s.position="absolute";s.left="-10000px";_4.body().appendChild(p);var _1c=g.normalizedLength(g.splitFontString(_18).size),box=_4.marginBox(p);_4.body().removeChild(p);s.position="relative";switch(_16){case "middle":s.left=Math.floor(x-box.w/2)+"px";break;case "end":s.left=Math.floor(x-box.w)+"px";break;default:s.left=Math.floor(x)+"px";break;}s.top=Math.floor(y-_1c)+"px";var _1e=_4.doc.createElement("div"),w=_1e.style;_8(w);w.width="0px";w.height="0px";_1e.appendChild(p);_12.node.insertBefore(_1e,_12.node.firstChild);return _1e;}}});})();}}};});