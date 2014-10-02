/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.AdapterRegistry"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.AdapterRegistry"]){_4._hasResource["dojo.AdapterRegistry"]=true;_4.provide("dojo.AdapterRegistry");_4.AdapterRegistry=function(_7){this.pairs=[];this.returnWrappers=_7||false;};_4.extend(_4.AdapterRegistry,{register:function(_8,_9,_a,_b,_c){this.pairs[((_c)?"unshift":"push")]([_8,_9,_a,_b]);},match:function(){for(var i=0;i<this.pairs.length;i++){var _e=this.pairs[i];if(_e[1].apply(this,arguments)){if((_e[3])||(this.returnWrappers)){return _e[2];}else{return _e[2].apply(this,arguments);}}}throw new Error("No match found");},unregister:function(_f){for(var i=0;i<this.pairs.length;i++){var _11=this.pairs[i];if(_11[0]==_f){this.pairs.splice(i,1);return true;}}return false;}});}}};});