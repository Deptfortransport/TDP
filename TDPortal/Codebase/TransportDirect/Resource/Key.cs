// ***********************************************
// NAME 		: Key.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: A number of seperate classes where each
// defines a KeyType for a specific data type that can be saved
// by the session manager.  
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/Key.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:41:08   mturner
//Initial revision.
//
//   Rev 1.9   Apr 23 2005 15:54:22   COwczarek
//Add key classes for deferred arrays
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.8   Jan 26 2005 15:29:12   PNorell
//Added new keys.
//Added new key-classes and interfaces.
//
//   Rev 1.7   Nov 03 2004 14:54:50   Schand
//// added Seasonal Notice board
//
//   Rev 1.6   Mar 10 2004 15:53:12   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.5   Jan 21 2004 11:34:48   PNorell
//Updates for 5.2
//
//   Rev 1.4   Jul 17 2003 13:46:36   kcheung
//Added boolkey
//
//   Rev 1.3   Jul 17 2003 12:15:54   kcheung
//Updated class header summary comments
//
//   Rev 1.2   Jul 17 2003 12:13:22   kcheung
//Added the keys for transition event and page id
//
//   Rev 1.1   Jul 07 2003 17:10:54   AWindley
//Added comments
//
//   Rev 1.0   Jul 03 2003 17:30:10   AWindley
//Initial Revision

using System;

namespace TransportDirect.UserPortal.Resource
{
	// Before any new data types can be stored by the session manager
	// they must have a class entered into this file which corresponds to
	// the new data type.  This class must implement the IKey interface.


	/// <summary>
	/// Base functionality that are used by almost every key
	/// </summary>
	public class AbstractKey : IKey
	{
		protected string KeyConst = string.Empty;

		/// <summary>
		/// Get ID string representing TypePrefix + KeyName 
		/// </summary>
		public string ID
		{
			get	{return KeyConst;}
		}

		/// <summary>
		/// A string representation of this key.
		/// </summary>
		/// <returns>The string representation of this key</returns>
		public override string ToString()
		{
			return KeyConst;
		}

		/// <summary>
		/// Returns a hash code suitable for being used in hash tables or other types
		/// of hash:ed collections
		/// </summary>
		/// <returns>A suitable hash code</returns>
		public override int GetHashCode()
		{
			return KeyConst.GetHashCode();
		}
	}


	/// <summary>
	/// DateKey class for the DateTime data type
	/// </summary>
	public class DateKey : AbstractKey
	{
		public DateKey(string keyName)
		{
			KeyConst = "dte@" + keyName;
		}
	}

	/// <summary>
	/// DoubleKey class for the Double data type
	/// </summary>
	public class DoubleKey : AbstractKey
	{
		public DoubleKey(string keyName)
		{
			KeyConst = "dbl@" + keyName;
		}
	}

	/// <summary>
	/// IntKey class for the int32 data type
	/// </summary>
	public class IntKey : AbstractKey
	{
		public IntKey(string keyName)
		{
			KeyConst = "int@" + keyName;
		}
	}

	/// <summary>
	/// StringKey class for the string data type
	/// </summary>
	public class StringKey : AbstractKey
	{
		public StringKey(string keyName)
		{
			KeyConst = "str@" + keyName;
		}
	}

	/// <summary>
	/// BoolKey class for the bool data type
	/// </summary>
	public class BoolKey : AbstractKey
	{
		public BoolKey(string keyName)
		{
			KeyConst = "bool@" + keyName;
		}

	}

	/// <summary>
	/// PageIdKey class for the pageId data type
	/// </summary>
	public class PageIdKey : AbstractKey
	{
		public PageIdKey(string keyName)
		{
			KeyConst = "pgId@" + keyName;
		}
	}

	/// <summary>
	/// TransitionEventKey class for the transitionEvent data type
	/// </summary>
	public class TransitionEventKey : AbstractKey
	{
		public TransitionEventKey(string keyName)
		{
			KeyConst = "tev@" + keyName;
		}
	}

	/// <summary>
	/// The key identifier for keys that should only exists one page-cycle (register in page a, accessible in page b, removed for page c)
	/// </summary>
	public class OneUseKey : AbstractKey
	{
		public const string PREFIX = "1pass@";

		public OneUseKey(string keyName) : this( keyName, false )
		{
			
		}

		public OneUseKey(string keyName, bool prefixed)
		{
			KeyConst = (prefixed ? string.Empty : PREFIX) + keyName;
		}

		public override bool Equals(object o )
		{
			return o is OneUseKey && ((OneUseKey)o).KeyConst == KeyConst;
		}

		public static bool operator ==(OneUseKey compare, OneUseKey comparee)
		{
			return compare.Equals( comparee );
		}

		public static bool operator !=(OneUseKey compare, OneUseKey comparee)
		{
			return !compare.Equals( comparee );
		}

		/// <summary>
		/// Returns a hash code suitable for being used in hash tables or other types
		/// of hash:ed collections
		/// </summary>
		/// <returns>A suitable hash code</returns>
		public override int GetHashCode()
		{
			return KeyConst.GetHashCode();
		}

	}

	public class UserKey : AbstractKey
	{
		public UserKey(string keyName)
		{
			KeyConst = "usr@" + keyName;
		}

	}

    /// <summary>
    /// Key for saving objects in deferred storage
    /// </summary>
    public class DeferredKey : AbstractKey
	{
		public DeferredKey(string keyName)
		{
			KeyConst = "def@" + keyName;
		}
		
		/// <summary>
		/// So this keytype can be extended without changing the KeyConst.
		/// </summary>
		protected DeferredKey()
		{
		}
	}

    /// <summary>
    /// Key type where object needs to be saved in different partitions
    /// </summary>
    public class PartionableDeferredKey : DeferredKey, ITDSessionPartionable
	{
		public PartionableDeferredKey(string keyName) : base()
		{
			KeyConst = "pdef@"+keyName;
		}
	}

    /// <summary>
    /// Key type used for arrays where each element requires individual serialization/deserialization
    /// and where array needs to be saved in different partitions
    /// </summary>
    public class PartionableDeferredArrayKey : DeferredKey, ITDSessionPartionable, ITDDeferredArray
    {
        public PartionableDeferredArrayKey(string keyName) : base()
        {
            KeyConst = "pdef@"+keyName;
        }
    }

    /// <summary>
    /// Key type used for arrays where each element requires individual serialization/deserialization
    /// </summary>
    public class DeferredArrayKey : DeferredKey, ITDDeferredArray
    {
        public DeferredArrayKey(string keyName) : base()
        {
            KeyConst = "def@" + keyName;
        }
    }

}