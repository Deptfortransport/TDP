/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.gfx3d.gradient"],["require","dojox.gfx3d.vector"],["require","dojox.gfx3d.matrix"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.gfx3d.gradient"]){_4._hasResource["dojox.gfx3d.gradient"]=true;_4.provide("dojox.gfx3d.gradient");_4.require("dojox.gfx3d.vector");_4.require("dojox.gfx3d.matrix");(function(){var _7=function(a,b){return Math.sqrt(Math.pow(b.x-a.x,2)+Math.pow(b.y-a.y,2));};var N=32;_6.gfx3d.gradient=function(_b,_c,_d,_e,_f,to,_11){var m=_6.gfx3d.matrix,v=_6.gfx3d.vector,mx=m.normalize(_11),f=m.multiplyPoint(mx,_e*Math.cos(_f)+_d.x,_e*Math.sin(_f)+_d.y,_d.z),t=m.multiplyPoint(mx,_e*Math.cos(to)+_d.x,_e*Math.sin(to)+_d.y,_d.z),c=m.multiplyPoint(mx,_d.x,_d.y,_d.z),_18=(to-_f)/N,r=_7(f,t)/2,mod=_b[_c.type],fin=_c.finish,pmt=_c.color,_1d=[{offset:0,color:mod.call(_b,v.substract(f,c),fin,pmt)}];for(var a=_f+_18;a<to;a+=_18){var p=m.multiplyPoint(mx,_e*Math.cos(a)+_d.x,_e*Math.sin(a)+_d.y,_d.z),df=_7(f,p),dt=_7(t,p);_1d.push({offset:df/(df+dt),color:mod.call(_b,v.substract(p,c),fin,pmt)});}_1d.push({offset:1,color:mod.call(_b,v.substract(t,c),fin,pmt)});return {type:"linear",x1:0,y1:-r,x2:0,y2:r,colors:_1d};};})();}}};});