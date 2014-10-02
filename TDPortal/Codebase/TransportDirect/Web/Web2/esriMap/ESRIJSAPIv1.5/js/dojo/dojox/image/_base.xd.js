/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.image._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.image._base"]){_4._hasResource["dojox.image._base"]=true;_4.provide("dojox.image._base");(function(d){var _8;_6.image.preload=function(_9){if(!_8){_8=d.create("div",{style:{position:"absolute",top:"-9999px",display:"none"}},d.body());}d.forEach(_9,function(_a){d.create("img",{src:_a},_8);});};if(d.config.preloadImages){d.addOnLoad(function(){_6.image.preload(d.config.preloadImages);});}})(_4);}}};});