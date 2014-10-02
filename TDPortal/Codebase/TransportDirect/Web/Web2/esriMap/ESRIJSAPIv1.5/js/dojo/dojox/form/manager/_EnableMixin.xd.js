/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.form.manager._EnableMixin"],["require","dojox.form.manager._Mixin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.form.manager._EnableMixin"]){_4._hasResource["dojox.form.manager._EnableMixin"]=true;_4.provide("dojox.form.manager._EnableMixin");_4.require("dojox.form.manager._Mixin");(function(){var fm=_6.form.manager,aa=fm.actionAdapter,ia=fm.inspectorAdapter;_4.declare("dojox.form.manager._EnableMixin",null,{gatherEnableState:function(_a){var _b=this.inspectFormWidgets(ia(function(_c,_d){return !_d.attr("disabled");}),_a);if(this.inspectFormNodes){_4.mixin(_b,this.inspectFormNodes(ia(function(_e,_f){return !_4.attr(_f,"disabled");}),_a));}return _b;},enable:function(_10,_11){if(arguments.length<2||_11===undefined){_11=true;}this.inspectFormWidgets(aa(function(_12,_13,_14){_13.attr("disabled",!_14);}),_10,_11);if(this.inspectFormNodes){this.inspectFormNodes(aa(function(_15,_16,_17){_4.attr(_16,"disabled",!_17);}),_10,_11);}return this;},disable:function(_18){var _19=this.gatherEnableState();this.enable(_18,false);return _19;}});})();}}};});