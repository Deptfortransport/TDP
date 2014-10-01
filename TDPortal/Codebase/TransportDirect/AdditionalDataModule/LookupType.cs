// ********************************************************************* 
// NAME                 : LookupType.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of LookupType
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/LookupType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:10   mturner
//Initial revision.
//
//   Rev 1.1   Nov 07 2003 16:29:24   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.0   Oct 16 2003 20:52:40   acaunt
//Initial Revision
using System;
using System.Collections;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Enum type class for the various Types we can lookup given a NaPTAN
	/// A enum isn't used because:
	/// 1. Some of the values have spaces in them
	/// 2. It is convenient to hang to values of the key - type name and category name
	/// </summary>
	[Serializable]
	public class LookupType 
	{
		public static Hashtable lookupTypes = new Hashtable();

		public static LookupType NLC_Code = new LookupType("NLC Code", "Lookup");
		public static LookupType CRS_Code = new LookupType("CRS Code", "Lookup");

		private string type = string.Empty;
		private string category = string.Empty;

		private LookupType(string type, string category)
		{
			this.type = type;
			this.category = category;
			// Add the newly created LookupType to the HashTable for easy retrieval
			lookupTypes.Add(type, this);
		}

		public override string ToString()
		{
			return type;
		}

		public string Type
		{
			get {return type;}
		}

		public string Category
		{
			get {return category;}
		}

		public static LookupType FindLookupType(string type)
		{
			if (lookupTypes.ContainsKey(type))
			{
				return (LookupType)lookupTypes[type];
			}
			else 
			{
				return null;
			}
		}
	}
}
