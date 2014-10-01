//**************************************************************
//NAME			: TDCultureInfo.cs
//AUTHOR		: Callum Shillan
//DATE CREATED	: 01/07/2003
//DESCRIPTION	: Extends the CultureInfo class to allow custom 
//				  cultures to be created 
//**************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDCultureInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:04   mturner
//Initial revision.
//
//   Rev 1.0   Jan 30 2006 14:20:18   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jul 03 2003 10:29:42   JMorrissey
//Changed property name back to LCID
//
//   Rev 1.5   Jul 02 2003 15:24:40   JMorrissey
//Changed parameter name from LCID to LCID (pascal case)

//Rev 1.4   Jul 02 2003 15:17:34   JMorrissey

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Summary description for CultureEx.
	/// </summary>
	public class TDCultureInfo : System.Globalization.CultureInfo
	{
		
		private string cultureName;
		private int cultureLCID;

		/// <summary>
		/// Creates an extended culture info object
		/// </summary>
		/// <param name="cultureNameBase">The base culture on which the new one will be built</param>
		/// <param name="cultureName">The name of the new culture</param>
		/// <param name="cultureLCID">The Locale ID of the new culture</param>
		public TDCultureInfo( string cultureNameBase, string cultureName, int cultureLCID ) : base(cultureNameBase)
		{
			//
			// Remember our culture name and locale id
			//
			this.cultureName = cultureName;
			this.cultureLCID = cultureLCID;
		}

		//
		// Override the base cultureinfo's DisplayName property
		//
		public override string DisplayName
		{
			get
			{
				if ( this.Name == "cy-GB" )
				{
					//
					// Force a suitable return value for Welsh
					//
					return ("Welsh (United Kingdom)");
				}
				else
				{
					//
					// Otherwise, return the underlying cultureinfo's property
					//
					return ( base.DisplayName );
				}

			}
		}


		//
		// Override the base cultureinfo's EnglishName property
		//
		public override string EnglishName
		{
			get
			{
				if ( this.Name == "cy-GB" )
				{
					//
					// Force a suitable return value for Welsh
					//
					return ("Welsh (United Kingdom)");
				}
				else
				{
					//
					// Otherwise, return the underlying cultureinfo's property
					//
					return ( base.EnglishName );
				}
			}
		}

		//
		// Override the base cultureinfo's NativeName property
		//
		public override string NativeName
		{
			get
			{
				if ( this.Name == "cy-GB" )
				{
					//
					// Force a suitable return value for Welsh
					//
					return ("Cymraeg (Deyrnas Unedig)");
				}
				else
				{
					//
					// Otherwise, return the underlying cultureinfo's property
					//
					return base.NativeName;
				}
			}
		}

		//
		// Override the base cultureinfo's Name property
		//
		public override string Name
		{
			get
			{
				//
				// Return our cultureName
				//
				return cultureName;
			}
		}

		//
		// Override the base cultureinfo's LCID property
		//
		public override int LCID
		{
			get
			{
				if ( this.Name == "cy-GB" )
				{
					//
					// Force a suitable return value for Welsh
					//
					return (1106);
				}
				else
				{
					//
					// Otherwise, return the underlying cultureinfo's property
					//
					return base.LCID;
				}
			}
		}
	}
}
