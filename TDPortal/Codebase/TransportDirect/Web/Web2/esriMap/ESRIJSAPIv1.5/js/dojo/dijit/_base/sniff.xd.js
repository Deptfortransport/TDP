/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._base.sniff"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._base.sniff"]){_4._hasResource["dijit._base.sniff"]=true;_4.provide("dijit._base.sniff");(function(){var d=_4,_8=d.doc.documentElement,ie=d.isIE,_a=d.isOpera,_b=Math.floor,ff=d.isFF,_d=d.boxModel.replace(/-/,""),_e={dj_ie:ie,dj_ie6:_b(ie)==6,dj_ie7:_b(ie)==7,dj_iequirks:ie&&d.isQuirks,dj_opera:_a,dj_opera8:_b(_a)==8,dj_opera9:_b(_a)==9,dj_khtml:d.isKhtml,dj_webkit:d.isWebKit,dj_safari:d.isSafari,dj_gecko:d.isMozilla,dj_ff2:_b(ff)==2,dj_ff3:_b(ff)==3};_e["dj_"+_d]=true;for(var p in _e){if(_e[p]){if(_8.className){_8.className+=" "+p;}else{_8.className=p;}}}_4._loaders.unshift(function(){if(!_4._isBodyLtr()){_8.className+=" dijitRtl";for(var p in _e){if(_e[p]){_8.className+=" "+p+"-rtl";}}}});})();}}};});