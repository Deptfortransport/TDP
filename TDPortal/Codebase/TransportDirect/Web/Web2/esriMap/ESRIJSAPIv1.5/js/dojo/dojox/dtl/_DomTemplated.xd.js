/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.dtl._DomTemplated"],["require","dijit._Templated"],["require","dojox.dtl.dom"],["require","dojox.dtl.render.dom"],["require","dojox.dtl.contrib.dijit"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.dtl._DomTemplated"]){_4._hasResource["dojox.dtl._DomTemplated"]=true;_4.provide("dojox.dtl._DomTemplated");_4.require("dijit._Templated");_4.require("dojox.dtl.dom");_4.require("dojox.dtl.render.dom");_4.require("dojox.dtl.contrib.dijit");_6.dtl._DomTemplated={prototype:{_dijitTemplateCompat:false,buildRendering:function(){this.domNode=this.srcNodeRef;if(!this._render){var _7=_6.dtl.contrib.dijit;var _8=_7.widgetsInTemplate;_7.widgetsInTemplate=this.widgetsInTemplate;this.template=this.template||this._getCachedTemplate(this.templatePath,this.templateString);this._render=new _6.dtl.render.dom.Render(this.domNode,this.template);_7.widgetsInTemplate=_8;}this.render();this.domNode=this.template.getRootNode();if(this.srcNodeRef&&this.srcNodeRef.parentNode){_4.destroy(this.srcNodeRef);delete this.srcNodeRef;}},setTemplate:function(_9,_a){if(_6.dtl.text._isTemplate(_9)){this.template=this._getCachedTemplate(null,_9);}else{this.template=this._getCachedTemplate(_9);}this.render(_a);},render:function(_b,_c){if(_c){this.template=_c;}this._render.render(this._getContext(_b),this.template);},_getContext:function(_d){if(!(_d instanceof _6.dtl.Context)){_d=false;}_d=_d||new _6.dtl.Context(this);_d.setThis(this);return _d;},_getCachedTemplate:function(_e,_f){if(!this._templates){this._templates={};}var key=_f||_e.toString();var _11=this._templates;if(_11[key]){return _11[key];}return (_11[key]=new _6.dtl.DomTemplate(_5._Templated.getCachedTemplate(_e,_f,true)));}}};}}};});