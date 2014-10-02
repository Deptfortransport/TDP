//********************************************************************************
//NAME         : Route.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Route.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
//
//   Rev 1.0   Oct 13 2003 15:25:50   CHosegood
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for Route.
	/// </summary>
	public class Route
	{
        private string code = string.Empty;
        private bool crossLondon;

        /// <summary>
        /// 
        /// </summary>
        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CrossLondon 
        {
            get 
            { 
                if ( crossLondon.Equals( true ) )
                    return( "Y" );
                else
                    return( "N" );
            }
            set 
            {
                if ( value.Equals( 1.ToString() ) )
                    this.crossLondon = true;
                else
                    this.crossLondon = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="crossLondon"></param>
		public Route(string code, string crossLondon)
		{
            this.code = code;
            this.CrossLondon = crossLondon;
		}
	}
}
