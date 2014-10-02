// *********************************************** 
// NAME			: TransactionInjectorPropertyValidator.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the TransactionInjectorPropertyValidator class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjectorPropertyValidator.cs-arc  $
//
//   Rev 1.1   Mar 16 2009 12:41:40   build
//Manual merge for stream 5215
//
//   Rev 1.0.2.0   Jan 13 2009 11:09:30   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0.1.0   Jan 13 2009 10:57:26   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:14   mturner
//Initial revision.
//
//   Rev 1.8   Jun 21 2004 15:25:18   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.7   Nov 06 2003 17:19:36   geaton
//Refactored following handover from JT to GE.

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using Logging = TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Validates configuration properties of the TransactionInjector component.
	/// </summary>
	public class TransactionInjectorPropertyValidator : PropertyValidator
	{

		private string serviceName;
		/// <summary>
		/// Constructor, passes property object to base constructor.
		/// </summary>
		/// <param name="properties">Properties object.</param>
		public TransactionInjectorPropertyValidator( IPropertyProvider properties , string serviceName) : base( properties )
		{
			this.serviceName = serviceName;
			
		}

		/// <summary>
		/// Validates a given property.
		/// </summary>
		/// <param name="key">Property to validate.</param>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		public override bool ValidateProperty( string key, ArrayList errors )
		{			
			if (key == Keys.TransactionInjectorTransaction)
				return ValidateTransactionInjectorTransaction(errors);	
			else if (key == Keys.TransactionInjectorFrequency)
				return ValidateTransactionInjectorFrequency(errors);
			else if (key == Keys.TransactionInjectorWebService)
				return ValidateTransactionInjectorWebService(errors);
			else if (key == Keys.TransactionInjectorTemplateFileDirectory)
				return ValidateTransactionInjectorTemplateFileDirectory(errors);
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPTransactionInjectorUnknownPropertyKey);
			}						
		}

		/// <summary>
		/// Validates Transaction key and it's connected properties.
		/// </summary>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		private bool ValidateTransactionInjectorTransaction( ArrayList errors )
		{
			int errorsBefore = errors.Count;	

			string transactionInjectorTransaction = string.Format(Keys.TransactionInjectorTransactionType, serviceName);
			string[] idList = properties[transactionInjectorTransaction ].Split(' ');												

			if( !(idList.Length == 1 && idList[0].Length == 0) )
			{
				string transClassKey = null;
				string transPathKey  = null;			
			
				foreach( string id in idList )
				{
					if( id != " " )
					{
						transClassKey = string.Format( Keys.TransactionInjectorTransactionClass, id );					
						transPathKey  = string.Format( Keys.TransactionInjectorTransactionPath,  id );					
						
						if( ValidateExistence( transClassKey, Optionality.Mandatory, errors ) && 
							ValidateExistence( transPathKey,  Optionality.Mandatory, errors ) )  
						{
							ValidateTransactionInjectorTransactionClass(transClassKey, errors);
							ValidateTransactionInjectorTransactionPath(transPathKey, transClassKey, errors); 
						}
					}
				}
			}
			else
			{
				errors.Add(String.Format(Messages.Validation_InvalidTransactions, properties[Keys.TransactionInjectorTransaction], Keys.TransactionInjectorTransaction)); 
			}

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Validates the path property.
		/// </summary>
		/// <param name="transPathKey">Path key to validate.</param>
		/// <param name="transClassKey">Class key relating to path key.</param>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		private bool ValidateTransactionInjectorTransactionPath( string transPathKey, string transClassKey, ArrayList errors )
		{
			int errorsBefore = errors.Count;		

			string transPath = properties[ transPathKey ];

			if( !Directory.Exists( transPath ) )
			{
				errors.Add( String.Format(	TransportDirect.Common.Messages.PropertyValueBad,
											transPathKey, transPath, "Path does not exist." ));
			}
			else
			{
				string error = null;

				try
				{
					string[] fileList	= Directory.GetFiles( transPath );						

					if (fileList.Length == 0)
						error = "No data files in directory. One or more transaction data files must exist.";

					foreach( string filePath in fileList )
					{								
						string fileName  = Path.GetFileNameWithoutExtension( filePath );

						string className = properties[ transClassKey ];	
						
						if( fileName.StartsWith("SLA") || fileName.StartsWith("KPI") )
						{
							string fileExtension = Path.GetExtension( filePath );
							
							if( fileExtension.ToLower().EndsWith( ".xml" ) )
							{
								string fileNumber = fileName.Substring(3);
								
								try
								{
									int.Parse( fileNumber.Substring( 0, 2 ) );
								}
								catch
								{
									error = "File name \"" + fileName + "\" not in valid format.";											
								}
							}
							else 
							{
								error = "File name \"" + fileName + "\" is not a \".xml\" file.";																			
							}
						}
						else
						{
							error = "File name \"" + fileName + "\" is not a recognised SLA or KPI file.";		
						}
										
					}
				}
				catch( Exception e )
				{
					error = e.Message;				
				}

				if( error != null )
					errors.Add( String.Format(	TransportDirect.Common.Messages.PropertyValueBad,
												transPathKey, transPath, error ));										
			}			
			
			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Validates the class property.
		/// </summary>
		/// <param name="transPathKey">Class key to validate.</param>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		private bool ValidateTransactionInjectorTransactionClass( string transClassKey, ArrayList errors )
		{
			int errorsBefore = errors.Count;				

			Assembly assembly = Assembly.GetExecutingAssembly();			

			string transClass	 = properties[ transClassKey ];
			string thisNamespace = this.GetType().Namespace;
						
			Type type = assembly.GetType( thisNamespace + "." + transClass );	
		
			if( type == null )
			{					
				errors.Add( String.Format(	TransportDirect.Common.Messages.PropertyValueBad,
							transClassKey, 
							transClass,
							"Class does not exist in this assembly." ));
			}

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Validates the frequency property.
		/// </summary>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		private bool ValidateTransactionInjectorFrequency(ArrayList errors)
		{
			int errorsBefore = errors.Count;	
			
			try
			{
				int frequency = int.Parse( properties[ Keys.TransactionInjectorFrequency ] );

				if( frequency <= 0 )					
				{
					errors.Add( String.Format(	TransportDirect.Common.Messages.PropertyValueBad,
								Keys.TransactionInjectorFrequency, 
								properties[ Keys.TransactionInjectorFrequency ],
								"Frequency value must be greater than zero." ));
				}
			}
			catch( Exception e )
			{
				errors.Add( String.Format(	TransportDirect.Common.Messages.PropertyValueBad,
							Keys.TransactionInjectorFrequency, 
							properties[ Keys.TransactionInjectorFrequency ],
							e.Message ));				
			}

			return (errorsBefore == errors.Count);
		}

		private bool ValidateTransactionInjectorWebService(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(Keys.TransactionInjectorWebService, Optionality.Mandatory, errors))
			{

				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride(properties[Keys.TransactionInjectorWebService]);

				try
				{
					if (!webservice.TestActive())
						errors.Add(String.Format(Messages.Validation_WebServiceUnavailable, "Test method returned false."));
				}
				catch (Exception exception)
				{
					errors.Add(String.Format(Messages.Validation_WebServiceUnavailable, exception.Message));
				}
			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateTransactionInjectorTemplateFileDirectory(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(Keys.TransactionInjectorTemplateFileDirectory, Optionality.Mandatory, errors))
			{
				if (!Directory.Exists(properties[Keys.TransactionInjectorTemplateFileDirectory]))
					errors.Add(String.Format(Messages.Validation_InvalidTemplateDir, properties[Keys.TransactionInjectorTemplateFileDirectory], Keys.TransactionInjectorTemplateFileDirectory));
			}
			
			return (errorsBefore == errors.Count);
		}

	}
}
