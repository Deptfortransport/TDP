/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.MultiSelect"],["require","dijit.form._FormWidget"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.MultiSelect"]){_4._hasResource["dijit.form.MultiSelect"]=true;_4.provide("dijit.form.MultiSelect");_4.require("dijit.form._FormWidget");_4.declare("dijit.form.MultiSelect",_5.form._FormWidget,{size:7,templateString:"<select multiple='true' ${nameAttrSetting} dojoAttachPoint='containerNode,focusNode' dojoAttachEvent='onchange: _onChange'></select>",attributeMap:_4.delegate(_5.form._FormWidget.prototype.attributeMap,{size:"focusNode"}),reset:function(){this._hasBeenBlurred=false;this._setValueAttr(this._resetValue,true);},addSelected:function(_7){_7.getSelected().forEach(function(n){this.containerNode.appendChild(n);this.domNode.scrollTop=this.domNode.offsetHeight;var _9=_7.domNode.scrollTop;_7.domNode.scrollTop=0;_7.domNode.scrollTop=_9;},this);},getSelected:function(){return _4.query("option",this.containerNode).filter(function(n){return n.selected;});},_getValueAttr:function(){return this.getSelected().map(function(n){return n.value;});},_multiValue:true,_setValueAttr:function(_c){_4.query("option",this.containerNode).forEach(function(n){n.selected=(_4.indexOf(_c,n.value)!=-1);});},invertSelection:function(_e){_4.query("option",this.containerNode).forEach(function(n){n.selected=!n.selected;});this._handleOnChange(this.attr("value"),_e==true);},_onChange:function(e){this._handleOnChange(this.attr("value"),true);},resize:function(_11){if(_11){_4.marginBox(this.domNode,_11);}},postCreate:function(){this._onChange();}});}}};});