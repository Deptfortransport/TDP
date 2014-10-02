/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.io.proxy.xip"],["require","dojo.io.iframe"],["require","dojox.data.dom"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.io.proxy.xip"]){_4._hasResource["dojox.io.proxy.xip"]=true;_4.provide("dojox.io.proxy.xip");_4.require("dojo.io.iframe");_4.require("dojox.data.dom");_6.io.proxy.xip={xipClientUrl:((_4.config||djConfig)["xipClientUrl"])||_4.moduleUrl("dojox.io.proxy","xip_client.html"),urlLimit:4000,_callbackName:(_6._scopeName||"dojox")+".io.proxy.xip.fragmentReceived",_state:{},_stateIdCounter:0,_isWebKit:navigator.userAgent.indexOf("WebKit")!=-1,send:function(_7){var _8=this.xipClientUrl;if(_8.split(":")[0].match(/javascript/i)||_7._ifpServerUrl.split(":")[0].match(/javascript/i)){return;}var _9=_8.indexOf(":");var _a=_8.indexOf("/");if(_9==-1||_a<_9){var _b=window.location.href;if(_a==0){_8=_b.substring(0,_b.indexOf("/",9))+_8;}else{_8=_b.substring(0,(_b.lastIndexOf("/")+1))+_8;}}this.fullXipClientUrl=_8;if(typeof document.postMessage!="undefined"){document.addEventListener("message",_4.hitch(this,this.fragmentReceivedEvent),false);}this.send=this._realSend;return this._realSend(_7);},_realSend:function(_c){var _d="XhrIframeProxy"+(this._stateIdCounter++);_c._stateId=_d;var _e=_c._ifpServerUrl+"#0:init:id="+_d+"&client="+encodeURIComponent(this.fullXipClientUrl)+"&callback="+encodeURIComponent(this._callbackName);this._state[_d]={facade:_c,stateId:_d,clientFrame:_4.io.iframe.create(_d,"",_e),isSending:false,serverUrl:_c._ifpServerUrl,requestData:null,responseMessage:"",requestParts:[],idCounter:1,partIndex:0,serverWindow:null};return _d;},receive:function(_f,_10){var _11={};var _12=_10.split("&");for(var i=0;i<_12.length;i++){if(_12[i]){var _14=_12[i].split("=");_11[decodeURIComponent(_14[0])]=decodeURIComponent(_14[1]);}}var _15=this._state[_f];var _16=_15.facade;_16._setResponseHeaders(_11.responseHeaders);if(_11.status==0||_11.status){_16.status=parseInt(_11.status,10);}if(_11.statusText){_16.statusText=_11.statusText;}if(_11.responseText){_16.responseText=_11.responseText;var _17=_16.getResponseHeader("Content-Type");if(_17){var _18=_17.split(";")[0];if(_18.indexOf("application/xml")==0||_18.indexOf("text/xml")==0){_16.responseXML=_6.data.dom.createDocument(_11.responseText,_17);}}}_16.readyState=4;this.destroyState(_f);},frameLoaded:function(_19){var _1a=this._state[_19];var _1b=_1a.facade;var _1c=[];for(var _1d in _1b._requestHeaders){_1c.push(_1d+": "+_1b._requestHeaders[_1d]);}var _1e={uri:_1b._uri};if(_1c.length>0){_1e.requestHeaders=_1c.join("\r\n");}if(_1b._method){_1e.method=_1b._method;}if(_1b._bodyData){_1e.data=_1b._bodyData;}this.sendRequest(_19,_4.objectToQuery(_1e));},destroyState:function(_1f){var _20=this._state[_1f];if(_20){delete this._state[_1f];var _21=_20.clientFrame.parentNode;_21.removeChild(_20.clientFrame);_20.clientFrame=null;_20=null;}},createFacade:function(){if(arguments&&arguments[0]&&arguments[0].iframeProxyUrl){return new _6.io.proxy.xip.XhrIframeFacade(arguments[0].iframeProxyUrl);}else{return _6.io.proxy.xip._xhrObjOld.apply(_4,arguments);}},sendRequest:function(_22,_23){var _24=this._state[_22];if(!_24.isSending){_24.isSending=true;_24.requestData=_23||"";_24.serverWindow=frames[_24.stateId];if(!_24.serverWindow){_24.serverWindow=document.getElementById(_24.stateId).contentWindow;}if(typeof document.postMessage=="undefined"){if(_24.serverWindow.contentWindow){_24.serverWindow=_24.serverWindow.contentWindow;}}this.sendRequestStart(_22);}},sendRequestStart:function(_25){var _26=this._state[_25];_26.requestParts=[];var _27=_26.requestData;var _28=_26.serverUrl.length;var _29=this.urlLimit-_28;var _2a=0;while((_27.length-_2a)+_28>this.urlLimit){var _2b=_27.substring(_2a,_2a+_29);var _2c=_2b.lastIndexOf("%");if(_2c==_2b.length-1||_2c==_2b.length-2){_2b=_2b.substring(0,_2c);}_26.requestParts.push(_2b);_2a+=_2b.length;}_26.requestParts.push(_27.substring(_2a,_27.length));_26.partIndex=0;this.sendRequestPart(_25);},sendRequestPart:function(_2d){var _2e=this._state[_2d];if(_2e.partIndex<_2e.requestParts.length){var _2f=_2e.requestParts[_2e.partIndex];var cmd="part";if(_2e.partIndex+1==_2e.requestParts.length){cmd="end";}else{if(_2e.partIndex==0){cmd="start";}}this.setServerUrl(_2d,cmd,_2f);_2e.partIndex++;}},setServerUrl:function(_31,cmd,_33){var _34=this.makeServerUrl(_31,cmd,_33);var _35=this._state[_31];if(this._isWebKit){_35.serverWindow.location=_34;}else{_35.serverWindow.location.replace(_34);}},makeServerUrl:function(_36,cmd,_38){var _39=this._state[_36];var _3a=_39.serverUrl+"#"+(_39.idCounter++)+":"+cmd;if(_38){_3a+=":"+_38;}return _3a;},fragmentReceivedEvent:function(evt){if(evt.uri.split("#")[0]==this.fullXipClientUrl){this.fragmentReceived(evt.data);}},fragmentReceived:function(_3c){var _3d=_3c.indexOf("#");var _3e=_3c.substring(0,_3d);var _3f=_3c.substring(_3d+1,_3c.length);var msg=this.unpackMessage(_3f);var _41=this._state[_3e];switch(msg.command){case "loaded":this.frameLoaded(_3e);break;case "ok":this.sendRequestPart(_3e);break;case "start":_41.responseMessage=""+msg.message;this.setServerUrl(_3e,"ok");break;case "part":_41.responseMessage+=msg.message;this.setServerUrl(_3e,"ok");break;case "end":this.setServerUrl(_3e,"ok");_41.responseMessage+=msg.message;this.receive(_3e,_41.responseMessage);break;}},unpackMessage:function(_42){var _43=_42.split(":");var _44=_43[1];_42=_43[2]||"";var _45=null;if(_44=="init"){var _46=_42.split("&");_45={};for(var i=0;i<_46.length;i++){var _48=_46[i].split("=");_45[decodeURIComponent(_48[0])]=decodeURIComponent(_48[1]);}}return {command:_44,message:_42,config:_45};}};_6.io.proxy.xip._xhrObjOld=_4._xhrObj;_4._xhrObj=_6.io.proxy.xip.createFacade;_6.io.proxy.xip.XhrIframeFacade=function(_49){this._requestHeaders={};this._allResponseHeaders=null;this._responseHeaders={};this._method=null;this._uri=null;this._bodyData=null;this.responseText=null;this.responseXML=null;this.status=null;this.statusText=null;this.readyState=0;this._ifpServerUrl=_49;this._stateId=null;};_4.extend(_6.io.proxy.xip.XhrIframeFacade,{open:function(_4a,uri){this._method=_4a;this._uri=uri;this.readyState=1;},setRequestHeader:function(_4c,_4d){this._requestHeaders[_4c]=_4d;},send:function(_4e){this._bodyData=_4e;this._stateId=_6.io.proxy.xip.send(this);this.readyState=2;},abort:function(){_6.io.proxy.xip.destroyState(this._stateId);},getAllResponseHeaders:function(){return this._allResponseHeaders;},getResponseHeader:function(_4f){return this._responseHeaders[_4f];},_setResponseHeaders:function(_50){if(_50){this._allResponseHeaders=_50;_50=_50.replace(/\r/g,"");var _51=_50.split("\n");for(var i=0;i<_51.length;i++){if(_51[i]){var _53=_51[i].split(": ");this._responseHeaders[_53[0]]=_53[1];}}}}});}}};});