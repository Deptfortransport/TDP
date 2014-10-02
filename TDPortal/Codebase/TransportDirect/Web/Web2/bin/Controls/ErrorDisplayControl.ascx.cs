//******************************************************************************
//NAME			: ErrorDisplayControl.cs
//AUTHOR		: Andrew Sinclair
//DATE CREATED	: 11/07/2005
//DESCRIPTION	: Control used to display an error or 
// a warning message.   
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ErrorDisplayControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Nov 20 2009 09:26:16   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jan 08 2009 14:57:12   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jul 02 2008 13:03:00   rbroddle
//Added alt text for error/warning image
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.2   Mar 31 2008 13:19:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:06   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.1   Jan 30 2006 14:41:02   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4.1.0   Jan 10 2006 15:24:00   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Oct 18 2005 17:30:58   scraddock
//Corrected typo and removed commented out code
//
//   Rev 1.3   Oct 18 2005 17:20:52   scraddock
//prevent display of journey ref number text in error message display
//
//   Rev 1.2   Aug 10 2005 11:06:28   asinclair
//Added text strings to langstrings
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 11:56:28   jgeorge
//Updated to allow multiple error messages to be passed.
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 20 2005 18:35:18   asinclair
//Initial revision.
namespace  TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;


	/// <summary>
	/// Enumeration to identify error type
	/// </summary>
	public enum ErrorsDisplayType
	{
		Error,
		Warning,
        Custom
	}

	/// <summary>
	///		Summary description for ErrorDisplayControl.
	/// </summary>
	public partial class ErrorDisplayControl : TDUserControl
	{
		protected System.Web.UI.WebControls.Label labelErrorMessageText;

		private string[] errorStrings = new string[0];
		private string referenceNumber;
		private ErrorsDisplayType type;
        private string errorsDisplayTypeText = string.Empty;

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if (type == ErrorsDisplayType.Error)
			{
				imageErrorType.ImageUrl = resourceManager.GetString( "ErrorDisplayControl.ErrorImageUrl", TDCultureInfo.CurrentUICulture );
                imageErrorType.AlternateText = resourceManager.GetString("ErrorDisplayControl.Error", TDCultureInfo.CurrentUICulture); 
				labelErrorDisplayType.Text = resourceManager.GetString( "ErrorDisplayControl.Error", TDCultureInfo.CurrentUICulture ); 
				string pleasequote = resourceManager.GetString( "ErrorDisplayControl.PleaseQuoteText", TDCultureInfo.CurrentUICulture );
				string refnumbertext = resourceManager.GetString( "ErrorDisplayControl.JourneyRefNumberText", TDCultureInfo.CurrentUICulture );

				// Removed use of labelJourneyRefNumber. to prevent the control appearing - ref IR2876 Kirsty Gibson (Dft)

			}
			else if (type == ErrorsDisplayType.Custom)
            {
                imageErrorType.ImageUrl = resourceManager.GetString( "ErrorDisplayControl.WarningImageUrl", TDCultureInfo.CurrentUICulture );
                imageErrorType.AlternateText = resourceManager.GetString("ErrorDisplayControl.Warning", TDCultureInfo.CurrentUICulture); 
				labelErrorDisplayType.Text = errorsDisplayTypeText; 
				labelJourneyRefNumber.Visible = false;
            }
            else
			{
				imageErrorType.ImageUrl = resourceManager.GetString( "ErrorDisplayControl.WarningImageUrl", TDCultureInfo.CurrentUICulture );
                imageErrorType.AlternateText = resourceManager.GetString("ErrorDisplayControl.Warning", TDCultureInfo.CurrentUICulture); 
				labelErrorDisplayType.Text = resourceManager.GetString( "ErrorDisplayControl.Warning", TDCultureInfo.CurrentUICulture ); 
				labelJourneyRefNumber.Visible = false;
			}

			// Set invisible outside of if statement so control never appears - ref IR2876 Kirsty Gibson (Dft)
			labelJourneyRefNumber.Visible = false;

			errorsList.DataSource = errorStrings;
			errorsList.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	
	
		public string[] ErrorStrings
		{
			get
			{
				return errorStrings;
			}
			set
			{
				errorStrings = value;
			}
		}

		public string ReferenceNumber
		{
			get
			{
				return referenceNumber;
			}
			set
			{
				referenceNumber = value;
			}

		}

		public ErrorsDisplayType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

        public string ErrorsDisplayTypeText
        {
            get
            {
                return errorsDisplayTypeText;
            }
            set
            {
                errorsDisplayTypeText = value;
            }
        }

	}
}
