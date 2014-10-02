/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.string"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.string"]){_4._hasResource["dojo.string"]=true;_4.provide("dojo.string");_4.string.rep=function(_7,_8){if(_8<=0||!_7){return "";}var _9=[];for(;;){if(_8&1){_9.push(_7);}if(!(_8>>=1)){break;}_7+=_7;}return _9.join("");};_4.string.pad=function(_a,_b,ch,_d){if(!ch){ch="0";}var _e=String(_a),_f=_4.string.rep(ch,Math.ceil((_b-_e.length)/ch.length));return _d?_e+_f:_f+_e;};_4.string.substitute=function(_10,map,_12,_13){_13=_13||_4.global;_12=(!_12)?function(v){return v;}:_4.hitch(_13,_12);return _10.replace(/\$\{([^\s\:\}]+)(?:\:([^\s\:\}]+))?\}/g,function(_15,key,_17){var _18=_4.getObject(key,false,map);if(_17){_18=_4.getObject(_17,false,_13).call(_13,_18,key);}return _12(_18,key).toString();});};_4.string.trim=String.prototype.trim?_4.trim:function(str){str=str.replace(/^\s+/,"");for(var i=str.length-1;i>=0;i--){if(/\S/.test(str.charAt(i))){str=str.substring(0,i+1);break;}}return str;};}}};});