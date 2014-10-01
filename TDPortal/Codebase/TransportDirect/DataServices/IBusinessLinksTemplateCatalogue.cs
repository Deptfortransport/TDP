// *********************************************************************** 
// NAME                 :	IBusinessLinksTemplateCatalogue.cs
// AUTHOR               :	Tolu Olomolaiye
// DATE CREATED         :	22 Nov 2005 
// DESCRIPTION			:	Interface for BusinessLinksTemplateCatalogue
// ************************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/IBusinessLinksTemplateCatalogue.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:48   mturner
//Initial revision.
//
//   Rev 1.1   Dec 16 2005 12:09:20   jbroome
//Added GetDefault method
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.0   Nov 22 2005 11:23:10   tolomolaiye
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Interface for BusinessLinksTemplateCatalogue
	/// </summary>
	public interface IBusinessLinksTemplateCatalogue
	{
		BusinessLinkTemplate[] GetAll();

		BusinessLinkTemplate Get(int templateID);

		BusinessLinkTemplate GetDefault();
	}
}
