// *********************************************** 
// NAME                 : BuildLanguage.js
// AUTHOR               : Callum Shillan
// DATE CREATED         : 01/07/03
// DESCRIPTION			: Builds Welsh language satellite assembly
//
// ************************************************ 
/*
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ResourceManager/Resources/BuildLanguage.js-arc  $
//
//   Rev 1.0   Nov 08 2007 12:45:48   mturner
//Initial revision.
//
//   Rev 1.2   Feb 02 2006 11:02:00   mdambrine
//removed the commented code. 
//
//   Rev 1.1   Jan 27 2006 16:07:08   mdambrine
//fixed bug on folder
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 11 2006 12:12:06   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

   Rev 1.4   Oct 11 2004 14:39:24   jmorrissey
Updated header layout to fix PVCS bug with this filetype

//   Rev 1.3   Oct 11 2004 14:28:28   jmorrissey
// Sanjeev Chand  - Fix for handling multiple resource file

*/

// Check we have the correct number of command line parameters
if ( WScript.Arguments.length < 4 )
{
	// WScript.Echo ( "Usage: buildLanguage sourceResource culture targetResource referenceTemplate" );
	WScript.Echo ( "Usage: buildLanguage culture targetResource referenceTemplate sourceResource_1 sourceResource_2 .... sourceResource_n" )
	WScript.Quit( 1 );
}

//
// Assign the command line parameters
// <Javascript file> <UIculture> <Out file name without resources.dll> <TargetResource> <referenceTemplate> <sourceResource_1> <sourceResource_2> ... <sourceResource_n>
culture = WScript.Arguments(0);

outputFile = WScript.Arguments(1);
targetResource = WScript.Arguments(2);
referenceTemplate = WScript.Arguments(3);

//
// Build full references
// assuming all resource files belong to one culture
fullCultureFolder = "..\\bin\\Debug\\" + culture;

var loopCount=0;

//var MaxResourceFile = 10 ;
var fullInputFileArr = new Array();
var FullResourceFileArr = new Array();
var fullInternalNameArr = new Array();

// now building and storing resource file command  
for(i=4;i< WScript.Arguments.length; i++)
{
	fullInputFileArr[loopCount] = WScript.Arguments(i) + "." + culture + ".resx";
	FullResourceFileArr[loopCount]= WScript.Arguments(i)  + "." + culture + ".resources";  
	fullInternalNameArr[loopCount]= targetResource +  "." +  WScript.Arguments(i) + "." + culture + ".resources";		

	loopCount = loopCount + 1 ;

}

fullOutputFilename = fullCultureFolder + "\\" + outputFile + ".resources.dll";
fullTemplateFile = "..\\bin\\debug\\" + referenceTemplate + ".dll";

//
// Check the source file exists
//
FSO = WScript.CreateObject("Scripting.FileSystemObject");


// Now check whether file exist for each resource file 

for(i=0;i< fullInputFileArr.length; i++)
{
	//WScript.Echo("fullInputFileArr[" + i + "]: " + fullInputFileArr[i]);

	if ( FSO.FileExists( fullInputFileArr[i]) != true )
	{
		WScript.Echo ( "Unable to find source file: " + fullInputFileArr[i] );
		WScript.Quit( 2 );
	}
}

//
// Check the destination folder exists
//
WScript.Echo ( "Checking Culture folder " + fullCultureFolder);
if ( FSO.FolderExists( fullCultureFolder ) != true )
{
	FSO.CreateFolder( fullCultureFolder );
}

//
// Get a shell object so that we can launch command line programs
//
SHELL = WScript.CreateObject("WScript.Shell");

WScript.Echo ( "\nStarting language build\n" );

//
// Invoke the RESGEN
//

// Generating resource.dll file for each input resource file
for(i=0;i< fullInputFileArr.length; i++)
{
	resgenCmdLine = "ResGen " + fullInputFileArr[i];
	WScript.Echo ( resgenCmdLine + "\n");
	SHELL.run( resgenCmdLine, 2, true );
}

//
// Build a series of clauses for the AL.EXE linker
//
cultureClause =  " /culture:" + culture;
outClause = " /out:" + fullOutputFilename;

// Now Generating embed clause for each resource file 
embedClause = "" ; 
for(i=0;i< fullInputFileArr.length; i++)
{
	embedClause = embedClause + " /embed:" + FullResourceFileArr[i] + "," + fullInternalNameArr[i] ;	
}

// WScript.Echo ("embedClause " + embedClause );

templateClause = " /template:" + fullTemplateFile;

//
// Build the AL.EXE command line that we want to execute
//
alCmdLine = "al.exe " + cultureClause + outClause + embedClause + " " + templateClause;
WScript.Echo ( alCmdLine );
SHELL.run( alCmdLine, 2, true );

WScript.Echo ( "\n\nFinished language build\n" );
