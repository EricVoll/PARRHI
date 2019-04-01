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

        public const string XSDFilePath = "";

        public XMLValidationResult XMLValidationResult;

        public void Import(string xmlFilePath)
        {
            HelperClasses.XML.XMLSerializerClass xmlSerializer = new HelperClasses.XML.XMLSerializerClass();
            XMLValidationResult = xmlSerializer.ValidateXML(xmlFilePath, XSDFilePath);

            if (!XMLValidationResult.DidThrowExceptionWhileValidating)
            {
                var inputData = xmlSerializer.Deserialize<Objects.InputData>(xmlFilePath, XSDFilePath);
                
            }
            
        }

        private List<string> Ids = new List<string>();
        /// <summary>
        /// Adds the Id to the specified element and checks for duplicates
        /// </summary>
        /// <param name="id"></param>
        public void RegisterId(string id)
        {
            if (Ids.Any(x => x == id)) throw new Exception($"Duplicate id found: {id}");

            Ids.Add(id);
        }
    }
}
