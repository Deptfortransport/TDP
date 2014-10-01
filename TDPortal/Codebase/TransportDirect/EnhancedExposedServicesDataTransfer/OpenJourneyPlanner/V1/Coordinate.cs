// *********************************************** 
// NAME                 : Coordinate.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a easting/northing coordinate. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Coordinate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:36   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:20:28   COwczarek
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1

{

    /// <summary>
    /// This class represents a easting/northing coordinate. 
    /// </summary>
    [Serializable]
    public class Coordinate
    {

        public Coordinate(){}

        private int easting;
        private int northing;

        /// <summary>
        /// Read/write property that is a 6 digit easting distance (metres).
        /// </summary>
        public int Easting
        {
            get {  return easting; }
            set { easting = value; }    
        }
    
        /// <summary>
        /// Read/write property that is a 6 or 7 digit northing distance (metres).
        /// </summary>
        public int Northing
        {
            get {  return northing; }
            set { northing = value; }    
        }
    }
 
}