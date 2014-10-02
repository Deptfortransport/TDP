/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.cometd._base"],["require","dojo.AdapterRegistry"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.cometd._base"]){_4._hasResource["dojox.cometd._base"]=true;_4.provide("dojox.cometd._base");_4.require("dojo.AdapterRegistry");_6.cometd={Connection:function(_7){_4.mixin(this,{prefix:_7,_status:"unconnected",_handshook:false,_initialized:false,_polling:false,expectedNetworkDelay:10000,connectTimeout:0,version:"1.0",minimumVersion:"0.9",clientId:null,messageId:0,batch:0,_isXD:false,handshakeReturn:null,currentTransport:null,url:null,lastMessage:null,_messageQ:[],handleAs:"json",_advice:{},_backoffInterval:0,_backoffIncrement:1000,_backoffMax:60000,_deferredSubscribes:{},_deferredUnsubscribes:{},_subscriptions:[],_extendInList:[],_extendOutList:[]});this.state=function(){return this._status;};this.init=function(_8,_9,_a){_9=_9||{};_9.version=this.version;_9.minimumVersion=this.minimumVersion;_9.channel="/meta/handshake";_9.id=""+this.messageId++;this.url=_8||_4.config["cometdRoot"];if(!this.url){throw "no cometd root";return null;}var _b="^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?$";var _c=(""+window.location).match(new RegExp(_b));if(_c[4]){var _d=_c[4].split(":");var _e=_d[0];var _f=_d[1]||"80";_c=this.url.match(new RegExp(_b));if(_c[4]){_d=_c[4].split(":");var _10=_d[0];var _11=_d[1]||"80";this._isXD=((_10!=_e)||(_11!=_f));}}if(!this._isXD){_9.supportedConnectionTypes=_4.map(_6.cometd.connectionTypes.pairs,"return item[0]");}_9=this._extendOut(_9);var _12={url:this.url,handleAs:this.handleAs,content:{"message":_4.toJson([_9])},load:_4.hitch(this,function(msg){this._backon();this._finishInit(msg);}),error:_4.hitch(this,function(e){this._backoff();this._finishInit(e);}),timeout:this.expectedNetworkDelay};if(_a){_4.mixin(_12,_a);}this._props=_9;for(var _15 in this._subscriptions){for(var sub in this._subscriptions[_15]){if(this._subscriptions[_15][sub].topic){_4.unsubscribe(this._subscriptions[_15][sub].topic);}}}this._messageQ=[];this._subscriptions=[];this._initialized=true;this._status="handshaking";this.batch=0;this.startBatch();var r;if(this._isXD){_12.callbackParamName="jsonp";r=_4.io.script.get(_12);}else{r=_4.xhrPost(_12);}return r;};this.publish=function(_18,_19,_1a){var _1b={data:_19,channel:_18};if(_1a){_4.mixin(_1b,_1a);}this._sendMessage(_1b);};this.subscribe=function(_1c,_1d,_1e,_1f){_1f=_1f||{};if(_1d){var _20=_7+_1c;var _21=this._subscriptions[_20];if(!_21||_21.length==0){_21=[];_1f.channel="/meta/subscribe";_1f.subscription=_1c;this._sendMessage(_1f);var _ds=this._deferredSubscribes;if(_ds[_1c]){_ds[_1c].cancel();delete _ds[_1c];}_ds[_1c]=new _4.Deferred();}for(var i in _21){if(_21[i].objOrFunc===_1d&&(!_21[i].funcName&&!_1e||_21[i].funcName==_1e)){return null;}}var _24=_4.subscribe(_20,_1d,_1e);_21.push({topic:_24,objOrFunc:_1d,funcName:_1e});this._subscriptions[_20]=_21;}var ret=this._deferredSubscribes[_1c]||{};ret.args=_4._toArray(arguments);return ret;};this.unsubscribe=function(_26,_27,_28,_29){if((arguments.length==1)&&(!_4.isString(_26))&&(_26.args)){return this.unsubscribe.apply(this,_26.args);}var _2a=_7+_26;var _2b=this._subscriptions[_2a];if(!_2b||_2b.length==0){return null;}var s=0;for(var i in _2b){var sb=_2b[i];if((!_27)||(sb.objOrFunc===_27&&(!sb.funcName&&!_28||sb.funcName==_28))){_4.unsubscribe(_2b[i].topic);delete _2b[i];}else{s++;}}if(s==0){_29=_29||{};_29.channel="/meta/unsubscribe";_29.subscription=_26;delete this._subscriptions[_2a];this._sendMessage(_29);this._deferredUnsubscribes[_26]=new _4.Deferred();if(this._deferredSubscribes[_26]){this._deferredSubscribes[_26].cancel();delete this._deferredSubscribes[_26];}}return this._deferredUnsubscribes[_26];};this.disconnect=function(){for(var _2f in this._subscriptions){for(var sub in this._subscriptions[_2f]){if(this._subscriptions[_2f][sub].topic){_4.unsubscribe(this._subscriptions[_2f][sub].topic);}}}this._subscriptions=[];this._messageQ=[];if(this._initialized&&this.currentTransport){this._initialized=false;this.currentTransport.disconnect();}if(!this._polling){this._publishMeta("connect",false);}this._initialized=false;this._handshook=false;this._status="disconnected";this._publishMeta("disconnect",true);};this.subscribed=function(_31,_32){};this.unsubscribed=function(_33,_34){};this.tunnelInit=function(_35,_36){};this.tunnelCollapse=function(){};this._backoff=function(){if(!this._advice){this._advice={reconnect:"retry",interval:0};}else{if(!this._advice.interval){this._advice.interval=0;}}if(this._backoffInterval<this._backoffMax){this._backoffInterval+=this._backoffIncrement;}};this._backon=function(){this._backoffInterval=0;};this._interval=function(){var i=this._backoffInterval+(this._advice?(this._advice.interval?this._advice.interval:0):0);if(i>0){console.log("Retry in interval+backoff="+this._advice.interval+"+"+this._backoffInterval+"="+i+"ms");}return i;};this._publishMeta=function(_38,_39,_3a){try{var _3b={cometd:this,action:_38,successful:_39,state:this.state()};if(_3a){_4.mixin(_3b,_3a);}_4.publish(this.prefix+"/meta",[_3b]);}catch(e){console.log(e);}};this._finishInit=function(_3c){if(this._status!="handshaking"){return;}var _3d=this._handshook;var _3e=false;var _3f={};if(_3c instanceof Error){_4.mixin(_3f,{reestablish:false,failure:true,error:_3c,advice:this._advice});}else{_3c=_3c[0];_3c=this._extendIn(_3c);this.handshakeReturn=_3c;if(_3c["advice"]){this._advice=_3c.advice;}_3e=_3c.successful?_3c.successful:false;if(_3c.version<this.minimumVersion){if(console.log){console.log("cometd protocol version mismatch. We wanted",this.minimumVersion,"but got",_3c.version);}_3e=false;this._advice.reconnect="none";}_4.mixin(_3f,{reestablish:_3e&&_3d,response:_3c});}this._publishMeta("handshake",_3e,_3f);if(this._status!="handshaking"){return;}if(_3e){this._status="connecting";this._handshook=true;this.currentTransport=_6.cometd.connectionTypes.match(_3c.supportedConnectionTypes,_3c.version,this._isXD);var _40=this.currentTransport;_40._cometd=this;_40.version=_3c.version;this.clientId=_3c.clientId;this.tunnelInit=_40.tunnelInit&&_4.hitch(_40,"tunnelInit");this.tunnelCollapse=_40.tunnelCollapse&&_4.hitch(_40,"tunnelCollapse");_40.startup(_3c);}else{if(!this._advice||this._advice["reconnect"]!="none"){setTimeout(_4.hitch(this,"init",this.url,this._props),this._interval());}}};this._extendIn=function(_41){_4.forEach(_6.cometd._extendInList,function(f){_41=f(_41)||_41;});return _41;};this._extendOut=function(_43){_4.forEach(_6.cometd._extendOutList,function(f){_43=f(_43)||_43;});return _43;};this.deliver=function(_45){_4.forEach(_45,this._deliver,this);return _45;};this._deliver=function(_46){_46=this._extendIn(_46);if(!_46["channel"]){if(_46["success"]!==true){return;}}this.lastMessage=_46;if(_46.advice){this._advice=_46.advice;}var _47=null;if((_46["channel"])&&(_46.channel.length>5)&&(_46.channel.substr(0,5)=="/meta")){switch(_46.channel){case "/meta/connect":var _48={response:_46};if(_46.successful){if(this._status!="connected"){this._status="connected";this.endBatch();}}if(this._initialized){this._publishMeta("connect",_46.successful,_48);}break;case "/meta/subscribe":_47=this._deferredSubscribes[_46.subscription];try{if(!_46.successful){if(_47){_47.errback(new Error(_46.error));}this.currentTransport.cancelConnect();return;}if(_47){_47.callback(true);}this.subscribed(_46.subscription,_46);}catch(e){log.warn(e);}break;case "/meta/unsubscribe":_47=this._deferredUnsubscribes[_46.subscription];try{if(!_46.successful){if(_47){_47.errback(new Error(_46.error));}this.currentTransport.cancelConnect();return;}if(_47){_47.callback(true);}this.unsubscribed(_46.subscription,_46);}catch(e){log.warn(e);}break;default:if(_46.successful&&!_46.successful){this.currentTransport.cancelConnect();return;}}}this.currentTransport.deliver(_46);if(_46.data){try{var _49=[_46];var _4a=_7+_46.channel;var _4b=_46.channel.split("/");var _4c=_7;for(var i=1;i<_4b.length-1;i++){_4.publish(_4c+"/**",_49);_4c+="/"+_4b[i];}_4.publish(_4c+"/**",_49);_4.publish(_4c+"/*",_49);_4.publish(_4a,_49);}catch(e){console.log(e);}}};this._sendMessage=function(_4e){if(this.currentTransport&&!this.batch){return this.currentTransport.sendMessages([_4e]);}else{this._messageQ.push(_4e);return null;}};this.startBatch=function(){this.batch++;};this.endBatch=function(){if(--this.batch<=0&&this.currentTransport&&this._status=="connected"){this.batch=0;var _4f=this._messageQ;this._messageQ=[];if(_4f.length>0){this.currentTransport.sendMessages(_4f);}}};this._onUnload=function(){_4.addOnUnload(_6.cometd,"disconnect");};this._connectTimeout=function(){var _50=0;if(this._advice&&this._advice.timeout&&this.expectedNetworkDelay>0){_50=this._advice.timeout+this.expectedNetworkDelay;}if(this.connectTimeout>0&&this.connectTimeout<_50){return this.connectTimeout;}return _50;};},connectionTypes:new _4.AdapterRegistry(true)};_6.cometd.Connection.call(_6.cometd,"/cometd");_4.addOnUnload(_6.cometd,"_onUnload");}}};});