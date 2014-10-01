/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.fx.Toggler"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.fx.Toggler"]){_4._hasResource["dojo.fx.Toggler"]=true;_4.provide("dojo.fx.Toggler");_4.declare("dojo.fx.Toggler",null,{constructor:function(_7){var _t=this;_4.mixin(_t,_7);_t.node=_7.node;_t._showArgs=_4.mixin({},_7);_t._showArgs.node=_t.node;_t._showArgs.duration=_t.showDuration;_t.showAnim=_t.showFunc(_t._showArgs);_t._hideArgs=_4.mixin({},_7);_t._hideArgs.node=_t.node;_t._hideArgs.duration=_t.hideDuration;_t.hideAnim=_t.hideFunc(_t._hideArgs);_4.connect(_t.showAnim,"beforeBegin",_4.hitch(_t.hideAnim,"stop",true));_4.connect(_t.hideAnim,"beforeBegin",_4.hitch(_t.showAnim,"stop",true));},node:null,showFunc:_4.fadeIn,hideFunc:_4.fadeOut,showDuration:200,hideDuration:200,show:function(_9){return this.showAnim.play(_9||0);},hide:function(_a){return this.hideAnim.play(_a||0);}});}}};});