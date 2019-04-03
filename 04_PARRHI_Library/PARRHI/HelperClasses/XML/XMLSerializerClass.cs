using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PARRHI.HelperClasses.XML
{
    public class XMLSerializerClass
    {
        /// <summary>
        /// Deserializes the object without validating the xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath"></param>
        /// <param name="xsdFilePath"></param>
        /// <returns></returns>
        public T Deserialize<T>(string xmlFilePath)
        {
            T obj = (T)new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(xmlFilePath));
            
            return obj;
        }

        public T DeserializeFromContent<T>(string xmlFileContent)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new System.IO.StringReader(xmlFileContent))
            {
                return (T)serializer.Deserialize(reader);
            }

            return default(T);
        }

        /// <summary>
        /// Result object that collects all errors
        /// </summary>
        private XMLValidationResult Result { get; set; }

        /// <summary>
        /// Validate the xml file against the specified xsd scheme
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xsdFilePath"></param>
        /// <returns></returns>
        public XMLValidationResult ValidateXML(string xmlFilePath, string xsdFilePath)
        {
            Result = new XMLValidationResult();
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.CloseInput = true;
            XmlReader books = null;

            try
            {
                booksSettings.Schemas.Add("PARRHI", xsdFilePath);
                booksSettings.ValidationType = ValidationType.Schema;
                booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

                books = XmlReader.Create(xmlFilePath, booksSettings);

                while (books.Read()) { /*Don't do anything*/ }
            }
            catch (Exception ex)
            {
                Result.DidThrowExceptionWhileValidating = true;
                Output.Instance.Error(ex.Message);
            }
            finally
            {
                booksSettings.Reset();
                if (books != null)
                {
                    books.Close();
                    books.Dispose();
                }
            }

            return Result;
        }

        /// <summary>
        /// EventHandler for invalid xml nodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Result.AddError(new XMLValidationError($"{e.Message} Line:{e.Exception.LineNumber}, Pos:{e.Exception.LinePosition}", e.Severity, e.Exception));

            if (e.Severity == XmlSeverityType.Warning)
            {
                Output.Instance.Write("WARNING: ");
                Output.Instance.Write(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Output.Instance.Error("ERROR: ");
                Output.Instance.Error(e.Message);
            }
        }
    }
}
