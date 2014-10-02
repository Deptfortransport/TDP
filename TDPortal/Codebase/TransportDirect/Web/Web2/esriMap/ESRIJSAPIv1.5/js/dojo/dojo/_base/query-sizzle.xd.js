/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo._base.query"],["require","dojo._base.NodeList"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base.query"]){_4._hasResource["dojo._base.query"]=true;if(typeof _4!="undefined"){_4.provide("dojo._base.query");_4.require("dojo._base.NodeList");_4.query=function(_7,_8,_9){_9=_9||_4.NodeList;if(!_7){return new _9();}if(_7.constructor==_9){return _7;}if(!_4.isString(_7)){return new _9(_7);}if(_4.isString(_8)){_8=_4.byId(_8);if(!_8){return new _9();}}return _4.Sizzle(_7,_8,new _9());};_4._filterQueryResult=function(_a,_b){return _4.Sizzle.filter(_b,_a);};}(function(ns){var _d=/((?:\((?:\([^()]+\)|[^()]+)+\)|\[(?:\[[^[\]]*\]|[^[\]]+)+\]|\\.|[^ >+~,(\[]+)+|[>+~])(\s*,\s*)?/g,_e=0,_f=Object.prototype.toString;var _10=function(_11,_12,_13,_14){_13=_13||[];_12=_12||document;if(_12.nodeType!==1&&_12.nodeType!==9){return [];}if(!_11||typeof _11!=="string"){return _13;}var _15=[],m,set,_18,_19,_1a,_1b,_1c=true;_d.lastIndex=0;while((m=_d.exec(_11))!==null){_15.push(m[1]);if(m[2]){_1b=RegExp.rightContext;break;}}if(_15.length>1&&_1d.match.POS.exec(_11)){if(_15.length===2&&_1d.relative[_15[0]]){var _1e="",_1f;while((_1f=_1d.match.POS.exec(_11))){_1e+=_1f[0];_11=_11.replace(_1d.match.POS,"");}set=_10.filter(_1e,_10(_11,_12));}else{set=_1d.relative[_15[0]]?[_12]:_10(_15.shift(),_12);while(_15.length){var _20=[];_11=_15.shift();if(_1d.relative[_11]){_11+=_15.shift();}for(var i=0,l=set.length;i<l;i++){_10(_11,set[i],_20);}set=_20;}}}else{var ret=_14?{expr:_15.pop(),set:_24(_14)}:_10.find(_15.pop(),_15.length===1&&_12.parentNode?_12.parentNode:_12);set=_10.filter(ret.expr,ret.set);if(_15.length>0){_18=_24(set);}else{_1c=false;}while(_15.length){var cur=_15.pop(),pop=cur;if(!_1d.relative[cur]){cur="";}else{pop=_15.pop();}if(pop==null){pop=_12;}_1d.relative[cur](_18,pop);}}if(!_18){_18=set;}if(!_18){throw "Syntax error, unrecognized expression: "+(cur||_11);}if(_f.call(_18)==="[object Array]"){if(!_1c){_13.push.apply(_13,_18);}else{if(_12.nodeType===1){for(var i=0;_18[i]!=null;i++){if(_18[i]&&(_18[i]===true||_18[i].nodeType===1&&_27(_12,_18[i]))){_13.push(set[i]);}}}else{for(var i=0;_18[i]!=null;i++){if(_18[i]&&_18[i].nodeType===1){_13.push(set[i]);}}}}}else{_24(_18,_13);}if(_1b){_10(_1b,_12,_13,_14);}return _13;};_10.matches=function(_28,set){return _10(_28,null,null,set);};_10.find=function(_2a,_2b){var set,_2d;if(!_2a){return [];}for(var i=0,l=_1d.order.length;i<l;i++){var _30=_1d.order[i],_2d;if((_2d=_1d.match[_30].exec(_2a))){var _31=RegExp.leftContext;if(_31.substr(_31.length-1)!=="\\"){_2d[1]=(_2d[1]||"").replace(/\\/g,"");set=_1d.find[_30](_2d,_2b);if(set!=null){_2a=_2a.replace(_1d.match[_30],"");break;}}}}if(!set){set=_2b.getElementsByTagName("*");}return {set:set,expr:_2a};};_10.filter=function(_32,set,_34,not){var old=_32,_37=[],_38=set,_39,_3a;while(_32&&set.length){for(var _3b in _1d.filter){if((_39=_1d.match[_3b].exec(_32))!=null){var _3c=_1d.filter[_3b],_3d=null,_3e=0,_3f,_40;_3a=false;if(_38==_37){_37=[];}if(_1d.preFilter[_3b]){_39=_1d.preFilter[_3b](_39,_38,_34,_37,not);if(!_39){_3a=_3f=true;}else{if(_39[0]===true){_3d=[];var _41=null,_42;for(var i=0;(_42=_38[i])!==undefined;i++){if(_42&&_41!==_42){_3d.push(_42);_41=_42;}}}}}if(_39){for(var i=0;(_40=_38[i])!==undefined;i++){if(_40){if(_3d&&_40!=_3d[_3e]){_3e++;}_3f=_3c(_40,_39,_3e,_3d);var _44=not^!!_3f;if(_34&&_3f!=null){if(_44){_3a=true;}else{_38[i]=false;}}else{if(_44){_37.push(_40);_3a=true;}}}}}if(_3f!==undefined){if(!_34){_38=_37;}_32=_32.replace(_1d.match[_3b],"");if(!_3a){return [];}break;}}}_32=_32.replace(/\s*,\s*/,"");if(_32==old){if(_3a==null){throw "Syntax error, unrecognized expression: "+_32;}else{break;}}old=_32;}return _38;};var _1d=_10.selectors={order:["ID","NAME","TAG"],match:{ID:/#((?:[\w\u0128-\uFFFF_-]|\\.)+)/,CLASS:/\.((?:[\w\u0128-\uFFFF_-]|\\.)+)/,NAME:/\[name=['"]*((?:[\w\u0128-\uFFFF_-]|\\.)+)['"]*\]/,ATTR:/\[((?:[\w\u0128-\uFFFF_-]|\\.)+)\s*(?:(\S?=)\s*(['"]*)(.*?)\3|)\]/,TAG:/^((?:[\w\u0128-\uFFFF\*_-]|\\.)+)/,CHILD:/:(only|nth|last|first)-child\(?(even|odd|[\dn+-]*)\)?/,POS:/:(nth|eq|gt|lt|first|last|even|odd)\(?(\d*)\)?(?:[^-]|$)/,PSEUDO:/:((?:[\w\u0128-\uFFFF_-]|\\.)+)(?:\((['"]*)((?:\([^\)]+\)|[^\2\(\)]*)+)\2\))?/},attrMap:{"class":"className","for":"htmlFor"},relative:{"+":function(_45,_46){for(var i=0,l=_45.length;i<l;i++){var _49=_45[i];if(_49){var cur=_49.previousSibling;while(cur&&cur.nodeType!==1){cur=cur.previousSibling;}_45[i]=typeof _46==="string"?cur||false:cur===_46;}}if(typeof _46==="string"){_10.filter(_46,_45,true);}},">":function(_4b,_4c){if(typeof _4c==="string"&&!/\W/.test(_4c)){_4c=_4c.toUpperCase();for(var i=0,l=_4b.length;i<l;i++){var _4f=_4b[i];if(_4f){var _50=_4f.parentNode;_4b[i]=_50.nodeName===_4c?_50:false;}}}else{for(var i=0,l=_4b.length;i<l;i++){var _4f=_4b[i];if(_4f){_4b[i]=typeof _4c==="string"?_4f.parentNode:_4f.parentNode===_4c;}}if(typeof _4c==="string"){_10.filter(_4c,_4b,true);}}},"":function(_51,_52){var _53="done"+(_e++),_54=_55;if(!_52.match(/\W/)){var _56=_52=_52.toUpperCase();_54=_57;}_54("parentNode",_52,_53,_51,_56);},"~":function(_58,_59){var _5a="done"+(_e++),_5b=_55;if(typeof _59==="string"&&!_59.match(/\W/)){var _5c=_59=_59.toUpperCase();_5b=_57;}_5b("previousSibling",_59,_5a,_58,_5c);}},find:{ID:function(_5d,_5e){if(_5e.getElementById){var m=_5e.getElementById(_5d[1]);return m?[m]:[];}},NAME:function(_60,_61){return _61.getElementsByName?_61.getElementsByName(_60[1]):null;},TAG:function(_62,_63){return _63.getElementsByTagName(_62[1]);}},preFilter:{CLASS:function(_64,_65,_66,_67,not){_64=" "+_64[1].replace(/\\/g,"")+" ";for(var i=0;_65[i];i++){if(not^(" "+_65[i].className+" ").indexOf(_64)>=0){if(!_66){_67.push(_65[i]);}}else{if(_66){_65[i]=false;}}}return false;},ID:function(_6a){return _6a[1];},TAG:function(_6b){return _6b[1].toUpperCase();},CHILD:function(_6c){if(_6c[1]=="nth"){var _6d=/(-?)(\d*)n((?:\+|-)?\d*)/.exec(_6c[2]=="even"&&"2n"||_6c[2]=="odd"&&"2n+1"||!/\D/.test(_6c[2])&&"0n+"+_6c[2]||_6c[2]);_6c[2]=(_6d[1]+(_6d[2]||1))-0;_6c[3]=_6d[3]-0;}_6c[0]="done"+(_e++);return _6c;},ATTR:function(_6e){var _6f=_6e[1];if(_1d.attrMap[_6f]){_6e[1]=_1d.attrMap[_6f];}if(_6e[2]==="~="){_6e[4]=" "+_6e[4]+" ";}return _6e;},PSEUDO:function(_70,_71,_72,_73,not){if(_70[1]==="not"){if(_70[3].match(_d).length>1){_70[3]=_10(_70[3],null,null,_71);}else{var ret=_10.filter(_70[3],_71,_72,true^not);if(!_72){_73.push.apply(_73,ret);}return false;}}return _70;},POS:function(_76){_76.unshift(true);return _76;}},filters:{enabled:function(_77){return _77.disabled===false&&_77.type!=="hidden";},disabled:function(_78){return _78.disabled===true;},checked:function(_79){return _79.checked===true;},selected:function(_7a){_7a.parentNode.selectedIndex;return _7a.selected===true;},parent:function(_7b){return !!_7b.firstChild;},empty:function(_7c){return !_7c.firstChild;},has:function(_7d,i,_7f){return !!_10(_7f[3],_7d).length;},header:function(_80){return /h\d/i.test(_80.nodeName);},text:function(_81){return "text"===_81.type;},radio:function(_82){return "radio"===_82.type;},checkbox:function(_83){return "checkbox"===_83.type;},file:function(_84){return "file"===_84.type;},password:function(_85){return "password"===_85.type;},submit:function(_86){return "submit"===_86.type;},image:function(_87){return "image"===_87.type;},reset:function(_88){return "reset"===_88.type;},button:function(_89){return "button"===_89.type||_89.nodeName.toUpperCase()==="BUTTON";},input:function(_8a){return /input|select|textarea|button/i.test(_8a.nodeName);}},setFilters:{first:function(_8b,i){return i===0;},last:function(_8d,i,_8f,_90){return i===_90.length-1;},even:function(_91,i){return i%2===0;},odd:function(_93,i){return i%2===1;},lt:function(_95,i,_97){return i<_97[3]-0;},gt:function(_98,i,_9a){return i>_9a[3]-0;},nth:function(_9b,i,_9d){return _9d[3]-0==i;},eq:function(_9e,i,_a0){return _a0[3]-0==i;}},filter:{CHILD:function(_a1,_a2){var _a3=_a2[1],_a4=_a1.parentNode;var _a5=_a2[0];if(_a4&&!_a4[_a5]){var _a6=1;for(var _a7=_a4.firstChild;_a7;_a7=_a7.nextSibling){if(_a7.nodeType==1){_a7.nodeIndex=_a6++;}}_a4[_a5]=_a6-1;}if(_a3=="first"){return _a1.nodeIndex==1;}else{if(_a3=="last"){return _a1.nodeIndex==_a4[_a5];}else{if(_a3=="only"){return _a4[_a5]==1;}else{if(_a3=="nth"){var add=false,_a9=_a2[2],_aa=_a2[3];if(_a9==1&&_aa==0){return true;}if(_a9==0){if(_a1.nodeIndex==_aa){add=true;}}else{if((_a1.nodeIndex-_aa)%_a9==0&&(_a1.nodeIndex-_aa)/_a9>=0){add=true;}}return add;}}}}},PSEUDO:function(_ab,_ac,i,_ae){var _af=_ac[1],_b0=_1d.filters[_af];if(_b0){return _b0(_ab,i,_ac,_ae);}else{if(_af==="contains"){return (_ab.textContent||_ab.innerText||"").indexOf(_ac[3])>=0;}else{if(_af==="not"){var not=_ac[3];for(var i=0,l=not.length;i<l;i++){if(not[i]===_ab){return false;}}return true;}}}},ID:function(_b3,_b4){return _b3.nodeType===1&&_b3.getAttribute("id")===_b4;},TAG:function(_b5,_b6){return (_b6==="*"&&_b5.nodeType===1)||_b5.nodeName===_b6;},CLASS:function(_b7,_b8){return _b8.test(_b7.className);},ATTR:function(_b9,_ba){var _bb=_b9[_ba[1]]||_b9.getAttribute(_ba[1]),_bc=_bb+"",_bd=_ba[2],_be=_ba[4];return _bb==null?false:_bd==="="?_bc===_be:_bd==="*="?_bc.indexOf(_be)>=0:_bd==="~="?(" "+_bc+" ").indexOf(_be)>=0:!_ba[4]?_bb:_bd==="!="?_bc!=_be:_bd==="^="?_bc.indexOf(_be)===0:_bd==="$="?_bc.substr(_bc.length-_be.length)===_be:_bd==="|="?_bc===_be||_bc.substr(0,_be.length+1)===_be+"-":false;},POS:function(_bf,_c0,i,_c2){var _c3=_c0[2],_c4=_1d.setFilters[_c3];if(_c4){return _c4(_bf,i,_c0,_c2);}}}};for(var _c5 in _1d.match){_1d.match[_c5]=RegExp(_1d.match[_c5].source+/(?![^\[]*\])(?![^\(]*\))/.source);}var _24=function(_c6,_c7){_c6=Array.prototype.slice.call(_c6);if(_c7){_c7.push.apply(_c7,_c6);return _c7;}return _c6;};try{Array.prototype.slice.call(document.documentElement.childNodes);}catch(e){_24=function(_c8,_c9){var ret=_c9||[];if(_f.call(_c8)==="[object Array]"){Array.prototype.push.apply(ret,_c8);}else{if(typeof _c8.length==="number"){for(var i=0,l=_c8.length;i<l;i++){ret.push(_c8[i]);}}else{for(var i=0;_c8[i];i++){ret.push(_c8[i]);}}}return ret;};}(function(){var _cd=document.createElement("form"),id="script"+(new Date).getTime();_cd.innerHTML="<input name='"+id+"'/>";var _cf=document.documentElement;_cf.insertBefore(_cd,_cf.firstChild);if(!!document.getElementById(id)){_1d.find.ID=function(_d0,_d1){if(_d1.getElementById){var m=_d1.getElementById(_d0[1]);return m?m.id===_d0[1]||m.getAttributeNode&&m.getAttributeNode("id").nodeValue===_d0[1]?[m]:undefined:[];}};_1d.filter.ID=function(_d3,_d4){var _d5=_d3.getAttributeNode&&_d3.getAttributeNode("id");return _d3.nodeType===1&&_d5&&_d5.nodeValue===_d4;};}_cf.removeChild(_cd);})();(function(){var div=document.createElement("div");div.appendChild(document.createComment(""));if(div.getElementsByTagName("*").length>0){_1d.find.TAG=function(_d7,_d8){var _d9=_d8.getElementsByTagName(_d7[1]);if(_d7[1]==="*"){var tmp=[];for(var i=0;_d9[i];i++){if(_d9[i].nodeType===1){tmp.push(_d9[i]);}}_d9=tmp;}return _d9;};}})();if(document.querySelectorAll){(function(){var _dc=_10;_10=function(_dd,_de,_df,_e0){_de=_de||document;if(!_e0&&_de.nodeType===9){try{return _24(_de.querySelectorAll(_dd),_df);}catch(e){}}return _dc(_dd,_de,_df,_e0);};_10.find=_dc.find;_10.filter=_dc.filter;_10.selectors=_dc.selectors;_10.matches=_dc.matches;})();}if(document.documentElement.getElementsByClassName){_1d.order.splice(1,0,"CLASS");_1d.find.CLASS=function(_e1,_e2){return _e2.getElementsByClassName(_e1[1]);};}function _57(dir,cur,_e5,_e6,_e7){for(var i=0,l=_e6.length;i<l;i++){var _ea=_e6[i];if(_ea){_ea=_ea[dir];var _eb=false;while(_ea&&_ea.nodeType){var _ec=_ea[_e5];if(_ec){_eb=_e6[_ec];break;}if(_ea.nodeType===1){_ea[_e5]=i;}if(_ea.nodeName===cur){_eb=_ea;break;}_ea=_ea[dir];}_e6[i]=_eb;}}};function _55(dir,cur,_ef,_f0,_f1){for(var i=0,l=_f0.length;i<l;i++){var _f4=_f0[i];if(_f4){_f4=_f4[dir];var _f5=false;while(_f4&&_f4.nodeType){if(_f4[_ef]){_f5=_f0[_f4[_ef]];break;}if(_f4.nodeType===1){_f4[_ef]=i;if(typeof cur!=="string"){if(_f4===cur){_f5=true;break;}}else{if(_10.filter(cur,[_f4]).length>0){_f5=_f4;break;}}}_f4=_f4[dir];}_f0[i]=_f5;}}};var _27=document.compareDocumentPosition?function(a,b){return a.compareDocumentPosition(b)&16;}:function(a,b){return a!==b&&(a.contains?a.contains(b):true);};(ns||window).Sizzle=_10;})(typeof _4=="undefined"?null:_4);}}};});