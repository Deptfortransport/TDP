// *********************************************** 
// NAME                 : UserSurveyRadioMatrix.ascx.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 21/10/2003
// DESCRIPTION			: Custom control to show a question 
//							comprising matrix of radiobuttons
//							on the User Survey Page
// ************************************************ 
// $Log:

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls

{

	/// <summary>
	///		Summary description for UserSurveyRadioMatrix.
	/// </summary>
	public partial  class UserSurveyRadioMatrix : System.Web.UI.UserControl
	{
		private IDataServices ds;
		protected System.Web.UI.WebControls.Label InvalidMarker;
		
		private TDResourceManager rm;

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Databind the control		
			ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];	
			Q9RadioMatrix.DataSource = ds.GetList(DataServiceType.UserSurveyQ9Radio);
			Q9RadioMatrix.DataBind();

			LabelInvalid.Visible = false;
			
		}

		

		#region Properties 
		
		/// <summary>
		/// Get property - returns Header1 for the first radio button in the group
		/// </summary>
		public string GetQ9Statement(DSDropItem dataItem)
		{
			string temp = ds.GetText(DataServiceType.UserSurveyQ9Radio, dataItem.ItemValue, rm );
			return temp;
		}
		
		/// <summary>
		/// Returns the header entry html for the given column
		/// </summary>
		public string HeaderItem(int column)
		{
			string headerItemText;

			headerItemText = rm.GetString("UserSurveyRadioMatrix.HeaderItemText" 
					+ column, TDCultureInfo.CurrentUICulture);			

			return "<div id=\"usrmheader" + column.ToString(TDCultureInfo.CurrentUICulture) + "\">" + headerItemText + "</div>";
		}

		/// <summary>
		/// Allows a different resource manager to be used
		/// </summary>
		public TDResourceManager ResourceManager
		{
			get { return rm; }
			set { rm = value; }
		}


		#endregion

		#region Methods

		/// <summary>
		/// Validate the control by ensuring that one response per element has been clicked.
		/// Returns a bool.
		/// </summary>
		/// <returns></returns>
		public bool ValidateMatrix()
		{
			validated = true;			

			foreach ( RepeaterItem i in Q9RadioMatrix.Items)
			{
				bool isRowValid = false;				

				if ((i.ItemType == ListItemType.Item) || (i.ItemType == ListItemType.AlternatingItem))
				{
					foreach (Control c in i.Controls)
					{
						if (c is RadioButton)
						{
							if (((RadioButton)c).Checked)
							{
								isRowValid = true;
								break;
							}
						}
					}
					if (isRowValid == false )
					{
						validated = false;
						// Highlight row's error by showing asterisk
						i.FindControl("InvalidMarker").Visible = true;
					}
					else
					{
                        // Hide asterisk
						i.FindControl("InvalidMarker").Visible = false;
					}
				}
			}
			if (!validated)
			{
				//set err msg label visibility
				LabelInvalid.Text = rm.GetString("UserSurveyQ9RadioMatrix.LabelInvalid", TDCultureInfo.CurrentUICulture);	
				LabelInvalid.Visible = true;
			}
			else
			{

			}			
			//return overall validation result
			return validated;
		}

		/// <summary>
		/// Get the answers for each statement. Return a string[] of values.
		/// </summary>
		/// <returns></returns>
		public Hashtable GetAnswers()
		{
			char[] trimchars = new char[1];
			trimchars[0] = 'a';
			int count = 1;
			Hashtable answers = new Hashtable();
			string questionNumber = "Q9_";			
			//Check all answers are valid before storing them
			if (validated)
			{
				//Row level loop
				foreach ( RepeaterItem i in Q9RadioMatrix.Items)
				{
					//Check both items and alternating items in the repeater
					if ((i.ItemType == ListItemType.Item) || (i.ItemType == ListItemType.AlternatingItem))
					{
						foreach (Control c in i.Controls)
						{
							if (c is RadioButton)
							{
								if (((RadioButton)c).Checked)
								{
									string answer = string.Empty;

									//Add Answer value to array
									answer = c.ID.TrimStart(trimchars).ToString(TDCultureInfo.CurrentCulture);
									answers.Add(questionNumber + count.ToString(TDCultureInfo.CurrentCulture),answer);
									count++;
								}
							}
						}
					}
				}
				
			}
			return answers;

		}

		/// <summary>
		/// 
		/// </summary>
		private bool validated = true;
		public bool Validated
		{
			get
			{
				return validated;
			}
			set
			{
				validated = value;
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


	}
}
