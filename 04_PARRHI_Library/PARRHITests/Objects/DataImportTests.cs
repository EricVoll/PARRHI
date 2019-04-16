using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PARRHI.Objects;

namespace PARRHI.Objects.Tests
{
    [TestClass()]
    public class DataImportTests
    {
        [TestMethod()]
        public void ImportTest()
        {
            new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\InputData.xml");
        }
        [TestMethod()]
        public void ImportTest2()
        {
            new DataImport().Import(PARRHITests.Properties.Resources.inputxml);
        }

        [TestMethod()]
        public void ImportTest3()
        {
            new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\InputData.xml", @"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\parrhiScheme.xsd");
        }
    }
}