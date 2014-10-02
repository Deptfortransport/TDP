// ***********************************************
// NAME 		: TypeSafeDictionary.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: A type safe implementation of the IDictionary interface 
// that allows the TDSession manager to storm FormShift data between page loads.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TypeSafeDictionary.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:48   mturner
//Initial revision.
//
//   Rev 1.11   Mar 08 2005 09:34:30   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.10   Mar 10 2004 15:53:18   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.9   Jan 21 2004 11:35:14   PNorell
//Updates for 5.2
//
//   Rev 1.8   Sep 26 2003 18:15:46   RPhilpott
//Return false if a boolkey not found, to support use by ForceRedirect flag.
//
//   Rev 1.7   Sep 18 2003 18:27:58   PNorell
//Added default return for transition events.
//
//   Rev 1.6   Sep 18 2003 15:54:04   kcheung
//Updated TransitionEvent
//
//   Rev 1.5   Aug 19 2003 10:53:10   PNorell
//Added support for service discovery.
//
//   Rev 1.4   Jul 17 2003 14:05:18   MTurner
//Added documentation comments after code review
//
//   Rev 1.3   Jul 17 2003 13:28:56   kcheung
//Added TransitionEvent
//
//   Rev 1.2   Jul 07 2003 17:31:00   mturner
//key.GetHashCode reinstated as the GetHashCode method has been overridden for classes in Key.cs
//
//   Rev 1.1   Jul 07 2003 12:14:56   MTurner
//key.ID used to store formshift data rather than key.getHashCode() 
//
//   Rev 1.0   Jul 03 2003 17:31:30   AWindley
//Initial Revision

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// TypeSafe Dictionary manages a temporary storage area for FormShift data
	/// </summary>
	public class TypeSafeDictionary : IDictionary
	{
		// Hashtable to store form form shift data.
		private Hashtable StoredValues = new Hashtable();

		// The following Get methods take an instance of the appropriate Keytype,
		// they then attempt to find this key in the StoredValues hashtable. 
		// A succesfully matched entry is returned to the calling class.
		// The set methods take a value and an instance of a key type.  Both of these
		// are then written to the StoredValues hashtable.

		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>Int32</value>
		public int this[IntKey key]
		{
			//GetHashCode has been overridden for this class in Key.cs
			get
			{
				return (int)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}
		
		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>String</value>
		public string this[StringKey key]
		{
			get
			{
				return (string)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}
		
		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>Double</value>
		public double this[DoubleKey key]
		{
			get
			{
				return (double)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}
		
		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>DateTime</value>
		public DateTime this[DateKey key]
		{
			get
			{
				return (DateTime)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>Boolean</value>
		public bool this[BoolKey key]
		{
			get
			{
				if( StoredValues[key] == null )
				{
					return false;
				}
				return (bool)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		/// <summary>
		/// Get/Set FormShift data.  Read/Write access.
		/// </summary>
		/// <value>TransitionEvent</value>
		public TransitionEvent this[TransitionEventKey key]
		{
			get
			{
				if( StoredValues[key] == null )
				{
					return TransitionEvent.Default;
				}
				return (TransitionEvent)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		[CLSCompliant(false)]
		public TDUser this[ UserKey key ]
		{
			get
			{
				return (TDUser)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type PageID to
		/// the ASP session data.
		/// </summary>
		/// <value>PageId</value>
		public PageId this[PageIdKey key]
		{
			get
			{
				return (PageId)StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type PageID to
		/// the ASP session data.
		/// </summary>
		/// <value>PageId</value>
		public string this[OneUseKey key]
		{
			get
			{
				return (string)StoredValues[key.ID];
			}
			set
			{
				StoredValues[key.ID] = value;
			}
		}

		/// <summary>
		/// This method is to allow implementation of the IDictionary interface. 
		/// As type-unsafe additions to the table are not allowed this method does
		/// not actually add any data to the StoredValues hashtable.
		/// </summary>
		/// <param name="key">dummy parameter of type object</param>
		/// <param name="data">dummy parameter of type object</param>
 		public void Add(object key, object data)
		{
		}
		
		/// <summary>
		/// This method is to allow implementation of the IDictionary interface.
		/// It sould not be called programatically.
		/// </summary>
		/// <param name="key">An object refering to the key value of the object to be 
		/// deleted from the hashtable</param>
		public void Remove(object key)
		{
			StoredValues.Remove(key);
		}

		/// <summary>
		/// This method is to allow implementation of the IDictionary interface.
		/// Clear() removes all entries from the StoredValues hashtable.
		/// </summary>
		public void Clear()
		{
			StoredValues.Clear();
		}

		/// <summary>
		/// This method is to allow implementation of the IDictionary interface.
		/// A returned Boolean indicates whether the given object key exists in the
		/// StoredValues hashtable.
		/// </summary>
		/// <param name="key">An object refering to the key value of the object to be 
		/// found in the hashtable</param>
		/// <returns>true if entry found.  Otherwise false</returns>
		public Boolean Contains(object key)
		{
			return StoredValues.Contains(key);
		}

		/// <summary>
		/// This property is to allow implementation of the IDictionary interface.
		/// This Read only Boolean property indicates whether the StoredValues hashtable is
		/// read only.  As it is always a Read/Write hashtable false is always returned.
		/// </summary>
		public Boolean IsReadOnly
		{
			get{return false;}
		}

		/// <summary>
		/// This property is to allow implementation of the IDictionary interface.
		/// This Read only Boolean property indicates whether the StoredValues hashtable
		/// is of fixed size.  As it is always a variable size  hashtable false is 
		/// always returned.
		/// </summary>
		public Boolean IsFixedSize
		{
			get{return false;}
		}

		/// <summary>
		/// This property is to allow implementation of the IDictionary interface.
		/// This Read/Write property returns from the StoredValues hashtable a piece of 
		/// data found by use of the given key object.
		/// As type-unsafe retrievals from the table are not allowed this method does
		/// should not be used to retrieve data from the hashtable.
		/// </summary>
		object IDictionary.this[object key]
		{
			get
			{
				return StoredValues[key];
			}
			set
			{
				StoredValues[key] = value;
			}
		}

		/// <summary>
		/// This property is to allow implementation of the ICollection interface.
		/// This Read only property returns a list of all the keys currently contianed in
		/// the StoredValues hashtable.
		/// </summary>
		public ICollection Keys
		{
			get{return StoredValues.Keys;}
		}

		/// <summary>
		/// This property is to allow implementation of the ICollection interface.
		/// This Read only property returns a list of all the values currently contianed in
		/// the StoredValues hashtable.
		/// </summary>
		public ICollection Values
		{
			get{return StoredValues.Values;}
		}

		/// <summary>
		/// This method is to allow implementation of the ICollection interface.
		/// It copies the values form the StoredValues hashtable into a dataArray object.
		/// </summary>
		/// <param name="dataArray">A DataArray to copy the hashtable contents to</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
		public void CopyTo(Array dataArray, int arrayIndex)
		{
			StoredValues.CopyTo(dataArray, arrayIndex);
		}

		/// <summary>
		/// This method is to allow implementation of the ICollection interface.
		/// It copies the values form the StoredValues hashtable into a string[] object.
		/// </summary>
		/// <param name="dataArray">A string[] to copy the hashtable contents to</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
		public void CopyTo(string[] dataArray, int arrayIndex)
		{
			StoredValues.CopyTo(dataArray, arrayIndex);
		}

		/// <summary>
		/// This property is to allow implementation of the ICollection interface.
		/// This Read only property returns the number of values currently contianed in
		/// the StoredValues hashtable.
		/// </summary>
		public int Count
		{
			get{return StoredValues.Count;}
		}

		/// <summary>
		/// This property is to allow implementation of the ICollection interface.
		/// This Read only property returns whether access to the StoredValues 
		/// hashtable is synchronised.
		/// </summary>
		public Boolean IsSynchronized
		{
			get{return StoredValues.IsSynchronized;}
		}

		/// <summary>
		/// This property is to allow implementation of the ICollection interface.
		/// This Read only property, when implemented by a class, gets a value indicating 
		/// whether access to the ICollection is synchronized (thread-safe).
		/// </summary>
		public object SyncRoot
		{
			get{return StoredValues.SyncRoot;}
		}

		/// <summary>
		/// This explicitly defined method allows implementation of the IEnumerator 
		/// interface by distinguishing it from the IDictionary method of the same name.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return StoredValues.GetEnumerator();
		}

		/// <summary>
		/// This method is to allow implementation of the IDictionary interface.
		/// </summary>
		/// <returns>An IDictionaryEnumerator for the current TypeSafeDictionary.</returns>
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return StoredValues.GetEnumerator();
		}
	}
}