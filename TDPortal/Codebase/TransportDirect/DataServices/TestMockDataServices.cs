// ***********************************************
// NAME 		: TestMockDataServices.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 31-Mar-2004
// DESCRIPTION 	: Mock Data Services object - data is read entirely from the properties service.
// ************************************************

using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Object that can be used for test purposes when a dataservices object is required.
	/// All data is read from the current properties service instead of from a database
	/// </summary>
	public class TestMockDataServices : IDataServices
	{
		/// <summary>
		/// Maintains a flag indicating of cache has been loaded.
		/// </summary>
		static private volatile bool CacheIsLoaded = false;

		/// <summary>
		/// Holds dataset ID as key and hash/array as value.
		/// </summary>
		static private Hashtable hash = null;

		#region Constructor
		/// <summary>
		/// Loads the cache if not already loaded in class variables.
		/// </summary>
		public TestMockDataServices()
		{
			if(CacheIsLoaded == false)
			{
				lock(typeof(DataServices))  // Lock DataServices type at this stage.
				{
					// Test again in case it got loaded when lock was acquired.
					if(CacheIsLoaded == false)
					{
						hash = new Hashtable();
						CacheIsLoaded = true;
						LoadDataCache();
					}
				} // lock
			} // If cache is not loaded
		}
		#endregion

		#region Public Interface
		/// <summary>
		/// Returns an Array-List type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Array-List type.</param>
		/// <returns>Structure could contain a list of date and dropdown items.</returns>
		public ArrayList GetList(DataServiceType item)
		{
			object o = hash[item];
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}
			return (ArrayList) ((ArrayList) o).Clone(); // Shallow copy.
		}

		public string GetResourceId (DataServiceType type, string value)
		{
			ArrayList items = GetList(type);

			foreach (DSDropItem item in items)
			{
				if ( item.ItemValue == value)
					return item.ResourceID;
			}
			return string.Empty;
		}

		public string GetValue (DataServiceType type, string resourceId)
		{
			ArrayList list = GetList(type);

			foreach (DSDropItem item in list)
			{
				if (item.ResourceID == resourceId)
					return item.ItemValue;
				
			}
			return string.Empty;
		}

		public string GetText (DataServiceType type, string value)
		{
			ArrayList list = GetList(type);

			string temp = "DataServices." + type.ToString() + ".";

			foreach (DSDropItem item in list)
			{
				if (item.ItemValue == value)
					
				{
					return item.ResourceID;
						
				}
				
			}
			return string.Empty;
		}

		public string GetText (DataServiceType type, string value, TDResourceManager rm)
		{
			ArrayList list = GetList(type);

			string temp = "DataServices." + type.ToString() + ".";

			foreach (DSDropItem item in list)
			{
				if (item.ItemValue == value)
					
				{
					return rm.GetString(
						temp + item.ResourceID, System.Globalization.CultureInfo.CurrentUICulture);
						
				}
				
			}
			return string.Empty;
		}

		#region Selection Helper
		public void Select(ListControl list, string resourceId)
		{
			for (int i= 0; i< list.Items.Count; i++)
			{
				ListItem item = list.Items[i];
				if (item.Value == resourceId)
				{
					list.SelectedIndex = i;
					break;
				}
			}
		}
		public void SelectInCheckBoxList(CheckBoxList list, string resourceId)
		{
			for (int i= 0; i< list.Items.Count; i++)
			{
				ListItem item = list.Items[i];
				if (item.Value == resourceId)
				{
					item.Selected = true;
					break;
				}
			}
		}
		#endregion

		/// <summary>
		/// Returns an Hash-Table type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Hash-Table type.</param>
		/// <returns>Structure contains list of key-value pairs.</returns>
		public Hashtable GetHash(DataServiceType item)
		{
			object o = hash[item];
			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, GetHash";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}
			return (Hashtable) ((Hashtable) o).Clone(); // Shallow copy.
		}

		/// <summary>
		/// Performs a lookup on a specified hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public string FindHash(DataServiceType item, string key)
		{
			object o = hash[item]; Hashtable finder;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;
			return finder[key].ToString();
		}

		/// <summary>
		/// Performs a lookup on a specified categorised hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public CategorisedHashData FindCategorisedHash(DataServiceType item, string key)
		{
			object o = hash[item]; Hashtable finder;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;
			return (CategorisedHashData)finder[key];
		}

		/// <summary>
		/// Returns true if a given date is a bank holiday.
		/// </summary>
		/// <param name="item">Cache data item to test for.</param>
		/// <param name="dateToTest">The date to test.</param>
		/// <param name="country">The country to test for.</param>
		/// <returns>True if date is a bank holiday.</returns>
		public bool IsHoliday(DataServiceType item, TDDateTime dateToTest, DataServiceCountries country)
		{
			object o = hash[item]; Hashtable finder; bool isHoliday = false;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;

			try
			{
				isHoliday = ((DataServiceCountries) finder[dateToTest] & country) == country;
			}
			catch(NullReferenceException)
			{
				// Do nothing, the key was not found.
			}


			return isHoliday;
		}

		/// <summary>
		/// Populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		public void LoadListControl(DataServiceType dataSet, ListControl control)
		{
			ArrayList list;

			object o = hash[dataSet];
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			list = (ArrayList)o;

			string temp; DSDropItem itemCache;


			control.Items.Clear();
			temp = "DataServices." + dataSet.ToString() + ".";

			for (int i=0; i< list.Count; i++)
			{
				ListItem itemWeb = new ListItem();
				itemCache = (DSDropItem) list[i];

				// Use resource ID for corresponding text
				itemWeb.Text = itemCache.ResourceID;

				itemWeb.Value    = itemCache.ResourceID;

				control.Items.Add(itemWeb); // Append this item to the list.

				if (itemCache.IsSelected)
				{
					if (control is DropDownList)
					{
						((DropDownList)control).SelectedIndex = i;
					}
					else if (control is RadioButtonList)
					{
						((RadioButtonList)control).SelectedIndex = i;
					}
					else
					{
						itemWeb.Selected = true;
					}
				}
			}
		}

		/// <summary>
		/// Overload of method that populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		/// <param name="rm">The resource manager to use for looking up lang strings</param>
		public void LoadListControl(DataServiceType dataSet, ListControl control, TDResourceManager rm)
		{
			ArrayList list;

			object o = hash[dataSet];
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
					);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			list = (ArrayList)o;

			string temp; DSDropItem itemCache;


			control.Items.Clear();
			temp = "DataServices." + dataSet.ToString() + ".";

			for (int i=0; i< list.Count; i++)
			{
				ListItem itemWeb = new ListItem();
				itemCache = (DSDropItem) list[i];

				// Get corresponding text from resource file.
				itemWeb.Text = rm.GetString(
					temp + itemCache.ResourceID, System.Globalization.CultureInfo.CurrentUICulture
					);

				itemWeb.Value    = itemCache.ResourceID;

				control.Items.Add(itemWeb); // Append this item to the list.

				if (itemCache.IsSelected)
				{
					if (control is DropDownList)
					{
						((DropDownList)control).SelectedIndex = i;
					}
					else if (control is RadioButtonList)
					{
						((RadioButtonList)control).SelectedIndex = i;
					}
					else
					{
						itemWeb.Selected = true;
					}
				}
			}
		}

		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The value or string.empty if not found</returns>
		public string GetDefaultListControlValue(DataServiceType dataSet)
		{
			ArrayList list = GetList(dataSet) as ArrayList;
			if( list != null )
			{
				for (int i=0; i< list.Count; i++)
				{
					DSDropItem itemCache = (DSDropItem) list[i];
					if( itemCache.IsSelected )
					{
						return itemCache.ItemValue;
					}
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The resource id or string.empty if not found</returns>
		public string GetDefaultListControlResourceId(DataServiceType dataSet)
		{
			ArrayList list = GetList(dataSet) as ArrayList;
			if( list != null )
			{
				for (int i=0; i< list.Count; i++)
				{
					DSDropItem itemCache = (DSDropItem) list[i];
					if( itemCache.IsSelected )
					{
						return itemCache.ResourceID;
					}
				}
			}		
			return string.Empty;
		}

		#endregion

		#region Load Data Cache
		/// <summary>
		/// Loads the datacache from databases.
		/// </summary>
		private void LoadDataCache()
		{
			IPropertyProvider currProps = Properties.Current;

			int i, max = (int) DataServiceType.DataServiceTypeEnd;
			String parType, parTemp;
			int itemCount;


			for(i=0; i < max; i++)
			{
				parTemp = "TransportDirect.UserPortal.DataServices."
					+ ((DataServiceType) i).ToString();

				parType  = currProps[ parTemp + ".type"  ];

				if (parType != null)
				{
					ICollection dataset; 
					itemCount = Convert.ToInt32( currProps[ parTemp + ".items" ] );

					switch(parType)
					{
						case "1": dataset = ReadList(parTemp, itemCount); break;
						case "2": dataset = ReadHash(parTemp, itemCount); break;
						case "3": dataset = ReadDrop(parTemp, itemCount); break;
						case "4": dataset = ReadDate(parTemp, itemCount); break;
						case "5": dataset = ReadCategorisedHash(parTemp, itemCount); break;
						default:
							string message = "Type is unknown";
							throw new TDException(message, false, TDExceptionIdentifier.DSUnknownType);
					}

					hash.Add((DataServiceType) i, dataset);

				}
			} // For each enum
	
		}
		#endregion

		#region Collection Readers
		/// <summary>
		/// Reads Array-List data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadList(string parTemp, int itemCount)
		{
			IPropertyProvider currProps = Properties.Current;
			string itemTemp;
			ArrayList arrayList = new ArrayList();

			for (int i = 1; i <= itemCount; i++ )
			{
				itemTemp = currProps[ parTemp + ".item." + i.ToString() ].Trim();
				arrayList.Add(itemTemp);
			}
			return arrayList;
		}

		/// <summary>
		/// Reads hash-table data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadHash(string parTemp, int itemCount)
		{
			IPropertyProvider currProps = Properties.Current;
			string itemTemp, temp1, temp2;
			string[] itemTempArr;
			Hashtable hash = new Hashtable();

			for (int i = 1; i <= itemCount; i++ )
			{
				itemTemp = currProps[ parTemp + ".item." + i.ToString() ].Trim();
				itemTempArr = itemTemp.Split("|".ToCharArray());
				temp1 = itemTempArr[0].Trim();
				temp2 = itemTempArr[0].Trim();
				hash.Add(temp1, temp2);
			}
			return hash;
		}

		/// <summary>
		/// Reads hash-table data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadCategorisedHash(string parTemp, int itemCount)
		{
			IPropertyProvider currProps = Properties.Current;
			string itemTemp, temp1, temp2, temp3, temp4;
			string[] itemTempArr;
			Hashtable hash = new Hashtable(); 

			for (int i = 1; i <= itemCount; i++ )
			{
				itemTemp = currProps[ parTemp + ".item." + i.ToString() ].Trim();
				itemTempArr = itemTemp.Split("|".ToCharArray());
				temp1 = itemTempArr[0].Trim();
				temp2 = itemTempArr[1].Trim();
				temp3 = itemTempArr[2].Trim();
				temp4 = itemTempArr[3].Trim();
				CategorisedHashData data = new CategorisedHashData(temp2, temp3, temp4);
				hash.Add(temp1, data);
			}
			return hash;
		}

		/// <summary>
		/// Reads data for the dropdown-list for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadDrop(string parTemp, int itemCount)
		{
			IPropertyProvider currProps = Properties.Current;
			string itemTemp, resourceID, itemValue; 
			string[] itemTempArr;
			bool isSelected;
			ArrayList arrayList = new ArrayList();

			for (int i = 1; i <= itemCount; i++ )
			{
				itemTemp = currProps[ parTemp + ".item." + i.ToString() ].Trim();
				itemTempArr = itemTemp.Split("|".ToCharArray());
				resourceID = itemTempArr[0].Trim();
				itemValue  = itemTempArr[1];
				isSelected = itemTempArr[2] != "0";

				if(itemValue != null)
					itemValue = itemValue.Trim();

				DSDropItem item = new DSDropItem(resourceID, itemValue, isSelected);

				arrayList.Add(item);
			}

			return arrayList;
		}

		/// <summary>
		/// Reads data for the bank-holidays for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadDate(string parTemp, int itemCount)
		{
			IPropertyProvider currProps = Properties.Current;
			string itemTemp;//, temp1, temp2, temp3;
			string[] itemTempArr;
			Hashtable hash = new Hashtable();
			TDDateTime holiday; 
			DataServiceCountries country;

			for (int i = 1; i <= itemCount; i++ )
			{
				itemTemp = currProps[ parTemp + ".item." + i.ToString() ].Trim();
				itemTempArr = itemTemp.Split("|".ToCharArray());
				holiday = Convert.ToDateTime( itemTempArr[0] );
				country = (DataServiceCountries) Convert.ToInt32( itemTempArr[1] );
				hash.Add(holiday, country);
			}
			return hash;
		}
		#endregion

	}
}
