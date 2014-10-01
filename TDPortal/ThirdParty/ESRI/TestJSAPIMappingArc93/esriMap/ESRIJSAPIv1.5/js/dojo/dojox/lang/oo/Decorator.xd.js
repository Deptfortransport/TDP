/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.oo.Decorator"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.oo.Decorator"]){_4._hasResource["dojox.lang.oo.Decorator"]=true;_4.provide("dojox.lang.oo.Decorator");(function(){var oo=_6.lang.oo,D=oo.Decorator=function(_9,_a){this.value=_9;this.decorator=typeof _a=="object"?function(){return _a.exec.apply(_a,arguments);}:_a;};oo.makeDecorator=function(_b){return function(_c){return new D(_c,_b);};};})();}}};});