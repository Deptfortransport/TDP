// *********************************************** 
// NAME                 : FindNearestFuelGenie.cs
// AUTHOR               : Phil Scott
// DATE CREATED         : 15/12/2011
// DESCRIPTION  		: FindNearestFuelGenie Class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/FindNearest/v1/FindNearestFuelGenie.cs-arc  $
//
//   Rev 1.0   Jan 12 2012 15:41:56   PScott
//Initial revision.
//Resolution for 5781: Fuel Genie EES
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	/// <summary>
	/// Represents a specific FindNearestFuelGenie
	/// </summary>
	[Serializable]
	public class FindNearestFuelGenie
	{
        private string postCode;
        private string fuelGenieSite;
        private int easting;   
		private int northing;
        private string brand;
        private string siteReference1;
        private string siteReference2;
        private string siteReference3;
        private string siteReference4;
        private float miles;
 


		public FindNearestFuelGenie()
		{
		}

        /// <summary>
        /// Read/Write Property. PostCode
        /// </summary>
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }
        /// <summary>
        /// Read/Write Property. PostCode
        /// </summary>
        public string FuelGenieSite
        {
            get { return fuelGenieSite; }
            set { fuelGenieSite = value; }
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

        /// <summary>
        /// Read/Write Property. Brand
        /// </summary>
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        /// <summary>
        /// Read/Write Property. SiteReference1
        /// </summary>
        public string SiteReference1
        {
            get { return siteReference1; }
            set{ siteReference1 = value; }
        }

        /// <summary>
        /// Read/Write Property. SiteReference2
        /// </summary>
        public string SiteReference2
        {
            get { return siteReference2; }
            set { siteReference2 = value; }
        }
        /// <summary>
        /// Read/Write Property. SiteReference3
        /// </summary>
        public string SiteReference3
        {
            get { return siteReference3; }
            set { siteReference3 = value; }
        }
        /// <summary>
        /// Read/Write Property. SiteReference4
        /// </summary>
        public string SiteReference4
        {
            get { return siteReference4; }
            set { siteReference4 = value; }
        }

        /// <summary>
        /// Read/Write Property. Miles
        /// </summary>
        public float Miles
        {
            get { return miles; }
            set { miles = value; }
        }
		
	}// END CLASS DEFINITION FindNearestFuelGenie
}



