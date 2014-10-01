/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.dnd.common"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.dnd.common"]){_4._hasResource["dojo.dnd.common"]=true;_4.provide("dojo.dnd.common");_4.dnd._isMac=navigator.appVersion.indexOf("Macintosh")>=0;_4.dnd._copyKey=_4.dnd._isMac?"metaKey":"ctrlKey";_4.dnd.getCopyKeyState=function(e){return e[_4.dnd._copyKey];};_4.dnd._uniqueId=0;_4.dnd.getUniqueId=function(){var id;do{id=_4._scopeName+"Unique"+(++_4.dnd._uniqueId);}while(_4.byId(id));return id;};_4.dnd._empty={};_4.dnd.isFormElement=function(e){var t=e.target;if(t.nodeType==3){t=t.parentNode;}return " button textarea input select option ".indexOf(" "+t.tagName.toLowerCase()+" ")>=0;};_4.dnd._lmb=_4.isIE?1:0;_4.dnd._isLmbPressed=_4.isIE?function(e){return e.button&1;}:function(e){return e.button===0;};}}};});