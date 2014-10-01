/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.layout.RadioGroup"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dijit._Contained"],["require","dijit.layout.StackContainer"],["require","dojo.fx.easing"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.layout.RadioGroup"]){_4._hasResource["dojox.layout.RadioGroup"]=true;_4.provide("dojox.layout.RadioGroup");_4.experimental("dojox.layout.RadioGroup");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dijit._Contained");_4.require("dijit.layout.StackContainer");_4.require("dojo.fx.easing");_4.declare("dojox.layout.RadioGroup",[_5.layout.StackContainer,_5._Templated],{duration:750,hasButtons:false,buttonClass:"dojox.layout._RadioButton",templateString:"<div class=\"dojoxRadioGroup\">"+" \t<div dojoAttachPoint=\"buttonHolder\" style=\"display:none;\">"+"\t\t<table class=\"dojoxRadioButtons\"><tbody><tr class=\"dojoxRadioButtonRow\" dojoAttachPoint=\"buttonNode\"></tr></tbody></table>"+"\t</div>"+"\t<div class=\"dojoxRadioView\" dojoAttachPoint=\"containerNode\"></div>"+"</div>",startup:function(){this.inherited(arguments);this._children=this.getChildren();this._buttons=this._children.length;this._size=_4.coords(this.containerNode);if(this.hasButtons){_4.style(this.buttonHolder,"display","block");}},_setupChild:function(_7){if(this.hasButtons){_4.style(_7.domNode,"position","absolute");var _8=this.buttonNode.appendChild(_4.create("td"));var n=_4.create("div",null,_8),_a=_4.getObject(this.buttonClass),_b=new _a({label:_7.title,page:_7},n);_4.mixin(_7,{_radioButton:_b});_b.startup();}_7.domNode.style.display="none";},removeChild:function(_c){if(this.hasButtons&&_c._radioButton){_c._radioButton.destroy();delete _c._radioButton;}this.inherited(arguments);},_transition:function(_d,_e){this._showChild(_d);if(_e){this._hideChild(_e);}if(this.doLayout&&_d.resize){_d.resize(this._containerContentBox||this._contentBox);}},_showChild:function(_f){var _10=this.getChildren();_f.isFirstChild=(_f==_10[0]);_f.isLastChild=(_f==_10[_10.length-1]);_f.selected=true;_f.domNode.style.display="";if(_f._onShow){_f._onShow();}else{if(_f.onShow){_f.onShow();}}},_hideChild:function(_11){_11.selected=false;_11.domNode.style.display="none";if(_11.onHide){_11.onHide();}}});_4.declare("dojox.layout.RadioGroupFade",_6.layout.RadioGroup,{_hideChild:function(_12){_4.fadeOut({node:_12.domNode,duration:this.duration,onEnd:_4.hitch(this,"inherited",arguments)}).play();},_showChild:function(_13){this.inherited(arguments);_4.style(_13.domNode,"opacity",0);_4.fadeIn({node:_13.domNode,duration:this.duration}).play();}});_4.declare("dojox.layout.RadioGroupSlide",_6.layout.RadioGroup,{easing:"dojo.fx.easing.backOut",zTop:99,constructor:function(){if(_4.isString(this.easing)){this.easing=_4.getObject(this.easing);}},_positionChild:function(_14){if(!this._size){return;}var rA=true,rB=true;switch(_14.slideFrom){case "bottom":rB=!rB;break;case "right":rA=!rA;rB=!rB;break;case "top":break;case "left":rA=!rA;break;default:rA=Math.round(Math.random());rB=Math.round(Math.random());break;}var _17=rA?"top":"left",val=(rB?"-":"")+(this._size[rA?"h":"w"]+20)+"px";_4.style(_14.domNode,_17,val);},_showChild:function(_19){var _1a=this.getChildren();_19.isFirstChild=(_19==_1a[0]);_19.isLastChild=(_19==_1a[_1a.length-1]);_19.selected=true;_4.style(_19.domNode,{zIndex:this.zTop,display:""});if(this._anim&&this._anim.status()=="playing"){this._anim.gotoPercent(100,true);}this._anim=_4.animateProperty({node:_19.domNode,properties:{left:0,top:0},duration:this.duration,easing:this.easing,onEnd:_4.hitch(_19,function(){if(this.onShow){this.onShow();}if(this._onShow){this._onShow();}}),beforeBegin:_4.hitch(this,"_positionChild",_19)});this._anim.play();},_hideChild:function(_1b){_1b.selected=false;_1b.domNode.style.zIndex=this.zTop-1;if(_1b.onHide){_1b.onHide();}}});_4.declare("dojox.layout._RadioButton",[_5._Widget,_5._Templated,_5._Contained],{label:"",page:null,templateString:"<div dojoAttachPoint=\"focusNode\" class=\"dojoxRadioButton\"><span dojoAttachPoint=\"titleNode\" class=\"dojoxRadioButtonLabel\">${label}</span></div>",startup:function(){this.connect(this.domNode,"onmouseenter","_onMouse");},_onMouse:function(e){this.getParent().selectChild(this.page);this._clearSelected();_4.addClass(this.domNode,"dojoxRadioButtonSelected");},_clearSelected:function(){_4.query(".dojoxRadioButtonSelected",this.domNode.parentNode.parentNode).removeClass("dojoxRadioButtonSelected");}});_4.extend(_5._Widget,{slideFrom:"random"});}}};});