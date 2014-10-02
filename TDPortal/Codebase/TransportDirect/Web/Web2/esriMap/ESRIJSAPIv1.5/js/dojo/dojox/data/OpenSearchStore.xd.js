/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.OpenSearchStore"],["require","dojo.data.util.simpleFetch"],["require","dojox.xml.DomParser"],["require","dojox.xml.parser"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.OpenSearchStore"]){_4._hasResource["dojox.data.OpenSearchStore"]=true;_4.provide("dojox.data.OpenSearchStore");_4.require("dojo.data.util.simpleFetch");_4.require("dojox.xml.DomParser");_4.require("dojox.xml.parser");_4.experimental("dojox.data.OpenSearchStore");_4.declare("dojox.data.OpenSearchStore",null,{constructor:function(_7){if(_7){this.label=_7.label;this.url=_7.url;this.itemPath=_7.itemPath;}var _8=_4.xhrGet({url:this.url,handleAs:"xml",sync:true});_8.addCallback(this,"_processOsdd");_8.addErrback(function(){throw new Error("Unable to load OpenSearch Description document from ".args.url);});},url:"",itemPath:"",_storeRef:"_S",urlElement:null,iframeElement:null,ATOM_CONTENT_TYPE:3,ATOM_CONTENT_TYPE_STRING:"atom",RSS_CONTENT_TYPE:2,RSS_CONTENT_TYPE_STRING:"rss",XML_CONTENT_TYPE:1,XML_CONTENT_TYPE_STRING:"xml",_assertIsItem:function(_9){if(!this.isItem(_9)){throw new Error("dojox.data.OpenSearchStore: a function was passed an item argument that was not an item");}},_assertIsAttribute:function(_a){if(typeof _a!=="string"){throw new Error("dojox.data.OpenSearchStore: a function was passed an attribute argument that was not an attribute name string");}},getFeatures:function(){return {"dojo.data.api.Read":true};},getValue:function(_b,_c,_d){var _e=this.getValues(_b,_c);if(_e){return _e[0];}return _d;},getAttributes:function(_f){return ["content"];},hasAttribute:function(_10,_11){if(this.getValue(_10,_11)){return true;}return false;},isItemLoaded:function(_12){return this.isItem(_12);},loadItem:function(_13){},getLabel:function(_14){return undefined;},getLabelAttributes:function(_15){return null;},containsValue:function(_16,_17,_18){var _19=this.getValues(_16,_17);for(var i=0;i<_19.length;i++){if(_19[i]===_18){return true;}}return false;},getValues:function(_1b,_1c){this._assertIsItem(_1b);this._assertIsAttribute(_1c);var _1d=this.processItem(_1b,_1c);if(_1d){return [_1d];}return undefined;},isItem:function(_1e){if(_1e&&_1e[this._storeRef]===this){return true;}return false;},close:function(_1f){},process:function(_20){return this["_processOSD"+this.contentType](_20);},processItem:function(_21,_22){return this["_processItem"+this.contentType](_21.node,_22);},_createSearchUrl:function(_23){var _24=this.urlElement.attributes.getNamedItem("template").nodeValue;var _25=this.urlElement.attributes;var _26=_24.indexOf("{searchTerms}");_24=_24.substring(0,_26)+_23.query.searchTerms+_24.substring(_26+13);_4.forEach([{"name":"count","test":_23.count,"def":"10"},{"name":"startIndex","test":_23.start,"def":this.urlElement.attributes.getNamedItem("indexOffset")?this.urlElement.attributes.getNamedItem("indexOffset").nodeValue:0},{"name":"startPage","test":_23.startPage,"def":this.urlElement.attributes.getNamedItem("pageOffset")?this.urlElement.attributes.getNamedItem("pageOffset").nodeValue:0},{"name":"language","test":_23.language,"def":"*"},{"name":"inputEncoding","test":_23.inputEncoding,"def":"UTF-8"},{"name":"outputEncoding","test":_23.outputEncoding,"def":"UTF-8"}],function(_27){_24=_24.replace("{"+_27.name+"}",_27.test||_27.def);_24=_24.replace("{"+_27.name+"?}",_27.test||_27.def);});return _24;},_fetchItems:function(_28,_29,_2a){if(!_28.query){_28.query={};}var _2b=this;var url=this._createSearchUrl(_28);var _2d={url:url,preventCache:true};var xhr=_4.xhrGet(_2d);xhr.addErrback(function(_2f){_2a(_2f,_28);});xhr.addCallback(function(_30){var _31=[];if(_30){_31=_2b.process(_30);for(var i=0;i<_31.length;i++){_31[i]={node:_31[i]};_31[i][_2b._storeRef]=_2b;}}_29(_31,_28);});},_processOSDxml:function(_33){var div=_4.doc.createElement("div");div.innerHTML=_33;return _4.query(this.itemPath,div);},_processItemxml:function(_35,_36){if(_36==="content"){return _35.innerHTML;}return undefined;},_processOSDatom:function(_37){return this._processOSDfeed(_37,"entry");},_processItematom:function(_38,_39){return this._processItemfeed(_38,_39,"content");},_processOSDrss:function(_3a){return this._processOSDfeed(_3a,"item");},_processItemrss:function(_3b,_3c){return this._processItemfeed(_3b,_3c,"description");},_processOSDfeed:function(_3d,_3e){_3d=_6.xml.parser.parse(_3d);var _3f=[];var _40=_3d.getElementsByTagName(_3e);for(var i=0;i<_40.length;i++){_3f.push(_40.item(i));}return _3f;},_processItemfeed:function(_42,_43,_44){if(_43==="content"){var _45=_42.getElementsByTagName(_44).item(0);return this._getNodeXml(_45,true);}return undefined;},_getNodeXml:function(_46,_47){var i;switch(_46.nodeType){case 1:var xml=[];if(!_47){xml.push("<"+_46.tagName);var _4a;for(i=0;i<_46.attributes.length;i++){_4a=_46.attributes.item(i);xml.push(" "+_4a.nodeName+"=\""+_4a.nodeValue+"\"");}xml.push(">");}for(i=0;i<_46.childNodes.length;i++){xml.push(this._getNodeXml(_46.childNodes.item(i)));}if(!_47){xml.push("</"+_46.tagName+">\n");}return xml.join("");case 3:case 4:return _46.nodeValue;}return undefined;},_processOsdd:function(doc){var _4c=doc.getElementsByTagName("Url");var _4d=[];var _4e;var i;for(i=0;i<_4c.length;i++){_4e=_4c[i].attributes.getNamedItem("type").nodeValue;switch(_4e){case "application/rss+xml":_4d[i]=this.RSS_CONTENT_TYPE;break;case "application/atom+xml":_4d[i]=this.ATOM_CONTENT_TYPE;break;default:_4d[i]=this.XML_CONTENT_TYPE;break;}}var _50=0;var _51=_4d[0];for(i=1;i<_4c.length;i++){if(_4d[i]>_51){_50=i;_51=_4d[i];}}var _52=_4c[_50].nodeName.toLowerCase();if(_52=="url"){var _53=_4c[_50].attributes;this.urlElement=_4c[_50];switch(_4d[_50]){case this.ATOM_CONTENT_TYPE:this.contentType=this.ATOM_CONTENT_TYPE_STRING;break;case this.RSS_CONTENT_TYPE:this.contentType=this.RSS_CONTENT_TYPE_STRING;break;case this.XML_CONTENT_TYPE:this.contentType=this.XML_CONTENT_TYPE_STRING;break;}}}});_4.extend(_6.data.OpenSearchStore,_4.data.util.simpleFetch);}}};});