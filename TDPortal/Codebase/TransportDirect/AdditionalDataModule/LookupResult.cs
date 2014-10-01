// ********************************************************************* 
// NAME                 : LookupResult.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of LookupResult
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/LookupResult.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:08   mturner
//Initial revision.
//
//   Rev 1.0   Oct 16 2003 20:52:38   acaunt
//Initial Revision
using System;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Summary description for LookupResult.
	/// </summary>
	public class LookupResult
	{
		private LookupType type;
		private string value;

		public LookupResult(LookupType type, string value)
		{
			this.type = type;
			this.value = value;
		}

		public LookupType Type
		{
			get {return type;}
		}

		public string Value
		{
			get {return value;}
		}
	}
}
