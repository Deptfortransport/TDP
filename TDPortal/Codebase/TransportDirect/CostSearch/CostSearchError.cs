// ************************************************************** 
// NAME			: CostSearchError.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 24/01/2005 
// DESCRIPTION	: Implementation of the CostSearchError class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CostSearchError.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:16   mturner
//Initial revision.
//
//   Rev 1.4   Apr 20 2005 11:58:00   rhopkins
//Corrected capiltalisation error on resourceID assignment
//
//   Rev 1.3   Apr 19 2005 19:56:48   RPhilpott
//Add ctor that takes resourceId as parameter.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.2   Mar 14 2005 16:25:50   jmorrissey
//Removed Message field. No longer necessary.
//
//   Rev 1.1   Mar 01 2005 16:52:06   jmorrissey
//Made Serializable
//
//   Rev 1.0   Jan 26 2005 10:26:36   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for CostSearchError.
	/// </summary>
	[Serializable]
	public class CostSearchError
	{
		private string resourceID;		

		public CostSearchError()
		{
		}

		public CostSearchError(string resourceId)
		{
			this.resourceID = resourceId;
		}

		#region properties

		public string ResourceID
		{
			get
			{
				return resourceID;
			}
			set
			{
				resourceID = value;
			}
		}

		#endregion
	}

	
}
