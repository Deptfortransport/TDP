dependencies = {
	stripConsole: "normal",  //  strips all 'console.log' statements etc...
	internStrings: true,  //  draws in all dijit template html files and stores them as template strings
	expandProvide: true,  //  expands provide statements - increases speed execution of script
	layers: [
		{  //  this layer will be discarded 
			name: "esri.discard",  //  name is only needed for list of layerDependencies in next layer
			discard: true,  //  discard it
			dependencies: [
				//  following is a list of modules used by the esri js api
				"dijit._base.manager",
				"dijit._Widget",
				"dijit._Templated",
				"dijit._Container",
				"dijit.form.HorizontalSlider",
				"dijit.form.VerticalSlider",
				"dijit.form.SliderRule",
				"dijit.form.SliderRuleLabels",
				"dojox.gfx",
				"dojox.xml.parser",
				"dojox.collections.ArrayList",
				//  include own modules here that are not required for esriuk.js build
				//  users can then edit it after building the app
				"ESRIUK.Config" 
			]
		},
		{
			name: "../esriuk.js",  //  the name of our output layer/file
			copyrightFile: "../../../esriukBuildNotice.txt",  //  our custom copyright notice to add to top of files
			layerDependencies:[ 
				//  this means all modules loaded in 'esri.discard' are assumed to exist and are not added here
				"esri.discard" 
			],
			dependencies:[ 
				//  "ESRIUK.Dijits.Map" requires all other ESRIUK modules via 'dojo.require'
				//  therefore, only need to include this one
				"ESRIUK.Dijits.Map" 
			]
		}    
	],
	prefixes: [
		[ "dijit", "../dijit" ],  //  we use references in the dijit namespace
		[ "dojox", "../dojox" ],   //  we use references in the dojox namespace
		[ "ESRIUK", "../../../ESRIUK"]  // esriuk own namespace
	]
}

