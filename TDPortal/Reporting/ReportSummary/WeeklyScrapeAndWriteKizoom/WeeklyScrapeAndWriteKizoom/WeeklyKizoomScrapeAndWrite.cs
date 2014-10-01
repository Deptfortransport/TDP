using System;
using System.Collections.Generic;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WeeklyKizoomScrapeAndWrite
{
    class WeeklyKizoomScrapeAndWrite
    {
        
        static void Main(string[] args)
        {

            string myConn1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\kizoom.xls;Extended Properties=""Excel 8.0;HDR=YES""";

            DateTime weekStart = DateTime.Now;
            DateTime maxDate = DateTime.Now.AddDays(-1);

            #region get date from extract file
            string path = @"D:\FTPTemp\DailySummaryExtract.csv";
            string searchString = @"/";
            string returnString = null;

            if (GetLine(path, searchString, out returnString))
            {
                weekStart = Convert.ToDateTime(returnString);
            }
            else
            {
                Console.WriteLine(@"Failed to read D:\FTPTemp\DailySummaryExtract.csv");
            }
            #endregion

            #region readwebsite
            string[] Pages = new string[7];
            string[] TVPages = new string[7];
            string[] Sessions = new string[7];
            string[] TVSessions = new string[7];
            string[] tempTot = new string[7];
            string[] cellDescription = new string[5];

            string chosenDateStr = null;
            string tempStr = null;
            string strUrl = null;
            DateTime chosenDate = weekStart;
            WebClient webClient = new WebClient();
            string urlContent;

            NetworkCredential myCred = new NetworkCredential("transportdirect", "Crip5ombow", "");
            CredentialCache myCache = new CredentialCache();
            byte[] reqHTML;

         
            for (int dayPtr = 0; dayPtr < 7; dayPtr++)
            {

                chosenDate = weekStart.AddDays(dayPtr);
                chosenDateStr = string.Format("{0:yyyy/MM/dd}", chosenDate);
                if (chosenDate <= maxDate)
                {
                    try
                    {
                        strUrl = "http://stats.kizoom.com/transportdirect/"
                            + chosenDateStr + "/html/daily_web_stats.html#dir";

                        myCache.Add(new Uri(strUrl), "Basic", myCred);

                        WebRequest wr = WebRequest.Create(strUrl);
                        wr.Credentials = myCache;

                        webClient.Credentials = wr.Credentials;
                        reqHTML = webClient.DownloadData(strUrl);
                        UTF8Encoding objUTF8 = new UTF8Encoding();
                        urlContent = objUTF8.GetString(reqHTML);

                        urlContent = urlContent.Substring(urlContent.IndexOf("%pages: directory"), 1000);


                        string[] myInitArray = null;
                        string[] mySubArray = null;
                        Pages[dayPtr] = "0";
                        TVPages[dayPtr] = "0";
                        myInitArray = urlContent.Split('\n');
                        for (int i = 2; i < myInitArray.Length - 1; i++)
                        {
                            mySubArray = null;
                            mySubArray = myInitArray[i].Split(':');
                            if (mySubArray.Length > 2)
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
                        strUrl = "http://stats.kizoom.com/transportdirect/" + chosenDateStr + "/html/daily_sessions.html";

                        reqHTML = webClient.DownloadData(strUrl);
                        objUTF8 = new UTF8Encoding();
                        urlContent = objUTF8.GetString(reqHTML);

                        tempStr = urlContent.Substring(urlContent.IndexOf("iDTV") + 50, 20);
                        tempStr = tempStr.Substring(0, tempStr.IndexOf("<"));
                        if (tempStr.IndexOf(',') > 0)
                        {
                            tempStr = tempStr.Remove(tempStr.IndexOf(','), 1);
                        }
                        else
                        {
                            if (tempStr.Length==0) tempStr = "0";
                        }
                        TVSessions[dayPtr] = tempStr;

                        tempStr = urlContent.Substring(urlContent.IndexOf("Total:") + 53, 10);
                        tempStr = tempStr.Substring(0, tempStr.IndexOf("<"));
                        if (tempStr.IndexOf(',') > 0)
                        {
                            tempStr = tempStr.Remove(tempStr.IndexOf(','), 1);
                        }
                        else
                        {
                            if (tempStr.Length == 0) tempStr = "0";
                        }
                        Sessions[dayPtr] = tempStr;






                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(strUrl + " cannot be found \n" + ex.Message + "\n" + ex.StackTrace);
                    }
                }
                else
                {
                    TVPages[dayPtr] = "0";
                    TVSessions[dayPtr] = "0";
                    Sessions[dayPtr] = "0";
                    Pages[dayPtr] = "0";
                }
            }

            #endregion



            #region write website values to kizoom spreadsheet
            System.IO.StreamWriter file;
            string filePathString;
            filePathString = @"D:\FTPTemp\Kizoom.csv";
            file = new StreamWriter(new FileStream(filePathString, System.IO.FileMode.Create));
			
            //OleDbConnection conn = new OleDbConnection(myConn1);

            try
            {
              
                file.WriteLine("PID,DataName,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
                cellDescription[1] = "1,Sessions";
                cellDescription[2] = "2,TVSessions";
                cellDescription[3] = "3,Pages";
                cellDescription[4] = "4,TVPages";

                StringBuilder tmpString = new StringBuilder();
                for (int descPtr = 1; descPtr < 5; descPtr++)
                {
                     tmpString = new StringBuilder();
                     tmpString.Append(cellDescription[descPtr]);
                        
                    for (int dayPtr = 0; dayPtr < 7; dayPtr++)
                    {   
                        tempTot[dayPtr] = "0";
                        if (descPtr == 1) tempTot[dayPtr] = Sessions[dayPtr];
                        if (descPtr == 2) tempTot[dayPtr] = TVSessions[dayPtr];
                        if (descPtr == 3) tempTot[dayPtr] = Pages[dayPtr];
                        if (descPtr == 4) tempTot[dayPtr] = TVPages[dayPtr];
                        if (tempTot[dayPtr] == null) tempTot[dayPtr] = "0";
                        tmpString.Append("," + tempTot[dayPtr]);
                    }
                    file.WriteLine(tmpString);
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                file.Flush();
                file.Close();

            }
            #endregion






        }




        static bool GetLine(string Path, string SearchString, out string ReturnString)
        {

            // Initialize "myArray"
            int pos = 0;


            string currentLine = null;
            ReturnString = null;
            bool itemFound = false;
            using (StreamReader sr = new StreamReader(Path))
            {
                while (sr.Peek() >= 0 && !itemFound)
                {
                    currentLine = sr.ReadLine();
                    pos = currentLine.IndexOf(SearchString);
                    if (pos >= 0)
                    {
                        itemFound = true;
                        ReturnString = currentLine;
                    }
                }
                // Close reader
                sr.Close();

            }

            return itemFound;
        }





    }
}



/*
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

		private void UpdateStatsBtn_Click(object sender, System.EventArgs e)
		{

	}
}
*/