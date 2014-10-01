/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.widget.Pager"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dojo.fx"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.widget.Pager"]){_4._hasResource["dojox.widget.Pager"]=true;_4.provide("dojox.widget.Pager");_4.experimental("dojox.widget.Pager");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dojo.fx");_4.declare("dojox.widget.Pager",[_5._Widget,_5._Templated],{templateString:"<div dojoAttachPoint=\"pagerContainer\" tabIndex=\"0\" dojoAttachEvent=\"onkeypress: _handleKey, onfocus: _a11yStyle, onblur:_a11yStyle\" class=\"${orientation}PagerContainer\">\r\n    <div class=\"pagerContainer\">\r\n\t\t<div dojoAttachPoint=\"pagerContainerStatus\" class=\"${orientation}PagerStatus\"></div>\r\n\t\t<div dojoAttachPoint=\"pagerContainerView\" class=\"${orientation}PagerView\">\r\n\t\t    <div dojoAttachPoint=\"pagerItemContainer\"><ul dojoAttachPoint=\"pagerItems\" class=\"pagerItems\"></ul></div>\r\n\t\t</div>\r\n\t\t<div dojoAttachPoint=\"pagerContainerPager\" class=\"${orientation}PagerPager\">\r\n\t\t\t<div tabIndex=\"0\" dojoAttachPoint=\"pagerNext\" class=\"pagerIconContainer\" dojoAttachEvent=\"onclick: _pagerNext\"><img dojoAttachPoint=\"pagerIconNext\" src=\"${iconNext}\" alt=\"Next\" /></div>\r\n\t\t\t<div tabIndex=\"0\" dojoAttachPoint=\"pagerPrevious\" class=\"pagerIconContainer\" dojoAttachEvent=\"onclick: _pagerPrevious\"><img dojoAttachPoint=\"pagerIconPrevious\" src=\"${iconPrevious}\" alt=\"Previous\" /></div>\r\n\t\t</div>\r\n    </div>\r\n\t<div dojoAttachPoint=\"containerNode\" style=\"display:none\"></div>\r\n</div>\r\n",iconPage:_4.moduleUrl("dojox.widget","Pager/images/pageInactive.png"),iconPageActive:_4.moduleUrl("dojox.widget","Pager/images/pageActive.png"),store:null,orientation:"horizontal",statusPos:"leading",pagerPos:"center",duration:500,itemSpace:2,resizeChildren:true,itemClass:"dojox.widget._PagerItem",itemsPage:3,postMixInProperties:function(){var h=(this.orientation=="horizontal");_4.mixin(this,{_totalPages:0,_currentPage:1,dirClass:"pager"+(h?"Horizontal":"Vertical"),iconNext:_4.moduleUrl("dojox.widget","Pager/images/"+(h?"h":"v")+"Next.png"),iconPrevious:_4.moduleUrl("dojox.widget","Pager/images/"+(h?"h":"v")+"Previous.png")});},postCreate:function(){this.inherited(arguments);this.store.fetch({onComplete:_4.hitch(this,"_init")});},_a11yStyle:function(e){_4[(e.type=="focus"?"addClass":"removeClass")](e.target,"pagerFocus");},_handleKey:function(e){var dk=_4.keys;var _b=(e.charCode==dk.SPACE?dk.SPACE:e.keyCode);switch(_b){case dk.UP_ARROW:case dk.RIGHT_ARROW:case 110:case 78:e.preventDefault();this._pagerNext();break;case dk.DOWN_ARROW:case dk.LEFT_ARROW:case 112:case 80:e.preventDefault();this._pagerPrevious();break;case dk.ENTER:switch(e.target){case this.pagerNext:this._pagerNext();break;case this.pagerPrevious:this._pagerPrevious();break;}break;}},_init:function(_c){this.items=_c;this._renderPages();this._renderStatus();this._renderPager();},_renderPages:function(){var _d=this.pagerContainerView;var _h=(this.orientation=="horizontal");var _f=_4.style;if(_h){var _10=_4.marginBox(this.pagerContainerPager).h;var _11=_4.marginBox(this.pagerContainerStatus).h;if(this.pagerPos!="center"){var _12=_10+_11;}else{var _12=_11;var _13=this.pagerIconNext.width;var _14=_f(_d,"width");var _15=_14-(2*_13);_f(_d,{width:_15+"px",marginLeft:this.pagerIconNext.width+"px",marginRight:this.pagerIconNext.width+"px"});}var _16=_f(this.pagerContainer,"height")-_12;_f(this.pagerContainerView,"height",_16+"px");var _17=Math.floor(_f(_d,"width")/this.itemsPage);if(this.statusPos=="trailing"){if(this.pagerPos!="center"){_f(_d,"marginTop",_10+"px");}_f(_d,"marginBottom",_11+"px");}else{_f(_d,"marginTop",_11+"px");if(this.pagerPos!="center"){_f(_d,"marginTop",_10+"px");}}}else{var _18=_4.marginBox(this.pagerContainerPager).w;var _19=_4.marginBox(this.pagerContainerStatus).w;var _1a=_f(this.pagerContainer,"width");if(this.pagerPos!="center"){var _1b=_18+_19;}else{var _1b=_19;var _1c=this.pagerIconNext.height;var _1d=_f(_d,"height");var _1e=_1d-(2*_1c);_f(_d,{height:_1e+"px",marginTop:this.pagerIconNext.height+"px",marginBottom:this.pagerIconNext.height+"px"});}var _1f=_f(this.pagerContainer,"width")-_1b;_f(_d,"width",_1f+"px");var _17=Math.floor(_f(_d,"height")/this.itemsPage);if(this.statusPos=="trailing"){if(this.pagerPos!="center"){_f(_d,"marginLeft",_18+"px");}_f(_d,"marginRight",_19+"px");}else{_f(_d,"marginLeft",_19+"px");if(this.pagerPos!="center"){_f(_d,"marginRight",_18+"px");}}}var _20=_4.getObject(this.itemClass);var _21="padding"+(_h?"Left":"Top");var _22="padding"+(_h?"Right":"Bottom");_4.forEach(this.items,function(_23,cnt){var _25=_4.create("div",{innerHTML:_23.content});var _26=new _20({id:this.id+"-item-"+(cnt+1)},_25);this.pagerItems.appendChild(_26.domNode);var _27={};_27[(_h?"width":"height")]=(_17-this.itemSpace)+"px";var p=(_h?"height":"width");_27[p]=_f(_d,p)+"px";_f(_26.containerNode,_27);if(this.resizeChildren){_26.resizeChildren();}_26.parseChildren();_f(_26.domNode,"position","absolute");if(cnt<this.itemsPage){var pos=(cnt)*_17;var _2a=(_h?"left":"top");var dir=(_h?"top":"left");_f(_26.domNode,dir,"0px");_f(_26.domNode,_2a,pos+"px");}else{_f(_26.domNode,"top","-1000px");_f(_26.domNode,"left","-1000px");}_f(_26.domNode,_22,(this.itemSpace/2)+"px");_f(_26.domNode,_21,(this.itemSpace/2)+"px");},this);},_renderPager:function(){var tcp=this.pagerContainerPager;var _2d="0px";var _h=(this.orientation=="horizontal");if(_h){if(this.statusPos=="center"){}else{if(this.statusPos=="trailing"){_4.style(tcp,"top",_2d);}else{_4.style(tcp,"bottom",_2d);}}_4.style(this.pagerNext,"right",_2d);_4.style(this.pagerPrevious,"left",_2d);}else{if(this.statusPos=="trailing"){_4.style(tcp,"left",_2d);}else{_4.style(tcp,"right",_2d);}_4.style(this.pagerNext,"bottom",_2d);_4.style(this.pagerPrevious,"top",_2d);}},_renderStatus:function(){this._totalPages=Math.ceil(this.items.length/this.itemsPage);this.iconWidth=0;this.iconHeight=0;this.iconsLoaded=0;this._iconConnects=[];for(var i=1;i<=this._totalPages;i++){var _30=new Image();var _31=i;_4.connect(_30,"onclick",_4.hitch(this,function(_32){this._pagerSkip(_32);},_31));this._iconConnects[_31]=_4.connect(_30,"onload",_4.hitch(this,function(_33){this.iconWidth+=_30.width;this.iconHeight+=_30.height;this.iconsLoaded++;if(this._totalPages==this.iconsLoaded){if(this.orientation=="horizontal"){if(this.statusPos=="trailing"){if(this.pagerPos=="center"){var _34=_4.style(this.pagerContainer,"height");var _35=_4.style(this.pagerContainerStatus,"height");_4.style(this.pagerContainerPager,"top",((_34/2)-(_35/2))+"px");}_4.style(this.pagerContainerStatus,"bottom","0px");}else{if(this.pagerPos=="center"){var _34=_4.style(this.pagerContainer,"height");var _35=_4.style(this.pagerContainerStatus,"height");_4.style(this.pagerContainerPager,"bottom",((_34/2)-(_35/2))+"px");}_4.style(this.pagerContainerStatus,"top","0px");}var _36=(_4.style(this.pagerContainer,"width")/2)-(this.iconWidth/2);_4.style(this.pagerContainerStatus,"paddingLeft",_36+"px");}else{if(this.statusPos=="trailing"){if(this.pagerPos=="center"){var _37=_4.style(this.pagerContainer,"width");var _38=_4.style(this.pagerContainerStatus,"width");_4.style(this.pagerContainerPager,"left",((_37/2)-(_38/2))+"px");}_4.style(this.pagerContainerStatus,"right","0px");}else{if(this.pagerPos=="center"){var _37=_4.style(this.pagerContainer,"width");var _38=_4.style(this.pagerContainerStatus,"width");_4.style(this.pagerContainerPager,"right",((_37/2)-(_38/2))+"px");}_4.style(this.pagerContainerStatus,"left","0px");}var _36=(_4.style(this.pagerContainer,"height")/2)-(this.iconHeight/2);_4.style(this.pagerContainerStatus,"paddingTop",_36+"px");}}_4.disconnect(this._iconConnects[_33]);},_31));if(i==this._currentPage){_30.src=this.iconPageActive;}else{_30.src=this.iconPage;}var _31=i;_4.addClass(_30,this.orientation+"PagerIcon");_4.attr(_30,"id",this.id+"-status-"+i);this.pagerContainerStatus.appendChild(_30);if(this.orientation=="vertical"){_4.style(_30,"display","block");}}},_pagerSkip:function(_39){if(this._currentPage==_39){return;}else{var _3a;var _3b;if(_39<this._currentPage){_3a=this._currentPage-_39;_3b=(this._totalPages+_39)-this._currentPage;}else{_3a=(this._totalPages+this._currentPage)-_39;_3b=_39-this._currentPage;}var b=(_3b>_3a);this._toScroll=(b?_3a:_3b);var cmd=(b?"_pagerPrevious":"_pagerNext");var _3e=this.connect(this,"onScrollEnd",function(){this._toScroll--;if(this._toScroll<1){this.disconnect(_3e);}else{this[cmd]();}});this[cmd]();}},_pagerNext:function(){if(this._anim){return;}var _3f=[];for(var i=this._currentPage*this.itemsPage;i>(this._currentPage-1)*this.itemsPage;i--){if(!_4.byId(this.id+"-item-"+i)){continue;}var _41=_4.byId(this.id+"-item-"+i);var _42=_4.marginBox(_41);if(this.orientation=="horizontal"){var _43=_42.l-(this.itemsPage*_42.w);_3f.push(_4.fx.slideTo({node:_41,left:_43,duration:this.duration}));}else{var _43=_42.t-(this.itemsPage*_42.h);_3f.push(_4.fx.slideTo({node:_41,top:_43,duration:this.duration}));}}var _44=this._currentPage;if(this._currentPage==this._totalPages){this._currentPage=1;}else{this._currentPage++;}var cnt=this.itemsPage;for(var i=this._currentPage*this.itemsPage;i>(this._currentPage-1)*this.itemsPage;i--){if(_4.byId(this.id+"-item-"+i)){var _41=_4.byId(this.id+"-item-"+i);var _42=_4.marginBox(_41);if(this.orientation=="horizontal"){var _46=(_4.style(this.pagerContainerView,"width")+((cnt-1)*_42.w))-1;_4.style(_41,"left",_46+"px");_4.style(_41,"top","0px");var _43=_46-(this.itemsPage*_42.w);_3f.push(_4.fx.slideTo({node:_41,left:_43,duration:this.duration}));}else{_46=(_4.style(this.pagerContainerView,"height")+((cnt-1)*_42.h))-1;_4.style(_41,"top",_46+"px");_4.style(_41,"left","0px");var _43=_46-(this.itemsPage*_42.h);_3f.push(_4.fx.slideTo({node:_41,top:_43,duration:this.duration}));}}cnt--;}this._anim=_4.fx.combine(_3f);var _47=this.connect(this._anim,"onEnd",function(){delete this._anim;this.onScrollEnd();this.disconnect(_47);});this._anim.play();_4.byId(this.id+"-status-"+_44).src=this.iconPage;_4.byId(this.id+"-status-"+this._currentPage).src=this.iconPageActive;},_pagerPrevious:function(){if(this._anim){return;}var _48=[];for(var i=this._currentPage*this.itemsPage;i>(this._currentPage-1)*this.itemsPage;i--){if(!_4.byId(this.id+"-item-"+i)){continue;}var _4a=_4.byId(this.id+"-item-"+i);var _4b=_4.marginBox(_4a);if(this.orientation=="horizontal"){var _4c=_4.style(_4a,"left")+(this.itemsPage*_4b.w);_48.push(_4.fx.slideTo({node:_4a,left:_4c,duration:this.duration}));}else{var _4c=_4.style(_4a,"top")+(this.itemsPage*_4b.h);_48.push(_4.fx.slideTo({node:_4a,top:_4c,duration:this.duration}));}}var _4d=this._currentPage;if(this._currentPage==1){this._currentPage=this._totalPages;}else{this._currentPage--;}var cnt=this.itemsPage;var j=1;for(var i=this._currentPage*this.itemsPage;i>(this._currentPage-1)*this.itemsPage;i--){if(_4.byId(this.id+"-item-"+i)){var _4a=_4.byId(this.id+"-item-"+i);var _4b=_4.marginBox(_4a);if(this.orientation=="horizontal"){var _50=-(j*_4b.w)+1;_4.style(_4a,"left",_50+"px");_4.style(_4a,"top","0px");var _4c=((cnt-1)*_4b.w);_48.push(_4.fx.slideTo({node:_4a,left:_4c,duration:this.duration}));var _4c=_50+(this.itemsPage*_4b.w);_48.push(_4.fx.slideTo({node:_4a,left:_4c,duration:this.duration}));}else{_50=-((j*_4b.h)+1);_4.style(_4a,"top",_50+"px");_4.style(_4a,"left","0px");var _4c=((cnt-1)*_4b.h);_48.push(_4.fx.slideTo({node:_4a,top:_4c,duration:this.duration}));}}cnt--;j++;}this._anim=_4.fx.combine(_48);var _51=_4.connect(this._anim,"onEnd",_4.hitch(this,function(){delete this._anim;this.onScrollEnd();_4.disconnect(_51);}));this._anim.play();_4.byId(this.id+"-status-"+_4d).src=this.iconPage;_4.byId(this.id+"-status-"+this._currentPage).src=this.iconPageActive;},onScrollEnd:function(){}});_4.declare("dojox.widget._PagerItem",[_5._Widget,_5._Templated],{templateString:"<li class=\"pagerItem\" dojoAttachPoint=\"containerNode\"></li>",resizeChildren:function(){var box=_4.marginBox(this.containerNode);_4.style(this.containerNode.firstChild,{width:box.w+"px",height:box.h+"px"});},parseChildren:function(){_4.parser.parse(this.containerNode);}});}}};});