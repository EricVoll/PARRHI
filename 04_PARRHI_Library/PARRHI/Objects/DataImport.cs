using PARRHI.HelperClasses;
using PARRHI.HelperClasses.XML;
using System;
using System.Collections.Generic;
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

        public const string XSDFilePath = @"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\parsScheme.xsd";

        public XMLValidationResult XMLValidationResult { get; set; }

        public Container Import(string xmlFilePath)
        {
            HelperClasses.XML.XMLSerializerClass xmlSerializer = new HelperClasses.XML.XMLSerializerClass();
            XMLValidationResult = xmlSerializer.ValidateXML(xmlFilePath, XSDFilePath);

            if (!XMLValidationResult.DidThrowExceptionWhileValidating)
            {
                var inputData = xmlSerializer.Deserialize<Objects.InputData>(xmlFilePath, XSDFilePath);
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
            if (Ids.Any(x => x == id)) {
                XMLValidationResult.AddConversionError(new XMLValidationError($"Duplicate ID: {id}, nrOfElements:{Ids.Count}", System.Xml.Schema.XmlSeverityType.Error, null));
            }

            Ids.Add(id);
        }
    }
}
