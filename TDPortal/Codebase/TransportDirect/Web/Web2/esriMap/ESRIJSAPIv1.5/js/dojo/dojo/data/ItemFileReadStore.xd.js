/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.data.ItemFileReadStore"],["require","dojo.data.util.filter"],["require","dojo.data.util.simpleFetch"],["require","dojo.date.stamp"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.data.ItemFileReadStore"]){_4._hasResource["dojo.data.ItemFileReadStore"]=true;_4.provide("dojo.data.ItemFileReadStore");_4.require("dojo.data.util.filter");_4.require("dojo.data.util.simpleFetch");_4.require("dojo.date.stamp");_4.declare("dojo.data.ItemFileReadStore",null,{constructor:function(_7){this._arrayOfAllItems=[];this._arrayOfTopLevelItems=[];this._loadFinished=false;this._jsonFileUrl=_7.url;this._jsonData=_7.data;this._datatypeMap=_7.typeMap||{};if(!this._datatypeMap["Date"]){this._datatypeMap["Date"]={type:Date,deserialize:function(_8){return _4.date.stamp.fromISOString(_8);}};}this._features={"dojo.data.api.Read":true,"dojo.data.api.Identity":true};this._itemsByIdentity=null;this._storeRefPropName="_S";this._itemNumPropName="_0";this._rootItemPropName="_RI";this._reverseRefMap="_RRM";this._loadInProgress=false;this._queuedFetches=[];if(_7.urlPreventCache!==undefined){this.urlPreventCache=_7.urlPreventCache?true:false;}if(_7.clearOnClose){this.clearOnClose=true;}},url:"",data:null,typeMap:null,clearOnClose:false,urlPreventCache:false,_assertIsItem:function(_9){if(!this.isItem(_9)){throw new Error("dojo.data.ItemFileReadStore: Invalid item argument.");}},_assertIsAttribute:function(_a){if(typeof _a!=="string"){throw new Error("dojo.data.ItemFileReadStore: Invalid attribute argument.");}},getValue:function(_b,_c,_d){var _e=this.getValues(_b,_c);return (_e.length>0)?_e[0]:_d;},getValues:function(_f,_10){this._assertIsItem(_f);this._assertIsAttribute(_10);return _f[_10]||[];},getAttributes:function(_11){this._assertIsItem(_11);var _12=[];for(var key in _11){if((key!==this._storeRefPropName)&&(key!==this._itemNumPropName)&&(key!==this._rootItemPropName)&&(key!==this._reverseRefMap)){_12.push(key);}}return _12;},hasAttribute:function(_14,_15){return this.getValues(_14,_15).length>0;},containsValue:function(_16,_17,_18){var _19=undefined;if(typeof _18==="string"){_19=_4.data.util.filter.patternToRegExp(_18,false);}return this._containsValue(_16,_17,_18,_19);},_containsValue:function(_1a,_1b,_1c,_1d){return _4.some(this.getValues(_1a,_1b),function(_1e){if(_1e!==null&&!_4.isObject(_1e)&&_1d){if(_1e.toString().match(_1d)){return true;}}else{if(_1c===_1e){return true;}}});},isItem:function(_1f){if(_1f&&_1f[this._storeRefPropName]===this){if(this._arrayOfAllItems[_1f[this._itemNumPropName]]===_1f){return true;}}return false;},isItemLoaded:function(_20){return this.isItem(_20);},loadItem:function(_21){this._assertIsItem(_21.item);},getFeatures:function(){return this._features;},getLabel:function(_22){if(this._labelAttr&&this.isItem(_22)){return this.getValue(_22,this._labelAttr);}return undefined;},getLabelAttributes:function(_23){if(this._labelAttr){return [this._labelAttr];}return null;},_fetchItems:function(_24,_25,_26){var _27=this;var _28=function(_29,_2a){var _2b=[];var i,key;if(_29.query){var _2e;var _2f=_29.queryOptions?_29.queryOptions.ignoreCase:false;var _30={};for(key in _29.query){_2e=_29.query[key];if(typeof _2e==="string"){_30[key]=_4.data.util.filter.patternToRegExp(_2e,_2f);}}for(i=0;i<_2a.length;++i){var _31=true;var _32=_2a[i];if(_32===null){_31=false;}else{for(key in _29.query){_2e=_29.query[key];if(!_27._containsValue(_32,key,_2e,_30[key])){_31=false;}}}if(_31){_2b.push(_32);}}_25(_2b,_29);}else{for(i=0;i<_2a.length;++i){var _33=_2a[i];if(_33!==null){_2b.push(_33);}}_25(_2b,_29);}};if(this._loadFinished){_28(_24,this._getItemsArray(_24.queryOptions));}else{if(this._jsonFileUrl){if(this._loadInProgress){this._queuedFetches.push({args:_24,filter:_28});}else{this._loadInProgress=true;var _34={url:_27._jsonFileUrl,handleAs:"json-comment-optional",preventCache:this.urlPreventCache};var _35=_4.xhrGet(_34);_35.addCallback(function(_36){try{_27._getItemsFromLoadedData(_36);_27._loadFinished=true;_27._loadInProgress=false;_28(_24,_27._getItemsArray(_24.queryOptions));_27._handleQueuedFetches();}catch(e){_27._loadFinished=true;_27._loadInProgress=false;_26(e,_24);}});_35.addErrback(function(_37){_27._loadInProgress=false;_26(_37,_24);});var _38=null;if(_24.abort){_38=_24.abort;}_24.abort=function(){var df=_35;if(df&&df.fired===-1){df.cancel();df=null;}if(_38){_38.call(_24);}};}}else{if(this._jsonData){try{this._loadFinished=true;this._getItemsFromLoadedData(this._jsonData);this._jsonData=null;_28(_24,this._getItemsArray(_24.queryOptions));}catch(e){_26(e,_24);}}else{_26(new Error("dojo.data.ItemFileReadStore: No JSON source data was provided as either URL or a nested Javascript object."),_24);}}}},_handleQueuedFetches:function(){if(this._queuedFetches.length>0){for(var i=0;i<this._queuedFetches.length;i++){var _3b=this._queuedFetches[i];var _3c=_3b.args;var _3d=_3b.filter;if(_3d){_3d(_3c,this._getItemsArray(_3c.queryOptions));}else{this.fetchItemByIdentity(_3c);}}this._queuedFetches=[];}},_getItemsArray:function(_3e){if(_3e&&_3e.deep){return this._arrayOfAllItems;}return this._arrayOfTopLevelItems;},close:function(_3f){if(this.clearOnClose&&(this._jsonFileUrl!=="")){this._arrayOfAllItems=[];this._arrayOfTopLevelItems=[];this._loadFinished=false;this._itemsByIdentity=null;this._loadInProgress=false;this._queuedFetches=[];}},_getItemsFromLoadedData:function(_40){var _41=false;function _42(_43){var _44=((_43!==null)&&(typeof _43==="object")&&(!_4.isArray(_43)||_41)&&(!_4.isFunction(_43))&&(_43.constructor==Object||_4.isArray(_43))&&(typeof _43._reference==="undefined")&&(typeof _43._type==="undefined")&&(typeof _43._value==="undefined"));return _44;};var _45=this;function _46(_47){_45._arrayOfAllItems.push(_47);for(var _48 in _47){var _49=_47[_48];if(_49){if(_4.isArray(_49)){var _4a=_49;for(var k=0;k<_4a.length;++k){var _4c=_4a[k];if(_42(_4c)){_46(_4c);}}}else{if(_42(_49)){_46(_49);}}}}};this._labelAttr=_40.label;var i;var _4e;this._arrayOfAllItems=[];this._arrayOfTopLevelItems=_40.items;for(i=0;i<this._arrayOfTopLevelItems.length;++i){_4e=this._arrayOfTopLevelItems[i];if(_4.isArray(_4e)){_41=true;}_46(_4e);_4e[this._rootItemPropName]=true;}var _4f={};var key;for(i=0;i<this._arrayOfAllItems.length;++i){_4e=this._arrayOfAllItems[i];for(key in _4e){if(key!==this._rootItemPropName){var _51=_4e[key];if(_51!==null){if(!_4.isArray(_51)){_4e[key]=[_51];}}else{_4e[key]=[null];}}_4f[key]=key;}}while(_4f[this._storeRefPropName]){this._storeRefPropName+="_";}while(_4f[this._itemNumPropName]){this._itemNumPropName+="_";}while(_4f[this._reverseRefMap]){this._reverseRefMap+="_";}var _52;var _53=_40.identifier;if(_53){this._itemsByIdentity={};this._features["dojo.data.api.Identity"]=_53;for(i=0;i<this._arrayOfAllItems.length;++i){_4e=this._arrayOfAllItems[i];_52=_4e[_53];var _54=_52[0];if(!this._itemsByIdentity[_54]){this._itemsByIdentity[_54]=_4e;}else{if(this._jsonFileUrl){throw new Error("dojo.data.ItemFileReadStore:  The json data as specified by: ["+this._jsonFileUrl+"] is malformed.  Items within the list have identifier: ["+_53+"].  Value collided: ["+_54+"]");}else{if(this._jsonData){throw new Error("dojo.data.ItemFileReadStore:  The json data provided by the creation arguments is malformed.  Items within the list have identifier: ["+_53+"].  Value collided: ["+_54+"]");}}}}}else{this._features["dojo.data.api.Identity"]=Number;}for(i=0;i<this._arrayOfAllItems.length;++i){_4e=this._arrayOfAllItems[i];_4e[this._storeRefPropName]=this;_4e[this._itemNumPropName]=i;}for(i=0;i<this._arrayOfAllItems.length;++i){_4e=this._arrayOfAllItems[i];for(key in _4e){_52=_4e[key];for(var j=0;j<_52.length;++j){_51=_52[j];if(_51!==null&&typeof _51=="object"){if(_51._type&&_51._value){var _56=_51._type;var _57=this._datatypeMap[_56];if(!_57){throw new Error("dojo.data.ItemFileReadStore: in the typeMap constructor arg, no object class was specified for the datatype '"+_56+"'");}else{if(_4.isFunction(_57)){_52[j]=new _57(_51._value);}else{if(_4.isFunction(_57.deserialize)){_52[j]=_57.deserialize(_51._value);}else{throw new Error("dojo.data.ItemFileReadStore: Value provided in typeMap was neither a constructor, nor a an object with a deserialize function");}}}}if(_51._reference){var _58=_51._reference;if(!_4.isObject(_58)){_52[j]=this._itemsByIdentity[_58];}else{for(var k=0;k<this._arrayOfAllItems.length;++k){var _5a=this._arrayOfAllItems[k];var _5b=true;for(var _5c in _58){if(_5a[_5c]!=_58[_5c]){_5b=false;}}if(_5b){_52[j]=_5a;}}}if(this.referenceIntegrity){var _5d=_52[j];if(this.isItem(_5d)){this._addReferenceToMap(_5d,_4e,key);}}}else{if(this.isItem(_51)){if(this.referenceIntegrity){this._addReferenceToMap(_51,_4e,key);}}}}}}}},_addReferenceToMap:function(_5e,_5f,_60){},getIdentity:function(_61){var _62=this._features["dojo.data.api.Identity"];if(_62===Number){return _61[this._itemNumPropName];}else{var _63=_61[_62];if(_63){return _63[0];}}return null;},fetchItemByIdentity:function(_64){var _65;var _66;if(!this._loadFinished){var _67=this;if(this._jsonFileUrl){if(this._loadInProgress){this._queuedFetches.push({args:_64});}else{this._loadInProgress=true;var _68={url:_67._jsonFileUrl,handleAs:"json-comment-optional",preventCache:this.urlPreventCache};var _69=_4.xhrGet(_68);_69.addCallback(function(_6a){var _6b=_64.scope?_64.scope:_4.global;try{_67._getItemsFromLoadedData(_6a);_67._loadFinished=true;_67._loadInProgress=false;_65=_67._getItemByIdentity(_64.identity);if(_64.onItem){_64.onItem.call(_6b,_65);}_67._handleQueuedFetches();}catch(error){_67._loadInProgress=false;if(_64.onError){_64.onError.call(_6b,error);}}});_69.addErrback(function(_6c){_67._loadInProgress=false;if(_64.onError){var _6d=_64.scope?_64.scope:_4.global;_64.onError.call(_6d,_6c);}});}}else{if(this._jsonData){_67._getItemsFromLoadedData(_67._jsonData);_67._jsonData=null;_67._loadFinished=true;_65=_67._getItemByIdentity(_64.identity);if(_64.onItem){_66=_64.scope?_64.scope:_4.global;_64.onItem.call(_66,_65);}}}}else{_65=this._getItemByIdentity(_64.identity);if(_64.onItem){_66=_64.scope?_64.scope:_4.global;_64.onItem.call(_66,_65);}}},_getItemByIdentity:function(_6e){var _6f=null;if(this._itemsByIdentity){_6f=this._itemsByIdentity[_6e];}else{_6f=this._arrayOfAllItems[_6e];}if(_6f===undefined){_6f=null;}return _6f;},getIdentityAttributes:function(_70){var _71=this._features["dojo.data.api.Identity"];if(_71===Number){return null;}else{return [_71];}},_forceLoad:function(){var _72=this;if(this._jsonFileUrl){var _73={url:_72._jsonFileUrl,handleAs:"json-comment-optional",preventCache:this.urlPreventCache,sync:true};var _74=_4.xhrGet(_73);_74.addCallback(function(_75){try{if(_72._loadInProgress!==true&&!_72._loadFinished){_72._getItemsFromLoadedData(_75);_72._loadFinished=true;}else{if(_72._loadInProgress){throw new Error("dojo.data.ItemFileReadStore:  Unable to perform a synchronous load, an async load is in progress.");}}}catch(e){console.log(e);throw e;}});_74.addErrback(function(_76){throw _76;});}else{if(this._jsonData){_72._getItemsFromLoadedData(_72._jsonData);_72._jsonData=null;_72._loadFinished=true;}}}});_4.extend(_4.data.ItemFileReadStore,_4.data.util.simpleFetch);}}};});