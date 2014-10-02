// *************************************************************** 
// NAME                 : RetailBusinessObjectPropertyValidator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 15/10/2003 
// DESCRIPTION  : Validates Retail Business Object properties.
// ***************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RetailBusinessObjectPropertyValidator.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 17:13:48   mmodi
//Updated to validate ZPBO properties
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:20   mturner
//Initial revision.
//
//   Rev 1.7   Feb 16 2005 16:32:20   jmorrissey
//Fixed CLS Compliant warnings
//
//   Rev 1.6   Feb 10 2005 17:38:18   RScott
//Updated to include Reservation and Supplement Business Objects (RVBO, SBO)
//
//   Rev 1.5   Oct 29 2003 11:57:08   geaton
//Added check for maximum values.
//
//   Rev 1.4   Oct 28 2003 20:05:04   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.3   Oct 21 2003 15:22:56   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.2   Oct 16 2003 10:42:46   geaton
//Added method to validate object id.
//
//   Rev 1.1   Oct 15 2003 21:36:08   geaton
//Corrected parameter order for message.
//
//   Rev 1.0   Oct 15 2003 14:40:38   geaton
//Initial Revision

using System;
using System.Collections;

using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Utility class used to validate logging specific properties.
	/// Derives from PropertyValidator which provides general
	/// validation methods.
	/// </summary>
	public class RetailBusinessObjectPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Length of valid interface version property values.
		/// </summary>
		private const int InterfaceVersionLength = 4;

		/// <summary>
		/// Constructor used to create class based on properties.
		/// </summary>
		/// <param name="properties">Properties that are used to perform validation against.</param>
		public RetailBusinessObjectPropertyValidator(IPropertyProvider properties) :
		base(properties)
		{}

		/// <summary>
		/// Used to validate a property with a given <c>key</c>.
		/// </summary>
		/// <param name="key">Key of property to validate.</param>
		/// <param name="errors">Holds validation errors if any.</param>
		/// <returns><c>true</c> if property is valid otherwise <c>false</c>.</returns>
		override public bool ValidateProperty(string key, ArrayList errors)
		{
			// A validation method is defined for every key that may be passed.

			if (key == Keys.FBOInterfaceVersion)
				return ValidateInterfaceVersion(errors, Keys.FBOInterfaceVersion);
			else if (key == Keys.LBOInterfaceVersion)
				return ValidateInterfaceVersion(errors, Keys.LBOInterfaceVersion);
			else if (key == Keys.RBOInterfaceVersion)
				return ValidateInterfaceVersion(errors, Keys.RBOInterfaceVersion);
			else if (key == Keys.RVBOInterfaceVersion)
				return ValidateInterfaceVersion(errors, Keys.RVBOInterfaceVersion);
			else if (key == Keys.SBOInterfaceVersion)
				return ValidateInterfaceVersion(errors, Keys.SBOInterfaceVersion);
            else if (key == Keys.ZPBOInterfaceVersion)
                return ValidateInterfaceVersion(errors, Keys.ZPBOInterfaceVersion);

			else if (key == Keys.FBOPoolSize)	
				return ValidatePoolSize(errors, Keys.FBOPoolSize, FBOPool.MaximumPoolSize);
			else if (key == Keys.LBOPoolSize)
				return ValidatePoolSize(errors, Keys.LBOPoolSize, LBOPool.MaximumPoolSize);
			else if (key == Keys.RBOPoolSize)
				return ValidatePoolSize(errors, Keys.RBOPoolSize, RBOPool.MaximumPoolSize);
			else if (key == Keys.RVBOPoolSize)
				return ValidatePoolSize(errors, Keys.RVBOPoolSize, RVBOPool.MaximumPoolSize);
			else if (key == Keys.SBOPoolSize)
				return ValidatePoolSize(errors, Keys.SBOPoolSize, SBOPool.MaximumPoolSize);
            else if (key == Keys.ZPBOPoolSize)
                return ValidatePoolSize(errors, Keys.ZPBOPoolSize, ZPBOPool.MaximumPoolSize);

			else if (key == Keys.FBOObjectId)
				return ValidateObjectId(errors, Keys.FBOObjectId);
			else if (key == Keys.LBOObjectId)
				return ValidateObjectId(errors, Keys.LBOObjectId);
			else if (key == Keys.RBOObjectId)
				return ValidateObjectId(errors, Keys.RBOObjectId);
			else if (key == Keys.RVBOObjectId)
				return ValidateObjectId(errors, Keys.RVBOObjectId);
			else if (key == Keys.SBOObjectId)
				return ValidateObjectId(errors, Keys.SBOObjectId);
            else if (key == Keys.ZPBOObjectId)
                return ValidateObjectId(errors, Keys.ZPBOObjectId);

			else if (key == Keys.FBOTimeoutDuration)
				return ValidateTimeoutDuration(errors, Keys.FBOTimeoutDuration);
			else if (key == Keys.LBOTimeoutDuration)
				return ValidateTimeoutDuration(errors, Keys.LBOTimeoutDuration);
			else if (key == Keys.RBOTimeoutDuration)
				return ValidateTimeoutDuration(errors, Keys.RBOTimeoutDuration);
			else if (key == Keys.RVBOTimeoutDuration)
				return ValidateTimeoutDuration(errors, Keys.RVBOTimeoutDuration);
			else if (key == Keys.SBOTimeoutDuration)
				return ValidateTimeoutDuration(errors, Keys.SBOTimeoutDuration);
            else if (key == Keys.ZPBOTimeoutDuration)
                return ValidateTimeoutDuration(errors, Keys.ZPBOTimeoutDuration);

			else if (key == Keys.FBOTimeoutCheckFrequency)
				return ValidateTimeoutCheckFrequency(errors, Keys.FBOTimeoutCheckFrequency);
			else if (key == Keys.LBOTimeoutCheckFrequency)
				return ValidateTimeoutCheckFrequency(errors, Keys.LBOTimeoutCheckFrequency);
			else if (key == Keys.RBOTimeoutCheckFrequency)
				return ValidateTimeoutCheckFrequency(errors, Keys.RBOTimeoutCheckFrequency);
			else if (key == Keys.RVBOTimeoutCheckFrequency)
				return ValidateTimeoutCheckFrequency(errors, Keys.RVBOTimeoutCheckFrequency);
			else if (key == Keys.SBOTimeoutCheckFrequency)
				return ValidateTimeoutCheckFrequency(errors, Keys.SBOTimeoutCheckFrequency);
            else if (key == Keys.ZPBOTimeoutCheckFrequency)
                return ValidateTimeoutCheckFrequency(errors, Keys.ZPBOTimeoutCheckFrequency);

			else if (key == Keys.FBOTimeoutChecking)
				return ValidateTimeoutChecking(errors, Keys.FBOTimeoutChecking);
			else if (key == Keys.LBOTimeoutChecking)
				return ValidateTimeoutChecking(errors, Keys.LBOTimeoutChecking);
			else if (key == Keys.RBOTimeoutChecking)
				return ValidateTimeoutChecking(errors, Keys.RBOTimeoutChecking);
			else if (key == Keys.RVBOTimeoutChecking)
				return ValidateTimeoutChecking(errors, Keys.RVBOTimeoutChecking);
			else if (key == Keys.SBOTimeoutChecking)
				return ValidateTimeoutChecking(errors, Keys.SBOTimeoutChecking);
            else if (key == Keys.ZPBOTimeoutChecking)
                return ValidateTimeoutChecking(errors, Keys.ZPBOTimeoutChecking);

			else if (key == Keys.FBOMinimumPoolSize)
				return ValidateMinimumPoolSize(errors, Keys.FBOMinimumPoolSize, Keys.FBOPoolSize);
			else if (key == Keys.LBOMinimumPoolSize)
				return ValidateMinimumPoolSize(errors, Keys.LBOMinimumPoolSize, Keys.LBOPoolSize);
			else if (key == Keys.RBOMinimumPoolSize)
				return ValidateMinimumPoolSize(errors, Keys.RBOMinimumPoolSize, Keys.RBOPoolSize);
			else if (key == Keys.RVBOMinimumPoolSize)
				return ValidateMinimumPoolSize(errors, Keys.RVBOMinimumPoolSize, Keys.RVBOPoolSize);
			else if (key == Keys.SBOMinimumPoolSize)
				return ValidateMinimumPoolSize(errors, Keys.SBOMinimumPoolSize, Keys.SBOPoolSize);
            else if (key == Keys.ZPBOMinimumPoolSize)
                return ValidateMinimumPoolSize(errors, Keys.ZPBOMinimumPoolSize, Keys.ZPBOPoolSize);

			else if (key == Keys.HousekeepingCheckFrequency)
				return ValidateHousekeepingCheckFrequency(errors);

			else
			{
				errors.Add(String.Format(Messages.Property_ValidatorKeyBad, key));
			}

			return false;
		}

		private bool ValidateInterfaceVersion(ArrayList errors, string key)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{
				ValidateLength(key, InterfaceVersionLength, InterfaceVersionLength, errors);
			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateObjectId(ArrayList errors, string key)
		{
			int errorsBefore = errors.Count;

			ValidateExistence(key, Optionality.Mandatory, errors);
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidatePoolSize(ArrayList errors, string key, int maximumSize)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{
				if ( (int.Parse(Properties.Current[key]) < 0) || 
					 (int.Parse(Properties.Current[key]) > maximumSize) )
				{
					errors.Add(String.Format(Messages.Property_PoolSizeInvalid,
											 Properties.Current[key],  
											 key,
											 maximumSize)); 
				}
				
			}
			return (errorsBefore == errors.Count);
		}

		private bool ValidateTimeoutDuration(ArrayList errors, string key)
		{
			int errorsBefore = errors.Count;
			
			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{
				if ( (int.Parse(Properties.Current[key]) < 0) || 
					 (int.Parse(Properties.Current[key]) > Int32.MaxValue) )
					errors.Add(String.Format(Messages.Property_TimeoutDurationInvalid,
							   Properties.Current[key],  
							   key));
			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateTimeoutCheckFrequency(ArrayList errors, string key)
		{
			int errorsBefore = errors.Count;
			
			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{
				if ( (int.Parse(Properties.Current[key]) < 0) ||
					 (int.Parse(Properties.Current[key]) > Int32.MaxValue) )
					errors.Add(String.Format(Messages.Property_TimeoutCheckFrequecyInvalid,
							   Properties.Current[key],  
							   key)); 
			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateTimeoutChecking(ArrayList errors, string key)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{
				ValidateEnumProperty(key,
									 typeof(TimeoutCheckingSwitch),
									 Optionality.Mandatory, errors);

			}

			return (errorsBefore == errors.Count);
		}

		private bool ValidateMinimumPoolSize(ArrayList errors, string key, string keyPoolSizeNum)
		{
			int errorsBefore = errors.Count;

            if (ValidateExistence(keyPoolSizeNum, Optionality.Mandatory, errors))
            {
                int poolSizeNum = int.Parse(Properties.Current[keyPoolSizeNum]);

                if (ValidateExistence(key, Optionality.Mandatory, errors))
                {

                    if ((int.Parse(Properties.Current[key]) <= 0) ||
                         (int.Parse(Properties.Current[key]) > poolSizeNum))
                    {
                        errors.Add(String.Format(Messages.Property_MinimumPoolSizeInvalid,
                                   Properties.Current[key],
                                   key,
                                   poolSizeNum));
                    }
                }

            }

			return (errorsBefore == errors.Count);
		}

		private bool ValidateHousekeepingCheckFrequency(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(Keys.HousekeepingCheckFrequency, Optionality.Mandatory, errors))
			{
				if ( (int.Parse(Properties.Current[Keys.HousekeepingCheckFrequency]) <= 0) ||
					 (int.Parse(Properties.Current[Keys.HousekeepingCheckFrequency]) > Int32.MaxValue) ) 
				{
					errors.Add(String.Format(Messages.Property_HousekeepingCheckFrequencyInvalid,
											 Properties.Current[Keys.HousekeepingCheckFrequency],  
											 Keys.HousekeepingCheckFrequency)); 
				}
				
			}

			return (errorsBefore == errors.Count);
		}
	}
}
