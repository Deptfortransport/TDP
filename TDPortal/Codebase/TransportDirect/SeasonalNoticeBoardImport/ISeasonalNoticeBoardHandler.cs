using System;
using System.Data;

namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{
	/// <summary>
	/// Summary description for Interface SeasonalNoticeBoardHandler.
	/// </summary>
	public interface ISeasonalNoticeBoardHandler
	{
		
		DataTable GetData();
		bool DataAvailable {get;}
		SeasonalNoticeBoardHandler Copy();

	}
}
