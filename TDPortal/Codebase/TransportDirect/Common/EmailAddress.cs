// ********************************************************************* 
// NAME                 : EmailAddress.cs 
// AUTHOR               : Richard Smith of The Code Project 
//						  Adapted for TDP by Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION			: RFC822 Compliant Email Address Validator
// This code was downloaded from The Code Project website and may
// be used free of charge according to The Code Project terms and
// conditions. www.codetools.com/csharp/RFC822Validator.asp  
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/EmailAddress.cs-arc  $ 
//
//   Rev 1.1   Jan 13 2010 16:54:02   apatel
//updated EmailAddress(string emailAddy) method to not initialize regex every time
//Resolution for 5368: Logon to portal slow
//
//   Rev 1.0   Nov 08 2007 12:19:02   mturner
//Initial revision.
//
//   Rev 1.2   May 04 2006 10:36:18   pscott
//IR 4073 vantive 4262881
//Do not allow email addresses which contain spaces
//
//   Rev 1.1   Sep 04 2003 09:53:38   geaton
//Updated file header.

using System;
using System.Text.RegularExpressions;

namespace TransportDirect.Common
{
	/// <summary>
	/// This class implements Jeffrey E.F. Friedl's rfc 822 compliant email validator routines.
	/// </summary>
	public class EmailAddress
	{
		// static so that it's shared accross multiple instances.
		private static Regex oRegex;

		// Constants to combat backslashitis
		private const string Escape		= @"\\";
		private const string Period		= @"\.";
		private const string Space		= @"\040";
		private const string Tab		= @"\t";
		private const string OpenBr		= @"\[";
		private const string CloseBr	= @"\]";
		private const string OpenParen	= @"\(";
		private const string CloseParen = @"\)";
		private const string NonAscii	= @"\x80-\xff";
		private const string Ctrl		= @"\000-\037";
		private const string CRList		= @"\n\015"; // Should only really be \015
		
		private string sMailBox;
		private string sLocalPart;
		private string sDomain;
		private string sQuotedStr;
		private bool bIsValid = false;

		public string LocalPart
		{
			get { return sLocalPart; }
		}

		public string Domain
		{
			get { return sDomain; }
		}

		public string QuotedString
		{
			get { return sQuotedStr; }
		}

		public string Mailbox
		{
			get { return sMailBox; }
		}

		public bool IsValid
		{
			get { return bIsValid; }
		}

		public EmailAddress()
		{
			// initialise the regex...
			initRegex();
		}

		public EmailAddress(string emailAddy)
		{
			// initialise the regex...
            if (EmailAddress.oRegex == null)
            {
                initRegex();
            } 

            Parse(emailAddy);
		}
		/// <summary>
		/// This is the actual implementation of the parse routine.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public bool Parse(string email)
		{
			// Match against the regex...
			Match m = EmailAddress.oRegex.Match(email);

			this.bIsValid = m.Success;
			this.sDomain = m.Groups["domain"].ToString();
			this.sLocalPart = m.Groups["localpart"].ToString();
			this.sMailBox = m.Groups["mailbox"].ToString();
			this.sQuotedStr = m.Groups["quotedstr"].ToString();
			if (this.bIsValid && email.IndexOf(' ',0,email.Length)> 0)
			{
				// Although allowed by rfc 822 compliance regular expression
				// most SMTP systems will reject addresses containing spaces
				// it is a special address that the public will not be using
				// as their username 
				//therefore if it does contain spaces disallow
				this.bIsValid = false;
			}

			return this.bIsValid;
		}

		/// <summary>
		/// Init regex initialised the huge regex and compiles it so that it runs a little faster.
		/// </summary>
		private void initRegex()
		{
		
			// for within "";
			string qtext = @"[^" + EmailAddress.Escape + 
				EmailAddress.NonAscii + 
				EmailAddress.CRList + "\"]";
			string dtext = @"[^" + EmailAddress.Escape + 
				EmailAddress.NonAscii + 
				EmailAddress.CRList + 
				EmailAddress.OpenBr + 
				EmailAddress.CloseBr + "\"]";

			string quoted_pair = " " + EmailAddress.Escape + " [^" + EmailAddress.NonAscii + "] ";

			// *********************************************
			// comments.
			// Impossible to do properly with a regex, I make do by allowing at most 
			// one level of nesting.
			string ctext = @" [^" + EmailAddress.Escape + 
				EmailAddress.NonAscii + 
				EmailAddress.CRList + "()] ";
			
			// Nested quoted Pairs
			string Cnested = "";
			Cnested += EmailAddress.OpenParen;
			Cnested += ctext + "*";
			Cnested += "(?:" + quoted_pair + " " + ctext + "*)*";
			Cnested += EmailAddress.CloseParen;

			// A Comment Usually 
			string comment = "";
			comment += EmailAddress.OpenParen;
			comment += ctext + "*";
			comment += "(?:";
			comment += "(?: " + quoted_pair + " | " + Cnested + ")";
			comment += ctext + "*";
			comment += ")*";
			comment += EmailAddress.CloseParen;
			
			// *********************************************
			// X is optional whitespace/comments
			string X = "";
			X += "[" + EmailAddress.Space + EmailAddress.Tab + "]*";
			X += "(?: " + comment + " [" + EmailAddress.Space + EmailAddress.Tab + "]* )*";
			
			// an email address atom...
			string atom_char = @"[^(" + EmailAddress.Space + ")<>\\@,;:\\\"." + EmailAddress.Escape + EmailAddress.OpenBr + 
				EmailAddress.CloseBr +
				EmailAddress.Ctrl +
				EmailAddress.NonAscii + "]";
			string atom = "";
				atom += atom_char + "+";
				atom += "(?!" + atom_char + ")";

			// doublequoted string, unrolled.
			string quoted_str = "(?'quotedstr'";
				quoted_str += "\\\"";
				quoted_str += qtext + " *";
				quoted_str += "(?: " + quoted_pair + qtext + " * )*";
				quoted_str += "\\\")";

			// A word is an atom or quoted string
			string word = "";
				word += "(?:";
				word += atom;
				word += "|";
				word += quoted_str;
				word += ")";
			
			// A domain-ref is just an atom
			string domain_ref = atom;

			// A domain-literal is like a quoted string, but [...] instead of "..."
			string domain_lit = "";
				domain_lit += EmailAddress.OpenBr;
				domain_lit += "(?: " + dtext + " | " + quoted_pair + " )*";
				domain_lit += EmailAddress.CloseBr;

			// A sub-domain is a domain-ref or a domain-literal
			string  sub_domain = "";
				sub_domain += "(?:";
				sub_domain += domain_ref;
				sub_domain += "|";
				sub_domain += domain_lit;
				sub_domain += ")";
			sub_domain += X;

			// a domain is a list of subdomains separated by dots
			string domain = "(?'domain'";
				domain += sub_domain;
				domain += "(:?";
				domain += EmailAddress.Period + " " + X + " " + sub_domain;
				domain += ")*)";

			// a a route. A bunch of "@ domain" separated by commas, followed by a colon.
			string route = "";
				route += "\\@ " + X + " " + domain;
				route += "(?: , " + X + " \\@ " + X + " " + domain + ")*";
				route += ":";
				route += X;
				
			// a local-part is a bunch of 'word' separated by periods
			string local_part = "(?'localpart'";
				local_part += word + " " + X;
				local_part += "(?:";
				local_part += EmailAddress.Period + " " + X + " " + word + " " + X;
				local_part += ")*)";

			// an addr-spec is local@domain
			string addr_spec = local_part + " \\@ " + X + " " + domain;
            
			// a route-addr is <route? addr-spec>
			string route_addr = "";
				route_addr += "< " + X;
				route_addr += "(?: " + route + " )?";
				route_addr += addr_spec;
				route_addr += ">";

			// a phrase........
			string phrase_ctrl = @"\000-\010\012-\037";

			// Like atom-char, but without listing space, and uses phrase_ctrl.
			// Since the class is negated, this matches the same as atom-char plus space and tab
			
			string phrase_char = "[^()<>\\@,;:\\\"." + EmailAddress.Escape + 
				EmailAddress.OpenBr +
				EmailAddress.CloseBr + 
				EmailAddress.NonAscii + 
				phrase_ctrl + "]";
			
			// We've worked it so that word, comment, and quoted_str to not consume trailing X
			// because we take care of it manually
			string phrase = "";
				phrase += word;
				phrase += phrase_char;
				phrase += "(?:";
				phrase += "(?: " + comment + " | " + quoted_str + " )";
				phrase += phrase_char + " *";
				phrase += ")*";
			
			// A mailbox is an addr_spec or a phrase/route_addr
			string mailbox = "";
				mailbox += X;
				mailbox += "(?'mailbox'";
				mailbox += addr_spec;
				mailbox += "|";
				mailbox += phrase + " " + route_addr;
				mailbox += ")";
			
			// okay, now setup the object... We'll compile it since this is a rather large (euphemistically 
			// speaking) regex... We also need to IgnorePatternWhitespace unless it's escaped.

			EmailAddress.oRegex = new Regex(mailbox,RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
		}
	}

}