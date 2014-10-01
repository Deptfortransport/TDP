// ***********************************************
// NAME 		: IDataServices.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 31-Mar-2004
// DESCRIPTION 	: Data Services Interface.
// ************************************************

using System;
using System.Collections;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

using System.Resources;

namespace TransportDirect.UserPortal.DataServices
{

	public interface IDataServices
	{

		/// <summary>
		/// Returns an Array-List type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Array-List type.</param>
		/// <returns>Structure could contain a list of date and dropdown items.</returns>
		ArrayList GetList(DataServiceType item);

		string GetResourceId (DataServiceType type, string value);

		string GetValue (DataServiceType type, string resourceId);

		string GetText (DataServiceType type, string value);

		/// <summary>
		/// overloaded method allows a different resource manager to be used
		/// </summary>
		/// <param name="type"></param>
		/// <param name="value"></param>		
		/// <param name="rm">The resource manager to use for looking up lang strings</param>
		string GetText (DataServiceType type, string value, TDResourceManager rm);

		void Select(ListControl list, string resourceId);

		void SelectInCheckBoxList(CheckBoxList list, string resourceId);

		/// <summary>
		/// Returns an Hash-Table type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Hash-Table type.</param>
		/// <returns>Structure contains list of key-value pairs.</returns>
		Hashtable GetHash(DataServiceType item);

		/// <summary>
		/// Performs a lookup on a specified hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		string FindHash(DataServiceType item, string key);

		/// <summary>
		/// Performs a lookup on a specified categorised hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		CategorisedHashData FindCategorisedHash(DataServiceType item, string key);

		/// <summary>
		/// Returns true if a given date is a bank holiday.
		/// </summary>
		/// <param name="item">Cache data item to test for.</param>
		/// <param name="dateToTest">The date to test.</param>
		/// <param name="country">The country to test for.</param>
		/// <returns>True if date is a bank holiday.</returns>
		bool IsHoliday(DataServiceType item, TDDateTime dateToTest, DataServiceCountries country);

		/// <summary>
		/// Populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		void LoadListControl(DataServiceType dataSet, ListControl control);

		/// <summary>
		/// Overload of method that populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		/// <param name="rm">The resource manager to use for looking up lang strings</param>
		void LoadListControl(DataServiceType dataSet, ListControl control, TDResourceManager rm);

		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The value or string.empty if not found</returns>
		string GetDefaultListControlValue(DataServiceType dataSet);

		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The resource id or string.empty if not found</returns>
		string GetDefaultListControlResourceId(DataServiceType dataSet);
	}
}
