/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._base.focus"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._base.focus"]){_4._hasResource["dijit._base.focus"]=true;_4.provide("dijit._base.focus");_4.mixin(_5,{_curFocus:null,_prevFocus:null,isCollapsed:function(){var _7=_4.doc;if(_7.selection){var s=_7.selection;if(s.type=="Text"){return !s.createRange().htmlText.length;}else{return !s.createRange().length;}}else{var _9=_4.global;var _a=_9.getSelection();if(_4.isString(_a)){return !_a;}else{return !_a||_a.isCollapsed||!_a.toString();}}},getBookmark:function(){var _b,_c=_4.doc.selection;if(_c){var _d=_c.createRange();if(_c.type.toUpperCase()=="CONTROL"){if(_d.length){_b=[];var i=0,_f=_d.length;while(i<_f){_b.push(_d.item(i++));}}else{_b=null;}}else{_b=_d.getBookmark();}}else{if(window.getSelection){_c=_4.global.getSelection();if(_c){_d=_c.getRangeAt(0);_b=_d.cloneRange();}}else{console.warn("No idea how to store the current selection for this browser!");}}return _b;},moveToBookmark:function(_10){var _11=_4.doc;if(_11.selection){var _12;if(_4.isArray(_10)){_12=_11.body.createControlRange();_4.forEach(_10,function(n){_12.addElement(n);});}else{_12=_11.selection.createRange();_12.moveToBookmark(_10);}_12.select();}else{var _14=_4.global.getSelection&&_4.global.getSelection();if(_14&&_14.removeAllRanges){_14.removeAllRanges();_14.addRange(_10);}else{console.warn("No idea how to restore selection for this browser!");}}},getFocus:function(_15,_16){return {node:_15&&_4.isDescendant(_5._curFocus,_15.domNode)?_5._prevFocus:_5._curFocus,bookmark:!_4.withGlobal(_16||_4.global,_5.isCollapsed)?_4.withGlobal(_16||_4.global,_5.getBookmark):null,openedForWindow:_16};},focus:function(_17){if(!_17){return;}var _18="node" in _17?_17.node:_17,_19=_17.bookmark,_1a=_17.openedForWindow;if(_18){var _1b=(_18.tagName.toLowerCase()=="iframe")?_18.contentWindow:_18;if(_1b&&_1b.focus){try{_1b.focus();}catch(e){}}_5._onFocusNode(_18);}if(_19&&_4.withGlobal(_1a||_4.global,_5.isCollapsed)){if(_1a){_1a.focus();}try{_4.withGlobal(_1a||_4.global,_5.moveToBookmark,null,[_19]);}catch(e){}}},_activeStack:[],registerIframe:function(_1c){_5.registerWin(_1c.contentWindow,_1c);},registerWin:function(_1d,_1e){_4.connect(_1d.document,"onmousedown",function(evt){_5._justMouseDowned=true;setTimeout(function(){_5._justMouseDowned=false;},0);_5._onTouchNode(_1e||evt.target||evt.srcElement);});var doc=_1d.document;if(doc){if(_4.isIE){doc.attachEvent("onactivate",function(evt){if(evt.srcElement.tagName.toLowerCase()!="#document"){_5._onFocusNode(_1e||evt.srcElement);}});doc.attachEvent("ondeactivate",function(evt){_5._onBlurNode(_1e||evt.srcElement);});}else{doc.addEventListener("focus",function(evt){_5._onFocusNode(_1e||evt.target);},true);doc.addEventListener("blur",function(evt){_5._onBlurNode(_1e||evt.target);},true);}}doc=null;},_onBlurNode:function(_25){_5._prevFocus=_5._curFocus;_5._curFocus=null;if(_5._justMouseDowned){return;}if(_5._clearActiveWidgetsTimer){clearTimeout(_5._clearActiveWidgetsTimer);}_5._clearActiveWidgetsTimer=setTimeout(function(){delete _5._clearActiveWidgetsTimer;_5._setStack([]);_5._prevFocus=null;},100);},_onTouchNode:function(_26){if(_5._clearActiveWidgetsTimer){clearTimeout(_5._clearActiveWidgetsTimer);delete _5._clearActiveWidgetsTimer;}var _27=[];try{while(_26){if(_26.dijitPopupParent){_26=_5.byId(_26.dijitPopupParent).domNode;}else{if(_26.tagName&&_26.tagName.toLowerCase()=="body"){if(_26===_4.body()){break;}_26=_5.getDocumentWindow(_26.ownerDocument).frameElement;}else{var id=_26.getAttribute&&_26.getAttribute("widgetId");if(id){_27.unshift(id);}_26=_26.parentNode;}}}}catch(e){}_5._setStack(_27);},_onFocusNode:function(_29){if(!_29){return;}if(_29.nodeType==9){return;}_5._onTouchNode(_29);if(_29==_5._curFocus){return;}if(_5._curFocus){_5._prevFocus=_5._curFocus;}_5._curFocus=_29;_4.publish("focusNode",[_29]);},_setStack:function(_2a){var _2b=_5._activeStack;_5._activeStack=_2a;for(var _2c=0;_2c<Math.min(_2b.length,_2a.length);_2c++){if(_2b[_2c]!=_2a[_2c]){break;}}for(var i=_2b.length-1;i>=_2c;i--){var _2e=_5.byId(_2b[i]);if(_2e){_2e._focused=false;_2e._hasBeenBlurred=true;if(_2e._onBlur){_2e._onBlur();}if(_2e._setStateClass){_2e._setStateClass();}_4.publish("widgetBlur",[_2e]);}}for(i=_2c;i<_2a.length;i++){_2e=_5.byId(_2a[i]);if(_2e){_2e._focused=true;if(_2e._onFocus){_2e._onFocus();}if(_2e._setStateClass){_2e._setStateClass();}_4.publish("widgetFocus",[_2e]);}}}});_4.addOnLoad(function(){_5.registerWin(window);});}}};});