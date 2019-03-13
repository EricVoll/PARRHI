using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace PARS_XSD_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateXMLScheme();
            //ValidateXMLScheme();
        }


        public static void ValidateXMLScheme()
        {
            do
            {
                XmlReaderSettings booksSettings = new XmlReaderSettings();
                booksSettings.CloseInput = true;
                XmlReader books = null;
                bool didThrow = false;
                try
                {
                    booksSettings.Schemas.Add("pars", @"C:\Users\ericv\Documents\TUM\BA\10_XSD\PARS_XSD_Test\PARS_XSD_Test\parsScheme.xsd");
                    booksSettings.ValidationType = ValidationType.Schema;
                    booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

                    books = XmlReader.Create(@"C:\Users\ericv\Documents\TUM\BA\10_XSD\PARS_XSD_Test\PARS_XSD_Test\input.xml", booksSettings);

                    while (books.Read()) { }
                }
                catch (Exception ex)
                {
                    didThrow = true;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    GC.Collect();
                    booksSettings.Reset();
                    if (books != null)
                    {
                        books.Close();
                        books.Dispose();
                    }
                }
                if (!didThrow) Console.WriteLine("XML is valid\n");
                else Console.WriteLine("XML is invalid\n");

            } while (Console.ReadKey().Key != ConsoleKey.E);
        }


        public static void GenerateXMLScheme()
        {
            do
            {
                List<XmlReader> xmlReader = new List<XmlReader>();
                int counter = 0;
                try
                {
                    string basePath = @"C:\Users\ericv\Documents\TUM\BA\10_XSD\PARS_XSD_Test\PARS_XSD_Test\";
                    while (File.Exists(Path.Combine(basePath, $"input{counter}.xml"))) xmlReader.Add(XmlReader.Create(Path.Combine(basePath, $"input{counter++}.xml")));
                    XmlSchemaSet schemaSet = new XmlSchemaSet();
                    XmlSchemaInference inference = new XmlSchemaInference();

                    foreach (var item in xmlReader)
                    {
                        schemaSet = inference.InferSchema(item, schemaSet);
                    }

                    // Display the refined schema.
                    foreach (XmlSchema schema in schemaSet.Schemas("pars"))
                    {
                        FileStream file = new FileStream(@"C:\Users\ericv\Documents\TUM\BA\10_XSD\PARS_XSD_Test\PARS_XSD_Test\parsScheme.xsd", FileMode.Create, FileAccess.ReadWrite);
                        XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
                        xwriter.Formatting = Formatting.Indented;
                        schema.Write(xwriter);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    foreach (var item in xmlReader)
                    {
                        item.Close();
                    }
                }
                Console.Write("\nNumber of input files: " + counter);
            }
            while (false);
        }




        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
