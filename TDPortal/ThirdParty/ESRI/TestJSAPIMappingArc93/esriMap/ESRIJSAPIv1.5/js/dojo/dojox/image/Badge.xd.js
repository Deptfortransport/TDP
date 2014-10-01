/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.image.Badge"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dojo.fx.easing"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.image.Badge"]){_4._hasResource["dojox.image.Badge"]=true;_4.provide("dojox.image.Badge");_4.experimental("dojox.image.Badge");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dojo.fx.easing");_4.declare("dojox.image.Badge",[_5._Widget,_5._Templated],{baseClass:"dojoxBadge",templateString:"<div class=\"dojoxBadge\" dojoAttachPoint=\"containerNode\"></div>",children:"div.dojoxBadgeImage",rows:4,cols:5,cellSize:50,delay:2000,threads:1,easing:"dojo.fx.easing.backOut",startup:function(){if(this._started){return;}if(_4.isString(this.easing)){this.easing=_4.getObject(this.easing);}this.inherited(arguments);this._init();},_init:function(){var _7=0,_w=this.cellSize;_4.style(this.domNode,{width:_w*this.cols+"px",height:_w*this.rows+"px"});this._nl=_4.query(this.children,this.containerNode).forEach(function(n,_a){var _b=_a%this.cols,t=_7*_w,l=_b*_w;_4.style(n,{top:t+"px",left:l+"px",width:_w-2+"px",height:_w-2+"px"});if(_b==this.cols-1){_7++;}_4.addClass(n,this.baseClass+"Image");},this);var l=this._nl.length;while(this.threads--){var s=Math.floor(Math.random()*l);setTimeout(_4.hitch(this,"_enbiggen",{target:this._nl[s]}),this.delay*this.threads);}},_getCell:function(n){var _11=this._nl.indexOf(n);if(_11>=0){var _12=_11%this.cols;var _13=Math.floor(_11/this.cols);return {x:_12,y:_13,n:this._nl[_11],io:_11};}else{return undefined;}},_getImage:function(){return "url('')";},_enbiggen:function(e){var _15=this._getCell(e.target||e);if(_15){var _cc=(this.cellSize*2)-2;var _17={height:_cc,width:_cc};var _18=function(){return Math.round(Math.random());};if(_15.x==this.cols-1||(_15.x>0&&_18())){_17.left=this.cellSize*(_15.x-1);}if(_15.y==this.rows-1||(_15.y>0&&_18())){_17.top=this.cellSize*(_15.y-1);}var bc=this.baseClass;_4.addClass(_15.n,bc+"Top");_4.addClass(_15.n,bc+"Seen");_4.animateProperty({node:_15.n,properties:_17,onEnd:_4.hitch(this,"_loadUnder",_15,_17),easing:this.easing}).play();}},_loadUnder:function(_1a,_1b){var idx=_1a.io;var _1d=[];var _1e=(_1b.left>=0);var _1f=(_1b.top>=0);var c=this.cols,e=idx+(_1e?-1:1),f=idx+(_1f?-c:c),g=(_1f?(_1e?e-c:f+1):(_1e?f-1:e+c)),bc=this.baseClass;_4.forEach([e,f,g],function(x){var n=this._nl[x];if(n){if(_4.hasClass(n,bc+"Seen")){_4.removeClass(n,bc+"Seen");}}},this);setTimeout(_4.hitch(this,"_disenbiggen",_1a,_1b),this.delay*1.25);},_disenbiggen:function(_27,_28){if(_28.top>=0){_28.top+=this.cellSize;}if(_28.left>=0){_28.left+=this.cellSize;}var _cc=this.cellSize-2;_4.animateProperty({node:_27.n,properties:_4.mixin(_28,{width:_cc,height:_cc}),onEnd:_4.hitch(this,"_cycle",_27,_28)}).play(5);},_cycle:function(_2a,_2b){var bc=this.baseClass;_4.removeClass(_2a.n,bc+"Top");var ns=this._nl.filter(function(n){return !_4.hasClass(n,bc+"Seen");});var c=ns[Math.floor(Math.random()*ns.length)];setTimeout(_4.hitch(this,"_enbiggen",{target:c}),this.delay/2);}});}}};});