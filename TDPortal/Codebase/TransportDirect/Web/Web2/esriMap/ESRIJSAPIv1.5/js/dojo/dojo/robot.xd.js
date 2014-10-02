/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.robot"],["require","doh.robot"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.robot"]){_4._hasResource["dojo.robot"]=true;_4.provide("dojo.robot");_4.experimental("dojo.robot");_4.require("doh.robot");(function(){_4.mixin(doh.robot,{_scrollIntoView:function(_7){if(typeof _7=="function"){_7=_7();}_7.scrollIntoView(false);},scrollIntoView:function(_8,_9){doh.robot.sequence(function(){doh.robot._scrollIntoView(_8);},_9);},mouseMoveAt:function(_a,_b,_c,_d,_e){doh.robot._assertRobot();_c=_c||100;this.sequence(function(){if(typeof _a=="function"){_a=_a();}if(!_a){return;}_a=_4.byId(_a);if(_e===undefined){var _f=_4.contentBox(_a);_d=_f.w/2;_e=_f.h/2;}var x=_d;var y=_e;doh.robot._scrollIntoView(_a);var c=_4.coords(_a);x+=c.x;y+=c.y;doh.robot._mouseMove(x,y,false,_c);},_b,_c);}});})();}}};});