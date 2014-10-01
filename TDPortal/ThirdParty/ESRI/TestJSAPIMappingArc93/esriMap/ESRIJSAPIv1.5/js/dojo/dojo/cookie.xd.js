/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.cookie"],["require","dojo.regexp"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.cookie"]){_4._hasResource["dojo.cookie"]=true;_4.provide("dojo.cookie");_4.require("dojo.regexp");_4.cookie=function(_7,_8,_9){var c=document.cookie;if(arguments.length==1){var _b=c.match(new RegExp("(?:^|; )"+_4.regexp.escapeString(_7)+"=([^;]*)"));return _b?decodeURIComponent(_b[1]):undefined;}else{_9=_9||{};var _c=_9.expires;if(typeof _c=="number"){var d=new Date();d.setTime(d.getTime()+_c*24*60*60*1000);_c=_9.expires=d;}if(_c&&_c.toUTCString){_9.expires=_c.toUTCString();}_8=encodeURIComponent(_8);var _e=_7+"="+_8,_f;for(_f in _9){_e+="; "+_f;var _10=_9[_f];if(_10!==true){_e+="="+_10;}}document.cookie=_e;}};_4.cookie.isSupported=function(){if(!("cookieEnabled" in navigator)){this("__djCookieTest__","CookiesAllowed");navigator.cookieEnabled=this("__djCookieTest__")=="CookiesAllowed";if(navigator.cookieEnabled){this("__djCookieTest__","",{expires:-1});}}return navigator.cookieEnabled;};}}};});