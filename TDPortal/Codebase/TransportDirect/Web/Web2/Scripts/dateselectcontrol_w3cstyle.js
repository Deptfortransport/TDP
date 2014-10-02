// *********************************************** 
// NAME                 : DateSelectControl_W3CStyle.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 06/10/2004
// DESCRIPTION			: JavaScript functionality for DateSelectControl
// ************************************************ 
// 

/// <summary>
///	Called by the onchange event of the 'day' dropdown in the control.
//	Resets the values of the associated dropdowns when a day is selected.
///	</summary>
/// <param name="controlID">ID of the DateSelectControl</param>
function DaySelectionChanged(controlID)
{
	if (document.getElementById(controlID + '_listDays')) 
	{
		if (document.getElementById(controlID + '_listDays').value != "")
		{
			switch (document.getElementById(controlID + '_listMonths').value)
			{
				case "NoReturn":
					document.getElementById(controlID + '_listMonths').selectedIndex = 0;
				case "OpenReturn":
					document.getElementById(controlID + '_listMonths').selectedIndex = 0;
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
	if (document.getElementById(controlID + '_listMonths')) 
	{
		if (document.getElementById(controlID + '_listMonths').value == 'NoReturn' ||
			document.getElementById(controlID + '_listMonths').value == 'OpenReturn')
		{
			ResetDropDown(controlID + '_listDays');
			ResetDropDown(controlID + '_listHours');
			ResetDropDown(controlID + '_listMinutes');
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
	if (document.getElementById(controlID + '_listHours')) 
	{
		switch (document.getElementById(controlID + '_listHours').value)
		{
			case "Any":
				ResetDropDown(controlID + '_listMinutes');
			case "":
				return false;
			default:
				if (document.getElementById(controlID + '_listMinutes').value ==  "")
				{
					document.getElementById(controlID + '_listMinutes').selectedIndex = 0;
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
function ResetDropDown(controlID)
{
	if (document.getElementById(controlID))
	{
		document.getElementById(controlID).selectedIndex = (document.getElementById(controlID).length-1);
	}
}