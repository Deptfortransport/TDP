/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.storage.AirEncryptedLocalStorageProvider"],["require","dojox.storage.manager"],["require","dojox.storage.Provider"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.storage.AirEncryptedLocalStorageProvider"]){_4._hasResource["dojox.storage.AirEncryptedLocalStorageProvider"]=true;_4.provide("dojox.storage.AirEncryptedLocalStorageProvider");_4.require("dojox.storage.manager");_4.require("dojox.storage.Provider");if(_4.isAIR){(function(){if(!_7){var _7={};}_7.ByteArray=window.runtime.flash.utils.ByteArray;_7.EncryptedLocalStore=window.runtime.flash.data.EncryptedLocalStore,_4.declare("dojox.storage.AirEncryptedLocalStorageProvider",[_6.storage.Provider],{initialize:function(){_6.storage.manager.loaded();},isAvailable:function(){return true;},_getItem:function(_8){var _9=_7.EncryptedLocalStore.getItem("__dojo_"+_8);return _9?_9.readUTFBytes(_9.length):"";},_setItem:function(_a,_b){var _c=new _7.ByteArray();_c.writeUTFBytes(_b);_7.EncryptedLocalStore.setItem("__dojo_"+_a,_c);},_removeItem:function(_d){_7.EncryptedLocalStore.removeItem("__dojo_"+_d);},put:function(_e,_f,_10,_11){if(this.isValidKey(_e)==false){throw new Error("Invalid key given: "+_e);}_11=_11||this.DEFAULT_NAMESPACE;if(this.isValidKey(_11)==false){throw new Error("Invalid namespace given: "+_11);}try{var _12=this._getItem("namespaces")||"|";if(_12.indexOf("|"+_11+"|")==-1){this._setItem("namespaces",_12+_11+"|");}var _13=this._getItem(_11+"_keys")||"|";if(_13.indexOf("|"+_e+"|")==-1){this._setItem(_11+"_keys",_13+_e+"|");}this._setItem("_"+_11+"_"+_e,_f);}catch(e){console.debug("dojox.storage.AirEncryptedLocalStorageProvider.put:",e);_10(this.FAILED,_e,e.toString(),_11);return;}if(_10){_10(this.SUCCESS,_e,null,_11);}},get:function(key,_15){if(this.isValidKey(key)==false){throw new Error("Invalid key given: "+key);}_15=_15||this.DEFAULT_NAMESPACE;return this._getItem("_"+_15+"_"+key);},getNamespaces:function(){var _16=[this.DEFAULT_NAMESPACE];var _17=(this._getItem("namespaces")||"|").split("|");for(var i=0;i<_17.length;i++){if(_17[i].length&&_17[i]!=this.DEFAULT_NAMESPACE){_16.push(_17[i]);}}return _16;},getKeys:function(_19){_19=_19||this.DEFAULT_NAMESPACE;if(this.isValidKey(_19)==false){throw new Error("Invalid namespace given: "+_19);}var _1a=[];var _1b=(this._getItem(_19+"_keys")||"|").split("|");for(var i=0;i<_1b.length;i++){if(_1b[i].length){_1a.push(_1b[i]);}}return _1a;},clear:function(_1d){if(this.isValidKey(_1d)==false){throw new Error("Invalid namespace given: "+_1d);}var _1e=this._getItem("namespaces")||"|";if(_1e.indexOf("|"+_1d+"|")!=-1){this._setItem("namespaces",_1e.replace("|"+_1d+"|","|"));}var _1f=(this._getItem(_1d+"_keys")||"|").split("|");for(var i=0;i<_1f.length;i++){if(_1f[i].length){this._removeItem(_1d+"_"+_1f[i]);}}this._removeItem(_1d+"_keys");},remove:function(key,_22){_22=_22||this.DEFAULT_NAMESPACE;var _23=this._getItem(_22+"_keys")||"|";if(_23.indexOf("|"+key+"|")!=-1){this._setItem(_22+"_keys",_23.replace("|"+key+"|","|"));}this._removeItem("_"+_22+"_"+key);},putMultiple:function(_24,_25,_26,_27){if(this.isValidKeyArray(_24)===false||!_25 instanceof Array||_24.length!=_25.length){throw new Error("Invalid arguments: keys = ["+_24+"], values = ["+_25+"]");}if(_27==null||typeof _27=="undefined"){_27=this.DEFAULT_NAMESPACE;}if(this.isValidKey(_27)==false){throw new Error("Invalid namespace given: "+_27);}this._statusHandler=_26;try{for(var i=0;i<_24.length;i++){this.put(_24[i],_25[i],null,_27);}}catch(e){console.debug("dojox.storage.AirEncryptedLocalStorageProvider.putMultiple:",e);if(_26){_26(this.FAILED,_24,e.toString(),_27);}return;}if(_26){_26(this.SUCCESS,_24,null);}},getMultiple:function(_29,_2a){if(this.isValidKeyArray(_29)===false){throw new Error("Invalid key array given: "+_29);}if(_2a==null||typeof _2a=="undefined"){_2a=this.DEFAULT_NAMESPACE;}if(this.isValidKey(_2a)==false){throw new Error("Invalid namespace given: "+_2a);}var _2b=[];for(var i=0;i<_29.length;i++){_2b[i]=this.get(_29[i],_2a);}return _2b;},removeMultiple:function(_2d,_2e){_2e=_2e||this.DEFAULT_NAMESPACE;for(var i=0;i<_2d.length;i++){this.remove(_2d[i],_2e);}},isPermanent:function(){return true;},getMaximumSize:function(){return this.SIZE_NO_LIMIT;},hasSettingsUI:function(){return false;},showSettingsUI:function(){throw new Error(this.declaredClass+" does not support a storage settings user-interface");},hideSettingsUI:function(){throw new Error(this.declaredClass+" does not support a storage settings user-interface");}});_6.storage.manager.register("dojox.storage.AirEncryptedLocalStorageProvider",new _6.storage.AirEncryptedLocalStorageProvider());_6.storage.manager.initialize();})();}}}};});