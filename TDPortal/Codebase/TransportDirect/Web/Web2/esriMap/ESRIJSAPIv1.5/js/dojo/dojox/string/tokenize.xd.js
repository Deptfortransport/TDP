/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.string.tokenize"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.string.tokenize"]){_4._hasResource["dojox.string.tokenize"]=true;_4.provide("dojox.string.tokenize");_6.string.tokenize=function(_7,re,_9,_a){var _b=[];var _c,_d,_e=0;while(_c=re.exec(_7)){_d=_7.slice(_e,re.lastIndex-_c[0].length);if(_d.length){_b.push(_d);}if(_9){if(_4.isOpera){var _f=_c.slice(0);while(_f.length<_c.length){_f.push(null);}_c=_f;}var _10=_9.apply(_a,_c.slice(1).concat(_b.length));if(typeof _10!="undefined"){_b.push(_10);}}_e=re.lastIndex;}_d=_7.slice(_e);if(_d.length){_b.push(_d);}return _b;};}}};});