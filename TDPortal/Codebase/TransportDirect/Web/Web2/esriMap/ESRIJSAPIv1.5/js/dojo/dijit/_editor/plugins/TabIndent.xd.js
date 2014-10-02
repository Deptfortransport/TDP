/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._editor.plugins.TabIndent"],["require","dijit._editor._Plugin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._editor.plugins.TabIndent"]){_4._hasResource["dijit._editor.plugins.TabIndent"]=true;_4.provide("dijit._editor.plugins.TabIndent");_4.experimental("dijit._editor.plugins.TabIndent");_4.require("dijit._editor._Plugin");_4.declare("dijit._editor.plugins.TabIndent",_5._editor._Plugin,{useDefaultCommand:false,buttonClass:_5.form.ToggleButton,command:"tabIndent",_initButton:function(){this.inherited("_initButton",arguments);this.connect(this.button,"onClick",this._tabIndent);},updateState:function(){var _e=this.editor;var _c=this.command;if(!_e){return;}if(!_e.isLoaded){return;}if(!_c.length){return;}if(this.button){try{var _9=_e.isTabIndent;if(typeof this.button.checked=="boolean"){this.button.attr("checked",_9);}}catch(e){console.debug(e);}}},_tabIndent:function(){this.editor.isTabIndent=!this.editor.isTabIndent;}});_4.subscribe(_5._scopeName+".Editor.getPlugin",null,function(o){if(o.plugin){return;}switch(o.args.name){case "tabIndent":o.plugin=new _5._editor.plugins.TabIndent({command:o.args.name});}});}}};});