using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PARRHI.HelperClasses.XML
{
    public class XMLValidationError
    {
        public XMLValidationError(string message, XmlSeverityType severity, XmlSchemaException exception)
        {
            Message = message;
            Severity = severity;
            Exception = exception;
        }

        public XMLValidationError(ValidationEventArgs e)
        {
            Message = e.Message;
            Severity = e.Severity;
            Exception = e.Exception;
        }

        public string Message { get; set; }
        public XmlSeverityType Severity { get; set; }
        public XmlSchemaException Exception { get; set; }
    }
}
