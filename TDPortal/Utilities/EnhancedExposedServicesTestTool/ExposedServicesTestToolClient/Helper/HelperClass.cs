// ***********************************************************************************
// NAME 		: HelperClass
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: Helperclass for the testtool
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Helper/HelperClass.cs-arc  $
//
//   Rev 1.1   Aug 18 2009 15:27:12   mmodi
//Corrected warning
//
//   Rev 1.0   Nov 08 2007 12:49:14   mturner
//Initial revision.
//
//   Rev 1.3   Feb 07 2006 10:50:12   mdambrine
//added pvcs log

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Threading;


namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// 
	/// </summary>
	public class HelperClass
	{	
	
		#region properties	
		/// <summary>
		/// Read-only property return the currentculture
		/// </summary>
		public static IFormatProvider Provider
		{
			get{ return Thread.CurrentThread.CurrentCulture;}			
		}
		#endregion

		/// <summary>
		/// Reads an XML file from a filepath
		/// </summary>
		/// <param name="fileFullPathName">physical path to the xml file</param>
		/// <returns>a string with the XML</returns>
		public static string ReadXmlFromFile(string fileFullPathName)
		{										
			FileStream fs = new FileStream(fileFullPathName, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);
							
			char[] buffer = new char[(int)fs.Length];
			sr.ReadBlock(buffer, 0, (int)fs.Length);
			sr.Close();
							
			return new string(buffer);			
		}

		/// <summary>
		/// This method will browse through an XML message, look for an element with the 
		/// same name as the parameter send through. If the element name is found and has child elements 
		/// then the method should return all its child elements innertext as an objectarray. If the method does 
		/// not have any child elements return the innertext of that element.
		/// </summary>
		/// <param name="soapMessage">The Xml message</param>
		/// <param name="elementName">the element to look for</param>
		/// <param name="allOuterXml">returns outerxml for all elements</param>
		/// <param name="nameSpace">eventual namespace that might be in the xml document</param>
		/// <returns></returns>
		public static ArrayList GetParameters(string soapMessage, 
										      string elementName,
											  bool allOuterXml,
											  string nameSpace)
		{					
			XmlDocument xmlDocument = new XmlDocument();
			ArrayList parameters = new ArrayList();
			int count = 0; 		

			//load the xml into the xmldocument object
			xmlDocument.PreserveWhitespace = false;
			xmlDocument.LoadXml(soapMessage);
			
			//get all the nodes below the elementname
			XmlNodeList nodeList = xmlDocument.SelectNodes("//" + elementName);

			if (nodeList.Count == 0 )
			{
				XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				namespaceManager.AddNamespace("pf", nameSpace);
				nodeList = xmlDocument.SelectNodes("//pf:" + elementName, namespaceManager);
			}

			foreach(XmlNode node in nodeList)
			{	
				//Only return one element when there are no childnodes
				if (node.ChildNodes.Count <= 1 && node.FirstChild.HasChildNodes == false)
				{
					object[] innerParameters = new object[1];

					if (!allOuterXml)
						innerParameters[0] = node.InnerText;
					else
						innerParameters[0] = node.OuterXml;

					parameters.Add(innerParameters);
				}
				//return all the childnodes in an parameterarray of objects
				else
				{		
					object[] innerParameters = new object[node.ChildNodes.Count];
					count = 0;

					foreach(XmlNode childnode in node.ChildNodes)
					{		
						if (childnode.ChildNodes.Count == 0 || (childnode.ChildNodes.Count == 1 && childnode.FirstChild.HasChildNodes == false))
						{
							if (!allOuterXml)
								innerParameters[count] = childnode.InnerText;								
							else
								innerParameters[count] = childnode.OuterXml;	
						}
						else
							innerParameters[count] =  childnode.OuterXml;
						count++;
					}

					parameters.Add(innerParameters);
				}
				
			}

			return parameters;			
		}

		/// <summary>
		/// This method will browse through an XML message, look for an element with the 
		/// same name as the parameter send through. If the element name is found and has child elements 
		/// then the method should return all its child elements innertext as an objectarray. If the method does 
		/// not have any child elements return the innertext of that element.
		/// </summary>
		/// <param name="soapMessage">The Xml message</param>
		/// <param name="elementName">the element to look for</param>
		/// <param name="allOuterXml">returns outerxml for all elements</param>
		/// <param name="nameSpace">eventual namespace that might be in the xml document</param>
		/// <returns></returns>
		public static string GetParameter(string soapMessage, 
										  string elementName,
										  bool allOuterXml,
										  string nameSpace)
		{					
			ArrayList parametersArray = GetParameters(soapMessage, elementName, allOuterXml, nameSpace);
			object[] parameter = (object[]) parametersArray[0];

			return (string) parameter[0];
		}

		/// <summary>
		/// Replaces a tag with the value passed through within a string
		/// </summary>
		/// <param name="soapMessage">soapmessage to amend</param>
		/// <param name="elementName">element in xml to replace</param>
		/// <param name="replaceText">text to replace the xml element(need to be the outerXML)</param>
		/// <returns></returns>
		public static string ReplaceParameter(string soapMessage,
											  string elementName,
											  string replaceText)											  
		{
			int messageLength = soapMessage.ToCharArray().Length; 
			int startIndex = soapMessage.IndexOf("<" + elementName);
			int endIndex = soapMessage.IndexOf("</" + elementName + ">");			

			string newMessage = soapMessage.Substring(0, startIndex) +
								replaceText +
								soapMessage.Substring(endIndex, messageLength - endIndex);

			return newMessage;
		}

		/// <summary>
		/// Gets the outerxml of an element, use this function if the namespace in the
		/// xml document is unknown. this function does not use the dom document.
		/// </summary>
		/// <param name="soapMessage">Message</param>
		/// <param name="elementName">element to return outerxml</param>
		/// <returns>outerxml of element</returns>
		public static string GetOuterXml(string soapMessage,
									     string elementName)
		{
			int startIndex = soapMessage.IndexOf("<" + elementName);
			int endIndex = soapMessage.IndexOf("</" + elementName + ">");			
			
			return soapMessage.Substring(startIndex, endIndex - startIndex);
		}

		/// <summary>
		/// Gets the innerxml of an element, use this function if the namespace in the
		/// xml document is unknown. this function does not use the dom document.
		/// </summary>
		/// <param name="soapMessage">Message</param>
		/// <param name="elementName">element to return innerXml</param>
		/// <returns>innerXml of element</returns>
		public static string GetInnerXml(string soapMessage,
										 string elementName)
		{
			string startTag = "<" + elementName + ">";
			string endTag = "</" + elementName + ">";

			int startIndex = soapMessage.IndexOf(startTag) + startTag.Length;
			int endIndex = soapMessage.IndexOf(endTag);			
			
			return soapMessage.Substring(startIndex, endIndex - startIndex);
		}

		/// <summary>
		/// Splits a string that contains multiple soapmessages in an array of strings
		/// each containing one soapmessage
		/// </summary>
		/// <param name="inputXml">XML file to split</param>
		/// <param name="soapSeparator">Seperator indicating the start of new message</param>
		/// <returns>an array of strings each containing one soapmessage</returns>
		public static string[] SplitSoapMessages(string inputXml, string soapSeparator)
		{									
			int SoapSeparatorLength = soapSeparator.ToCharArray().Length;

			ArrayList messagesArrayList = new ArrayList();

			int newIndex= 0;
			int oldIndex = 0;

			while(newIndex != -1)
			{
				newIndex = 0;
				oldIndex = 0;

				int inputXmlLength = inputXml.ToCharArray().Length;

				newIndex = inputXml.IndexOf(soapSeparator, SoapSeparatorLength, inputXmlLength - SoapSeparatorLength);
				if (newIndex != -1)
				{
					messagesArrayList.Add(inputXml.Substring(oldIndex, newIndex - oldIndex));
					inputXml = inputXml.Remove(oldIndex, newIndex - oldIndex);
				}
				else
					messagesArrayList.Add(inputXml);
				
			}


			//convert to an array of strings
			string[] messagesArray = new string[messagesArrayList.Count];
			int count = 0;
			foreach(string message in messagesArrayList)
			{
				if (message.Length != 0)
					messagesArray[count] = message;
				count++;
			}

			return messagesArray;

			}

		/// <summary>
		/// Convert a stream to a string from a path
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string ConvertStreamToString(string path)
		{
			StreamReader streamReader = new StreamReader(path);

			try
			{
				return streamReader.ReadToEnd();
			}
			finally
			{
				streamReader.Close();				
			}
		}

		/// <summary>
		/// Convert a stream to a string 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string ConvertStreamToString(Stream inputStream)
		{
			StreamReader streamReader = new StreamReader(inputStream);

			try
			{
				return streamReader.ReadToEnd();
			}
			finally
			{
				streamReader.Close();				
			}
		}

		/// <summary>
		/// return configuration value
		/// </summary>
		/// <param name="Key">key to lookup</param>
		/// <returns>value in the setting</returns>
		public static string GetConfigSetting(string key)
		{
			return ConfigurationManager.AppSettings[key].ToString();
		}

		/// <summary>
		/// parses an xml document
		/// </summary>
		/// <param name="soapMessage">soapmessage to parse</param>
		/// <returns>true when a xml file is valid</returns>
		public static bool IsValidXml(string soapMessage)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();

				//load the xml into the xmldocument object
				xmlDocument.PreserveWhitespace = false;
				xmlDocument.LoadXml(soapMessage);

				return true;
			}
			catch
			{
				return false;
			}

		}

		
	}
}
