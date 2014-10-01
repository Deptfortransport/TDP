/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.XmlStore"],["provide","dojox.data.XmlItem"],["require","dojo.data.util.simpleFetch"],["require","dojo.data.util.filter"],["require","dojox.xml.parser"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.XmlStore"]){_4._hasResource["dojox.data.XmlStore"]=true;_4.provide("dojox.data.XmlStore");_4.provide("dojox.data.XmlItem");_4.require("dojo.data.util.simpleFetch");_4.require("dojo.data.util.filter");_4.require("dojox.xml.parser");_4.declare("dojox.data.XmlStore",null,{constructor:function(_7){if(_7){this.url=_7.url;this.rootItem=(_7.rootItem||_7.rootitem||this.rootItem);this.keyAttribute=(_7.keyAttribute||_7.keyattribute||this.keyAttribute);this._attributeMap=(_7.attributeMap||_7.attributemap);this.label=_7.label||this.label;this.sendQuery=(_7.sendQuery||_7.sendquery||this.sendQuery);}this._newItems=[];this._deletedItems=[];this._modifiedItems=[];},url:"",rootItem:"",keyAttribute:"",label:"",sendQuery:false,attributeMap:null,getValue:function(_8,_9,_a){var _b=_8.element;var i;var _d;if(_9==="tagName"){return _b.nodeName;}else{if(_9==="childNodes"){for(i=0;i<_b.childNodes.length;i++){_d=_b.childNodes[i];if(_d.nodeType===1){return this._getItem(_d);}}return _a;}else{if(_9==="text()"){for(i=0;i<_b.childNodes.length;i++){_d=_b.childNodes[i];if(_d.nodeType===3||_d.nodeType===4){return _d.nodeValue;}}return _a;}else{_9=this._getAttribute(_b.nodeName,_9);if(_9.charAt(0)==="@"){var _e=_9.substring(1);var _f=_b.getAttribute(_e);return (_f!==undefined)?_f:_a;}else{for(i=0;i<_b.childNodes.length;i++){_d=_b.childNodes[i];if(_d.nodeType===1&&_d.nodeName===_9){return this._getItem(_d);}}return _a;}}}}},getValues:function(_10,_11){var _12=_10.element;var _13=[];var i;var _15;if(_11==="tagName"){return [_12.nodeName];}else{if(_11==="childNodes"){for(i=0;i<_12.childNodes.length;i++){_15=_12.childNodes[i];if(_15.nodeType===1){_13.push(this._getItem(_15));}}return _13;}else{if(_11==="text()"){var ec=_12.childNodes;for(i=0;i<ec.length;i++){_15=ec[i];if(_15.nodeType===3||_15.nodeType===4){_13.push(_15.nodeValue);}}return _13;}else{_11=this._getAttribute(_12.nodeName,_11);if(_11.charAt(0)==="@"){var _17=_11.substring(1);var _18=_12.getAttribute(_17);return (_18!==undefined)?[_18]:[];}else{for(i=0;i<_12.childNodes.length;i++){_15=_12.childNodes[i];if(_15.nodeType===1&&_15.nodeName===_11){_13.push(this._getItem(_15));}}return _13;}}}}},getAttributes:function(_19){var _1a=_19.element;var _1b=[];var i;_1b.push("tagName");if(_1a.childNodes.length>0){var _1d={};var _1e=true;var _1f=false;for(i=0;i<_1a.childNodes.length;i++){var _20=_1a.childNodes[i];if(_20.nodeType===1){var _21=_20.nodeName;if(!_1d[_21]){_1b.push(_21);_1d[_21]=_21;}_1e=true;}else{if(_20.nodeType===3){_1f=true;}}}if(_1e){_1b.push("childNodes");}if(_1f){_1b.push("text()");}}for(i=0;i<_1a.attributes.length;i++){_1b.push("@"+_1a.attributes[i].nodeName);}if(this._attributeMap){for(var key in this._attributeMap){i=key.indexOf(".");if(i>0){var _23=key.substring(0,i);if(_23===_1a.nodeName){_1b.push(key.substring(i+1));}}else{_1b.push(key);}}}return _1b;},hasAttribute:function(_24,_25){return (this.getValue(_24,_25)!==undefined);},containsValue:function(_26,_27,_28){var _29=this.getValues(_26,_27);for(var i=0;i<_29.length;i++){if((typeof _28==="string")){if(_29[i].toString&&_29[i].toString()===_28){return true;}}else{if(_29[i]===_28){return true;}}}return false;},isItem:function(_2b){if(_2b&&_2b.element&&_2b.store&&_2b.store===this){return true;}return false;},isItemLoaded:function(_2c){return this.isItem(_2c);},loadItem:function(_2d){},getFeatures:function(){var _2e={"dojo.data.api.Read":true,"dojo.data.api.Write":true};if(!this.sendQuery||this.keyAttribute!==""){_2e["dojo.data.api.Identity"]=true;}return _2e;},getLabel:function(_2f){if((this.label!=="")&&this.isItem(_2f)){var _30=this.getValue(_2f,this.label);if(_30){return _30.toString();}}return undefined;},getLabelAttributes:function(_31){if(this.label!==""){return [this.label];}return null;},_fetchItems:function(_32,_33,_34){var url=this._getFetchUrl(_32);console.log("XmlStore._fetchItems(): url="+url);if(!url){_34(new Error("No URL specified."));return;}var _36=(!this.sendQuery?_32:{});var _37=this;var _38={url:url,handleAs:"xml",preventCache:true};var _39=_4.xhrGet(_38);_39.addCallback(function(_3a){var _3b=_37._getItems(_3a,_36);console.log("XmlStore._fetchItems(): length="+(_3b?_3b.length:0));if(_3b&&_3b.length>0){_33(_3b,_32);}else{_33([],_32);}});_39.addErrback(function(_3c){_34(_3c,_32);});},_getFetchUrl:function(_3d){if(!this.sendQuery){return this.url;}var _3e=_3d.query;if(!_3e){return this.url;}if(_4.isString(_3e)){return this.url+_3e;}var _3f="";for(var _40 in _3e){var _41=_3e[_40];if(_41){if(_3f){_3f+="&";}_3f+=(_40+"="+_41);}}if(!_3f){return this.url;}var _42=this.url;if(_42.indexOf("?")<0){_42+="?";}else{_42+="&";}return _42+_3f;},_getItems:function(_43,_44){var _45=null;if(_44){_45=_44.query;}var _46=[];var _47=null;if(this.rootItem!==""){_47=_4.query(this.rootItem,_43);}else{_47=_43.documentElement.childNodes;}var _48=_44.queryOptions?_44.queryOptions.deep:false;if(_48){_47=this._flattenNodes(_47);}for(var i=0;i<_47.length;i++){var _4a=_47[i];if(_4a.nodeType!=1){continue;}var _4b=this._getItem(_4a);if(_45){var _4c=true;var _4d=_44.queryOptions?_44.queryOptions.ignoreCase:false;var _4e;var _4f={};for(var key in _45){_4e=_45[key];if(typeof _4e==="string"){_4f[key]=_4.data.util.filter.patternToRegExp(_4e,_4d);}}for(var _51 in _45){_4e=this.getValue(_4b,_51);if(_4e){var _52=_45[_51];if((typeof _4e)==="string"&&(_4f[_51])){if((_4e.match(_4f[_51]))!==null){continue;}}else{if((typeof _4e)==="object"){if(_4e.toString&&(_4f[_51])){var _53=_4e.toString();if((_53.match(_4f[_51]))!==null){continue;}}else{if(_52==="*"||_52===_4e){continue;}}}}}_4c=false;break;}if(!_4c){continue;}}_46.push(_4b);}_4.forEach(_46,function(_54){_54.element.parentNode.removeChild(_54.element);},this);return _46;},_flattenNodes:function(_55){var _56=[];if(_55){var i;for(i=0;i<_55.length;i++){var _58=_55[i];_56.push(_58);if(_58.childNodes&&_58.childNodes.length>0){_56=_56.concat(this._flattenNodes(_58.childNodes));}}}return _56;},close:function(_59){},newItem:function(_5a,_5b){console.log("XmlStore.newItem()");_5a=(_5a||{});var _5c=_5a.tagName;if(!_5c){_5c=this.rootItem;if(_5c===""){return null;}}var _5d=this._getDocument();var _5e=_5d.createElement(_5c);for(var _5f in _5a){var _60;if(_5f==="tagName"){continue;}else{if(_5f==="text()"){_60=_5d.createTextNode(_5a[_5f]);_5e.appendChild(_60);}else{_5f=this._getAttribute(_5c,_5f);if(_5f.charAt(0)==="@"){var _61=_5f.substring(1);_5e.setAttribute(_61,_5a[_5f]);}else{var _62=_5d.createElement(_5f);_60=_5d.createTextNode(_5a[_5f]);_62.appendChild(_60);_5e.appendChild(_62);}}}}var _63=this._getItem(_5e);this._newItems.push(_63);var _64=null;if(_5b&&_5b.parent&&_5b.attribute){_64={item:_5b.parent,attribute:_5b.attribute,oldValue:undefined};var _65=this.getValues(_5b.parent,_5b.attribute);if(_65&&_65.length>0){var _66=_65.slice(0,_65.length);if(_65.length===1){_64.oldValue=_65[0];}else{_64.oldValue=_65.slice(0,_65.length);}_66.push(_63);this.setValues(_5b.parent,_5b.attribute,_66);_64.newValue=this.getValues(_5b.parent,_5b.attribute);}else{this.setValues(_5b.parent,_5b.attribute,_63);_64.newValue=_63;}}return _63;},deleteItem:function(_67){console.log("XmlStore.deleteItem()");var _68=_67.element;if(_68.parentNode){this._backupItem(_67);_68.parentNode.removeChild(_68);return true;}this._forgetItem(_67);this._deletedItems.push(_67);return true;},setValue:function(_69,_6a,_6b){if(_6a==="tagName"){return false;}this._backupItem(_69);var _6c=_69.element;var _6d;var _6e;if(_6a==="childNodes"){_6d=_6b.element;_6c.appendChild(_6d);}else{if(_6a==="text()"){while(_6c.firstChild){_6c.removeChild(_6c.firstChild);}_6e=this._getDocument(_6c).createTextNode(_6b);_6c.appendChild(_6e);}else{_6a=this._getAttribute(_6c.nodeName,_6a);if(_6a.charAt(0)==="@"){var _6f=_6a.substring(1);_6c.setAttribute(_6f,_6b);}else{for(var i=0;i<_6c.childNodes.length;i++){var _71=_6c.childNodes[i];if(_71.nodeType===1&&_71.nodeName===_6a){_6d=_71;break;}}var _72=this._getDocument(_6c);if(_6d){while(_6d.firstChild){_6d.removeChild(_6d.firstChild);}}else{_6d=_72.createElement(_6a);_6c.appendChild(_6d);}_6e=_72.createTextNode(_6b);_6d.appendChild(_6e);}}}return true;},setValues:function(_73,_74,_75){if(_74==="tagName"){return false;}this._backupItem(_73);var _76=_73.element;var i;var _78;var _79;if(_74==="childNodes"){while(_76.firstChild){_76.removeChild(_76.firstChild);}for(i=0;i<_75.length;i++){_78=_75[i].element;_76.appendChild(_78);}}else{if(_74==="text()"){while(_76.firstChild){_76.removeChild(_76.firstChild);}var _7a="";for(i=0;i<_75.length;i++){_7a+=_75[i];}_79=this._getDocument(_76).createTextNode(_7a);_76.appendChild(_79);}else{_74=this._getAttribute(_76.nodeName,_74);if(_74.charAt(0)==="@"){var _7b=_74.substring(1);_76.setAttribute(_7b,_75[0]);}else{for(i=_76.childNodes.length-1;i>=0;i--){var _7c=_76.childNodes[i];if(_7c.nodeType===1&&_7c.nodeName===_74){_76.removeChild(_7c);}}var _7d=this._getDocument(_76);for(i=0;i<_75.length;i++){_78=_7d.createElement(_74);_79=_7d.createTextNode(_75[i]);_78.appendChild(_79);_76.appendChild(_78);}}}}return true;},unsetAttribute:function(_7e,_7f){if(_7f==="tagName"){return false;}this._backupItem(_7e);var _80=_7e.element;if(_7f==="childNodes"||_7f==="text()"){while(_80.firstChild){_80.removeChild(_80.firstChild);}}else{_7f=this._getAttribute(_80.nodeName,_7f);if(_7f.charAt(0)==="@"){var _81=_7f.substring(1);_80.removeAttribute(_81);}else{for(var i=_80.childNodes.length-1;i>=0;i--){var _83=_80.childNodes[i];if(_83.nodeType===1&&_83.nodeName===_7f){_80.removeChild(_83);}}}}return true;},save:function(_84){if(!_84){_84={};}var i;for(i=0;i<this._modifiedItems.length;i++){this._saveItem(this._modifiedItems[i],_84,"PUT");}for(i=0;i<this._newItems.length;i++){var _86=this._newItems[i];if(_86.element.parentNode){this._newItems.splice(i,1);i--;continue;}this._saveItem(this._newItems[i],_84,"POST");}for(i=0;i<this._deletedItems.length;i++){this._saveItem(this._deletedItems[i],_84,"DELETE");}},revert:function(){console.log("XmlStore.revert() _newItems="+this._newItems.length);console.log("XmlStore.revert() _deletedItems="+this._deletedItems.length);console.log("XmlStore.revert() _modifiedItems="+this._modifiedItems.length);this._newItems=[];this._restoreItems(this._deletedItems);this._deletedItems=[];this._restoreItems(this._modifiedItems);this._modifiedItems=[];return true;},isDirty:function(_87){if(_87){var _88=this._getRootElement(_87.element);return (this._getItemIndex(this._newItems,_88)>=0||this._getItemIndex(this._deletedItems,_88)>=0||this._getItemIndex(this._modifiedItems,_88)>=0);}else{return (this._newItems.length>0||this._deletedItems.length>0||this._modifiedItems.length>0);}},_saveItem:function(_89,_8a,_8b){var url;var _8d;if(_8b==="PUT"){url=this._getPutUrl(_89);}else{if(_8b==="DELETE"){url=this._getDeleteUrl(_89);}else{url=this._getPostUrl(_89);}}if(!url){if(_8a.onError){_8d=_8a.scope||_4.global;_8a.onError.call(_8d,new Error("No URL for saving content: "+this._getPostContent(_89)));}return;}var _8e={url:url,method:(_8b||"POST"),contentType:"text/xml",handleAs:"xml"};var _8f;if(_8b==="PUT"){_8e.putData=this._getPutContent(_89);_8f=_4.rawXhrPut(_8e);}else{if(_8b==="DELETE"){_8f=_4.xhrDelete(_8e);}else{_8e.postData=this._getPostContent(_89);_8f=_4.rawXhrPost(_8e);}}_8d=(_8a.scope||_4.global);var _90=this;_8f.addCallback(function(_91){_90._forgetItem(_89);if(_8a.onComplete){_8a.onComplete.call(_8d);}});_8f.addErrback(function(_92){if(_8a.onError){_8a.onError.call(_8d,_92);}});},_getPostUrl:function(_93){return this.url;},_getPutUrl:function(_94){return this.url;},_getDeleteUrl:function(_95){var url=this.url;if(_95&&this.keyAttribute!==""){var _97=this.getValue(_95,this.keyAttribute);if(_97){var key=this.keyAttribute.charAt(0)==="@"?this.keyAttribute.substring(1):this.keyAttribute;url+=url.indexOf("?")<0?"?":"&";url+=key+"="+_97;}}return url;},_getPostContent:function(_99){var _9a=_99.element;var _9b="<?xml version=\"1.0\"?>";return _9b+_6.xml.parser.innerXML(_9a);},_getPutContent:function(_9c){var _9d=_9c.element;var _9e="<?xml version=\"1.0\"?>";return _9e+_6.xml.parser.innerXML(_9d);},_getAttribute:function(_9f,_a0){if(this._attributeMap){var key=_9f+"."+_a0;var _a2=this._attributeMap[key];if(_a2){_a0=_a2;}else{_a2=this._attributeMap[_a0];if(_a2){_a0=_a2;}}}return _a0;},_getItem:function(_a3){try{var q=null;if(this.keyAttribute===""){q=this._getXPath(_a3);}return new _6.data.XmlItem(_a3,this,q);}catch(e){console.log(e);}return null;},_getItemIndex:function(_a5,_a6){for(var i=0;i<_a5.length;i++){if(_a5[i].element===_a6){return i;}}return -1;},_backupItem:function(_a8){var _a9=this._getRootElement(_a8.element);if(this._getItemIndex(this._newItems,_a9)>=0||this._getItemIndex(this._modifiedItems,_a9)>=0){return;}if(_a9!=_a8.element){_a8=this._getItem(_a9);}_a8._backup=_a9.cloneNode(true);this._modifiedItems.push(_a8);},_restoreItems:function(_aa){_4.forEach(_aa,function(_ab){if(_ab._backup){_ab.element=_ab._backup;_ab._backup=null;}},this);},_forgetItem:function(_ac){var _ad=_ac.element;var _ae=this._getItemIndex(this._newItems,_ad);if(_ae>=0){this._newItems.splice(_ae,1);}_ae=this._getItemIndex(this._deletedItems,_ad);if(_ae>=0){this._deletedItems.splice(_ae,1);}_ae=this._getItemIndex(this._modifiedItems,_ad);if(_ae>=0){this._modifiedItems.splice(_ae,1);}},_getDocument:function(_af){if(_af){return _af.ownerDocument;}else{if(!this._document){return _6.xml.parser.parse();}}return null;},_getRootElement:function(_b0){while(_b0.parentNode){_b0=_b0.parentNode;}return _b0;},_getXPath:function(_b1){var _b2=null;if(!this.sendQuery){var _b3=_b1;_b2="";while(_b3&&_b3!=_b1.ownerDocument){var pos=0;var _b5=_b3;var _b6=_b3.nodeName;while(_b5){_b5=_b5.previousSibling;if(_b5&&_b5.nodeName===_b6){pos++;}}var _b7="/"+_b6+"["+pos+"]";if(_b2){_b2=_b7+_b2;}else{_b2=_b7;}_b3=_b3.parentNode;}}return _b2;},getIdentity:function(_b8){if(!this.isItem(_b8)){throw new Error("dojox.data.XmlStore: Object supplied to getIdentity is not an item");}else{var id=null;if(this.sendQuery&&this.keyAttribute!==""){id=this.getValue(_b8,this.keyAttribute).toString();}else{if(!this.serverQuery){if(this.keyAttribute!==""){id=this.getValue(_b8,this.keyAttribute).toString();}else{id=_b8.q;}}}return id;}},getIdentityAttributes:function(_ba){if(!this.isItem(_ba)){throw new Error("dojox.data.XmlStore: Object supplied to getIdentity is not an item");}else{if(this.keyAttribute!==""){return [this.keyAttribute];}else{return null;}}},fetchItemByIdentity:function(_bb){var _bc=null;var _bd=null;var _be=this;var url=null;var _c0=null;var _c1=null;if(!_be.sendQuery){_bc=function(_c2){if(_c2){if(_be.keyAttribute!==""){var _c3={};_c3.query={};_c3.query[_be.keyAttribute]=_bb.identity;var _c4=_be._getItems(_c2,_c3);_bd=_bb.scope||_4.global;if(_c4.length===1){if(_bb.onItem){_bb.onItem.call(_bd,_c4[0]);}}else{if(_c4.length===0){if(_bb.onItem){_bb.onItem.call(_bd,null);}}else{if(_bb.onError){_bb.onError.call(_bd,new Error("Items array size for identity lookup greater than 1, invalid keyAttribute."));}}}}else{var _c5=_bb.identity.split("/");var i;var _c7=_c2;for(i=0;i<_c5.length;i++){if(_c5[i]&&_c5[i]!==""){var _c8=_c5[i];_c8=_c8.substring(0,_c8.length-1);var _c9=_c8.split("[");var tag=_c9[0];var _cb=parseInt(_c9[1],10);var pos=0;if(_c7){var _cd=_c7.childNodes;if(_cd){var j;var _cf=null;for(j=0;j<_cd.length;j++){var _d0=_cd[j];if(_d0.nodeName===tag){if(pos<_cb){pos++;}else{_cf=_d0;break;}}}if(_cf){_c7=_cf;}else{_c7=null;}}else{_c7=null;}}else{break;}}}var _d1=null;if(_c7){_d1=_be._getItem(_c7);_d1.element.parentNode.removeChild(_d1.element);}if(_bb.onItem){_bd=_bb.scope||_4.global;_bb.onItem.call(_bd,_d1);}}}};url=this._getFetchUrl(null);_c0={url:url,handleAs:"xml",preventCache:true};_c1=_4.xhrGet(_c0);_c1.addCallback(_bc);if(_bb.onError){_c1.addErrback(function(_d2){var s=_bb.scope||_4.global;_bb.onError.call(s,_d2);});}}else{if(_be.keyAttribute!==""){var _d4={query:{}};_d4.query[_be.keyAttribute]=_bb.identity;url=this._getFetchUrl(_d4);_bc=function(_d5){var _d6=null;if(_d5){var _d7=_be._getItems(_d7,{});if(_d7.length===1){_d6=_d7[0];}else{if(_bb.onError){var _d8=_bb.scope||_4.global;_bb.onError.call(_d8,new Error("More than one item was returned from the server for the denoted identity"));}}}if(_bb.onItem){_d8=_bb.scope||_4.global;_bb.onItem.call(_d8,_d6);}};_c0={url:url,handleAs:"xml",preventCache:true};_c1=_4.xhrGet(_c0);_c1.addCallback(_bc);if(_bb.onError){_c1.addErrback(function(_d9){var s=_bb.scope||_4.global;_bb.onError.call(s,_d9);});}}else{if(_bb.onError){var s=_bb.scope||_4.global;_bb.onError.call(s,new Error("XmlStore is not told that the server to provides identity support.  No keyAttribute specified."));}}}}});_4.declare("dojox.data.XmlItem",null,{constructor:function(_dc,_dd,_de){this.element=_dc;this.store=_dd;this.q=_de;},toString:function(){var str="";if(this.element){for(var i=0;i<this.element.childNodes.length;i++){var _e1=this.element.childNodes[i];if(_e1.nodeType===3||_e1.nodeType===4){str+=_e1.nodeValue;}}}return str;}});_4.extend(_6.data.XmlStore,_4.data.util.simpleFetch);}}};});