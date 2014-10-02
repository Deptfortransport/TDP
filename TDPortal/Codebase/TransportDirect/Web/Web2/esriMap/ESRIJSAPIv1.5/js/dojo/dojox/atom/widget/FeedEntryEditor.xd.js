/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.atom.widget.FeedEntryEditor"],["require","dojox.atom.widget.FeedEntryViewer"],["require","dijit._Widget"],["require","dijit._Templated"],["require","dijit._Container"],["require","dijit.Editor"],["require","dijit.form.TextBox"],["require","dijit.form.SimpleTextarea"],["requireLocalization","dojox.atom.widget","FeedEntryEditor",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"],["requireLocalization","dojox.atom.widget","PeopleEditor",null,"ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw","ROOT,ar,ca,cs,da,de,el,es,fi,fr,he,hu,it,ja,ko,nb,nl,pl,pt,pt-pt,ru,sk,sl,sv,th,tr,zh,zh-tw"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.atom.widget.FeedEntryEditor"]){_4._hasResource["dojox.atom.widget.FeedEntryEditor"]=true;_4.provide("dojox.atom.widget.FeedEntryEditor");_4.require("dojox.atom.widget.FeedEntryViewer");_4.require("dijit._Widget");_4.require("dijit._Templated");_4.require("dijit._Container");_4.require("dijit.Editor");_4.require("dijit.form.TextBox");_4.require("dijit.form.SimpleTextarea");_4.experimental("dojox.atom.widget.FeedEntryEditor");_4.declare("dojox.atom.widget.FeedEntryEditor",_6.atom.widget.FeedEntryViewer,{_contentEditor:null,_oldContent:null,_setObject:null,enableEdit:false,_contentEditorCreator:null,_editors:{},entryNewButton:null,_editable:false,templateString:"<div class=\"feedEntryViewer\">\r\n    <table border=\"0\" width=\"100%\" class=\"feedEntryViewerMenuTable\" dojoAttachPoint=\"feedEntryViewerMenu\" style=\"display: none;\">\r\n        <tr width=\"100%\"  dojoAttachPoint=\"entryCheckBoxDisplayOptions\">\r\n        \t<td align=\"left\" dojoAttachPoint=\"entryNewButton\">\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"doNew\" dojoAttachEvent=\"onclick:_toggleNew\"></span>\r\n        \t</td>\r\n            <td align=\"left\" dojoAttachPoint=\"entryEditButton\" style=\"display: none;\">\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"edit\" dojoAttachEvent=\"onclick:_toggleEdit\"></span>\r\n            </td>\r\n            <td align=\"left\" dojoAttachPoint=\"entrySaveCancelButtons\" style=\"display: none;\">\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"save\" dojoAttachEvent=\"onclick:saveEdits\"></span>\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"cancel\" dojoAttachEvent=\"onclick:cancelEdits\"></span>\r\n            </td>\r\n            <td align=\"right\">\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"displayOptions\" dojoAttachEvent=\"onclick:_toggleOptions\"></span>\r\n            </td>\r\n        </tr>\r\n        <tr class=\"feedEntryViewerDisplayCheckbox\" dojoAttachPoint=\"entryCheckBoxRow\" width=\"100%\" style=\"display: none;\">\r\n            <td dojoAttachPoint=\"feedEntryCelltitle\">\r\n                <input type=\"checkbox\" name=\"title\" value=\"Title\" dojoAttachPoint=\"feedEntryCheckBoxTitle\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelTitle\"></label>\r\n            </td>\r\n            <td dojoAttachPoint=\"feedEntryCellauthors\">\r\n                <input type=\"checkbox\" name=\"authors\" value=\"Authors\" dojoAttachPoint=\"feedEntryCheckBoxAuthors\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelAuthors\"></label>\r\n            </td>\r\n            <td dojoAttachPoint=\"feedEntryCellcontributors\">\r\n                <input type=\"checkbox\" name=\"contributors\" value=\"Contributors\" dojoAttachPoint=\"feedEntryCheckBoxContributors\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelContributors\"></label>\r\n            </td>\r\n            <td dojoAttachPoint=\"feedEntryCellid\">\r\n                <input type=\"checkbox\" name=\"id\" value=\"Id\" dojoAttachPoint=\"feedEntryCheckBoxId\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelId\"></label>\r\n            </td>\r\n            <td rowspan=\"2\" align=\"right\">\r\n                <span class=\"feedEntryViewerMenu\" dojoAttachPoint=\"close\" dojoAttachEvent=\"onclick:_toggleOptions\"></span>\r\n            </td>\r\n\t\t</tr>\r\n\t\t<tr class=\"feedEntryViewerDisplayCheckbox\" dojoAttachPoint=\"entryCheckBoxRow2\" width=\"100%\" style=\"display: none;\">\r\n            <td dojoAttachPoint=\"feedEntryCellupdated\">\r\n                <input type=\"checkbox\" name=\"updated\" value=\"Updated\" dojoAttachPoint=\"feedEntryCheckBoxUpdated\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelUpdated\"></label>\r\n            </td>\r\n            <td dojoAttachPoint=\"feedEntryCellsummary\">\r\n                <input type=\"checkbox\" name=\"summary\" value=\"Summary\" dojoAttachPoint=\"feedEntryCheckBoxSummary\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelSummary\"></label>\r\n            </td>\r\n            <td dojoAttachPoint=\"feedEntryCellcontent\">\r\n                <input type=\"checkbox\" name=\"content\" value=\"Content\" dojoAttachPoint=\"feedEntryCheckBoxContent\" dojoAttachEvent=\"onclick:_toggleCheckbox\"/>\r\n\t\t\t\t<label for=\"title\" dojoAttachPoint=\"feedEntryCheckBoxLabelContent\"></label>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    \r\n    <table class=\"feedEntryViewerContainer\" border=\"0\" width=\"100%\">\r\n        <tr class=\"feedEntryViewerTitle\" dojoAttachPoint=\"entryTitleRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryTitleHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>\r\n                        \t<select dojoAttachPoint=\"entryTitleSelect\" dojoAttachEvent=\"onchange:_switchEditor\" style=\"display: none\">\r\n                        \t\t<option value=\"text\">Text</option>\r\n\t\t\t\t\t\t\t\t<option value=\"html\">HTML</option>\r\n\t\t\t\t\t\t\t\t<option value=\"xhtml\">XHTML</option>\r\n                        \t</select>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td colspan=\"2\" dojoAttachPoint=\"entryTitleNode\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr class=\"feedEntryViewerAuthor\" dojoAttachPoint=\"entryAuthorRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryAuthorHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entryAuthorNode\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr class=\"feedEntryViewerContributor\" dojoAttachPoint=\"entryContributorRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryContributorHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entryContributorNode\" class=\"feedEntryViewerContributorNames\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        \r\n        <tr class=\"feedEntryViewerId\" dojoAttachPoint=\"entryIdRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryIdHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entryIdNode\" class=\"feedEntryViewerIdText\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    \r\n        <tr class=\"feedEntryViewerUpdated\" dojoAttachPoint=\"entryUpdatedRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryUpdatedHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entryUpdatedNode\" class=\"feedEntryViewerUpdatedText\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    \r\n        <tr class=\"feedEntryViewerSummary\" dojoAttachPoint=\"entrySummaryRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\" colspan=\"2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entrySummaryHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>\r\n                        \t<select dojoAttachPoint=\"entrySummarySelect\" dojoAttachEvent=\"onchange:_switchEditor\" style=\"display: none\">\r\n                        \t\t<option value=\"text\">Text</option>\r\n\t\t\t\t\t\t\t\t<option value=\"html\">HTML</option>\r\n\t\t\t\t\t\t\t\t<option value=\"xhtml\">XHTML</option>\r\n                        \t</select>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entrySummaryNode\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    \r\n        <tr class=\"feedEntryViewerContent\" dojoAttachPoint=\"entryContentRow\" style=\"display: none;\">\r\n            <td>\r\n                <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                    <tr class=\"graphic-tab-lgray\">\r\n\t\t\t\t\t\t<td class=\"lp2\">\r\n\t\t\t\t\t\t\t<span class=\"lp\" dojoAttachPoint=\"entryContentHeader\"></span>\r\n\t\t\t\t\t\t</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>\r\n                        \t<select dojoAttachPoint=\"entryContentSelect\" dojoAttachEvent=\"onchange:_switchEditor\" style=\"display: none\">\r\n                        \t\t<option value=\"text\">Text</option>\r\n\t\t\t\t\t\t\t\t<option value=\"html\">HTML</option>\r\n\t\t\t\t\t\t\t\t<option value=\"xhtml\">XHTML</option>\r\n                        \t</select>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td dojoAttachPoint=\"entryContentNode\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</div>\r\n",postCreate:function(){if(this.entrySelectionTopic!==""){this._subscriptions=[_4.subscribe(this.entrySelectionTopic,this,"_handleEvent")];}var _7=_4.i18n.getLocalization("dojox.atom.widget","FeedEntryViewer");this.displayOptions.innerHTML=_7.displayOptions;this.feedEntryCheckBoxLabelTitle.innerHTML=_7.title;this.feedEntryCheckBoxLabelAuthors.innerHTML=_7.authors;this.feedEntryCheckBoxLabelContributors.innerHTML=_7.contributors;this.feedEntryCheckBoxLabelId.innerHTML=_7.id;this.close.innerHTML=_7.close;this.feedEntryCheckBoxLabelUpdated.innerHTML=_7.updated;this.feedEntryCheckBoxLabelSummary.innerHTML=_7.summary;this.feedEntryCheckBoxLabelContent.innerHTML=_7.content;_7=_4.i18n.getLocalization("dojox.atom.widget","FeedEntryEditor");this.doNew.innerHTML=_7.doNew;this.edit.innerHTML=_7.edit;this.save.innerHTML=_7.save;this.cancel.innerHTML=_7.cancel;},setEntry:function(_8,_9,_a){if(this._entry!==_8){this._editMode=false;_a=false;}else{_a=true;}_6.atom.widget.FeedEntryEditor.superclass.setEntry.call(this,_8,_9);this._editable=this._isEditable(_8);if(!_a&&!this._editable){_4.style(this.entryEditButton,"display","none");_4.style(this.entrySaveCancelButtons,"display","none");}if(this._editable&&this.enableEdit){if(!_a){_4.style(this.entryEditButton,"display","");if(this.enableMenuFade&&this.entrySaveCancelButton){_4.fadeOut({node:this.entrySaveCancelButton,duration:250}).play();}}}},_toggleEdit:function(){if(this._editable&&this.enableEdit){_4.style(this.entryEditButton,"display","none");_4.style(this.entrySaveCancelButtons,"display","");this._editMode=true;this.setEntry(this._entry,this._feed,true);}},_handleEvent:function(_b){if(_b.source!=this&&_b.action=="delete"&&_b.entry&&_b.entry==this._entry){_4.style(this.entryEditButton,"display","none");}_6.atom.widget.FeedEntryEditor.superclass._handleEvent.call(this,_b);},_isEditable:function(_c){var _d=false;if(_c&&_c!==null&&_c.links&&_c.links!==null){for(var x in _c.links){if(_c.links[x].rel&&_c.links[x].rel=="edit"){_d=true;break;}}}return _d;},setTitle:function(_f,_10,_11){if(!_10){_6.atom.widget.FeedEntryEditor.superclass.setTitle.call(this,_f,_10,_11);if(_11.title&&_11.title.value&&_11.title.value!==null){this.setFieldValidity("title",true);}}else{if(_11.title&&_11.title.value&&_11.title.value!==null){if(!this._toLoad){this._toLoad=[];}this.entryTitleSelect.value=_11.title.type;var _12=this._createEditor(_f,_11.title,true,_11.title.type==="html"||_11.title.type==="xhtml");_12.name="title";this._toLoad.push(_12);this.setFieldValidity("titleedit",true);this.setFieldValidity("title",true);}}},setAuthors:function(_13,_14,_15){if(!_14){_6.atom.widget.FeedEntryEditor.superclass.setAuthors.call(this,_13,_14,_15);if(_15.authors&&_15.authors.length>0){this.setFieldValidity("authors",true);}}else{if(_15.authors&&_15.authors.length>0){this._editors.authors=this._createPeopleEditor(this.entryAuthorNode,{data:_15.authors,name:"Author"});this.setFieldValidity("authors",true);}}},setContributors:function(_16,_17,_18){if(!_17){_6.atom.widget.FeedEntryEditor.superclass.setContributors.call(this,_16,_17,_18);if(_18.contributors&&_18.contributors.length>0){this.setFieldValidity("contributors",true);}}else{if(_18.contributors&&_18.contributors.length>0){this._editors.contributors=this._createPeopleEditor(this.entryContributorNode,{data:_18.contributors,name:"Contributor"});this.setFieldValidity("contributors",true);}}},setId:function(_19,_1a,_1b){if(!_1a){_6.atom.widget.FeedEntryEditor.superclass.setId.call(this,_19,_1a,_1b);if(_1b.id&&_1b.id!==null){this.setFieldValidity("id",true);}}else{if(_1b.id&&_1b.id!==null){this._editors.id=this._createEditor(_19,_1b.id);this.setFieldValidity("id",true);}}},setUpdated:function(_1c,_1d,_1e){if(!_1d){_6.atom.widget.FeedEntryEditor.superclass.setUpdated.call(this,_1c,_1d,_1e);if(_1e.updated&&_1e.updated!==null){this.setFieldValidity("updated",true);}}else{if(_1e.updated&&_1e.updated!==null){this._editors.updated=this._createEditor(_1c,_1e.updated);this.setFieldValidity("updated",true);}}},setSummary:function(_1f,_20,_21){if(!_20){_6.atom.widget.FeedEntryEditor.superclass.setSummary.call(this,_1f,_20,_21);if(_21.summary&&_21.summary.value&&_21.summary.value!==null){this.setFieldValidity("summary",true);}}else{if(_21.summary&&_21.summary.value&&_21.summary.value!==null){if(!this._toLoad){this._toLoad=[];}this.entrySummarySelect.value=_21.summary.type;var _22=this._createEditor(_1f,_21.summary,true,_21.summary.type==="html"||_21.summary.type==="xhtml");_22.name="summary";this._toLoad.push(_22);this.setFieldValidity("summaryedit",true);this.setFieldValidity("summary",true);}}},setContent:function(_23,_24,_25){if(!_24){_6.atom.widget.FeedEntryEditor.superclass.setContent.call(this,_23,_24,_25);if(_25.content&&_25.content.value&&_25.content.value!==null){this.setFieldValidity("content",true);}}else{if(_25.content&&_25.content.value&&_25.content.value!==null){if(!this._toLoad){this._toLoad=[];}this.entryContentSelect.value=_25.content.type;var _26=this._createEditor(_23,_25.content,true,_25.content.type==="html"||_25.content.type==="xhtml");_26.name="content";this._toLoad.push(_26);this.setFieldValidity("contentedit",true);this.setFieldValidity("content",true);}}},_createEditor:function(_27,_28,_29,rte){var _2b;var box;if(!_28){if(rte){return {anchorNode:_27,entryValue:"",editor:null,generateEditor:function(){var _2d=document.createElement("div");_2d.innerHTML=this.entryValue;this.anchorNode.appendChild(_2d);var _2e=new _5.Editor({},_2d);this.editor=_2e;return _2e;}};}if(_29){_2b=document.createElement("textarea");_27.appendChild(_2b);_4.style(_2b,"width","90%");box=new _5.form.SimpleTextarea({},_2b);}else{_2b=document.createElement("input");_27.appendChild(_2b);_4.style(_2b,"width","95%");box=new _5.form.TextBox({},_2b);}box.attr("value","");return box;}var _2f;if(_28.value!==undefined){_2f=_28.value;}else{if(_28.attr){_2f=_28.attr("value");}else{_2f=_28;}}if(rte){if(_2f.indexOf("<")!=-1){_2f=_2f.replace(/</g,"&lt;");}return {anchorNode:_27,entryValue:_2f,editor:null,generateEditor:function(){var _30=document.createElement("div");_30.innerHTML=this.entryValue;this.anchorNode.appendChild(_30);var _31=new _5.Editor({},_30);this.editor=_31;return _31;}};}if(_29){_2b=document.createElement("textarea");_27.appendChild(_2b);_4.style(_2b,"width","90%");box=new _5.form.SimpleTextarea({},_2b);}else{_2b=document.createElement("input");_27.appendChild(_2b);_4.style(_2b,"width","95%");box=new _5.form.TextBox({},_2b);}box.attr("value",_2f);return box;},_switchEditor:function(_32){var _33=null;var _34=null;var _35=null;if(_4.isIE){_34=_32.srcElement;}else{_34=_32.target;}if(_34===this.entryTitleSelect){_35=this.entryTitleNode;_33="title";}else{if(_34===this.entrySummarySelect){_35=this.entrySummaryNode;_33="summary";}else{_35=this.entryContentNode;_33="content";}}var _36=this._editors[_33];var _37;var _38;if(_34.value==="text"){if(_36.declaredClass==="dijit.Editor"){_38=_36.attr("value",false);_36.close(false,true);_36.destroy();while(_35.firstChild){_4.destroy(_35.firstChild);}_37=this._createEditor(_35,{value:_38},true,false);this._editors[_33]=_37;}}else{if(_36.declaredClass!="dijit.Editor"){_38=_36.attr("value");_36.destroy();while(_35.firstChild){_4.destroy(_35.firstChild);}_37=this._createEditor(_35,{value:_38},true,true);_37=_4.hitch(_37,_37.generateEditor)();this._editors[_33]=_37;}}},_createPeopleEditor:function(_39,_3a){var _3b=document.createElement("div");_39.appendChild(_3b);return new _6.atom.widget.PeopleEditor(_3a,_3b);},saveEdits:function(){_4.style(this.entrySaveCancelButtons,"display","none");_4.style(this.entryEditButton,"display","");_4.style(this.entryNewButton,"display","");var _3c=false;var _3d;var i;var _3f;var _40;var _41;var _42;if(!this._new){_40=this.getEntry();if(this._editors.title&&(this._editors.title.attr("value")!=_40.title.value||this.entryTitleSelect.value!=_40.title.type)){_3d=this._editors.title.attr("value");if(this.entryTitleSelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);if(_3d.indexOf("<div xmlns=\"http://www.w3.org/1999/xhtml\">")!==0){_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}}_40.title=new _6.atom.io.model.Content("title",_3d,null,this.entryTitleSelect.value);_3c=true;}if(this._editors.id.attr("value")!=_40.id){_40.id=this._editors.id.attr("value");_3c=true;}if(this._editors.summary&&(this._editors.summary.attr("value")!=_40.summary.value||this.entrySummarySelect.value!=_40.summary.type)){_3d=this._editors.summary.attr("value");if(this.entrySummarySelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);if(_3d.indexOf("<div xmlns=\"http://www.w3.org/1999/xhtml\">")!==0){_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}}_40.summary=new _6.atom.io.model.Content("summary",_3d,null,this.entrySummarySelect.value);_3c=true;}if(this._editors.content&&(this._editors.content.attr("value")!=_40.content.value||this.entryContentSelect.value!=_40.content.type)){_3d=this._editors.content.attr("value");if(this.entryContentSelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);if(_3d.indexOf("<div xmlns=\"http://www.w3.org/1999/xhtml\">")!==0){_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}}_40.content=new _6.atom.io.model.Content("content",_3d,null,this.entryContentSelect.value);_3c=true;}if(this._editors.authors){if(_3c){_40.authors=[];_41=this._editors.authors.getValues();for(i in _41){if(_41[i].name||_41[i].email||_41[i].uri){_40.addAuthor(_41[i].name,_41[i].email,_41[i].uri);}}}else{var _43=_40.authors;var _44=function(_45,_46,uri){for(i in _43){if(_43[i].name===_45&&_43[i].email===_46&&_43[i].uri===uri){return true;}}return false;};_41=this._editors.authors.getValues();_3f=false;for(i in _41){if(!_44(_41[i].name,_41[i].email,_41[i].uri)){_3f=true;break;}}if(_3f){_40.authors=[];for(i in _41){if(_41[i].name||_41[i].email||_41[i].uri){_40.addAuthor(_41[i].name,_41[i].email,_41[i].uri);}}_3c=true;}}}if(this._editors.contributors){if(_3c){_40.contributors=[];_42=this._editors.contributors.getValues();for(i in _42){if(_42[i].name||_42[i].email||_42[i].uri){_40.addAuthor(_42[i].name,_42[i].email,_42[i].uri);}}}else{var _48=_40.contributors;var _49=function(_4a,_4b,uri){for(i in _48){if(_48[i].name===_4a&&_48[i].email===_4b&&_48[i].uri===uri){return true;}}return false;};_42=this._editors.contributors.getValues();_3f=false;for(i in _42){if(_49(_42[i].name,_42[i].email,_42[i].uri)){_3f=true;break;}}if(_3f){_40.contributors=[];for(i in _42){if(_42[i].name||_42[i].email||_42[i].uri){_40.addContributor(_42[i].name,_42[i].email,_42[i].uri);}}_3c=true;}}}if(_3c){_4.publish(this.entrySelectionTopic,[{action:"update",source:this,entry:_40,callback:this._handleSave}]);}}else{this._new=false;_40=new _6.atom.io.model.Entry();_3d=this._editors.title.attr("value");if(this.entryTitleSelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}_40.setTitle(_3d,this.entryTitleSelect.value);_40.id=this._editors.id.attr("value");_41=this._editors.authors.getValues();for(i in _41){if(_41[i].name||_41[i].email||_41[i].uri){_40.addAuthor(_41[i].name,_41[i].email,_41[i].uri);}}_42=this._editors.contributors.getValues();for(i in _42){if(_42[i].name||_42[i].email||_42[i].uri){_40.addContributor(_42[i].name,_42[i].email,_42[i].uri);}}_3d=this._editors.summary.attr("value");if(this.entrySummarySelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}_40.summary=new _6.atom.io.model.Content("summary",_3d,null,this.entrySummarySelect.value);_3d=this._editors.content.attr("value");if(this.entryContentSelect.value==="xhtml"){_3d=this._enforceXhtml(_3d);_3d="<div xmlns=\"http://www.w3.org/1999/xhtml\">"+_3d+"</div>";}_40.content=new _6.atom.io.model.Content("content",_3d,null,this.entryContentSelect.value);_4.style(this.entryNewButton,"display","");_4.publish(this.entrySelectionTopic,[{action:"post",source:this,entry:_40}]);}this._editMode=false;this.setEntry(_40,this._feed,true);},_handleSave:function(_4d,_4e){this._editMode=false;this.clear();this.setEntry(_4d,this.getFeed(),true);},cancelEdits:function(){this._new=false;_4.style(this.entrySaveCancelButtons,"display","none");if(this._editable){_4.style(this.entryEditButton,"display","");}_4.style(this.entryNewButton,"display","");this._editMode=false;this.clearEditors();this.setEntry(this.getEntry(),this.getFeed(),true);},clear:function(){this._editable=false;this.clearEditors();_6.atom.widget.FeedEntryEditor.superclass.clear.apply(this);if(this._contentEditor){this._contentEditor=this._setObject=this._oldContent=this._contentEditorCreator=null;this._editors={};}},clearEditors:function(){for(var key in this._editors){if(this._editors[key].declaredClass==="dijit.Editor"){this._editors[key].close(false,true);}this._editors[key].destroy();}this._editors={};},_enforceXhtml:function(_50){var _51=null;if(_50){var _52=/<br>/g;_51=_50.replace(_52,"<br/>");_51=this._closeTag(_51,"hr");_51=this._closeTag(_51,"img");}return _51;},_closeTag:function(_53,tag){var _55="<"+tag;var _56=_53.indexOf(_55);if(_56!==-1){while(_56!==-1){var _57="";var _58=false;for(var i=0;i<_53.length;i++){var c=_53.charAt(i);if(i<=_56||_58){_57+=c;}else{if(c===">"){_57+="/";_58=true;}_57+=c;}}_53=_57;_56=_53.indexOf(_55,_56+1);}}return _53;},_toggleNew:function(){_4.style(this.entryNewButton,"display","none");_4.style(this.entryEditButton,"display","none");_4.style(this.entrySaveCancelButtons,"display","");this.entrySummarySelect.value="text";this.entryContentSelect.value="text";this.entryTitleSelect.value="text";this.clearNodes();this._new=true;var _5b=_4.i18n.getLocalization("dojox.atom.widget","FeedEntryViewer");var _5c=new _6.atom.widget.EntryHeader({title:_5b.title});this.entryTitleHeader.appendChild(_5c.domNode);this._editors.title=this._createEditor(this.entryTitleNode,null);this.setFieldValidity("title",true);var _5d=new _6.atom.widget.EntryHeader({title:_5b.authors});this.entryAuthorHeader.appendChild(_5d.domNode);this._editors.authors=this._createPeopleEditor(this.entryAuthorNode,{name:"Author"});this.setFieldValidity("authors",true);var _5e=new _6.atom.widget.EntryHeader({title:_5b.contributors});this.entryContributorHeader.appendChild(_5e.domNode);this._editors.contributors=this._createPeopleEditor(this.entryContributorNode,{name:"Contributor"});this.setFieldValidity("contributors",true);var _5f=new _6.atom.widget.EntryHeader({title:_5b.id});this.entryIdHeader.appendChild(_5f.domNode);this._editors.id=this._createEditor(this.entryIdNode,null);this.setFieldValidity("id",true);var _60=new _6.atom.widget.EntryHeader({title:_5b.updated});this.entryUpdatedHeader.appendChild(_60.domNode);this._editors.updated=this._createEditor(this.entryUpdatedNode,null);this.setFieldValidity("updated",true);var _61=new _6.atom.widget.EntryHeader({title:_5b.summary});this.entrySummaryHeader.appendChild(_61.domNode);this._editors.summary=this._createEditor(this.entrySummaryNode,null,true);this.setFieldValidity("summaryedit",true);this.setFieldValidity("summary",true);var _62=new _6.atom.widget.EntryHeader({title:_5b.content});this.entryContentHeader.appendChild(_62.domNode);this._editors.content=this._createEditor(this.entryContentNode,null,true);this.setFieldValidity("contentedit",true);this.setFieldValidity("content",true);this._displaySections();},_displaySections:function(){_4.style(this.entrySummarySelect,"display","none");_4.style(this.entryContentSelect,"display","none");_4.style(this.entryTitleSelect,"display","none");if(this.isFieldValid("contentedit")){_4.style(this.entryContentSelect,"display","");}if(this.isFieldValid("summaryedit")){_4.style(this.entrySummarySelect,"display","");}if(this.isFieldValid("titleedit")){_4.style(this.entryTitleSelect,"display","");}_6.atom.widget.FeedEntryEditor.superclass._displaySections.apply(this);if(this._toLoad){for(var i in this._toLoad){var _64;if(this._toLoad[i].generateEditor){_64=_4.hitch(this._toLoad[i],this._toLoad[i].generateEditor)();}else{_64=this._toLoad[i];}this._editors[this._toLoad[i].name]=_64;this._toLoad[i]=null;}this._toLoad=null;}}});_4.declare("dojox.atom.widget.PeopleEditor",[_5._Widget,_5._Templated,_5._Container],{templateString:"<div class=\"peopleEditor\">\r\n\t<table style=\"width: 100%\">\r\n\t\t<tbody dojoAttachPoint=\"peopleEditorEditors\"></tbody>\r\n\t</table>\r\n\t<span class=\"peopleEditorButton\" dojoAttachPoint=\"peopleEditorButton\" dojoAttachEvent=\"onclick:_add\"></span>\r\n</div>\r\n",_rows:[],_editors:[],_index:0,_numRows:0,postCreate:function(){var _65=_4.i18n.getLocalization("dojox.atom.widget","PeopleEditor");if(this.name){if(this.name=="Author"){this.peopleEditorButton.appendChild(document.createTextNode("["+_65.addAuthor+"]"));}else{if(this.name=="Contributor"){this.peopleEditorButton.appendChild(document.createTextNode("["+_65.addContributor+"]"));}}}else{this.peopleEditorButton.appendChild(document.createTextNode("["+_65.add+"]"));}this._editors=[];if(!this.data||this.data.length===0){this._createEditors(null,null,null,0,this.name);this._index=1;}else{for(var i in this.data){this._createEditors(this.data[i].name,this.data[i].email,this.data[i].uri,i);this._index++;this._numRows++;}}},destroy:function(){for(var key in this._editors){for(var _68 in this._editors[key]){this._editors[key][_68].destroy();}}this._editors=[];},_createEditors:function(_69,_6a,uri,_6c,_6d){var row=document.createElement("tr");this.peopleEditorEditors.appendChild(row);row.id="removeRow"+_6c;var _6f=document.createElement("td");_6f.setAttribute("align","right");row.appendChild(_6f);_6f.colSpan=2;if(this._numRows>0){var hr=document.createElement("hr");_6f.appendChild(hr);hr.id="hr"+_6c;}row=document.createElement("span");_6f.appendChild(row);row.className="peopleEditorButton";_4.style(row,"font-size","x-small");_4.connect(row,"onclick",this,"_removeEditor");row.id="remove"+_6c;_6f=document.createTextNode("[X]");row.appendChild(_6f);row=document.createElement("tr");this.peopleEditorEditors.appendChild(row);row.id="editorsRow"+_6c;var _71=document.createElement("td");row.appendChild(_71);_4.style(_71,"width","20%");_6f=document.createElement("td");row.appendChild(_6f);row=document.createElement("table");_71.appendChild(row);_4.style(row,"width","100%");_71=document.createElement("tbody");row.appendChild(_71);row=document.createElement("table");_6f.appendChild(row);_4.style(row,"width","100%");_6f=document.createElement("tbody");row.appendChild(_6f);this._editors[_6c]=[];this._editors[_6c].push(this._createEditor(_69,_6d+"name"+_6c,"Name:",_71,_6f));this._editors[_6c].push(this._createEditor(_6a,_6d+"email"+_6c,"Email:",_71,_6f));this._editors[_6c].push(this._createEditor(uri,_6d+"uri"+_6c,"URI:",_71,_6f));},_createEditor:function(_72,id,_74,_75,_76){var row=document.createElement("tr");_75.appendChild(row);var _78=document.createElement("label");_78.setAttribute("for",id);_78.appendChild(document.createTextNode(_74));_75=document.createElement("td");_75.appendChild(_78);row.appendChild(_75);row=document.createElement("tr");_76.appendChild(row);_76=document.createElement("td");row.appendChild(_76);var _79=document.createElement("input");_79.setAttribute("id",id);_76.appendChild(_79);_4.style(_79,"width","95%");var box=new _5.form.TextBox({},_79);box.attr("value",_72);return box;},_removeEditor:function(_7b){var _7c=null;if(_4.isIE){_7c=_7b.srcElement;}else{_7c=_7b.target;}var id=_7c.id;id=id.substring(6);for(var key in this._editors[id]){this._editors[id][key].destroy();}var _7f=_4.byId("editorsRow"+id);var _80=_7f.parentNode;_80.removeChild(_7f);_7f=_4.byId("removeRow"+id);_80=_7f.parentNode;_80.removeChild(_7f);this._numRows--;if(this._numRows===1&&_80.firstChild.firstChild.firstChild.tagName.toLowerCase()==="hr"){_7f=_80.firstChild.firstChild;_7f.removeChild(_7f.firstChild);}this._editors[id]=null;},_add:function(){this._createEditors(null,null,null,this._index);this._index++;this._numRows++;},getValues:function(){var _81=[];for(var i in this._editors){if(this._editors[i]){_81.push({name:this._editors[i][0].attr("value"),email:this._editors[i][1].attr("value"),uri:this._editors[i][2].attr("value")});}}return _81;}});}}};});