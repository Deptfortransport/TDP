/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.flash._base"],["require","dijit._base.place"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.flash._base"]){_4._hasResource["dojox.flash._base"]=true;_4.provide("dojox.flash._base");_4.experimental("dojox.flash");_4.require("dijit._base.place");_6.flash=function(){};_6.flash={ready:false,url:null,_visible:true,_loadedListeners:[],_installingListeners:[],setSwf:function(_7,_8){this.url=_7;this._visible=true;if(_8!==null&&_8!==undefined){this._visible=_8;}this._initialize();},addLoadedListener:function(_9){this._loadedListeners.push(_9);},addInstallingListener:function(_a){this._installingListeners.push(_a);},loaded:function(){_6.flash.ready=true;if(_6.flash._loadedListeners.length){for(var i=0;i<_6.flash._loadedListeners.length;i++){_6.flash._loadedListeners[i].call(null);}}},installing:function(){if(_6.flash._installingListeners.length){for(var i=0;i<_6.flash._installingListeners.length;i++){_6.flash._installingListeners[i].call(null);}}},_initialize:function(){var _d=new _6.flash.Install();_6.flash.installer=_d;if(_d.needed()){_d.install();}else{_6.flash.obj=new _6.flash.Embed(this._visible);_6.flash.obj.write();_6.flash.comm=new _6.flash.Communicator();}}};_6.flash.Info=function(){this._detectVersion();};_6.flash.Info.prototype={version:-1,versionMajor:-1,versionMinor:-1,versionRevision:-1,capable:false,installing:false,isVersionOrAbove:function(_e,_f,_10){_10=parseFloat("."+_10);if(this.versionMajor>=_e&&this.versionMinor>=_f&&this.versionRevision>=_10){return true;}else{return false;}},_detectVersion:function(){var _11;for(var _12=25;_12>0;_12--){if(_4.isIE){var axo;try{if(_12>6){axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash."+_12);}else{axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash");}if(typeof axo=="object"){if(_12==6){axo.AllowScriptAccess="always";}_11=axo.GetVariable("$version");}}catch(e){continue;}}else{_11=this._JSFlashInfo(_12);}if(_11==-1){this.capable=false;return;}else{if(_11!=0){var _14;if(_4.isIE){var _15=_11.split(" ");var _16=_15[1];_14=_16.split(",");}else{_14=_11.split(".");}this.versionMajor=_14[0];this.versionMinor=_14[1];this.versionRevision=_14[2];var _17=this.versionMajor+"."+this.versionRevision;this.version=parseFloat(_17);this.capable=true;break;}}}},_JSFlashInfo:function(_18){if(navigator.plugins!=null&&navigator.plugins.length>0){if(navigator.plugins["Shockwave Flash 2.0"]||navigator.plugins["Shockwave Flash"]){var _19=navigator.plugins["Shockwave Flash 2.0"]?" 2.0":"";var _1a=navigator.plugins["Shockwave Flash"+_19].description;var _1b=_1a.split(" ");var _1c=_1b[2].split(".");var _1d=_1c[0];var _1e=_1c[1];var _1f=(_1b[3]||_1b[4]).split("r");var _20=_1f[1]>0?_1f[1]:0;var _21=_1d+"."+_1e+"."+_20;return _21;}}return -1;}};_6.flash.Embed=function(_22){this._visible=_22;};_6.flash.Embed.prototype={width:215,height:138,id:"flashObject",_visible:true,protocol:function(){switch(window.location.protocol){case "https:":return "https";break;default:return "http";break;}},write:function(_23){var _24;var _25=_6.flash.url;var _26=_25;var _27=_25;var _28=_4.baseUrl;var _29=document.location.protocol+"//"+document.location.host;if(_23){var _2a=escape(window.location);document.title=document.title.slice(0,47)+" - Flash Player Installation";var _2b=escape(document.title);_26+="?MMredirectURL="+_2a+"&MMplayerType=ActiveX"+"&MMdoctitle="+_2b+"&baseUrl="+escape(_28)+"&xdomain="+escape(_29);_27+="?MMredirectURL="+_2a+"&MMplayerType=PlugIn"+"&baseUrl="+escape(_28)+"&xdomain="+escape(_29);}else{_26+="?cachebust="+new Date().getTime();_26+="&baseUrl="+escape(_28);_26+="&xdomain="+escape(_29);}if(_27.indexOf("?")==-1){_27+="?baseUrl="+escape(_28);}else{_27+="&baseUrl="+escape(_28);}_27+="&xdomain="+escape(_29);_24="<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" "+"codebase=\""+this.protocol()+"://fpdownload.macromedia.com/pub/shockwave/cabs/flash/"+"swflash.cab#version=8,0,0,0\"\n "+"width=\""+this.width+"\"\n "+"height=\""+this.height+"\"\n "+"id=\""+this.id+"\"\n "+"name=\""+this.id+"\"\n "+"align=\"middle\">\n "+"<param name=\"allowScriptAccess\" value=\"always\"></param>\n "+"<param name=\"movie\" value=\""+_26+"\"></param>\n "+"<param name=\"quality\" value=\"high\"></param>\n "+"<param name=\"bgcolor\" value=\"#ffffff\"></param>\n "+"<embed src=\""+_27+"\" "+"quality=\"high\" "+"bgcolor=\"#ffffff\" "+"width=\""+this.width+"\" "+"height=\""+this.height+"\" "+"id=\""+this.id+"Embed"+"\" "+"name=\""+this.id+"\" "+"swLiveConnect=\"true\" "+"align=\"middle\" "+"allowScriptAccess=\"always\" "+"type=\"application/x-shockwave-flash\" "+"pluginspage=\""+this.protocol()+"://www.macromedia.com/go/getflashplayer\" "+"></embed>\n"+"</object>\n";_4.connect(_4,"loaded",_4.hitch(this,function(){var _2c=this.id+"Container";if(_4.byId(_2c)){return;}var div=document.createElement("div");div.id=this.id+"Container";div.style.width=this.width+"px";div.style.height=this.height+"px";if(!this._visible){div.style.position="absolute";div.style.zIndex="10000";div.style.top="-1000px";}div.innerHTML=_24;var _2e=document.getElementsByTagName("body");if(!_2e||!_2e.length){throw new Error("No body tag for this page");}_2e=_2e[0];_2e.appendChild(div);}));},get:function(){if(_4.isIE||_4.isWebKit){return _4.byId(this.id);}else{return document[this.id+"Embed"];}},setVisible:function(_2f){var _30=_4.byId(this.id+"Container");if(_2f){_30.style.position="absolute";_30.style.visibility="visible";}else{_30.style.position="absolute";_30.style.y="-1000px";_30.style.visibility="hidden";}},center:function(){var _31=this.width;var _32=this.height;var _33=_5.getViewport();var x=_33.l+(_33.w-_31)/2;var y=_33.t+(_33.h-_32)/2;var _36=_4.byId(this.id+"Container");_36.style.top=y+"px";_36.style.left=x+"px";}};_6.flash.Communicator=function(){};_6.flash.Communicator.prototype={_addExternalInterfaceCallback:function(_37){var _38=_4.hitch(this,function(){var _39=new Array(arguments.length);for(var i=0;i<arguments.length;i++){_39[i]=this._encodeData(arguments[i]);}var _3b=this._execFlash(_37,_39);_3b=this._decodeData(_3b);return _3b;});this[_37]=_38;},_encodeData:function(_3c){if(!_3c||typeof _3c!="string"){return _3c;}_3c=_3c.replace("\\","&custom_backslash;");_3c=_3c.replace(/\0/g,"&custom_null;");return _3c;},_decodeData:function(_3d){if(_3d&&_3d.length&&typeof _3d!="string"){_3d=_3d[0];}if(!_3d||typeof _3d!="string"){return _3d;}_3d=_3d.replace(/\&custom_null\;/g,"\x00");_3d=_3d.replace(/\&custom_lt\;/g,"<").replace(/\&custom_gt\;/g,">").replace(/\&custom_backslash\;/g,"\\");return _3d;},_execFlash:function(_3e,_3f){var _40=_6.flash.obj.get();_3f=(_3f)?_3f:[];for(var i=0;i<_3f;i++){if(typeof _3f[i]=="string"){_3f[i]=this._encodeData(_3f[i]);}}var _42=function(){return eval(_40.CallFunction("<invoke name=\""+_3e+"\" returntype=\"javascript\">"+__flash__argumentsToXML(_3f,0)+"</invoke>"));};var _43=_42.call(_3f);if(typeof _43=="string"){_43=this._decodeData(_43);}return _43;}};_6.flash.Install=function(){};_6.flash.Install.prototype={needed:function(){if(!_6.flash.info.capable){return true;}if(!_6.flash.info.isVersionOrAbove(8,0,0)){return true;}return false;},install:function(){var _44;_6.flash.info.installing=true;_6.flash.installing();if(_6.flash.info.capable==false){_44=new _6.flash.Embed(false);_44.write();}else{if(_6.flash.info.isVersionOrAbove(6,0,65)){_44=new _6.flash.Embed(false);_44.write(true);_44.setVisible(true);_44.center();}else{alert("This content requires a more recent version of the Macromedia "+" Flash Player.");window.location.href=+_6.flash.Embed.protocol()+"://www.macromedia.com/go/getflashplayer";}}},_onInstallStatus:function(msg){if(msg=="Download.Complete"){_6.flash._initialize();}else{if(msg=="Download.Cancelled"){alert("This content requires a more recent version of the Macromedia "+" Flash Player.");window.location.href=_6.flash.Embed.protocol()+"://www.macromedia.com/go/getflashplayer";}else{if(msg=="Download.Failed"){alert("There was an error downloading the Flash Player update. "+"Please try again later, or visit macromedia.com to download "+"the latest version of the Flash plugin.");}}}}};_6.flash.info=new _6.flash.Info();}}};});