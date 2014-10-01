/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {defineResource:function(_4,_5,_6){if(!window["OpenAjax"]){OpenAjax=new function(){var t=true;var f=false;var g=window;var _a;var _b="org.openajax.hub.";var h={};this.hub=h;h.implementer="http://openajax.org";h.implVersion="0.6";h.specVersion="0.6";h.implExtraData={};var _a={};h.libraries=_a;h.registerLibrary=function(_d,_e,_f,_10){_a[_d]={prefix:_d,namespaceURI:_e,version:_f,extraData:_10};this.publish(_b+"registerLibrary",_a[_d]);};h.unregisterLibrary=function(_11){this.publish(_b+"unregisterLibrary",_a[_11]);delete _a[_11];};h._subscriptions={c:{},s:[]};h._cleanup=[];h._subIndex=0;h._pubDepth=0;h.subscribe=function(_12,_13,_14,_15,_16){if(!_14){_14=window;}var _17=_12+"."+this._subIndex;var sub={scope:_14,cb:_13,fcb:_16,data:_15,sid:this._subIndex++,hdl:_17};var _19=_12.split(".");this._subscribe(this._subscriptions,_19,0,sub);return _17;};h.publish=function(_1a,_1b){var _1c=_1a.split(".");this._pubDepth++;this._publish(this._subscriptions,_1c,0,_1a,_1b);this._pubDepth--;if((this._cleanup.length>0)&&(this._pubDepth==0)){for(var i=0;i<this._cleanup.length;i++){this.unsubscribe(this._cleanup[i].hdl);}delete (this._cleanup);this._cleanup=[];}};h.unsubscribe=function(sub){var _1f=sub.split(".");var sid=_1f.pop();this._unsubscribe(this._subscriptions,_1f,0,sid);};h._subscribe=function(_21,_22,_23,sub){var _25=_22[_23];if(_23==_22.length){_21.s.push(sub);}else{if(typeof _21.c=="undefined"){_21.c={};}if(typeof _21.c[_25]=="undefined"){_21.c[_25]={c:{},s:[]};this._subscribe(_21.c[_25],_22,_23+1,sub);}else{this._subscribe(_21.c[_25],_22,_23+1,sub);}}};h._publish=function(_26,_27,_28,_29,msg){if(typeof _26!="undefined"){var _2b;if(_28==_27.length){_2b=_26;}else{this._publish(_26.c[_27[_28]],_27,_28+1,_29,msg);this._publish(_26.c["*"],_27,_28+1,_29,msg);_2b=_26.c["**"];}if(typeof _2b!="undefined"){var _2c=_2b.s;var max=_2c.length;for(var i=0;i<max;i++){if(_2c[i].cb){var sc=_2c[i].scope;var cb=_2c[i].cb;var fcb=_2c[i].fcb;var d=_2c[i].data;if(typeof cb=="string"){cb=sc[cb];}if(typeof fcb=="string"){fcb=sc[fcb];}if((!fcb)||(fcb.call(sc,_29,msg,d))){cb.call(sc,_29,msg,d);}}}}}};h._unsubscribe=function(_33,_34,_35,sid){if(typeof _33!="undefined"){if(_35<_34.length){var _37=_33.c[_34[_35]];this._unsubscribe(_37,_34,_35+1,sid);if(_37.s.length==0){for(var x in _37.c){return;}delete _33.c[_34[_35]];}return;}else{var _39=_33.s;var max=_39.length;for(var i=0;i<max;i++){if(sid==_39[i].sid){if(this._pubDepth>0){_39[i].cb=null;this._cleanup.push(_39[i]);}else{_39.splice(i,1);}return;}}}}};h.reinit=function(){for(var lib in OpenAjax.hub.libraries){delete OpenAjax.hub.libraries[lib];}OpenAjax.hub.registerLibrary("OpenAjax","http://openajax.org/hub","0.6",{});delete OpenAjax._subscriptions;OpenAjax._subscriptions={c:{},s:[]};delete OpenAjax._cleanup;OpenAjax._cleanup=[];OpenAjax._subIndex=0;OpenAjax._pubDepth=0;};};OpenAjax.hub.registerLibrary("OpenAjax","http://openajax.org/hub","0.6",{});}}};});