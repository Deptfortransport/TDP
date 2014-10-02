//********************************************************************************
//NAME         : TocDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for a Train Operator Company
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/TocDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:56   mturner
//Initial revision.
//
//   Rev 1.1   Oct 15 2003 11:33:12   CHosegood
//Toc code is 2 not 3 characters long
//
//   Rev 1.0   Oct 13 2003 13:27:14   CHosegood
//Initial Revision

using System;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object for Train Operator Company.
	/// </summary>
	[Serializable]
	public class TocDto
	{
        private string code = String.Empty;

        /// <summary>
        /// This is the 2 character code that identifies the TOC
        /// </summary>
        public string Code 
        {
            get {return this.code; }
            set 
            {
                if (value != null) 
                {
                    if (value.Length > 2)
                        throw new TDException("Invalid TOC code: " + value, false, TDExceptionIdentifier.PRHInvalidPricingRequest );
                } 
                else 
                {
                    value = string.Empty;
                }

                this.code = value.PadLeft(2, ' ').Substring(0,2);
            }
        }

        /// <summary>
        /// Data Transfer Object for Train Operator Company 
        /// </summary>
        /// <param name="code">The Code that identifies the TOC</param>
		public TocDto(string code)
		{
            this.Code = code;
		}
	}
}
