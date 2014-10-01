/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.encoding.digests.SHA1"],["require","dojox.encoding.digests._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.encoding.digests.SHA1"]){_4._hasResource["dojox.encoding.digests.SHA1"]=true;_4.provide("dojox.encoding.digests.SHA1");_4.require("dojox.encoding.digests._base");(function(){var _7=_6.encoding.digests;var _8=8,_9=(1<<_8)-1;function R(n,c){return (n<<c)|(n>>>(32-c));};function FT(t,b,c,d){if(t<20){return (b&c)|((~b)&d);}if(t<40){return b^c^d;}if(t<60){return (b&c)|(b&d)|(c&d);}return b^c^d;};function KT(t){return (t<20)?1518500249:(t<40)?1859775393:(t<60)?-1894007588:-899497514;};function _14(x,len){x[len>>5]|=128<<(24-len%32);x[((len+64>>9)<<4)+15]=len;var w=new Array(80),a=1732584193,b=-271733879,c=-1732584194,d=271733878,e=-1009589776;for(var i=0;i<x.length;i+=16){var _1e=a,_1f=b,_20=c,_21=d,_22=e;for(var j=0;j<80;j++){if(j<16){w[j]=x[i+j];}else{w[j]=R(w[j-3]^w[j-8]^w[j-14]^w[j-16],1);}var t=_7.addWords(_7.addWords(R(a,5),FT(j,b,c,d)),_7.addWords(_7.addWords(e,w[j]),KT(j)));e=d;d=c;c=R(b,30);b=a;a=t;}a=_7.addWords(a,_1e);b=_7.addWords(b,_1f);c=_7.addWords(c,_20);d=_7.addWords(d,_21);e=_7.addWords(e,_22);}return [a,b,c,d,e];};function _25(_26,key){var wa=_29(key);if(wa.length>16){wa=_14(wa,key.length*_8);}var _2a=new Array(16),_2b=new Array(16);for(var i=0;i<16;i++){_2a[i]=wa[i]^909522486;_2b[i]=wa[i]^1549556828;}var _2d=_14(_2a.concat(_29(_26)),512+_26.length*_8);return _14(_2b.concat(_2d),512+160);};function _29(s){var wa=[];for(var i=0,l=s.length*_8;i<l;i+=_8){wa[i>>5]|=(s.charCodeAt(i/_8)&_9)<<(32-_8-i%32);}return wa;};function _32(wa){var h="0123456789abcdef",s=[];for(var i=0,l=wa.length*4;i<l;i++){s.push(h.charAt((wa[i>>2]>>((3-i%4)*8+4))&15),h.charAt((wa[i>>2]>>((3-i%4)*8))&15));}return s.join("");};function _38(wa){var s=[];for(var i=0,l=wa.length*32;i<l;i+=_8){s.push(String.fromCharCode((wa[i>>5]>>>(32-_8-i%32))&_9));}return s.join("");};function _3d(wa){var p="=",tab="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",s=[];for(var i=0,l=wa.length*4;i<l;i+=3){var t=(((wa[i>>2]>>8*(3-i%4))&255)<<16)|(((wa[i+1>>2]>>8*(3-(i+1)%4))&255)<<8)|((wa[i+2>>2]>>8*(3-(i+2)%4))&255);for(var j=0;j<4;j++){if(i*8+j*6>wa.length*32){s.push(p);}else{s.push(tab.charAt((t>>6*(3-j))&63));}}}return s.join("");};_7.SHA1=function(_46,_47){var out=_47||_7.outputTypes.Base64;var wa=_14(_29(_46),_46.length*_8);switch(out){case _7.outputTypes.Raw:return wa;case _7.outputTypes.Hex:return _32(wa);case _7.outputTypes.String:return _38(wa);default:return _3d(wa);}};_7.SHA1._hmac=function(_4a,key,_4c){var out=_4c||_7.outputTypes.Base64;var wa=_25(_4a,key);switch(out){case _7.outputTypes.Raw:return wa;case _7.outputTypes.Hex:return _32(wa);case _7.outputTypes.String:return _38(wa);default:return _3d(wa);}};})();}}};});