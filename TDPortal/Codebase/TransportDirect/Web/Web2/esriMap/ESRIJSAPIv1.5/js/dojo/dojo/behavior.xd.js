/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.behavior"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.behavior"]){_4._hasResource["dojo.behavior"]=true;_4.provide("dojo.behavior");_4.behavior=new function(){function _7(_8,_9){if(!_8[_9]){_8[_9]=[];}return _8[_9];};var _a=0;function _b(_c,_d,_e){var _f={};for(var x in _c){if(typeof _f[x]=="undefined"){if(!_e){_d(_c[x],x);}else{_e.call(_d,_c[x],x);}}}};this._behaviors={};this.add=function(_11){var _12={};_b(_11,this,function(_13,_14){var _15=_7(this._behaviors,_14);if(typeof _15["id"]!="number"){_15.id=_a++;}var _16=[];_15.push(_16);if((_4.isString(_13))||(_4.isFunction(_13))){_13={found:_13};}_b(_13,function(_17,_18){_7(_16,_18).push(_17);});});};var _19=function(_1a,_1b,_1c){if(_4.isString(_1b)){if(_1c=="found"){_4.publish(_1b,[_1a]);}else{_4.connect(_1a,_1c,function(){_4.publish(_1b,arguments);});}}else{if(_4.isFunction(_1b)){if(_1c=="found"){_1b(_1a);}else{_4.connect(_1a,_1c,_1b);}}}};this.apply=function(){_b(this._behaviors,function(_1d,id){_4.query(id).forEach(function(_1f){var _20=0;var bid="_dj_behavior_"+_1d.id;if(typeof _1f[bid]=="number"){_20=_1f[bid];if(_20==(_1d.length)){return;}}for(var x=_20,_23;_23=_1d[x];x++){_b(_23,function(_24,_25){if(_4.isArray(_24)){_4.forEach(_24,function(_26){_19(_1f,_26,_25);});}});}_1f[bid]=_1d.length;});});};};_4.addOnLoad(_4.behavior,"apply");}}};});