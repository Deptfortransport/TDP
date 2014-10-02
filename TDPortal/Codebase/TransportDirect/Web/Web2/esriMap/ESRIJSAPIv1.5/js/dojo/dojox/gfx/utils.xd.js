/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.gfx.utils"],["require","dojox.gfx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.gfx.utils"]){_4._hasResource["dojox.gfx.utils"]=true;_4.provide("dojox.gfx.utils");_4.require("dojox.gfx");(function(){var d=_4,g=_6.gfx,gu=g.utils;_4.mixin(gu,{forEach:function(_a,f,o){o=o||d.global;f.call(o,_a);if(_a instanceof g.Surface||_a instanceof g.Group){d.forEach(_a.children,function(_d){gu.forEach(_d,f,o);});}},serialize:function(_e){var t={},v,_11=_e instanceof g.Surface;if(_11||_e instanceof g.Group){t.children=d.map(_e.children,gu.serialize);if(_11){return t.children;}}else{t.shape=_e.getShape();}if(_e.getTransform){v=_e.getTransform();if(v){t.transform=v;}}if(_e.getStroke){v=_e.getStroke();if(v){t.stroke=v;}}if(_e.getFill){v=_e.getFill();if(v){t.fill=v;}}if(_e.getFont){v=_e.getFont();if(v){t.font=v;}}return t;},toJson:function(_12,_13){return d.toJson(gu.serialize(_12),_13);},deserialize:function(_14,_15){if(_15 instanceof Array){return d.map(_15,d.hitch(null,gu.deserialize,_14));}var _16=("shape" in _15)?_14.createShape(_15.shape):_14.createGroup();if("transform" in _15){_16.setTransform(_15.transform);}if("stroke" in _15){_16.setStroke(_15.stroke);}if("fill" in _15){_16.setFill(_15.fill);}if("font" in _15){_16.setFont(_15.font);}if("children" in _15){d.forEach(_15.children,d.hitch(null,gu.deserialize,_16));}return _16;},fromJson:function(_17,_18){return gu.deserialize(_17,d.fromJson(_18));}});})();}}};});