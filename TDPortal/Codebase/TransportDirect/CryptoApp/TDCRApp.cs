using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SupportApps.CryptoApp
{

	/// <summary>
	/// The TDP crypo application.
	/// Can be used for managing keys and installing them to a machine.
	/// 
	/// </summary>
	public class TDCRApp : System.Windows.Forms.Form
	{
		#region Form declarations
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.TextBox tbKey;
		private System.Windows.Forms.TextBox tbValue;
		private System.Windows.Forms.TextBox tbCryptedKey;
		private System.Windows.Forms.TextBox tbCryptedValue;
		private System.Windows.Forms.Label laValue;
		private System.Windows.Forms.Button buCryptedKeyCopy;
		private System.Windows.Forms.Label laKey;
		private System.Windows.Forms.Button buCryptClear;
		private System.Windows.Forms.MenuItem RSAInstFile;
		private System.Windows.Forms.MenuItem RSAInstGenerate;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buCryptedValueCopy;
		private System.Windows.Forms.MenuItem RSAStoreDelete;
		private System.Windows.Forms.MenuItem RSAStoreInst;
		private System.Windows.Forms.RadioButton rbCrypt;
		private System.Windows.Forms.RadioButton rbDecrypt;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.CheckBox cbStrongCrypt;
		private System.Windows.Forms.MenuItem menuRegenerateKey;
		private System.Windows.Forms.MenuItem menuFileHead;
		private System.Windows.Forms.MenuItem menuRSAStoreHead;
		private System.Windows.Forms.MenuItem menuSaveCryptInf;
		private System.Windows.Forms.MenuItem menuLoadCryptInf;
		private System.Windows.Forms.MenuItem menuAesHead;
		private System.Windows.Forms.Label laAESCrKeyDesc;
		private System.Windows.Forms.MenuItem menuBatchConversion;
		private System.Windows.Forms.MenuItem menuFileSep1;
		private System.Windows.Forms.MenuItem menuCreateTemplate;
		private System.Windows.Forms.MenuItem menuConvert;
		private System.Windows.Forms.MenuItem menuBatchSep1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MenuItem menuCryptDecrypt;
		private System.Windows.Forms.SaveFileDialog saveCryptInfoFileDialog1;
		private System.Windows.Forms.OpenFileDialog openCryptInfoFileDialog1;
		private System.Windows.Forms.OpenFileDialog openBatchFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveBatchFileDialog1;
		private System.Windows.Forms.MenuItem menuCSPStore;
		private System.Windows.Forms.MenuItem menuCSPMachine;
		private System.Windows.Forms.MenuItem menuCSPUser;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuCryptKeyAndValue;
		private System.Windows.Forms.MenuItem menuCryptKeyOnly;
		private System.Windows.Forms.MenuItem menuCryptValueOnly;
		private System.Windows.Forms.Button buCryptedAesKeyCopy;
		private System.Windows.Forms.Button buAesIVCopy;
		private System.Windows.Forms.Label laAESIVDesc;
		private System.Windows.Forms.TextBox tbAesCrKey;
		private System.Windows.Forms.MenuItem menuAesSep1;
		private System.Windows.Forms.MenuItem menuAesSep2;
		private System.Windows.Forms.MenuItem copyAESInClear;
		private System.Windows.Forms.TextBox tbAesIV;
		#endregion

		#region Constructor and destructors
		/// <summary>
		/// The constructor initialises all winform elements created by the 
		/// visual form designer.
		/// </summary>
		public TDCRApp()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			// Use this to locate the path to the given candidate machine key

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


		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TDCRApp));
			this.tbKey = new System.Windows.Forms.TextBox();
			this.tbValue = new System.Windows.Forms.TextBox();
			this.tbCryptedKey = new System.Windows.Forms.TextBox();
			this.tbCryptedValue = new System.Windows.Forms.TextBox();
			this.laValue = new System.Windows.Forms.Label();
			this.laKey = new System.Windows.Forms.Label();
			this.buCryptClear = new System.Windows.Forms.Button();
			this.buCryptedKeyCopy = new System.Windows.Forms.Button();
			this.buCryptedValueCopy = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuFileHead = new System.Windows.Forms.MenuItem();
			this.menuBatchConversion = new System.Windows.Forms.MenuItem();
			this.menuConvert = new System.Windows.Forms.MenuItem();
			this.menuBatchSep1 = new System.Windows.Forms.MenuItem();
			this.menuCreateTemplate = new System.Windows.Forms.MenuItem();
			this.menuFileSep1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuCryptKeyAndValue = new System.Windows.Forms.MenuItem();
			this.menuCryptKeyOnly = new System.Windows.Forms.MenuItem();
			this.menuCryptValueOnly = new System.Windows.Forms.MenuItem();
			this.menuAesHead = new System.Windows.Forms.MenuItem();
			this.menuCryptDecrypt = new System.Windows.Forms.MenuItem();
			this.menuAesSep1 = new System.Windows.Forms.MenuItem();
			this.menuLoadCryptInf = new System.Windows.Forms.MenuItem();
			this.menuSaveCryptInf = new System.Windows.Forms.MenuItem();
			this.menuAesSep2 = new System.Windows.Forms.MenuItem();
			this.menuRegenerateKey = new System.Windows.Forms.MenuItem();
			this.menuRSAStoreHead = new System.Windows.Forms.MenuItem();
			this.menuCSPStore = new System.Windows.Forms.MenuItem();
			this.menuCSPMachine = new System.Windows.Forms.MenuItem();
			this.menuCSPUser = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.RSAStoreInst = new System.Windows.Forms.MenuItem();
			this.RSAInstFile = new System.Windows.Forms.MenuItem();
			this.RSAInstGenerate = new System.Windows.Forms.MenuItem();
			this.RSAStoreDelete = new System.Windows.Forms.MenuItem();
			this.rbCrypt = new System.Windows.Forms.RadioButton();
			this.rbDecrypt = new System.Windows.Forms.RadioButton();
			this.cbStrongCrypt = new System.Windows.Forms.CheckBox();
			this.saveCryptInfoFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openCryptInfoFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.laAESCrKeyDesc = new System.Windows.Forms.Label();
			this.tbAesCrKey = new System.Windows.Forms.TextBox();
			this.tbAesIV = new System.Windows.Forms.TextBox();
			this.laAESIVDesc = new System.Windows.Forms.Label();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.openBatchFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveBatchFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.buCryptedAesKeyCopy = new System.Windows.Forms.Button();
			this.buAesIVCopy = new System.Windows.Forms.Button();
			this.copyAESInClear = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// tbKey
			// 
			this.tbKey.Enabled = false;
			this.tbKey.Location = new System.Drawing.Point(112, 56);
			this.tbKey.Name = "tbKey";
			this.tbKey.Size = new System.Drawing.Size(432, 20);
			this.tbKey.TabIndex = 0;
			this.tbKey.Text = "";
			// 
			// tbValue
			// 
			this.tbValue.Enabled = false;
			this.tbValue.Location = new System.Drawing.Point(112, 136);
			this.tbValue.Name = "tbValue";
			this.tbValue.Size = new System.Drawing.Size(432, 20);
			this.tbValue.TabIndex = 1;
			this.tbValue.Text = "";
			// 
			// tbCryptedKey
			// 
			this.tbCryptedKey.Location = new System.Drawing.Point(112, 80);
			this.tbCryptedKey.Name = "tbCryptedKey";
			this.tbCryptedKey.ReadOnly = true;
			this.tbCryptedKey.Size = new System.Drawing.Size(432, 20);
			this.tbCryptedKey.TabIndex = 2;
			this.tbCryptedKey.Text = "";
			// 
			// tbCryptedValue
			// 
			this.tbCryptedValue.Location = new System.Drawing.Point(112, 160);
			this.tbCryptedValue.Name = "tbCryptedValue";
			this.tbCryptedValue.ReadOnly = true;
			this.tbCryptedValue.Size = new System.Drawing.Size(432, 20);
			this.tbCryptedValue.TabIndex = 3;
			this.tbCryptedValue.Text = "";
			// 
			// laValue
			// 
			this.laValue.Location = new System.Drawing.Point(112, 112);
			this.laValue.Name = "laValue";
			this.laValue.Size = new System.Drawing.Size(432, 23);
			this.laValue.TabIndex = 7;
			this.laValue.Text = "Value";
			// 
			// laKey
			// 
			this.laKey.Location = new System.Drawing.Point(112, 32);
			this.laKey.Name = "laKey";
			this.laKey.Size = new System.Drawing.Size(432, 23);
			this.laKey.TabIndex = 12;
			this.laKey.Text = "Key";
			// 
			// buCryptClear
			// 
			this.buCryptClear.Enabled = false;
			this.buCryptClear.Location = new System.Drawing.Point(584, 192);
			this.buCryptClear.Name = "buCryptClear";
			this.buCryptClear.Size = new System.Drawing.Size(72, 24);
			this.buCryptClear.TabIndex = 11;
			this.buCryptClear.Text = "Encr&ypt";
			this.buCryptClear.Click += new System.EventHandler(this.buCryptClear_Click);
			// 
			// buCryptedKeyCopy
			// 
			this.buCryptedKeyCopy.Enabled = false;
			this.buCryptedKeyCopy.Location = new System.Drawing.Point(552, 80);
			this.buCryptedKeyCopy.Name = "buCryptedKeyCopy";
			this.buCryptedKeyCopy.Size = new System.Drawing.Size(104, 24);
			this.buCryptedKeyCopy.TabIndex = 10;
			this.buCryptedKeyCopy.Text = "Copy to clipboard";
			this.buCryptedKeyCopy.Click += new System.EventHandler(this.buCryptedKeyCopy_Click);
			// 
			// buCryptedValueCopy
			// 
			this.buCryptedValueCopy.Enabled = false;
			this.buCryptedValueCopy.Location = new System.Drawing.Point(552, 160);
			this.buCryptedValueCopy.Name = "buCryptedValueCopy";
			this.buCryptedValueCopy.Size = new System.Drawing.Size(104, 24);
			this.buCryptedValueCopy.TabIndex = 9;
			this.buCryptedValueCopy.Text = "Copy to clipboard";
			this.buCryptedValueCopy.Click += new System.EventHandler(this.buCryptedValueCopy_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuFileHead,
																					  this.menuItem2,
																					  this.menuAesHead,
																					  this.menuRSAStoreHead});
			// 
			// menuFileHead
			// 
			this.menuFileHead.Index = 0;
			this.menuFileHead.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuBatchConversion,
																						 this.menuFileSep1,
																						 this.menuExit});
			this.menuFileHead.Text = "&File";
			// 
			// menuBatchConversion
			// 
			this.menuBatchConversion.Index = 0;
			this.menuBatchConversion.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.menuConvert,
																								this.menuBatchSep1,
																								this.menuCreateTemplate});
			this.menuBatchConversion.Text = "&Batch";
			// 
			// menuConvert
			// 
			this.menuConvert.Index = 0;
			this.menuConvert.Text = "Convert...";
			this.menuConvert.Click += new System.EventHandler(this.menuConvert_Click);
			// 
			// menuBatchSep1
			// 
			this.menuBatchSep1.Index = 1;
			this.menuBatchSep1.Text = "-";
			// 
			// menuCreateTemplate
			// 
			this.menuCreateTemplate.Index = 2;
			this.menuCreateTemplate.Text = "Create template file...";
			this.menuCreateTemplate.Click += new System.EventHandler(this.menuCreateTemplate_Click);
			// 
			// menuFileSep1
			// 
			this.menuFileSep1.Index = 1;
			this.menuFileSep1.Text = "-";
			// 
			// menuExit
			// 
			this.menuExit.Index = 2;
			this.menuExit.Text = "E&xit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuCryptKeyAndValue,
																					  this.menuCryptKeyOnly,
																					  this.menuCryptValueOnly});
			this.menuItem2.Text = "Encryption";
			// 
			// menuCryptKeyAndValue
			// 
			this.menuCryptKeyAndValue.Checked = true;
			this.menuCryptKeyAndValue.Index = 0;
			this.menuCryptKeyAndValue.RadioCheck = true;
			this.menuCryptKeyAndValue.Text = "Key and Value";
			this.menuCryptKeyAndValue.Click += new System.EventHandler(this.menuCryptKeyAndValue_Click);
			// 
			// menuCryptKeyOnly
			// 
			this.menuCryptKeyOnly.Checked = true;
			this.menuCryptKeyOnly.Index = 1;
			this.menuCryptKeyOnly.RadioCheck = true;
			this.menuCryptKeyOnly.Text = "Key only";
			this.menuCryptKeyOnly.Click += new System.EventHandler(this.menuCryptKeyOnly_Click);
			// 
			// menuCryptValueOnly
			// 
			this.menuCryptValueOnly.Checked = true;
			this.menuCryptValueOnly.Index = 2;
			this.menuCryptValueOnly.RadioCheck = true;
			this.menuCryptValueOnly.Text = "Value only";
			this.menuCryptValueOnly.Click += new System.EventHandler(this.menuCryptValueOnly_Click);
			// 
			// menuAesHead
			// 
			this.menuAesHead.Index = 2;
			this.menuAesHead.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuCryptDecrypt,
																						this.menuAesSep1,
																						this.menuLoadCryptInf,
																						this.menuSaveCryptInf,
																						this.menuAesSep2,
																						this.menuRegenerateKey,
																						this.copyAESInClear});
			this.menuAesHead.Text = "&AES";
			// 
			// menuCryptDecrypt
			// 
			this.menuCryptDecrypt.Index = 0;
			this.menuCryptDecrypt.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
			this.menuCryptDecrypt.Text = "Encr&ypt/Decrypt value";
			this.menuCryptDecrypt.Click += new System.EventHandler(this.buCryptClear_Click);
			// 
			// menuAesSep1
			// 
			this.menuAesSep1.Index = 1;
			this.menuAesSep1.Text = "-";
			// 
			// menuLoadCryptInf
			// 
			this.menuLoadCryptInf.Index = 2;
			this.menuLoadCryptInf.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.menuLoadCryptInf.Text = "&Import Crypto information...";
			this.menuLoadCryptInf.Click += new System.EventHandler(this.menuLoadCryptInf_Click);
			// 
			// menuSaveCryptInf
			// 
			this.menuSaveCryptInf.Index = 3;
			this.menuSaveCryptInf.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuSaveCryptInf.Text = "&Export Crypto information...";
			this.menuSaveCryptInf.Click += new System.EventHandler(this.menuSaveCryptInf_Click);
			// 
			// menuAesSep2
			// 
			this.menuAesSep2.Index = 4;
			this.menuAesSep2.Text = "-";
			// 
			// menuRegenerateKey
			// 
			this.menuRegenerateKey.Index = 5;
			this.menuRegenerateKey.Text = "&Generate new AES key";
			this.menuRegenerateKey.Click += new System.EventHandler(this.menuRegenerateKey_Click);
			// 
			// menuRSAStoreHead
			// 
			this.menuRSAStoreHead.Index = 3;
			this.menuRSAStoreHead.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.menuCSPStore,
																							 this.menuItem1,
																							 this.RSAStoreInst,
																							 this.RSAStoreDelete});
			this.menuRSAStoreHead.Text = "&RSA Store";
			// 
			// menuCSPStore
			// 
			this.menuCSPStore.Index = 0;
			this.menuCSPStore.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuCSPMachine,
																						 this.menuCSPUser});
			this.menuCSPStore.Text = "CSP storage";
			// 
			// menuCSPMachine
			// 
			this.menuCSPMachine.Checked = true;
			this.menuCSPMachine.Index = 0;
			this.menuCSPMachine.RadioCheck = true;
			this.menuCSPMachine.Text = "Machine";
			this.menuCSPMachine.Click += new System.EventHandler(this.menuCSPMachine_Click);
			// 
			// menuCSPUser
			// 
			this.menuCSPUser.Checked = true;
			this.menuCSPUser.Index = 1;
			this.menuCSPUser.RadioCheck = true;
			this.menuCSPUser.Text = "User";
			this.menuCSPUser.Click += new System.EventHandler(this.menuCSPUser_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "-";
			// 
			// RSAStoreInst
			// 
			this.RSAStoreInst.Index = 2;
			this.RSAStoreInst.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.RSAInstFile,
																						 this.RSAInstGenerate});
			this.RSAStoreInst.Text = "&Install TDP RSA Store";
			// 
			// RSAInstFile
			// 
			this.RSAInstFile.Index = 0;
			this.RSAInstFile.Text = "From file...";
			this.RSAInstFile.Click += new System.EventHandler(this.RSAInstFile_Click);
			// 
			// RSAInstGenerate
			// 
			this.RSAInstGenerate.Index = 1;
			this.RSAInstGenerate.Text = "Generate file (RSA and AES)...";
			this.RSAInstGenerate.Click += new System.EventHandler(this.RSAInstGenerate_Click);
			// 
			// RSAStoreDelete
			// 
			this.RSAStoreDelete.Index = 3;
			this.RSAStoreDelete.Text = "&Delete existing TDP RSA Store key";
			this.RSAStoreDelete.Click += new System.EventHandler(this.RSAStoreDelete_Click);
			// 
			// rbCrypt
			// 
			this.rbCrypt.Checked = true;
			this.rbCrypt.Location = new System.Drawing.Point(8, 32);
			this.rbCrypt.Name = "rbCrypt";
			this.rbCrypt.TabIndex = 13;
			this.rbCrypt.TabStop = true;
			this.rbCrypt.Text = "Encrypt";
			this.rbCrypt.CheckedChanged += new System.EventHandler(this.rbCrypt_CheckedChanged);
			// 
			// rbDecrypt
			// 
			this.rbDecrypt.Location = new System.Drawing.Point(8, 56);
			this.rbDecrypt.Name = "rbDecrypt";
			this.rbDecrypt.TabIndex = 14;
			this.rbDecrypt.Text = "Decrypt";
			this.rbDecrypt.CheckedChanged += new System.EventHandler(this.rbDecrypt_CheckedChanged);
			// 
			// cbStrongCrypt
			// 
			this.cbStrongCrypt.Location = new System.Drawing.Point(8, 8);
			this.cbStrongCrypt.Name = "cbStrongCrypt";
			this.cbStrongCrypt.Size = new System.Drawing.Size(320, 24);
			this.cbStrongCrypt.TabIndex = 15;
			this.cbStrongCrypt.Text = "Use strong RSA cryptography for the AES (RIJANDEL) key";
			// 
			// saveCryptInfoFileDialog1
			// 
			this.saveCryptInfoFileDialog1.DefaultExt = "cryinf";
			this.saveCryptInfoFileDialog1.Filter = "Crypt information files (*.cryinf)|*.cryinf";
			this.saveCryptInfoFileDialog1.Title = "Open cryptography information  file";
			// 
			// openCryptInfoFileDialog1
			// 
			this.openCryptInfoFileDialog1.DefaultExt = "cryinf";
			this.openCryptInfoFileDialog1.Filter = "Crypt information files (*.cryinf)|*.cryinf";
			this.openCryptInfoFileDialog1.Title = "Save cryptography information  file";
			// 
			// laAESCrKeyDesc
			// 
			this.laAESCrKeyDesc.Location = new System.Drawing.Point(8, 264);
			this.laAESCrKeyDesc.Name = "laAESCrKeyDesc";
			this.laAESCrKeyDesc.Size = new System.Drawing.Size(672, 23);
			this.laAESCrKeyDesc.TabIndex = 16;
			this.laAESCrKeyDesc.Text = "AES (RIJANDEL) crypted key";
			// 
			// tbAesCrKey
			// 
			this.tbAesCrKey.Location = new System.Drawing.Point(8, 288);
			this.tbAesCrKey.Name = "tbAesCrKey";
			this.tbAesCrKey.ReadOnly = true;
			this.tbAesCrKey.Size = new System.Drawing.Size(536, 20);
			this.tbAesCrKey.TabIndex = 17;
			this.tbAesCrKey.Text = "";
			// 
			// tbAesIV
			// 
			this.tbAesIV.Location = new System.Drawing.Point(8, 344);
			this.tbAesIV.Name = "tbAesIV";
			this.tbAesIV.ReadOnly = true;
			this.tbAesIV.Size = new System.Drawing.Size(536, 20);
			this.tbAesIV.TabIndex = 19;
			this.tbAesIV.Text = "";
			// 
			// laAESIVDesc
			// 
			this.laAESIVDesc.Location = new System.Drawing.Point(8, 320);
			this.laAESIVDesc.Name = "laAESIVDesc";
			this.laAESIVDesc.Size = new System.Drawing.Size(664, 23);
			this.laAESIVDesc.TabIndex = 18;
			this.laAESIVDesc.Text = "AES (RIJANDEL) IV";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 383);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(686, 22);
			this.statusBar1.TabIndex = 20;
			// 
			// openBatchFileDialog1
			// 
			this.openBatchFileDialog1.DefaultExt = "crybch";
			this.openBatchFileDialog1.Filter = "Crypt batch files (*.crybch)|*.crybch";
			this.openBatchFileDialog1.Title = "Batch file to open";
			// 
			// saveBatchFileDialog1
			// 
			this.saveBatchFileDialog1.DefaultExt = "xml";
			this.saveBatchFileDialog1.Filter = "XML files (*.xml)|*.xml";
			this.saveBatchFileDialog1.Title = "Save batch results to";
			// 
			// buCryptedAesKeyCopy
			// 
			this.buCryptedAesKeyCopy.Enabled = false;
			this.buCryptedAesKeyCopy.Location = new System.Drawing.Point(552, 288);
			this.buCryptedAesKeyCopy.Name = "buCryptedAesKeyCopy";
			this.buCryptedAesKeyCopy.Size = new System.Drawing.Size(104, 24);
			this.buCryptedAesKeyCopy.TabIndex = 21;
			this.buCryptedAesKeyCopy.Text = "Copy to clipboard";
			this.buCryptedAesKeyCopy.Click += new System.EventHandler(this.buCryptedAesKeyCopy_Click);
			// 
			// buAesIVCopy
			// 
			this.buAesIVCopy.Enabled = false;
			this.buAesIVCopy.Location = new System.Drawing.Point(552, 344);
			this.buAesIVCopy.Name = "buAesIVCopy";
			this.buAesIVCopy.Size = new System.Drawing.Size(104, 24);
			this.buAesIVCopy.TabIndex = 22;
			this.buAesIVCopy.Text = "Copy to clipboard";
			this.buAesIVCopy.Click += new System.EventHandler(this.buAesIVCopy_Click);
			// 
			// copyAESInClear
			// 
			this.copyAESInClear.Index = 6;
			this.copyAESInClear.Text = "Copy base64 AES key (clear) to clipboard";
			this.copyAESInClear.Click += new System.EventHandler(this.copyAESInClear_Click);
			// 
			// TDCRApp
			// 
			this.AcceptButton = this.buCryptClear;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(686, 405);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buAesIVCopy,
																		  this.buCryptedAesKeyCopy,
																		  this.statusBar1,
																		  this.tbAesIV,
																		  this.laAESIVDesc,
																		  this.tbAesCrKey,
																		  this.laAESCrKeyDesc,
																		  this.cbStrongCrypt,
																		  this.rbDecrypt,
																		  this.rbCrypt,
																		  this.tbCryptedKey,
																		  this.laKey,
																		  this.laValue,
																		  this.tbKey,
																		  this.buCryptClear,
																		  this.buCryptedKeyCopy,
																		  this.tbValue,
																		  this.buCryptedValueCopy,
																		  this.tbCryptedValue});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "TDCRApp";
			this.Text = "TDP Crypto app";
			this.Load += new System.EventHandler(this.TDCRApp_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Private variable declarations
		/// <summary>
		/// Constant indicating the location of the machine keys.
		/// </summary>
		private const string MACHINE_KEY_LOC = @"\Microsoft\Crypto\RSA\MachineKeys";
		/// As enhancemnt apply automatic logic to ensure this key is readable by selected user/group

		/// <summary>
		/// Indicates if the application is in encrypt or decrypt mode.
		/// </summary>
		private bool isCryptMode = true;
		/// <summary>
		/// Indicates if the application uses machine store or user store.
		/// </summary>
		private bool machineStore = true;

		/// <summary>
		/// Indicates if keys should be encrypted/decrypted
		/// </summary>
		private bool cryptKey = true;
		/// <summary>
		/// Indicates if values should be encrypted/decrypted
		/// </summary>
		private bool cryptValue = true;

		/// <summary>
		/// Holder for the encrypted symmetric key (RIJANDEL).
		/// </summary>
		private byte[] crSymKey;
		/// <summary>
		/// Holder for the initialisation vector.
		/// </summary>
		private byte[] symIV;

		#endregion

		#region Field handling
		/// <summary>
		/// Handles disable/enable and visibility of all the form fields and menues.
		/// </summary>
		private void EnableFields()
		{
			bool hasRSAStore = StoreExists();

			tbKey.Enabled = hasRSAStore;
			tbValue.Enabled = hasRSAStore;
			tbCryptedKey.Enabled = hasRSAStore;
			tbCryptedValue.Enabled = hasRSAStore;

			buCryptedKeyCopy.Enabled = tbCryptedKey.Text != string.Empty;
			buCryptedValueCopy.Enabled = tbCryptedValue.Text != string.Empty;
			buCryptClear.Enabled = hasRSAStore;

			RSAStoreDelete.Enabled = hasRSAStore;

			buCryptClear.Text = isCryptMode ? "Encr&ypt" : "Decr&ypt";
			tbKey.ReadOnly = !isCryptMode || !cryptKey;
			tbCryptedKey.ReadOnly = isCryptMode || !cryptKey;
			tbValue.ReadOnly = !isCryptMode || !cryptValue;
			tbCryptedValue.ReadOnly = isCryptMode || !cryptValue;

			buCryptedKeyCopy.Enabled = isCryptMode;
			buCryptedValueCopy.Enabled = isCryptMode;

			tbAesCrKey.Text = hasRSAStore ? Convert.ToBase64String(crSymKey) : string.Empty;
			tbAesIV.Text = hasRSAStore ? Convert.ToBase64String(symIV) : string.Empty;

			menuBatchConversion.Enabled = hasRSAStore;

			menuCSPMachine.Checked = machineStore;
			menuCSPUser.Checked = !machineStore;

			menuCryptKeyAndValue.Checked = cryptValue && cryptKey;
			menuCryptKeyOnly.Checked = !cryptValue && cryptKey;
			menuCryptValueOnly.Checked = cryptValue && !cryptKey;

			buCryptedAesKeyCopy.Enabled = tbAesCrKey.Text != string.Empty;
			buAesIVCopy.Enabled = tbAesIV.Text != string.Empty;

		}
		#endregion

		#region RSA Store information and manipulation
		/// <summary>
		/// Ensures that the user has the ability to access the CSP store and that the
		/// symmetric key and initialisation vector is loaded.
		/// </summary>
		/// <returns>true if store can be accessed and key plus IV exists</returns>
		private bool StoreExists()
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = TDCrypt.CSP_STORE;
			// If machine store is selected - use it else use the default (user store)
			if( machineStore )
			{
				csp.Flags = CspProviderFlags.UseMachineKeyStore;
			}

			// Find the rsa crypto
			try
			{
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
			}
			catch( CryptographicException ce )
			{
				// Only happens if CSP cannot be acquired
				statusBar1.Text = ce.Message;
				return false;
			}
			return crSymKey != null && symIV != null;

		}

		/// <summary>
		/// Deletes the current store
		/// </summary>
		private void StoreDelete()
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = TDCrypt.CSP_STORE;
			csp.Flags = CspProviderFlags.UseMachineKeyStore;
			// Save it to the container
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

			rsa.PersistKeyInCsp = false;

			rsa.Clear();

		}

		/// <summary>
		/// Installs a new store key.
		/// </summary>
		/// <returns>The RSA parameters containing information for the new RSA key</returns>
		private RSAParameters StoreInstall()
		{
			return StoreInstall( false, new RSAParameters() );
		}

		private RSAParameters StoreInstall( bool useSuppliedRSA, RSAParameters rsaparams )
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = TDCrypt.CSP_STORE;
			csp.Flags = CspProviderFlags.UseMachineKeyStore;

			// Save it to the container
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

			if( useSuppliedRSA )
			{
				rsa.ImportParameters( rsaparams );
			}
			else 
			{
				rsaparams = rsa.ExportParameters(true);
			}

			return rsaparams;
		}

		/// <summary>
		/// Creates a new AES key and IV
		/// </summary>
		/// <returns>The new Rijandel crypto</returns>
		private Rijndael CreateAES()
		{

			Rijndael rij = RijndaelManaged.Create();

			rij.GenerateKey();
			rij.GenerateIV();

			return rij;
		}

		/// <summary>
		/// Encrypts the AES key with the RSA key as well as stores the IV.
		/// The IV is unencrypted.
		/// </summary>
		/// <param name="rij">The Rijandel</param>
		private void EncryptAES(Rijndael rij)
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = TDCrypt.CSP_STORE;
			csp.Flags = CspProviderFlags.UseMachineKeyStore;

			// Save it to the container
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

			// Decrypt the crypted key into readable value
			crSymKey = rsa.Encrypt( rij.Key , false );
			symIV = rij.IV;

		}

		/// <summary>
		/// Saves the crypto information needed.
		/// </summary>
		/// <param name="file">The name of the file to write</param>
		private void SaveCryptoInformation(string file)
		{
			RSAParameters rsaparams = StoreInstall();

			CryptoSaver cr = new CryptoSaver( rsaparams, crSymKey, symIV );
			cr.UseMachineStore = machineStore;
			cr.Store = TDCrypt.CSP_STORE;

			try
			{
				XmlSerializer xmls = new XmlSerializer( cr.GetType());
				using( FileStream fs = File.OpenWrite(file) )
				{
					xmls.Serialize( fs, cr );
				}
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}
		}

		/// <summary>
		/// Loads the crypto information, installs RSA and captures the AES key.
		/// </summary>
		/// <param name="installRSA">true if the RSA is to be installed, false otherwise</param>
		/// <param name="file">The file that the information is read from</param>
		/// <returns></returns>
		private CryptoSaver LoadCryptoInformation(bool installRSA, string file)
		{
			XmlSerializer xmls = new XmlSerializer(typeof( CryptoSaver ));

			using( FileStream fs = File.OpenRead(file) )
			{
				CryptoSaver cs = (CryptoSaver)xmls.Deserialize( fs );
				if( installRSA )
				{
					StoreInstall( true, cs.RSAParams );	
				}
				// Load cSymKey and VI from crypto saver
				symIV = Convert.FromBase64String(cs.SymIV);
				crSymKey = Convert.FromBase64String( cs.CryptSymKey );
				return cs; 
			}
		}
		#endregion

		#region Main run method and Form load
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TDCRApp());
		}

		/// <summary>
		/// The onload event ensures all fields are at their correct state.
		/// </summary>
		private void TDCRApp_Load(object sender, System.EventArgs e)
		{
			EnableFields();
		}
		#endregion

		#region Event handling

		/// <summary>
		/// Event for the installation of a RSA crypto file.
		/// </summary>
		private void RSAInstFile_Click(object sender, System.EventArgs e)
		{
			openCryptInfoFileDialog1.ShowDialog( this );
			if( !File.Exists(openCryptInfoFileDialog1.FileName) )
			{
				return;
			}
			statusBar1.Text = string.Empty;

			try
			{
				LoadCryptoInformation(true, openCryptInfoFileDialog1.FileName);
				statusBar1.Text = "Installed RSA successfully";
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}

			EnableFields();
		}

		/// <summary>
		/// The event for generating a new RSA & AES key.
		/// </summary>
		private void RSAInstGenerate_Click(object sender, System.EventArgs e)
		{
			saveCryptInfoFileDialog1.ShowDialog( this );
			if( saveCryptInfoFileDialog1.FileName == string.Empty || saveCryptInfoFileDialog1.FileName == null )
			{
				return;
			}
			statusBar1.Text = string.Empty;

			try
			{
				Rijndael rij = CreateAES();
				EncryptAES(rij);

				SaveCryptoInformation(saveCryptInfoFileDialog1.FileName);
				statusBar1.Text = "Generated RSA successfully";
			}
			catch( Exception exce )
			{
				statusBar1.Text = exce.Message;
			}

			EnableFields();
		}


		/// <summary>
		/// The event for deleting the RSA key
		/// </summary>
		private void RSAStoreDelete_Click(object sender, System.EventArgs e)
		{
			StoreDelete();
			crSymKey = null;
			symIV = null;

			EnableFields();
		}

		/// <summary>
		/// The event for changing between encrypting/decrypting
		/// </summary>
		private void rbCrypt_CheckedChanged(object sender, System.EventArgs e)
		{
			isCryptMode = true;
			tbKey.Text = string.Empty;
			tbValue.Text = string.Empty;
			tbCryptedKey.Text = string.Empty;
			tbCryptedValue.Text =  string.Empty;
			EnableFields();
		}

		/// <summary>
		/// The event for changing between encrypting/decrypting
		/// </summary>
		private void rbDecrypt_CheckedChanged(object sender, System.EventArgs e)
		{
			isCryptMode = false;
			tbKey.Text = string.Empty;
			tbValue.Text = string.Empty;
			tbCryptedKey.Text = string.Empty;
			tbCryptedValue.Text =  string.Empty;
			EnableFields();		
		}

		/// <summary>
		/// The event for executing the actual encryption/decryption
		/// </summary>
		private void buCryptClear_Click(object sender, System.EventArgs e)
		{	
			statusBar1.Text = string.Empty;

			string key = tbKey.Text;
			string val = tbValue.Text;
			string cKey = tbCryptedKey.Text;
			string cVal = tbCryptedValue.Text;


			TDCrypt tdc = new TDCrypt( crSymKey, symIV, false, machineStore);

			try
			{
				if( isCryptMode )
				{
					if( key != string.Empty )
						cKey = TDCrypt.CRYPT_PREFIX + tdc.Encrypt( key );
					if( val != string.Empty )
						cVal = TDCrypt.CRYPT_PREFIX + tdc.Encrypt( val );
				}
				else
				{
					if( cKey != string.Empty && cKey.Length > TDCrypt.CRYPT_PREFIX.Length )
						key = tdc.Decrypt( cKey.Substring( TDCrypt.CRYPT_PREFIX.Length ) );
					if( cVal != string.Empty && cVal.Length > TDCrypt.CRYPT_PREFIX.Length )
						val = tdc.Decrypt( cVal.Substring( TDCrypt.CRYPT_PREFIX.Length ) );
				}
				tbKey.Text = key;
				tbValue.Text = val;
				tbCryptedKey.Text = cKey;
				tbCryptedValue.Text = cVal;
				statusBar1.Text =  buCryptClear.Text.ToLower().Replace("&","") + " successfully";
			}
			catch( Exception exce )
			{
				statusBar1.Text = "Can not "+ buCryptClear.Text.ToLower().Replace("&","")+" key/values. Reason: "+exce.Message;
			}

			EnableFields();
		}

		/// <summary>
		/// The event for exiting the application
		/// </summary>
		private void menuExit_Click(object sender, System.EventArgs e)
		{			
			Application.Exit();
		}

		/// <summary>
		/// The event for copy the crypted property name into clipboard buffer
		/// </summary>
		private void buCryptedKeyCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject( tbCryptedKey.Text, true );
		}

		/// <summary>
		/// The event for copy the crypted property value into clipboard buffer
		/// </summary>
		private void buCryptedValueCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject( tbCryptedValue.Text, true );
		}

		/// <summary>
		/// The event for generating a new AES key.
		/// </summary>
		private void menuRegenerateKey_Click(object sender, System.EventArgs e)
		{
			statusBar1.Text = string.Empty;
			try
			{
				Rijndael rij = CreateAES();
				EncryptAES(rij);
				statusBar1.Text = "Generated AES successfully";
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}

			EnableFields();
		}

		/// <summary>
		/// The event for saving crypto information file.
		/// </summary>
		private void menuSaveCryptInf_Click(object sender, System.EventArgs e)
		{
			saveCryptInfoFileDialog1.ShowDialog( this );
			if( saveCryptInfoFileDialog1.FileName == string.Empty || saveCryptInfoFileDialog1.FileName == null )
			{
				return;
			}
			statusBar1.Text = string.Empty;

			try
			{
				SaveCryptoInformation( saveCryptInfoFileDialog1.FileName );
				statusBar1.Text = "Save successfull";
			}
			catch( Exception exce )
			{
				statusBar1.Text = exce.Message;
			}

			EnableFields();
		
		}

		/// <summary>
		/// The event for loading a crypto information file, but only install the AES key.
		/// </summary>
		private void menuLoadCryptInf_Click(object sender, System.EventArgs e)
		{
			openCryptInfoFileDialog1.ShowDialog( this );
			if( !File.Exists(openCryptInfoFileDialog1.FileName) )
			{
				return;
			}
			statusBar1.Text = string.Empty;
			try
			{
				LoadCryptoInformation(false, openCryptInfoFileDialog1.FileName);
				statusBar1.Text = "Load successfull";
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}


			EnableFields();
		}

		/// <summary>
		/// The event for batch converting properties.
		/// </summary>
		private void menuConvert_Click(object sender, System.EventArgs e)
		{
			openBatchFileDialog1.ShowDialog( this );
			if( !File.Exists(openBatchFileDialog1.FileName) )
			{
				return;
			}
			saveBatchFileDialog1.ShowDialog( this );
			if( saveBatchFileDialog1.FileName == string.Empty || saveBatchFileDialog1.FileName == null )
			{
				return;
			}
			statusBar1.Text = string.Empty;


			try
			{
				TDCrypt tdc = new TDCrypt( crSymKey, symIV, false, machineStore);
				XmlSerializer xmls = new XmlSerializer(typeof( NameValuePair[] ));
				using ( FileStream fs = File.OpenRead(openBatchFileDialog1.FileName))
				{
					NameValuePair[] original = (NameValuePair[])xmls.Deserialize( fs );
					NameValuePair[] crypted = new NameValuePair[ original.Length ];
					for( int i = 0; i < original.Length; i++)
					{
						crypted[i] = new NameValuePair();
						if( cryptKey )
							crypted[i].Name = TDCrypt.CRYPT_PREFIX + tdc.Encrypt( original[i].Name );
						else 
							crypted[i].Name = original[i].Name;

						if( cryptValue )
							crypted[i].Value = TDCrypt.CRYPT_PREFIX + tdc.Encrypt( original[i].Value );
						else 
							crypted[i].Value = original[i].Value;
					}
					using( FileStream fs2 = File.OpenWrite(saveBatchFileDialog1.FileName) )
					{
						xmls.Serialize( fs2, crypted );
					}
					statusBar1.Text = "Batch conversion successfull";
				}
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}
		}

		/// <summary>
		/// The event for creating a batch-file template.
		/// </summary>
		private void menuCreateTemplate_Click(object sender, System.EventArgs e)
		{
			saveBatchFileDialog1.ShowDialog( this );
			if( saveBatchFileDialog1.FileName == string.Empty || saveBatchFileDialog1.FileName == null )
			{
				return;
			}
			statusBar1.Text = string.Empty;

			try
			{
				XmlSerializer xmls = new XmlSerializer( typeof( NameValuePair[] ) );
				NameValuePair[] nvp = new NameValuePair[3];
				for(int i = 0; i < nvp.Length; i++)
				{
					nvp[i] = new NameValuePair();
					// For template generation - use hard coded names/values
					nvp[i].Name = "Name "+(i+1);
					nvp[i].Value = "Value "+(i+1);
				}
				using( FileStream fs = File.OpenWrite(saveBatchFileDialog1.FileName) )
				{
					xmls.Serialize( fs, nvp );
				}
				statusBar1.Text = "Template created successfully";
			}
			catch( Exception exce)
			{
				statusBar1.Text = exce.Message;
			}		
		}

		/// <summary>
		/// The event that switches which store that should be used.
		/// </summary>
		private void menuCSPUser_Click(object sender, System.EventArgs e)
		{
			machineStore = false;
			EnableFields();
		}

		/// <summary>
		/// The event that switches which store that should be used.
		/// </summary>
		private void menuCSPMachine_Click(object sender, System.EventArgs e)
		{
			machineStore = true;
			EnableFields();
		}

		/// <summary>
		/// Changes which property fields should be encrypted - key + value, key only, value only
		/// </summary>
		private void menuCryptKeyAndValue_Click(object sender, System.EventArgs e)
		{
			cryptKey = true;
			cryptValue = true;
			EnableFields();
		}

		/// <summary>
		/// Changes which property fields should be encrypted - key + value, key only, value only
		/// </summary>
		private void menuCryptKeyOnly_Click(object sender, System.EventArgs e)
		{
			cryptKey = true;
			cryptValue = false;
			EnableFields();
		}

		/// <summary>
		/// Changes which property fields should be encrypted - key + value, key only, value only
		/// </summary>
		private void menuCryptValueOnly_Click(object sender, System.EventArgs e)
		{
			cryptKey = false;
			cryptValue = true;
			EnableFields();
		
		}

		/// <summary>
		/// The event for copy the IV name into clipboard buffer
		/// </summary>
		private void buAesIVCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject( tbAesIV.Text, true );
		}

		/// <summary>
		/// The event for copy the crypted AES key into clipboard buffer
		/// </summary>
		private void buCryptedAesKeyCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject( tbAesCrKey.Text, true );
		}
		/// <summary>
		/// Copies current AES key into the clipboard as cleartext
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyAESInClear_Click(object sender, System.EventArgs e)
		{
			// Find csp store
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = TDCrypt.CSP_STORE;
			csp.Flags = CspProviderFlags.UseMachineKeyStore;

			// Save it to the container
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

			// Decrypt the crypted key into readable value
			byte[] clearTextAES = rsa.Decrypt( crSymKey , false );

			Clipboard.SetDataObject( Convert.ToBase64String( clearTextAES ), false );
	
		}

		#endregion


	}
}
