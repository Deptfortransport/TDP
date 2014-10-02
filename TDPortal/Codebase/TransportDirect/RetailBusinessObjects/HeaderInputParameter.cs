//********************************************************************************
//NAME         : HeaderInputParameter.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/HeaderInputParameter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:06   mturner
//Initial revision.
//
//   Rev 1.0   Oct 13 2003 15:25:42   CHosegood
//Initial Revision
//
//   Rev 1.0   Oct 08 2003 11:46:32   CHosegood
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for HeaderParameter.
	/// </summary>
	public class HeaderInputParameter
	{
        private string parameter;
        private string inputType;

        /// <summary>
        /// A parameter to send to the business object
        /// </summary>
        public String Parameter 
        {
            get { return this.parameter; }
            set { this.parameter = value; }
        }

        /// <summary>
        /// The type of parameter.  Input or Output
        /// </summary>
        public string InputType
        {
            get { return this.inputType; }
        }

        /// <summary>
        /// The length of the parameter
        /// </summary>
        public int Length
        {
            get { return parameter.Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="val"></param>
		public HeaderInputParameter( string parameter )
		{
            this.inputType = "I";
            this.parameter = parameter;
		}
	}
}
