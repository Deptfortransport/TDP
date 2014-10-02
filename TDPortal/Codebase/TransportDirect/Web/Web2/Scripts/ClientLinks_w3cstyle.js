// ***********************************************
// NAME     : ClientLinks_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

function MakeClientLinkVisible(controlId)
{
	if (window.external && (typeof(window.external.AddFavorite) != "undefined"))
	{
		var clientLinkControl = document.getElementById(controlId);
		if (clientLinkControl && clientLinkControl.style)
		{
			clientLinkControl.style.display='';
		}
	}
	return false;
}

function CreateBookmark(bookmarkLink, url, title)
{
	if (window.external && (typeof(window.external.AddFavorite) != "undefined"))
	{
		window.external.AddFavorite( url, title );
	}
	return false;
}
