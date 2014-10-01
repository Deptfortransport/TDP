/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.form.FileInputAuto"],["require","dojox.form.FileInput"],["require","dojo.io.iframe"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.form.FileInputAuto"]){_4._hasResource["dojox.form.FileInputAuto"]=true;_4.provide("dojox.form.FileInputAuto");_4.require("dojox.form.FileInput");_4.require("dojo.io.iframe");_4.declare("dojox.form.FileInputAuto",_6.form.FileInput,{url:"",blurDelay:2000,duration:500,uploadMessage:"Uploading ...",triggerEvent:"onblur",_sent:false,templateString:"<div class=\"dijitFileInput\">\r\n\t<input id=\"${id}\" name=\"${name}\" class=\"dijitFileInputReal\" type=\"file\" dojoAttachPoint=\"fileInput\" />\r\n\t<div class=\"dijitFakeInput\" dojoAttachPoint=\"fakeNodeHolder\">\r\n\t\t<input class=\"dijitFileInputVisible\" type=\"text\" dojoAttachPoint=\"focusNode, inputNode\" />\r\n\t\t<div class=\"dijitInline dijitFileInputText\" dojoAttachPoint=\"titleNode\">${label}</div>\r\n\t\t<div class=\"dijitInline dijitFileInputButton\" dojoAttachPoint=\"cancelNode\" dojoAttachEvent=\"onclick:reset\">${cancelText}</div>\r\n\t</div>\r\n\t<div class=\"dijitProgressOverlay\" dojoAttachPoint=\"overlay\">&nbsp;</div>\r\n</div>\r\n",startup:function(){this._blurListener=this.connect(this.fileInput,this.triggerEvent,"_onBlur");this._focusListener=this.connect(this.fileInput,"onfocus","_onFocus");this.inherited(arguments);},_onFocus:function(){if(this._blurTimer){clearTimeout(this._blurTimer);}},_onBlur:function(){if(this._blurTimer){clearTimeout(this._blurTimer);}if(!this._sent){this._blurTimer=setTimeout(_4.hitch(this,"_sendFile"),this.blurDelay);}},setMessage:function(_7){this.overlay.removeChild(this.overlay.firstChild);this.overlay.appendChild(document.createTextNode(_7));},_sendFile:function(e){if(this._sent||this._sending||!this.fileInput.value){return;}this._sending=true;_4.style(this.fakeNodeHolder,"display","none");_4.style(this.overlay,{opacity:0,display:"block"});this.setMessage(this.uploadMessage);_4.fadeIn({node:this.overlay,duration:this.duration}).play();var _9;if(_4.isIE){_9=document.createElement("<form enctype=\"multipart/form-data\" method=\"post\">");_9.encoding="multipart/form-data";}else{_9=document.createElement("form");_9.setAttribute("enctype","multipart/form-data");}_9.appendChild(this.fileInput);_4.body().appendChild(_9);_4.io.iframe.send({url:this.url,form:_9,handleAs:"json",handle:_4.hitch(this,"_handleSend")});},_handleSend:function(_a,_b){this.overlay.removeChild(this.overlay.firstChild);this._sent=true;this._sending=false;_4.style(this.overlay,{opacity:0,border:"none",background:"none"});this.overlay.style.backgroundImage="none";this.fileInput.style.display="none";this.fakeNodeHolder.style.display="none";_4.fadeIn({node:this.overlay,duration:this.duration}).play(250);this.disconnect(this._blurListener);this.disconnect(this._focusListener);_4.body().removeChild(_b.args.form);this.fileInput=null;this.onComplete(_a,_b,this);},reset:function(e){if(this._blurTimer){clearTimeout(this._blurTimer);}this.disconnect(this._blurListener);this.disconnect(this._focusListener);this.overlay.style.display="none";this.fakeNodeHolder.style.display="";this.inherited(arguments);this._sent=false;this._sending=false;this._blurListener=this.connect(this.fileInput,this.triggerEvent,"_onBlur");this._focusListener=this.connect(this.fileInput,"onfocus","_onFocus");},onComplete:function(_d,_e,_f){}});_4.declare("dojox.form.FileInputBlind",_6.form.FileInputAuto,{startup:function(){this.inherited(arguments);this._off=_4.style(this.inputNode,"width");this.inputNode.style.display="none";this._fixPosition();},_fixPosition:function(){if(_4.isIE){_4.style(this.fileInput,"width","1px");}else{_4.style(this.fileInput,"left","-"+(this._off)+"px");}},reset:function(e){this.inherited(arguments);this._fixPosition();}});}}};});