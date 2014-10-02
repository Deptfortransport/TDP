// *********************************************** 
// NAME                 : AmendDateSelectControl_W3CStyle.js 
// AUTHOR               : Amit Patel
// DATE CREATED         : 13/02/2009
// DESCRIPTION			: JavaScript functionality for DateSelectControl on Amend Data and Time footer
//                        This is for the return date and time dropdown
// ************************************************ 
// 

/// <summary>
///	Called by the onchange event of the 'day' dropdown in the control.
//	Resets the values of the associated dropdowns when a day is selected.
///	</summary>
/// <param name="controlID">ID of the DateSelectControl</param>
function DaySelectionChanged(controlID)
{
	if (document.getElementById(controlID + '_dropDownListReturningDate')) 
	{
		if (document.getElementById(controlID + '_dropDownListReturningDate').value != "")
		{
			switch (document.getElementById(controlID + '_dropDownListReturningMonthYear').value)
			{
				case "NoReturn":
					document.getElementById(controlID + '_dropDownListReturningMonthYear').selectedIndex = 0;
				case "OpenReturn":
					document.getElementById(controlID + '_dropDownListReturningMonthYear').selectedIndex = 0;
				default:
					return false;
			}
		}
	}
	else
	{return false;}
}
/// <summary>
///	Called by the onchange event of the 'month' dropdown in the control.
//	Resets the values of the associated dropdowns when No Return is selected.
///	</summary>
/// <param name="controlID">ID of the DateSelectControl</param>
function MonthSelectionChanged(controlID)
{
	if (document.getElementById(controlID + '_dropDownListReturningMonthYear')) 
	{
		if (document.getElementById(controlID + '_dropDownListReturningMonthYear').value == 'NoReturn' ||
			document.getElementById(controlID + '_dropDownListReturningMonthYear').value == 'OpenReturn')
		{
			ResetDropDown(controlID + '_dropDownListReturningDate',true);
			ResetDropDown(controlID + '_dropDownListReturningHour',false);
			ResetDropDown(controlID + '_dropDownListReturningMinute',true);
			return true;
		}
		else
		{return false;}
	}
	else
	{return false;}
}

/// <summary>
///	Called by the onchange event of the 'hours' dropdown in the control.
//	Resets the values of the 'minutes' dropdown when 'Any time' is selected.
///	</summary>
/// <param name="controlID">ID of the DateSelectControl</param>
function HoursSelectionChanged(controlID)
{
	if (document.getElementById(controlID + '_dropDownListReturningHour')) 
	{
		switch (document.getElementById(controlID + '_dropDownListReturningHour').value)
		{
			case "Any":
				ResetDropDown(controlID + '_dropDownListReturningMinute', true);
			case "":
				return false;
			default:
				if (document.getElementById(controlID + '_dropDownListReturningMinute').value ==  "")
				{
					document.getElementById(controlID + '_dropDownListReturningMinute').selectedIndex = 1;
				}
		}
	}
	else
	{return false;}
}

/// <summary>
/// Resets a dropdown boxes to its default values
/// <summary>
/// <param name="controlID">ID of the control</param>
function ResetDropDown(controlID,dashFirst)
{
	if (document.getElementById(controlID))
	{
	    if(dashFirst)
	    {
	        document.getElementById(controlID).selectedIndex = 0;
	    }
	    else
	    {
		    document.getElementById(controlID).selectedIndex = (document.getElementById(controlID).length-1);
		}
	}
}