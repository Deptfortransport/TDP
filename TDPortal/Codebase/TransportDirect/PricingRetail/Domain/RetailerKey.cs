// *********************************************** 
// NAME			: RetailerKey.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 06.10.03
// DESCRIPTION	: Implments a key that identifies retailers capable of selling tickets for the
// operator code and travel mode defined by the key value.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RetailerKey.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:56   mturner
//Initial revision.
//
//   Rev 1.7   Jan 18 2006 18:16:34   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.6   Nov 18 2003 16:10:10   COwczarek
//SCR#247 :Add $Log: for PVCS history
 
using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
    /// Implments a key that identifies retailers capable of selling tickets for the operator
    /// code and travel mode defined by the key value.
	/// </summary>
	internal class RetailerKey
	{
        private ModeType mode;
        private string operatorCode;

        // Value to indicate operator code is not significant for lookup of retailer
        public const string IgnoreOperatorCode = "NONE";

        /// <summary>
        /// Constructs a composite key based on the supplied parameters.
        /// </summary>
        /// <param name="operatorCode">The operator code</param>
        /// <param name="mode">The travel mode</param>
		public RetailerKey(string operatorCode, ModeType mode)
		{
            this.mode = mode;
            this.operatorCode = operatorCode;
		}
		
		#region Properties
		
        /// <summary>
        /// The operator code of the composite key.
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode;}
        }

        /// <summary>
        /// The travel mode of the composite key.
        /// </summary>
        public ModeType Mode
        {
            get {return mode;}
        }

        #endregion Properties
        
        #region Public methods
        
        /// <summary>
        /// Determines the equality of two key instances.
        /// </summary>
        /// <returns>True if supplied object is of the same type as this instance and
        /// operator code and travel modes are equal, false otherwise.</returns>
        public override bool Equals(object o) 
        {
            if (o is RetailerKey) 
            {
                RetailerKey retailerKey = (RetailerKey)o;
                return (retailerKey.OperatorCode.Equals(operatorCode) &&
                        retailerKey.Mode.Equals(mode));
            } 
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash value for this instance.
        /// </summary>
        /// <returns>A hash value of this instance</returns>
        public override int GetHashCode()
        {
            // Given that it is anticipated we will have a small number of retailers
            // we can produce a perfect hash
            return (operatorCode.GetHashCode() + mode.GetHashCode());
        }

        #endregion Public methods
        
    }
}
