/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.oo.general"],["require","dojox.lang.oo.Decorator"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.oo.general"]){_4._hasResource["dojox.lang.oo.general"]=true;_4.provide("dojox.lang.oo.general");_4.require("dojox.lang.oo.Decorator");(function(){var oo=_6.lang.oo,md=oo.makeDecorator,_9=oo.general;_9.augment=md(function(_a,_b,_c){return typeof _c=="undefined"?_b:_c;});_9.override=md(function(_d,_e,_f){return typeof _f!="undefined"?_e:_f;});_9.shuffle=md(function(_10,_11,_12){return _4.isFunction(_12)?function(){return _12.apply(this,_11.apply(this,arguments));}:_12;});_9.wrap=md(function(_13,_14,_15){return function(){return _14.call(this,_15,arguments);};});_9.tap=md(function(_16,_17,_18){return function(){_17.apply(this,arguments);return this;};});_9.before=md(function(_19,_1a,_1b){return _4.isFunction(_1b)?function(){_1a.apply(this,arguments);return _1b.apply(this,arguments);}:_1a;});_9.after=md(function(_1c,_1d,_1e){return _4.isFunction(_1e)?function(){_1e.apply(this,arguments);return _1d.apply(this,arguments);}:_1d;});})();}}};});