/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.collections.Dictionary"],["require","dojox.collections._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.collections.Dictionary"]){_4._hasResource["dojox.collections.Dictionary"]=true;_4.provide("dojox.collections.Dictionary");_4.require("dojox.collections._base");_6.collections.Dictionary=function(_7){var _8={};this.count=0;var _9={};this.add=function(k,v){var b=(k in _8);_8[k]=new _6.collections.DictionaryEntry(k,v);if(!b){this.count++;}};this.clear=function(){_8={};this.count=0;};this.clone=function(){return new _6.collections.Dictionary(this);};this.contains=this.containsKey=function(k){if(_9[k]){return false;}return (_8[k]!=null);};this.containsValue=function(v){var e=this.getIterator();while(e.get()){if(e.element.value==v){return true;}}return false;};this.entry=function(k){return _8[k];};this.forEach=function(fn,_12){var a=[];for(var p in _8){if(!_9[p]){a.push(_8[p]);}}_4.forEach(a,fn,_12);};this.getKeyList=function(){return (this.getIterator()).map(function(_15){return _15.key;});};this.getValueList=function(){return (this.getIterator()).map(function(_16){return _16.value;});};this.item=function(k){if(k in _8){return _8[k].valueOf();}return undefined;};this.getIterator=function(){return new _6.collections.DictionaryIterator(_8);};this.remove=function(k){if(k in _8&&!_9[k]){delete _8[k];this.count--;return true;}return false;};if(_7){var e=_7.getIterator();while(e.get()){this.add(e.element.key,e.element.value);}}};}}};});