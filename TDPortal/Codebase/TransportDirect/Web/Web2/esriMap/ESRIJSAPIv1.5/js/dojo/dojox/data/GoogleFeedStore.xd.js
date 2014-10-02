/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.GoogleFeedStore"],["require","dojox.data.GoogleSearchStore"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.GoogleFeedStore"]){_4._hasResource["dojox.data.GoogleFeedStore"]=true;_4.provide("dojox.data.GoogleFeedStore");_4.experimental("dojox.data.GoogleFeedStore");_4.require("dojox.data.GoogleSearchStore");_4.declare("dojox.data.GoogleFeedStore",_6.data.GoogleSearchStore,{_type:"",_googleUrl:"http://ajax.googleapis.com/ajax/services/feed/load",_attributes:["title","link","author","published","content","summary","categories"],_queryAttr:"url",_processItem:function(_7,_8){this.inherited(arguments);_7["summary"]=_7["contentSnippet"];_7["published"]=_7["publishedDate"];},_getItems:function(_9){return _9["feed"]&&_9.feed[["entries"]]?_9.feed[["entries"]]:null;},_createContent:function(_a,_b,_c){var cb=this.inherited(arguments);cb.num=(_c.count||10)+(_c.start||0);return cb;}});}}};});