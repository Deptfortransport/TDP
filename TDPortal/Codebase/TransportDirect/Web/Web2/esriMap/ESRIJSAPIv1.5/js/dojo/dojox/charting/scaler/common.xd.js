/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.charting.scaler.common"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.charting.scaler.common"]){_4._hasResource["dojox.charting.scaler.common"]=true;_4.provide("dojox.charting.scaler.common");(function(){var eq=function(a,b){return Math.abs(a-b)<=0.000001*(Math.abs(a)+Math.abs(b));};_4.mixin(_6.charting.scaler.common,{findString:function(_a,_b){_a=_a.toLowerCase();for(var i=0;i<_b.length;++i){if(_a==_b[i]){return true;}}return false;},getNumericLabel:function(_d,_e,_f){var def=_f.fixed?_d.toFixed(_e<0?-_e:0):_d.toString();if(_f.labelFunc){var r=_f.labelFunc(def,_d,_e);if(r){return r;}}if(_f.labels){var l=_f.labels,lo=0,hi=l.length;while(lo<hi){var mid=Math.floor((lo+hi)/2),val=l[mid].value;if(val<_d){lo=mid+1;}else{hi=mid;}}if(lo<l.length&&eq(l[lo].value,_d)){return l[lo].text;}--lo;if(lo>=0&&lo<l.length&&eq(l[lo].value,_d)){return l[lo].text;}lo+=2;if(lo<l.length&&eq(l[lo].value,_d)){return l[lo].text;}}return def;}});})();}}};});