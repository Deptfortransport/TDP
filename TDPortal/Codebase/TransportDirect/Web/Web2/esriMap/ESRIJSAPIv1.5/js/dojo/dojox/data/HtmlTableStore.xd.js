/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.HtmlTableStore"],["require","dojox.xml.parser"],["require","dojo.data.util.simpleFetch"],["require","dojo.data.util.filter"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.HtmlTableStore"]){_4._hasResource["dojox.data.HtmlTableStore"]=true;_4.provide("dojox.data.HtmlTableStore");_4.require("dojox.xml.parser");_4.require("dojo.data.util.simpleFetch");_4.require("dojo.data.util.filter");_4.declare("dojox.data.HtmlTableStore",null,{constructor:function(_7){_4.deprecated("dojox.data.HtmlTableStore","Please use dojox.data.HtmlStore");if(_7.url){if(!_7.tableId){throw new Error("dojo.data.HtmlTableStore: Cannot instantiate using url without an id!");}this.url=_7.url;this.tableId=_7.tableId;}else{if(_7.tableId){this._rootNode=_4.byId(_7.tableId);this.tableId=this._rootNode.id;}else{this._rootNode=_4.byId(this.tableId);}this._getHeadings();for(var i=0;i<this._rootNode.rows.length;i++){this._rootNode.rows[i].store=this;}}},url:"",tableId:"",_getHeadings:function(){this._headings=[];_4.forEach(this._rootNode.tHead.rows[0].cells,_4.hitch(this,function(th){this._headings.push(_6.xml.parser.textContent(th));}));},_getAllItems:function(){var _a=[];for(var i=1;i<this._rootNode.rows.length;i++){_a.push(this._rootNode.rows[i]);}return _a;},_assertIsItem:function(_c){if(!this.isItem(_c)){throw new Error("dojo.data.HtmlTableStore: a function was passed an item argument that was not an item");}},_assertIsAttribute:function(_d){if(typeof _d!=="string"){throw new Error("dojo.data.HtmlTableStore: a function was passed an attribute argument that was not an attribute name string");return -1;}return _4.indexOf(this._headings,_d);},getValue:function(_e,_f,_10){var _11=this.getValues(_e,_f);return (_11.length>0)?_11[0]:_10;},getValues:function(_12,_13){this._assertIsItem(_12);var _14=this._assertIsAttribute(_13);if(_14>-1){return [_6.xml.parser.textContent(_12.cells[_14])];}return [];},getAttributes:function(_15){this._assertIsItem(_15);var _16=[];for(var i=0;i<this._headings.length;i++){if(this.hasAttribute(_15,this._headings[i])){_16.push(this._headings[i]);}}return _16;},hasAttribute:function(_18,_19){return this.getValues(_18,_19).length>0;},containsValue:function(_1a,_1b,_1c){var _1d=undefined;if(typeof _1c==="string"){_1d=_4.data.util.filter.patternToRegExp(_1c,false);}return this._containsValue(_1a,_1b,_1c,_1d);},_containsValue:function(_1e,_1f,_20,_21){var _22=this.getValues(_1e,_1f);for(var i=0;i<_22.length;++i){var _24=_22[i];if(typeof _24==="string"&&_21){return (_24.match(_21)!==null);}else{if(_20===_24){return true;}}}return false;},isItem:function(_25){if(_25&&_25.store&&_25.store===this){return true;}return false;},isItemLoaded:function(_26){return this.isItem(_26);},loadItem:function(_27){this._assertIsItem(_27.item);},_fetchItems:function(_28,_29,_2a){if(this._rootNode){this._finishFetchItems(_28,_29,_2a);}else{if(!this.url){this._rootNode=_4.byId(this.tableId);this._getHeadings();for(var i=0;i<this._rootNode.rows.length;i++){this._rootNode.rows[i].store=this;}}else{var _2c={url:this.url,handleAs:"text"};var _2d=this;var _2e=_4.xhrGet(_2c);_2e.addCallback(function(_2f){var _30=function(_31,id){if(_31.id==id){return _31;}if(_31.childNodes){for(var i=0;i<_31.childNodes.length;i++){var _34=_30(_31.childNodes[i],id);if(_34){return _34;}}}return null;};var d=document.createElement("div");d.innerHTML=_2f;_2d._rootNode=_30(d,_2d.tableId);_2d._getHeadings.call(_2d);for(var i=0;i<_2d._rootNode.rows.length;i++){_2d._rootNode.rows[i].store=_2d;}_2d._finishFetchItems(_28,_29,_2a);});_2e.addErrback(function(_37){_2a(_37,_28);});}}},_finishFetchItems:function(_38,_39,_3a){var _3b=null;var _3c=this._getAllItems();if(_38.query){var _3d=_38.queryOptions?_38.queryOptions.ignoreCase:false;_3b=[];var _3e={};var _3f;var key;for(key in _38.query){_3f=_38.query[key]+"";if(typeof _3f==="string"){_3e[key]=_4.data.util.filter.patternToRegExp(_3f,_3d);}}for(var i=0;i<_3c.length;++i){var _42=true;var _43=_3c[i];for(key in _38.query){_3f=_38.query[key]+"";if(!this._containsValue(_43,key,_3f,_3e[key])){_42=false;}}if(_42){_3b.push(_43);}}_39(_3b,_38);}else{if(_3c.length>0){_3b=_3c.slice(0,_3c.length);}_39(_3b,_38);}},getFeatures:function(){return {"dojo.data.api.Read":true,"dojo.data.api.Identity":true};},close:function(_44){},getLabel:function(_45){if(this.isItem(_45)){return "Table Row #"+this.getIdentity(_45);}return undefined;},getLabelAttributes:function(_46){return null;},getIdentity:function(_47){this._assertIsItem(_47);if(!_4.isOpera){return _47.sectionRowIndex;}else{return (_4.indexOf(this._rootNode.rows,_47)-1);}},getIdentityAttributes:function(_48){return null;},fetchItemByIdentity:function(_49){var _4a=_49.identity;var _4b=this;var _4c=null;var _4d=null;if(!this._rootNode){if(!this.url){this._rootNode=_4.byId(this.tableId);this._getHeadings();for(var i=0;i<this._rootNode.rows.length;i++){this._rootNode.rows[i].store=this;}_4c=this._rootNode.rows[_4a+1];if(_49.onItem){_4d=_49.scope?_49.scope:_4.global;_49.onItem.call(_4d,_4c);}}else{var _4f={url:this.url,handleAs:"text"};var _50=_4.xhrGet(_4f);_50.addCallback(function(_51){var _52=function(_53,id){if(_53.id==id){return _53;}if(_53.childNodes){for(var i=0;i<_53.childNodes.length;i++){var _56=_52(_53.childNodes[i],id);if(_56){return _56;}}}return null;};var d=document.createElement("div");d.innerHTML=_51;_4b._rootNode=_52(d,_4b.tableId);_4b._getHeadings.call(_4b);for(var i=0;i<_4b._rootNode.rows.length;i++){_4b._rootNode.rows[i].store=_4b;}_4c=_4b._rootNode.rows[_4a+1];if(_49.onItem){_4d=_49.scope?_49.scope:_4.global;_49.onItem.call(_4d,_4c);}});_50.addErrback(function(_59){if(_49.onError){_4d=_49.scope?_49.scope:_4.global;_49.onError.call(_4d,_59);}});}}else{if(this._rootNode.rows[_4a+1]){_4c=this._rootNode.rows[_4a+1];if(_49.onItem){_4d=_49.scope?_49.scope:_4.global;_49.onItem.call(_4d,_4c);}}}}});_4.extend(_6.data.HtmlTableStore,_4.data.util.simpleFetch);}}};});