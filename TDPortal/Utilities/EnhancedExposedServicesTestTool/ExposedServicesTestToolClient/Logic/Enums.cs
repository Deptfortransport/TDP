// ***********************************************************************************
// NAME 		: StatusEnum
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: eumeration classes for the testtool
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/Enums.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:14   mturner
//Initial revision.
//
//   Rev 1.3   Feb 07 2006 10:19:24   mdambrine
//added log

using System;


namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// eumeration classes for the testtool
	/// </summary>
	public enum Status
	{
		None = 0,
		Created = 1,
		Pending = 2,
		Received = 3,
		Error = 4,
		All = 5
	}
	
}
