/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.SimpleTextarea"],["require","dijit.form.TextBox"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.SimpleTextarea"]){_4._hasResource["dijit.form.SimpleTextarea"]=true;_4.provide("dijit.form.SimpleTextarea");_4.require("dijit.form.TextBox");_4.declare("dijit.form.SimpleTextarea",_5.form.TextBox,{baseClass:"dijitTextArea",attributeMap:_4.delegate(_5.form._FormValueWidget.prototype.attributeMap,{rows:"textbox",cols:"textbox"}),rows:"3",cols:"20",templatePath:null,templateString:"<textarea ${nameAttrSetting} dojoAttachPoint='focusNode,containerNode,textbox' autocomplete='off'></textarea>",postMixInProperties:function(){if(!this.value&&this.srcNodeRef){this.value=this.srcNodeRef.value;}this.inherited(arguments);},filter:function(_7){if(_7){_7=_7.replace(/\r/g,"");}return this.inherited(arguments);},postCreate:function(){this.inherited(arguments);if(_4.isIE&&this.cols){_4.addClass(this.domNode,"dijitTextAreaCols");}},_previousValue:"",_onInput:function(e){if(this.maxLength){var _9=parseInt(this.maxLength);var _a=this.textbox.value.replace(/\r/g,"");var _b=_a.length-_9;if(_b>0){_4.stopEvent(e);var _c=this.textbox;if(_c.selectionStart){var _d=_c.selectionStart;var cr=0;if(_4.isOpera){cr=(this.textbox.value.substring(0,_d).match(/\r/g)||[]).length;}this.textbox.value=_a.substring(0,_d-_b-cr)+_a.substring(_d-cr);_c.setSelectionRange(_d-_b,_d-_b);}else{if(_4.doc.selection){_c.focus();var _f=_4.doc.selection.createRange();_f.moveStart("character",-_b);_f.text="";_f.select();}}}this._previousValue=this.textbox.value;}this.inherited(arguments);}});}}};});