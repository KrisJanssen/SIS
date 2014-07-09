// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationUtil.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Provide comparison of string data.  This class currently
//   implement System.Web.UI.WebControls validation so that
//   we don't have to write more codes.  Eventually, we may want
//   to implement out own code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;
    using System.Xml.Serialization;

    /// <summary>
    /// Provide comparison of string data.  This class currently
    /// implement System.Web.UI.WebControls validation so that
    /// we don't have to write more codes.  Eventually, we may want
    /// to implement out own code.
    /// </summary>
    internal class ValidationUtil : BaseCompareValidator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidationUtil"/> class from being created. 
        /// Disable default ctor.
        /// </summary>
        private ValidationUtil()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Compare two values using provided operator and data type.
        /// </summary>
        /// <param name="leftText">
        /// </param>
        /// <param name="rightText">
        /// </param>
        /// <param name="op">
        /// </param>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CompareValues(
            string leftText, 
            string rightText, 
            ValidationCompareOperator op, 
            ValidationDataType type)
        {
            System.Web.UI.WebControls.ValidationCompareOperator vco =
                (System.Web.UI.WebControls.ValidationCompareOperator)
                Enum.Parse(typeof(System.Web.UI.WebControls.ValidationCompareOperator), op.ToString());

            System.Web.UI.WebControls.ValidationDataType vdt =
                (System.Web.UI.WebControls.ValidationDataType)
                Enum.Parse(typeof(System.Web.UI.WebControls.ValidationDataType), type.ToString());

            return ValidationUtil.Compare(leftText, rightText, vco, vdt);
        }

        /// <summary>
        /// Load the entire text file into a string.
        /// </summary>
        /// <param name="sFile">
        /// Full pathname of file to read.
        /// </param>
        /// <returns>
        /// String content of the text file.
        /// </returns>
        public static string FileToString(string sFile)
        {
            string sText = string.Empty;
            using (StreamReader sr = new StreamReader(sFile))
            {
                sText = sr.ReadToEnd();
            }

            return sText;
        }

        /// <summary>
        /// Load the text file with specified size as return text.
        /// </summary>
        /// <param name="sFile">
        /// File to read from.
        /// </param>
        /// <param name="size">
        /// Number of char to read.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FileToString(string sFile, int size)
        {
            char[] cToRead = new char[size];
            string sText = string.Empty;
            using (StreamReader sr = new StreamReader(sFile))
            {
                sr.Read(cToRead, 0, size);
                sText = new string(cToRead);
            }

            return sText;
        }

        /// <summary>
        /// Write object to xml string.
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ObjectToXmlString(object obj)
        {
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            TextWriter w = null;
            string sReturn = string.Empty;

            try
            {
                w = new StringWriter();
                x.Serialize(w, obj);
                sReturn = w.ToString();
            }
            finally
            {
                if (w != null)
                {
                    w.Close();
                }
            }

            return sReturn;
        }

        /// <summary>
        /// Save a string to file.
        /// </summary>
        /// <param name="strValue">
        /// String value to save.
        /// </param>
        /// <param name="strFileName">
        /// File name to save to.
        /// </param>
        /// <param name="bAppendToFile">
        /// True - to append string to file.  Default false - overwrite file.
        /// </param>
        public static void StringToFile(string strValue, string strFileName, bool bAppendToFile)
        {
            using (StreamWriter sw = new StreamWriter(strFileName, bAppendToFile))
            {
                sw.Write(strValue);
            }
        }

        /// <summary>
        /// Save a string to file.
        /// </summary>
        /// <param name="strValue">
        /// The str Value.
        /// </param>
        /// <param name="strFileName">
        /// The str File Name.
        /// </param>
        public static void StringToFile(string strValue, string strFileName)
        {
            StringToFile(strValue, strFileName, false);
        }

        /// <summary>
        /// Utility method validation regular expression.
        /// </summary>
        /// <param name="valueText">
        /// </param>
        /// <param name="patternText">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ValidateRegEx(string valueText, string patternText)
        {
            Match m = Regex.Match(valueText, patternText);
            return m.Success;
        }

        /// <summary>
        /// Get object from an xml string.
        /// </summary>
        /// <param name="xmlString">
        /// </param>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object XmlStringToObject(string xmlString, Type type)
        {
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
            TextReader r = null;
            object retObj = null;
            try
            {
                r = new StringReader(xmlString);
                retObj = x.Deserialize(r);
            }
            finally
            {
                if (r != null)
                {
                    r.Close();
                }
            }

            return retObj;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Here because base class required it.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool EvaluateIsValid()
        {
            return false;
        }

        #endregion
    }
}