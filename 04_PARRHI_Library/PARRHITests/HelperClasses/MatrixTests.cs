using Microsoft.VisualStudio.TestTools.UnitTesting;
using PARRHI.HelperClasses;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void MatrixMulpoint()
        {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, -3 }, { 1, 5, 3 }, { 2, 3, 4 } });
            Point point = new Point(10, -2, 3);
            var p = matrix * point;
            Assert.AreEqual(-3, p.X);
            Assert.AreEqual(9, p.Y);
            Assert.AreEqual(26, p.Z);
        }
        [TestMethod()]
        public void MatrixMulMatrix()
        {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, -2, 6 }, { 3, 99, 1 } });
            Matrix matrix2 = new Matrix(new double[,] { { 4, -1, 7 }, { 0.4, 1, 5 }, { -0.5, 2, 4 } });
            Matrix expected = new Matrix(new double[,] { { 3.3, 7, 29 }, { 12.2, 6, 42 }, { 51.1, 98, 520 } });
            Matrix actual = matrix * matrix2;
            for (int row = 0; row < expected.NrRows; row++)
            {
                for (int col = 0; col < expected.NrCols; col++)
                {
                    Assert.AreEqual(expected.matrix[row, col], actual.matrix[row, col]);
                }
            }
        }
        [TestMethod()]
        public void MatrixMulMatrix2()
        {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 5 },{4, 5, 6} });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2, 3, 4 },{ 2, 3, 4, 5 },{ 3, 4, 5, 6 } });
            Matrix expected = new Matrix(new double[,] { { 14, 20, 26, 32 },{ 20, 29, 38, 47 },{ 26, 38, 50, 62 },{ 32, 47, 62, 77 } });
            Matrix actual = matrix * matrix2;
            for (int row = 0; row < expected.NrRows; row++)
            {
                for (int col = 0; col < expected.NrCols; col++)
                {
                    Assert.AreEqual(expected.matrix[row, col], actual.matrix[row, col]);
                }
            }
        }

        [TestMethod()]
        public void TestDimensions()
        {

            Matrix matrix2 = new Matrix(new double[,] { { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 3, 4, 5, 6 } });
            Assert.AreEqual(4, matrix2.NrCols);
            Assert.AreEqual(3, matrix2.NrRows);
        }
    }
}