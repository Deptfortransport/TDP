//********************************************************************************
//NAME         : BusinessObjectInput.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/10/2003
//DESCRIPTION  : Provides a class to wrap input data used to call
//               BusinessObject class instances.
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/BusinessObjectInput.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.3   Oct 28 2003 20:04:48   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.2   Oct 14 2003 12:27:48   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.1   Oct 13 2003 15:26:14   CHosegood
//Initial Version
//
//   Rev 1.0   Oct 08 2003 11:46:26   CHosegood
//Initial Revision

using System;
using System.Diagnostics;
using System.Collections;
using System.Text;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Wraps input data to BusinessObject calls.
    /// </summary>
    [Serializable]
    public class BusinessObjectInput
    {
        private String sectionID = "STDIN   ";
        private String objectID = "  ";
        private String functionID = "  ";
        private String instanceID = "        ";
        private String interfaceVersion = "    ";
        private String errorCode = "    ";

        private ArrayList inputParameters;
        private ArrayList outputParameters;

        /// <summary>
        /// Class constructor.
        /// </summary>
        public BusinessObjectInput( String objectID,
									String functionID,
									String interfaceVersion ) 
        {
            inputParameters = new ArrayList();
            outputParameters = new ArrayList();
            this.objectID = objectID;
            this.functionID = functionID;
            this.interfaceVersion = interfaceVersion;
        }

        /// <summary>
        /// Default class constuctor.
        /// </summary>
        public BusinessObjectInput() 
        {
            inputParameters = new ArrayList();
            outputParameters = new ArrayList();
        }

        /// <summary>
        /// Constant used for validation: Value = "STDIN   "
        /// Required to be 8 characters
        /// </summary>
        public String SectionID 
        {
            get { return this.sectionID; }
            set 
            {
                if (value.Length > 8)
                    throw new TDException("Section Identifier cannot have more than 8 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.sectionID = value.PadRight(8,' '); 
            }
        }

        /// <summary>
        /// Gets and sets the unique ID of the object.
        /// The comms business object uses this to direct the call to the correct object
        /// Required to be 2 characters
        /// </summary>
        public String ObjectID
        {
            get {return this.objectID;}
            set 
            {
                if (value.Length > 2)
                    throw new TDException("Object Identifier cannot have more than 2 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.objectID = value.PadRight(2,' '); 
            }
        }

        /// <summary>
        /// Gets and sets the Function ID within the object.
        /// The object uses this to direct the call to the correct function.
        /// 3 codes are reserved “IN” for Initialise, “GS” for State and “TE” for Terminate.
        /// Required to be 2 characters
        /// </summary>
        public String FunctionID
        {
            get {return this.functionID;}
            set 
            {
                if (value.Length > 2)
                    throw new TDException("Function Identifier cannot have more than 2 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.functionID = value.PadRight(2,' '); 
            }
        }

        /// <summary>
        /// Gets and sets the instance identifier.
        /// Applications could use this field for other purposes
        /// Note that Lennon Fares Server uses this field as the date the download file was created for.
        /// Required to be 8 characters
        /// </summary>
        public String InstanceID
        {
            get {return this.instanceID;}
            set 
            {
                if (value.Length > 8)
                    throw new TDException("Instance Identifier cannot have more than 8 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.instanceID = value.PadRight(8,' '); 
            }
        }

        /// <summary>
        /// Gets and sets the version of the object’s interface expected by the application.
        /// The first two characters are the Major Version.
        /// This will only change when the object is no longer backward compatible with the application.
        /// The last two characters are the Minor Version of the interface.
        /// This will change whenever a backwards-compatible change is introduced to the object.
        /// Required to be 4 characters
        /// </summary>
        public String InterfaceVersion
        {
            get {return this.interfaceVersion;}
            set 
            {
                if (value.Length > 4)
                    throw new TDException("Interface Version cannot have more than 4 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.interfaceVersion = value.PadRight(4,' '); 
            }
        }

        /// <summary>
        /// Gets and sets the error code used by the Get_State function to return error text
        /// Required to be 4 characters
        /// </summary>
        public String ErrorCode
        {
            get {return this.errorCode;}
            set 
            {
                if (value.Length > 4)
                    throw new TDException("Error Code cannot have more than 4 characters", false, TDExceptionIdentifier.PRHInvalidInputHeader );
                this.errorCode = value.PadRight(4,' '); 
            }
        }

        /// <summary>
        /// Gets the input body.
        /// Converts the value of this instance to its equivalent string representation
        /// SectionID ObjectID FunctionID InstanceID InterfaceVersion ErrorCode Parameters
        /// 6         2        2          8          4                4         6*15
        /// </summary>
        public string InputBody 
        {
            get 
            {
                StringBuilder inputBody = new StringBuilder();
                for ( int i = 0; i < inputParameters.Count; i++) 
                {
                    inputBody.Append( ((HeaderInputParameter) inputParameters[i]).Parameter );
                }
                return inputBody.ToString();
            }
        }

        /// <summary>
        /// Gets the output length.
        /// </summary>
        public int OutputLength
        {
            get
            {
                int length = 0;
                for ( int i = 0; i < outputParameters.Count; i++)
                {
                    length += ((HeaderOutputParameter) outputParameters[i]).Length;
                }
                return length;
            }
        }

		/// <summary>
		/// Adds the output parameter.
		/// </summary>
		/// <param name="param">Output parameter to add.</param>
		/// <param name="index">Index to add parameter at.</param>
        public void AddOutputParameter( HeaderOutputParameter param, int index ) 
        {
            this.outputParameters.Insert( index, param );
        }

		/// <summary>
		/// Adds the input parameter.
		/// </summary>
		/// <param name="param">Output parameter to add.</param>
		/// <param name="index">Index to add parameter at.</param>
        public void AddInputParameter( HeaderInputParameter param, int index ) 
        {
            this.inputParameters.Insert( index, param );
        }

		/// <summary>
		/// Overrides ToString.
		/// </summary>
		/// <returns>Business Object Input class as a string.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sectionID);
            sb.Append(objectID);
            sb.Append(functionID);
            sb.Append(instanceID);
            sb.Append(interfaceVersion);
            sb.Append(errorCode);
            for( int i=0;i< inputParameters.Count;i++)
            {
                sb.Append( ((HeaderInputParameter) inputParameters[i]).InputType);
                sb.Append( ((HeaderInputParameter) inputParameters[i]).Length.ToString().PadLeft(5,'0'));
            }
            for( int i=0;i< outputParameters.Count;i++)
            {
                sb.Append( ((HeaderOutputParameter) outputParameters[i]).InputType);
                sb.Append( ((HeaderOutputParameter) outputParameters[i]).Length.ToString().PadLeft(5,'0'));
            }

            return sb.ToString().PadRight(168,' ');
        }
    }
}
