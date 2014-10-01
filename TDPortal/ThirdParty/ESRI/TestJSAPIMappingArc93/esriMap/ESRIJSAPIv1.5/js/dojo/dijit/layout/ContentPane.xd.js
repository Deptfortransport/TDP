/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.layout.ContentPane"],["require","dijit._Widget"],["require","dijit._Contained"],["require","dijit.layout._LayoutWidget"],["require","dojo.parser"],["require","dojo.string"],["require","dojo.html"],["requireLocalization","dijit","loading",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.layout.ContentPane"]){_4._hasResource["dijit.layout.ContentPane"]=true;_4.provide("dijit.layout.ContentPane");_4.require("dijit._Widget");_4.require("dijit._Contained");_4.require("dijit.layout._LayoutWidget");_4.require("dojo.parser");_4.require("dojo.string");_4.require("dojo.html");_4.declare("dijit.layout.ContentPane",_5._Widget,{href:"",extractContent:false,parseOnLoad:true,preventCache:false,preload:false,refreshOnShow:false,loadingMessage:"<span class='dijitContentPaneLoading'>${loadingState}</span>",errorMessage:"<span class='dijitContentPaneError'>${errorState}</span>",isLoaded:false,baseClass:"dijitContentPane",doLayout:true,ioArgs:{},isContainer:true,postMixInProperties:function(){this.inherited(arguments);var _7=_4.i18n.getLocalization("dijit","loading",this.lang);this.loadingMessage=_4.string.substitute(this.loadingMessage,_7);this.errorMessage=_4.string.substitute(this.errorMessage,_7);if(!this.href&&this.srcNodeRef&&this.srcNodeRef.innerHTML){this.isLoaded=true;}},buildRendering:function(){this.inherited(arguments);if(!this.containerNode){this.containerNode=this.domNode;}},postCreate:function(){this.domNode.title="";if(!_4.attr(this.domNode,"role")){_5.setWaiRole(this.domNode,"group");}_4.addClass(this.domNode,this.baseClass);},startup:function(){if(this._started){return;}if(this.isLoaded){_4.forEach(this.getChildren(),function(_8){_8.startup();});if(this.doLayout){this._checkIfSingleChild();}if(!this._singleChild||!_5._Contained.prototype.getParent.call(this)){this._scheduleLayout();}}this._loadCheck();this.inherited(arguments);},_checkIfSingleChild:function(){var _9=_4.query(">",this.containerNode),_a=_9.filter(function(_b){return _4.hasAttr(_b,"dojoType")||_4.hasAttr(_b,"widgetId");}),_c=_4.filter(_a.map(_5.byNode),function(_d){return _d&&_d.domNode&&_d.resize;});if(_9.length==_a.length&&_c.length==1){this._singleChild=_c[0];}else{delete this._singleChild;}},setHref:function(_e){_4.deprecated("dijit.layout.ContentPane.setHref() is deprecated. Use attr('href', ...) instead.","","2.0");return this.attr("href",_e);},_setHrefAttr:function(_f){this.cancel();this.href=_f;if(this._created&&(this.preload||this._isShown())){return this.refresh();}else{this._hrefChanged=true;}},setContent:function(_10){_4.deprecated("dijit.layout.ContentPane.setContent() is deprecated.  Use attr('content', ...) instead.","","2.0");this.attr("content",_10);},_setContentAttr:function(_11){this.href="";this.cancel();this._setContent(_11||"");this._isDownloaded=false;},_getContentAttr:function(){return this.containerNode.innerHTML;},cancel:function(){if(this._xhrDfd&&(this._xhrDfd.fired==-1)){this._xhrDfd.cancel();}delete this._xhrDfd;},uninitialize:function(){if(this._beingDestroyed){this.cancel();}},destroyRecursive:function(_12){if(this._beingDestroyed){return;}this._beingDestroyed=true;this.inherited(arguments);},resize:function(_13){_4.marginBox(this.domNode,_13);var _14=this.containerNode,mb=_4.mixin(_4.marginBox(_14),_13||{});var cb=(this._contentBox=_5.layout.marginBox2contentBox(_14,mb));if(this._singleChild&&this._singleChild.resize){this._singleChild.resize({w:cb.w,h:cb.h});}},_isShown:function(){if("open" in this){return this.open;}else{var _17=this.domNode;return (_17.style.display!="none")&&(_17.style.visibility!="hidden")&&!_4.hasClass(_17,"dijitHidden");}},_onShow:function(){if(this._needLayout){this._layoutChildren();}this._loadCheck();if(this.onShow){this.onShow();}},_loadCheck:function(){if((this.href&&!this._xhrDfd)&&(!this.isLoaded||this._hrefChanged||this.refreshOnShow)&&(this.preload||this._isShown())){delete this._hrefChanged;this.refresh();}},refresh:function(){this.cancel();this._setContent(this.onDownloadStart(),true);var _18=this;var _19={preventCache:(this.preventCache||this.refreshOnShow),url:this.href,handleAs:"text"};if(_4.isObject(this.ioArgs)){_4.mixin(_19,this.ioArgs);}var _1a=(this._xhrDfd=(this.ioMethod||_4.xhrGet)(_19));_1a.addCallback(function(_1b){try{_18._isDownloaded=true;_18._setContent(_1b,false);_18.onDownloadEnd();}catch(err){_18._onError("Content",err);}delete _18._xhrDfd;return _1b;});_1a.addErrback(function(err){if(!_1a.canceled){_18._onError("Download",err);}delete _18._xhrDfd;return err;});},_onLoadHandler:function(_1d){this.isLoaded=true;try{this.onLoad(_1d);}catch(e){console.error("Error "+this.widgetId+" running custom onLoad code: "+e.message);}},_onUnloadHandler:function(){this.isLoaded=false;try{this.onUnload();}catch(e){console.error("Error "+this.widgetId+" running custom onUnload code: "+e.message);}},destroyDescendants:function(){if(this.isLoaded){this._onUnloadHandler();}var _1e=this._contentSetter;_4.forEach(this.getChildren(),function(_1f){if(_1f.destroyRecursive){_1f.destroyRecursive();}});if(_1e){_4.forEach(_1e.parseResults,function(_20){if(_20.destroyRecursive&&_20.domNode&&_20.domNode.parentNode==_4.body()){_20.destroyRecursive();}});delete _1e.parseResults;}_4.html._emptyNode(this.containerNode);},_setContent:function(_21,_22){this.destroyDescendants();delete this._singleChild;var _23=this._contentSetter;if(!(_23&&_23 instanceof _4.html._ContentSetter)){_23=this._contentSetter=new _4.html._ContentSetter({node:this.containerNode,_onError:_4.hitch(this,this._onError),onContentError:_4.hitch(this,function(e){var _25=this.onContentError(e);try{this.containerNode.innerHTML=_25;}catch(e){console.error("Fatal "+this.id+" could not change content due to "+e.message,e);}})});}var _26=_4.mixin({cleanContent:this.cleanContent,extractContent:this.extractContent,parseContent:this.parseOnLoad},this._contentSetterParams||{});_4.mixin(_23,_26);_23.set((_4.isObject(_21)&&_21.domNode)?_21.domNode:_21);delete this._contentSetterParams;if(!_22){_4.forEach(this.getChildren(),function(_27){_27.startup();});if(this.doLayout){this._checkIfSingleChild();}this._scheduleLayout();this._onLoadHandler(_21);}},_onError:function(_28,err,_2a){var _2b=this["on"+_28+"Error"].call(this,err);if(_2a){console.error(_2a,err);}else{if(_2b){this._setContent(_2b,true);}}},_scheduleLayout:function(){if(this._isShown()){this._layoutChildren();}else{this._needLayout=true;}},_layoutChildren:function(){if(this._singleChild&&this._singleChild.resize){var cb=this._contentBox||_4.contentBox(this.containerNode);this._singleChild.resize({w:cb.w,h:cb.h});}else{_4.forEach(this.getChildren(),function(_2d){if(_2d.resize){_2d.resize();}});}delete this._needLayout;},onLoad:function(_2e){},onUnload:function(){},onDownloadStart:function(){return this.loadingMessage;},onContentError:function(_2f){},onDownloadError:function(_30){return this.errorMessage;},onDownloadEnd:function(){}});}}};});