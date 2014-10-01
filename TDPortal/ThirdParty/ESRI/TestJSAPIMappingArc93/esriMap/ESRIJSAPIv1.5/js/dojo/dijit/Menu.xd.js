/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.Menu"],["require","dijit._Widget"],["require","dijit._KeyNavContainer"],["require","dijit._Templated"],["require","dijit.MenuItem"],["require","dijit.PopupMenuItem"],["require","dijit.CheckedMenuItem"],["require","dijit.MenuSeparator"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.Menu"]){_4._hasResource["dijit.Menu"]=true;_4.provide("dijit.Menu");_4.require("dijit._Widget");_4.require("dijit._KeyNavContainer");_4.require("dijit._Templated");_4.declare("dijit._MenuBase",[_5._Widget,_5._Templated,_5._KeyNavContainer],{parentMenu:null,popupDelay:500,startup:function(){if(this._started){return;}_4.forEach(this.getChildren(),function(_7){_7.startup();});this.startupKeyNavChildren();this.inherited(arguments);},onExecute:function(){},onCancel:function(_8){},_moveToPopup:function(_9){if(this.focusedChild&&this.focusedChild.popup&&!this.focusedChild.disabled){this.focusedChild._onClick(_9);}else{var _a=this._getTopMenu();if(_a&&_a._isMenuBar){_a.focusNext();}}},onItemHover:function(_b){if(this.isActive){this.focusChild(_b);if(this.focusedChild.popup&&!this.focusedChild.disabled&&!this.hover_timer){this.hover_timer=setTimeout(_4.hitch(this,"_openPopup"),this.popupDelay);}}},_onChildBlur:function(_c){_c._setSelected(false);_5.popup.close(_c.popup);this._stopPopupTimer();},onItemUnhover:function(_d){if(this.isActive){this._stopPopupTimer();}},_stopPopupTimer:function(){if(this.hover_timer){clearTimeout(this.hover_timer);this.hover_timer=null;}},_getTopMenu:function(){for(var _e=this;_e.parentMenu;_e=_e.parentMenu){}return _e;},onItemClick:function(_f,evt){if(_f.disabled){return false;}this.focusChild(_f);if(_f.popup){if(!this.is_open){this._openPopup();}}else{this.onExecute();_f.onClick(evt);}},_openPopup:function(){this._stopPopupTimer();var _11=this.focusedChild;var _12=_11.popup;if(_12.isShowingNow){return;}_12.parentMenu=this;var _13=this;_5.popup.open({parent:this,popup:_12,around:_11.domNode,orient:this._orient||(this.isLeftToRight()?{"TR":"TL","TL":"TR"}:{"TL":"TR","TR":"TL"}),onCancel:function(){_5.popup.close(_12);_11.focus();_13.currentPopup=null;},onExecute:_4.hitch(this,"_onDescendantExecute")});this.currentPopup=_12;if(_12.focus){setTimeout(_4.hitch(_12,"focus"),0);}},onOpen:function(e){this.isShowingNow=true;},onClose:function(){this._stopPopupTimer();this.parentMenu=null;this.isShowingNow=false;this.currentPopup=null;if(this.focusedChild){this._onChildBlur(this.focusedChild);this.focusedChild=null;}},_onFocus:function(){this.isActive=true;_4.addClass(this.domNode,"dijitMenuActive");_4.removeClass(this.domNode,"dijitMenuPassive");this.inherited(arguments);},_onBlur:function(){this.isActive=false;_4.removeClass(this.domNode,"dijitMenuActive");_4.addClass(this.domNode,"dijitMenuPassive");this.onClose();this.inherited(arguments);},_onDescendantExecute:function(){this.onClose();}});_4.declare("dijit.Menu",_5._MenuBase,{constructor:function(){this._bindings=[];},templateString:"<table class=\"dijit dijitMenu dijitMenuPassive dijitReset dijitMenuTable\" waiRole=\"menu\" tabIndex=\"${tabIndex}\" dojoAttachEvent=\"onkeypress:_onKeyPress\">\r\n\t<tbody class=\"dijitReset\" dojoAttachPoint=\"containerNode\"></tbody>\r\n</table>\r\n",targetNodeIds:[],contextMenuForWindow:false,leftClickToOpen:false,_contextMenuWithMouse:false,postCreate:function(){if(this.contextMenuForWindow){this.bindDomNode(_4.body());}else{_4.forEach(this.targetNodeIds,this.bindDomNode,this);}var k=_4.keys,l=this.isLeftToRight();this._openSubMenuKey=l?k.RIGHT_ARROW:k.LEFT_ARROW;this._closeSubMenuKey=l?k.LEFT_ARROW:k.RIGHT_ARROW;this.connectKeyNavHandlers([k.UP_ARROW],[k.DOWN_ARROW]);},_onKeyPress:function(evt){if(evt.ctrlKey||evt.altKey){return;}switch(evt.charOrCode){case this._openSubMenuKey:this._moveToPopup(evt);_4.stopEvent(evt);break;case this._closeSubMenuKey:if(this.parentMenu){if(this.parentMenu._isMenuBar){this.parentMenu.focusPrev();}else{this.onCancel(false);}}else{_4.stopEvent(evt);}break;}},_iframeContentWindow:function(_18){var win=_5.getDocumentWindow(_5.Menu._iframeContentDocument(_18))||_5.Menu._iframeContentDocument(_18)["__parent__"]||(_18.name&&_4.doc.frames[_18.name])||null;return win;},_iframeContentDocument:function(_1a){var doc=_1a.contentDocument||(_1a.contentWindow&&_1a.contentWindow.document)||(_1a.name&&_4.doc.frames[_1a.name]&&_4.doc.frames[_1a.name].document)||null;return doc;},bindDomNode:function(_1c){_1c=_4.byId(_1c);var win=_5.getDocumentWindow(_1c.ownerDocument);if(_1c.tagName.toLowerCase()=="iframe"){win=this._iframeContentWindow(_1c);_1c=_4.withGlobal(win,_4.body);}var cn=(_1c==_4.body()?_4.doc:_1c);_1c[this.id]=this._bindings.push([_4.connect(cn,(this.leftClickToOpen)?"onclick":"oncontextmenu",this,"_openMyself"),_4.connect(cn,"onkeydown",this,"_contextKey"),_4.connect(cn,"onmousedown",this,"_contextMouse")]);},unBindDomNode:function(_1f){var _20=_4.byId(_1f);if(_20){var bid=_20[this.id]-1,b=this._bindings[bid];_4.forEach(b,_4.disconnect);delete this._bindings[bid];}},_contextKey:function(e){this._contextMenuWithMouse=false;if(e.keyCode==_4.keys.F10){_4.stopEvent(e);if(e.shiftKey&&e.type=="keydown"){var _e={target:e.target,pageX:e.pageX,pageY:e.pageY};_e.preventDefault=_e.stopPropagation=function(){};window.setTimeout(_4.hitch(this,function(){this._openMyself(_e);}),1);}}},_contextMouse:function(e){this._contextMenuWithMouse=true;},_openMyself:function(e){if(this.leftClickToOpen&&e.button>0){return;}_4.stopEvent(e);var x,y;if(_4.isSafari||this._contextMenuWithMouse){x=e.pageX;y=e.pageY;}else{var _29=_4.coords(e.target,true);x=_29.x+10;y=_29.y+10;}var _2a=this;var _2b=_5.getFocus(this);function _2c(){_5.focus(_2b);_5.popup.close(_2a);};_5.popup.open({popup:this,x:x,y:y,onExecute:_2c,onCancel:_2c,orient:this.isLeftToRight()?"L":"R"});this.focus();this._onBlur=function(){this.inherited("_onBlur",arguments);_5.popup.close(this);};},uninitialize:function(){_4.forEach(this.targetNodeIds,this.unBindDomNode,this);this.inherited(arguments);}});_4.require("dijit.MenuItem");_4.require("dijit.PopupMenuItem");_4.require("dijit.CheckedMenuItem");_4.require("dijit.MenuSeparator");}}};});