/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.grid.compat._grid.lib"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.grid.compat._grid.lib"]){_4._hasResource["dojox.grid.compat._grid.lib"]=true;_4.provide("dojox.grid.compat._grid.lib");_4.mixin(_6.grid,{na:"...",nop:function(){},getTdIndex:function(td){return td.cellIndex>=0?td.cellIndex:_4.indexOf(td.parentNode.cells,td);},getTrIndex:function(tr){return tr.rowIndex>=0?tr.rowIndex:_4.indexOf(tr.parentNode.childNodes,tr);},getTr:function(_9,_a){return _9&&((_9.rows||0)[_a]||_9.childNodes[_a]);},getTd:function(_b,_c,_d){return (_6.grid.getTr(_b,_c)||0)[_d];},findTable:function(_e){for(var n=_e;n&&n.tagName!="TABLE";n=n.parentNode){}return n;},ascendDom:function(_10,_11){for(var n=_10;n&&_11(n);n=n.parentNode){}return n;},makeNotTagName:function(_13){var _14=_13.toUpperCase();return function(_15){return _15.tagName!=_14;};},fire:function(ob,ev,_18){var fn=ob&&ev&&ob[ev];return fn&&(_18?fn.apply(ob,_18):ob[ev]());},setStyleText:function(_1a,_1b){if(_1a.style.cssText==undefined){_1a.setAttribute("style",_1b);}else{_1a.style.cssText=_1b;}},getStyleText:function(_1c,_1d){return (_1c.style.cssText==undefined?_1c.getAttribute("style"):_1c.style.cssText);},setStyle:function(_1e,_1f,_20){if(_1e&&_1e.style[_1f]!=_20){_1e.style[_1f]=_20;}},setStyleHeightPx:function(_21,_22){if(_22>=0){_6.grid.setStyle(_21,"height",_22+"px");}},mouseEvents:["mouseover","mouseout","mousedown","mouseup","click","dblclick","contextmenu"],keyEvents:["keyup","keydown","keypress"],funnelEvents:function(_23,_24,_25,_26){var _27=(_26?_26:_6.grid.mouseEvents.concat(_6.grid.keyEvents));for(var i=0,l=_27.length;i<l;i++){_4.connect(_23,"on"+_27[i],_24,_25);}},removeNode:function(_2a){_2a=_4.byId(_2a);_2a&&_2a.parentNode&&_2a.parentNode.removeChild(_2a);return _2a;},getScrollbarWidth:function(){if(this._scrollBarWidth){return this._scrollBarWidth;}this._scrollBarWidth=18;try{var e=document.createElement("div");e.style.cssText="top:0;left:0;width:100px;height:100px;overflow:scroll;position:absolute;visibility:hidden;";document.body.appendChild(e);this._scrollBarWidth=e.offsetWidth-e.clientWidth;document.body.removeChild(e);delete e;}catch(ex){}return this._scrollBarWidth;},getRef:function(_2c,_2d,_2e){var obj=_2e||_4.global,_30=_2c.split("."),_31=_30.pop();for(var i=0,p;obj&&(p=_30[i]);i++){obj=(p in obj?obj[p]:(_2d?obj[p]={}:undefined));}return {obj:obj,prop:_31};},getProp:function(_34,_35,_36){with(_6.grid.getRef(_34,_35,_36)){return (obj)&&(prop)&&(prop in obj?obj[prop]:(_35?obj[prop]={}:undefined));}},indexInParent:function(_37){var i=0,n,p=_37.parentNode;while((n=p.childNodes[i++])){if(n==_37){return i-1;}}return -1;},cleanNode:function(_3b){if(!_3b){return;}var _3c=function(inW){return inW.domNode&&_4.isDescendant(inW.domNode,_3b,true);};var ws=_5.registry.filter(_3c);for(var i=0,w;(w=ws[i]);i++){w.destroy();}delete ws;},getTagName:function(_41){var _42=_4.byId(_41);return (_42&&_42.tagName?_42.tagName.toLowerCase():"");},nodeKids:function(_43,_44){var _45=[];var i=0,n;while((n=_43.childNodes[i++])){if(_6.grid.getTagName(n)==_44){_45.push(n);}}return _45;},divkids:function(_48){return _6.grid.nodeKids(_48,"div");},focusSelectNode:function(_49){try{_6.grid.fire(_49,"focus");_6.grid.fire(_49,"select");}catch(e){}},whenIdle:function(){setTimeout(_4.hitch.apply(_4,arguments),0);},arrayCompare:function(inA,inB){for(var i=0,l=inA.length;i<l;i++){if(inA[i]!=inB[i]){return false;}}return (inA.length==inB.length);},arrayInsert:function(_4e,_4f,_50){if(_4e.length<=_4f){_4e[_4f]=_50;}else{_4e.splice(_4f,0,_50);}},arrayRemove:function(_51,_52){_51.splice(_52,1);},arraySwap:function(_53,inI,inJ){var _56=_53[inI];_53[inI]=_53[inJ];_53[inJ]=_56;},initTextSizePoll:function(_57){var f=document.createElement("div");with(f.style){top="0px";left="0px";position="absolute";visibility="hidden";}f.innerHTML="TheQuickBrownFoxJumpedOverTheLazyDog";document.body.appendChild(f);var fw=f.offsetWidth;var job=function(){if(f.offsetWidth!=fw){fw=f.offsetWidth;_6.grid.textSizeChanged();}};window.setInterval(job,_57||200);_6.grid.initTextSizePoll=_6.grid.nop;},textSizeChanged:function(){}});_6.grid.jobs={cancel:function(_5b){if(_5b){window.clearTimeout(_5b);}},jobs:[],job:function(_5c,_5d,_5e){_6.grid.jobs.cancelJob(_5c);var job=function(){delete _6.grid.jobs.jobs[_5c];_5e();};_6.grid.jobs.jobs[_5c]=setTimeout(job,_5d);},cancelJob:function(_60){_6.grid.jobs.cancel(_6.grid.jobs.jobs[_60]);}};}}};});