// *********************************************** 
// NAME                 : TestHelper.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 14/06/2010 
// DESCRIPTION  	    : Helper class allowing access to private 
//                        members of a class to test private members
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestHelper.cs-arc  $ 
//
//   Rev 1.0   Jun 14 2010 12:08:26   apatel
//Initial revision.
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// TestHelper class
    /// </summary>
    public class TestHelper
    {
        private TestHelper()
		{

		}

		#region Run Method

		/// <summary>
		/// Runs a method on a type, given its parameters. This is useful for
		/// calling static methods including private static methods.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="strMethod"></param>
		/// <param name="aobjParams"></param>
		/// <returns>The return value of the called method.</returns>
		public static object RunStaticMethod(System.Type t, string strMethod, object [] aobjParams) 
		{
			BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, null, aobjParams, eFlags);
		} 

        /// <summary>
        ///  Runs a method on a type, given its parameters. This is useful for
        ///  calling private instance methods.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="strMethod"></param>
        /// <param name="objInstance"></param>
        /// <param name="aobjParams"></param>
        /// <returns></returns>
		public static object RunInstanceMethod(System.Type t, string strMethod, object objInstance, object [] aobjParams) 
		{
			BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, objInstance, aobjParams, eFlags);
		}

        /// <summary>
        /// Runs method on a class using Reflection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="strMethod"></param>
        /// <param name="objInstance"></param>
        /// <param name="aobjParams"></param>
        /// <param name="eFlags"></param>
        /// <returns></returns>
		private static object RunMethod(System.Type t, string strMethod, object objInstance, object [] aobjParams, BindingFlags eFlags) 
		{
			MethodInfo m;
			try 
			{
                
                MemberInfo[] mInfo = t.GetMember(strMethod, eFlags);
                if(mInfo.Length  > 1)
                {
                    List<Type> tlist = new List<Type>();
                    foreach (object obj in aobjParams)
                    {
                        tlist.Add(obj.GetType());
                    }

                    m = t.GetMethod(strMethod, eFlags, null, tlist.ToArray(), null);

                }
                else
                {
				    m = t.GetMethod(strMethod, eFlags);
                }
                if (m == null)
				{
					throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t.ToString() + "'.");
				}

				object objRet = m.Invoke(objInstance, aobjParams);
				return objRet;
			}
			catch
			{
				throw;
			}
		} 


        /// <summary>
        /// Gets private field value for instance of a class using Reflection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="objInstance"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetFieldValue(System.Type t, object objInstance, string fieldName)
        {
            BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreReturn | BindingFlags.GetField;

            try 
			{
                
                FieldInfo fInfo = t.GetField(fieldName, eFlags);
                
				object objRet = fInfo.GetValue(objInstance);
				return objRet;
			}
			catch
			{
				throw;
			}
		}

        /// <summary>
        /// Sets private field value for instance of a class using Reflection
        /// </summary>
        /// <param name="t"></param>
        /// <param name="objInstance"></param>
        /// <param name="fieldName"></param>
        /// <param name="objValue"></param>
        public static void SetFieldValue(System.Type t, object objInstance, string fieldName, object objValue)
        {
            BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreReturn | BindingFlags.GetField;

            try
            {

                FieldInfo fInfo = t.GetField(fieldName, eFlags);
                fInfo.SetValue(objInstance, objValue);
            }
            catch
            {
                throw;
            }
        }
        
		#endregion
    }
}
