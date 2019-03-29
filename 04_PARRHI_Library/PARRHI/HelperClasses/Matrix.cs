using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses
{
    public class Matrix
    {
        public double[,] matrix { get; set; }
        public int NrRows { get { return matrix.GetLength(0); } }
        public int NrCols { get { return matrix.GetLength(1); } }

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }


        public static Point operator *(Matrix a, Point b)
        {
            double[] value = new double[3];
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    value[r] += a.matrix[r,c] * b[c];
                }
            }
            return new Point(value[0], value[1], value[2]);
        }

        public static Matrix operator * (Matrix a, Matrix b)
        {
            double[,] values = new double[a.NrRows, b.matrix.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
            {
                for (int col = 0; col < values.GetLength(1); col++)
                {
                    values[row, col] = 0;
                    for (int i = 0; i < a.NrCols; i++)
                    {
                        values[row, col] += a.matrix[row, i] * b.matrix[i, col];
                    }
                }
            }
            return new Matrix(values);
        }
    }
}
