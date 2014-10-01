/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.encoding.ascii85"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.encoding.ascii85"]){_4._hasResource["dojox.encoding.ascii85"]=true;_4.provide("dojox.encoding.ascii85");(function(){var c=function(_8,_9,_a){var i,j,n,b=[0,0,0,0,0];for(i=0;i<_9;i+=4){n=((_8[i]*256+_8[i+1])*256+_8[i+2])*256+_8[i+3];if(!n){_a.push("z");}else{for(j=0;j<5;b[j++]=n%85+33,n=Math.floor(n/85)){}}_a.push(String.fromCharCode(b[4],b[3],b[2],b[1],b[0]));}};_6.encoding.ascii85.encode=function(_f){var _10=[],_11=_f.length%4,_12=_f.length-_11;c(_f,_12,_10);if(_11){var t=_f.slice(_12);while(t.length<4){t.push(0);}c(t,4,_10);var x=_10.pop();if(x=="z"){x="!!!!!";}_10.push(x.substr(0,_11+1));}return _10.join("");};_6.encoding.ascii85.decode=function(_15){var n=_15.length,r=[],b=[0,0,0,0,0],i,j,t,x,y,d;for(i=0;i<n;++i){if(_15.charAt(i)=="z"){r.push(0,0,0,0);continue;}for(j=0;j<5;++j){b[j]=_15.charCodeAt(i+j)-33;}d=n-i;if(d<5){for(j=d;j<4;b[++j]=0){}b[d]=85;}t=(((b[0]*85+b[1])*85+b[2])*85+b[3])*85+b[4];x=t&255;t>>>=8;y=t&255;t>>>=8;r.push(t>>>8,t&255,y,x);for(j=d;j<5;++j,r.pop()){}i+=4;}return r;};})();}}};});