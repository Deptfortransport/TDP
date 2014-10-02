/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._Templated"],["require","dijit._Widget"],["require","dojo.string"],["require","dojo.parser"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._Templated"]){_4._hasResource["dijit._Templated"]=true;_4.provide("dijit._Templated");_4.require("dijit._Widget");_4.require("dojo.string");_4.require("dojo.parser");_4.declare("dijit._Templated",null,{templateString:null,templatePath:null,widgetsInTemplate:false,_skipNodeCache:false,_stringRepl:function(_7){var _8=this.declaredClass,_9=this;return _4.string.substitute(_7,this,function(_a,_b){if(_b.charAt(0)=="!"){_a=_4.getObject(_b.substr(1),false,_9);}if(typeof _a=="undefined"){throw new Error(_8+" template:"+_b);}if(_a==null){return "";}return _b.charAt(0)=="!"?_a:_a.toString().replace(/"/g,"&quot;");},this);},buildRendering:function(){var _c=_5._Templated.getCachedTemplate(this.templatePath,this.templateString,this._skipNodeCache);var _d;if(_4.isString(_c)){_d=_4._toDom(this._stringRepl(_c));}else{_d=_c.cloneNode(true);}this.domNode=_d;this._attachTemplateNodes(_d);if(this.widgetsInTemplate){var cw=(this._supportingWidgets=_4.parser.parse(_d));this._attachTemplateNodes(cw,function(n,p){return n[p];});}this._fillContent(this.srcNodeRef);},_fillContent:function(_11){var _12=this.containerNode;if(_11&&_12){while(_11.hasChildNodes()){_12.appendChild(_11.firstChild);}}},_attachTemplateNodes:function(_13,_14){_14=_14||function(n,p){return n.getAttribute(p);};var _17=_4.isArray(_13)?_13:(_13.all||_13.getElementsByTagName("*"));var x=_4.isArray(_13)?0:-1;for(;x<_17.length;x++){var _19=(x==-1)?_13:_17[x];if(this.widgetsInTemplate&&_14(_19,"dojoType")){continue;}var _1a=_14(_19,"dojoAttachPoint");if(_1a){var _1b,_1c=_1a.split(/\s*,\s*/);while((_1b=_1c.shift())){if(_4.isArray(this[_1b])){this[_1b].push(_19);}else{this[_1b]=_19;}}}var _1d=_14(_19,"dojoAttachEvent");if(_1d){var _1e,_1f=_1d.split(/\s*,\s*/);var _20=_4.trim;while((_1e=_1f.shift())){if(_1e){var _21=null;if(_1e.indexOf(":")!=-1){var _22=_1e.split(":");_1e=_20(_22[0]);_21=_20(_22[1]);}else{_1e=_20(_1e);}if(!_21){_21=_1e;}this.connect(_19,_1e,_21);}}}var _23=_14(_19,"waiRole");if(_23){_5.setWaiRole(_19,_23);}var _24=_14(_19,"waiState");if(_24){_4.forEach(_24.split(/\s*,\s*/),function(_25){if(_25.indexOf("-")!=-1){var _26=_25.split("-");_5.setWaiState(_19,_26[0],_26[1]);}});}}}});_5._Templated._templateCache={};_5._Templated.getCachedTemplate=function(_27,_28,_29){var _2a=_5._Templated._templateCache;var key=_28||_27;var _2c=_2a[key];if(_2c){if(!_2c.ownerDocument||_2c.ownerDocument==_4.doc){return _2c;}_4.destroy(_2c);}if(!_28){_28=_5._Templated._sanitizeTemplateString(_4.trim(_4._getText(_27)));}_28=_4.string.trim(_28);if(_29||_28.match(/\$\{([^\}]+)\}/g)){return (_2a[key]=_28);}else{return (_2a[key]=_4._toDom(_28));}};_5._Templated._sanitizeTemplateString=function(_2d){if(_2d){_2d=_2d.replace(/^\s*<\?xml(\s)+version=[\'\"](\d)*.(\d)*[\'\"](\s)*\?>/im,"");var _2e=_2d.match(/<body[^>]*>\s*([\s\S]+)\s*<\/body>/im);if(_2e){_2d=_2e[1];}}else{_2d="";}return _2d;};if(_4.isIE){_4.addOnWindowUnload(function(){var _2f=_5._Templated._templateCache;for(var key in _2f){var _31=_2f[key];if(!isNaN(_31.nodeType)){_4.destroy(_31);}delete _2f[key];}});}_4.extend(_5._Widget,{dojoAttachEvent:"",dojoAttachPoint:"",waiRole:"",waiState:""});}}};});