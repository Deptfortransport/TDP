/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.grid.compat._grid.scroller"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.grid.compat._grid.scroller"]){_4._hasResource["dojox.grid.compat._grid.scroller"]=true;_4.provide("dojox.grid.compat._grid.scroller");_4.declare("dojox.grid.scroller.base",null,{constructor:function(){this.pageHeights=[];this.stack=[];},rowCount:0,defaultRowHeight:10,keepRows:100,contentNode:null,scrollboxNode:null,defaultPageHeight:0,keepPages:10,pageCount:0,windowHeight:0,firstVisibleRow:0,lastVisibleRow:0,page:0,pageTop:0,init:function(_7,_8,_9){switch(arguments.length){case 3:this.rowsPerPage=_9;case 2:this.keepRows=_8;case 1:this.rowCount=_7;}this.defaultPageHeight=this.defaultRowHeight*this.rowsPerPage;this.pageCount=Math.ceil(this.rowCount/this.rowsPerPage);this.setKeepInfo(this.keepRows);this.invalidate();if(this.scrollboxNode){this.scrollboxNode.scrollTop=0;this.scroll(0);this.scrollboxNode.onscroll=_4.hitch(this,"onscroll");}},setKeepInfo:function(_a){this.keepRows=_a;this.keepPages=!this.keepRows?this.keepRows:Math.max(Math.ceil(this.keepRows/this.rowsPerPage),2);},invalidate:function(){this.invalidateNodes();this.pageHeights=[];this.height=(this.pageCount?(this.pageCount-1)*this.defaultPageHeight+this.calcLastPageHeight():0);this.resize();},updateRowCount:function(_b){this.invalidateNodes();this.rowCount=_b;var _c=this.pageCount;this.pageCount=Math.ceil(this.rowCount/this.rowsPerPage);if(this.pageCount<_c){for(var i=_c-1;i>=this.pageCount;i--){this.height-=this.getPageHeight(i);delete this.pageHeights[i];}}else{if(this.pageCount>_c){this.height+=this.defaultPageHeight*(this.pageCount-_c-1)+this.calcLastPageHeight();}}this.resize();},pageExists:function(_e){},measurePage:function(_f){},positionPage:function(_10,_11){},repositionPages:function(_12){},installPage:function(_13){},preparePage:function(_14,_15,_16){},renderPage:function(_17){},removePage:function(_18){},pacify:function(_19){},pacifying:false,pacifyTicks:200,setPacifying:function(_1a){if(this.pacifying!=_1a){this.pacifying=_1a;this.pacify(this.pacifying);}},startPacify:function(){this.startPacifyTicks=new Date().getTime();},doPacify:function(){var _1b=(new Date().getTime()-this.startPacifyTicks)>this.pacifyTicks;this.setPacifying(true);this.startPacify();return _1b;},endPacify:function(){this.setPacifying(false);},resize:function(){if(this.scrollboxNode){this.windowHeight=this.scrollboxNode.clientHeight;}_6.grid.setStyleHeightPx(this.contentNode,this.height);},calcLastPageHeight:function(){if(!this.pageCount){return 0;}var _1c=this.pageCount-1;var _1d=((this.rowCount%this.rowsPerPage)||(this.rowsPerPage))*this.defaultRowHeight;this.pageHeights[_1c]=_1d;return _1d;},updateContentHeight:function(_1e){this.height+=_1e;this.resize();},updatePageHeight:function(_1f){if(this.pageExists(_1f)){var oh=this.getPageHeight(_1f);var h=(this.measurePage(_1f))||(oh);this.pageHeights[_1f]=h;if((h)&&(oh!=h)){this.updateContentHeight(h-oh);this.repositionPages(_1f);}}},rowHeightChanged:function(_22){this.updatePageHeight(Math.floor(_22/this.rowsPerPage));},invalidateNodes:function(){while(this.stack.length){this.destroyPage(this.popPage());}},createPageNode:function(){var p=document.createElement("div");p.style.position="absolute";p.style[_4._isBodyLtr()?"left":"right"]="0";return p;},getPageHeight:function(_24){var ph=this.pageHeights[_24];return (ph!==undefined?ph:this.defaultPageHeight);},pushPage:function(_26){return this.stack.push(_26);},popPage:function(){return this.stack.shift();},findPage:function(_27){var i=0,h=0;for(var ph=0;i<this.pageCount;i++,h+=ph){ph=this.getPageHeight(i);if(h+ph>=_27){break;}}this.page=i;this.pageTop=h;},buildPage:function(_2b,_2c,_2d){this.preparePage(_2b,_2c);this.positionPage(_2b,_2d);this.installPage(_2b);this.renderPage(_2b);this.pushPage(_2b);},needPage:function(_2e,_2f){var h=this.getPageHeight(_2e),oh=h;if(!this.pageExists(_2e)){this.buildPage(_2e,this.keepPages&&(this.stack.length>=this.keepPages),_2f);h=this.measurePage(_2e)||h;this.pageHeights[_2e]=h;if(h&&(oh!=h)){this.updateContentHeight(h-oh);}}else{this.positionPage(_2e,_2f);}return h;},onscroll:function(){this.scroll(this.scrollboxNode.scrollTop);},scroll:function(_32){this.startPacify();this.findPage(_32);var h=this.height;var b=this.getScrollBottom(_32);for(var p=this.page,y=this.pageTop;(p<this.pageCount)&&((b<0)||(y<b));p++){y+=this.needPage(p,y);}this.firstVisibleRow=this.getFirstVisibleRow(this.page,this.pageTop,_32);this.lastVisibleRow=this.getLastVisibleRow(p-1,y,b);if(h!=this.height){this.repositionPages(p-1);}this.endPacify();},getScrollBottom:function(_37){return (this.windowHeight>=0?_37+this.windowHeight:-1);},processNodeEvent:function(e,_39){var t=e.target;while(t&&(t!=_39)&&t.parentNode&&(t.parentNode.parentNode!=_39)){t=t.parentNode;}if(!t||!t.parentNode||(t.parentNode.parentNode!=_39)){return false;}var _3b=t.parentNode;e.topRowIndex=_3b.pageIndex*this.rowsPerPage;e.rowIndex=e.topRowIndex+_6.grid.indexInParent(t);e.rowTarget=t;return true;},processEvent:function(e){return this.processNodeEvent(e,this.contentNode);},dummy:0});_4.declare("dojox.grid.scroller",_6.grid.scroller.base,{constructor:function(){this.pageNodes=[];},renderRow:function(_3d,_3e){},removeRow:function(_3f){},getDefaultNodes:function(){return this.pageNodes;},getDefaultPageNode:function(_40){return this.getDefaultNodes()[_40];},positionPageNode:function(_41,_42){_41.style.top=_42+"px";},getPageNodePosition:function(_43){return _43.offsetTop;},repositionPageNodes:function(_44,_45){var _46=0;for(var i=0;i<this.stack.length;i++){_46=Math.max(this.stack[i],_46);}var n=_45[_44];var y=(n?this.getPageNodePosition(n)+this.getPageHeight(_44):0);for(var p=_44+1;p<=_46;p++){n=_45[p];if(n){if(this.getPageNodePosition(n)==y){return;}this.positionPage(p,y);}y+=this.getPageHeight(p);}},invalidatePageNode:function(_4b,_4c){var p=_4c[_4b];if(p){delete _4c[_4b];this.removePage(_4b,p);_6.grid.cleanNode(p);p.innerHTML="";}return p;},preparePageNode:function(_4e,_4f,_50){var p=(_4f===null?this.createPageNode():this.invalidatePageNode(_4f,_50));p.pageIndex=_4e;p.id=(this._pageIdPrefix||"")+"page-"+_4e;_50[_4e]=p;},pageExists:function(_52){return Boolean(this.getDefaultPageNode(_52));},measurePage:function(_53){var p=this.getDefaultPageNode(_53);var h=p.offsetHeight;if(!this._defaultRowHeight){if(p){this._defaultRowHeight=8;var fr=p.firstChild;if(fr){var _57=_4.doc.createTextNode("T");fr.appendChild(_57);this._defaultRowHeight=fr.offsetHeight;fr.removeChild(_57);}}}return (this.rowsPerPage==h)?(h*this._defaultRowHeight):h;},positionPage:function(_58,_59){this.positionPageNode(this.getDefaultPageNode(_58),_59);},repositionPages:function(_5a){this.repositionPageNodes(_5a,this.getDefaultNodes());},preparePage:function(_5b,_5c){this.preparePageNode(_5b,(_5c?this.popPage():null),this.getDefaultNodes());},installPage:function(_5d){this.contentNode.appendChild(this.getDefaultPageNode(_5d));},destroyPage:function(_5e){var p=this.invalidatePageNode(_5e,this.getDefaultNodes());_6.grid.removeNode(p);},renderPage:function(_60){var _61=this.pageNodes[_60];for(var i=0,j=_60*this.rowsPerPage;(i<this.rowsPerPage)&&(j<this.rowCount);i++,j++){this.renderRow(j,_61);}},removePage:function(_64){for(var i=0,j=_64*this.rowsPerPage;i<this.rowsPerPage;i++,j++){this.removeRow(j);}},getPageRow:function(_67){return _67*this.rowsPerPage;},getLastPageRow:function(_68){return Math.min(this.rowCount,this.getPageRow(_68+1))-1;},getFirstVisibleRowNodes:function(_69,_6a,_6b,_6c){var row=this.getPageRow(_69);var _6e=_6.grid.divkids(_6c[_69]);for(var i=0,l=_6e.length;i<l&&_6a<_6b;i++,row++){_6a+=_6e[i].offsetHeight;}return (row?row-1:row);},getFirstVisibleRow:function(_71,_72,_73){if(!this.pageExists(_71)){return 0;}return this.getFirstVisibleRowNodes(_71,_72,_73,this.getDefaultNodes());},getLastVisibleRowNodes:function(_74,_75,_76,_77){var row=this.getLastPageRow(_74);var _79=_6.grid.divkids(_77[_74]);for(var i=_79.length-1;i>=0&&_75>_76;i--,row--){_75-=_79[i].offsetHeight;}return row+1;},getLastVisibleRow:function(_7b,_7c,_7d){if(!this.pageExists(_7b)){return 0;}return this.getLastVisibleRowNodes(_7b,_7c,_7d,this.getDefaultNodes());},findTopRowForNodes:function(_7e,_7f){var _80=_6.grid.divkids(_7f[this.page]);for(var i=0,l=_80.length,t=this.pageTop,h;i<l;i++){h=_80[i].offsetHeight;t+=h;if(t>=_7e){this.offset=h-(t-_7e);return i+this.page*this.rowsPerPage;}}return -1;},findScrollTopForNodes:function(_85,_86){var _87=Math.floor(_85/this.rowsPerPage);var t=0;for(var i=0;i<_87;i++){t+=this.getPageHeight(i);}this.pageTop=t;this.needPage(_87,this.pageTop);var _8a=_6.grid.divkids(_86[_87]);var r=_85-this.rowsPerPage*_87;for(var i=0,l=_8a.length;i<l&&i<r;i++){t+=_8a[i].offsetHeight;}return t;},findTopRow:function(_8d){return this.findTopRowForNodes(_8d,this.getDefaultNodes());},findScrollTop:function(_8e){return this.findScrollTopForNodes(_8e,this.getDefaultNodes());},dummy:0});_4.declare("dojox.grid.scroller.columns",_6.grid.scroller,{constructor:function(_8f){this.setContentNodes(_8f);},setContentNodes:function(_90){this.contentNodes=_90;this.colCount=(this.contentNodes?this.contentNodes.length:0);this.pageNodes=[];for(var i=0;i<this.colCount;i++){this.pageNodes[i]=[];}},getDefaultNodes:function(){return this.pageNodes[0]||[];},scroll:function(_92){if(this.colCount){_6.grid.scroller.prototype.scroll.call(this,_92);}},resize:function(){if(this.scrollboxNode){this.windowHeight=this.scrollboxNode.clientHeight;}for(var i=0;i<this.colCount;i++){_6.grid.setStyleHeightPx(this.contentNodes[i],this.height);}},positionPage:function(_94,_95){for(var i=0;i<this.colCount;i++){this.positionPageNode(this.pageNodes[i][_94],_95);}},preparePage:function(_97,_98){var p=(_98?this.popPage():null);for(var i=0;i<this.colCount;i++){this.preparePageNode(_97,p,this.pageNodes[i]);}},installPage:function(_9b){for(var i=0;i<this.colCount;i++){this.contentNodes[i].appendChild(this.pageNodes[i][_9b]);}},destroyPage:function(_9d){for(var i=0;i<this.colCount;i++){_6.grid.removeNode(this.invalidatePageNode(_9d,this.pageNodes[i]));}},renderPage:function(_9f){var _a0=[];for(var i=0;i<this.colCount;i++){_a0[i]=this.pageNodes[i][_9f];}for(var i=0,j=_9f*this.rowsPerPage;(i<this.rowsPerPage)&&(j<this.rowCount);i++,j++){this.renderRow(j,_a0);}}});}}};});