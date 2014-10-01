// *********************************************** 
// NAME                 : OSGridReference.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 11/01/2006
// DESCRIPTION  		: OSGridReference Class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/OSGridReference.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 19:38:40   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 12 2006 15:41:28   RWilby
//Added class attributes and xml comments
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	/// <summary>
	/// Represents a specific OSGridReference
	/// </summary>
	[Serializable]
	public class OSGridReference
	{
		private int easting;   
		private int northing;


		public OSGridReference()
		{
		}

		/// <summary>
		/// Read/Write Property. Easting
		/// </summary>
		public int Easting
		{
			get {return easting;}
			set {easting = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Northing
		/// </summary>
		public int Northing
		{
			get {return northing;}
			set {northing = value;}
		}

	}// END CLASS DEFINITION OSGridReference
}



