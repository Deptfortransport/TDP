/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._Container"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._Container"]){_4._hasResource["dijit._Container"]=true;_4.provide("dijit._Container");_4.declare("dijit._Container",null,{isContainer:true,buildRendering:function(){this.inherited(arguments);if(!this.containerNode){this.containerNode=this.domNode;}},addChild:function(_7,_8){var _9=this.containerNode;if(_8&&typeof _8=="number"){var _a=this.getChildren();if(_a&&_a.length>=_8){_9=_a[_8-1].domNode;_8="after";}}_4.place(_7.domNode,_9,_8);if(this._started&&!_7._started){_7.startup();}},removeChild:function(_b){if(typeof _b=="number"&&_b>0){_b=this.getChildren()[_b];}if(!_b||!_b.domNode){return;}var _c=_b.domNode;_c.parentNode.removeChild(_c);},_nextElement:function(_d){do{_d=_d.nextSibling;}while(_d&&_d.nodeType!=1);return _d;},_firstElement:function(_e){_e=_e.firstChild;if(_e&&_e.nodeType!=1){_e=this._nextElement(_e);}return _e;},getChildren:function(){return _4.query("> [widgetId]",this.containerNode).map(_5.byNode);},hasChildren:function(){return !!this._firstElement(this.containerNode);},destroyDescendants:function(_f){_4.forEach(this.getChildren(),function(_10){_10.destroyRecursive(_f);});},_getSiblingOfChild:function(_11,dir){var _13=_11.domNode;var _14=(dir>0?"nextSibling":"previousSibling");do{_13=_13[_14];}while(_13&&(_13.nodeType!=1||!_5.byNode(_13)));return _13?_5.byNode(_13):null;},getIndexOfChild:function(_15){var _16=this.getChildren();for(var i=0,c;c=_16[i];i++){if(c==_15){return i;}}return -1;}});}}};});