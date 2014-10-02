// *********************************************** 
// NAME                 : CheckBoxListRequiredFieldValidator.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 21/10/2003
// DESCRIPTION			: Custom Validator to additionally 
//							handle for CheckBoxLists
// ************************************************ 
// $Log:

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for CheckBoxListRequiredFieldValidator.
	/// </summary>
	[ToolboxData("<{0}:CheckBoxListRequiredFieldValidator runat=server></{0}:CheckBoxListRequiredFieldValidator>")]
	public class CheckBoxListRequiredFieldValidator : System.Web.UI.WebControls.BaseValidator
	{
		private ListControl listctrl;
		private Panel panelctrl;
 
		public CheckBoxListRequiredFieldValidator()
		{
			base.EnableClientScript = false;
		}
 
		/// <summary>
		/// Override base class method
		/// </summary>
		/// <returns></returns>
		protected override bool ControlPropertiesValid()
		{
			Control ctrl = FindControl(ControlToValidate);
       
			if (ctrl != null) 
			{
				if (ctrl is ListControl)
				{
					listctrl = ctrl as ListControl;
					return true;
				}
				else if (ctrl is Panel)
				{
					panelctrl = ctrl as Panel;
					return true;
				}
				else
					return false;
			}
			else 
				return false;  // raise exception
		}
 
		protected override bool EvaluateIsValid()
		{     
			if (listctrl != null)
			{
				// Validation for list control
				return listctrl.SelectedIndex != -1;
			}
//			else if (panelctrl != null)
//			{
//				// Validation for panel control
//				foreach (Control c in panelctrl.Controls)
//				{
//					if (c is CheckBox)
//					{
//						if (c != null)
//						{
//							return true;
//						}
//						else
//						{
//							return false;
//						}
//					}
//				}
//			}
			else
				return true;
		}

	}
}
