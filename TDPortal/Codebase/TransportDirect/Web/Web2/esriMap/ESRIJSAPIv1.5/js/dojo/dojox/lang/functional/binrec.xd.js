/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.functional.binrec"],["require","dojox.lang.functional.lambda"],["require","dojox.lang.functional.util"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.functional.binrec"]){_4._hasResource["dojox.lang.functional.binrec"]=true;_4.provide("dojox.lang.functional.binrec");_4.require("dojox.lang.functional.lambda");_4.require("dojox.lang.functional.util");(function(){var df=_6.lang.functional,_8=df.inlineLambda,_x="_x",_a=["_z.r","_r","_z.a"];df.binrec=function(_b,_c,_d,_e){var c,t,b,a,cs,ts,bs,as,_17={},_18={},_19=function(x){_17[x]=1;};if(typeof _b=="string"){cs=_8(_b,_x,_19);}else{c=df.lambda(_b);cs="_c.apply(this, _x)";_18["_c=_t.c"]=1;}if(typeof _c=="string"){ts=_8(_c,_x,_19);}else{t=df.lambda(_c);ts="_t.apply(this, _x)";}if(typeof _d=="string"){bs=_8(_d,_x,_19);}else{b=df.lambda(_d);bs="_b.apply(this, _x)";_18["_b=_t.b"]=1;}if(typeof _e=="string"){as=_8(_e,_a,_19);}else{a=df.lambda(_e);as="_a.call(this, _z.r, _r, _z.a)";_18["_a=_t.a"]=1;}var _1b=df.keys(_17),_1c=df.keys(_18),f=new Function([],"var _x=arguments,_y,_z,_r".concat(_1b.length?","+_1b.join(","):"",_1c.length?",_t=_x.callee,"+_1c.join(","):"",t?(_1c.length?",_t=_t.t":"_t=_x.callee.t"):"",";while(!",cs,"){_r=",bs,";_y={p:_y,a:_r[1]};_z={p:_z,a:_x};_x=_r[0]}for(;;){do{_r=",ts,";if(!_z)return _r;while(\"r\" in _z){_r=",as,";if(!(_z=_z.p))return _r}_z.r=_r;_x=_y.a;_y=_y.p}while(",cs,");do{_r=",bs,";_y={p:_y,a:_r[1]};_z={p:_z,a:_x};_x=_r[0]}while(!",cs,")}"));if(c){f.c=c;}if(t){f.t=t;}if(b){f.b=b;}if(a){f.a=a;}return f;};})();}}};});