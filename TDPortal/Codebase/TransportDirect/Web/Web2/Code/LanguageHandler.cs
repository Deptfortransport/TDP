//******************************************************************************
//NAME			: LanguageHandler.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 18/07/2003
//DESCRIPTION	: LanguageHandler class used for non-lookup multi-lingual 
//				functionality 
//
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/LanguageHandler.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:18:50   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 01 2008 09:42:05 sbarker 
//  revision problem with reading parent node when reading resx file for title text
//  changed page.id to pageName, return an empty string. This is OK, since
//  the string is used to look up a value in a resx file.
//
//   Rev 1.0   Nov 08 2007 13:10:56   mturner
//Initial revision.
//
//   Rev 1.26   Feb 23 2006 16:20:12   RWilby
//Merged stream3129
//
//   Rev 1.25   Jan 30 2006 16:39:28   aviitanen
//Use local resource manager of control rather than resource manager of parent.
//Resolution for 3515: Del 8 Welsh Text:  Various welsh text issues
//
//   Rev 1.24   Dec 14 2005 14:41:40   rgreenwood
//Changed SetTextOnControls() method to include HtmlForm control.
//
//   Rev 1.23   May 24 2005 11:45:32   pcross
//Added LocalSetTextOnControls function similar to SetTextOnControls but not recursive. Particularly written for use with FooterControl (but could be used elsewhere).
//Resolution for 2367: DEL 7 Welsh text missing
//
//   Rev 1.22   Mar 22 2005 09:49:12   jgeorge
//FxCop changes
//
//   Rev 1.21   Feb 16 2005 16:39:28   jgeorge
//Updated to skip processing for controls that implement ILanguageHandlerIndependent
//
//   Rev 1.20   Nov 05 2004 15:50:12   rgreenwood
//Added extra validation control types for User Survey
//
//   Rev 1.19   Nov 04 2004 14:23:20   rgreenwood
//Added requiredfieldvalidator and checkboxlistvalidator types
//
//   Rev 1.18   Oct 08 2004 12:26:46   jmorrissey
//SetTextOnControls method now takes additional parameter of TDResourceManager, allowing different resource managers to be used in the project.
//
//   Rev 1.17   Jun 30 2004 15:47:16   CHosegood
//Now returns supported languages.
//Resolution for 1057: Identify Natural language of the document
//
//   Rev 1.16   Nov 26 2003 13:11:04   asinclair
//Added code to switch ToolTips
//
//   Rev 1.15   Oct 22 2003 15:51:40   asinclair
//added check for CheckBoxLists
//
//   Rev 1.14   Oct 21 2003 11:57:12   JMorrissey
//Added support for RadioButtons and RadioButtonLists
//
//   Rev 1.13   Oct 16 2003 17:10:12   JMorrissey
//now updates hyperlink.navigateUrl and image.imageUrl 
//
//   Rev 1.12   Oct 10 2003 13:51:44   passuied
//implemented AlternateText picked up from Resources for images
//
//   Rev 1.11   Oct 10 2003 13:39:20   passuied
//Implemented : Get AlternateText from resources for ImageButtons
//
//   Rev 1.10   Sep 18 2003 10:46:24   JMorrissey
//Removed check for HelpLabelControls as the string retrieval for custom help labels is now handled by the Render event of the HelpLabelControl class 
//
//   Rev 1.9   Sep 17 2003 11:55:58   passuied
//Added support for checkboxes
//
//   Rev 1.8   Sep 04 2003 16:24:08   JMorrissey
//Fixed problem with ImageButton checking in SetTextOnControls method
//
//   Rev 1.7   Sep 04 2003 16:06:06   JMorrissey
//Added check for ImageUrl on Image Buttons in SetTextOnControls
//
//   Rev 1.6   Sep 04 2003 10:16:44   JMorrissey
//Added update hyperlinks code within the HtmlAnchor section of SetTextOnControls
//
//   Rev 1.5   Aug 05 2003 09:34:28   asinclair
//Added a check for HyperLinks
//
//   Rev 1.4   Jul 22 2003 15:45:42   ALole
//Uncommented the DateTimeFormat set in SetThreadLanguageCulture
//
//   Rev 1.3   Jul 21 2003 11:55:36   JMorrissey
//Updated header info to show correct class name!
//
//   Rev 1.2   Jul 18 2003 16:44:04   JMorrissey
//Added check for HtmlForm in method SetTextOnControls
//
//   Rev 1.1   Jul 18 2003 16:21:38   JMorrissey
//Added description for class.
//
//   Rev 1.0   Jul 18 2003 16:02:56   JMorrissey
//Initial Revision

using System.Collections;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// LanguageHandler contains multi-lingual support methods 
	/// that set thread cultures and screen text. 
	/// 
	/// </summary>
	public sealed class LanguageHandler
	{
		/// <summary>
		/// Private constructor to prevent instantiation
		/// </summary>
		private LanguageHandler()
		{
		}

		/// <summary>
		/// Method accepts a cultureName in RFC 1766 format and sets 
		/// thread cultures accordingly
		/// </summary>
		/// <param name="cultureName"></param>
		public static void SetThreadLanguageCulture( string cultureName )
		{
			//new TDCultureIno object
			TDCultureInfo cultureInfo;

			//following is a switch statement in case any more unsupported languages
			//are added in the future
			switch (cultureName) 
			{
				case "cy-GB" :
					cultureInfo = new TDCultureInfo( "en-GB", cultureName, 1106 );
					//Set the Date/Time Text to be welsh with English formatting
					cultureInfo.DateTimeFormat = Global.TDDTIManager.GetDateTimeFormat("cy-GB");
					break;
				
				default:			
					CultureInfo ci = new CultureInfo( cultureName );
					cultureInfo = new TDCultureInfo( ci.Name, ci.Name, ci.LCID );
					break;
			}

			//set culture to use according to the cultureInfo settings			
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}

        /// <summary>
        /// Return a collection of language codes for the supported languages of the portal
        /// </summary>
        public static IList Languages
        {
            get 
            {
                //string languages = "cy-GB,en-GB";
                string languages = Properties.Current["supported.languages"];

                IList list = new ArrayList();

                if ( (languages != null) && ( languages.Length != 0 ) )
				{
                    IEnumerator language = languages.Split( ',' ).GetEnumerator();
                    while ( language.MoveNext() ) 
                    {
                        //cy-GB Welsh
                        //en-GB English
                        list.Add( language.Current );
                    }
                }
                return list;
            }
        }

		/// <summary>
		/// Loops through all controls on a web page and sets control strings 
		/// according to the CurrentUICulture
		/// Checks for any other web and user controls should be added to the main 
		/// loop routine as DEL5 develops
		/// </summary>
		/// <param name="page"></param>
		public static void SetTextOnControls(Control page, TDResourceManager inResourceManager)
		{

			TDResourceManager resourceManager;

			if (page is TDUserControl)
			{
				resourceManager = ((TDUserControl)page).ResourceManager;
			}
			else
			{
				resourceManager = inResourceManager;
			}

			// If the page implements the marker interface ILanguageHandlerIndependent, don't do the
			// normal processing - just recusively call this function for child controls that
			// inherit from System.Web.UI.UserControl (used this rather than TDUserControl to
			// pick up the few cases where people have forgotten to inherit TDUserControl).
			if (page is ILanguageHandlerIndependent)
			{
				foreach (Control c in page.Controls)
				{
					if (((c is UserControl) && c.HasControls()) || (c is HtmlForm))
						SetTextOnControls(c, resourceManager);
				}
			}
			else
			{
				//loop through all controls contained in the page's Controls collection
				foreach (Control c in page.Controls)			
				{
					//temporary variables
                    string pageName = GetCurrentFormName(c);
                    string controlType;
					string resourceName;						
					controlType = c.GetType().Name;
				
					//check for specified control types
					switch ( controlType )
					{
							//check for buttons
						case "Button":						
							Button aButton = (Button) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aButton.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for link buttons
						case "LinkButton" :
							LinkButton linkButton = (LinkButton) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + linkButton.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								linkButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for link buttons
						case "ImageButton" :
							ImageButton imageButton = (ImageButton) c;

							//set the resource name for ImageUrl
							resourceName = pageName + "." + imageButton.ID + ".ImageUrl";

							//update image on image button if necessary						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								imageButton.ImageUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							//set the resource name for AlternateText
							resourceName = pageName + "." + imageButton.ID + ".AlternateText";

							//update alternatetext on image button if necessary						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								imageButton.AlternateText = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;
							//check for image
						case "Image" :
							Image image = (Image) c;
						
							//set the resource name for AlternateText
							resourceName = pageName + "." + image.ID + ".ImageUrl";

							//update image on image button 			
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								image.ImageUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							//set the resource name for AlternateText
							resourceName = pageName + "." + image.ID + ".AlternateText";

							//update alternatetext on image button if necessary						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								image.AlternateText = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
												
							break;

							//check for labels
						case "Label" :
							Label aLabel = (Label) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aLabel.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aLabel.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for checkboxes
						case "CheckBox" :
							CheckBox aCheckBox = (CheckBox) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aCheckBox.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aCheckBox.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for radio buttons
						case "RadioButton" :
							RadioButton aRadioButton = (RadioButton) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aRadioButton.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aRadioButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for radiobuttonlists
						case "RadioButtonList" :
							RadioButtonList aRadioButtonList = (RadioButtonList) c;

							//loop through all items in the radiobuttonlist
							foreach(ListItem li in aRadioButtonList.Items)
							{
								//set the resource name you are looking for						
								resourceName = aRadioButtonList.ID + "." + li.ToString();

								//retrieve text for list item if a resource of that name exists						
								if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
								{
									li.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
								}
							}
						
							break;		
			
							//check for checkboxlists
						case "CheckBoxList" :
							CheckBoxList aCheckBoxList = (CheckBoxList) c;

							//loop through all items in the checkboxlist
							foreach(ListItem li in aCheckBoxList.Items)
							{
								//set the resource name you are looking for						
								resourceName = aCheckBoxList.ID + "." + li.ToString();

								//retrieve text for list item if a resource of that name exists						
								if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
								{
									li.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
								}
							}
						
							break;

							//check for htmlanchors
						case "HtmlAnchor" :
							HtmlAnchor aControl = (HtmlAnchor) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aControl.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aControl.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							//update hyperlinks if necessary
							resourceName = resourceName + ".Href";
							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aControl.HRef = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;

							//check for htmlgenericcontrols
						case "HtmlGenericControl" :
							HtmlGenericControl htmlGenericControl = (HtmlGenericControl) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + htmlGenericControl.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								htmlGenericControl.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;

							//check for htmltablecells
						case "HtmlTableCell" :
							HtmlTableCell htmlTableCell = (HtmlTableCell) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + htmlTableCell.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								htmlTableCell.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;		
				
							//check for HtmlForm
						case "HtmlForm" :
							HtmlForm htmlForm = (HtmlForm) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + htmlForm.ID;

                            //retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								htmlForm.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
							break;	
		
							//check for hyperlink
						case "HyperLink":						
							HyperLink aHyperLink = (HyperLink) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + aHyperLink.ID;

							//retrieve text for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aHyperLink.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
						
							//update ToolTip 
							resourceName = pageName + "." + aHyperLink.ID + ".ToolTip";

							//retrieve ToolTip for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aHyperLink.ToolTip = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							//update hyperlink URL 
							resourceName = pageName + "." + aHyperLink.ID + ".navigateURL";

							//retrieve url for hyperlink if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								aHyperLink.NavigateUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;

						case "RequiredFieldValidator":
							RequiredFieldValidator requiredfieldvalidator = (RequiredFieldValidator) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + requiredfieldvalidator.ID;
						
							//retrieve errormessage for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								requiredfieldvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;

							//Custom control used on the UserSurvey.aspx page
						case "CheckBoxListRequiredFieldValidator":
							CheckBoxListRequiredFieldValidator checkboxlistrequiredfieldvalidator = (CheckBoxListRequiredFieldValidator) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + checkboxlistrequiredfieldvalidator.ID;
						
							//retrieve errormessage for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								checkboxlistrequiredfieldvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;

						case "CompareValidator":
							CompareValidator comparevalidator = (CompareValidator) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + comparevalidator.ID;
						
							//retrieve errormessage for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								comparevalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;

						case "RegularExpressionValidator":
							RegularExpressionValidator regularexpressionvalidator = (RegularExpressionValidator) c;

							//set the resource name you are looking for
							resourceName = pageName + "." + regularexpressionvalidator.ID;
						
							//retrieve errormessage for control if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								regularexpressionvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}

							break;
				
						default :

							//if control not found, we still need to loop through any 
							//child controls contained within control
							if (c.Controls.Count > 0)
							{
								SetTextOnControls(c, resourceManager);
							}
							continue;
						
					}

					//loop through any child controls contained within each control
					if (c.Controls.Count > 0)
					{
						SetTextOnControls(c, resourceManager);
					}
				}
			}
		}

        private static string GetCurrentFormName(Control control)
        {
            //If the page is form, then return the name:
            if (control is HtmlForm)
            {
                return control.ID;
            }

            //If not, check to see if there is a parent:
            if (control.Parent != null)
            {
                //There is a parent, so attempt to extract the 
                //name of that:
                return GetCurrentFormName(control.Parent);
            }

            //The current item is not a form, and has no parent.
            //In this case, return an empty string. This is OK, since
            //the string is used to look up a value in a resx file.
            //If the control does not have a valid name, then there
            //is nothing to look up anyway:
            return "";
        }



		/// <summary>
		/// Loops through all controls on a web page and sets control strings 
		/// according to the CurrentUICulture.
		/// DOES NOT populate subcontrols - there is no recursiveness.
		/// </summary>
		/// </note>
		/// Does similar job to SetTextOnControls BUT is only called locally (ie not from
		/// TDPage base class and subsequent recursive calls).
		/// There is no recursive processing as we only want it to run for the control we
		/// are calling it from.
		/// It does not check the ILanguageHandlerIndependent flag - if it is called, we
		/// want it to run.
		/// Used for footer control as this always has the same (default) resource manager and
		/// therefore can end up having different resource manager to parent page. For this reason
		/// the user control opts out of the SetTextOnControls function but runs this one instead
		/// as we can properly control what resource we want to use instead of having it cascaded from
		/// page definition.
		/// </note>
		/// <param name="page"></param>
		public static void LocalSetTextOnControls(Control page, TDResourceManager resourceManager)
		{

			//loop through all controls contained in the page's Controls collection
			foreach (Control c in page.Controls)			
			{
				//temporary variables
				string controlType;
				string resourceName;						
				controlType = c.GetType().Name;
			
				//check for specified control types
				switch ( controlType )
				{
						//check for buttons
					case "Button":						
						Button aButton = (Button) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aButton.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for link buttons
					case "LinkButton" :
						LinkButton linkButton = (LinkButton) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + linkButton.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							linkButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for link buttons
					case "ImageButton" :
						ImageButton imageButton = (ImageButton) c;

						//set the resource name for ImageUrl
						resourceName = GetCurrentFormName(c) + "." + imageButton.ID + ".ImageUrl";

						//update image on image button if necessary						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							imageButton.ImageUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						//set the resource name for AlternateText
						resourceName = GetCurrentFormName(c) + "." + imageButton.ID + ".AlternateText";

						//update alternatetext on image button if necessary						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							imageButton.AlternateText = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;
						//check for image
					case "Image" :
						Image image = (Image) c;
					
						//set the resource name for AlternateText
						resourceName = GetCurrentFormName(c) + "." + image.ID + ".ImageUrl";

						//update image on image button 			
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							image.ImageUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						//set the resource name for AlternateText
						resourceName = GetCurrentFormName(c) + "." + image.ID + ".AlternateText";

						//update alternatetext on image button if necessary						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							image.AlternateText = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
											
						break;

						//check for labels
					case "Label" :
						Label aLabel = (Label) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aLabel.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aLabel.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for checkboxes
					case "CheckBox" :
						CheckBox aCheckBox = (CheckBox) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aCheckBox.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aCheckBox.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for radio buttons
					case "RadioButton" :
						RadioButton aRadioButton = (RadioButton) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aRadioButton.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aRadioButton.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for radiobuttonlists
					case "RadioButtonList" :
						RadioButtonList aRadioButtonList = (RadioButtonList) c;

						//loop through all items in the radiobuttonlist
						foreach(ListItem li in aRadioButtonList.Items)
						{
							//set the resource name you are looking for						
							resourceName = aRadioButtonList.ID + "." + li.ToString();

							//retrieve text for list item if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								li.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
						}
					
						break;		
		
						//check for checkboxlists
					case "CheckBoxList" :
						CheckBoxList aCheckBoxList = (CheckBoxList) c;

						//loop through all items in the checkboxlist
						foreach(ListItem li in aCheckBoxList.Items)
						{
							//set the resource name you are looking for						
							resourceName = aCheckBoxList.ID + "." + li.ToString();

							//retrieve text for list item if a resource of that name exists						
							if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
							{
								li.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
							}
						}
					
						break;

						//check for htmlanchors
					case "HtmlAnchor" :
						HtmlAnchor aControl = (HtmlAnchor) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aControl.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aControl.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						//update hyperlinks if necessary
						resourceName = resourceName + ".Href";
						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aControl.HRef = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;

						//check for htmlgenericcontrols
					case "HtmlGenericControl" :
						HtmlGenericControl htmlGenericControl = (HtmlGenericControl) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + htmlGenericControl.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							htmlGenericControl.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;

						//check for htmltablecells
					case "HtmlTableCell" :
						HtmlTableCell htmlTableCell = (HtmlTableCell) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + htmlTableCell.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							htmlTableCell.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;		
			
						//check for HtmlForm
					case "HtmlForm" :
						HtmlForm htmlForm = (HtmlForm) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + htmlForm.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							htmlForm.InnerHtml = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
						break;	
	
						//check for hyperlink
					case "HyperLink":						
						HyperLink aHyperLink = (HyperLink) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + aHyperLink.ID;

						//retrieve text for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aHyperLink.Text = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}
					
						//update ToolTip 
						resourceName = GetCurrentFormName(c) + "." + aHyperLink.ID + ".ToolTip";

						//retrieve ToolTip for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aHyperLink.ToolTip = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						//update hyperlink URL 
						resourceName = GetCurrentFormName(c) + "." + aHyperLink.ID + ".navigateURL";

						//retrieve url for hyperlink if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							aHyperLink.NavigateUrl = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;

					case "RequiredFieldValidator":
						RequiredFieldValidator requiredfieldvalidator = (RequiredFieldValidator) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + requiredfieldvalidator.ID;
					
						//retrieve errormessage for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							requiredfieldvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;

						//Custom control used on the UserSurvey.aspx page
					case "CheckBoxListRequiredFieldValidator":
						CheckBoxListRequiredFieldValidator checkboxlistrequiredfieldvalidator = (CheckBoxListRequiredFieldValidator) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + checkboxlistrequiredfieldvalidator.ID;
					
						//retrieve errormessage for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							checkboxlistrequiredfieldvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;

					case "CompareValidator":
						CompareValidator comparevalidator = (CompareValidator) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + comparevalidator.ID;
					
						//retrieve errormessage for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							comparevalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;

					case "RegularExpressionValidator":
						RegularExpressionValidator regularexpressionvalidator = (RegularExpressionValidator) c;

						//set the resource name you are looking for
						resourceName = GetCurrentFormName(c) + "." + regularexpressionvalidator.ID;
					
						//retrieve errormessage for control if a resource of that name exists						
						if (resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture) != null)
						{
							regularexpressionvalidator.ErrorMessage = resourceManager.GetString(resourceName,TDCultureInfo.CurrentUICulture);
						}

						break;
			
					default :

						//control not found - continue to next control
						continue;
					
				}
			}
		}

	}
}
