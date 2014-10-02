/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.secure.DOM"],["require","dojox.lang.observable"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.secure.DOM"]){_4._hasResource["dojox.secure.DOM"]=true;_4.provide("dojox.secure.DOM");_4.require("dojox.lang.observable");_6.secure.DOM=function(_7){function _8(_9){if(!_9){return _9;}var _a=_9;do{if(_a==_7){return _b(_9);}}while((_a=_a.parentNode));return null;};function _b(_c){if(_c){if(_c.nodeType){var _d=_e(_c);if(_c.nodeType==1&&typeof _d.style=="function"){_d.style=_f(_c.style);_d.ownerDocument=_10;_d.childNodes={__get__:function(i){return _b(_c.childNodes[i]);},length:0};}return _d;}if(_c&&typeof _c=="object"){if(_c.__observable){return _c.__observable;}_d=_c instanceof Array?[]:{};_c.__observable=_d;for(var i in _c){if(i!="__observable"){_d[i]=_b(_c[i]);}}_d.data__=_c;return _d;}if(typeof _c=="function"){var _13=function(_14){if(typeof _14=="function"){return function(){for(var i=0;i<arguments.length;i++){arguments[i]=_b(arguments[i]);}return _13(_14.apply(_b(this),arguments));};}return _6.secure.unwrap(_14);};return function(){if(_c.safetyCheck){_c.safetyCheck.apply(_13(this),arguments);}for(var i=0;i<arguments.length;i++){arguments[i]=_13(arguments[i]);}return _b(_c.apply(_13(this),arguments));};}}return _c;};unwrap=_6.secure.unwrap;function _17(css){css+="";if(css.match(/behavior:|content:|javascript:|binding|expression|\@import/)){throw new Error("Illegal CSS");}var id=_7.id||(_7.id="safe"+(""+Math.random()).substring(2));return css.replace(/(\}|^)\s*([^\{]*\{)/g,function(t,a,b){return a+" #"+id+" "+b;});};function _1d(url){if(url.match(/:/)&&!url.match(/^(http|ftp|mailto)/)){throw new Error("Unsafe URL "+url);}};function _1f(el){if(el&&el.nodeType==1){if(el.tagName.match(/script/i)){var src=el.src;if(src&&src!=""){el.parentNode.removeChild(el);_4.xhrGet({url:src,secure:true}).addCallback(function(_22){_10.evaluate(_22);});}else{var _23=el.innerHTML;el.parentNode.removeChild(el);_b.evaluate(_23);}}if(el.tagName.match(/link/i)){throw new Error("illegal tag");}if(el.tagName.match(/style/i)){var _24=function(_25){if(el.styleSheet){el.styleSheet.cssText=_25;}else{var _26=doc.createTextNode(_25);if(el.childNodes[0]){el.replaceChild(_26,el.childNodes[0]);}else{el.appendChild(_26);}}};src=el.src;if(src&&src!=""){alert("src"+src);el.src=null;_4.xhrGet({url:src,secure:true}).addCallback(function(_27){_24(_17(_27));});}_24(_17(el.innerHTML));}if(el.style){_17(el.style.cssText);}if(el.href){_1d(el.href);}if(el.src){_1d(el.src);}var _28,i=0;while((_28=el.attributes[i++])){if(_28.name.substring(0,2)=="on"&&_28.value!="null"&&_28.value!=""){throw new Error("event handlers not allowed in the HTML, they must be set with element.addEventListener");}}var _2a=el.childNodes;for(var i=0,l=_2a.length;i<l;i++){_1f(_2a[i]);}}};function _2c(_2d){var div=document.createElement("div");if(_2d.match(/<object/i)){throw new Error("The object tag is not allowed");}div.innerHTML=_2d;_1f(div);return div;};var doc=_7.ownerDocument;var _10={getElementById:function(id){return _8(doc.getElementById(id));},createElement:function(_31){return _b(doc.createElement(_31));},createTextNode:function(_32){return _b(doc.createTextNode(_32));},write:function(str){var div=_2c(str);while(div.childNodes.length){_7.appendChild(div.childNodes[0]);}}};_10.open=_10.close=function(){};var _35={innerHTML:function(_36,_37){console.log("setting innerHTML");_36.innerHTML=_2c(_37).innerHTML;}};_35.outerHTML=function(_38,_39){throw new Error("Can not set this property");};function _3a(_3b,_3c){return function(_3d,_3e){_1f(_3e[_3c]);return _3d[_3b](_3e[0]);};};var _3f={appendChild:_3a("appendChild",0),insertBefore:_3a("insertBefore",0),replaceChild:_3a("replaceChild",1),cloneNode:function(_40,_41){return _40.cloneNode(_41[0]);},addEventListener:function(_42,_43){_4.connect(_42,"on"+_43[0],this,function(_44){_44=_e(_44||window.event);_43[1].call(this,_44);});}};_3f.childNodes=_3f.style=_3f.ownerDocument=function(){};function _45(_46){return _6.lang.makeObservable(function(_47,_48){var _49;return _47[_48];},_46,function(_4a,_4b,_4c,_4d){for(var i=0;i<_4d.length;i++){_4d[i]=unwrap(_4d[i]);}if(_3f[_4c]){return _b(_3f[_4c].call(_4a,_4b,_4d));}return _b(_4b[_4c].apply(_4b,_4d));},_3f);};var _e=_45(function(_4f,_50,_51){if(_35[_50]){_35[_50](_4f,_51);}_4f[_50]=_51;});var _52={behavior:1,MozBinding:1};var _f=_45(function(_53,_54,_55){if(!_52[_54]){_53[_54]=_17(_55);}});_b.safeHTML=_2c;_b.safeCSS=_17;return _b;};_6.secure.unwrap=function unwrap(_56){return (_56&&_56.data__)||_56;};}}};});