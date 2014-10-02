// ***********************************************
// NAME     : Resources_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

function ResourceIdToString(resourceId)
{
	var resource = resources[0];
	
	if (typeof(resource[resourceId]) == "undefined")	
		return "";
	else
		return resource[resourceId];		
}