// *********************************************** 
// NAME                 : OperatorLinkControl.ascx
// AUTHOR               : Paul Cross
// DATE CREATED         : 18/07/2005
// DESCRIPTION			: Shows a hyperlink to a defined external url for a web page
//						  for the given operator (and mode).
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/OperatorLinkControl.ascx.cs-arc  $
//
//   Rev 1.5   Feb 21 2010 23:23:02   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Oct 17 2008 11:55:24   build
//Automatically merged from branch for stream0093
//
//   Rev 1.3.1.0   Aug 06 2008 10:10:40   apatel
//added text for operator hyper link opening in new window.
//Resolution for 5096: ArrivalBoard and DepartureBoard , and related sites -  labels missing "Opens new window" text
//
//   Rev 1.3   Apr 03 2008 11:03:02   apatel
//Updated PopulateControls method to set region as "air" if regionid is "tt" and transport mode is "air"
//
//   Rev 1.2   Mar 31 2008 13:22:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:46   mturner
//Initial revision.
//
//   Rev 1.10   Apr 03 2007 10:19:40   dsawe
//updated for local zonal services phase 2 & 3
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.9   Apr 02 2007 12:49:18   dsawe
//added code for extra url text
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.8   Mar 16 2007 10:00:50   build
//Automatically merged from branch for stream4362
//
//   Rev 1.7.1.0   Mar 12 2007 16:05:24   dsawe
//added code for zonal operator link 
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.7   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:26:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Aug 25 2005 10:21:14   RWilby
//Fix for IR2676. Updated control to check for null values in OperatorName property.
//Resolution for 2676: DN059 - Viewing details for journey Inverness-Folkestone throws an error
//
//   Rev 1.5   Aug 03 2005 16:32:58   pcross
//Minor change to tooltip
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Jul 25 2005 21:03:58   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jul 21 2005 18:29:32   pcross
//Minor, non-functional
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 20 2005 18:07:08   pcross
//Correction to handling when no url returned
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 19 2005 14:44:16   pcross
//Update to allow compile
//
//   Rev 1.0   Jul 18 2005 16:24:50   pcross
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.JourneyPlanning.CJPInterface;

	[System.Runtime.InteropServices.ComVisible(false)]

	/// <summary>
	///		OperatorLinkControl shows a link (url) to an operator's external website.
	///		The url is gained from the ExternalLinks table for a given operator and mode of travel.
	/// In order of preference, this control will display:
	///		a url
	///		a label describing the operator name
	///		a label describing the travel mode

	/// </summary>
	public partial class OperatorLinkControl : TDPrintableUserControl
	{

		private string operatorCode;
		private string operatorName;
		private ModeType travelMode;
        private string region;
		private string extraUrlText;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set the resource manager
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{
			PopulateControls();
		}

		#region Properties

		public string OperatorCode
		{
			get{return operatorCode;}
			set{operatorCode = value;}
		}

		public string OperatorName
		{
			get{return operatorName;}
			set{operatorName = value;}
		}

		public ModeType TravelMode
		{
			get{return travelMode;}
			set{travelMode = value;}
		}

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

		public string ExtraUrlText
		{
			get{return extraUrlText;}
			set{extraUrlText = value;}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Uses the property values set for the control to lookup the associated url and display.
		/// In order of preference, this control will display:
		///		a url
		///		a label describing the operator name
		///		a label describing the travel mode
		/// </summary>
		private void PopulateControls()
		{

			ServiceOperator objMatchedOperator = new ServiceOperator();
			ServiceOperator objMatchedZonalOperator = new ServiceOperator();
			string matchedOperatorUrl = string.Empty;	// url value returned from Operator object
			string matchedOperatorName = string.Empty;	// name value returned from Operator object

			// Get the URL to display with the help of the OperatorCatalogue class
			// Get OperatorCatalogue instance from service discovery
			IOperatorCatalogue currentOperatorCatalogue = OperatorCatalogue.Current;

			// Get the URL based on the current operator and mode
			objMatchedOperator = currentOperatorCatalogue.GetOperator(operatorCode, travelMode);

            // if travel mode is air and region is "tt" set region as "air"
            if (travelMode == ModeType.Air && region.ToLower().Trim() == "tt")
                region = "air";

			//Get the Zonal Operator URL based on the operator, mode, and region
			objMatchedZonalOperator = currentOperatorCatalogue.GetZonalOperatorLinks(travelMode.ToString(), operatorCode, region);

			// If no match is found then null is returned.
			// Additionally, a match may be found but there may be no URL or operator name present.
			if (objMatchedOperator == null)
			{
				if(objMatchedZonalOperator == null)
				{
					// No match was found. Output text to the label of the operator name (set as a property
					// on this control).
					if ( this.operatorName != null && this.operatorName.Length > 0)
						operatorNameLabel.Text = operatorName + extraUrlText;
					else	// no operator name passed in - use the mode
						operatorNameLabel.Text = travelMode.ToString() + extraUrlText;

					operatorNameLabel.Visible = true;
				}
				else
				{
					// An operator object has been returned. See if it has values.
					String matchedZonalOperatorUrl = objMatchedZonalOperator.Url;
	
					// If we still don't have an operator name (and we really should!) just use text value of travel mode
					if (operatorName == null || operatorName.Length == 0)
						operatorName = travelMode.ToString();
					// If there is a link returned then update the hyperlink and set it's other properties
					if (matchedZonalOperatorUrl.Length > 0)
					{
						// If in printer friendly mode then don't show as hyperlink
						if (!this.PrinterFriendly)
						{
							// Set the hyperlink control properties
							operatorHyperLink.NavigateUrl = matchedZonalOperatorUrl;
                            operatorHyperLink.Text = string.Format("{0} {1}", operatorName + extraUrlText, GetResource("langStrings","ExternalLinks.OpensNewWindowText")); 
							//operatorHyperLink.ToolTip = GetResource("OperatorLinks.Hyperlink.Title");
							operatorHyperLink.Target = "_blank";
							operatorHyperLink.Visible = true;
						}
						else
						{
							// Set the label title text
							operatorNameLabel.Text = operatorName + extraUrlText;
							operatorNameLabel.Visible = true;
						}
					}
					else
					{
						// Set the label title text
						operatorNameLabel.Text = operatorName + extraUrlText;
						operatorNameLabel.Visible = true;
					}
				}
			}
			else
			{
				// An operator object has been returned. See if it has values.
				matchedOperatorUrl = objMatchedOperator.Url;
				matchedOperatorName = objMatchedOperator.Name;

				// If we still don't have an operator name (and we really should!) just use text value of travel mode
                if (operatorName == null || operatorName.Length == 0)
                {
                    // If we don't have operator name check for the matchOperatorName came from operator catalogue
                    if (!string.IsNullOrEmpty(matchedOperatorName))
                    {
                        operatorName = matchedOperatorName;
                    }
                    else //show the mode name if we don't hava operator name at all
                    {
                        operatorName = travelMode.ToString();
                    }
                }

				// If there is a link returned then update the hyperlink and set it's other properties
				if (matchedOperatorUrl.Length > 0)
				{

					// If in printer friendly mode then don't show as hyperlink
					if (!this.PrinterFriendly)
					{
						// Set the hyperlink control properties
						operatorHyperLink.NavigateUrl = matchedOperatorUrl;
                        operatorHyperLink.Text = string.Format("{0} {1}", operatorName + extraUrlText, GetResource("langStrings", "ExternalLinks.OpensNewWindowText")); 
						//operatorHyperLink.ToolTip = GetResource("OperatorLinks.Hyperlink.Title");
						operatorHyperLink.Target = "_blank";
						operatorHyperLink.Visible = true;
					}
					else
					{
						// Set the label title text
						operatorNameLabel.Text = operatorName + extraUrlText;
						operatorNameLabel.Visible = true;
					}

				}
				else
				{
					// Set the label title text
					operatorNameLabel.Text = operatorName + extraUrlText;
					operatorNameLabel.Visible = true;
				}
			}

		}

		#endregion

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

	}
}
