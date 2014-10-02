// *********************************************** 
// NAME			: NotesDisplayAdapter.cs
// AUTHOR		: Russell Wilby	
// DATE CREATED	: 18/07/05
// DESCRIPTION	: Responsible for processing Display Notes into the format required by UI
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/NotesDisplayAdapter.cs-arc  $
//
//   Rev 1.5   Mar 21 2013 10:13:02   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.4   Dec 05 2012 13:55:06   mmodi
//Removed line break to improve layout
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Oct 24 2008 13:55:40   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:26   mturner
//Initial revision.
//
//   Rev 1.4   Jun 02 2006 11:09:00   CRees
//Added line break to GetDisplayableNotes method to visually separate notes from preceding sections. 
//Resolution for 4109: No line break between Network Map link and Journey Notes
//
//   Rev 1.3   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:17:42   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 09 2005 16:20:54   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
using System;
using TransportDirect.Common.ResourceManager;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for NotesDisplayAdapter.
	/// </summary>
	public class NotesDisplayAdapter
	{
		public NotesDisplayAdapter()
		{
		}
		
		#region Private constants
		/// <summary>
		/// Property Key for Web.MaxNotesDisplayed
		/// </summary>
		private const string MaxNotesDisplayed = "Web.MaxNotesDisplayed";
		
		private const string NewLine = "\n";
		private const string HTMLLineBreak = "<br />";
		#endregion
        
		/// <summary>
		/// Formats Display Notes ready for UI and imposes max displayable notes restriction 
		/// </summary>
		/// <param name="noteStrings">Notes String Array</param>
		/// <returns>HTML formated Display Notes</returns>
        public string GetDisplayableNotes(Journey journey, JourneyLeg journeyLeg)
        {
            if (journeyLeg != null)
            {
                string[] noteStrings = journeyLeg.GetDisplayNotes();

                if (noteStrings != null && noteStrings.Length > 0)
                {
                    #region Read and set variables

                    //Set MaxNote to int.MaxValue in case Properties table value is blank
                    bool showNote = true;
                    string note = string.Empty;
                    int maxNotes = int.MaxValue;
                    int notesCounter = 0;
                    StringBuilder result = new StringBuilder();

                    if (!string.IsNullOrEmpty(Properties.Current[MaxNotesDisplayed]))
                        maxNotes = Convert.ToInt32(Properties.Current[MaxNotesDisplayed], CultureInfo.InvariantCulture.NumberFormat);

                    // IR 4109 - Added following section to separate notes from any other content displayed in journey result.
                    if (maxNotes > 0)
                    {
                        result.Append(HTMLLineBreak);
                    }
                    // end IR 4109

                    // Display note check variables
                    ICJP.ModeType modeType = journeyLeg.Mode;
                    string region = string.Empty;
                    bool accessibleJourney = false;

                    if (journeyLeg is PublicJourneyDetail)
                    {
                        region = ((PublicJourneyDetail)journeyLeg).Region;
                    }
                    if (journey != null && journey is PublicJourney)
                    {
                        accessibleJourney = ((PublicJourney)journey).AccessibleJourney;
                    }

                    #endregion

                    //For each note
                    for (int i = 0; i < noteStrings.Length && notesCounter < maxNotes; i++)
                    {
                        showNote = true; // Default to show note
                        note = noteStrings[i];

                        // Only check the display note for PJD as the region and accessible flags will 
                        // have been set correctly
                        if (journeyLeg is PublicJourneyDetail)
                        {
                            showNote = JourneyNoteFilter.Current.DisplayNote(modeType, region, accessibleJourney, note);
                        }
                        
                        if (showNote)
                        {
                            //Split note in seperate array at NewLine breaks
                            string[] splitResult = note.Replace(NewLine, "|").Split('|');

                            //For each sub-note in the note
                            for (int j = 0; j < splitResult.Length && notesCounter < maxNotes; j++)
                            {
                                //Append note and HTML line break
                                result.Append(HttpUtility.HtmlEncode(splitResult[j]));
                                result.Append(HTMLLineBreak);
                                notesCounter++;
                            }
                        }
                    }

                    return result.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Finds and formats the Display Notes which shouldn't be displayed 
        /// </summary>
        /// <param name="noteStrings">Notes String Array</param>
        /// <returns>HTML formated Display Notes</returns>
        public string GetNonDisplayableNotes(Journey journey, JourneyLeg journeyLeg)
        {
            if (journeyLeg != null)
            {
                string[] noteStrings = journeyLeg.GetDisplayNotes();

                if (noteStrings != null && noteStrings.Length > 0)
                {
                    #region Read and set variables

                    bool showNote = true;
                    string note = string.Empty;
                    StringBuilder result = new StringBuilder();
                                        
                    // Display note check variables
                    ICJP.ModeType modeType = journeyLeg.Mode;
                    string region = string.Empty;
                    bool accessibleJourney = false;

                    if (journeyLeg is PublicJourneyDetail)
                    {
                        region = ((PublicJourneyDetail)journeyLeg).Region;
                    }
                    if (journey != null && journey is PublicJourney)
                    {
                        accessibleJourney = ((PublicJourney)journey).AccessibleJourney;
                    }

                    #endregion

                    //For each note
                    for (int i = 0; i < noteStrings.Length; i++)
                    {
                        showNote = true; // Default to show note
                        note = noteStrings[i];

                        // Only check the display note for PJD as the region and accessible flags will 
                        // have been set correctly
                        if (journeyLeg is PublicJourneyDetail)
                        {
                            showNote = JourneyNoteFilter.Current.DisplayNote(modeType, region, accessibleJourney, note);
                        }

                        // And only include the "non displayable" notes
                        if (!showNote)
                        {
                            if (i == 0)
                                result.Append(HTMLLineBreak);

                            //Split note in seperate array at NewLine breaks
                            string[] splitResult = note.Replace(NewLine, "|").Split('|');

                            //For each sub-note in the note
                            for (int j = 0; j < splitResult.Length; j++)
                            {
                                //Append note and HTML line break
                                result.Append(HttpUtility.HtmlEncode(splitResult[j]));
                                result.Append(HTMLLineBreak);
                            }
                        }
                    }

                    return result.ToString();
                }
            }

            return string.Empty;
        }
	}
}
