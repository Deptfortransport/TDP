/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.ValidationTextBox"],["require","dojo.i18n"],["require","dijit.form.TextBox"],["require","dijit.Tooltip"],["requireLocalization","dijit.form","validate",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.ValidationTextBox"]){_4._hasResource["dijit.form.ValidationTextBox"]=true;_4.provide("dijit.form.ValidationTextBox");_4.require("dojo.i18n");_4.require("dijit.form.TextBox");_4.require("dijit.Tooltip");_4.declare("dijit.form.ValidationTextBox",_5.form.TextBox,{templateString:"<div class=\"dijit dijitReset dijitInlineTable dijitLeft\"\r\n\tid=\"widget_${id}\"\r\n\tdojoAttachEvent=\"onmouseenter:_onMouse,onmouseleave:_onMouse,onmousedown:_onMouse\" waiRole=\"presentation\"\r\n\t><div style=\"overflow:hidden;\"\r\n\t\t><div class=\"dijitReset dijitValidationIcon\"><br></div\r\n\t\t><div class=\"dijitReset dijitValidationIconText\">&Chi;</div\r\n\t\t><div class=\"dijitReset dijitInputField\"\r\n\t\t\t><input class=\"dijitReset\" dojoAttachPoint='textbox,focusNode' autocomplete=\"off\"\r\n\t\t\t${nameAttrSetting} type='${type}'\r\n\t\t/></div\r\n\t></div\r\n></div>\r\n",baseClass:"dijitTextBox",required:false,promptMessage:"",invalidMessage:"$_unset_$",constraints:{},regExp:".*",regExpGen:function(_7){return this.regExp;},state:"",tooltipPosition:[],_setValueAttr:function(){this.inherited(arguments);this.validate(this._focused);},validator:function(_8,_9){return (new RegExp("^(?:"+this.regExpGen(_9)+")"+(this.required?"":"?")+"$")).test(_8)&&(!this.required||!this._isEmpty(_8))&&(this._isEmpty(_8)||this.parse(_8,_9)!==undefined);},_isValidSubset:function(){return this.textbox.value.search(this._partialre)==0;},isValid:function(_a){return this.validator(this.textbox.value,this.constraints);},_isEmpty:function(_b){return /^\s*$/.test(_b);},getErrorMessage:function(_c){return this.invalidMessage;},getPromptMessage:function(_d){return this.promptMessage;},_maskValidSubsetError:true,validate:function(_e){var _f="";var _10=this.disabled||this.isValid(_e);if(_10){this._maskValidSubsetError=true;}var _11=!_10&&_e&&this._isValidSubset();var _12=this._isEmpty(this.textbox.value);this.state=(_10||(!this._hasBeenBlurred&&_12)||_11)?"":"Error";if(this.state=="Error"){this._maskValidSubsetError=false;}this._setStateClass();_5.setWaiState(this.focusNode,"invalid",_10?"false":"true");if(_e){if(_12){_f=this.getPromptMessage(true);}if(!_f&&(this.state=="Error"||(_11&&!this._maskValidSubsetError))){_f=this.getErrorMessage(true);}}this.displayMessage(_f);return _10;},_message:"",displayMessage:function(_13){if(this._message==_13){return;}this._message=_13;_5.hideTooltip(this.domNode);if(_13){_5.showTooltip(_13,this.domNode,this.tooltipPosition);}},_refreshState:function(){this.validate(this._focused);this.inherited(arguments);},constructor:function(){this.constraints={};},postMixInProperties:function(){this.inherited(arguments);this.constraints.locale=this.lang;this.messages=_4.i18n.getLocalization("dijit.form","validate",this.lang);if(this.invalidMessage=="$_unset_$"){this.invalidMessage=this.messages.invalidMessage;}var p=this.regExpGen(this.constraints);this.regExp=p;var _15="";if(p!=".*"){this.regExp.replace(/\\.|\[\]|\[.*?[^\\]{1}\]|\{.*?\}|\(\?[=:!]|./g,function(re){switch(re.charAt(0)){case "{":case "+":case "?":case "*":case "^":case "$":case "|":case "(":_15+=re;break;case ")":_15+="|$)";break;default:_15+="(?:"+re+"|$)";break;}});}try{"".search(_15);}catch(e){_15=this.regExp;console.warn("RegExp error in "+this.declaredClass+": "+this.regExp);}this._partialre="^(?:"+_15+")$";},_setDisabledAttr:function(_17){this.inherited(arguments);if(this.valueNode){this.valueNode.disabled=_17;}this._refreshState();},_setRequiredAttr:function(_18){this.required=_18;_5.setWaiState(this.focusNode,"required",_18);this._refreshState();},postCreate:function(){if(_4.isIE){var s=_4.getComputedStyle(this.focusNode);if(s){var ff=s.fontFamily;if(ff){this.focusNode.style.fontFamily=ff;}}}this.inherited(arguments);},reset:function(){this._maskValidSubsetError=true;this.inherited(arguments);}});_4.declare("dijit.form.MappedTextBox",_5.form.ValidationTextBox,{postMixInProperties:function(){this.inherited(arguments);this.nameAttrSetting="";},serialize:function(val,_1c){return val.toString?val.toString():"";},toString:function(){var val=this.filter(this.attr("value"));return val!=null?(typeof val=="string"?val:this.serialize(val,this.constraints)):"";},validate:function(){this.valueNode.value=this.toString();return this.inherited(arguments);},buildRendering:function(){this.inherited(arguments);this.valueNode=_4.create("input",{style:{display:"none"},type:this.type,name:this.name},this.textbox,"after");},_setDisabledAttr:function(_1e){this.inherited(arguments);_4.attr(this.valueNode,"disabled",_1e);},reset:function(){this.valueNode.value="";this.inherited(arguments);}});_4.declare("dijit.form.RangeBoundTextBox",_5.form.MappedTextBox,{rangeMessage:"",rangeCheck:function(_1f,_20){var _21="min" in _20;var _22="max" in _20;if(_21||_22){return (!_21||this.compare(_1f,_20.min)>=0)&&(!_22||this.compare(_1f,_20.max)<=0);}return true;},isInRange:function(_23){return this.rangeCheck(this.attr("value"),this.constraints);},_isDefinitelyOutOfRange:function(){var val=this.attr("value");var _25=false;var _26=false;if("min" in this.constraints){var min=this.constraints.min;val=this.compare(val,((typeof min=="number")&&min>=0&&val!=0)?0:min);_25=(typeof val=="number")&&val<0;}if("max" in this.constraints){var max=this.constraints.max;val=this.compare(val,((typeof max!="number")||max>0)?max:0);_26=(typeof val=="number")&&val>0;}return _25||_26;},_isValidSubset:function(){return this.inherited(arguments)&&!this._isDefinitelyOutOfRange();},isValid:function(_29){return this.inherited(arguments)&&((this._isEmpty(this.textbox.value)&&!this.required)||this.isInRange(_29));},getErrorMessage:function(_2a){if(_5.form.RangeBoundTextBox.superclass.isValid.call(this,false)&&!this.isInRange(_2a)){return this.rangeMessage;}return this.inherited(arguments);},postMixInProperties:function(){this.inherited(arguments);if(!this.rangeMessage){this.messages=_4.i18n.getLocalization("dijit.form","validate",this.lang);this.rangeMessage=this.messages.rangeMessage;}},postCreate:function(){this.inherited(arguments);if(this.constraints.min!==undefined){_5.setWaiState(this.focusNode,"valuemin",this.constraints.min);}if(this.constraints.max!==undefined){_5.setWaiState(this.focusNode,"valuemax",this.constraints.max);}},_setValueAttr:function(_2b,_2c){_5.setWaiState(this.focusNode,"valuenow",_2b);this.inherited(arguments);}});}}};});