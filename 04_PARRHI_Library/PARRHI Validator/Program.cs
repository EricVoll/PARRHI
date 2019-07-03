using PARRHI;
using PARRHI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI_Validator
{
    class Program
    {
        static void Main(string[] args)
        {

            Output.Instance.SetErrorDelegate(Write);
            Output.Instance.SetLogDelegate(Write);
            Output.Instance.SetOutputDelegate(Write);

            DataImport import = new DataImport();
            var Container = import.Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\ParametrisedProgam_Evaluation_Elene.xml", @"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\parrhiScheme.xsd");

            foreach (var item in import.XMLValidationResult.GetAllErrors())
            {
                Console.WriteLine("Error: ");
                Console.WriteLine("Message: " + item.Message);
                Console.WriteLine("Exception: " + item.Exception);
                Console.WriteLine("Severity: " + item.Severity);
            }

        }

        private static void Write(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
