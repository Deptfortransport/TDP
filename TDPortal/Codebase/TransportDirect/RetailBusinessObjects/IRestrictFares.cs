//********************************************************************************
//NAME         : IRestrictFares.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Interface for RBO request processing wrappers.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/IRestrictFares.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:17:16   mmodi
//Add new MR method call
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.1   Mar 22 2005 16:09:08   RPhilpott
//Addition of cost-based search for Del 7.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

	public enum FunctionIDType
	{
		GA,
		GB,
		GC,
		GD,
		GL,
		GM,
		GN,
		GO,
		GP,
		GQ,
		GR,
		GS,
        MR

	}

	/// <summary>
	/// Interface for RBO request processing wrappers.
	/// </summary>
	public interface IRestrictFares
	{
		FunctionIDType FunctionID{get;}
		string FunctionIDString{get;}
		ArrayList Restrict (ArrayList fares, Fare fare);
		BusinessObjectOutput FilterOutput (PricingRequestDto request);
		
	}
}
