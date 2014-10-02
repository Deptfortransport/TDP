// ********************************************************************* 
// NAME                 : UserNavigationDefaultAction_w3cstyle.js
// AUTHOR               : Atos Origin
// DATE CREATED         : 05/09/2005
// DESCRIPTION			: JavaScript functionality for taking default action (Next Button) when user hits enter button 
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Scripts/UserNavigationDefaultAction_w3cstyle.js-arc  $ 
//
//   Rev 1.3   Mar 19 2010 13:39:38   mmodi
//Added header to file
//Resolution for 5471: Maps - Code Review - Add headers to Javascript files
//
//   Rev 1.2   Mar 31 2008 13:26:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:42   mturner
//Initial revision.
//
//   Rev 1.2   Sep 13 2005 14:45:28   Schand
//DN079 UEE Default Navigation on enter key.
//Updated Javascript function.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.1   Sep 09 2005 14:13:20   Schand
//DN079 UEE Enter Key.
//Updated Javascript function. 
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.0   Sep 05 2005 18:05:22   Schand
//Initial revision.

function TakeDefaultAction()
{	        
	if (event.keyCode == 13)
	{	
		event.returnValue=false;
		event.cancel = true;             
		var inputColl = document.getElementsByTagName("input");            
				    
		for (var i=0;i<inputColl.length; i++) 
		{
			var elementName = inputColl[i].name;				
			if (elementName != null)
			{
				if (elementName.indexOf("defaultActionButton")>= 0)
				{   					
					inputColl[i].click();									
					break;
				}						
			}
		}            
	}
	
}

