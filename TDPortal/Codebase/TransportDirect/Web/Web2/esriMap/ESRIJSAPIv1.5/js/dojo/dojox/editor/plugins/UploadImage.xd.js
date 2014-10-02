/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.editor.plugins.UploadImage"],["require","dijit._editor._Plugin"],["require","dojox.form.FileUploader"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.editor.plugins.UploadImage"]){_4._hasResource["dojox.editor.plugins.UploadImage"]=true;_4.provide("dojox.editor.plugins.UploadImage");_4.require("dijit._editor._Plugin");_4.require("dojox.form.FileUploader");_4.experimental("dojox.editor.plugins.UploadImage");_4.declare("dojox.editor.plugins.UploadImage",_5._editor._Plugin,{tempImageUrl:"",iconClassPrefix:"editorIcon",useDefaultCommand:false,uploadUrl:"",fileInput:null,label:"Mike",_initButton:function(){this.command="uploadImage";this.editor.commands[this.command]="Upload Image";this.inherited("_initButton",arguments);delete this.command;setTimeout(_4.hitch(this,"createFileInput"),200);},createFileInput:function(){var _7=[["Jpeg File","*.jpg;*.jpeg"],["GIF File","*.gif"],["PNG File","*.png"],["All Images","*.jpg;*.jpeg;*.gif;*.png"]];console.warn("downloadPath:",this.downloadPath);this.fileInput=new _6.form.FileUploader({isDebug:true,button:this.button,uploadUrl:this.uploadUrl,uploadOnChange:true,selectMultipleFiles:false,fileMask:_7});_4.connect(this.fileInput,"onChange",this,"insertTempImage");_4.connect(this.fileInput,"onComplete",this,"onComplete");},onComplete:function(_8,_9,_a){_8=_8[0];var _b=_4.withGlobal(this.editor.window,"byId",_4,[this.currentImageId]);var _c;if(this.downloadPath){_c=this.downloadPath+_8.name;}else{_c=_8.file;}_b.src=_c;if(_8.width){_b.width=_8.width;_b.height=_8.height;}},insertTempImage:function(){this.currentImageId="img_"+(new Date().getTime());var _d="<img id=\""+this.currentImageId+"\" src=\""+this.tempImageUrl+"\" width=\"32\" height=\"32\"/>";this.editor.execCommand("inserthtml",_d);}});_4.subscribe(_5._scopeName+".Editor.getPlugin",null,function(o){if(o.plugin){return;}switch(o.args.name){case "uploadImage":o.plugin=new _6.editor.plugins.UploadImage({url:o.args.url});}});}}};});