//******************************************************************************
//NAME			: TDPrintablePage.cs
//AUTHOR		: Jonathan George
//DATE CREATED	: 06/01/2005
//DESCRIPTION	: A subclass of TDPage used for Printable pages. Will automatically
//set the PrinterFriendly property to true for any control implementing the 
//IPrintable interface
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/TDPrintablePage.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:18:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:04   mturner
//Initial revision.
//
//   Rev 1.3   Jun 08 2006 09:33:12   mmodi
//IR4114. Text formatting methods added to be used to format location names on Printable map pages
//Resolution for 4114: Map Locations with long names are cut when Printer Friendly is printed
//
//   Rev 1.2   Feb 23 2006 19:16:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:54:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Mar 22 2005 09:49:30   jgeorge
//FxCop changes
//
//   Rev 1.0   Jan 18 2005 11:41:40   jgeorge
//Initial revision.

using System;
using System.Text;
using System.Web.UI;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for TDPrintablePage.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TDPrintablePage : TDPage
	{
		/// <summary>
		/// Constructor. Does nothing extra
		/// </summary>
		public TDPrintablePage() : base()
		{
		}

		protected override void OnLoad(EventArgs e)
		{
			// Loop through all controls on the page and set the PrinterFriendly
			// property of any IPrintable control to true.
			SetPrinterFriendly(Controls);
			base.OnLoad(e);
		}

		private void SetPrinterFriendly(ControlCollection controls)
		{
			foreach (Control c in controls)
			{
				IPrintable printable = c as IPrintable;
				if (printable != null)
					printable.PrinterFriendly = true;
				if (c.HasControls())
					SetPrinterFriendly(c.Controls);
			}
		}


		#region Text formatting methods

		/// <summary>
		/// Method to include spaces after commas in specified text
		/// </summary>
		/// <param name="textToAddSpaces">Text string to add spaces to</param>
		protected string AddSpacesToText (string textToAddSpaces)
		{
			StringBuilder sentence = new StringBuilder(textToAddSpaces);
			// Add space after comma. But because locations are not consistent e.g. Attractions 
			// have space after comma, and Addresses dont, we remove the space before applying 
			// the change. This retains any original comma space formatting in the text
			sentence.Replace(", ", ",");
			sentence.Replace(",", ", ");

			return sentence.ToString();
		}

		/// <summary>
		/// Method to wrap text at specified length 
		/// </summary>
		/// <param name="textToWrap">Text string to wrap</param>
		/// <param name="maxChars">Max number of characters per line</param>
		/// <param name="separator">New line separator to be included in text</param>
		protected string WrapText(string textToWrap, int maxChars, string separator)
		{
			if (textToWrap.Length < maxChars)
			{
				return textToWrap;
			}
			// Ensure wrapping only occurs at the end of a word
			string[] words = textToWrap.Split(new char[] {' '});
			StringBuilder sentence = new StringBuilder("");
			StringBuilder phrase = new StringBuilder("");

			foreach (string word in words)
			{
				if (phrase.Length == 0 || phrase.Length + word.Length + 1 > maxChars)
				{
					if (sentence.Length > 0)
					{
						sentence.Append(separator);
					}
					sentence.Append(phrase);
					phrase = new StringBuilder(word);
				}
				else
				{
					phrase.Append(" ");
					phrase.Append(word);
				}
			}
			if (sentence.Length > 0)
			{
				sentence.Append(separator);
			}
			sentence.Append(phrase);
			
			return sentence.ToString();
		}

		/// <summary>
		/// Method to wrap text at specified length 
		/// </summary>
		/// <param name="textToWrap">Text string to wrap</param>
		/// <param name="maxChars">Max number of characters per line</param>
		protected string WrapText(string textToWrap, int maxChars)
		{
			string separator = "<br />";
			
			return WrapText(textToWrap, maxChars, separator);
		}
		#endregion
	}
}
