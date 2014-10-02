/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.HorizontalSlider"],["require","dijit.form._FormWidget"],["require","dijit._Container"],["require","dojo.dnd.move"],["require","dijit.form.Button"],["require","dojo.number"],["require","dojo._base.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.HorizontalSlider"]){_4._hasResource["dijit.form.HorizontalSlider"]=true;_4.provide("dijit.form.HorizontalSlider");_4.require("dijit.form._FormWidget");_4.require("dijit._Container");_4.require("dojo.dnd.move");_4.require("dijit.form.Button");_4.require("dojo.number");_4.require("dojo._base.fx");_4.declare("dijit.form.HorizontalSlider",[_5.form._FormValueWidget,_5._Container],{templateString:"<table class=\"dijit dijitReset dijitSlider\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" rules=\"none\" dojoAttachEvent=\"onkeypress:_onKeyPress\"\r\n\t><tr class=\"dijitReset\"\r\n\t\t><td class=\"dijitReset\" colspan=\"2\"></td\r\n\t\t><td dojoAttachPoint=\"containerNode,topDecoration\" class=\"dijitReset\" style=\"text-align:center;width:100%;\"></td\r\n\t\t><td class=\"dijitReset\" colspan=\"2\"></td\r\n\t></tr\r\n\t><tr class=\"dijitReset\"\r\n\t\t><td class=\"dijitReset dijitSliderButtonContainer dijitSliderButtonContainerH\"\r\n\t\t\t><div class=\"dijitSliderDecrementIconH\" tabIndex=\"-1\" style=\"display:none\" dojoAttachPoint=\"decrementButton\"><span class=\"dijitSliderButtonInner\">-</span></div\r\n\t\t></td\r\n\t\t><td class=\"dijitReset\"\r\n\t\t\t><div class=\"dijitSliderBar dijitSliderBumper dijitSliderBumperH dijitSliderLeftBumper dijitSliderLeftBumper\" dojoAttachEvent=\"onmousedown:_onClkDecBumper\"></div\r\n\t\t></td\r\n\t\t><td class=\"dijitReset\"\r\n\t\t\t><input dojoAttachPoint=\"valueNode\" type=\"hidden\" ${nameAttrSetting}\r\n\t\t\t/><div class=\"dijitReset dijitSliderBarContainerH\" waiRole=\"presentation\" dojoAttachPoint=\"sliderBarContainer\"\r\n\t\t\t\t><div waiRole=\"presentation\" dojoAttachPoint=\"progressBar\" class=\"dijitSliderBar dijitSliderBarH dijitSliderProgressBar dijitSliderProgressBarH\" dojoAttachEvent=\"onmousedown:_onBarClick\"\r\n\t\t\t\t\t><div class=\"dijitSliderMoveable dijitSliderMoveableH\" \r\n\t\t\t\t\t\t><div dojoAttachPoint=\"sliderHandle,focusNode\" class=\"dijitSliderImageHandle dijitSliderImageHandleH\" dojoAttachEvent=\"onmousedown:_onHandleClick\" waiRole=\"slider\" valuemin=\"${minimum}\" valuemax=\"${maximum}\"></div\r\n\t\t\t\t\t></div\r\n\t\t\t\t></div\r\n\t\t\t\t><div waiRole=\"presentation\" dojoAttachPoint=\"remainingBar\" class=\"dijitSliderBar dijitSliderBarH dijitSliderRemainingBar dijitSliderRemainingBarH\" dojoAttachEvent=\"onmousedown:_onBarClick\"></div\r\n\t\t\t></div\r\n\t\t></td\r\n\t\t><td class=\"dijitReset\"\r\n\t\t\t><div class=\"dijitSliderBar dijitSliderBumper dijitSliderBumperH dijitSliderRightBumper dijitSliderRightBumper\" dojoAttachEvent=\"onmousedown:_onClkIncBumper\"></div\r\n\t\t></td\r\n\t\t><td class=\"dijitReset dijitSliderButtonContainer dijitSliderButtonContainerH\" style=\"right:0px;\"\r\n\t\t\t><div class=\"dijitSliderIncrementIconH\" tabIndex=\"-1\" style=\"display:none\" dojoAttachPoint=\"incrementButton\"><span class=\"dijitSliderButtonInner\">+</span></div\r\n\t\t></td\r\n\t></tr\r\n\t><tr class=\"dijitReset\"\r\n\t\t><td class=\"dijitReset\" colspan=\"2\"></td\r\n\t\t><td dojoAttachPoint=\"containerNode,bottomDecoration\" class=\"dijitReset\" style=\"text-align:center;\"></td\r\n\t\t><td class=\"dijitReset\" colspan=\"2\"></td\r\n\t></tr\r\n></table>\r\n",value:0,showButtons:true,minimum:0,maximum:100,discreteValues:Infinity,pageIncrement:2,clickSelect:true,slideDuration:_5.defaultDuration,widgetsInTemplate:true,attributeMap:_4.delegate(_5.form._FormWidget.prototype.attributeMap,{id:""}),baseClass:"dijitSlider",_mousePixelCoord:"pageX",_pixelCount:"w",_startingPixelCoord:"x",_startingPixelCount:"l",_handleOffsetCoord:"left",_progressPixelSize:"width",_onKeyPress:function(e){if(this.disabled||this.readOnly||e.altKey||e.ctrlKey){return;}switch(e.charOrCode){case _4.keys.HOME:this._setValueAttr(this.minimum,true);break;case _4.keys.END:this._setValueAttr(this.maximum,true);break;case ((this._descending||this.isLeftToRight())?_4.keys.RIGHT_ARROW:_4.keys.LEFT_ARROW):case (this._descending===false?_4.keys.DOWN_ARROW:_4.keys.UP_ARROW):case (this._descending===false?_4.keys.PAGE_DOWN:_4.keys.PAGE_UP):this.increment(e);break;case ((this._descending||this.isLeftToRight())?_4.keys.LEFT_ARROW:_4.keys.RIGHT_ARROW):case (this._descending===false?_4.keys.UP_ARROW:_4.keys.DOWN_ARROW):case (this._descending===false?_4.keys.PAGE_UP:_4.keys.PAGE_DOWN):this.decrement(e);break;default:return;}_4.stopEvent(e);},_onHandleClick:function(e){if(this.disabled||this.readOnly){return;}if(!_4.isIE){_5.focus(this.sliderHandle);}_4.stopEvent(e);},_isReversed:function(){return !this.isLeftToRight();},_onBarClick:function(e){if(this.disabled||this.readOnly||!this.clickSelect){return;}_5.focus(this.sliderHandle);_4.stopEvent(e);var _a=_4.coords(this.sliderBarContainer,true);var _b=e[this._mousePixelCoord]-_a[this._startingPixelCoord];this._setPixelValue(this._isReversed()?(_a[this._pixelCount]-_b):_b,_a[this._pixelCount],true);this._movable.onMouseDown(e);},_setPixelValue:function(_c,_d,_e){if(this.disabled||this.readOnly){return;}_c=_c<0?0:_d<_c?_d:_c;var _f=this.discreteValues;if(_f<=1||_f==Infinity){_f=_d;}_f--;var _10=_d/_f;var _11=Math.round(_c/_10);this._setValueAttr((this.maximum-this.minimum)*_11/_f+this.minimum,_e);},_setValueAttr:function(_12,_13){this.valueNode.value=this.value=_12;_5.setWaiState(this.focusNode,"valuenow",_12);this.inherited(arguments);var _14=(_12-this.minimum)/(this.maximum-this.minimum);var _15=(this._descending===false)?this.remainingBar:this.progressBar;var _16=(this._descending===false)?this.progressBar:this.remainingBar;if(this._inProgressAnim&&this._inProgressAnim.status!="stopped"){this._inProgressAnim.stop(true);}if(_13&&this.slideDuration>0&&_15.style[this._progressPixelSize]){var _17=this;var _18={};var _19=parseFloat(_15.style[this._progressPixelSize]);var _1a=this.slideDuration*(_14-_19/100);if(_1a==0){return;}if(_1a<0){_1a=0-_1a;}_18[this._progressPixelSize]={start:_19,end:_14*100,units:"%"};this._inProgressAnim=_4.animateProperty({node:_15,duration:_1a,onAnimate:function(v){_16.style[_17._progressPixelSize]=(100-parseFloat(v[_17._progressPixelSize]))+"%";},onEnd:function(){delete _17._inProgressAnim;},properties:_18});this._inProgressAnim.play();}else{_15.style[this._progressPixelSize]=(_14*100)+"%";_16.style[this._progressPixelSize]=((1-_14)*100)+"%";}},_bumpValue:function(_1c){if(this.disabled||this.readOnly){return;}var s=_4.getComputedStyle(this.sliderBarContainer);var c=_4._getContentBox(this.sliderBarContainer,s);var _1f=this.discreteValues;if(_1f<=1||_1f==Infinity){_1f=c[this._pixelCount];}_1f--;var _20=(this.value-this.minimum)*_1f/(this.maximum-this.minimum)+_1c;if(_20<0){_20=0;}if(_20>_1f){_20=_1f;}_20=_20*(this.maximum-this.minimum)/_1f+this.minimum;this._setValueAttr(_20,true);},_onClkBumper:function(val){if(this.disabled||this.readOnly||!this.clickSelect){return;}this._setValueAttr(val,true);},_onClkIncBumper:function(){this._onClkBumper(this._descending===false?this.minimum:this.maximum);},_onClkDecBumper:function(){this._onClkBumper(this._descending===false?this.maximum:this.minimum);},decrement:function(e){this._bumpValue(e.charOrCode==_4.keys.PAGE_DOWN?-this.pageIncrement:-1);},increment:function(e){this._bumpValue(e.charOrCode==_4.keys.PAGE_UP?this.pageIncrement:1);},_mouseWheeled:function(evt){_4.stopEvent(evt);var _25=!_4.isMozilla;var _26=evt[(_25?"wheelDelta":"detail")]*(_25?1:-1);this[(_26<0?"decrement":"increment")](evt);},startup:function(){_4.forEach(this.getChildren(),function(_27){if(this[_27.container]!=this.containerNode){this[_27.container].appendChild(_27.domNode);}},this);},_typematicCallback:function(_28,_29,e){if(_28==-1){return;}this[(_29==(this._descending?this.incrementButton:this.decrementButton))?"decrement":"increment"](e);},postCreate:function(){if(this.showButtons){this.incrementButton.style.display="";this.decrementButton.style.display="";this._connects.push(_5.typematic.addMouseListener(this.decrementButton,this,"_typematicCallback",25,500));this._connects.push(_5.typematic.addMouseListener(this.incrementButton,this,"_typematicCallback",25,500));}this.connect(this.domNode,!_4.isMozilla?"onmousewheel":"DOMMouseScroll","_mouseWheeled");var _2b=this;var _2c=function(){_5.form._SliderMover.apply(this,arguments);this.widget=_2b;};_4.extend(_2c,_5.form._SliderMover.prototype);this._movable=new _4.dnd.Moveable(this.sliderHandle,{mover:_2c});var _2d=_4.query("label[for=\""+this.id+"\"]");if(_2d.length){_2d[0].id=(this.id+"_label");_5.setWaiState(this.focusNode,"labelledby",_2d[0].id);}_5.setWaiState(this.focusNode,"valuemin",this.minimum);_5.setWaiState(this.focusNode,"valuemax",this.maximum);this.inherited(arguments);},destroy:function(){this._movable.destroy();if(this._inProgressAnim&&this._inProgressAnim.status!="stopped"){this._inProgressAnim.stop(true);}this.inherited(arguments);}});_4.declare("dijit.form._SliderMover",_4.dnd.Mover,{onMouseMove:function(e){var _2f=this.widget;var _30=_2f._abspos;if(!_30){_30=_2f._abspos=_4.coords(_2f.sliderBarContainer,true);_2f._setPixelValue_=_4.hitch(_2f,"_setPixelValue");_2f._isReversed_=_2f._isReversed();}var _31=e[_2f._mousePixelCoord]-_30[_2f._startingPixelCoord];_2f._setPixelValue_(_2f._isReversed_?(_30[_2f._pixelCount]-_31):_31,_30[_2f._pixelCount],false);},destroy:function(e){_4.dnd.Mover.prototype.destroy.apply(this,arguments);var _33=this.widget;_33._abspos=null;_33._setValueAttr(_33.value,true);}});}}};});