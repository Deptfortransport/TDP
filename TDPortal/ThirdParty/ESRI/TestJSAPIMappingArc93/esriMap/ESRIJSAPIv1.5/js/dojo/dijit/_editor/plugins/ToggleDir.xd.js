/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit._editor.plugins.ToggleDir"],["require","dijit._editor._Plugin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit._editor.plugins.ToggleDir"]){_4._hasResource["dijit._editor.plugins.ToggleDir"]=true;_4.provide("dijit._editor.plugins.ToggleDir");_4.experimental("dijit._editor.plugins.ToggleDir");_4.require("dijit._editor._Plugin");_4.declare("dijit._editor.plugins.ToggleDir",_5._editor._Plugin,{useDefaultCommand:false,command:"toggleDir",_initButton:function(){this.inherited("_initButton",arguments);this.connect(this.button,"onClick",this._toggleDir);},updateState:function(){},_toggleDir:function(){var _7=this.editor.editorObject.contentWindow.document.documentElement;var _8=_4.getComputedStyle(_7).direction=="ltr";_7.dir=_8?"rtl":"ltr";}});_4.subscribe(_5._scopeName+".Editor.getPlugin",null,function(o){if(o.plugin){return;}switch(o.args.name){case "toggleDir":o.plugin=new _5._editor.plugins.ToggleDir({command:o.args.name});}});}}};});