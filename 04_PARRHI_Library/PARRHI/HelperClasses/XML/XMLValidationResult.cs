using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PARRHI.HelperClasses.XML
{
    public class XMLValidationResult
    {
        public XMLValidationResult()
        {

        }

        #region XSD Validation Errors

        private List<XMLValidationError> Errors { get; set; } = new List<XMLValidationError>();

        public void AddError(XMLValidationError error)
        {
            Errors.Add(error);
        }
        public void AddError(ValidationEventArgs e)
        {
            XMLValidationError error = new XMLValidationError(e);
            AddError(error);
        }
        public List<XMLValidationError> GetAllErrors() { return new List<XMLValidationError>(Errors.Union(ConversionError)); }

        public bool DidThrowExceptionWhileValidating { get; set; }

        #endregion

        #region Conversion Errors

        private List<XMLValidationError> ConversionError { get; set; } = new List<XMLValidationError>();

        public void AddConversionError(XMLValidationError e)
        {
            ConversionError.Add(e);
        }

        #endregion
    }
}
