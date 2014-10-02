/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.layout.ToggleSplitter"],["require","dijit.layout.BorderContainer"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.layout.ToggleSplitter"]){_4._hasResource["dojox.layout.ToggleSplitter"]=true;_4.provide("dojox.layout.ToggleSplitter");_4.experimental("dojox.layout.ToggleSplitter");_4.require("dijit.layout.BorderContainer");_4.declare("dojox.layout.ToggleSplitter",[_5.layout._Splitter],{open:true,closedThreshold:5,openSize:"",_closedSize:"0",templateString:"<div class=\"dijitSplitter dojoxToggleSplitter\" dojoAttachEvent=\"onkeypress:_onKeyPress,onmousedown:_onMouseDown\" tabIndex=\"0\" waiRole=\"separator\"><div dojoAttachPoint=\"toggleNode\" class=\"dijitSplitterThumb dojoxToggleSplitterIcon\"></div></div>",postCreate:function(){this._started=false;this.inherited(arguments);var _7=this.region;_4.addClass(this.domNode,"dojoxToggleSplitter"+_7.charAt(0).toUpperCase()+_7.substring(1));this.connect(this,"onDblClick","_toggleMe");},startup:function(){this.inherited(arguments);var _8=this.child.domNode,_9=_4.style(_8,(this.horizontal?"height":"width"));_4.forEach(["toggleSplitterOpen","toggleSplitterClosedThreshold","toggleSplitterOpenSize"],function(_a){var _b=_a.substring("toggleSplitter".length);_b=_b.charAt(0).toLowerCase()+_b.substring(1);if(_a in this.child){this[_b]=this.child[_a];}},this);if(!this.openSize){this.openSize=(this.open)?_9+"px":"75px";}this._openStyleProps=this._getStyleProps(_8,true);this._started=true;this.attr("open",this.open);return this;},_onMouseUp:function(_c){_4.disconnect(this._onMoveHandle);_4.disconnect(this._onUpHandle);delete this._onMoveHandle;delete this._onUpHandle;delete this._startPosn;},_onPrelimMouseMove:function(_d){var _e=this._startPosn||0;var _f=3;var _10=Math.abs(_e-(this.horizontal?_d.clientY:_d.clientX));if(_10>=_f){_4.disconnect(this._onMoveHandle);this._startDrag(_d);}},_onMouseDown:function(evt){if(!this.open){return;}if(!this._onUpHandle){this._onUpHandle=_4.connect(_4.body(),"onmouseup",this,"_onMouseUp");}if(!this._onMoveHandle){this._startPosn=this.horizontal?evt.clientY:evt.clientX;this._onMoveHandle=_4.connect(_4.body(),"onmousemove",this,"_onPrelimMouseMove");}},_handleOnChange:function(){var _12=this.child.domNode,_13,dim=this.horizontal?"height":"width";if(this.open){var _15=_4.mixin({display:"block",overflow:"auto",visibility:"visible"},this._openStyleProps);_15[dim]=(this._openStyleProps&&this._openStyleProps[dim])?this._openStyleProps[dim]:this.openSize;_4.style(_12,_15);this.connect(this.domNode,"onmousedown","_onMouseDown");}else{var _16=_4.getComputedStyle(_12);_13=this._getStyleProps(_12,true,_16);var _17=this._getStyleProps(_12,false,_16);this._openStyleProps=_13;_4.style(_12,_17);}this._setStateClass();if(this.container._started){this.container._layoutChildren(this.region);}},_getStyleProps:function(_18,_19,_1a){if(!_1a){_1a=_4.getComputedStyle(_18);}var _1b={},dim=this.horizontal?"height":"width";_1b["overflow"]=(_19)?_1a["overflow"]:"hidden";_1b["visibility"]=(_19)?_1a["visibility"]:"hidden";_1b[dim]=(_19)?_18.style[dim]||_1a[dim]:this._closedSize;var _1d=["Top","Right","Bottom","Left"];_4.forEach(["padding","margin","border"],function(_1e){for(var i=0;i<_1d.length;i++){var _20=_1e+_1d[i];if(_1e=="border"){_1e+="Width";}if(undefined!==_1a[_20]){_1b[_20]=(_19)?_1a[_20]:0;}}});return _1b;},_setStateClass:function(){if(this.open){_4.removeClass(this.domNode,"dojoxToggleSplitterClosed");_4.addClass(this.domNode,"dojoxToggleSplitterOpen");_4.removeClass(this.toggleNode,"dojoxToggleSplitterIconClosed");_4.addClass(this.toggleNode,"dojoxToggleSplitterIconOpen");}else{_4.addClass(this.domNode,"dojoxToggleSplitterClosed");_4.removeClass(this.domNode,"dojoxToggleSplitterOpen");_4.addClass(this.toggleNode,"dojoxToggleSplitterIconClosed");_4.removeClass(this.toggleNode,"dojoxToggleSplitterIconOpen");}},_setOpenAttr:function(_21){if(!this._started){return;}this.open=_21;this._handleOnChange(_21,true);var evt=this.open?"onOpen":"onClose";this[evt](this.child);},onOpen:function(){},onClose:function(){},_toggleMe:function(evt){if(evt){_4.stopEvent(evt);}this.attr("open",!this.open);},_onKeyPress:function(e){this.inherited(arguments);}});_4.extend(_5._Widget,{toggleSplitterOpen:true,toggleSplitterClosedThreshold:5,toggleSplitterOpenSize:""});}}};});