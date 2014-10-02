/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.grid._EditManager"],["require","dojox.grid.util"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.grid._EditManager"]){_4._hasResource["dojox.grid._EditManager"]=true;_4.provide("dojox.grid._EditManager");_4.require("dojox.grid.util");_4.declare("dojox.grid._EditManager",null,{constructor:function(_7){this.grid=_7;this.connections=[];if(_4.isIE){this.connections.push(_4.connect(document.body,"onfocus",_4.hitch(this,"_boomerangFocus")));}},info:{},destroy:function(){_4.forEach(this.connections,_4.disconnect);},cellFocus:function(_8,_9){if(this.grid.singleClickEdit||this.isEditRow(_9)){this.setEditCell(_8,_9);}else{this.apply();}if(this.isEditing()||(_8&&_8.editable&&_8.alwaysEditing)){this._focusEditor(_8,_9);}},rowClick:function(e){if(this.isEditing()&&!this.isEditRow(e.rowIndex)){this.apply();}},styleRow:function(_b){if(_b.index==this.info.rowIndex){_b.customClasses+=" dojoxGridRowEditing";}},dispatchEvent:function(e){var c=e.cell,ed=(c&&c["editable"])?c:0;return ed&&ed.dispatchEvent(e.dispatch,e);},isEditing:function(){return this.info.rowIndex!==undefined;},isEditCell:function(_f,_10){return (this.info.rowIndex===_f)&&(this.info.cell.index==_10);},isEditRow:function(_11){return this.info.rowIndex===_11;},setEditCell:function(_12,_13){if(!this.isEditCell(_13,_12.index)&&this.grid.canEdit&&this.grid.canEdit(_12,_13)){this.start(_12,_13,this.isEditRow(_13)||_12.editable);}},_focusEditor:function(_14,_15){_6.grid.util.fire(_14,"focus",[_15]);},focusEditor:function(){if(this.isEditing()){this._focusEditor(this.info.cell,this.info.rowIndex);}},_boomerangWindow:500,_shouldCatchBoomerang:function(){return this._catchBoomerang>new Date().getTime();},_boomerangFocus:function(){if(this._shouldCatchBoomerang()){this.grid.focus.focusGrid();this.focusEditor();this._catchBoomerang=0;}},_doCatchBoomerang:function(){if(_4.isIE){this._catchBoomerang=new Date().getTime()+this._boomerangWindow;}},start:function(_16,_17,_18){this.grid.beginUpdate();this.editorApply();if(this.isEditing()&&!this.isEditRow(_17)){this.applyRowEdit();this.grid.updateRow(_17);}if(_18){this.info={cell:_16,rowIndex:_17};this.grid.doStartEdit(_16,_17);this.grid.updateRow(_17);}else{this.info={};}this.grid.endUpdate();this.grid.focus.focusGrid();this._focusEditor(_16,_17);this._doCatchBoomerang();},_editorDo:function(_19){var c=this.info.cell;c&&c.editable&&c[_19](this.info.rowIndex);},editorApply:function(){this._editorDo("apply");},editorCancel:function(){this._editorDo("cancel");},applyCellEdit:function(_1b,_1c,_1d){if(this.grid.canEdit(_1c,_1d)){this.grid.doApplyCellEdit(_1b,_1d,_1c.field);}},applyRowEdit:function(){this.grid.doApplyEdit(this.info.rowIndex,this.info.cell.field);},apply:function(){if(this.isEditing()){this.grid.beginUpdate();this.editorApply();this.applyRowEdit();this.info={};this.grid.endUpdate();this.grid.focus.focusGrid();this._doCatchBoomerang();}},cancel:function(){if(this.isEditing()){this.grid.beginUpdate();this.editorCancel();this.info={};this.grid.endUpdate();this.grid.focus.focusGrid();this._doCatchBoomerang();}},save:function(_1e,_1f){var c=this.info.cell;if(this.isEditRow(_1e)&&(!_1f||c.view==_1f)&&c.editable){c.save(c,this.info.rowIndex);}},restore:function(_21,_22){var c=this.info.cell;if(this.isEditRow(_22)&&c.view==_21&&c.editable){c.restore(c,this.info.rowIndex);}}});}}};});