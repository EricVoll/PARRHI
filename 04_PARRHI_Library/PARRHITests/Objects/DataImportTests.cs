﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void ImportTest0()
        {

            var Container = new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\ParametrisedProgam_Evaluation.xml", @"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\parrhiScheme.xsd");
            Assert.IsNotNull(Container);
        }

        [TestMethod()]
        public void ImportTest()
        {
            //new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\input1.xml");
            Assert.IsNotNull(new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\ParametrisedProgram.xml", false));
        }
        [TestMethod()]
        public void ImportTest2()
        {
            //new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\input1.xml");
            Assert.IsNotNull(new DataImport().Import(PARRHITests.Properties.Resources.inputxml));
        }
        [TestMethod()]
        public void ImportTest3()
        {
            Assert.IsNotNull(new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\input1.xml", false));
        }
        [TestMethod()]
        public void ImportTest4()
        {
            Assert.IsNotNull(new DataImport().Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\input1.xml", @"C:\Users\ericv\Documents\TUM\BA\PARRHI\10_XSD\PARS_XSD_Test\PARS_XSD_Test\parsScheme.xsd"));
        }
    }
}