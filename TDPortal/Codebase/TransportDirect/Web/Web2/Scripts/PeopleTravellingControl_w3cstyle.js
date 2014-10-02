// ***********************************************
// NAME     : PeopleTravellingControl_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Ensures that the sum of the values in the two controls cannot exceed the specified maximum
// by restricting the entries in the target based on the source value.
function handlePeopleTravellingChange(sourceControlId, targetControlId, maxPeople)
{
	var sourceControl = document.getElementById(sourceControlId);
	var targetControl = document.getElementById(targetControlId);

	var sourceSelection = GetSelectedElement(sourceControl);

	var targetLimit = maxPeople - sourceSelection;

	if (targetControl.options.length != (targetLimit + 1))
	{
		var targetSelection = GetSelectedElement(targetControl);

		// Clear out the target and repopulate with the correct number of entries
		while (targetControl.options.length > 0)
			targetControl.options[0] = null;

		var newOption;	
		for (var index = 0; index <= targetLimit; index++)
		{
			newOption = document.createElement("OPTION");
			newOption.value = index;
			newOption.text = index;
			targetControl.options[targetControl.options.length] = newOption;
		}

		// Attempt to reselect the original item from the target	
		SetSelectedElement(targetControl, targetSelection);
		handlePeopleTravellingChange(targetControlId, sourceControlId, maxPeople);
	}
}

// Returns the selected element from the dropdown
function GetSelectedElement(selectControl)
{
	return selectControl.options[selectControl.selectedIndex].value;
}

// Selects the given element in the dropdown. If not found, the last item in the list is selected.
function SetSelectedElement(selectControl, requiredValue)
{
	var foundIndex = -1;
	for (var index = 0; (index < selectControl.options.length) && (foundIndex == -1); index++)
		if (selectControl.options[index].value == requiredValue)
			foundIndex = index;
	
	if (foundIndex == -1)
		selectControl.selectedIndex = selectControl.options.length - 1;
	else
		selectControl.selectedIndex = foundIndex;
}