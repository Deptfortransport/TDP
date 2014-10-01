/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.SnapLogicStore"],["require","dojo.io.script"],["require","dojo.data.util.sorter"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.SnapLogicStore"]){_4._hasResource["dojox.data.SnapLogicStore"]=true;_4.provide("dojox.data.SnapLogicStore");_4.require("dojo.io.script");_4.require("dojo.data.util.sorter");_4.declare("dojox.data.SnapLogicStore",null,{Parts:{DATA:"data",COUNT:"count"},url:"",constructor:function(_7){if(_7.url){this.url=_7.url;}this._parameters=_7.parameters;},_assertIsItem:function(_8){if(!this.isItem(_8)){throw new Error("dojox.data.SnapLogicStore: a function was passed an item argument that was not an item");}},_assertIsAttribute:function(_9){if(typeof _9!=="string"){throw new Error("dojox.data.SnapLogicStore: a function was passed an attribute argument that was not an attribute name string");}},getFeatures:function(){return {"dojo.data.api.Read":true};},getValue:function(_a,_b,_c){this._assertIsItem(_a);this._assertIsAttribute(_b);var i=_4.indexOf(_a.attributes,_b);if(i!==-1){return _a.values[i];}return _c;},getAttributes:function(_e){this._assertIsItem(_e);return _e.attributes;},hasAttribute:function(_f,_10){this._assertIsItem(_f);this._assertIsAttribute(_10);for(var i=0;i<_f.attributes.length;++i){if(_10==_f.attributes[i]){return true;}}return false;},isItemLoaded:function(_12){return this.isItem(_12);},loadItem:function(_13){},getLabel:function(_14){return undefined;},getLabelAttributes:function(_15){return null;},containsValue:function(_16,_17,_18){return this.getValue(_16,_17)===_18;},getValues:function(_19,_1a){this._assertIsItem(_19);this._assertIsAttribute(_1a);var i=_4.indexOf(_19.attributes,_1a);if(i!==-1){return [_19.values[i]];}return [];},isItem:function(_1c){if(_1c&&_1c._store===this){return true;}return false;},close:function(_1d){},_fetchHandler:function(_1e){var _1f=_1e.scope||_4.global;if(_1e.onBegin){_1e.onBegin.call(_1f,_1e._countResponse[0],_1e);}if(_1e.onItem||_1e.onComplete){var _20=_1e._dataResponse;if(!_20.length){_1e.onError.call(_1f,new Error("dojox.data.SnapLogicStore: invalid response of length 0"),_1e);return;}else{if(_1e.query!="record count"){var _21=_20.shift();var _22=[];for(var i=0;i<_20.length;++i){if(_1e._aborted){break;}_22.push({attributes:_21,values:_20[i],_store:this});}if(_1e.sort&&!_1e._aborted){_22.sort(_4.data.util.sorter.createSortFunction(_1e.sort,self));}}else{_22=[({attributes:["count"],values:_20,_store:this})];}}if(_1e.onItem){for(var i=0;i<_22.length;++i){if(_1e._aborted){break;}_1e.onItem.call(_1f,_22[i],_1e);}_22=null;}if(_1e.onComplete&&!_1e._aborted){_1e.onComplete.call(_1f,_22,_1e);}}},_partHandler:function(_24,_25,_26){if(_26 instanceof Error){if(_25==this.Parts.DATA){_24._dataHandle=null;}else{_24._countHandle=null;}_24._aborted=true;if(_24.onError){_24.onError.call(_24.scope,_26,_24);}}else{if(_24._aborted){return;}if(_25==this.Parts.DATA){_24._dataResponse=_26;}else{_24._countResponse=_26;}if((!_24._dataHandle||_24._dataResponse!==null)&&(!_24._countHandle||_24._countResponse!==null)){this._fetchHandler(_24);}}},fetch:function(_27){_27._countResponse=null;_27._dataResponse=null;_27._aborted=false;_27.abort=function(){if(!_27._aborted){_27._aborted=true;if(_27._dataHandle&&_27._dataHandle.cancel){_27._dataHandle.cancel();}if(_27._countHandle&&_27._countHandle.cancel){_27._countHandle.cancel();}}};if(_27.onItem||_27.onComplete){var _28=this._parameters||{};if(_27.start){if(_27.start<0){throw new Error("dojox.data.SnapLogicStore: request start value must be 0 or greater");}_28["sn.start"]=_27.start+1;}if(_27.count){if(_27.count<0){throw new Error("dojox.data.SnapLogicStore: request count value 0 or greater");}_28["sn.limit"]=_27.count;}_28["sn.content_type"]="application/javascript";var _29=this;var _2a=function(_2b,_2c){if(_2b instanceof Error){_29._fetchHandler(_2b,_27);}};var _2d={url:this.url,content:_28,timeout:60000,callbackParamName:"sn.stream_header",handle:_4.hitch(this,"_partHandler",_27,this.Parts.DATA)};_27._dataHandle=_4.io.script.get(_2d);}if(_27.onBegin){var _28={};_28["sn.count"]="records";_28["sn.content_type"]="application/javascript";var _2d={url:this.url,content:_28,timeout:60000,callbackParamName:"sn.stream_header",handle:_4.hitch(this,"_partHandler",_27,this.Parts.COUNT)};_27._countHandle=_4.io.script.get(_2d);}return _27;}});}}};});