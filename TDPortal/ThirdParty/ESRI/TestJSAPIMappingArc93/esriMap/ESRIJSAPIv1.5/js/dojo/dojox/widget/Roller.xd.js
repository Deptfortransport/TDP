/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.widget.Roller"],["require","dijit._Widget"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.widget.Roller"]){_4._hasResource["dojox.widget.Roller"]=true;_4.provide("dojox.widget.Roller");_4.require("dijit._Widget");_4.declare("dojox.widget.Roller",_5._Widget,{delay:2000,autoStart:true,itemSelector:"> li",durationIn:400,durationOut:275,_idx:-1,postCreate:function(){if(!this["items"]){this.items=[];}_4.addClass(this.domNode,"dojoxRoller");_4.query(this.itemSelector,this.domNode).forEach(function(_7,i){this.items.push(_7.innerHTML);if(i==0){this._roller=_7;this._idx=0;}else{_4.destroy(_7);}},this);if(!this._roller){this._roller=_4.create("li",null,this.domNode);}this.makeAnims();if(this.autoStart){this.start();}},makeAnims:function(){var n=this.domNode;_4.mixin(this,{_anim:{"in":_4.fadeIn({node:n,duration:this.durationIn}),"out":_4.fadeOut({node:n,duration:this.durationOut})}});this._setupConnects();},_setupConnects:function(){var _a=this._anim;this.connect(_a["out"],"onEnd",function(){this._set(this._idx+1);_a["in"].play(15);});this.connect(_a["in"],"onEnd",function(){this._timeout=setTimeout(_4.hitch(this,"_run"),this.delay);});},start:function(){if(!this.rolling){this.rolling=true;this._run();}},_run:function(){this._anim["out"].gotoPercent(0,true);},stop:function(){this.rolling=false;var m=this._anim,t=this._timeout;if(t){clearTimeout(t);}m["in"].stop();m["out"].stop();},_set:function(i){var l=this.items.length-1;if(i<0){i=l;}if(i>l){i=0;}this._roller.innerHTML=this.items[i]||"error!";this._idx=i;}});_4.declare("dojox.widget.RollerSlide",_6.widget.Roller,{durationOut:175,makeAnims:function(){var n=this.domNode,pos="position",_11={top:{end:0,start:25},opacity:1};_4.style(n,pos,"relative");_4.style(this._roller,pos,"absolute");_4.mixin(this,{_anim:{"in":_4.animateProperty({node:n,duration:this.durationIn,properties:_11}),"out":_4.fadeOut({node:n,duration:this.durationOut})}});this._setupConnects();}});_4.declare("dojox.widget._RollerHover",null,{postCreate:function(){this.inherited(arguments);this.connect(this.domNode,"onmouseenter","stop");this.connect(this.domNode,"onmouseleave","start");}});}}};});