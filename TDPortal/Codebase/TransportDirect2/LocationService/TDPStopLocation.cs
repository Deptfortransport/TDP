// *********************************************** 
// NAME             : TDPStopLocation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: Represents a Stop location, containing additional location code types
// ************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Represents a Stop location, containing additional location code types
    /// </summary>
    [Serializable]
    public class TDPStopLocation : TDPLocation
    {
        #region Private Fields

        private TDPCodeType codeType = TDPCodeType.NAPTAN;

        private string codeCRS = string.Empty;
        private string codeSMS = string.Empty;
        private string codeIATA = string.Empty;
                
        #endregion

        #region Constructors

        /// <summary>
        /// TDPStopLocation Constructor
        /// </summary>
        public TDPStopLocation()
            : base()
        {

        }

        /// <summary>
        /// TDPStopLocation Constructor
        /// </summary>
        public TDPStopLocation(string locationDisplayName, 
            TDPLocationType locationType, TDPLocationType locationTypeActual, string identifier,
            TDPCodeType codeType, string codeCRS, string codeSMS, string codeIATA)
            : base(locationDisplayName, locationType, locationTypeActual, identifier)
        {
            this.codeType = codeType;
            this.codeCRS = codeCRS;
            this.codeSMS = codeSMS;
            this.codeIATA = codeIATA;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. TDPCodeType for this location
        /// </summary>
        public TDPCodeType CodeType
        {
            get { return codeType; }
            set { codeType = value; }
        }

        /// <summary>
        /// Read only. Returns the code dependent on the code type
        /// </summary>
        public string Code
        {
            get
            {
                switch (codeType)
                {
                    case TDPCodeType.CRS:
                        return codeCRS;
                    case TDPCodeType.SMS:
                        return codeSMS;
                    case TDPCodeType.IATA:
                        return codeIATA;
                    case TDPCodeType.NAPTAN:
                        if (Naptan != null && Naptan.Count > 0)
                            return Naptan[0];
                        else
                            return string.Empty;
                    case TDPCodeType.Postcode:
                        return Name;
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Read/Write. CRS Code for this location
        /// </summary>
        public string CodeCRS
        {
            get { return codeCRS; }
            set { codeCRS = value; }
        }

        /// <summary>
        /// Read/Write. SMS Code for this location
        /// </summary>
        public string CodeSMS
        {
            get { return codeSMS; }
            set { codeSMS = value; }
        }

        /// <summary>
        /// Read/Write. IATA Code for this location
        /// </summary>
        public string CodeIATA
        {
            get { return codeIATA; }
            set { codeIATA = value; }
        }
                        
        #endregion

        #region Protected methods override
        
        /// <summary>
        /// Returns a formatted string representing the contents of this TDPStopLocation
        /// </summary>
        /// <returns></returns>
        public override string ToString(bool htmlLineBreaks)
        {
            string linebreak = (htmlLineBreaks) ? "<br />" : string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString(htmlLineBreaks));
            sb.AppendLine(string.Format("codeType: {0} {1}", codeType.ToString(), linebreak));
            sb.AppendLine(string.Format("codeCRS: {0} {1}", codeCRS, linebreak));       
            sb.AppendLine(string.Format("codeSMS: {0} {1}", codeSMS, linebreak));
            sb.AppendLine(string.Format("codeIATA: {0} {1}", codeIATA, linebreak));
            
            return sb.ToString();
        }

        #endregion
    }
}
