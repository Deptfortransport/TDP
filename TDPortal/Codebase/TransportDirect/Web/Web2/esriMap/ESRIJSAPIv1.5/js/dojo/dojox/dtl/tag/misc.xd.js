/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.dtl.tag.misc"],["require","dojox.dtl._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.dtl.tag.misc"]){_4._hasResource["dojox.dtl.tag.misc"]=true;_4.provide("dojox.dtl.tag.misc");_4.require("dojox.dtl._base");(function(){var dd=_6.dtl;var _8=dd.tag.misc;_8.DebugNode=_4.extend(function(_9){this.text=_9;},{render:function(_a,_b){var _c=_a.getKeys();var _d=[];var _e={};for(var i=0,key;key=_c[i];i++){_e[key]=_a[key];_d+="["+key+": "+typeof _a[key]+"]\n";}console.debug(_e);return this.text.set(_d).render(_a,_b,this);},unrender:function(_11,_12){return _12;},clone:function(_13){return new this.constructor(this.text.clone(_13));},toString:function(){return "ddtm.DebugNode";}});_8.FilterNode=_4.extend(function(_14,_15){this._varnode=_14;this._nodelist=_15;},{render:function(_16,_17){var _18=this._nodelist.render(_16,new _6.string.Builder());_16=_16.update({"var":_18.toString()});var _19=this._varnode.render(_16,_17);_16=_16.pop();return _17;},unrender:function(_1a,_1b){return _1b;},clone:function(_1c){return new this.constructor(this._expression,this._nodelist.clone(_1c));}});_8.FirstOfNode=_4.extend(function(_1d,_1e){this._vars=_1d;this.vars=_4.map(_1d,function(_1f){return new _6.dtl._Filter(_1f);});this.contents=_1e;},{render:function(_20,_21){for(var i=0,_23;_23=this.vars[i];i++){var _24=_23.resolve(_20);if(typeof _24!="undefined"){if(_24===null){_24="null";}this.contents.set(_24);return this.contents.render(_20,_21);}}return this.contents.unrender(_20,_21);},unrender:function(_25,_26){return this.contents.unrender(_25,_26);},clone:function(_27){return new this.constructor(this._vars,this.contents.clone(_27));}});_8.SpacelessNode=_4.extend(function(_28,_29){this.nodelist=_28;this.contents=_29;},{render:function(_2a,_2b){if(_2b.getParent){var _2c=[_4.connect(_2b,"onAddNodeComplete",this,"_watch"),_4.connect(_2b,"onSetParent",this,"_watchParent")];_2b=this.nodelist.render(_2a,_2b);_4.disconnect(_2c[0]);_4.disconnect(_2c[1]);}else{var _2d=this.nodelist.dummyRender(_2a);this.contents.set(_2d.replace(/>\s+</g,"><"));_2b=this.contents.render(_2a,_2b);}return _2b;},unrender:function(_2e,_2f){return this.nodelist.unrender(_2e,_2f);},clone:function(_30){return new this.constructor(this.nodelist.clone(_30),this.contents.clone(_30));},_isEmpty:function(_31){return (_31.nodeType==3&&!_31.data.match(/[^\s\n]/));},_watch:function(_32){if(this._isEmpty(_32)){var _33=false;if(_32.parentNode.firstChild==_32){_32.parentNode.removeChild(_32);}}else{var _34=_32.parentNode.childNodes;if(_32.nodeType==1&&_34.length>2){for(var i=2,_36;_36=_34[i];i++){if(_34[i-2].nodeType==1&&this._isEmpty(_34[i-1])){_32.parentNode.removeChild(_34[i-1]);return;}}}}},_watchParent:function(_37){var _38=_37.childNodes;if(_38.length){while(_37.childNodes.length){var _39=_37.childNodes[_37.childNodes.length-1];if(!this._isEmpty(_39)){return;}_37.removeChild(_39);}}}});_8.TemplateTagNode=_4.extend(function(tag,_3b){this.tag=tag;this.contents=_3b;},{mapping:{openblock:"{%",closeblock:"%}",openvariable:"{{",closevariable:"}}",openbrace:"{",closebrace:"}",opencomment:"{#",closecomment:"#}"},render:function(_3c,_3d){this.contents.set(this.mapping[this.tag]);return this.contents.render(_3c,_3d);},unrender:function(_3e,_3f){return this.contents.unrender(_3e,_3f);},clone:function(_40){return new this.constructor(this.tag,this.contents.clone(_40));}});_8.WidthRatioNode=_4.extend(function(_41,max,_43,_44){this.current=new dd._Filter(_41);this.max=new dd._Filter(max);this.width=_43;this.contents=_44;},{render:function(_45,_46){var _47=+this.current.resolve(_45);var max=+this.max.resolve(_45);if(typeof _47!="number"||typeof max!="number"||!max){this.contents.set("");}else{this.contents.set(""+Math.round((_47/max)*this.width));}return this.contents.render(_45,_46);},unrender:function(_49,_4a){return this.contents.unrender(_49,_4a);},clone:function(_4b){return new this.constructor(this.current.getExpression(),this.max.getExpression(),this.width,this.contents.clone(_4b));}});_8.WithNode=_4.extend(function(_4c,_4d,_4e){this.target=new dd._Filter(_4c);this.alias=_4d;this.nodelist=_4e;},{render:function(_4f,_50){var _51=this.target.resolve(_4f);_4f=_4f.push();_4f[this.alias]=_51;_50=this.nodelist.render(_4f,_50);_4f=_4f.pop();return _50;},unrender:function(_52,_53){return _53;},clone:function(_54){return new this.constructor(this.target.getExpression(),this.alias,this.nodelist.clone(_54));}});_4.mixin(_8,{comment:function(_55,_56){_55.skip_past("endcomment");return dd._noOpNode;},debug:function(_57,_58){return new _8.DebugNode(_57.create_text_node());},filter:function(_59,_5a){var _5b=_5a.contents.split(null,1)[1];var _5c=_59.create_variable_node("var|"+_5b);var _5d=_59.parse(["endfilter"]);_59.next_token();return new _8.FilterNode(_5c,_5d);},firstof:function(_5e,_5f){var _60=_5f.split_contents().slice(1);if(!_60.length){throw new Error("'firstof' statement requires at least one argument");}return new _8.FirstOfNode(_60,_5e.create_text_node());},spaceless:function(_61,_62){var _63=_61.parse(["endspaceless"]);_61.delete_first_token();return new _8.SpacelessNode(_63,_61.create_text_node());},templatetag:function(_64,_65){var _66=_65.contents.split();if(_66.length!=2){throw new Error("'templatetag' statement takes one argument");}var tag=_66[1];var _68=_8.TemplateTagNode.prototype.mapping;if(!_68[tag]){var _69=[];for(var key in _68){_69.push(key);}throw new Error("Invalid templatetag argument: '"+tag+"'. Must be one of: "+_69.join(", "));}return new _8.TemplateTagNode(tag,_64.create_text_node());},widthratio:function(_6b,_6c){var _6d=_6c.contents.split();if(_6d.length!=4){throw new Error("widthratio takes three arguments");}var _6e=+_6d[3];if(typeof _6e!="number"){throw new Error("widthratio final argument must be an integer");}return new _8.WidthRatioNode(_6d[1],_6d[2],_6e,_6b.create_text_node());},with_:function(_6f,_70){var _71=_70.split_contents();if(_71.length!=4||_71[2]!="as"){throw new Error("do_width expected format as 'with value as name'");}var _72=_6f.parse(["endwith"]);_6f.next_token();return new _8.WithNode(_71[1],_71[3],_72);}});})();}}};});