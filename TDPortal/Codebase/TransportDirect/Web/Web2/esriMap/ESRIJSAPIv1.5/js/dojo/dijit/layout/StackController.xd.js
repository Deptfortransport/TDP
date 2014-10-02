/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.layout.StackController"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dijit._Container"],["require","dijit.form.ToggleButton"],["require","dijit.Menu"],["requireLocalization","dijit","common",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.layout.StackController"]){_4._hasResource["dijit.layout.StackController"]=true;_4.provide("dijit.layout.StackController");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dijit._Container");_4.require("dijit.form.ToggleButton");_4.require("dijit.Menu");_4.declare("dijit.layout.StackController",[_5._Widget,_5._Templated,_5._Container],{templateString:"<span wairole='tablist' dojoAttachEvent='onkeypress' class='dijitStackController'></span>",containerId:"",buttonWidget:"dijit.layout._StackButton",postCreate:function(){_5.setWaiRole(this.domNode,"tablist");this.pane2button={};this.pane2handles={};this.pane2menu={};this._subscriptions=[_4.subscribe(this.containerId+"-startup",this,"onStartup"),_4.subscribe(this.containerId+"-addChild",this,"onAddChild"),_4.subscribe(this.containerId+"-removeChild",this,"onRemoveChild"),_4.subscribe(this.containerId+"-selectChild",this,"onSelectChild"),_4.subscribe(this.containerId+"-containerKeyPress",this,"onContainerKeyPress")];},onStartup:function(_7){_4.forEach(_7.children,this.onAddChild,this);this.onSelectChild(_7.selected);},destroy:function(){for(var _8 in this.pane2button){this.onRemoveChild(_8);}_4.forEach(this._subscriptions,_4.unsubscribe);this.inherited(arguments);},onAddChild:function(_9,_a){var _b=_4.doc.createElement("span");this.domNode.appendChild(_b);var _c=_4.getObject(this.buttonWidget);var _d=new _c({label:_9.title,closeButton:_9.closable},_b);this.addChild(_d,_a);this.pane2button[_9]=_d;_9.controlButton=_d;var _e=[];_e.push(_4.connect(_d,"onClick",_4.hitch(this,"onButtonClick",_9)));if(_9.closable){_e.push(_4.connect(_d,"onClickCloseButton",_4.hitch(this,"onCloseButtonClick",_9)));var _f=_4.i18n.getLocalization("dijit","common");var _10=new _5.Menu({targetNodeIds:[_d.id],id:_d.id+"_Menu"});var _11=new _5.MenuItem({label:_f.itemClose});_e.push(_4.connect(_11,"onClick",_4.hitch(this,"onCloseButtonClick",_9)));_10.addChild(_11);this.pane2menu[_9]=_10;}this.pane2handles[_9]=_e;if(!this._currentChild){_d.focusNode.setAttribute("tabIndex","0");this._currentChild=_9;}if(!this.isLeftToRight()&&_4.isIE&&this._rectifyRtlTabList){this._rectifyRtlTabList();}},onRemoveChild:function(_12){if(this._currentChild===_12){this._currentChild=null;}_4.forEach(this.pane2handles[_12],_4.disconnect);delete this.pane2handles[_12];var _13=this.pane2menu[_12];if(_13){_13.destroyRecursive();delete this.pane2menu[_12];}var _14=this.pane2button[_12];if(_14){_14.destroy();delete this.pane2button[_12];}},onSelectChild:function(_15){if(!_15){return;}if(this._currentChild){var _16=this.pane2button[this._currentChild];_16.attr("checked",false);_16.focusNode.setAttribute("tabIndex","-1");}var _17=this.pane2button[_15];_17.attr("checked",true);this._currentChild=_15;_17.focusNode.setAttribute("tabIndex","0");var _18=_5.byId(this.containerId);_5.setWaiState(_18.containerNode,"labelledby",_17.id);},onButtonClick:function(_19){var _1a=_5.byId(this.containerId);_1a.selectChild(_19);},onCloseButtonClick:function(_1b){var _1c=_5.byId(this.containerId);_1c.closeChild(_1b);var b=this.pane2button[this._currentChild];if(b){_5.focus(b.focusNode||b.domNode);}},adjacent:function(_1e){if(!this.isLeftToRight()&&(!this.tabPosition||/top|bottom/.test(this.tabPosition))){_1e=!_1e;}var _1f=this.getChildren();var _20=_4.indexOf(_1f,this.pane2button[this._currentChild]);var _21=_1e?1:_1f.length-1;return _1f[(_20+_21)%_1f.length];},onkeypress:function(e){if(this.disabled||e.altKey){return;}var _23=null;if(e.ctrlKey||!e._djpage){var k=_4.keys;switch(e.charOrCode){case k.LEFT_ARROW:case k.UP_ARROW:if(!e._djpage){_23=false;}break;case k.PAGE_UP:if(e.ctrlKey){_23=false;}break;case k.RIGHT_ARROW:case k.DOWN_ARROW:if(!e._djpage){_23=true;}break;case k.PAGE_DOWN:if(e.ctrlKey){_23=true;}break;case k.DELETE:if(this._currentChild.closable){this.onCloseButtonClick(this._currentChild);}_4.stopEvent(e);break;default:if(e.ctrlKey){if(e.charOrCode===k.TAB){this.adjacent(!e.shiftKey).onClick();_4.stopEvent(e);}else{if(e.charOrCode=="w"){if(this._currentChild.closable){this.onCloseButtonClick(this._currentChild);}_4.stopEvent(e);}}}}if(_23!==null){this.adjacent(_23).onClick();_4.stopEvent(e);}}},onContainerKeyPress:function(_25){_25.e._djpage=_25.page;this.onkeypress(_25.e);}});_4.declare("dijit.layout._StackButton",_5.form.ToggleButton,{tabIndex:"-1",postCreate:function(evt){_5.setWaiRole((this.focusNode||this.domNode),"tab");this.inherited(arguments);},onClick:function(evt){_5.focus(this.focusNode);},onClickCloseButton:function(evt){evt.stopPropagation();}});}}};});