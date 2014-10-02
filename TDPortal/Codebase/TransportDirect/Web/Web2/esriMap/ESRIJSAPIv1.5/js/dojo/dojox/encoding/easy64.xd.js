/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.encoding.easy64"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.encoding.easy64"]){_4._hasResource["dojox.encoding.easy64"]=true;_4.provide("dojox.encoding.easy64");(function(){var c=function(_8,_9,_a){for(var i=0;i<_9;i+=3){_a.push(String.fromCharCode((_8[i]>>>2)+33),String.fromCharCode(((_8[i]&3)<<4)+(_8[i+1]>>>4)+33),String.fromCharCode(((_8[i+1]&15)<<2)+(_8[i+2]>>>6)+33),String.fromCharCode((_8[i+2]&63)+33));}};_6.encoding.easy64.encode=function(_c){var _d=[],_e=_c.length%3,_f=_c.length-_e;c(_c,_f,_d);if(_e){var t=_c.slice(_f);while(t.length<3){t.push(0);}c(t,3,_d);for(var i=3;i>_e;_d.pop(),--i){}}return _d.join("");};_6.encoding.easy64.decode=function(_12){var n=_12.length,r=[],b=[0,0,0,0],i,j,d;for(i=0;i<n;i+=4){for(j=0;j<4;++j){b[j]=_12.charCodeAt(i+j)-33;}d=n-i;for(j=d;j<4;b[++j]=0){}r.push((b[0]<<2)+(b[1]>>>4),((b[1]&15)<<4)+(b[2]>>>2),((b[2]&3)<<6)+b[3]);for(j=d;j<4;++j,r.pop()){}}return r;};})();}}};});