/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.data.demos.widgets.FlickrViewList"],["require","dojox.dtl._Templated"],["require","dijit._Widget"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.data.demos.widgets.FlickrViewList"]){_4._hasResource["dojox.data.demos.widgets.FlickrViewList"]=true;_4.provide("dojox.data.demos.widgets.FlickrViewList");_4.require("dojox.dtl._Templated");_4.require("dijit._Widget");_4.declare("dojox.data.demos.widgets.FlickrViewList",[_5._Widget,_6.dtl._Templated],{store:null,items:null,templateString:"{% load dojox.dtl.contrib.data %}\r\n{% bind_data items to store as flickr %}\r\n<div dojoAttachPoint=\"list\">\r\n\t{% for item in flickr %}\r\n\t<div style=\"display: inline-block; align: top;\">\r\n\t\t<h5>{{ item.title }}</h5>\r\n\t\t<a href=\"{{ item.link }}\" style=\"border: none;\">\r\n\t\t\t<img src=\"{{ item.imageUrlMedium }}\">\r\n\t\t</a>\r\n\t\t<p>{{ item.author }}</p>\r\n\r\n\t\t<!--\r\n\t\t<img src=\"{{ item.imageUrl }}\">\r\n\t\t<p>{{ item.imageUrl }}</p>\r\n\t\t<img src=\"{{ item.imageUrlSmall }}\">\r\n\t\t-->\r\n\t</div>\r\n\t{% endfor %}\r\n</div>\r\n\r\n",fetch:function(_7){_7.onComplete=_4.hitch(this,"onComplete");_7.onError=_4.hitch(this,"onError");return this.store.fetch(_7);},onError:function(){console.trace();this.items=[];this.render();},onComplete:function(_8,_9){this.items=_8||[];this.render();}});}}};});