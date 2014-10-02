//**************************************************************
//NAME			: HelpLabelControlConverter.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 28/08/2003
//DESCRIPTION	: Custom Converter for HelpLabel controls
//**************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HelpLabelControlConverter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:52   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:25:22   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Sep 30 2003 15:22:54   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.1   Sep 04 2003 17:39:48   JMorrissey
//Now uses control.GetType() to find HelpLabelControl in GetControls method
//
//   Rev 1.0   Sep 04 2003 15:00:52   JMorrissey
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;
using System.ComponentModel;
using System.Collections;
using System.Web.UI;
using TransportDirect.UserPortal.Web.Controls;

//some comments to be added to this file!!

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Type converter for use with the HelpLabel
	/// </summary>
	public class HelpLabelControlConverter : StringConverter
	{
		public HelpLabelControlConverter() : base()
		{
		}

		/// <summary>
		/// returns an array of the names of all HelpLabelControls found
		/// </summary>
		/// <param name="container"></param>
		/// <returns></returns>
		private object[] GetControls(IContainer container)
		{
			ComponentCollection collection1;
			ArrayList list1;
			IComponent component1;
			Control control1;
			// No validation done in this method - code
			// saved if there is a need to implement this.
			// ValidationPropertyAttribute attribute1;
			IEnumerator enumerator1;
			IDisposable disposable1;
			collection1 = container.Components;
			list1 = new ArrayList();
			enumerator1 = collection1.GetEnumerator();

			try
			{
				while (enumerator1.MoveNext())
				{
					component1 = ((IComponent) enumerator1.Current);
					if ((component1 as Control) != null)
					{
						control1 = ((Control) component1);
						if ((control1.ID != null) && (control1.ID.Length != 0))
						{

							//tests to find HelpLabelControls only
							if (control1.GetType().Name == "HelpLabelControl")
							{
								//adds HelpLabelControls found to the list
								HelpLabelControl lab = (HelpLabelControl)control1;
								list1.Add(string.Copy(control1.ID));	
							}
 
						}
 
					}
 
				}
 
			}
			finally
			{
				disposable1 = (enumerator1 as IDisposable);
				if (disposable1 != null)
				{
					disposable1.Dispose();
 
				}
 
			}
			list1.Sort(Comparer.Default);
			return list1.ToArray(); 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object[] array1;
			if ((context == null) || (context.Container == null))
			{
				return null; 
			}
			array1 = this.GetControls(context.Container);
			if (array1 != null)
			{
				return new StandardValuesCollection(array1); 
			}
			return null; 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false; 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; 
		}
	}
}
