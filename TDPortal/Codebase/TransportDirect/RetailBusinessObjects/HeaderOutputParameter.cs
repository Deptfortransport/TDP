//********************************************************************************
//NAME         : HeaderOutputParameter.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/HeaderOutputParameter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.0   Oct 13 2003 15:25:44   CHosegood
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
	public class HeaderOutputParameter
	{
        private string inputType;
        private int length;

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
            get { return this.length; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="val"></param>
		public HeaderOutputParameter( int length )
		{
            this.inputType = "O";
            this.length = length;
		}
	}
}
