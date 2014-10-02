/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.oo.Filter"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.oo.Filter"]){_4._hasResource["dojox.lang.oo.Filter"]=true;_4.provide("dojox.lang.oo.Filter");(function(){var oo=_6.lang.oo,F=oo.Filter=function(_9,_a){this.bag=_9;this.filter=typeof _a=="object"?function(){return _a.exec.apply(_a,arguments);}:_a;};var _b=function(_c){this.map=_c;};_b.prototype.exec=function(_d){return this.map.hasOwnProperty(_d)?this.map[_d]:_d;};oo.filter=function(_e,_f){return new F(_e,new _b(_f));};})();}}};});