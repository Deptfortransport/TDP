/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._Contained"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._Contained"]){_4._hasResource["dijit._Contained"]=true;_4.provide("dijit._Contained");_4.declare("dijit._Contained",null,{getParent:function(){for(var p=this.domNode.parentNode;p;p=p.parentNode){var id=p.getAttribute&&p.getAttribute("widgetId");if(id){var _9=_5.byId(id);return _9.isContainer?_9:null;}}return null;},_getSibling:function(_a){var _b=this.domNode;do{_b=_b[_a+"Sibling"];}while(_b&&_b.nodeType!=1);if(!_b){return null;}var id=_b.getAttribute("widgetId");return _5.byId(id);},getPreviousSibling:function(){return this._getSibling("previous");},getNextSibling:function(){return this._getSibling("next");},getIndexInParent:function(){var p=this.getParent();if(!p||!p.getIndexOfChild){return -1;}return p.getIndexOfChild(this);}});}}};});