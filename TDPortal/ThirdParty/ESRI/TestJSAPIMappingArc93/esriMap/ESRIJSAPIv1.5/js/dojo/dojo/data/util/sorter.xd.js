/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.data.util.sorter"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.data.util.sorter"]){_4._hasResource["dojo.data.util.sorter"]=true;_4.provide("dojo.data.util.sorter");_4.data.util.sorter.basicComparator=function(a,b){var r=-1;if(a===null){a=undefined;}if(b===null){b=undefined;}if(a==b){r=0;}else{if(a>b||a==null){r=1;}}return r;};_4.data.util.sorter.createSortFunction=function(_a,_b){var _c=[];function _d(_e,_f){return function(_10,_11){var a=_b.getValue(_10,_e);var b=_b.getValue(_11,_e);var _14=null;if(_b.comparatorMap){if(typeof _e!=="string"){_e=_b.getIdentity(_e);}_14=_b.comparatorMap[_e]||_4.data.util.sorter.basicComparator;}_14=_14||_4.data.util.sorter.basicComparator;return _f*_14(a,b);};};var _15;for(var i=0;i<_a.length;i++){_15=_a[i];if(_15.attribute){var _17=(_15.descending)?-1:1;_c.push(_d(_15.attribute,_17));}}return function(_18,_19){var i=0;while(i<_c.length){var ret=_c[i++](_18,_19);if(ret!==0){return ret;}}return 0;};};}}};});