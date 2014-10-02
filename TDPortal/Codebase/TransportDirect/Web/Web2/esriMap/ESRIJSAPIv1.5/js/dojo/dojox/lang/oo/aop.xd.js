/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.oo.aop"],["require","dojox.lang.oo.Decorator"],["require","dojox.lang.oo.chain"],["require","dojox.lang.oo.general"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.oo.aop"]){_4._hasResource["dojox.lang.oo.aop"]=true;_4.provide("dojox.lang.oo.aop");_4.require("dojox.lang.oo.Decorator");_4.require("dojox.lang.oo.chain");_4.require("dojox.lang.oo.general");(function(){var oo=_6.lang.oo,md=oo.makeDecorator,_9=oo.aop;_9.before=oo.chain.before;_9.around=oo.general.wrap;_9.afterReturning=md(function(_a,_b,_c){return _4.isFunction(_c)?function(){var _d=_c.apply(this,arguments);_b.call(this,_d);return _d;}:function(){_b.call(this);};});_9.afterThrowing=md(function(_e,_f,_10){return _4.isFunction(_10)?function(){var ret;try{ret=_10.apply(this,arguments);}catch(e){_f.call(this,e);throw e;}return ret;}:_10;});_9.after=md(function(_12,_13,_14){return _4.isFunction(_14)?function(){var ret;try{ret=_14.apply(this,arguments);}finally{_13.call(this);}return ret;}:function(){_13.call(this);};});})();}}};});