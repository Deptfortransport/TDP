//********************************************************************************
//NAME         : RestrictFares.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : RestrictFares - abstract base class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFares.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
//
//   Rev 1.5   Mar 22 2005 16:09:16   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.4   Jun 17 2004 13:33:10   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.3   Apr 15 2004 17:03:56   CHosegood
//Now applies minimum fare on child tickets as well as adult
//Resolution for 663: Rail fares not being displayed
//
//   Rev 1.2   Oct 15 2003 20:04:38   CHosegood
//Now returns the collection of fare(s) instead of the Fares object

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// RestrictFares - abstract base class.
	/// </summary>
	public abstract class RestrictFares : IRestrictFares
	{
		protected BusinessObjectInput input;
		protected BusinessObjectOutput output;
		protected string stringFunctionID;
		protected FunctionIDType enumFunctionID;
		
		protected RestrictFares(BusinessObjectInput input, BusinessObjectOutput output)
		{
			this.input  = input;
			this.output = output;
		}

		public abstract ArrayList Restrict(ArrayList fares, Fare fare);
		
		public abstract BusinessObjectOutput FilterOutput(PricingRequestDto request);
		
		public FunctionIDType FunctionID
		{
			get { return enumFunctionID; }
		}

		public string FunctionIDString
		{
			get { return stringFunctionID; }
		}

	}
}
	
		


