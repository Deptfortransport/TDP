// ***********************************************
// NAME     : UnitsSwitchCycle_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

var outwardDropDown = document.getElementById("cycleAllDetailsControlOutward_cycleSummaryControl_dropdownlistCycleSummary");
var inwardDropDown = document.getElementById("cycleAllDetailsControlReturn_cycleSummaryControl_dropdownlistCycleSummary");

var outwardHidden = document.getElementById("cycleAllDetailsControlOutward_cycleSummaryControl_hdnUnitsState");
var inwardHidden = document.getElementById("cycleAllDetailsControlReturn_cycleSummaryControl_hdnUnitsState");

function ChangeUnits(hiddenInputId, PageName, callingControl)
{
	var callingControlName = callingControl.name;
	var printButton;
	var targetDropdown;
	var targetHidden;
	var nontargetDropdown;
	var nontargetHidden;

	if (outwardDropDown != null  && callingControl.id == outwardDropDown.id)
	{
		targetDropdown = outwardDropDown;
		targetHidden = outwardHidden;
		nontargetDropdown = inwardDropDown;
		nontargetHidden = inwardHidden;
	}
	else
	{
		targetDropdown = inwardDropDown;
		targetHidden = inwardHidden;
		nontargetDropdown = outwardDropDown;
		nontargetHidden = outwardHidden;		
	}
	
	//flip is the same
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

	if (PageName == "CycleJourneyDetails")
	{
		printButton = document.getElementById('journeyChangeSearchControl_printerFriendlyPageButton_printButton');
	}
	else
	{
		printButton = document.getElementById('theJourneyChangeSearchControl_printerFriendlyPageButton_hyperlinkImagePrintLink');
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
		if (nontargetDropdown != null)
		{
			SetSelectedElement( nontargetDropdown, "km" );		
			nontargetHidden.value = 'kms';
		}

		ReplaceOnClickSubstring(printButton, 'units=miles', 'units=kms');
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
		if (nontargetDropdown != null)
		{
			SetSelectedElement( nontargetDropdown, "miles" );	
			nontargetHidden.value = 'Miles';		
		}
		
		ReplaceOnClickSubstring(printButton, 'units=kms', 'units=miles');
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
		var stringOnclick = inControl.onclick.toString();

		// Strip Anonymous function declaration
		var startOfFunction = stringOnclick.indexOf("\{")+1;
		var endOfFunction = stringOnclick.lastIndexOf("\}");
		var stringOnclick2 = stringOnclick.substring(startOfFunction, endOfFunction);

		var stringOnclick3 = stringOnclick2.replace(oldString, newString);

		inControl.onclick = new Function(stringOnclick3);
	}
}

