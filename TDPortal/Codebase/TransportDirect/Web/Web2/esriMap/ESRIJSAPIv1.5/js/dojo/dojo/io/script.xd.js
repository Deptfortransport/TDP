/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.io.script"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.io.script"]){_4._hasResource["dojo.io.script"]=true;_4.provide("dojo.io.script");_4.io.script={get:function(_7){var _8=this._makeScriptDeferred(_7);var _9=_8.ioArgs;_4._ioAddQueryToUrl(_9);if(this._canAttach(_9)){this.attach(_9.id,_9.url,_7.frameDoc);}_4._ioWatch(_8,this._validCheck,this._ioCheck,this._resHandle);return _8;},attach:function(id,_b,_c){var _d=(_c||_4.doc);var _e=_d.createElement("script");_e.type="text/javascript";_e.src=_b;_e.id=id;_e.charset="utf-8";_d.getElementsByTagName("head")[0].appendChild(_e);},remove:function(id,_10){_4.destroy(_4.byId(id,_10));if(this["jsonp_"+id]){delete this["jsonp_"+id];}},_makeScriptDeferred:function(_11){var dfd=_4._ioSetArgs(_11,this._deferredCancel,this._deferredOk,this._deferredError);var _13=dfd.ioArgs;_13.id=_4._scopeName+"IoScript"+(this._counter++);_13.canDelete=false;if(_11.callbackParamName){_13.query=_13.query||"";if(_13.query.length>0){_13.query+="&";}_13.query+=_11.callbackParamName+"="+(_11.frameDoc?"parent.":"")+_4._scopeName+".io.script.jsonp_"+_13.id+"._jsonpCallback";_13.frameDoc=_11.frameDoc;_13.canDelete=true;dfd._jsonpCallback=this._jsonpCallback;this["jsonp_"+_13.id]=dfd;}return dfd;},_deferredCancel:function(dfd){dfd.canceled=true;if(dfd.ioArgs.canDelete){_4.io.script._addDeadScript(dfd.ioArgs);}},_deferredOk:function(dfd){if(dfd.ioArgs.canDelete){_4.io.script._addDeadScript(dfd.ioArgs);}if(dfd.ioArgs.json){return dfd.ioArgs.json;}else{return dfd.ioArgs;}},_deferredError:function(_16,dfd){if(dfd.ioArgs.canDelete){if(_16.dojoType=="timeout"){_4.io.script.remove(dfd.ioArgs.id,dfd.ioArgs.frameDoc);}else{_4.io.script._addDeadScript(dfd.ioArgs);}}console.log("dojo.io.script error",_16);return _16;},_deadScripts:[],_counter:1,_addDeadScript:function(_18){_4.io.script._deadScripts.push({id:_18.id,frameDoc:_18.frameDoc});_18.frameDoc=null;},_validCheck:function(dfd){var _1a=_4.io.script;var _1b=_1a._deadScripts;if(_1b&&_1b.length>0){for(var i=0;i<_1b.length;i++){_1a.remove(_1b[i].id,_1b[i].frameDoc);_1b[i].frameDoc=null;}_4.io.script._deadScripts=[];}return true;},_ioCheck:function(dfd){if(dfd.ioArgs.json){return true;}var _1e=dfd.ioArgs.args.checkString;if(_1e&&eval("typeof("+_1e+") != 'undefined'")){return true;}return false;},_resHandle:function(dfd){if(_4.io.script._ioCheck(dfd)){dfd.callback(dfd);}else{dfd.errback(new Error("inconceivable dojo.io.script._resHandle error"));}},_canAttach:function(_20){return true;},_jsonpCallback:function(_21){this.ioArgs.json=_21;}};}}};});