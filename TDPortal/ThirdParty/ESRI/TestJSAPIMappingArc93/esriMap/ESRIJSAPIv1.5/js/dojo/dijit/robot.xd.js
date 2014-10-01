/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.robot"],["require","dojo.robot"],["require","dijit._base.scroll"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.robot"]){_4._hasResource["dijit.robot"]=true;_4.provide("dijit.robot");_4.require("dojo.robot");_4.require("dijit._base.scroll");_4.mixin(doh.robot,{_scrollIntoView:function(_7){if(typeof _7=="function"){_7=_7();}_5.scrollIntoView(_7);}});}}};});