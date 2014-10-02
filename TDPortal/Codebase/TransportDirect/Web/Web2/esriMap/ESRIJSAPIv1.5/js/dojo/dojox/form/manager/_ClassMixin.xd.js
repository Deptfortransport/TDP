/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.form.manager._ClassMixin"],["require","dojox.form.manager._Mixin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.form.manager._ClassMixin"]){_4._hasResource["dojox.form.manager._ClassMixin"]=true;_4.provide("dojox.form.manager._ClassMixin");_4.require("dojox.form.manager._Mixin");(function(){var fm=_6.form.manager,aa=fm.actionAdapter,ia=fm.inspectorAdapter;_4.declare("dojox.form.manager._ClassMixin",null,{gatherClassState:function(_a,_b){var _c=this.inspect(ia(function(_d,_e){return _4.hasClass(_e,_a);}),_b);return _c;},addClass:function(_f,_10){this.inspect(aa(function(_11,_12){_4.addClass(_12,_f);}),_10);return this;},removeClass:function(_13,_14){this.inspect(aa(function(_15,_16){_4.removeClass(_16,_13);}),_14);return this;}});})();}}};});