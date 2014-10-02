/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["require","dojo._base.lang"],["provide","dojo._base.html"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo._base.html"]){_4._hasResource["dojo._base.html"]=true;_4.require("dojo._base.lang");_4.provide("dojo._base.html");try{document.execCommand("BackgroundImageCache",false,true);}catch(e){}if(_4.isIE||_4.isOpera){_4.byId=function(id,_8){if(_4.isString(id)){var _d=_8||_4.doc;var te=_d.getElementById(id);if(te&&(te.attributes.id.value==id||te.id==id)){return te;}else{var _b=_d.all[id];if(!_b||_b.nodeName){_b=[_b];}var i=0;while((te=_b[i++])){if((te.attributes&&te.attributes.id&&te.attributes.id.value==id)||te.id==id){return te;}}}}else{return id;}};}else{_4.byId=function(id,_e){return _4.isString(id)?(_e||_4.doc).getElementById(id):id;};}(function(){var d=_4;var _10=null;d.addOnWindowUnload(function(){_10=null;});_4._destroyElement=_4.destroy=function(_11){_11=d.byId(_11);try{if(!_10||_10.ownerDocument!=_11.ownerDocument){_10=_11.ownerDocument.createElement("div");}_10.appendChild(_11.parentNode?_11.parentNode.removeChild(_11):_11);_10.innerHTML="";}catch(e){}};_4.isDescendant=function(_12,_13){try{_12=d.byId(_12);_13=d.byId(_13);while(_12){if(_12===_13){return true;}_12=_12.parentNode;}}catch(e){}return false;};_4.setSelectable=function(_14,_15){_14=d.byId(_14);if(d.isMozilla){_14.style.MozUserSelect=_15?"":"none";}else{if(d.isKhtml||d.isWebKit){_14.style.KhtmlUserSelect=_15?"auto":"none";}else{if(d.isIE){var v=(_14.unselectable=_15?"":"on");d.query("*",_14).forEach("item.unselectable = '"+v+"'");}}}};var _17=function(_18,ref){var _1a=ref.parentNode;if(_1a){_1a.insertBefore(_18,ref);}};var _1b=function(_1c,ref){var _1e=ref.parentNode;if(_1e){if(_1e.lastChild==ref){_1e.appendChild(_1c);}else{_1e.insertBefore(_1c,ref.nextSibling);}}};_4.place=function(_1f,_20,_21){_20=d.byId(_20);if(d.isString(_1f)){_1f=_1f.charAt(0)=="<"?d._toDom(_1f,_20.ownerDocument):d.byId(_1f);}if(typeof _21=="number"){var cn=_20.childNodes;if(!cn.length||cn.length<=_21){_20.appendChild(_1f);}else{_17(_1f,cn[_21<0?0:_21]);}}else{switch(_21){case "before":_17(_1f,_20);break;case "after":_1b(_1f,_20);break;case "replace":_20.parentNode.replaceChild(_1f,_20);break;case "only":d.empty(_20);_20.appendChild(_1f);break;case "first":if(_20.firstChild){_17(_1f,_20.firstChild);break;}default:_20.appendChild(_1f);}}return _1f;};_4.boxModel="content-box";if(d.isIE){var _23=document.compatMode;d.boxModel=_23=="BackCompat"||_23=="QuirksMode"||d.isIE<6?"border-box":"content-box";}var gcs;if(d.isWebKit){gcs=function(_25){var s;if(_25.nodeType==1){var dv=_25.ownerDocument.defaultView;s=dv.getComputedStyle(_25,null);if(!s&&_25.style){_25.style.display="";s=dv.getComputedStyle(_25,null);}}return s||{};};}else{if(d.isIE){gcs=function(_28){return _28.nodeType==1?_28.currentStyle:{};};}else{gcs=function(_29){return _29.nodeType==1?_29.ownerDocument.defaultView.getComputedStyle(_29,null):{};};}}_4.getComputedStyle=gcs;if(!d.isIE){d._toPixelValue=function(_2a,_2b){return parseFloat(_2b)||0;};}else{d._toPixelValue=function(_2c,_2d){if(!_2d){return 0;}if(_2d=="medium"){return 4;}if(_2d.slice&&_2d.slice(-2)=="px"){return parseFloat(_2d);}with(_2c){var _2e=style.left;var _2f=runtimeStyle.left;runtimeStyle.left=currentStyle.left;try{style.left=_2d;_2d=style.pixelLeft;}catch(e){_2d=0;}style.left=_2e;runtimeStyle.left=_2f;}return _2d;};}var px=d._toPixelValue;var _31="DXImageTransform.Microsoft.Alpha";var af=function(n,f){try{return n.filters.item(_31);}catch(e){return f?{}:null;}};_4._getOpacity=d.isIE?function(_35){try{return af(_35).Opacity/100;}catch(e){return 1;}}:function(_36){return gcs(_36).opacity;};_4._setOpacity=d.isIE?function(_37,_38){var ov=_38*100;_37.style.zoom=1;af(_37,1).Enabled=!(_38==1);if(!af(_37)){_37.style.filter+=" progid:"+_31+"(Opacity="+ov+")";}else{af(_37,1).Opacity=ov;}if(_37.nodeName.toLowerCase()=="tr"){d.query("> td",_37).forEach(function(i){d._setOpacity(i,_38);});}return _38;}:function(_3b,_3c){return _3b.style.opacity=_3c;};var _3d={left:true,top:true};var _3e=/margin|padding|width|height|max|min|offset/;var _3f=function(_40,_41,_42){_41=_41.toLowerCase();if(d.isIE){if(_42=="auto"){if(_41=="height"){return _40.offsetHeight;}if(_41=="width"){return _40.offsetWidth;}}if(_41=="fontweight"){switch(_42){case 700:return "bold";case 400:default:return "normal";}}}if(!(_41 in _3d)){_3d[_41]=_3e.test(_41);}return _3d[_41]?px(_40,_42):_42;};var _43=d.isIE?"styleFloat":"cssFloat",_44={"cssFloat":_43,"styleFloat":_43,"float":_43};_4.style=function(_45,_46,_47){var n=d.byId(_45),_49=arguments.length,op=(_46=="opacity");_46=_44[_46]||_46;if(_49==3){return op?d._setOpacity(n,_47):n.style[_46]=_47;}if(_49==2&&op){return d._getOpacity(n);}var s=gcs(n);if(_49==2&&!d.isString(_46)){for(var x in _46){d.style(_45,x,_46[x]);}return s;}return (_49==1)?s:_3f(n,_46,s[_46]||n.style[_46]);};_4._getPadExtents=function(n,_4e){var s=_4e||gcs(n),l=px(n,s.paddingLeft),t=px(n,s.paddingTop);return {l:l,t:t,w:l+px(n,s.paddingRight),h:t+px(n,s.paddingBottom)};};_4._getBorderExtents=function(n,_53){var ne="none",s=_53||gcs(n),bl=(s.borderLeftStyle!=ne?px(n,s.borderLeftWidth):0),bt=(s.borderTopStyle!=ne?px(n,s.borderTopWidth):0);return {l:bl,t:bt,w:bl+(s.borderRightStyle!=ne?px(n,s.borderRightWidth):0),h:bt+(s.borderBottomStyle!=ne?px(n,s.borderBottomWidth):0)};};_4._getPadBorderExtents=function(n,_59){var s=_59||gcs(n),p=d._getPadExtents(n,s),b=d._getBorderExtents(n,s);return {l:p.l+b.l,t:p.t+b.t,w:p.w+b.w,h:p.h+b.h};};_4._getMarginExtents=function(n,_5e){var s=_5e||gcs(n),l=px(n,s.marginLeft),t=px(n,s.marginTop),r=px(n,s.marginRight),b=px(n,s.marginBottom);if(d.isWebKit&&(s.position!="absolute")){r=l;}return {l:l,t:t,w:l+r,h:t+b};};_4._getMarginBox=function(_64,_65){var s=_65||gcs(_64),me=d._getMarginExtents(_64,s);var l=_64.offsetLeft-me.l,t=_64.offsetTop-me.t,p=_64.parentNode;if(d.isMoz){var sl=parseFloat(s.left),st=parseFloat(s.top);if(!isNaN(sl)&&!isNaN(st)){l=sl,t=st;}else{if(p&&p.style){var pcs=gcs(p);if(pcs.overflow!="visible"){var be=d._getBorderExtents(p,pcs);l+=be.l,t+=be.t;}}}}else{if(d.isOpera||(d.isIE>7&&!d.isQuirks)){if(p){be=d._getBorderExtents(p);l-=be.l;t-=be.t;}}}return {l:l,t:t,w:_64.offsetWidth+me.w,h:_64.offsetHeight+me.h};};_4._getContentBox=function(_6f,_70){var s=_70||gcs(_6f),pe=d._getPadExtents(_6f,s),be=d._getBorderExtents(_6f,s),w=_6f.clientWidth,h;if(!w){w=_6f.offsetWidth,h=_6f.offsetHeight;}else{h=_6f.clientHeight,be.w=be.h=0;}if(d.isOpera){pe.l+=be.l;pe.t+=be.t;}return {l:pe.l,t:pe.t,w:w-pe.w-be.w,h:h-pe.h-be.h};};_4._getBorderBox=function(_76,_77){var s=_77||gcs(_76),pe=d._getPadExtents(_76,s),cb=d._getContentBox(_76,s);return {l:cb.l-pe.l,t:cb.t-pe.t,w:cb.w+pe.w,h:cb.h+pe.h};};_4._setBox=function(_7b,l,t,w,h,u){u=u||"px";var s=_7b.style;if(!isNaN(l)){s.left=l+u;}if(!isNaN(t)){s.top=t+u;}if(w>=0){s.width=w+u;}if(h>=0){s.height=h+u;}};_4._isButtonTag=function(_82){return _82.tagName=="BUTTON"||_82.tagName=="INPUT"&&_82.getAttribute("type").toUpperCase()=="BUTTON";};_4._usesBorderBox=function(_83){var n=_83.tagName;return d.boxModel=="border-box"||n=="TABLE"||d._isButtonTag(_83);};_4._setContentSize=function(_85,_86,_87,_88){if(d._usesBorderBox(_85)){var pb=d._getPadBorderExtents(_85,_88);if(_86>=0){_86+=pb.w;}if(_87>=0){_87+=pb.h;}}d._setBox(_85,NaN,NaN,_86,_87);};_4._setMarginBox=function(_8a,_8b,_8c,_8d,_8e,_8f){var s=_8f||gcs(_8a),bb=d._usesBorderBox(_8a),pb=bb?_93:d._getPadBorderExtents(_8a,s);if(d.isWebKit){if(d._isButtonTag(_8a)){var ns=_8a.style;if(_8d>=0&&!ns.width){ns.width="4px";}if(_8e>=0&&!ns.height){ns.height="4px";}}}var mb=d._getMarginExtents(_8a,s);if(_8d>=0){_8d=Math.max(_8d-pb.w-mb.w,0);}if(_8e>=0){_8e=Math.max(_8e-pb.h-mb.h,0);}d._setBox(_8a,_8b,_8c,_8d,_8e);};var _93={l:0,t:0,w:0,h:0};_4.marginBox=function(_96,box){var n=d.byId(_96),s=gcs(n),b=box;return !b?d._getMarginBox(n,s):d._setMarginBox(n,b.l,b.t,b.w,b.h,s);};_4.contentBox=function(_9b,box){var n=d.byId(_9b),s=gcs(n),b=box;return !b?d._getContentBox(n,s):d._setContentSize(n,b.w,b.h,s);};var _a0=function(_a1,_a2){if(!(_a1=(_a1||0).parentNode)){return 0;}var val,_a4=0,_b=d.body();while(_a1&&_a1.style){if(gcs(_a1).position=="fixed"){return 0;}val=_a1[_a2];if(val){_a4+=val-0;if(_a1==_b){break;}}_a1=_a1.parentNode;}return _a4;};_4._docScroll=function(){var _b=d.body(),_w=d.global,de=d.doc.documentElement;return {y:(_w.pageYOffset||de.scrollTop||_b.scrollTop||0),x:(_w.pageXOffset||d._fixIeBiDiScrollLeft(de.scrollLeft)||_b.scrollLeft||0)};};_4._isBodyLtr=function(){return ("_bodyLtr" in d)?d._bodyLtr:d._bodyLtr=gcs(d.body()).direction=="ltr";};_4._getIeDocumentElementOffset=function(){var de=d.doc.documentElement;if(d.isIE<7){return {x:d._isBodyLtr()||window.parent==window?de.clientLeft:de.offsetWidth-de.clientWidth-de.clientLeft,y:de.clientTop};}else{if(d.isIE<8){return {x:de.getBoundingClientRect().left,y:de.getBoundingClientRect().top};}else{return {x:0,y:0};}}};_4._fixIeBiDiScrollLeft=function(_aa){var dd=d.doc;if(d.isIE<8&&!d._isBodyLtr()){var de=dd.compatMode=="BackCompat"?dd.body:dd.documentElement;return _aa+de.clientWidth-de.scrollWidth;}return _aa;};_4._abs=function(_ad,_ae){var db=d.body(),dh=d.body().parentNode,ret;if(_ad["getBoundingClientRect"]){var _b2=_ad.getBoundingClientRect();ret={x:_b2.left,y:_b2.top};if(d.isFF>=3){var cs=gcs(dh);ret.x-=px(dh,cs.marginLeft)+px(dh,cs.borderLeftWidth);ret.y-=px(dh,cs.marginTop)+px(dh,cs.borderTopWidth);}if(d.isIE){var _b4=d._getIeDocumentElementOffset();ret.x-=_b4.x+(d.isQuirks?db.clientLeft:0);ret.y-=_b4.y+(d.isQuirks?db.clientTop:0);}}else{ret={x:0,y:0};if(_ad["offsetParent"]){ret.x-=_a0(_ad,"scrollLeft");ret.y-=_a0(_ad,"scrollTop");var _b5=_ad;do{var n=_b5.offsetLeft,t=_b5.offsetTop;ret.x+=isNaN(n)?0:n;ret.y+=isNaN(t)?0:t;cs=gcs(_b5);if(_b5!=_ad){if(d.isFF){ret.x+=2*px(_b5,cs.borderLeftWidth);ret.y+=2*px(_b5,cs.borderTopWidth);}else{ret.x+=px(_b5,cs.borderLeftWidth);ret.y+=px(_b5,cs.borderTopWidth);}}if(d.isFF&&cs.position=="static"){var _b8=_b5.parentNode;while(_b8!=_b5.offsetParent){var pcs=gcs(_b8);if(pcs.position=="static"){ret.x+=px(_b5,pcs.borderLeftWidth);ret.y+=px(_b5,pcs.borderTopWidth);}_b8=_b8.parentNode;}}_b5=_b5.offsetParent;}while((_b5!=dh)&&_b5);}else{if(_ad.x&&_ad.y){ret.x+=isNaN(_ad.x)?0:_ad.x;ret.y+=isNaN(_ad.y)?0:_ad.y;}}}if(_ae){var _ba=d._docScroll();ret.x+=_ba.x;ret.y+=_ba.y;}return ret;};_4.coords=function(_bb,_bc){var n=d.byId(_bb),s=gcs(n),mb=d._getMarginBox(n,s);var abs=d._abs(n,_bc);mb.x=abs.x;mb.y=abs.y;return mb;};var _c1=d.isIE<8;var _c2=function(_c3){switch(_c3.toLowerCase()){case "tabindex":return _c1?"tabIndex":"tabindex";case "readonly":return "readOnly";case "class":return "className";case "for":case "htmlfor":return _c1?"htmlFor":"for";default:return _c3;}};var _c4={colspan:"colSpan",enctype:"enctype",frameborder:"frameborder",method:"method",rowspan:"rowSpan",scrolling:"scrolling",shape:"shape",span:"span",type:"type",valuetype:"valueType",classname:"className",innerhtml:"innerHTML"};_4.hasAttr=function(_c5,_c6){_c5=d.byId(_c5);var _c7=_c2(_c6);_c7=_c7=="htmlFor"?"for":_c7;var _c8=_c5.getAttributeNode&&_c5.getAttributeNode(_c7);return _c8?_c8.specified:false;};var _c9={},_ca=0,_cb=_4._scopeName+"attrid",_cc={col:1,colgroup:1,table:1,tbody:1,tfoot:1,thead:1,tr:1,title:1};_4.attr=function(_cd,_ce,_cf){_cd=d.byId(_cd);var _d0=arguments.length;if(_d0==2&&!d.isString(_ce)){for(var x in _ce){d.attr(_cd,x,_ce[x]);}return;}_ce=_c2(_ce);if(_d0==3){if(d.isFunction(_cf)){var _d2=d.attr(_cd,_cb);if(!_d2){_d2=_ca++;d.attr(_cd,_cb,_d2);}if(!_c9[_d2]){_c9[_d2]={};}var h=_c9[_d2][_ce];if(h){d.disconnect(h);}else{try{delete _cd[_ce];}catch(e){}}_c9[_d2][_ce]=d.connect(_cd,_ce,_cf);}else{if(typeof _cf=="boolean"){_cd[_ce]=_cf;}else{if(_ce==="style"&&!d.isString(_cf)){d.style(_cd,_cf);}else{if(_ce=="className"){_cd.className=_cf;}else{if(_ce==="innerHTML"){if(d.isIE&&_cd.tagName.toLowerCase() in _cc){d.empty(_cd);_cd.appendChild(d._toDom(_cf,_cd.ownerDocument));}else{_cd[_ce]=_cf;}}else{_cd.setAttribute(_ce,_cf);}}}}}}else{var _d4=_c4[_ce.toLowerCase()];if(_d4){return _cd[_d4];}var _d5=_cd[_ce];return (typeof _d5=="boolean"||typeof _d5=="function")?_d5:(d.hasAttr(_cd,_ce)?_cd.getAttribute(_ce):null);}};_4.removeAttr=function(_d6,_d7){d.byId(_d6).removeAttribute(_c2(_d7));};_4.create=function(tag,_d9,_da,pos){var doc=d.doc;if(_da){_da=d.byId(_da);doc=_da.ownerDocument;}if(d.isString(tag)){tag=doc.createElement(tag);}if(_d9){d.attr(tag,_d9);}if(_da){d.place(tag,_da,pos);}return tag;};d.empty=d.isIE?function(_dd){_dd=d.byId(_dd);for(var c;c=_dd.lastChild;){d.destroy(c);}}:function(_df){d.byId(_df).innerHTML="";};var _e0={option:["select"],tbody:["table"],thead:["table"],tfoot:["table"],tr:["table","tbody"],td:["table","tbody","tr"],th:["table","thead","tr"],legend:["fieldset"],caption:["table"],colgroup:["table"],col:["table","colgroup"],li:["ul"]},_e1=/<\s*([\w\:]+)/,_e2={},_e3=0,_e4="__"+d._scopeName+"ToDomId";for(var _e5 in _e0){var tw=_e0[_e5];tw.pre=_e5=="option"?"<select multiple=\"multiple\">":"<"+tw.join("><")+">";tw.post="</"+tw.reverse().join("></")+">";}d._toDom=function(_e7,doc){doc=doc||d.doc;var _e9=doc[_e4];if(!_e9){doc[_e4]=_e9=++_e3+"";_e2[_e9]=doc.createElement("div");}_e7+="";var _ea=_e7.match(_e1),tag=_ea?_ea[1].toLowerCase():"",_ec=_e2[_e9],_ed,i,fc,df;if(_ea&&_e0[tag]){_ed=_e0[tag];_ec.innerHTML=_ed.pre+_e7+_ed.post;for(i=_ed.length;i;--i){_ec=_ec.firstChild;}}else{_ec.innerHTML=_e7;}if(_ec.childNodes.length==1){return _ec.removeChild(_ec.firstChild);}df=doc.createDocumentFragment();while(fc=_ec.firstChild){df.appendChild(fc);}return df;};var _f1="className";_4.hasClass=function(_f2,_f3){return ((" "+d.byId(_f2)[_f1]+" ").indexOf(" "+_f3+" ")>=0);};_4.addClass=function(_f4,_f5){_f4=d.byId(_f4);var cls=_f4[_f1];if((" "+cls+" ").indexOf(" "+_f5+" ")<0){_f4[_f1]=cls+(cls?" ":"")+_f5;}};_4.removeClass=function(_f7,_f8){_f7=d.byId(_f7);var t=d.trim((" "+_f7[_f1]+" ").replace(" "+_f8+" "," "));if(_f7[_f1]!=t){_f7[_f1]=t;}};_4.toggleClass=function(_fa,_fb,_fc){if(_fc===undefined){_fc=!d.hasClass(_fa,_fb);}d[_fc?"addClass":"removeClass"](_fa,_fb);};})();}}};});