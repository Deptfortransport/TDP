// *********************************************** 
// NAME                 : CSVReader.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Helper class to read in a CSV file as a stream
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/CSVReader.cs-arc  $
//
//   Rev 1.1   Apr 06 2009 16:05:34   mmodi
//New method to return a CSV row as a string
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:23:40   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.IO;
using System.Text;

namespace AO.Common
{
    /// <summary>
    /// A data-reader style interface for reading CSV files.
    /// </summary>
    public class CSVReader : IDisposable
    {

        #region Private variables

        private Stream stream;
        private StreamReader reader;

        private bool firstLineHasBeenIgnored = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new reader for the given stream.
        /// </summary>
        /// <param name="input">The stream to read the CSV from.</param>
        public CSVReader(Stream input) : this(input, null) { }

        /// <summary>
        /// Create a new reader for the given stream and encoding.
        /// </summary>
        /// <param name="input">The stream to read the CSV from.</param>
        /// <param name="enc">The encoding used.</param>
        public CSVReader(Stream input, Encoding enc)
        {

            this.stream = input;
            if (!input.CanRead)
            {
                throw new CSVReaderException("Could not read the given CSV stream!");
            }

            reader = (enc != null) ? new StreamReader(input, enc) : new StreamReader(input);
        }

        /// <summary>
        /// Creates a new reader for the given text file path.
        /// </summary>
        /// <param name="filename">The name of the file to be read.</param>
        public CSVReader(string filename) : this(filename, null) { }

        /// <summary>
        /// Creates a new reader for the given text file path and encoding.
        /// </summary>
        /// <param name="filename">The name of the file to be read.</param>
        /// <param name="enc">The encoding used.</param>
        public CSVReader(string filename, Encoding enc)
            : this(new FileStream(filename, FileMode.Open), enc) { }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the the next row of CSV data as the unparsed string (or null if at eof)
        /// </summary>
        /// <param name="ignoreFirstLine">Ignores the first line, set to true if CSV has a header row</param>
        /// <returns>A string of the CSV data row or null if at the end of file.</returns>
        public string GetCSVRow(bool ignoreFirstLine)
        {
            string data;

            if ((ignoreFirstLine) && (!firstLineHasBeenIgnored))
            {
                // Ensures the file moves on to the second line
                data = reader.ReadLine();

                // Prevents skipping a row the next time this method is called
                firstLineHasBeenIgnored = true;
            }

            data = reader.ReadLine();

            if (data == null) return null;
            if (data.Length == 0) return string.Empty;

            return data;
        }

        /// <summary>
        /// Returns the fields for the next row of CSV data (or null if at eof)
        /// </summary>
        /// <param name="ignoreFirstLine">Ignores the first line, set to true if CSV has a header row</param>
        /// <returns>A string array of fields or null if at the end of file.</returns>
        public string[] GetCSVLine(bool ignoreFirstLine)
        {
            string data;

            if ((ignoreFirstLine) && (!firstLineHasBeenIgnored))
            {   
                // Ensures the file moves on to the second line
                data = reader.ReadLine();

                // Prevents skipping a row the next time this method is called
                firstLineHasBeenIgnored = true;
            }

            data = reader.ReadLine();
            
            if (data == null) return null;
            if (data.Length == 0) return new string[0];

            ArrayList result = new ArrayList();

            ParseCSVFields(result, data);

            return (string[])result.ToArray(typeof(string));
        }

        /// <summary>
        /// Saves the input stream to a file. 
        /// ***** IMPORTANT THIS WILL MOVE POSITION TO THE END OF THE STREAM. *****
        /// Future work to redesign to place position back to where it was.
        /// </summary>
        /// <param name="file">Directory and filename to save to</param>
        public void Save(string file)
        {
            // File output to
            FileStream writeStream = new FileStream(file, FileMode.Create, FileAccess.Write);

            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = this.stream.Read(buffer, 0, Length);

            // Write the required bytes to the output file
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = this.stream.Read(buffer, 0, Length);
            }

            // Close the output stream
            writeStream.Flush();
            writeStream.Close();
        }

        #endregion

        #region Private methods

        // Parses the CSV fields and pushes the fields into the result arraylist
        private void ParseCSVFields(ArrayList result, string data)
        {

            int pos = -1;
            while (pos < data.Length)
                result.Add(ParseCSVField(data, ref pos));
        }

        // Parses the field at the given position of the data, modified pos to match
        // the first unparsed position and returns the parsed field
        private string ParseCSVField(string data, ref int startSeparatorPosition)
        {

            if (startSeparatorPosition == data.Length - 1)
            {
                startSeparatorPosition++;
                // The last field is empty
                return "";
            }

            int fromPos = startSeparatorPosition + 1;

            // Determine if this is a quoted field
            if (data[fromPos] == '"')
            {
                // If we're at the end of the string, let's consider this a field that
                // only contains the quote
                if (fromPos == data.Length - 1)
                {
                    fromPos++;
                    return "\"";
                }

                // Otherwise, return a string of appropriate length with double quotes collapsed
                // Note that FSQ returns data.Length if no single quote was found
                int nextSingleQuote = FindSingleQuote(data, fromPos + 1);
                startSeparatorPosition = nextSingleQuote + 1;
                return data.Substring(fromPos + 1, nextSingleQuote - fromPos - 1).Replace("\"\"", "\"");
            }

            // The field ends in the next comma or EOL
            int nextComma = data.IndexOf(',', fromPos);
            if (nextComma == -1)
            {
                startSeparatorPosition = data.Length;
                return data.Substring(fromPos);
            }
            else
            {
                startSeparatorPosition = nextComma;
                return data.Substring(fromPos, nextComma - fromPos);
            }
        }

        // Returns the index of the next single quote mark in the string 
        // (starting from startFrom)
        private int FindSingleQuote(string data, int startFrom)
        {

            int i = startFrom - 1;
            while (++i < data.Length)
                if (data[i] == '"')
                {
                    // If this is a double quote, bypass the chars
                    if (i < data.Length - 1 && data[i + 1] == '"')
                    {
                        i++;
                        continue;
                    }
                    else
                        return i;
                }
            // If no quote found, return the end value of i (data.Length)
            return i;
        }

        #endregion

        /// <summary>
        /// Disposes the CSVReader. The underlying stream is closed.
        /// </summary>
        public void Dispose()
        {
            // Closing the reader closes the underlying stream, too
            if (reader != null) reader.Close();
            else if (stream != null)
                stream.Close(); // In case we failed before the reader was constructed
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>
    /// Exception class for CSVReader exceptions.
    /// </summary>
    public class CSVReaderException : ApplicationException
    {

        /// <summary>
        /// Constructs a new exception object with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public CSVReaderException(string message) : base(message) { }
    }

}
