/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.fx.flip"],["require","dojo.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.fx.flip"]){_4._hasResource["dojox.fx.flip"]=true;_4.provide("dojox.fx.flip");_4.experimental("dojox.fx.flip");_4.require("dojo.fx");(function(){var _7="border",_8="Width",_9="Height",_a="Top",_b="Right",_c="Left",_d="Bottom";_6.fx.flip=function(_e){var _f=_4.create("div"),_10=_e.node=_4.byId(_e.node),s=_10.style,_12=null,hs=null,pn=null,_15=_e.lightColor||"#dddddd",_16=_e.darkColor||"#555555",_17=_4.style(_10,"backgroundColor"),_18=_e.endColor||_17,_19={},_1a=[],_1b=_e.duration?_e.duration/2:250,dir=_e.dir||"left",_1d=0.9,_1e="transparent",_1f=_e.whichAnim,_20=_e.axis||"center",_21=_e.depth;var _22=function(_23){return ((new _4.Color(_23)).toHex()==="#000000")?"#000001":_23;};if(_4.isIE<7){_18=_22(_18);_15=_22(_15);_16=_22(_16);_17=_22(_17);_1e="black";_f.style.filter="chroma(color='#000000')";}var _24=(function(n){return function(){var ret=_4.coords(n,true);_12={top:ret.y,left:ret.x,width:ret.w,height:ret.h};};})(_10);_24();hs={position:"absolute",top:_12["top"]+"px",left:_12["left"]+"px",height:"0",width:"0",zIndex:_e.zIndex||(s.zIndex||0),border:"0 solid "+_1e,fontSize:"0",visibility:"hidden"};var _27=[{},{top:_12["top"],left:_12["left"]}];var _28={left:[_c,_b,_a,_d,_8,_9,"end"+_9+"Min",_c,"end"+_9+"Max"],right:[_b,_c,_a,_d,_8,_9,"end"+_9+"Min",_c,"end"+_9+"Max"],top:[_a,_d,_c,_b,_9,_8,"end"+_8+"Min",_a,"end"+_8+"Max"],bottom:[_d,_a,_c,_b,_9,_8,"end"+_8+"Min",_a,"end"+_8+"Max"]};pn=_28[dir];if(typeof _21!="undefined"){_21=Math.max(0,Math.min(1,_21))/2;_1d=0.4+(0.5-_21);}else{_1d=Math.min(0.9,Math.max(0.4,_12[pn[5].toLowerCase()]/_12[pn[4].toLowerCase()]));}var p0=_27[0];for(var i=4;i<6;i++){if(_20=="center"||_20=="cube"){_12["end"+pn[i]+"Min"]=_12[pn[i].toLowerCase()]*_1d;_12["end"+pn[i]+"Max"]=_12[pn[i].toLowerCase()]/_1d;}else{if(_20=="shortside"){_12["end"+pn[i]+"Min"]=_12[pn[i].toLowerCase()];_12["end"+pn[i]+"Max"]=_12[pn[i].toLowerCase()]/_1d;}else{if(_20=="longside"){_12["end"+pn[i]+"Min"]=_12[pn[i].toLowerCase()]*_1d;_12["end"+pn[i]+"Max"]=_12[pn[i].toLowerCase()];}}}}if(_20=="center"){p0[pn[2].toLowerCase()]=_12[pn[2].toLowerCase()]-(_12[pn[8]]-_12[pn[6]])/4;}else{if(_20=="shortside"){p0[pn[2].toLowerCase()]=_12[pn[2].toLowerCase()]-(_12[pn[8]]-_12[pn[6]])/2;}}_19[pn[5].toLowerCase()]=_12[pn[5].toLowerCase()]+"px";_19[pn[4].toLowerCase()]="0";_19[_7+pn[1]+_8]=_12[pn[4].toLowerCase()]+"px";_19[_7+pn[1]+"Color"]=_17;p0[_7+pn[1]+_8]=0;p0[_7+pn[1]+"Color"]=_16;p0[_7+pn[2]+_8]=p0[_7+pn[3]+_8]=_20!="cube"?(_12["end"+pn[5]+"Max"]-_12["end"+pn[5]+"Min"])/2:_12[pn[6]]/2;p0[pn[7].toLowerCase()]=_12[pn[7].toLowerCase()]+_12[pn[4].toLowerCase()]/2+(_e.shift||0);p0[pn[5].toLowerCase()]=_12[pn[6]];var p1=_27[1];p1[_7+pn[0]+"Color"]={start:_15,end:_18};p1[_7+pn[0]+_8]=_12[pn[4].toLowerCase()];p1[_7+pn[2]+_8]=0;p1[_7+pn[3]+_8]=0;p1[pn[5].toLowerCase()]={start:_12[pn[6]],end:_12[pn[5].toLowerCase()]};_4.mixin(hs,_19);_4.style(_f,hs);_4.body().appendChild(_f);var _2c=function(){_4.destroy(_f);s.backgroundColor=_18;s.visibility="visible";};if(_1f=="last"){for(i in p0){p0[i]={start:p0[i]};}p0[_7+pn[1]+"Color"]={start:_16,end:_18};p1=p0;}if(!_1f||_1f=="first"){_1a.push(_4.animateProperty({node:_f,duration:_1b,properties:p0}));}if(!_1f||_1f=="last"){_1a.push(_4.animateProperty({node:_f,duration:_1b,properties:p1,onEnd:_2c}));}_4.connect(_1a[0],"play",function(){_f.style.visibility="visible";s.visibility="hidden";});return _4.fx.chain(_1a);};_6.fx.flipCube=function(_2d){var _2e=[],mb=_4.marginBox(_2d.node),_30=mb.w/2,_31=mb.h/2,_32={top:{pName:"height",args:[{whichAnim:"first",dir:"top",shift:-_31},{whichAnim:"last",dir:"bottom",shift:_31}]},right:{pName:"width",args:[{whichAnim:"first",dir:"right",shift:_30},{whichAnim:"last",dir:"left",shift:-_30}]},bottom:{pName:"height",args:[{whichAnim:"first",dir:"bottom",shift:_31},{whichAnim:"last",dir:"top",shift:-_31}]},left:{pName:"width",args:[{whichAnim:"first",dir:"left",shift:-_30},{whichAnim:"last",dir:"right",shift:_30}]}};var d=_32[_2d.dir||"left"],p=d.args;_2d.duration=_2d.duration?_2d.duration*2:500;_2d.depth=0.8;_2d.axis="cube";for(var i=p.length-1;i>=0;i--){_4.mixin(_2d,p[i]);_2e.push(_6.fx.flip(_2d));}return _4.fx.combine(_2e);};_6.fx.flipPage=function(_36){var n=_36.node,_38=_4.coords(n,true),x=_38.x,y=_38.y,w=_38.w,h=_38.h,_3d=_4.style(n,"backgroundColor"),_3e=_36.lightColor||"#dddddd",_3f=_36.darkColor,_40=_4.create("div"),_41=[],hn=[],dir=_36.dir||"right",pn={left:["left","right","x","w"],top:["top","bottom","y","h"],right:["left","left","x","w"],bottom:["top","top","y","h"]},_45={right:[1,-1],left:[-1,1],top:[-1,1],bottom:[1,-1]};_4.style(_40,{position:"absolute",width:w+"px",height:h+"px",top:y+"px",left:x+"px",visibility:"hidden"});var hs=[];for(var i=0;i<2;i++){var r=i%2,d=r?pn[dir][1]:dir,wa=r?"last":"first",_4b=r?_3d:_3e,_4c=r?_4b:_36.startColor||n.style.backgroundColor;hn[i]=_4.clone(_40);var _4d=function(x){return function(){_4.destroy(hn[x]);};}(i);_4.body().appendChild(hn[i]);hs[i]={backgroundColor:r?_4c:_3d};hs[i][pn[dir][0]]=_38[pn[dir][2]]+_45[dir][0]*i*_38[pn[dir][3]]+"px";_4.style(hn[i],hs[i]);_41.push(_6.fx.flip({node:hn[i],dir:d,axis:"shortside",depth:_36.depth,duration:_36.duration/2,shift:_45[dir][i]*_38[pn[dir][3]]/2,darkColor:_3f,lightColor:_3e,whichAnim:wa,endColor:_4b}));_4.connect(_41[i],"onEnd",_4d);}return _4.fx.chain(_41);};_6.fx.flipGrid=function(_4f){var _50=_4f.rows||4,_51=_4f.cols||4,_52=[],_53=_4.create("div"),n=_4f.node,_55=_4.coords(n,true),x=_55.x,y=_55.y,nw=_55.w,nh=_55.h,w=_55.w/_51,h=_55.h/_50,_5c=[];_4.style(_53,{position:"absolute",width:w+"px",height:h+"px",backgroundColor:_4.style(n,"backgroundColor")});for(var i=0;i<_50;i++){var r=i%2,d=r?"right":"left",_60=r?1:-1;var cn=_4.clone(n);_4.style(cn,{position:"absolute",width:nw+"px",height:nh+"px",top:y+"px",left:x+"px",clip:"rect("+i*h+"px,"+nw+"px,"+nh+"px,0)"});_4.body().appendChild(cn);_52[i]=[];for(var j=0;j<_51;j++){var hn=_4.clone(_53),l=r?j:_51-(j+1);var _65=function(xn,_67,_68){return function(){if(!(_67%2)){_4.style(xn,{clip:"rect("+_67*h+"px,"+(nw-(_68+1)*w)+"px,"+((_67+1)*h)+"px,0px)"});}else{_4.style(xn,{clip:"rect("+_67*h+"px,"+nw+"px,"+((_67+1)*h)+"px,"+((_68+1)*w)+"px)"});}};}(cn,i,j);_4.body().appendChild(hn);_4.style(hn,{left:x+l*w+"px",top:y+i*h+"px",visibility:"hidden"});var a=_6.fx.flipPage({node:hn,dir:d,duration:_4f.duration||900,shift:_60*w/2,depth:0.2,darkColor:_4f.darkColor,lightColor:_4f.lightColor,startColor:_4f.startColor||_4f.node.style.backgroundColor}),_6a=function(xn){return function(){_4.destroy(xn);};}(hn);_4.connect(a,"play",this,_65);_4.connect(a,"play",this,_6a);_52[i].push(a);}_5c.push(_4.fx.chain(_52[i]));}_4.connect(_5c[0],"play",function(){_4.style(n,{visibility:"hidden"});});return _4.fx.combine(_5c);};})();}}};});