/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.atom.io.model"],["require","dojox.xml.parser"],["require","dojo.string"],["require","dojo.date.stamp"],["requireLocalization","dojox.atom.io","messages",null,"",""]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.atom.io.model"]){_4._hasResource["dojox.atom.io.model"]=true;_4.provide("dojox.atom.io.model");_4.require("dojox.xml.parser");_4.require("dojo.string");_4.require("dojo.date.stamp");_6.atom.io.model._Constants={"ATOM_URI":"http://www.w3.org/2005/Atom","ATOM_NS":"http://www.w3.org/2005/Atom","PURL_NS":"http://purl.org/atom/app#","APP_NS":"http://www.w3.org/2007/app"};_6.atom.io.model._actions={"link":function(_7,_8){if(_7.links===null){_7.links=[];}var _9=new _6.atom.io.model.Link();_9.buildFromDom(_8);_7.links.push(_9);},"author":function(_a,_b){if(_a.authors===null){_a.authors=[];}var _c=new _6.atom.io.model.Person("author");_c.buildFromDom(_b);_a.authors.push(_c);},"contributor":function(_d,_e){if(_d.contributors===null){_d.contributors=[];}var _f=new _6.atom.io.model.Person("contributor");_f.buildFromDom(_e);_d.contributors.push(_f);},"category":function(obj,_11){if(obj.categories===null){obj.categories=[];}var cat=new _6.atom.io.model.Category();cat.buildFromDom(_11);obj.categories.push(cat);},"icon":function(obj,_14){obj.icon=_6.xml.parser.textContent(_14);},"id":function(obj,_16){obj.id=_6.xml.parser.textContent(_16);},"rights":function(obj,_18){obj.rights=_6.xml.parser.textContent(_18);},"subtitle":function(obj,_1a){var cnt=new _6.atom.io.model.Content("subtitle");cnt.buildFromDom(_1a);obj.subtitle=cnt;},"title":function(obj,_1d){var cnt=new _6.atom.io.model.Content("title");cnt.buildFromDom(_1d);obj.title=cnt;},"updated":function(obj,_20){obj.updated=_6.atom.io.model.util.createDate(_20);},"issued":function(obj,_22){obj.issued=_6.atom.io.model.util.createDate(_22);},"modified":function(obj,_24){obj.modified=_6.atom.io.model.util.createDate(_24);},"published":function(obj,_26){obj.published=_6.atom.io.model.util.createDate(_26);},"entry":function(obj,_28){if(obj.entries===null){obj.entries=[];}var _29=obj.createEntry?obj.createEntry():new _6.atom.io.model.Entry();_29.buildFromDom(_28);obj.entries.push(_29);},"content":function(obj,_2b){var cnt=new _6.atom.io.model.Content("content");cnt.buildFromDom(_2b);obj.content=cnt;},"summary":function(obj,_2e){var _2f=new _6.atom.io.model.Content("summary");_2f.buildFromDom(_2e);obj.summary=_2f;},"name":function(obj,_31){obj.name=_6.xml.parser.textContent(_31);},"email":function(obj,_33){obj.email=_6.xml.parser.textContent(_33);},"uri":function(obj,_35){obj.uri=_6.xml.parser.textContent(_35);},"generator":function(obj,_37){obj.generator=new _6.atom.io.model.Generator();obj.generator.buildFromDom(_37);}};_6.atom.io.model.util={createDate:function(_38){var _39=_6.xml.parser.textContent(_38);if(_39){return _4.date.stamp.fromISOString(_4.trim(_39));}return null;},escapeHtml:function(str){str=str.replace(/&/gm,"&amp;").replace(/</gm,"&lt;").replace(/>/gm,"&gt;").replace(/"/gm,"&quot;");str=str.replace(/'/gm,"&#39;");return str;},unEscapeHtml:function(str){str=str.replace(/&amp;/gm,"&").replace(/&lt;/gm,"<").replace(/&gt;/gm,">").replace(/&quot;/gm,"\"");str=str.replace(/&#39;/gm,"'");return str;},getNodename:function(_3c){var _3d=null;if(_3c!==null){_3d=_3c.localName?_3c.localName:_3c.nodeName;if(_3d!==null){var _3e=_3d.indexOf(":");if(_3e!==-1){_3d=_3d.substring((_3e+1),_3d.length);}}}return _3d;}};_4.declare("dojox.atom.io.model.Node",null,{constructor:function(_3f,_40,_41,_42,_43){this.name_space=_3f;this.name=_40;this.attributes=[];if(_41){this.attributes=_41;}this.content=[];this.rawNodes=[];this.textContent=null;if(_42){this.content.push(_42);}this.shortNs=_43;this._objName="Node";},buildFromDom:function(_44){this._saveAttributes(_44);this.name_space=_44.namespaceURI;this.shortNs=_44.prefix;this.name=_6.atom.io.model.util.getNodename(_44);for(var x=0;x<_44.childNodes.length;x++){var c=_44.childNodes[x];if(_6.atom.io.model.util.getNodename(c)!="#text"){this.rawNodes.push(c);var n=new _6.atom.io.model.Node();n.buildFromDom(c,true);this.content.push(n);}else{this.content.push(c.nodeValue);}}this.textContent=_6.xml.parser.textContent(_44);},_saveAttributes:function(_48){if(!this.attributes){this.attributes=[];}var _49=function(_4a){var _4b=_4a.attributes;if(_4b===null){return false;}return (_4b.length!==0);};if(_49(_48)&&this._getAttributeNames){var _4c=this._getAttributeNames(_48);if(_4c&&_4c.length>0){for(var x in _4c){var _4e=_48.getAttribute(_4c[x]);if(_4e){this.attributes[_4c[x]]=_4e;}}}}},addAttribute:function(_4f,_50){this.attributes[_4f]=_50;},getAttribute:function(_51){return this.attributes[_51];},_getAttributeNames:function(_52){var _53=[];for(var i=0;i<_52.attributes.length;i++){_53.push(_52.attributes[i].nodeName);}return _53;},toString:function(){var xml=[];var x;var _57=(this.shortNs?this.shortNs+":":"")+this.name;var _58=(this.name=="#cdata-section");if(_58){xml.push("<![CDATA[");xml.push(this.textContent);xml.push("]]>");}else{xml.push("<");xml.push(_57);if(this.name_space){xml.push(" xmlns='"+this.name_space+"'");}if(this.attributes){for(x in this.attributes){xml.push(" "+x+"='"+this.attributes[x]+"'");}}if(this.content){xml.push(">");for(x in this.content){xml.push(this.content[x]);}xml.push("</"+_57+">\n");}else{xml.push("/>\n");}}return xml.join("");},addContent:function(_59){this.content.push(_59);}});_4.declare("dojox.atom.io.model.AtomItem",_6.atom.io.model.Node,{constructor:function(_5a){this.ATOM_URI=_6.atom.io.model._Constants.ATOM_URI;this.links=null;this.authors=null;this.categories=null;this.contributors=null;this.icon=this.id=this.logo=this.xmlBase=this.rights=null;this.subtitle=this.title=null;this.updated=this.published=null;this.issued=this.modified=null;this.content=null;this.extensions=null;this.entries=null;this.name_spaces={};this._objName="AtomItem";},_getAttributeNames:function(){return null;},_accepts:{},accept:function(tag){return Boolean(this._accepts[tag]);},_postBuild:function(){},buildFromDom:function(_5c){var i,c,n;for(i=0;i<_5c.attributes.length;i++){c=_5c.attributes.item(i);n=_6.atom.io.model.util.getNodename(c);if(c.prefix=="xmlns"&&c.prefix!=n){this.addNamespace(c.nodeValue,n);}}c=_5c.childNodes;for(i=0;i<c.length;i++){if(c[i].nodeType==1){var _60=_6.atom.io.model.util.getNodename(c[i]);if(!_60){continue;}if(c[i].namespaceURI!=_6.atom.io.model._Constants.ATOM_NS&&_60!="#text"){if(!this.extensions){this.extensions=[];}var _61=new _6.atom.io.model.Node();_61.buildFromDom(c[i]);this.extensions.push(_61);}if(!this.accept(_60.toLowerCase())){continue;}var fn=_6.atom.io.model._actions[_60];if(fn){fn(this,c[i]);}}}this._saveAttributes(_5c);if(this._postBuild){this._postBuild();}},addNamespace:function(_63,_64){if(_63&&_64){this.name_spaces[_64]=_63;}},addAuthor:function(_65,_66,uri){if(!this.authors){this.authors=[];}this.authors.push(new _6.atom.io.model.Person("author",_65,_66,uri));},addContributor:function(_68,_69,uri){if(!this.contributors){this.contributors=[];}this.contributors.push(new _6.atom.io.model.Person("contributor",_68,_69,uri));},addLink:function(_6b,rel,_6d,_6e,_6f){if(!this.links){this.links=[];}this.links.push(new _6.atom.io.model.Link(_6b,rel,_6d,_6e,_6f));},removeLink:function(_70,rel){if(!this.links||!_4.isArray(this.links)){return;}var _72=0;for(var i=0;i<this.links.length;i++){if((!_70||this.links[i].href===_70)&&(!rel||this.links[i].rel===rel)){this.links.splice(i,1);_72++;}}return _72;},removeBasicLinks:function(){if(!this.links){return;}var _74=0;for(var i=0;i<this.links.length;i++){if(!this.links[i].rel){this.links.splice(i,1);_74++;i--;}}return _74;},addCategory:function(_76,_77,_78){if(!this.categories){this.categories=[];}this.categories.push(new _6.atom.io.model.Category(_76,_77,_78));},getCategories:function(_79){if(!_79){return this.categories;}var arr=[];for(var x in this.categories){if(this.categories[x].scheme===_79){arr.push(this.categories[x]);}}return arr;},removeCategories:function(_7c,_7d){if(!this.categories){return;}var _7e=0;for(var i=0;i<this.categories.length;i++){if((!_7c||this.categories[i].scheme===_7c)&&(!_7d||this.categories[i].term===_7d)){this.categories.splice(i,1);_7e++;i--;}}return _7e;},setTitle:function(str,_81){if(!str){return;}this.title=new _6.atom.io.model.Content("title");this.title.value=str;if(_81){this.title.type=_81;}},addExtension:function(_82,_83,_84,_85,_86){if(!this.extensions){this.extensions=[];}this.extensions.push(new _6.atom.io.model.Node(_82,_83,_84,_85,_86||"ns"+this.extensions.length));},getExtensions:function(_87,_88){var arr=[];if(!this.extensions){return arr;}for(var x in this.extensions){if((this.extensions[x].name_space===_87||this.extensions[x].shortNs===_87)&&(!_88||this.extensions[x].name===_88)){arr.push(this.extensions[x]);}}return arr;},removeExtensions:function(_8b,_8c){if(!this.extensions){return;}for(var i=0;i<this.extensions.length;i++){if((this.extensions[i].name_space==_8b||this.extensions[i].shortNs===_8b)&&this.extensions[i].name===_8c){this.extensions.splice(i,1);i--;}}},destroy:function(){this.links=null;this.authors=null;this.categories=null;this.contributors=null;this.icon=this.id=this.logo=this.xmlBase=this.rights=null;this.subtitle=this.title=null;this.updated=this.published=null;this.issued=this.modified=null;this.content=null;this.extensions=null;this.entries=null;}});_4.declare("dojox.atom.io.model.Category",_6.atom.io.model.Node,{constructor:function(_8e,_8f,_90){this.scheme=_8e;this.term=_8f;this.label=_90;this._objName="Category";},_postBuild:function(){},_getAttributeNames:function(){return ["label","scheme","term"];},toString:function(){var s=[];s.push("<category ");if(this.label){s.push(" label=\""+this.label+"\" ");}if(this.scheme){s.push(" scheme=\""+this.scheme+"\" ");}if(this.term){s.push(" term=\""+this.term+"\" ");}s.push("/>\n");return s.join("");},buildFromDom:function(_92){this._saveAttributes(_92);this.label=this.attributes.label;this.scheme=this.attributes.scheme;this.term=this.attributes.term;if(this._postBuild){this._postBuild();}}});_4.declare("dojox.atom.io.model.Content",_6.atom.io.model.Node,{constructor:function(_93,_94,src,_96,_97){this.tagName=_93;this.value=_94;this.src=src;this.type=_96;this.xmlLang=_97;this.HTML="html";this.TEXT="text";this.XHTML="xhtml";this.XML="xml";this._useTextContent="true";},_getAttributeNames:function(){return ["type","src"];},_postBuild:function(){},buildFromDom:function(_98){if(_98.innerHTML){this.value=_98.innerHTML;}else{this.value=_6.xml.parser.textContent(_98);}this._saveAttributes(_98);if(this.attributes){this.type=this.attributes.type;this.scheme=this.attributes.scheme;this.term=this.attributes.term;}if(!this.type){this.type="text";}var _99=this.type.toLowerCase();if(_99==="html"||_99==="text/html"||_99==="xhtml"||_99==="text/xhtml"){this.value=_6.atom.io.model.util.unEscapeHtml(this.value);}if(this._postBuild){this._postBuild();}},toString:function(){var s=[];s.push("<"+this.tagName+" ");if(!this.type){this.type="text";}if(this.type){s.push(" type=\""+this.type+"\" ");}if(this.xmlLang){s.push(" xml:lang=\""+this.xmlLang+"\" ");}if(this.xmlBase){s.push(" xml:base=\""+this.xmlBase+"\" ");}if(this.type.toLowerCase()==this.HTML){s.push(">"+_6.atom.io.model.util.escapeHtml(this.value)+"</"+this.tagName+">\n");}else{s.push(">"+this.value+"</"+this.tagName+">\n");}var ret=s.join("");return ret;}});_4.declare("dojox.atom.io.model.Link",_6.atom.io.model.Node,{constructor:function(_9c,rel,_9e,_9f,_a0){this.href=_9c;this.hrefLang=_9e;this.rel=rel;this.title=_9f;this.type=_a0;},_getAttributeNames:function(){return ["href","jrefLang","rel","title","type"];},_postBuild:function(){},buildFromDom:function(_a1){this._saveAttributes(_a1);this.href=this.attributes.href;this.hrefLang=this.attributes.hreflang;this.rel=this.attributes.rel;this.title=this.attributes.title;this.type=this.attributes.type;if(this._postBuild){this._postBuild();}},toString:function(){var s=[];s.push("<link ");if(this.href){s.push(" href=\""+this.href+"\" ");}if(this.hrefLang){s.push(" hrefLang=\""+this.hrefLang+"\" ");}if(this.rel){s.push(" rel=\""+this.rel+"\" ");}if(this.title){s.push(" title=\""+this.title+"\" ");}if(this.type){s.push(" type = \""+this.type+"\" ");}s.push("/>\n");return s.join("");}});_4.declare("dojox.atom.io.model.Person",_6.atom.io.model.Node,{constructor:function(_a3,_a4,_a5,uri){this.author="author";this.contributor="contributor";if(!_a3){_a3=this.author;}this.personType=_a3;this.name=_a4||"";this.email=_a5||"";this.uri=uri||"";this._objName="Person";},_getAttributeNames:function(){return null;},_postBuild:function(){},accept:function(tag){return Boolean(this._accepts[tag]);},buildFromDom:function(_a8){var c=_a8.childNodes;for(var i=0;i<c.length;i++){var _ab=_6.atom.io.model.util.getNodename(c[i]);if(!_ab){continue;}if(c[i].namespaceURI!=_6.atom.io.model._Constants.ATOM_NS&&_ab!="#text"){if(!this.extensions){this.extensions=[];}var _ac=new _6.atom.io.model.Node();_ac.buildFromDom(c[i]);this.extensions.push(_ac);}if(!this.accept(_ab.toLowerCase())){continue;}var fn=_6.atom.io.model._actions[_ab];if(fn){fn(this,c[i]);}}this._saveAttributes(_a8);if(this._postBuild){this._postBuild();}},_accepts:{"name":true,"uri":true,"email":true},toString:function(){var s=[];s.push("<"+this.personType+">\n");if(this.name){s.push("\t<name>"+this.name+"</name>\n");}if(this.email){s.push("\t<email>"+this.email+"</email>\n");}if(this.uri){s.push("\t<uri>"+this.uri+"</uri>\n");}s.push("</"+this.personType+">\n");return s.join("");}});_4.declare("dojox.atom.io.model.Generator",_6.atom.io.model.Node,{constructor:function(uri,_b0,_b1){this.uri=uri;this.version=_b0;this.value=_b1;},_postBuild:function(){},buildFromDom:function(_b2){this.value=_6.xml.parser.textContent(_b2);this._saveAttributes(_b2);this.uri=this.attributes.uri;this.version=this.attributes.version;if(this._postBuild){this._postBuild();}},toString:function(){var s=[];s.push("<generator ");if(this.uri){s.push(" uri=\""+this.uri+"\" ");}if(this.version){s.push(" version=\""+this.version+"\" ");}s.push(">"+this.value+"</generator>\n");var ret=s.join("");return ret;}});_4.declare("dojox.atom.io.model.Entry",_6.atom.io.model.AtomItem,{constructor:function(id){this.id=id;this._objName="Entry";this.feedUrl=null;},_getAttributeNames:function(){return null;},_accepts:{"author":true,"content":true,"category":true,"contributor":true,"created":true,"id":true,"link":true,"published":true,"rights":true,"summary":true,"title":true,"updated":true,"xmlbase":true,"issued":true,"modified":true},toString:function(_b6){var s=[];var i;if(_b6){s.push("<?xml version='1.0' encoding='UTF-8'?>");s.push("<entry xmlns='"+_6.atom.io.model._Constants.ATOM_URI+"'");}else{s.push("<entry");}if(this.xmlBase){s.push(" xml:base=\""+this.xmlBase+"\" ");}for(i in this.name_spaces){s.push(" xmlns:"+i+"=\""+this.name_spaces[i]+"\"");}s.push(">\n");s.push("<id>"+(this.id?this.id:"")+"</id>\n");if(this.issued&&!this.published){this.published=this.issued;}if(this.published){s.push("<published>"+_4.date.stamp.toISOString(this.published)+"</published>\n");}if(this.created){s.push("<created>"+_4.date.stamp.toISOString(this.created)+"</created>\n");}if(this.issued){s.push("<issued>"+_4.date.stamp.toISOString(this.issued)+"</issued>\n");}if(this.modified){s.push("<modified>"+_4.date.stamp.toISOString(this.modified)+"</modified>\n");}if(this.modified&&!this.updated){this.updated=this.modified;}if(this.updated){s.push("<updated>"+_4.date.stamp.toISOString(this.updated)+"</updated>\n");}if(this.rights){s.push("<rights>"+this.rights+"</rights>\n");}if(this.title){s.push(this.title.toString());}if(this.summary){s.push(this.summary.toString());}var _b9=[this.authors,this.categories,this.links,this.contributors,this.extensions];for(var x in _b9){if(_b9[x]){for(var y in _b9[x]){s.push(_b9[x][y]);}}}if(this.content){s.push(this.content.toString());}s.push("</entry>\n");return s.join("");},getEditHref:function(){if(this.links===null||this.links.length===0){return null;}for(var x in this.links){if(this.links[x].rel&&this.links[x].rel=="edit"){return this.links[x].href;}}return null;},setEditHref:function(url){if(this.links===null){this.links=[];}for(var x in this.links){if(this.links[x].rel&&this.links[x].rel=="edit"){this.links[x].href=url;return;}}this.addLink(url,"edit");}});_4.declare("dojox.atom.io.model.Feed",_6.atom.io.model.AtomItem,{_accepts:{"author":true,"content":true,"category":true,"contributor":true,"created":true,"id":true,"link":true,"published":true,"rights":true,"summary":true,"title":true,"updated":true,"xmlbase":true,"entry":true,"logo":true,"issued":true,"modified":true,"icon":true,"subtitle":true},addEntry:function(_bf){if(!_bf.id){var _c0=_4.i18n.getLocalization("dojox.atom.io","messages");throw new Error(_c0.noId);}if(!this.entries){this.entries=[];}_bf.feedUrl=this.getSelfHref();this.entries.push(_bf);},getFirstEntry:function(){if(!this.entries||this.entries.length===0){return null;}return this.entries[0];},getEntry:function(_c1){if(!this.entries){return null;}for(var x in this.entries){if(this.entries[x].id==_c1){return this.entries[x];}}return null;},removeEntry:function(_c3){if(!this.entries){return;}var _c4=0;for(var i=0;i<this.entries.length;i++){if(this.entries[i]===_c3){this.entries.splice(i,1);_c4++;}}return _c4;},setEntries:function(_c6){for(var x in _c6){this.addEntry(_c6[x]);}},toString:function(){var s=[];var i;s.push("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");s.push("<feed xmlns=\""+_6.atom.io.model._Constants.ATOM_URI+"\"");if(this.xmlBase){s.push(" xml:base=\""+this.xmlBase+"\"");}for(i in this.name_spaces){s.push(" xmlns:"+i+"=\""+this.name_spaces[i]+"\"");}s.push(">\n");s.push("<id>"+(this.id?this.id:"")+"</id>\n");if(this.title){s.push(this.title);}if(this.copyright&&!this.rights){this.rights=this.copyright;}if(this.rights){s.push("<rights>"+this.rights+"</rights>\n");}if(this.issued){s.push("<issued>"+_4.date.stamp.toISOString(this.issued)+"</issued>\n");}if(this.modified){s.push("<modified>"+_4.date.stamp.toISOString(this.modified)+"</modified>\n");}if(this.modified&&!this.updated){this.updated=this.modified;}if(this.updated){s.push("<updated>"+_4.date.stamp.toISOString(this.updated)+"</updated>\n");}if(this.published){s.push("<published>"+_4.date.stamp.toISOString(this.published)+"</published>\n");}if(this.icon){s.push("<icon>"+this.icon+"</icon>\n");}if(this.language){s.push("<language>"+this.language+"</language>\n");}if(this.logo){s.push("<logo>"+this.logo+"</logo>\n");}if(this.subtitle){s.push(this.subtitle.toString());}if(this.tagline){s.push(this.tagline.toString());}var _ca=[this.alternateLinks,this.authors,this.categories,this.contributors,this.otherLinks,this.extensions,this.entries];for(i in _ca){if(_ca[i]){for(var x in _ca[i]){s.push(_ca[i][x]);}}}s.push("</feed>");return s.join("");},createEntry:function(){var _cc=new _6.atom.io.model.Entry();_cc.feedUrl=this.getSelfHref();return _cc;},getSelfHref:function(){if(this.links===null||this.links.length===0){return null;}for(var x in this.links){if(this.links[x].rel&&this.links[x].rel=="self"){return this.links[x].href;}}return null;}});_4.declare("dojox.atom.io.model.Service",_6.atom.io.model.AtomItem,{constructor:function(_ce){this.href=_ce;},buildFromDom:function(_cf){var _d0;var i;var len=_cf.childNodes?_cf.childNodes.length:0;this.workspaces=[];if(_cf.tagName!="service"){return;}if(_cf.namespaceURI!=_6.atom.io.model._Constants.PURL_NS&&_cf.namespaceURI!=_6.atom.io.model._Constants.APP_NS){return;}var ns=_cf.namespaceURI;this.name_space=_cf.namespaceURI;var _d4;if(typeof (_cf.getElementsByTagNameNS)!="undefined"){_d4=_cf.getElementsByTagNameNS(ns,"workspace");}else{_d4=[];var _d5=_cf.getElementsByTagName("workspace");for(i=0;i<_d5.length;i++){if(_d5[i].namespaceURI==ns){_d4.push(_d5[i]);}}}if(_d4&&_d4.length>0){var _d6=0;var _d7;for(i=0;i<_d4.length;i++){_d7=(typeof (_d4.item)==="undefined"?_d4[i]:_d4.item(i));var _d8=new _6.atom.io.model.Workspace();_d8.buildFromDom(_d7);this.workspaces[_d6++]=_d8;}}},getCollection:function(url){for(var i=0;i<this.workspaces.length;i++){var _db=this.workspaces[i].collections;for(var j=0;j<_db.length;j++){if(_db[j].href==url){return _db;}}}return null;}});_4.declare("dojox.atom.io.model.Workspace",_6.atom.io.model.AtomItem,{constructor:function(_dd){this.title=_dd;this.collections=[];},buildFromDom:function(_de){var _df=_6.atom.io.model.util.getNodename(_de);if(_df!="workspace"){return;}var c=_de.childNodes;var len=0;for(var i=0;i<c.length;i++){var _e3=c[i];if(_e3.nodeType===1){_df=_6.atom.io.model.util.getNodename(_e3);if(_e3.namespaceURI==_6.atom.io.model._Constants.PURL_NS||_e3.namespaceURI==_6.atom.io.model._Constants.APP_NS){if(_df==="collection"){var _e4=new _6.atom.io.model.Collection();_e4.buildFromDom(_e3);this.collections[len++]=_e4;}}else{if(_e3.namespaceURI===_6.atom.io.model._Constants.ATOM_NS){if(_df==="title"){this.title=_6.xml.parser.textContent(_e3);}}else{var _e5=_4.i18n.getLocalization("dojox.atom.io","messages");throw new Error(_e5.badNS);}}}}}});_4.declare("dojox.atom.io.model.Collection",_6.atom.io.model.AtomItem,{constructor:function(_e6,_e7){this.href=_e6;this.title=_e7;this.attributes=[];this.features=[];this.children=[];this.memberType=null;this.id=null;},buildFromDom:function(_e8){this.href=_e8.getAttribute("href");var c=_e8.childNodes;for(var i=0;i<c.length;i++){var _eb=c[i];if(_eb.nodeType===1){var _ec=_6.atom.io.model.util.getNodename(_eb);if(_eb.namespaceURI==_6.atom.io.model._Constants.PURL_NS||_eb.namespaceURI==_6.atom.io.model._Constants.APP_NS){if(_ec==="member-type"){this.memberType=_6.xml.parser.textContent(_eb);}else{if(_ec=="feature"){if(_eb.getAttribute("id")){this.features.push(_eb.getAttribute("id"));}}else{var _ed=new _6.atom.io.model.Node();_ed.buildFromDom(_eb);this.children.push(_ed);}}}else{if(_eb.namespaceURI===_6.atom.io.model._Constants.ATOM_NS){if(_ec==="id"){this.id=_6.xml.parser.textContent(_eb);}else{if(_ec==="title"){this.title=_6.xml.parser.textContent(_eb);}}}}}}}});}}};});