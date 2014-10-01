// *********************************************** 
// NAME             : NotesDisplayAdapter.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: Responsible for processing Display Notes into the format required by UI
// ************************************************
// 

using System.Collections.Generic;
using System.Web;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.UserPortal.JourneyControl;
using System.Text;

namespace TDP.Common.Web
{
    /// <summary>
    /// Responsible for processing Display Notes into the format required by UI
    /// </summary>
    public class NotesDisplayAdapter
    {
        #region Private constants
        /// <summary>
        /// Property Key for Web.MaxNotesDisplayed
        /// </summary>
        private const string MaxNotesDisplayed = "JourneyOptions.NotesDisplayed.MaxNumber";

        private const string NewLine = "\n";
        private const string HTMLLineBreak = "<br />";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NotesDisplayAdapter()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Formats Display Notes ready for UI and imposes max displayable notes restriction 
        /// </summary>
        /// <param name="noteStrings">Notes String Array</param>
        /// <returns>HTML formated Display Notes string array</returns>
        public List<string> GetDisplayableNotes(JourneyDetail journeyLeg, List<string> noteStrings, bool isAccessibleJourney)
        {
            List<string> result = new List<string>();

            if (noteStrings != null && noteStrings.Count > 0)
            {
                #region Read and set variables

                //Set MaxNote to int.MaxValue in case Properties table value is blank
                bool showNote = true;
                string note = string.Empty;
                int maxNotes = int.MaxValue;
                int notesCounter = 0;

                if (!string.IsNullOrEmpty(Properties.Current[MaxNotesDisplayed]))
                    maxNotes = System.Convert.ToInt32(Properties.Current[MaxNotesDisplayed].Parse(20));

                // Display note check variables
               TDPModeType modeType = journeyLeg.Mode;
                string region = string.Empty;

                if (journeyLeg is PublicJourneyDetail)
                {
                    region = ((PublicJourneyDetail)journeyLeg).Region;
                }

                #endregion

                // For each note
                for (int i = 0; i < noteStrings.Count && notesCounter < maxNotes; i++)
                {
                    showNote = true; // Default to show note
                    note = noteStrings[i];

                    // Only check the display note for PJD as the region and accessible flags will 
                    // have been set correctly
                    if (journeyLeg is PublicJourneyDetail)
                    {
                        showNote = JourneyNoteFilter.Current.DisplayNote(modeType, region, isAccessibleJourney, note);
                    }

                    if (showNote)
                    {
                        // Split note in seperate array at NewLine breaks
                        string[] splitResult = noteStrings[i].Replace(NewLine, "|").Split('|');

                        // For each sub-note in the note
                        for (int j = 0; j < splitResult.Length && notesCounter < maxNotes; j++)
                        {
                            //Append note and HTML line break
                            note = HttpUtility.HtmlEncode(splitResult[j]);

                            result.Add(HttpUtility.HtmlDecode(note));

                            notesCounter++;
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}