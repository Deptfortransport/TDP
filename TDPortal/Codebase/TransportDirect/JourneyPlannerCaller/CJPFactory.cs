// *********************************************** 
// NAME                 : CJPFactory.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Class which gets an instance of the CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/CJPFactory.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:20   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 19 2010 15:17:06   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using TransportDirect.JourneyPlanning.CJP;

namespace JourneyPlannerCaller
{
	/// <summary>
	/// Factory used to create CJP instance
	/// </summary>
	public class CjpFactory
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public CjpFactory()
		{
		}

		/// <summary>
		///  Method used to get an
		///  instance of an implementation of ICJP
		/// </summary>
		/// <returns>A new instance of a CJP.</returns>
		public Object Get()
		{
			return new TransportDirect.JourneyPlanning.CJP.CJP();
		}
	}
}
