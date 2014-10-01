/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.dtl.ext-dojo.NodeList"],["require","dojox.dtl._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.dtl.ext-dojo.NodeList"]){_4._hasResource["dojox.dtl.ext-dojo.NodeList"]=true;_4.provide("dojox.dtl.ext-dojo.NodeList");_4.require("dojox.dtl._base");_4.extend(_4.NodeList,{dtl:function(_7,_8){var d=_6.dtl;var _a=this;var _b=function(_c,_d){var _e=_c.render(new d._Context(_d));_a.forEach(function(_f){_f.innerHTML=_e;});};d.text._resolveTemplateArg(_7).addCallback(function(_10){_7=new d.Template(_10);d.text._resolveContextArg(_8).addCallback(function(_11){_b(_7,_11);});});return this;}});}}};});