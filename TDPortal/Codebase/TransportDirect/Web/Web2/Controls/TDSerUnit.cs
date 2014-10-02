// *********************************************** 
// NAME                 : TDSerUnit.cs
// AUTHOR               : Annukka Viitanen
// DATE CREATED         : 03/02/2006 
// DESCRIPTION  		: Class for serialising unit information.
//	Substitute class for Unit replacing any instances of Unit.
//	Allows map state to be serialised and deserialised between page transitions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDSerUnit.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:10   mturner
//Initial revision.
//
//   Rev 1.0   Feb 09 2006 19:18:14   aviitanen
//Initial revision.

using System;
using System.Web.UI.WebControls;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for TDSerUnit.
	/// </summary>
	[Serializable]
	public class TDSerUnit
	{
		/// <summary>
		/// state measurement
		/// </summary>
		private double val;
		
		/// <summary>
		/// Gets/ sets Value
		/// </summary>
		public double Value
		{
			set { val = value; }
			get { return val; }
		}
	
		/// <summary>
		/// Gets Type
		/// </summary>
		public UnitType Type
		{
			get { return UnitType.Pixel; }
		}
	}
}
