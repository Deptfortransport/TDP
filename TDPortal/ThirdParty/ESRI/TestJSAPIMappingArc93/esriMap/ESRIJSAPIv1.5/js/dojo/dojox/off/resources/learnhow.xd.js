/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {defineResource:function(_4,_5,_6){window.onload=function(){var _7=window.location.href;var _8=_7.match(/appName=([a-z0-9 \%]*)/i);var _9="Application";if(_8&&_8.length>0){_9=decodeURIComponent(_8[1]);}var _a=document.getElementById("dot-learn-how-app-name");_a.innerHTML="";_a.appendChild(document.createTextNode(_9));_8=_7.match(/hasOfflineCache=(true|false)/);var _b=false;if(_8&&_8.length>0){_b=_8[1];_b=(_b=="true")?true:false;}if(_b==true){var _c=document.getElementById("dot-download-step");var _d=document.getElementById("dot-install-step");_c.parentNode.removeChild(_c);_d.parentNode.removeChild(_d);}_8=_7.match(/runLink=([^\&]*)\&runLinkText=([^\&]*)/);if(_8&&_8.length>0){var _e=decodeURIComponent(_8[1]);var _f=document.getElementById("dot-learn-how-run-link");_f.setAttribute("href",_e);var _10=decodeURIComponent(_8[2]);_f.innerHTML="";_f.appendChild(document.createTextNode(_10));}};}};});