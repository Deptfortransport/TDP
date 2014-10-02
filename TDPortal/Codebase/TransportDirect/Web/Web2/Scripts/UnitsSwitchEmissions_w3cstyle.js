// ***********************************************
// NAME     : UnitsSwitchEmissions_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Get the dropdown
var outwardDropDown = document.getElementById("journeyEmissionsCompareControlOutward_dropdownlistUnits1");

if (outwardDropDown == null) {
    outwardDropDown = document.getElementById("journeyEmissionsCompareControlOutward_dropdownlistUnits2");
}

// Get the hidden field
var outwardHidden = document.getElementById("journeyEmissionsCompareControlOutward_hdnUnitsState");

function ChangeUnits(hiddenInputId, PageName, callingControl)
{
	var callingControlName = callingControl.name;
	var printButton;
	var targetDropdown;
	var targetHidden;

	if (outwardDropDown != null  && callingControl.id == outwardDropDown.id)
	{
		targetDropdown = outwardDropDown;
		targetHidden = outwardHidden;
	}
	else
	{
		targetDropdown = outwardDropDown;
		targetHidden = outwardHidden;	
	}
	
	//IR1984fix - flip is the same
	if (targetHidden.value.toLowerCase() == targetDropdown.value.toLowerCase())
	{
		if (targetHidden.value=='Miles')
		{
			targetHidden.value = 'kms';
		}
		else 
		{
			targetHidden.value = 'Miles';
		}
	}

	if ((PageName == "JourneyEmissionsCompare") || (PageName == "JourneyEmissionsCompareJourney")) {
	    printButton = document.getElementById('printerFriendlyControl_printButton');
	}
	else if (PageName == "JourneyDetails") {
	    printButton = document.getElementById('JourneyChangeSearchControl1_printerFriendlyPageButton_printButton');
	}
	else {
	    printButton = document.getElementById('printerFriendlyControl_hyperlinkText');
	}

	if (targetHidden.value=='Miles')
	{
		var elements = document.getElementsByTagName('*');
        for (i=0; i<elements.length;i++)
        {
            if (elements[i].className=='milesshow') 
            {
                elements[i].className="mileshide";
            }
            
            if (elements[i].className=='kmshide') 
            {
                elements[i].className="kmsshow";
            }
        }

		SetSelectedElement( targetDropdown, "km" );
		targetHidden.value = 'kms';

		ReplaceOnClickSubstring(printButton, 'Units=miles', 'Units=kms');
	}	
	else
	{
		var elements = document.getElementsByTagName('*');
        for (i=0; i<elements.length;i++)
        {
            if (elements[i].className=='mileshide') 
            {
                elements[i].className="milesshow";
            }
            
            if (elements[i].className=='kmsshow') 
            {
                elements[i].className="kmshide";
            }
        }

		SetSelectedElement( targetDropdown, "miles" );	
		targetHidden.value = 'Miles';
		
		ReplaceOnClickSubstring(printButton, 'Units=kms', 'Units=miles');
	}
 }
 
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

function ReplaceOnClickSubstring(inControl, oldString, newString)
{
	if (inControl != null)
	{
		var stringOnclick = new String(inControl.onclick);
		
		eval("var stringOnClick2=" + stringOnclick.replace(oldString, newString));
		
		inControl.onclick = stringOnClick2;
	}
}

