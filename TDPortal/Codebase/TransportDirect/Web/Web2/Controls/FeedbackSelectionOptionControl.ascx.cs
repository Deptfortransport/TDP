// *********************************************** 
// NAME                 : FeedbackSelectionControl.aspx.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 15/09/2003 
// DESCRIPTION          : Control to allow user to
//                        submit feedback type on the TDPortal  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackSelectionOptionControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jan 16 2009 13:27:26   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:52   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:24:24   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Sep 16 2003 10:14:26   hahad
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Summary description for FeedbackSelectionOptionControl.
	/// </summary>
	public partial  class FeedbackSelectionOptionControl : TDUserControl
	{

		HtmlGenericControl [] lbl = new HtmlGenericControl[5];
		RadioButton [] rb = new RadioButton[5];
		TableRow [] tr = new TableRow[5];
		TableCell [] tc = new TableCell[5];
		
		string [] commentsSuggestion = new string[5];

		HtmlGenericControl [] lbl2 = new HtmlGenericControl[3];
		RadioButton [] rb2 = new RadioButton[3];
		TableRow [] tr2 = new TableRow[3];
		TableCell [] tc2 = new TableCell[3];

		string [] feedbackQuestion = new string[3];

		HtmlGenericControl [] lbl3 = new HtmlGenericControl[6];
		RadioButton [] rb3 = new RadioButton[6];
		TableRow [] tr3 = new TableRow[6];
		TableCell [] tc3 = new TableCell[6];

		string [] problemSelection = new string[6];

		HtmlGenericControl [] lbl4 = new HtmlGenericControl[6];
		RadioButton [] rb4 = new RadioButton[6];
		TableRow [] tr4 = new TableRow[6];
		TableCell [] tc4 = new TableCell[6];

		string [] errorSelection = new string[6];

		HtmlGenericControl [] lbl5 = new HtmlGenericControl[1];
		RadioButton [] rb5 = new RadioButton[1];
		TableRow [] tr5 = new TableRow[1];
		TableCell [] tc5 = new TableCell[1];

		string [] technicalSelection = new string[1];




		protected void Page_Load(object sender, System.EventArgs e)
		{

			commentsSuggestion[0] = "Transport Direct";
			commentsSuggestion[1] = "The Journey Planning Pages";
			commentsSuggestion[2] = "The Map Pages";
			commentsSuggestion[3] = "The Live Travel Pages";
			commentsSuggestion[4] = "Something else not in the list above";

			feedbackQuestion[0] = "That is not answered in the FAQs";
			feedbackQuestion[1] = "about the Transport Direct Service";
			feedbackQuestion[2] = "about something else not in the list above";

			problemSelection[0] = "using the journey planning pages";
			problemSelection[1] = "using the map pages";
			problemSelection[2] = "using the live travel pages";
			problemSelection[3] = "getting around the website";
			problemSelection[4] = "understand the information presented";
			problemSelection[5] = "Something else not in the list above";

			errorSelection[0] = "Buses";
			errorSelection[1] = "Coaches";
			errorSelection[2] = "Rail";
			errorSelection[3] = "Planes";
			errorSelection[4] = "Driving";
			errorSelection[5] = "Something else not in the list above";

			technicalSelection[0] = "I would like to report a technical fault in the website(not your environment)";


			for( int i =0; i <5; i++)
			{
				
				lbl[i] = new HtmlGenericControl();
				lbl[i].TagName = "label" + i;
				lbl[i].InnerText = commentsSuggestion[i];
				lbl[i].Attributes.Add("for", "rbid" + i);
				rb[i] = new RadioButton();
				rb[i].ID = "rbid" + i;
				rb[i].GroupName = "Comments_Suggestion";
				tr[i] = new TableRow();
				tc[i] = new TableCell();
				tc[i].HorizontalAlign.Equals("left");
				tc[i].Controls.Add(lbl[i]);
				tr[i].Cells.Add(tc[i]);
				tc[i] = new TableCell();
				tc[i].HorizontalAlign.Equals("right");
				tc[i].Controls.Add(rb[i]);
				tr[i].Cells.Add(tc[i]);
				TableCommentSuggestion.Rows.Add(tr[i]);
			}

			for( int i =0; i <3; i++)
			{
				
				lbl2[i] = new HtmlGenericControl();
				lbl2[i].TagName = "label2" + i;
				lbl2[i].InnerText = feedbackQuestion[i];
				lbl2[i].Attributes.Add("for", "rbid2" + i);
				rb2[i] = new RadioButton();
				rb2[i].ID = "rbid2" + i;
				rb2[i].GroupName = "Comments_Suggestion";
				tr2[i] = new TableRow();
				tc2[i] = new TableCell();
				tc2[i].Controls.Add(lbl2[i]);
				tr2[i].Cells.Add(tc2[i]);
				tc2[i] = new TableCell();
				tc2[i].Controls.Add(rb2[i]);
				tr2[i].Cells.Add(tc2[i]);
				TableFeedbackQuestion.Rows.Add(tr2[i]);
			}

			for( int i =0; i <6; i++)
			{
				
				lbl3[i] = new HtmlGenericControl();
				lbl3[i].TagName = "label3" + i;
				lbl3[i].InnerText = problemSelection[i];
				lbl3[i].Attributes.Add("for", "rbid3" + i);
				rb3[i] = new RadioButton();
				rb3[i].ID = "rbid3" + i;
				rb3[i].GroupName = "Comments_Suggestion";
				tr3[i] = new TableRow();
				tc3[i] = new TableCell();
				tc3[i].Controls.Add(lbl3[i]);
				tr3[i].Cells.Add(tc3[i]);
				tc3[i] = new TableCell();
				tc3[i].Controls.Add(rb3[i]);
				tr3[i].Cells.Add(tc3[i]);
				TableProblemSelection.Rows.Add(tr3[i]);
			}

			for( int i =0; i <6; i++)
			{
				
				lbl4[i] = new HtmlGenericControl();
				lbl4[i].TagName = "label4" + i;
				lbl4[i].InnerText = errorSelection[i];
				lbl4[i].Attributes.Add("for", "rbid4" + i);
				rb4[i] = new RadioButton();
				rb4[i].ID = "rbid4" + i;
				rb4[i].GroupName = "Comments_Suggestion";
				tr4[i] = new TableRow();
				tc4[i] = new TableCell();
				tc4[i].Controls.Add(lbl4[i]);
				tr4[i].Cells.Add(tc4[i]);
				tc4[i] = new TableCell();
				tc4[i].Controls.Add(rb4[i]);
				tr4[i].Cells.Add(tc4[i]);
				TableErrorSelection.Rows.Add(tr4[i]);
			}

			for( int i =0; i <1; i++)
			{
				
				lbl5[i] = new HtmlGenericControl();
				lbl5[i].TagName = "label5" + i;
				lbl5[i].InnerText = technicalSelection[i];
				lbl5[i].Attributes.Add("for", "rbid5" + i);
				rb5[i] = new RadioButton();
				rb5[i].ID = "rbid5" + i;
				rb5[i].GroupName = "Comments_Suggestion";
				tr5[i] = new TableRow();
				tc5[i] = new TableCell();
				tc5[i].Controls.Add(lbl5[i]);
				tr5[i].Cells.Add(tc5[i]);
				tc5[i] = new TableCell();
				tc5[i].Controls.Add(rb5[i]);
				tr5[i].Cells.Add(tc5[i]);
				TableTechnicalSelection.Rows.Add(tr5[i]);
			}

			// Put user code to initialize the page here
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
