// *********************************************** 
// NAME                 : MapKeyControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 21/08/2003 
// DESCRIPTION			: A custom user control to
// display a key for a map.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapKeyControl.ascx.cs-arc  $
//
//   Rev 1.6   Jul 28 2011 16:19:18   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.5   Nov 09 2009 15:47:18   mmodi
//Updated for use in new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Oct 12 2009 09:11:46   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Oct 12 2009 08:40:06   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 13 2008 16:44:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Oct 07 2008 10:12:12   mmodi
//Updated for cycle journeys
//Resolution for 5122: Cycle Planner - "Server Error" is displayed when user clicks on 'Priner Friendly' button on 'Find on Map' page
//
//   Rev 1.2   Mar 31 2008 13:22:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:24   mturner
//Initial revision.
//
//   Rev 1.29   Feb 23 2006 16:12:42   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.28   Nov 28 2005 15:21:16   jbroome
//Added new intialise method for Visit plan on Map key control
//Resolution for 3222: Visit Planner: Purple triangles should be in the map key for stopover locations
//
//   Rev 1.27   Nov 10 2005 14:03:08   tolomolaiye
//Ensure that only pulic mode icons are shown for Visit Planner as it does not cater for car (private) journeys
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.26   Aug 19 2005 14:08:36   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.25.1.1   Aug 04 2005 16:45:26   rgreenwood
//DD073 Map Details: Added comment for code added in previous check-in
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.25.1.0   Jul 27 2005 16:25:04   rgreenwood
//DD073 Map Details: removed InitialisePrivate() overload method, as not necessary.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.25   Jul 26 2005 16:10:04   rgreenwood
//DD073 Map Details: Added overloaded methods for InitialisePublic(), InitialisePrivate(), InitialiseSpecific() and InitialiseMixed. Added greyedOutMode and new KeyImagePair for greyedOutMode
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.24   Apr 22 2005 16:13:08   pcross
//IR2192. New key for public transport denoting a journey change.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.23   Mar 18 2005 14:18:52   asinclair
//Updated for DEL 7 - added toll and ferry icons
//
//   Rev 1.22   Oct 08 2004 11:13:18   esevern
//added check for existing rail replacement or rail mode type before getting map key item: IR1661
//
//   Rev 1.21   Sep 29 2004 15:13:30   esevern
//added check for ModeType of RailReplacementBus - this should display Rail in key
//
//   Rev 1.20   Sep 17 2004 15:13:58   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.19   Sep 09 2004 11:22:50   jgeorge
//Added code to ensure that key for "Underground/Metro" is displayed when an underground journey is returned from Find a...  
//Resolution for 1542: Underground key not shown on trunk map
//
//   Rev 1.18   Jul 12 2004 19:54:44   JHaydock
//DEL 5.4.7 Merge: IR 1132
//
//   Rev 1.17   Jun 08 2004 14:36:56   RPhilpott
//Add further support for only displaying key for specific modes. 
//
//   Rev 1.16   Jun 04 2004 16:55:30   RPhilpott
//Allow initialisation for specified modes, to support display of single mode for "Find A" results. 
//
//   Rev 1.15   Jun 02 2004 11:22:52   jbroome
//Extend Journey.
//Added initialisation for mixed (public and road) journies.
//
//   Rev 1.14   Apr 06 2004 17:19:04   CHosegood
//Changed so doesn't fall over if MapKeyControl.MapKeyControlItems.WhatEver does not exist.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.13   Apr 06 2004 12:54:12   CHosegood
//Del 5.2 Map QA changes.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.12   Apr 05 2004 17:13:12   CHosegood
//Del 5.2 map QA fixes
//
//   Rev 1.11   Dec 01 2003 12:51:12   kcheung
//Added commenting.
//
//   Rev 1.10   Dec 01 2003 12:07:36   kcheung
//Added label.
//Resolution for 459: Adjusted Route Label
//
//   Rev 1.9   Nov 28 2003 10:34:40   kcheung
//Removed Adjusted Route key
//Resolution for 433: Congestion Map Key shows key for adjusted route
//
//   Rev 1.8   Nov 05 2003 14:18:18   kcheung
//Fixed commenting
//
//   Rev 1.7   Oct 23 2003 13:57:10   passuied
//added images
//
//   Rev 1.6   Oct 22 2003 11:23:38   kcheung
//Fixed variable naming for FXCOP
//
//   Rev 1.5   Oct 20 2003 11:52:56   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.4   Oct 15 2003 09:50:18   ALole
//Added Initialise method to allow the route info keys to be switched off.
//
//   Rev 1.3   Oct 13 2003 12:37:54   kcheung
//Fixed ALT text
//
//   Rev 1.2   Sep 15 2003 16:08:18   kcheung
//Updated
//
//   Rev 1.1   Aug 22 2003 13:38:52   kcheung
//Working version
//
//   Rev 1.0   Aug 22 2003 10:26:24   kcheung
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	A custom user control to display a key for a map.
	/// </summary>
	public partial  class MapKeyControl : TDUserControl
    {
        #region Private members

        private bool trafficMapVisible = false;
		private bool isPrintable = false;
		private bool publicJourney = false;
		private bool privateJourney = false;
		private bool mixedJourney = false;
		private bool visitPlan = false;
        private bool cycleJourney = false;
        private bool ebcJourney = false;
		private ModeType[] specificModes = null;
		private bool greyedOutMode = false;
		private const string greyedOutKey = "GreyedOut";

        #endregion

        #region Initialise methods

        /// <summary>
		/// Initialise Method for public journeys.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		public void InitialisePublic( bool isPrintable )
		{
			this.publicJourney = true;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
		}
		
		/// <summary>
		/// Overloaded initialise Method for public journeys.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		/// </param name="greyedOutMode">Indicates whether any co-ordinates 
		/// in the leg are invalid, and that the map should use the greyed out style. False by default.</param>
		public void InitialisePublic( bool isPrintable, bool greyedOutMode)
		{
			this.publicJourney = true;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
			this.greyedOutMode = greyedOutMode;

		}

		/// <summary>
		/// Initialise Method for private journeys
		/// </summary>
		/// <param name="trafficMap">Indiciate if the control
		/// is being rendered for the traffic map. False by default.</param>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		public void InitialisePrivate( bool trafficMap , bool isPrintable) 
		{
			// true if used on TrafficMap.aspx
			// false otherwise
			this.publicJourney = false;
			this.privateJourney = true;
			this.mixedJourney = false;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = trafficMap;
			this.isPrintable = isPrintable;
			// GreyedOutMode cannot occur for a private (car) journey
			this.greyedOutMode = false;
		}

		/// <summary>
		/// Initialise Method for mixed (both Public and Road) journeys.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		public void InitialiseMixed( bool isPrintable)
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.mixedJourney = true;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
		}

		/// <summary>
		/// Overloaded initialise Method for mixed (both Public and Road) journeys.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		/// </param name="greyedOutMode">Indicates whether any co-ordinates 
		/// in the leg are invalid, and that the map should use the greyed out style. False by default.</param>
		public void InitialiseMixed( bool isPrintable, bool greyedOutMode )
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.visitPlan = false;
			this.mixedJourney = true;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
			this.greyedOutMode = greyedOutMode;
		}

		/// <summary>
		/// Initialise Method for Visit Planner results.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		public void InitialiseVisitPlan( bool isPrintable)
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = true;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
		}

		/// <summary>
		/// Initialise Method for Visit Planner results.
		/// </summary>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		/// </param name="greyedOutMode">Indicates whether any co-ordinates 
		/// in the leg are invalid, and that the map should use the greyed out style. False by default.</param>
		public void InitialiseVisitPlan( bool isPrintable, bool greyedOutMode )
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = true;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
			this.greyedOutMode = greyedOutMode;
		}

        /// <summary>
        /// Initialise Method for cycle journeys
        /// </summary>
        /// <param name="isPrintable">Indicates if the control is
        /// being rendered for a printer friendly page. False by default.</param>
        public void InitialiseCycle(bool isPrintable)
        {
            // true if used on TrafficMap.aspx
            // false otherwise
            this.publicJourney = false;
            this.privateJourney = false;
            this.mixedJourney = false;
            this.visitPlan = false;
            this.cycleJourney = true;
            this.trafficMapVisible = false;
            this.isPrintable = isPrintable;
            this.greyedOutMode = false;
        }

        /// <summary>
        /// Initialise Method for Environment benefits calculator
        /// </summary>
        /// <param name="isPrintable">Indicates if the control is
        /// being rendered for a printer friendly page. False by default.</param>
        public void InitialiseEBC(bool isPrintable)
        {
            // true if used on TrafficMap.aspx
            // false otherwise
            this.publicJourney = false;
            this.privateJourney = false;
            this.mixedJourney = false;
            this.visitPlan = false;
            this.cycleJourney = false;
            this.trafficMapVisible = false;
            this.isPrintable = isPrintable;
            this.greyedOutMode = false;
            this.ebcJourney = true;
        }

		/// <summary>
		/// Initialise Method for specific modes - expected to be used 
		/// by the "Find A" results maps which display a single mode.
		/// </summary>
		/// <param name="specificModes">Array of strings specifying 
		/// list of modes to be displayed.</param>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		public void InitialiseSpecificModes(ModeType[] specificModes, bool isPrintable)
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;

			// If the specificModes array passed contains Underground, we need to replace it with Metro
			// (if not already present)
			bool metroUsed = false;
			ArrayList specificModesTemp = new ArrayList(specificModes.Length);
			foreach (ModeType mode in specificModes)
			{
				if ( (mode == ModeType.Metro) || (mode == ModeType.Underground) )
				{
					// Add Metro to the list if not already present
					if (!metroUsed)
					{
						specificModesTemp.Add(ModeType.Metro);
						metroUsed = true;
					}
				}
				else
				{
					// Add the item
					specificModesTemp.Add(mode);
				}
			}
			this.specificModes = (ModeType[])specificModesTemp.ToArray(typeof(ModeType));
		}

		/// <summary>
		/// Overloaded initialise Method for specific modes - expected to be used 
		/// by the "Find A" results maps which display a single mode.
		/// </summary>
		/// <param name="specificModes">Array of strings specifying 
		/// list of modes to be displayed.</param>
		/// <param name="isPrintable">Indicates if the control is
		/// being rendered for a printer friendly page. False by default.</param>
		/// </param name="greyedOutMode">Indicates whether any co-ordinates 
		/// in the leg are invalid, and that the map should use the greyed out style. False by default.</param>
		public void InitialiseSpecificModes(ModeType[] specificModes, bool isPrintable, bool greyedOutMode)
		{
			this.publicJourney = false;
			this.privateJourney = false;
			this.mixedJourney = false;
			this.visitPlan = false;
            this.cycleJourney = false;
			this.trafficMapVisible = false;
			this.isPrintable = isPrintable;
			// DD073 Map details
			this.greyedOutMode = greyedOutMode;

			// If the specificModes array passed contains Underground, we need to replace it with Metro
			// (if not already present)
			bool metroUsed = false;
			ArrayList specificModesTemp = new ArrayList(specificModes.Length);
			foreach (ModeType mode in specificModes)
			{
				if ( (mode == ModeType.Metro) || (mode == ModeType.Underground) )
				{
					// Add Metro to the list if not already present
					if (!metroUsed)
					{
						specificModesTemp.Add(ModeType.Metro);
						metroUsed = true;
					}
				}
				else
				{
					// Add the item
					specificModesTemp.Add(mode);
				}
			}
			this.specificModes = (ModeType[])specificModesTemp.ToArray(typeof(ModeType));
        }

        #endregion

        #region Page_Load, PreRender

        /// <Summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// OnPreRender method - refreshes controls and calls the
		/// base OnPreRender.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
            LoadResources();

			// Load strings for keys/images from resourcing manager.
			ArrayList data = new ArrayList();

			//Get the keys for the particular type of map
			string[] mapKeyControlItems = null;

			if ( publicJourney ) 
			{
				string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.Public", TDCultureInfo.CurrentUICulture);
				if ( items != null )
				{
					mapKeyControlItems = items.Split(',');
				}
			}
			else if (privateJourney)
			{
				string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.Private" +(trafficMapVisible? ".TrafficMap": string.Empty), TDCultureInfo.CurrentUICulture);

				if ( items != null )
				{
					mapKeyControlItems = items.Split(',');
				}
			}
			else if (mixedJourney)
			{
				string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.Mixed", TDCultureInfo.CurrentUICulture);
				if ( items != null )
				{
					mapKeyControlItems = items.Split(',');
				}
			}
			else if (visitPlan)
			{
				string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.VisitPlan", TDCultureInfo.CurrentUICulture);
				if ( items != null )
				{
					mapKeyControlItems = items.Split(',');
				}
			}
            else if (cycleJourney)
            {
                string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.Cycle", TDCultureInfo.CurrentUICulture);
                if (items != null)
                {
                    mapKeyControlItems = items.Split(',');
                }
            }
            else if (ebcJourney) //environment benefits calculator journey
            {
                string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.EBC", TDCultureInfo.CurrentUICulture);
                if (items != null)
                {
                    mapKeyControlItems = items.Split(',');
                }
            }
			else if	(specificModes != null && specificModes.Length > 0) 
			{
				ArrayList modeItems = new ArrayList(specificModes.Length);
				bool railExists = false;
				bool replacementExists = false;
	
				foreach (ModeType mode in specificModes)
				{
					string temp = string.Empty;

					// added checks for rail and rail replacement.  If both exist in this journey
					// (ie. there is more than one leg), then only show one rail map key
					if(mode == ModeType.RailReplacementBus) 
					{
						// check that there isn't a rail mode (ie from a 2 leg journey, part rail, part replacement)
						replacementExists = true;
						if(!railExists)
						{
							temp = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.SpecificModes.Rail", TDCultureInfo.CurrentUICulture);
						}
					}
					else 
					{
						if(mode == ModeType.Rail) 
						{
							railExists = true;
						}
						if(!replacementExists) 
						{
							temp = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.SpecificModes." + mode.ToString(), TDCultureInfo.CurrentUICulture);
						}
					}

					if	(temp != null && temp.Length > 0)
					{
						modeItems.Add(temp); 
					}
				}
				
				string items = Global.tdResourceManager.GetString("MapKeyControl.MapKeyControlItems.SpecificModes.Locations", TDCultureInfo.CurrentUICulture);

				string[] locationItems = new string[0]; 
				
				if  (items != null)
				{
					locationItems = items.Split(',');
				}

				mapKeyControlItems = new string[modeItems.Count + locationItems.Length];

				int i = 0;

				// first add mode items to array ...
				for	(i = 0; i < modeItems.Count; i++) 
				{
					mapKeyControlItems[i] = (string) modeItems[i];
				}

				// ... then add location types to end of same array 
				for (int j = 0; j < locationItems.Length; j++, i++)
				{
					mapKeyControlItems[i] = locationItems[j];
				}
			}

			if  (mapKeyControlItems != null && mapKeyControlItems.Length > 0) 
			{
				//For each key returned from langstrings, add it to the repeater datasource list
				foreach (string key in mapKeyControlItems ) 
				{
                    // Remove alt text for traffic maps
                    if (privateJourney && trafficMapVisible)
                    {
                        data.Add(new KeyImagePair(
                            Global.tdResourceManager.GetString("MapKeyControl.label" + key + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                            Global.tdResourceManager.GetString("MapKeyControl.image" + key + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                            " "
                            ));
                    }
                    else
                    {
                        data.Add(new KeyImagePair(
                            Global.tdResourceManager.GetString("MapKeyControl.label" + key + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                            Global.tdResourceManager.GetString("MapKeyControl.image" + key + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                            Global.tdResourceManager.GetString("MapKeyControl.image" + key + ".AlternateText", TDCultureInfo.CurrentUICulture)
                            ));
                    }
					
				}
			}

			//DD073 Map Details: Show an "Unknown" key for greyed out legs
			if (greyedOutMode)
			{
                    // Remove alt text for traffic maps
                if (privateJourney && trafficMapVisible)
                {
                    data.Add(new KeyImagePair(
                        Global.tdResourceManager.GetString("MapKeyControl.label" + greyedOutKey + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                        Global.tdResourceManager.GetString("MapKeyControl.image" + greyedOutKey + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                        " "
                        ));
                }
                else
                {
                    data.Add(new KeyImagePair(
                        Global.tdResourceManager.GetString("MapKeyControl.label" + greyedOutKey + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                        Global.tdResourceManager.GetString("MapKeyControl.image" + greyedOutKey + (isPrintable ? ".Print" : string.Empty), TDCultureInfo.CurrentUICulture),
                        Global.tdResourceManager.GetString("MapKeyControl.image" + greyedOutKey + ".AlternateText", TDCultureInfo.CurrentUICulture)
                        ));
                }
			}

			keyRepeater.DataSource = data;
			keyRepeater.DataBind();

        }

        #endregion

        #region Private methods

        /// <summary>
		/// Returns the strong tag if index is less than 4,
		/// otherwise returns an empty string.
		/// </summary>
		/// <param name="index">Current index being rendered.</param>
		/// <returns>Strong string or empty string.</returns>
		protected string StrongTagOpen(int index)
		{
			string returnString = string.Empty;
			if (privateJourney) 
			{
				if (index >= 4 && index < 6)
					returnString = "<strong>";
			}
			else if (publicJourney)
			{
				if ( index >= 9 && index < 11)
					returnString = "<strong>";
			}
			return returnString;
		}

		/// <summary>
		/// Returns the strong close tag if index is less than 4,
		/// otherwise returns an empty string.
		/// </summary>
		/// <param name="index">Current index being rendered.</param>
		/// <returns>Strong string or empty string.</returns>
		protected string StrongTagClose(int index)
		{
			string returnString = string.Empty;
			if (privateJourney) 
			{
				if (index >= 4 && index < 6)
					returnString = "</strong>";
			}
			else if (publicJourney)
			{
				if ( index >= 9  && index < 11)
					returnString = "</strong>";
			}

			return returnString;
        }

        /// <summary>
        /// Loads any text labels on the page
        /// </summary>
        private void LoadResources()
        {
            labelKey.Text = Global.tdResourceManager.GetString("MapKeyControl.labelKey", TDCultureInfo.CurrentUICulture);
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}

	/// <summary>
	/// Wrapper class for repeater to generate table with.
	/// </summary>
	public class KeyImagePair
	{
		string key = String.Empty;
		string imageUrl = String.Empty;
		string alternateText = String.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="key">Key string text.</param>
		/// <param name="imageUrl">Url to the image.</param>
		/// <param name="alternateText">Alternate text for the key image.</param>
		public KeyImagePair(string key, string imageUrl, string alternateText)
		{
			this.key = key;
			this.imageUrl = imageUrl;
			this.alternateText = alternateText;
		}

		/// <summary>
		/// Get property - Returns the key name.
		/// </summary>
		public string Key
		{
			get
			{
				return key;
			}
		}

		/// <summary>
		/// Get property - Returns the key image url
		/// </summary>
		public string ImageUrl
		{
			get 
			{
				return imageUrl;
			}
		}

		/// <summary>
		/// Get property - returns the alternate text.
		/// </summary>
		public string AlternateText
		{
			get
			{
				return alternateText;
			}
		}
	}
}