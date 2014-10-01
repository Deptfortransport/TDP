// ********************************************************************* 
// NAME                 : TestStubAdditionalData.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of TestStubAdditionalData
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestStubAdditionalData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 16:17:44   mturner
//Initial revision.
//
//   Rev 1.11   Sep 27 2005 16:43:24   kjosling
//Merged with stream 2625. 
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.10   Aug 11 2005 17:35:20   kjosling
//Added more reference data for user control testing
//
//   Rev 1.9.1.0   Aug 10 2005 10:35:14   kjosling
//Added some extra code to load reference data for test cases (TrainTaxi Amendments). 
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.9   Mar 30 2005 09:40:04   rscott
//Updated after FXCop fixes
//
//   Rev 1.8   Jan 12 2005 16:58:08   RScott
//updated to accomodate new method LookupStationNameForCRS()
//
//   Rev 1.7   Jan 11 2005 15:16:50   RScott
//Updated Stub returns to include new method LookupNaptanForCode
//
//   Rev 1.6   May 26 2004 16:18:50   asinclair
//Added Byte column for latest version of dll
//
//   Rev 1.5   Nov 21 2003 16:55:00   acaunt
//TDAdditionalData now returns string.empty is an invalid NaPTAN (null or empty) is provided
//
//   Rev 1.4   Nov 07 2003 16:29:30   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.3   Nov 07 2003 14:06:16   RPhilpott
//Add TrainTaxiLink support.
//
//   Rev 1.2   Nov 05 2003 19:16:16   RPhilpott
//Add CRS and Station Name convenience methods
//
//   Rev 1.1   Oct 19 2003 18:13:12   acaunt
//Updated sample data
//
//   Rev 1.0   Oct 16 2003 20:52:30   acaunt
//Initial Revision

using System;
using System.Data;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Summary description for TDAddtionalData.
	/// </summary>
	[Serializable]
	public class TestStubAdditionalData : IAdditionalData, IServiceFactory
	{

		public TestStubAdditionalData()
		{
		}

		/// <summary>
		/// Dummy implementation of IAdditionalData method
		/// </summary>
		/// <param name="type"></param>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public string LookupFromNaPTAN(LookupType type, String naptan )
		{
			if (naptan == null || naptan.Length == 0)
			{
				return string.Empty;
			}
			if (type == LookupType.CRS_Code) 
			{
				if (naptan == "9100NTPG") 
				{
					return "NOT";
				} 
				else if (naptan == "9100STPX")
				{
					return "STP";
				} 
				else if (naptan == "9100VICTRIC" || naptan == "9100VICTRIE")
				{
					return "VIC";
				} 
				else if (naptan == "9100DOVERP")
				{
					return "DVP";
				} 
				else if (naptan == "9100MNCRPIC")
				{
					return "MAN";
				} 
				else 
				{
					return "ZBB";
				}
			}
			else if (type == LookupType.NLC_Code) 
			{
				if (naptan == "9100NTPG") 
				{
					return "18260";
				} 
				else if (naptan == "9100STPX")
				{
					return "15550";
				} 
				else if (naptan == "9100VICTRIC" || naptan == "9100VICTRIE")
				{
					return "54260";
				} 
				else if (naptan == "9100DOVERP")
				{
					return "50330";
				} 
				else if (naptan == "9100MNCRPIC")
				{
					return "29680";
				} 
				else 
				{
					return "12345";
				}
			}
			else
			{
				return string.Empty;
			}
		}


		/// <summary>
		/// Dummy implementation of IAdditionalData method
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public LookupResult[] LookupFromNaPTAN(String naptan)
		{
			LookupResult[] results = new LookupResult[5];
			results[0] = new LookupResult(LookupType.NLC_Code,"05010");
			results[1] = new LookupResult(LookupType.CRS_Code,"ZBB");
			return results;
		}

		/// <summary>
		/// Dummy implementation of IAdditionalData method
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public string[] LookupNaptanForCode(String code, LookupType type)
		{ 	
			//check code is not null or an empty string
			if (code == null || code.Length == 0)
			{
				string[] results = new string[0];
				return results;
			}
			//return result depending on value of code
			if ((code == "CLJ") && (type == LookupType.CRS_Code))
			{
				string[] results = new string[4];
				results[0] = "9100CLPHMJ2";
				results[1] = "9100CLPHMJC";
				results[2] = "9100CLPHMJM";
				results[3] = "9100CLPHMJW";
				return results;
			}
			else if ((code == "0501") && (type == LookupType.NLC_Code))
			{
				string[] results = new string[1];
				results[0] = "9100BRBCNLT";
				return results;
			}
			else if ((code == "0500") && (type == LookupType.NLC_Code))
			{
				string[] results = new string[0];
				return results;
			}
			else
			{
				string[] results = new string[0];
				return results;
			}
		}

		/// <summary>
		/// Dummy implementation of IAdditionalData method
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public string LookupStationNameForCRS(string code)
		{
			if	(code == "CLJ")
			{
				return "CLAPHAM JUNCTION Station";
			}
			else if (code == "ABA")
			{
				return "ABERDARE Station";
			}
			else
			{
				return string.Empty;
			}
		}



		public string LookupCrsForNaptan(string naptan)
		{
			return LookupFromNaPTAN(LookupType.CRS_Code, naptan); 
		}

		public string LookupNlcForNaptan(string naptan)
		{
			return LookupFromNaPTAN(LookupType.NLC_Code, naptan); 
		}

		public string LookupStationNameForNaptan(string naptan)
		{
			if	(naptan == "9100CREWE")
			{
				return "Crewe";
			}
			else if (naptan == "9100LEEDS")
			{
				return "Leeds";
			}
			else if (naptan == "9100HFX")
			{
				return "Halifax";
			}
			else if (naptan == "9100KNGX")
			{
				return "London Kings Cross";
			}
			else if (naptan == "9100NTWCH")
			{
				return "Nantwich";
			}
			else if (naptan == "9100SHRY")
			{
				return "Shrewsbury";
			}
			else if (naptan == "9100MNCR")
			{
				return "Manchester Piccadilly";
			}
			else if (naptan == "9100FRNGDN")
			{
				return "Farringdon";
			}
			else if (naptan == "9100BRADIN")
			{
				return "Bradford Interchange";
			}
			else
			{
				return string.Empty;
			}
		}

		
		public DataSet RetrieveTrainTaxiInfoForNaptan(string naptan)
		{
			if	(naptan == "RETURNSNULL")
			{
				return null;
			}

			if	(naptan == "EXCEPTION")
			{
				throw new TDException("Test exception", false, TDExceptionIdentifier.Undefined);
			}

			DataSet ds = new DataSet();
			
			DataTable dt = new DataTable("TrainTaxi");

			dt.Columns.Add("PDS Key",		typeof(string));
			dt.Columns.Add("Category",		typeof(string));
			dt.Columns.Add("Category Id",	typeof(string));
			dt.Columns.Add("Type",			typeof(string));
			dt.Columns.Add("Logical Type",	typeof(string));
			dt.Columns.Add("Value",			typeof(string));
			dt.Columns.Add("Byte",			typeof(byte));

			ds.Tables.Add(dt);

			if	(naptan == "NOROWS")
			{
				return ds;
			}

			DataRow dr = null; 
			
			switch (naptan)
			{ 
				case "9100HFX":
					dr = dt.NewRow();
					dr["PDS Key"] = "9100HFX";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "is a minor station with taxis usually available on a rank. Advance booking is not normally necessary or even possible, unless arriving early in the morning or late at night. Operators who may accept bookings include: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);
					
					dr = dt.NewRow();
					dr["PDS Key"] = "9100HFX";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "Fortran Cabbies|01274 576332|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100HFX";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "Ziggys Cabs|01274 343532|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100HFX";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "Boy Racers|01274 123666|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100HFX";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "Hellraiser Rides|01274 726095|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					break;

				case "9100LEEDS":
					dr = dt.NewRow();
					dr["PDS Key"] = "9100LEEDS";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "is a major station with taxis usually available on a rank. Advance booking is not normally necessary or even possible, unless arriving early in the morning or late at night. Operators who may accept bookings include: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);
					
					dr = dt.NewRow();
					dr["PDS Key"] = "9100LEEDS";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "City|01274 726095|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100LEEDS";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100CREWE|CREWE";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100LEEDS";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "ACCESSIBLE";
					dr["Logical Type"] = "ACCESSIBLE";
					dr["Value"] = "Our records show this firm operates wheelchair accessible vehicles. Travellers with impaired mobility are advised to contact the operator to check availablaility and suitability prior to travelling. Tripscopw (08457 58 56 61) offers expert advice and information on overcoming travel difficulties and may be able to offer information about an alternative, should this journey be unsuitable to your needs";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					break;

				case "9100BRADIN":
					dr = dt.NewRow();
					dr["PDS Key"] = "9100BRADIN";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "is a major station with taxis usually available on a rank. Advance booking is not normally necessary or even possible, unless arriving early in the morning or late at night. Operators who may accept bookings include: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);
					
					dr = dt.NewRow();
					dr["PDS Key"] = "9100BRADIN";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "City|01274 726095|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100BRADIN";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "AAA Taxis|01270 123456|Y";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100BRADIN";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "ACCESSIBLE";
					dr["Logical Type"] = "ACCESSIBLE";
					dr["Value"] = "Our records show this firm operates wheelchair accessible vehicles. Travellers with impaired mobility are advised to contact the operator to check availablaility and suitability prior to travelling. Tripscopw (08457 58 56 61) offers expert advice and information on overcoming travel difficulties and may be able to offer information about an alternative, should this journey be unsuitable to your needs";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

				break;
				
				case "9100CREWE":
					dr = dt.NewRow();
					dr["PDS Key"] = "9100CREWE";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "has a taxi rank - or a cab office - within 100 metres and clearly signposted. Taxis usually meet the principal train services. Advance booking is suggested to meet other services. Consider the following local operators and/or alternative stations: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100CREWE";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "AAA Taxis|01270 123456|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100CREWE";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128793";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "BBB Taxis|01270 666666|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100CREWE";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128794";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "ZZZ Taxis|01270 654321|Y";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					break;


				case "9100MNCR":
					dr = dt.NewRow();
					dr["PDS Key"] = "9100MNCR";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128791";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "has a taxi rank - or a cab office - within 100 metres and clearly signposted. Taxis usually meet the principal train services. Advance booking is suggested to meet other services. Consider the following local operators and/or alternative stations: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100MNCR";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "AAA Taxis|0161 123 4567|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100MNCR";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128794";
					dr["Type"] = "TaxiPHOperator";
					dr["Logical Type"] = "TaxiPHOperator";
					dr["Value"] = "ZZZ Taxis|0161 765 4321|";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					break;


				case "9100NTWCH":

					dr = dt.NewRow();
					dr["PDS Key"] = "9100NTWCH";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128791";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "has neither a taxi rank nor a cab office. Advance booking is essential. Consider the following local operators and/or alternative stations: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100NTWCH";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100CREWE|CREWE";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100NTWCH";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128793";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100SHRY|SHREWSBURY";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100NTWCH";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "ACCESSIBLE";
					dr["Logical Type"] = "ACCESSIBLE";
					dr["Value"] = "Our records show this firm operates wheelchair accessible vehicles. Travellers with impaired mobility are advised to contact the operator to check availablaility and suitability prior to travelling. Tripscopw (08457 58 56 61) offers expert advice and information on overcoming travel difficulties and may be able to offer information about an alternative, should this journey be unsuitable to your needs";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					break;


				case "9100SHRY":

					dr = dt.NewRow();
					dr["PDS Key"] = "9100SHRY";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128791";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "has neither a taxi rank nor a cab office. Advance booking is essential. Consider the following local operators and/or alternative stations: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100SHRY";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100CREWE|CREWE";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100SHRY";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "358474";
					dr["Type"] = "ACCESSIBLE";
					dr["Logical Type"] = "ACCESSIBLE";
					dr["Value"] = "Our records show this firm operates wheelchair accessible vehicles. Travellers with impaired mobility are advised to contact the operator to check availablaility and suitability prior to travelling. Tripscopw (08457 58 56 61) offers expert advice and information on overcoming travel difficulties and may be able to offer information about an alternative, should this journey be unsuitable to your needs";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);
					
					break;

				
				case "9100FRNGDN":

					dr = dt.NewRow();
					dr["PDS Key"] = "9100WEM";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128791";
					dr["Type"] = "Comment";
					dr["Logical Type"] = "Text";
					dr["Value"] = "has neither a taxi rank nor a cab office. Advance booking is essential. Consider the following local operators and/or alternative stations: ";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100WEM";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128792";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100CREWE|CREWE";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);

					dr = dt.NewRow();
					dr["PDS Key"] = "9100WEM";
					dr["Category"] = "TrainTaxiLink";
					dr["Category Id"] = "128793";
					dr["Type"] = "GOTO";
					dr["Logical Type"] = "GOTO";
					dr["Value"] = "9100MNCR|Manchester";
					dr["Byte"] = 1;
					dt.Rows.Add(dr);
					
					break;

			}

			return ds;
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the TestStubAdditionalData class.
		/// </summary>
		/// <returns>A new instance of a TestStubAdditionalData.</returns>
		public Object Get()
		{
			return this;
		}
	}
}

