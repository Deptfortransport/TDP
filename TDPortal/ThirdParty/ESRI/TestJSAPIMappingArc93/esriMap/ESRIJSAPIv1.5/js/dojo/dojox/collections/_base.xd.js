/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.collections._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.collections._base"]){_4._hasResource["dojox.collections._base"]=true;_4.provide("dojox.collections._base");_6.collections.DictionaryEntry=function(k,v){this.key=k;this.value=v;this.valueOf=function(){return this.value;};this.toString=function(){return String(this.value);};};_6.collections.Iterator=function(_9){var a=_9;var _b=0;this.element=a[_b]||null;this.atEnd=function(){return (_b>=a.length);};this.get=function(){if(this.atEnd()){return null;}this.element=a[_b++];return this.element;};this.map=function(fn,_d){return _4.map(a,fn,_d);};this.reset=function(){_b=0;this.element=a[_b];};};_6.collections.DictionaryIterator=function(_e){var a=[];var _10={};for(var p in _e){if(!_10[p]){a.push(_e[p]);}}var _12=0;this.element=a[_12]||null;this.atEnd=function(){return (_12>=a.length);};this.get=function(){if(this.atEnd()){return null;}this.element=a[_12++];return this.element;};this.map=function(fn,_14){return _4.map(a,fn,_14);};this.reset=function(){_12=0;this.element=a[_12];};};}}};});