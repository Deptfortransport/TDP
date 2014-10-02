// Function to toggle a branch between expanded and contracted
function ToggleExpandedMenuBranch(list, link)
{
	var listElement = document.getElementById(list);
	var categoryLink = document.getElementById(link);

	if (categoryLink.className == "highlighted")
	{
		if (listElement)
			listElement.style.display = "none";
		categoryLink.className = "default";
	}
	else
	{
		if (listElement)
			listElement.style.display = "block";
		categoryLink.className = "highlighted";
	}
}

// Function to collapse all expanded menu links
function CollapseExpandedMenuBranch()
{
	var expandableMenuControl = document.getElementById("ExpandableMenu");
	var subCategories = expandableMenuControl.getElementsByTagName("UL");
	
	for (var i=0; i < subCategories.length; i++)
	{
		if (subCategories[i].className == "subcategoryLink")
			subCategories[i].style.display = "none";
	}
}