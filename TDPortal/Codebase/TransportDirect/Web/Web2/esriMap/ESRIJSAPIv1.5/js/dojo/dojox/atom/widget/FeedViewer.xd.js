/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.atom.widget.FeedViewer"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dijit._Container"],["require","dojox.atom.io.Connection"],["requireLocalization","dojox.atom.widget","FeedViewerEntry",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.atom.widget.FeedViewer"]){_4._hasResource["dojox.atom.widget.FeedViewer"]=true;_4.provide("dojox.atom.widget.FeedViewer");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dijit._Container");_4.require("dojox.atom.io.Connection");_4.experimental("dojox.atom.widget.FeedViewer");_4.declare("dojox.atom.widget.FeedViewer",[_5._Widget,_5._Templated,_5._Container],{feedViewerTableBody:null,feedViewerTable:null,entrySelectionTopic:"",url:"",xmethod:false,localSaveOnly:false,templateString:"<div class=\"feedViewerContainer\" dojoAttachPoint=\"feedViewerContainerNode\">\r\n\t<table cellspacing=\"0\" cellpadding=\"0\" class=\"feedViewerTable\">\r\n\t\t<tbody dojoAttachPoint=\"feedViewerTableBody\" class=\"feedViewerTableBody\">\r\n\t\t</tbody>\r\n\t</table>\r\n</div>\r\n",_feed:null,_currentSelection:null,_includeFilters:null,alertsEnabled:false,postCreate:function(){this._includeFilters=[];if(this.entrySelectionTopic!==""){this._subscriptions=[_4.subscribe(this.entrySelectionTopic,this,"_handleEvent")];}this.atomIO=new _6.atom.io.Connection();this.childWidgets=[];},startup:function(){this.containerNode=this.feedViewerTableBody;var _7=this.getDescendants();for(var i in _7){var _9=_7[i];if(_9&&_9.isFilter){this._includeFilters.push(new _6.atom.widget.FeedViewer.CategoryIncludeFilter(_9.scheme,_9.term,_9.label));_9.destroy();}}if(this.url!==""){this.setFeedFromUrl(this.url);}},clear:function(){this.destroyDescendants();},setFeedFromUrl:function(_a){if(_a!==""){if(this._isRelativeURL(_a)){var _b="";if(_a.charAt(0)!=="/"){_b=this._calculateBaseURL(window.location.href,true);}else{_b=this._calculateBaseURL(window.location.href,false);}this.url=_b+_a;}this.atomIO.getFeed(_a,_4.hitch(this,this.setFeed));}},setFeed:function(_c){this._feed=_c;this.clear();var _d=function(a,b){var _10=this._displayDateForEntry(a);var _11=this._displayDateForEntry(b);if(_10>_11){return -1;}if(_10<_11){return 1;}return 0;};var _12=function(_13){var _14=_13.split(",");_14.pop();return _14.join(",");};var _15=_c.entries.sort(_4.hitch(this,_d));if(_c){var _16=null;for(var i=0;i<_15.length;i++){var _18=_15[i];if(this._isFilterAccepted(_18)){var _19=this._displayDateForEntry(_18);var _1a="";if(_19!==null){_1a=_12(_19.toLocaleString());if(_1a===""){_1a=""+(_19.getMonth()+1)+"/"+_19.getDate()+"/"+_19.getFullYear();}}if((_16===null)||(_16!=_1a)){this.appendGrouping(_1a);_16=_1a;}this.appendEntry(_18);}}}},_displayDateForEntry:function(_1b){if(_1b.updated){return _1b.updated;}if(_1b.modified){return _1b.modified;}if(_1b.issued){return _1b.issued;}return new Date();},appendGrouping:function(_1c){var _1d=new _6.atom.widget.FeedViewerGrouping({});_1d.setText(_1c);this.addChild(_1d);this.childWidgets.push(_1d);},appendEntry:function(_1e){var _1f=new _6.atom.widget.FeedViewerEntry({"xmethod":this.xmethod});_1f.setTitle(_1e.title.value);_1f.setTime(this._displayDateForEntry(_1e).toLocaleTimeString());_1f.entrySelectionTopic=this.entrySelectionTopic;_1f.feed=this;this.addChild(_1f);this.childWidgets.push(_1f);this.connect(_1f,"onClick","_rowSelected");_1e.domNode=_1f.entryNode;_1e._entryWidget=_1f;_1f.entry=_1e;},deleteEntry:function(_20){if(!this.localSaveOnly){this.atomIO.deleteEntry(_20.entry,_4.hitch(this,this._removeEntry,_20),null,this.xmethod);}else{this._removeEntry(_20,true);}_4.publish(this.entrySelectionTopic,[{action:"delete",source:this,entry:_20.entry}]);},_removeEntry:function(_21,_22){if(_22){var idx=_4.indexOf(this.childWidgets,_21);var _24=this.childWidgets[idx-1];var _25=this.childWidgets[idx+1];if(_24.declaredClass==="dojox.atom.widget.FeedViewerGrouping"&&(_25===undefined||_25.declaredClass==="dojox.atom.widget.FeedViewerGrouping")){_24.destroy();}_21.destroy();}else{}},_rowSelected:function(evt){var _27=evt.target;while(_27){if(_27.attributes){var _28=_27.attributes.getNamedItem("widgetid");if(_28&&_28.value.indexOf("FeedViewerEntry")!=-1){break;}}_27=_27.parentNode;}for(var i=0;i<this._feed.entries.length;i++){var _2a=this._feed.entries[i];if((_27===_2a.domNode)&&(this._currentSelection!==_2a)){_4.addClass(_2a.domNode,"feedViewerEntrySelected");_4.removeClass(_2a._entryWidget.timeNode,"feedViewerEntryUpdated");_4.addClass(_2a._entryWidget.timeNode,"feedViewerEntryUpdatedSelected");this.onEntrySelected(_2a);if(this.entrySelectionTopic!==""){_4.publish(this.entrySelectionTopic,[{action:"set",source:this,feed:this._feed,entry:_2a}]);}if(this._isEditable(_2a)){_2a._entryWidget.enableDelete();}this._deselectCurrentSelection();this._currentSelection=_2a;break;}else{if((_27===_2a.domNode)&&(this._currentSelection===_2a)){_4.publish(this.entrySelectionTopic,[{action:"delete",source:this,entry:_2a}]);this._deselectCurrentSelection();break;}}}},_deselectCurrentSelection:function(){if(this._currentSelection){_4.addClass(this._currentSelection._entryWidget.timeNode,"feedViewerEntryUpdated");_4.removeClass(this._currentSelection.domNode,"feedViewerEntrySelected");_4.removeClass(this._currentSelection._entryWidget.timeNode,"feedViewerEntryUpdatedSelected");this._currentSelection._entryWidget.disableDelete();this._currentSelection=null;}},_isEditable:function(_2b){var _2c=false;if(_2b&&_2b!==null&&_2b.links&&_2b.links!==null){for(var x in _2b.links){if(_2b.links[x].rel&&_2b.links[x].rel=="edit"){_2c=true;break;}}}return _2c;},onEntrySelected:function(_2e){},_isRelativeURL:function(url){function _30(url){var _32=false;if(url.indexOf("file://")===0){_32=true;}return _32;};function _33(url){var _35=false;if(url.indexOf("http://")===0){_35=true;}return _35;};var _36=false;if(url!==null){if(!_30(url)&&!_33(url)){_36=true;}}return _36;},_calculateBaseURL:function(_37,_38){var _39=null;if(_37!==null){var _3a=_37.indexOf("?");if(_3a!=-1){_37=_37.substring(0,_3a);}if(_38){_3a=_37.lastIndexOf("/");if((_3a>0)&&(_3a<_37.length)&&(_3a!==(_37.length-1))){_39=_37.substring(0,(_3a+1));}else{_39=_37;}}else{_3a=_37.indexOf("://");if(_3a>0){_3a=_3a+3;var _3b=_37.substring(0,_3a);var _3c=_37.substring(_3a,_37.length);_3a=_3c.indexOf("/");if((_3a<_3c.length)&&(_3a>0)){_39=_3b+_3c.substring(0,_3a);}else{_39=_3b+_3c;}}}}return _39;},_isFilterAccepted:function(_3d){var _3e=false;if(this._includeFilters&&(this._includeFilters.length>0)){for(var i=0;i<this._includeFilters.length;i++){var _40=this._includeFilters[i];if(_40.match(_3d)){_3e=true;break;}}}else{_3e=true;}return _3e;},addCategoryIncludeFilter:function(_41){if(_41){var _42=_41.scheme;var _43=_41.term;var _44=_41.label;var _45=true;if(!_42){_42=null;}if(!_43){_42=null;}if(!_44){_42=null;}if(this._includeFilters&&this._includeFilters.length>0){for(var i=0;i<this._includeFilters.length;i++){var _47=this._includeFilters[i];if((_47.term===_43)&&(_47.scheme===_42)&&(_47.label===_44)){_45=false;break;}}}if(_45){this._includeFilters.push(_6.atom.widget.FeedViewer.CategoryIncludeFilter(_42,_43,_44));}}},removeCategoryIncludeFilter:function(_48){if(_48){var _49=_48.scheme;var _4a=_48.term;var _4b=_48.label;if(!_49){_49=null;}if(!_4a){_49=null;}if(!_4b){_49=null;}var _4c=[];if(this._includeFilters&&this._includeFilters.length>0){for(var i=0;i<this._includeFilters.length;i++){var _4e=this._includeFilters[i];if(!((_4e.term===_4a)&&(_4e.scheme===_49)&&(_4e.label===_4b))){_4c.push(_4e);}}this._includeFilters=_4c;}}},_handleEvent:function(_4f){if(_4f.source!=this){if(_4f.action=="update"&&_4f.entry){var evt=_4f;if(!this.localSaveOnly){this.atomIO.updateEntry(evt.entry,_4.hitch(evt.source,evt.callback),null,true);}this._currentSelection._entryWidget.setTime(this._displayDateForEntry(evt.entry).toLocaleTimeString());this._currentSelection._entryWidget.setTitle(evt.entry.title.value);}else{if(_4f.action=="post"&&_4f.entry){if(!this.localSaveOnly){this.atomIO.addEntry(_4f.entry,this.url,_4.hitch(this,this._addEntry));}else{this._addEntry(_4f.entry);}}}}},_addEntry:function(_51){this._feed.addEntry(_51);this.setFeed(this._feed);_4.publish(this.entrySelectionTopic,[{action:"set",source:this,feed:this._feed,entry:_51}]);},destroy:function(){this.clear();_4.forEach(this._subscriptions,_4.unsubscribe);}});_4.declare("dojox.atom.widget.FeedViewerEntry",[_5._Widget,_5._Templated],{templateString:"<tr class=\"feedViewerEntry\" dojoAttachPoint=\"entryNode\" dojoAttachEvent=\"onclick:onClick\">\r\n    <td class=\"feedViewerEntryUpdated\" dojoAttachPoint=\"timeNode\">\r\n    </td>\r\n    <td>\r\n        <table border=\"0\" width=\"100%\" dojoAttachPoint=\"titleRow\">\r\n            <tr padding=\"0\" border=\"0\">\r\n                <td class=\"feedViewerEntryTitle\" dojoAttachPoint=\"titleNode\">\r\n                </td>\r\n                <td class=\"feedViewerEntryDelete\" align=\"right\">\r\n                    <span dojoAttachPoint=\"deleteButton\" dojoAttachEvent=\"onclick:deleteEntry\" class=\"feedViewerDeleteButton\" style=\"display:none;\">[delete]</span>\r\n                </td>\r\n            <tr>\r\n        </table>\r\n    </td>\r\n</tr>\r\n",entryNode:null,timeNode:null,deleteButton:null,entry:null,feed:null,postCreate:function(){var _52=_4.i18n.getLocalization("dojox.atom.widget","FeedViewerEntry");this.deleteButton.innerHTML=_52.deleteButton;},setTitle:function(_53){if(this.titleNode.lastChild){this.titleNode.removeChild(this.titleNode.lastChild);}var _54=document.createElement("div");_54.innerHTML=_53;this.titleNode.appendChild(_54);},setTime:function(_55){if(this.timeNode.lastChild){this.timeNode.removeChild(this.timeNode.lastChild);}var _56=document.createTextNode(_55);this.timeNode.appendChild(_56);},enableDelete:function(){if(this.deleteButton!==null){this.deleteButton.style.display="inline";}},disableDelete:function(){if(this.deleteButton!==null){this.deleteButton.style.display="none";}},deleteEntry:function(_57){_57.preventDefault();_57.stopPropagation();this.feed.deleteEntry(this);},onClick:function(e){}});_4.declare("dojox.atom.widget.FeedViewerGrouping",[_5._Widget,_5._Templated],{templateString:"<tr dojoAttachPoint=\"groupingNode\" class=\"feedViewerGrouping\">\r\n\t<td colspan=\"2\" dojoAttachPoint=\"titleNode\" class=\"feedViewerGroupingTitle\">\r\n\t</td>\r\n</tr>\r\n",groupingNode:null,titleNode:null,setText:function(_59){if(this.titleNode.lastChild){this.titleNode.removeChild(this.titleNode.lastChild);}var _5a=document.createTextNode(_59);this.titleNode.appendChild(_5a);}});_4.declare("dojox.atom.widget.AtomEntryCategoryFilter",[_5._Widget,_5._Templated],{scheme:"",term:"",label:"",isFilter:true});_4.declare("dojox.atom.widget.FeedViewer.CategoryIncludeFilter",null,{constructor:function(_5b,_5c,_5d){this.scheme=_5b;this.term=_5c;this.label=_5d;},match:function(_5e){var _5f=false;if(_5e!==null){var _60=_5e.categories;if(_60!==null){for(var i=0;i<_60.length;i++){var _62=_60[i];if(this.scheme!==""){if(this.scheme!==_62.scheme){break;}}if(this.term!==""){if(this.term!==_62.term){break;}}if(this.label!==""){if(this.label!==_62.label){break;}}_5f=true;}}}return _5f;}});}}};});