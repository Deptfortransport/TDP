// *********************************************** 
// NAME                 : BusinessLinkTemplateSelectControl.ascx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 24/11/2005
// DESCRIPTION			: 
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/BusinessLinkTemplateSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jan 07 2009 08:50:26   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:19:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:24   mturner
//Initial revision.
//
//   Rev 1.6   Feb 24 2006 14:45:18   RWilby
//Fix for merge stream3129.
//
//   Rev 1.5   Feb 24 2006 12:24:24   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.4   Jan 05 2006 17:45:54   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.3   Dec 16 2005 13:01:00   jbroome
//Updated control. Now fires changed event when radio button selection changes.
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Dec 02 2005 17:07:20   tolomolaiye
//Updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Nov 29 2005 20:48:50   asinclair
//Updated to use ScriptableRadioButton
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.0   Nov 28 2005 09:22:36   asinclair
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Web.Controls;

	/// <summary>
	/// Control consists of a data list which displays the available templates
	/// </summary>
	public partial class BusinessLinkTemplateSelectControl : TDUserControl
	{
		public event EventHandler TemplateSelectionChanged;

		protected TransportDirect.UserPortal.Web.Controls.ScriptableGroupRadioButton templateSelectRadioButton;

		private BusinessLinkTemplate selectedTemplate;

		private IBusinessLinksTemplateCatalogue linkCatalogue = (IBusinessLinksTemplateCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.BusinessLinksTemplateCatalogue];

		/// <summary>
		/// Constructor sets local resource manager
		/// </summary>
		public BusinessLinkTemplateSelectControl()
		{
			this.LocalResourceManager = TDResourceManager.TOOLS_TIPS_RM;
		}

		/// <summary>
		/// Page load method
		/// Sets data source and binds data
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			dataListBusinessTemplates.DataSource = linkCatalogue.GetAll();
			dataListBusinessTemplates.DataBind();
		}

		/// <summary>
		/// OnPreRender method
		/// Need to re-bind data so that checked-changed events will fire
		/// </summary>
		private void OnPreRender(object sender, EventArgs e)
		{
			dataListBusinessTemplates.DataBind();
		}

		/// <summary>
		/// Returns the path of the
		/// image for the template selected.
		/// </summary>
		/// <param name="dataItem">BusinessLinkTemplate data item</param>
		/// <returns>string path</returns>
		protected string GetTemplateImagePath(object dataItem )
		{
			return  ((BusinessLinkTemplate)dataItem).ImageUrl;
		}

		/// <summary>
		/// Returns the alt text of the
		/// image for the the template selected.
		/// </summary>
		/// <param name="dataItem">BusinessLinkTemplate data item</param>
		/// <returns>string alt text</returns>
		protected string GetTemplateImageAlt(object dataItem )
		{
			return GetResource(((BusinessLinkTemplate)dataItem).ResourceId + ".AltText");
		}

		/// <summary>
		/// Method returns the descriptive name
		/// of the template selected.
		/// </summary>
		/// <param name="dataItem">BusinessLinkTemplate data item</param>
		/// <returns>string description</returns>
		protected string GetTemplateDescription(object dataItem )
		{
			return GetResource(((BusinessLinkTemplate)dataItem).ResourceId + ".Text");
		}

		/// <summary>
		/// Method returns the id of the
		///	template selected.
		/// </summary>
		/// <param name="dataItem">BusinessLinkTemplate data item</param>
		/// <returns>int id</returns>
		protected int GetTemplateIdValue(object dataItem )
		{
			return ((BusinessLinkTemplate)dataItem).Id;
		}

		#region Public properties

		/// <summary>
		/// Read write property
		/// Gets/Sets the currently selected template  
		/// </summary>
		public BusinessLinkTemplate SelectedTemplate
		{
			get { return selectedTemplate; }
			set { selectedTemplate = value; }
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
			this.PreRender += new EventHandler(OnPreRender);
			dataListBusinessTemplates.ItemDataBound += new DataListItemEventHandler(dataListBusinessTemplates_ItemDataBound);
			dataListBusinessTemplates.ItemCreated += new DataListItemEventHandler(dataListBusinessTemplates_ItemCreated);
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Item data bound method of data list
		/// Will 'check' the correct radio button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataListBusinessTemplates_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (((BusinessLinkTemplate)e.Item.DataItem).Id  == (selectedTemplate.Id))
			{
				ScriptableGroupRadioButton s = (ScriptableGroupRadioButton)e.Item.FindControl("templateSelectRadioButton");
				s.Checked = true;
				return;
			}
		}

		/// <summary>
		/// Item created method of data list
		/// Wires up changed event of the radio buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataListBusinessTemplates_ItemCreated(object sender, DataListItemEventArgs e)
		{
			ScriptableGroupRadioButton s = (ScriptableGroupRadioButton)e.Item.FindControl("templateSelectRadioButton");
			s.CheckedChanged += new EventHandler(OnTemplateSelectionChanged);
		}

		/// <summary>
		/// OnTemplateSelectionChanged method fires
		/// when the radio button selection changes
		/// Sets the internal selectedTemplate property
		/// with the new template selected and raises
		/// public event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTemplateSelectionChanged(object sender, EventArgs e)
		{
			ScriptableGroupRadioButton s = sender as ScriptableGroupRadioButton;
			
			// Update selectedtemplate value
			selectedTemplate = linkCatalogue.Get(Convert.ToInt32(s.GetValue, TDCultureInfo.CurrentUICulture.NumberFormat));

			// Raise public event
			if (TemplateSelectionChanged != null)
			{
				TemplateSelectionChanged(this,e);
			}
		}

		#endregion
	}
}
