//********************************************************************************
//NAME         : UnprocessedFareData.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 21/10/2003
//DESCRIPTION  : Implementation of UnprocessedFareData class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/UnprocessedFareData.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.3   May 20 2004 14:15:34   acaunt
//Updated to merge otherwise incompatible journey legs if the Atkins data indicates that there should be in a single PricingUnit.
//
//   Rev 1.2   Oct 26 2003 15:51:08   acaunt
//bug fix
//
//   Rev 1.1   Oct 22 2003 11:36:44   COwczarek
//Add serializable attribute to class
//
//   Rev 1.0   Oct 22 2003 10:15:56   acaunt
//Initial Revision
using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Wrapper class to generalise the CJP specific information that we pass through PricingUnits and PriceSuppliers.
	/// Not essential but keeps dependencies tidy
	/// </summary>
	[Serializable]
	public class UnprocessedFareData
	{

		Object unprocessedData;
		int[] referencedLegs;

		public UnprocessedFareData(Object fareData, int[] referencedLegs)
		{
			this.unprocessedData = fareData;
			this.referencedLegs = referencedLegs;
		}

		public Object UnprocessedData
		{
			get {return unprocessedData;}
		}

		public int[] ReferencedLegs
		{
			get {return referencedLegs;}
	
		}
	}
}
