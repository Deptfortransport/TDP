/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.grid.compat._grid.builder"],["require","dojox.grid.compat._grid.drag"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.grid.compat._grid.builder"]){_4._hasResource["dojox.grid.compat._grid.builder"]=true;_4.provide("dojox.grid.compat._grid.builder");_4.require("dojox.grid.compat._grid.drag");_4.declare("dojox.grid.Builder",null,{constructor:function(_7){this.view=_7;this.grid=_7.grid;},view:null,_table:"<table class=\"dojoxGrid-row-table\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" role=\"wairole:presentation\">",generateCellMarkup:function(_8,_9,_a,_b){var _c=[],_d;if(_b){_d=["<th tabIndex=\"-1\" role=\"wairole:columnheader\""];}else{_d=["<td tabIndex=\"-1\" role=\"wairole:gridcell\""];}_8.colSpan&&_d.push(" colspan=\"",_8.colSpan,"\"");_8.rowSpan&&_d.push(" rowspan=\"",_8.rowSpan,"\"");_d.push(" class=\"dojoxGrid-cell ");_8.classes&&_d.push(_8.classes," ");_a&&_d.push(_a," ");_c.push(_d.join(""));_c.push("");_d=["\" idx=\"",_8.index,"\" style=\""];_d.push(_8.styles,_9||"");_8.unitWidth&&_d.push("width:",_8.unitWidth,";");_c.push(_d.join(""));_c.push("");_d=["\""];_8.attrs&&_d.push(" ",_8.attrs);_d.push(">");_c.push(_d.join(""));_c.push("");_c.push("</td>");return _c;},isCellNode:function(_e){return Boolean(_e&&_e.getAttribute&&_e.getAttribute("idx"));},getCellNodeIndex:function(_f){return _f?Number(_f.getAttribute("idx")):-1;},getCellNode:function(_10,_11){for(var i=0,row;row=_6.grid.getTr(_10.firstChild,i);i++){for(var j=0,_15;_15=row.cells[j];j++){if(this.getCellNodeIndex(_15)==_11){return _15;}}}},findCellTarget:function(_16,_17){var n=_16;while(n&&(!this.isCellNode(n)||(_6.grid.gridViewTag in n.offsetParent.parentNode&&n.offsetParent.parentNode[_6.grid.gridViewTag]!=this.view.id))&&(n!=_17)){n=n.parentNode;}return n!=_17?n:null;},baseDecorateEvent:function(e){e.dispatch="do"+e.type;e.grid=this.grid;e.sourceView=this.view;e.cellNode=this.findCellTarget(e.target,e.rowNode);e.cellIndex=this.getCellNodeIndex(e.cellNode);e.cell=(e.cellIndex>=0?this.grid.getCell(e.cellIndex):null);},findTarget:function(_1a,_1b){var n=_1a;while(n&&(n!=this.domNode)&&(!(_1b in n)||(_6.grid.gridViewTag in n&&n[_6.grid.gridViewTag]!=this.view.id))){n=n.parentNode;}return (n!=this.domNode)?n:null;},findRowTarget:function(_1d){return this.findTarget(_1d,_6.grid.rowIndexTag);},isIntraNodeEvent:function(e){try{return (e.cellNode&&e.relatedTarget&&_4.isDescendant(e.relatedTarget,e.cellNode));}catch(x){return false;}},isIntraRowEvent:function(e){try{var row=e.relatedTarget&&this.findRowTarget(e.relatedTarget);return !row&&(e.rowIndex==-1)||row&&(e.rowIndex==row.gridRowIndex);}catch(x){return false;}},dispatchEvent:function(e){if(e.dispatch in this){return this[e.dispatch](e);}},domouseover:function(e){if(e.cellNode&&(e.cellNode!=this.lastOverCellNode)){this.lastOverCellNode=e.cellNode;this.grid.onMouseOver(e);}this.grid.onMouseOverRow(e);},domouseout:function(e){if(e.cellNode&&(e.cellNode==this.lastOverCellNode)&&!this.isIntraNodeEvent(e,this.lastOverCellNode)){this.lastOverCellNode=null;this.grid.onMouseOut(e);if(!this.isIntraRowEvent(e)){this.grid.onMouseOutRow(e);}}},domousedown:function(e){if(e.cellNode){this.grid.onMouseDown(e);}this.grid.onMouseDownRow(e);}});_4.declare("dojox.grid.contentBuilder",_6.grid.Builder,{update:function(){this.prepareHtml();},prepareHtml:function(){var _25=this.grid.get,_26=this.view.structure.rows;for(var j=0,row;(row=_26[j]);j++){for(var i=0,_2a;(_2a=row[i]);i++){_2a.get=_2a.get||(_2a.value==undefined)&&_25;_2a.markup=this.generateCellMarkup(_2a,_2a.cellStyles,_2a.cellClasses,false);}}},generateHtml:function(_2b,_2c){var _2d=[this._table],v=this.view,obr=v.onBeforeRow,_30=v.structure.rows;obr&&obr(_2c,_30);for(var j=0,row;(row=_30[j]);j++){if(row.hidden||row.header){continue;}_2d.push(!row.invisible?"<tr>":"<tr class=\"dojoxGrid-invisible\">");for(var i=0,_34,m,cc,cs;(_34=row[i]);i++){m=_34.markup,cc=_34.customClasses=[],cs=_34.customStyles=[];m[5]=_34.format(_2b);m[1]=cc.join(" ");m[3]=cs.join(";");_2d.push.apply(_2d,m);}_2d.push("</tr>");}_2d.push("</table>");return _2d.join("");},decorateEvent:function(e){e.rowNode=this.findRowTarget(e.target);if(!e.rowNode){return false;}e.rowIndex=e.rowNode[_6.grid.rowIndexTag];this.baseDecorateEvent(e);e.cell=this.grid.getCell(e.cellIndex);return true;}});_4.declare("dojox.grid.headerBuilder",_6.grid.Builder,{bogusClickTime:0,overResizeWidth:4,minColWidth:1,_table:"<table class=\"dojoxGrid-row-table\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" role=\"wairole:presentation\"",update:function(){this.tableMap=new _6.grid.tableMap(this.view.structure.rows);},generateHtml:function(_39,_3a){var _3b=[this._table],_3c=this.view.structure.rows;if(this.view.viewWidth){_3b.push([" style=\"width:",this.view.viewWidth,";\""].join(""));}_3b.push(">");_6.grid.fire(this.view,"onBeforeRow",[-1,_3c]);for(var j=0,row;(row=_3c[j]);j++){if(row.hidden){continue;}_3b.push(!row.invisible?"<tr>":"<tr class=\"dojoxGrid-invisible\">");for(var i=0,_40,_41;(_40=row[i]);i++){_40.customClasses=[];_40.customStyles=[];_41=this.generateCellMarkup(_40,_40.headerStyles,_40.headerClasses,true);_41[5]=(_3a!=undefined?_3a:_39(_40));_41[3]=_40.customStyles.join(";");_41[1]=_40.customClasses.join(" ");_3b.push(_41.join(""));}_3b.push("</tr>");}_3b.push("</table>");return _3b.join("");},getCellX:function(e){var x=e.layerX;if(_4.isMoz){var n=_6.grid.ascendDom(e.target,_6.grid.makeNotTagName("th"));x-=(n&&n.offsetLeft)||0;var t=e.sourceView.getScrollbarWidth();if(!_4._isBodyLtr()&&e.sourceView.headerNode.scrollLeft<t){x-=t;}}var n=_6.grid.ascendDom(e.target,function(){if(!n||n==e.cellNode){return false;}x+=(n.offsetLeft<0?0:n.offsetLeft);return true;});return x;},decorateEvent:function(e){this.baseDecorateEvent(e);e.rowIndex=-1;e.cellX=this.getCellX(e);return true;},prepareResize:function(e,mod){var i=_6.grid.getTdIndex(e.cellNode);e.cellNode=(i?e.cellNode.parentNode.cells[i+mod]:null);e.cellIndex=(e.cellNode?this.getCellNodeIndex(e.cellNode):-1);return Boolean(e.cellNode);},canResize:function(e){if(!e.cellNode||e.cellNode.colSpan>1){return false;}var _4b=this.grid.getCell(e.cellIndex);return !_4b.noresize&&!_4b.isFlex();},overLeftResizeArea:function(e){if(_4._isBodyLtr()){return (e.cellIndex>0)&&(e.cellX<this.overResizeWidth)&&this.prepareResize(e,-1);}var t=e.cellNode&&(e.cellX<this.overResizeWidth);return;},overRightResizeArea:function(e){if(_4._isBodyLtr()){return e.cellNode&&(e.cellX>=e.cellNode.offsetWidth-this.overResizeWidth);}return (e.cellIndex>0)&&(e.cellX>=e.cellNode.offsetWidth-this.overResizeWidth)&&this.prepareResize(e,-1);},domousemove:function(e){var c=(this.overRightResizeArea(e)?"e-resize":(this.overLeftResizeArea(e)?"w-resize":""));if(c&&!this.canResize(e)){c="not-allowed";}e.sourceView.headerNode.style.cursor=c||"";if(c){_4.stopEvent(e);}},domousedown:function(e){if(!_6.grid.drag.dragging){if((this.overRightResizeArea(e)||this.overLeftResizeArea(e))&&this.canResize(e)){this.beginColumnResize(e);}else{this.grid.onMouseDown(e);this.grid.onMouseOverRow(e);}}},doclick:function(e){if(new Date().getTime()<this.bogusClickTime){_4.stopEvent(e);return true;}},beginColumnResize:function(e){_4.stopEvent(e);var _54=[],_55=this.tableMap.findOverlappingNodes(e.cellNode);for(var i=0,_57;(_57=_55[i]);i++){_54.push({node:_57,index:this.getCellNodeIndex(_57),width:_57.offsetWidth});}var _58={scrollLeft:e.sourceView.headerNode.scrollLeft,view:e.sourceView,node:e.cellNode,index:e.cellIndex,w:e.cellNode.clientWidth,spanners:_54};_6.grid.drag.start(e.cellNode,_4.hitch(this,"doResizeColumn",_58),_4.hitch(this,"endResizeColumn",_58),e);},doResizeColumn:function(_59,_5a){var _5b=_4._isBodyLtr();if(_5b){var w=_59.w+_5a.deltaX;}else{var w=_59.w-_5a.deltaX;}if(w>=this.minColWidth){for(var i=0,s,sw;(s=_59.spanners[i]);i++){if(_5b){sw=s.width+_5a.deltaX;}else{sw=s.width-_5a.deltaX;}s.node.style.width=sw+"px";_59.view.setColWidth(s.index,sw);}_59.node.style.width=w+"px";_59.view.setColWidth(_59.index,w);if(!_5b){_59.view.headerNode.scrollLeft=(_59.scrollLeft-_5a.deltaX);}}if(_59.view.flexCells&&!_59.view.testFlexCells()){var t=_6.grid.findTable(_59.node);t&&(t.style.width="");}},endResizeColumn:function(_61){this.bogusClickTime=new Date().getTime()+30;setTimeout(_4.hitch(_61.view,"update"),50);}});_4.declare("dojox.grid.tableMap",null,{constructor:function(_62){this.mapRows(_62);},map:null,mapRows:function(_63){var _64=_63.length;if(!_64){return;}this.map=[];for(var j=0,row;(row=_63[j]);j++){this.map[j]=[];}for(var j=0,row;(row=_63[j]);j++){for(var i=0,x=0,_69,_6a,_6b;(_69=row[i]);i++){while(this.map[j][x]){x++;}this.map[j][x]={c:i,r:j};_6b=_69.rowSpan||1;_6a=_69.colSpan||1;for(var y=0;y<_6b;y++){for(var s=0;s<_6a;s++){this.map[j+y][x+s]=this.map[j][x];}}x+=_6a;}}},dumpMap:function(){for(var j=0,row,h="";(row=this.map[j]);j++,h=""){for(var i=0,_72;(_72=row[i]);i++){h+=_72.r+","+_72.c+"   ";}console.log(h);}},getMapCoords:function(_73,_74){for(var j=0,row;(row=this.map[j]);j++){for(var i=0,_78;(_78=row[i]);i++){if(_78.c==_74&&_78.r==_73){return {j:j,i:i};}}}return {j:-1,i:-1};},getNode:function(_79,_7a,_7b){var row=_79&&_79.rows[_7a];return row&&row.cells[_7b];},_findOverlappingNodes:function(_7d,_7e,_7f){var _80=[];var m=this.getMapCoords(_7e,_7f);var row=this.map[m.j];for(var j=0,row;(row=this.map[j]);j++){if(j==m.j){continue;}with(row[m.i]){var n=this.getNode(_7d,r,c);if(n){_80.push(n);}}}return _80;},findOverlappingNodes:function(_85){return this._findOverlappingNodes(_6.grid.findTable(_85),_6.grid.getTrIndex(_85.parentNode),_6.grid.getTdIndex(_85));}});_6.grid.rowIndexTag="gridRowIndex";_6.grid.gridViewTag="gridView";}}};});