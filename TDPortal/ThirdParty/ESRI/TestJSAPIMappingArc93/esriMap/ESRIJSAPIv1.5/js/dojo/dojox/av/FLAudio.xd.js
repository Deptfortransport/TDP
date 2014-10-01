/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.av.FLAudio"],["require","dijit._Widget"],["require","dojox.embed.Flash"],["require","dojox.av._Media"],["require","dojox.timing.doLater"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.av.FLAudio"]){_4._hasResource["dojox.av.FLAudio"]=true;_4.provide("dojox.av.FLAudio");_4.experimental("dojox.av.FLAudio");_4.require("dijit._Widget");_4.require("dojox.embed.Flash");_4.require("dojox.av._Media");_4.require("dojox.timing.doLater");_4.declare("dojox.av.FLAudio",null,{id:"",initialVolume:0.7,initialPan:0,isDebug:false,statusInterval:200,_swfPath:_4.moduleUrl("dojox.av","resources/audio.swf"),constructor:function(_7){_4.mixin(this,_7||{});if(!this.id){this.id="flaudio_"+new Date().getTime();}this.domNode=_4.doc.createElement("div");_4.style(this.domNode,{postion:"relative",width:"1px",height:"1px",top:"1px",left:"1px"});_4.body().appendChild(this.domNode);this.init();},init:function(){this._subs=[];this.initialVolume=this._normalizeVolume(this.initialVolume);var _8={path:this._swfPath.uri,width:"1px",height:"1px",minimumVersion:9,expressInstall:true,params:{wmode:"transparent"},vars:{id:this.id,autoPlay:this.autoPlay,initialVolume:this.initialVolume,initialPan:this.initialPan,statusInterval:this.statusInterval,isDebug:this.isDebug}};this._sub("mediaError","onError");this._sub("filesProgress","onLoadStatus");this._sub("filesAllLoaded","onAllLoaded");this._sub("mediaPosition","onPlayStatus");this._sub("mediaMeta","onID3");this._flashObject=new _6.embed.Flash(_8,this.domNode);this._flashObject.onError=function(_9){console.warn("Flash Error:",_9);alert(_9);};this._flashObject.onLoad=_4.hitch(this,function(_a){this.flashMedia=_a;this.isPlaying=this.autoPlay;this.isStopped=!this.autoPlay;this.onLoad(this.flashMedia);});},load:function(_b){if(_6.timing.doLater(this.flashMedia,this)){return false;}if(!_b.url){throw new Error("An url is required for loading media");return false;}else{_b.url=this._normalizeUrl(_b.url);}this.flashMedia.load(_b);return _b.url;},doPlay:function(_c){this.flashMedia.doPlay(_c);},pause:function(_d){this.flashMedia.pause(_d);},stop:function(_e){this.flashMedia.doStop(_e);},setVolume:function(_f){this.flashMedia.setVolume(_f);},setPan:function(_10){this.flashMedia.setPan(_10);},getVolume:function(_11){return this.flashMedia.getVolume(_11);},getPan:function(_12){return this.flashMedia.getPan(_12);},onError:function(msg){console.warn("SWF ERROR:",msg);},onLoadStatus:function(_14){},onAllLoaded:function(){},onPlayStatus:function(_15){},onLoad:function(){},onID3:function(evt){},destroy:function(){if(!this.flashMedia){this._cons.push(_4.connect(this,"onLoad",this,"destroy"));return;}_4.forEach(this._subs,function(s){_4.unsubscribe(s);});_4.forEach(this._cons,function(c){_4.disconnect(c);});this._flashObject.destroy();},_sub:function(_19,_1a){_4.subscribe(this.id+"/"+_19,this,_1a);},_normalizeVolume:function(vol){if(vol>1){while(vol>1){vol*=0.1;}}return vol;},_normalizeUrl:function(_1c){if(_1c&&_1c.toLowerCase().indexOf("http")<0){var loc=window.location.href.split("/");loc.pop();loc=loc.join("/")+"/";_1c=loc+_1c;}return _1c;}});}}};});