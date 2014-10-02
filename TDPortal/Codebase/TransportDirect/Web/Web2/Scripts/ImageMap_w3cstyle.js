// ***********************************************
// NAME     : ImageMap_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

function preload(imageUrl)
{
	var newImage = new Image();
	newImage.src = imageUrl;
}

function swapImage(targetId, newUrl)
{
	var mapImage = document.getElementById(targetId);
	if (mapImage)
		mapImage.src = newUrl;
}