/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo._base.event"],["require","dojo._base.connect"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base.event"]){_4._hasResource["dojo._base.event"]=true;_4.provide("dojo._base.event");_4.require("dojo._base.connect");(function(){var _7=(_4._event_listener={add:function(_8,_9,fp){if(!_8){return;}_9=_7._normalizeEventName(_9);fp=_7._fixCallback(_9,fp);var _b=_9;if(!_4.isIE&&(_9=="mouseenter"||_9=="mouseleave")){var _c=fp;_9=(_9=="mouseenter")?"mouseover":"mouseout";fp=function(e){if(_4.isFF<=2){try{e.relatedTarget.tagName;}catch(e2){return;}}if(!_4.isDescendant(e.relatedTarget,_8)){return _c.call(this,e);}};}_8.addEventListener(_9,fp,false);return fp;},remove:function(_e,_f,_10){if(_e){_f=_7._normalizeEventName(_f);if(!_4.isIE&&(_f=="mouseenter"||_f=="mouseleave")){_f=(_f=="mouseenter")?"mouseover":"mouseout";}_e.removeEventListener(_f,_10,false);}},_normalizeEventName:function(_11){return _11.slice(0,2)=="on"?_11.slice(2):_11;},_fixCallback:function(_12,fp){return _12!="keypress"?fp:function(e){return fp.call(this,_7._fixEvent(e,this));};},_fixEvent:function(evt,_16){switch(evt.type){case "keypress":_7._setKeyChar(evt);break;}return evt;},_setKeyChar:function(evt){evt.keyChar=evt.charCode?String.fromCharCode(evt.charCode):"";evt.charOrCode=evt.keyChar||evt.keyCode;},_punctMap:{106:42,111:47,186:59,187:43,188:44,189:45,190:46,191:47,192:96,219:91,220:92,221:93,222:39}});_4.fixEvent=function(evt,_19){return _7._fixEvent(evt,_19);};_4.stopEvent=function(evt){evt.preventDefault();evt.stopPropagation();};var _1b=_4._listener;_4._connect=function(obj,_1d,_1e,_1f,_20){var _21=obj&&(obj.nodeType||obj.attachEvent||obj.addEventListener);var lid=_21?(_20?2:1):0,l=[_4._listener,_7,_1b][lid];var h=l.add(obj,_1d,_4.hitch(_1e,_1f));return [obj,_1d,h,lid];};_4._disconnect=function(obj,_26,_27,_28){([_4._listener,_7,_1b][_28]).remove(obj,_26,_27);};_4.keys={BACKSPACE:8,TAB:9,CLEAR:12,ENTER:13,SHIFT:16,CTRL:17,ALT:18,PAUSE:19,CAPS_LOCK:20,ESCAPE:27,SPACE:32,PAGE_UP:33,PAGE_DOWN:34,END:35,HOME:36,LEFT_ARROW:37,UP_ARROW:38,RIGHT_ARROW:39,DOWN_ARROW:40,INSERT:45,DELETE:46,HELP:47,LEFT_WINDOW:91,RIGHT_WINDOW:92,SELECT:93,NUMPAD_0:96,NUMPAD_1:97,NUMPAD_2:98,NUMPAD_3:99,NUMPAD_4:100,NUMPAD_5:101,NUMPAD_6:102,NUMPAD_7:103,NUMPAD_8:104,NUMPAD_9:105,NUMPAD_MULTIPLY:106,NUMPAD_PLUS:107,NUMPAD_ENTER:108,NUMPAD_MINUS:109,NUMPAD_PERIOD:110,NUMPAD_DIVIDE:111,F1:112,F2:113,F3:114,F4:115,F5:116,F6:117,F7:118,F8:119,F9:120,F10:121,F11:122,F12:123,F13:124,F14:125,F15:126,NUM_LOCK:144,SCROLL_LOCK:145};if(_4.isIE){var _29=function(e,_2b){try{return (e.keyCode=_2b);}catch(e){return 0;}};var iel=_4._listener;var _2d=(_4._ieListenersName="_"+_4._scopeName+"_listeners");if(!_4.config._allow_leaks){_1b=iel=_4._ie_listener={handlers:[],add:function(_2e,_2f,_30){_2e=_2e||_4.global;var f=_2e[_2f];if(!f||!f[_2d]){var d=_4._getIeDispatcher();d.target=f&&(ieh.push(f)-1);d[_2d]=[];f=_2e[_2f]=d;}return f[_2d].push(ieh.push(_30)-1);},remove:function(_34,_35,_36){var f=(_34||_4.global)[_35],l=f&&f[_2d];if(f&&l&&_36--){delete ieh[l[_36]];delete l[_36];}}};var ieh=iel.handlers;}_4.mixin(_7,{add:function(_39,_3a,fp){if(!_39){return;}_3a=_7._normalizeEventName(_3a);if(_3a=="onkeypress"){var kd=_39.onkeydown;if(!kd||!kd[_2d]||!kd._stealthKeydownHandle){var h=_7.add(_39,"onkeydown",_7._stealthKeyDown);kd=_39.onkeydown;kd._stealthKeydownHandle=h;kd._stealthKeydownRefs=1;}else{kd._stealthKeydownRefs++;}}return iel.add(_39,_3a,_7._fixCallback(fp));},remove:function(_3e,_3f,_40){_3f=_7._normalizeEventName(_3f);iel.remove(_3e,_3f,_40);if(_3f=="onkeypress"){var kd=_3e.onkeydown;if(--kd._stealthKeydownRefs<=0){iel.remove(_3e,"onkeydown",kd._stealthKeydownHandle);delete kd._stealthKeydownHandle;}}},_normalizeEventName:function(_42){return _42.slice(0,2)!="on"?"on"+_42:_42;},_nop:function(){},_fixEvent:function(evt,_44){if(!evt){var w=_44&&(_44.ownerDocument||_44.document||_44).parentWindow||window;evt=w.event;}if(!evt){return (evt);}evt.target=evt.srcElement;evt.currentTarget=(_44||evt.srcElement);evt.layerX=evt.offsetX;evt.layerY=evt.offsetY;var se=evt.srcElement,doc=(se&&se.ownerDocument)||document;var _48=((_4.isIE<6)||(doc["compatMode"]=="BackCompat"))?doc.body:doc.documentElement;var _49=_4._getIeDocumentElementOffset();evt.pageX=evt.clientX+_4._fixIeBiDiScrollLeft(_48.scrollLeft||0)-_49.x;evt.pageY=evt.clientY+(_48.scrollTop||0)-_49.y;if(evt.type=="mouseover"){evt.relatedTarget=evt.fromElement;}if(evt.type=="mouseout"){evt.relatedTarget=evt.toElement;}evt.stopPropagation=_7._stopPropagation;evt.preventDefault=_7._preventDefault;return _7._fixKeys(evt);},_fixKeys:function(evt){switch(evt.type){case "keypress":var c=("charCode" in evt?evt.charCode:evt.keyCode);if(c==10){c=0;evt.keyCode=13;}else{if(c==13||c==27){c=0;}else{if(c==3){c=99;}}}evt.charCode=c;_7._setKeyChar(evt);break;}return evt;},_stealthKeyDown:function(evt){var kp=evt.currentTarget.onkeypress;if(!kp||!kp[_2d]){return;}var k=evt.keyCode;var _4f=k!=13&&k!=32&&k!=27&&(k<48||k>90)&&(k<96||k>111)&&(k<186||k>192)&&(k<219||k>222);if(_4f||evt.ctrlKey){var c=_4f?0:k;if(evt.ctrlKey){if(k==3||k==13){return;}else{if(c>95&&c<106){c-=48;}else{if((!evt.shiftKey)&&(c>=65&&c<=90)){c+=32;}else{c=_7._punctMap[c]||c;}}}}var _51=_7._synthesizeEvent(evt,{type:"keypress",faux:true,charCode:c});kp.call(evt.currentTarget,_51);evt.cancelBubble=_51.cancelBubble;evt.returnValue=_51.returnValue;_29(evt,_51.keyCode);}},_stopPropagation:function(){this.cancelBubble=true;},_preventDefault:function(){this.bubbledKeyCode=this.keyCode;if(this.ctrlKey){_29(this,0);}this.returnValue=false;}});_4.stopEvent=function(evt){evt=evt||window.event;_7._stopPropagation.call(evt);_7._preventDefault.call(evt);};}_7._synthesizeEvent=function(evt,_54){var _55=_4.mixin({},evt,_54);_7._setKeyChar(_55);_55.preventDefault=function(){evt.preventDefault();};_55.stopPropagation=function(){evt.stopPropagation();};return _55;};if(_4.isOpera){_4.mixin(_7,{_fixEvent:function(evt,_57){switch(evt.type){case "keypress":var c=evt.which;if(c==3){c=99;}c=c<41&&!evt.shiftKey?0:c;if(evt.ctrlKey&&!evt.shiftKey&&c>=65&&c<=90){c+=32;}return _7._synthesizeEvent(evt,{charCode:c});}return evt;}});}if(_4.isWebKit){_7._add=_7.add;_7._remove=_7.remove;_4.mixin(_7,{add:function(_59,_5a,fp){if(!_59){return;}var _5c=_7._add(_59,_5a,fp);if(_7._normalizeEventName(_5a)=="keypress"){_5c._stealthKeyDownHandle=_7._add(_59,"keydown",function(evt){var k=evt.keyCode;var _5f=k!=13&&k!=32&&k!=27&&(k<48||k>90)&&(k<96||k>111)&&(k<186||k>192)&&(k<219||k>222);if(_5f||evt.ctrlKey){var c=_5f?0:k;if(evt.ctrlKey){if(k==3||k==13){return;}else{if(c>95&&c<106){c-=48;}else{if(!evt.shiftKey&&c>=65&&c<=90){c+=32;}else{c=_7._punctMap[c]||c;}}}}var _61=_7._synthesizeEvent(evt,{type:"keypress",faux:true,charCode:c});fp.call(evt.currentTarget,_61);}});}return _5c;},remove:function(_62,_63,_64){if(_62){if(_64._stealthKeyDownHandle){_7._remove(_62,"keydown",_64._stealthKeyDownHandle);}_7._remove(_62,_63,_64);}},_fixEvent:function(evt,_66){switch(evt.type){case "keypress":if(evt.faux){return evt;}var c=evt.charCode;c=c>=32?c:0;return _7._synthesizeEvent(evt,{charCode:c,faux:true});}return evt;}});}})();if(_4.isIE){_4._ieDispatcher=function(_68,_69){var ap=Array.prototype,h=_4._ie_listener.handlers,c=_68.callee,ls=c[_4._ieListenersName],t=h[c.target];var r=t&&t.apply(_69,_68);var lls=[].concat(ls);for(var i in lls){var f=h[lls[i]];if(!(i in ap)&&f){f.apply(_69,_68);}}return r;};_4._getIeDispatcher=function(){return new Function(_4._scopeName+"._ieDispatcher(arguments, this)");};_4._event_listener._fixCallback=function(fp){var f=_4._event_listener._fixEvent;return function(e){return fp.call(this,f(e,this));};};}}}};});