using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace WindowsApplication1
{
	/// <summary>
	/// Summary description for DailySummary.
	/// </summary>
	public class DailySummary : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button UpdateStatsBtn;

		private string myConn1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\kizoom.xls;Extended Properties=""Excel 8.0;HDR=YES""";
		private DateTime weekStart;
		private DateTime maxDate = DateTime.Now.AddDays(-1);
		

		public DailySummary()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.UpdateStatsBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UpdateStatsBtn
			// 
			this.UpdateStatsBtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UpdateStatsBtn.Location = new System.Drawing.Point(32, 24);
			this.UpdateStatsBtn.Name = "UpdateStatsBtn";
			this.UpdateStatsBtn.Size = new System.Drawing.Size(360, 48);
			this.UpdateStatsBtn.TabIndex = 1;
			this.UpdateStatsBtn.Text = "Press To Update Spreadsheet";
			this.UpdateStatsBtn.Click += new System.EventHandler(this.UpdateStatsBtn_Click);
			// 
			// DailySummary
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 109);
			this.Controls.Add(this.UpdateStatsBtn);
			this.Name = "DailySummary";
			this.Text = "DailySummary";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DailySummary());
		}


		static bool GetLine(string Path,string SearchString, out string ReturnString)
		{
			
			// Initialize "myArray"
			int pos = 0;

			
			string currentLine = null;
			ReturnString = null;
			bool itemFound = false;
			using (StreamReader sr = new StreamReader(Path)) 
			{
				while (sr.Peek() >= 0  && !itemFound) 
				{
					currentLine = sr.ReadLine();
					pos = currentLine.IndexOf(SearchString);
					if( pos >= 0)
					{
						itemFound=true;
						ReturnString = currentLine;
					}
				}
				// Close reader
				sr.Close();
				
			}

			return itemFound;
		}
		private void UpdateStatsBtn_Click(object sender, System.EventArgs e)
		{




			#region get date from extract file
			string path = @"c:\DailySummaryExtract.csv";
			string searchString = @"/";
			string returnString =null;

            if (!File.Exists(path))
            {
                MessageBox.Show(string.Format("File does not exist {0}", path));
                return;
            }

			if (GetLine(path,searchString, out returnString))
			{
				weekStart = Convert.ToDateTime(returnString);
			}
			else
			{
				MessageBox.Show(@"Failed to read C:\DailySummaryExtract.csv");
			}
			#endregion

			#region readwebsite
			string [] Pages = new string[7];
			string [] TVPages = new string[7];
			string [] Sessions = new string[7];
			string [] TVSessions = new string[7];
			
			string chosenDateStr = null;
			string tempStr = null;
			string strUrl=null;
			DateTime chosenDate = weekStart;
			WebClient webClient = new WebClient(); 
			string urlContent;
            
			NetworkCredential myCred = new NetworkCredential("transportdirect","Crip5ombow","");
			CredentialCache myCache = new CredentialCache();
			byte[] reqHTML; 
			
			
			for (int dayPtr = 0;dayPtr<7;dayPtr++)
			{
					
				chosenDate = weekStart.AddDays(dayPtr);
				chosenDateStr = string.Format("{0:yyyy/MM/dd}", chosenDate);
				if (chosenDate <= maxDate)		
				{
					try
					{
						strUrl = "http://stats.kizoom.com/transportdirect/"
							+chosenDateStr+"/html/daily_web_stats.html#dir"; 
		
						myCache.Add(new Uri(strUrl), "Basic", myCred);
			 
						WebRequest wr = WebRequest.Create(strUrl);
						wr.Credentials = myCache;
	
						webClient.Credentials = wr.Credentials;
						reqHTML = webClient.DownloadData(strUrl); 
						UTF8Encoding objUTF8 = new UTF8Encoding(); 
						urlContent = objUTF8.GetString(reqHTML);
			
						urlContent = urlContent.Substring(urlContent.IndexOf("%pages: directory"),500);


                        string[] myInitArray = null;
                        string[] mySubArray = null;
                        Pages[dayPtr] = "0";
                        TVPages[dayPtr] = "0";
                        myInitArray = urlContent.Split('\n');
                        for (int i = 2; i < myInitArray.Length-1; i++)
                        {
                            mySubArray = null;
                            mySubArray = myInitArray[i].Split(':');
                            if (mySubArray.Length>2)
                            {
                            if (mySubArray[2].Trim() == "100%")
                            {
                                Pages[dayPtr] = mySubArray[1].Trim();
                            }
                            else if (mySubArray.Length > 4)
                            {
                                if (mySubArray[4].Trim() == "//tdtit.kizoom.co.uk/")
                                {
                                    TVPages[dayPtr] = mySubArray[1].Trim();
                                }
                            }
                            }
                        }

/*
                        string [] myArray = null;
						myArray = urlContent.Split(':');

						Pages[dayPtr] = myArray[5].Trim();

						tempStr = urlContent.Substring(urlContent.IndexOf("http://tdtit.kizoom.co.uk")-30,30);
						myArray = null;
						myArray = tempStr.Split(':');
						TVPages[dayPtr] = myArray[1].Trim();
*/	
						strUrl = "http://stats.kizoom.com/transportdirect/"+chosenDateStr+"/html/daily_sessions.html";

						reqHTML = webClient.DownloadData(strUrl); 
						objUTF8 = new UTF8Encoding(); 
						urlContent = objUTF8.GetString(reqHTML);
			
						tempStr = urlContent.Substring(urlContent.IndexOf("iDTV")+50,20);
						tempStr = tempStr.Substring(0,tempStr.IndexOf("<"));
                        if (tempStr.IndexOf(',') > 0)
                        {
                            tempStr = tempStr.Remove(tempStr.IndexOf(','), 1);
                        }
                        else 
                        {
                            if (tempStr.Length == 0) tempStr = "0";
                        }
                       
                        TVSessions[dayPtr] = tempStr;

						tempStr = urlContent.Substring(urlContent.IndexOf("Total:")+53,10);
						tempStr = tempStr.Substring(0,tempStr.IndexOf("<"));
						if (tempStr.IndexOf(',')>0) 
                        {
                                tempStr = tempStr.Remove(tempStr.IndexOf(','),1);
                        }
                        else
                        {
                            if (tempStr.Length == 0) tempStr = "0";
                        }
                        Sessions[dayPtr]=tempStr;


                      
                    
                    
                    
                    }
					catch (Exception ex)
					{
						MessageBox.Show(strUrl+" cannot be found \n"+ex.Message + "\n" + ex.StackTrace);
					}
				}
				else
				{
					TVPages[dayPtr]="0";
					TVSessions[dayPtr]="0";
					Sessions[dayPtr]="0";
					Pages[dayPtr] ="0";
				}
			}
		
			#endregion



            #region write website values to kizoom spreadsheet
			OleDbConnection conn = new OleDbConnection(myConn1);

			try
			{
				conn.Open();

				OleDbCommand cmd = new OleDbCommand();
				cmd.Connection = conn;

				for (int dayPtr = 0;dayPtr<7;dayPtr++)
				{
					
					chosenDate = weekStart.AddDays(dayPtr);
					
					if (chosenDate <= maxDate)		
					{
                        if(Pages[dayPtr]==null) Pages[dayPtr]="0";
                        if(TVPages[dayPtr]==null) TVPages[dayPtr]="0";
                        if(Sessions[dayPtr]==null) Sessions[dayPtr]="0";
                        if (TVSessions[dayPtr] == null) TVSessions[dayPtr]="0";

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + Convert.ToInt32( Pages[dayPtr] ) + " WHERE DataName = 'Pages'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + Convert.ToInt32( TVPages[dayPtr] ) + " WHERE DataName = 'TVPages'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + Convert.ToInt32( Sessions[dayPtr] ) + " WHERE DataName = 'Sessions'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + Convert.ToInt32( TVSessions[dayPtr] ) + " WHERE DataName = 'TVSessions'";
						cmd.ExecuteNonQuery();


					}
					else
					{
						
						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + 0 + " WHERE DataName = 'Pages'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + 0 + " WHERE DataName = 'TVPages'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + 0 + " WHERE DataName = 'Sessions'";
						cmd.ExecuteNonQuery();

						cmd.CommandText = "UPDATE [kizoom$] SET "+
							chosenDate.DayOfWeek+
							" = " + 0 + " WHERE DataName = 'TVSessions'";
						cmd.ExecuteNonQuery();

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
			}
			finally
			{
				conn.Close();
			}
			#endregion





			UpdateStatsBtn.Text = "Process Complete";
			UpdateStatsBtn.Enabled = false;

		
			Process.Start("excel.exe", "C:\\DailySummaryExtract.csv C:\\DailySummaryReport.xls");

//			Process.Start("excel.exe", "C:\\DailySummaryReport.xls");


		}
	}
}
