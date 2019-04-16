using PARRHI.HelperClasses;
using PARRHI.HelperClasses.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PARRHI.Objects
{
    public class DataImport
    {
        public DataImport()
        {
            BaseElement.Element.RegisterId = RegisterId;
        }

        private string XSDFilePath = @"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\parsScheme.xsd";

        public XMLValidationResult XMLValidationResult { get; set; }

        /// <summary>
        /// Imports the PARRHI data with both files specified
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xsdFilePath"></param>
        /// <returns></returns>
        public Container Import(string xmlFilePath, string xsdFilePath)
        {
            XSDFilePath = xsdFilePath;
            return Import(xmlFilePath, false);
        }


        /// <summary>
        /// Imports the xml file with the default xsd file
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public Container Import(string xmlFilePath, bool skipValidation = true)
        {
            HelperClasses.XML.XMLSerializerClass xmlSerializer = new HelperClasses.XML.XMLSerializerClass();
            if (!skipValidation)
            {
                XMLValidationResult = xmlSerializer.ValidateXML(xmlFilePath, XSDFilePath);
            }
            else
            {
                XMLValidationResult = new XMLValidationResult() { DidThrowExceptionWhileValidating = false };
            }

            if (!XMLValidationResult.DidThrowExceptionWhileValidating)
            {
                var inputData = xmlSerializer.Deserialize<Objects.InputData>(xmlFilePath);
                var container = new InputDataToContainer(inputData, XMLValidationResult).ConvertToContainer();
                return container;
            }

            return null;
        }

        public Container Import(string xmlContent)
        {
            Output.Instance.Log("Skipped XML Validation");
            HelperClasses.XML.XMLSerializerClass xmlSerializer = new HelperClasses.XML.XMLSerializerClass();
            XMLValidationResult = new XMLValidationResult() { DidThrowExceptionWhileValidating = false };


            if (!XMLValidationResult.DidThrowExceptionWhileValidating)
            {
                var inputData = xmlSerializer.DeserializeFromContent<Objects.InputData>(xmlContent);
                var container = new InputDataToContainer(inputData, XMLValidationResult).ConvertToContainer();
                return container;
            }

            return null;
        }

        private List<string> Ids = new List<string>();
        /// <summary>
        /// Adds the Id to the specified element and checks for duplicates
        /// </summary>
        /// <param name="id"></param>
        public void RegisterId(string id)
        {
            if (Ids.Any(x => x == id))
            {
                XMLValidationResult.AddConversionError(new XMLValidationError($"Duplicate ID: {id}, nrOfElements:{Ids.Count}", System.Xml.Schema.XmlSeverityType.Error, null));
            }

            Ids.Add(id);
        }
    }
}
