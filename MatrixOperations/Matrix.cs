using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    class Matrix
    {
        /// <summary>
        /// 2 dimensional double array.
        /// </summary>
        private double[,] matrix;

        /// <summary>
        /// Count of rows.
        /// </summary>
        public readonly int Rows;

        /// <summary>
        /// Count of columns.
        /// </summary>
        public readonly int Columns;

        /// <summary>
        /// Constructs a new random matrix
        /// </summary>
        /// <param name="rows">The matrix's rows count</param>
        /// <param name="columns">The matrix's columns count</param>
        public Matrix(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            matrix = new double[this.Rows, this.Columns];
            Random random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrix[i, j] = random.Next(10);
                }
            }
        }

        /// <summary>
        /// Constructs a new matrix
        /// </summary>
        /// <param name="matrix">The two dimensioanl array</param>
        public Matrix(double[,] matrix)
        {
            this.Columns = matrix.GetLength(0);
            this.Rows = matrix.Length / this.Columns;
            this.matrix = new double[this.Rows, this.Columns];
            for(int i = 0; i < this.Rows; i++)
            {
                for(int j = 0; j < this.Columns; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// The indexer method
        /// </summary>
        /// <param name="i">The row index</param>
        /// <param name="j">The column index</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get { return this.matrix[i, j]; }
            private set { this.matrix[i, j] = value; }
        }

        /// <summary>
        /// Operator for adding tow matrices.
        /// </summary>
        /// <param name="first">The first matrix</param>
        /// <param name="second">The second matrix</param>
        /// <returns>Returns the sum matrix</returns>
        public static Matrix operator +(Matrix first, Matrix second)
        {
            if (first.Rows == second.Rows && first.Columns == second.Columns)
            {
                int rows = first.Rows;
                int columns = first.Columns;
                double[,] temporary = new double[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        temporary[i, j] = first[i, j] + second[i, j];
                    }
                }
                return new Matrix(temporary);
            }
            throw new Exception("The sizes of the matrices are not match");
        }

        /// <summary>
        /// Operator for multiplying two matrices.
        /// </summary>
        /// <param name="first">The first matrix</param>
        /// <param name="second">The second matrix</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix first, Matrix second)
        {
            if(first.Columns == second.Rows)
            {
                int rows = first.Rows;
                int columns = second.Columns;
                double[,] temporary = new double[rows, columns];
                for(int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        temporary[i, j] = 0;
                        for(int k = 0; k < second.Rows; k++)
                        {
                            temporary[i, j] += first[i, k] * second[k, j];
                        }
                    }
                }
                return new Matrix(temporary);
            }
            throw new Exception();
        }

        /// <summary>
        /// Operator for scalar multiplying.
        /// </summary>
        /// <param name="factor">The factor</param>
        /// <param name="second">The matrix</param>
        /// <returns></returns>
        public static Matrix operator *(double factor, Matrix second)
        {
            int rows = second.Rows;
            int columns = second.Columns;
            double[,] temporary = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    temporary[i, j] = factor * second[i, j];
                }
            }
            return new Matrix(temporary);
        }

        /// <summary>
        /// Convert the array into matrix.
        /// </summary>
        /// <param name="matrix">The two dimensional array</param>
        public static implicit operator Matrix(double[,] matrix)
        {
            return new Matrix(matrix);
        }

        /// <summary>
        /// Convert the matrix into two dimensional array.
        /// </summary>
        /// <param name="matrix">The matrix</param>
        public static implicit operator double[,](Matrix matrix)
        {
            int rows = matrix.Rows;
            int columns = matrix.Columns;
            double[,] temporary = new double[rows, columns];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    temporary[i, j] = matrix[i, j];
                }
            }
            return temporary;
        }

        /// <summary>
        /// Makes the inverse matrix of this.
        /// </summary>
        /// <returns>Returns the inverse matrix.</returns>
        public Matrix Inverse()
        {
            if(this.Rows == this.Columns)
            {
                int rows = this.Rows;
                int columns = this.Columns;
                double[,] inverse = new double[rows, columns];

                for(int i = 0; i < rows; i++)
                {
                    for(int j = 0; j < columns; j++)
                    {
                        if(i == j)
                        {
                            inverse[i, j] = 1;
                        }
                        else
                        {
                            inverse[i, j] = 0;
                        }
                    }
                }

                //double[,] temporary = new double[rows, columns];
                //for (int i = 0; i < rows; i++)
                //{
                //    for (int j = 0; j < columns; j++)
                //    {
                //        temporary[i, j] = this[i, j];
                //    }
                //}

                double[,] temporary = this;

                for(int i = 0; i < rows; i++)
                {
                    if(temporary[i, i] == 0)
                    {
                        bool isZero = true;
                        for(int k = i + 1; k < rows; k++)
                        {
                            if(temporary[k, i] != 0)
                            {
                                SwapRows(ref temporary, k, i);
                                SwapRows(ref inverse, k, i);
                                isZero = false;
                                break;
                            }
                        }
                        if(isZero)
                        {
                            throw new Exception();
                        }
                    }

                    double temp = temporary[i, i];

                    for (int j = 0; j < columns; j++)
                    {
                        inverse[i, j] = inverse[i, j] / temp;
                        temporary[i, j] = temporary[i, j] / temp;
                    }

                    for (int k = 0; k < rows; k++)
                    {
                        if(k != i)
                        {
                            temp = temporary[k, i];
                            for(int j = 0; j < columns; j++)
                            {
                                inverse[k, j] -= temp * inverse[i, j];
                                temporary[k, j] -= temp * temporary[i, j];
                            }
                        }
                    }
                }
                return new Matrix(inverse);

            }
            throw new Exception();
        }

        /// <summary>
        /// Swap two rows of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix</param>
        /// <param name="m">The first row</param>
        /// <param name="n">The second row</param>
        private static void SwapRows(ref double[,] matrix, int m, int n)
        {
            if(m == n)
            {
                return;
            }
            double temporary;
            for(int j = 0; j < Math.Sqrt(matrix.Length); j++)
            {
                temporary = matrix[m, j];
                matrix[m, j] = matrix[n, j];
                matrix[n, j] = temporary;
            }
        }
        
        /// <summary>
        /// Returns the transpose matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            int rows = this.Columns;
            int columns = this.Rows;
            double[,] temporary = new double[rows, columns];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    temporary[i, j] = this.matrix[j, i];
                }
            }
            return new Matrix(temporary);
        }

        /// <summary>
        /// Check if the matrix is orthogonal
        /// </summary>
        /// <returns></returns>
        public bool IsOrthogonal()
        {
            Matrix temporary = this * this.Transpose();
            for(int i = 0; i < this.Rows; i++)
            {
                for(int j = 0; j < this.Columns; j++)
                {
                    if(i == j)
                    {
                        if(temporary[i, j] != 1)
                        {
                            return false;
                        }
                    }
                    else if(temporary[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">The rotation across x-axis</param>
        /// <param name="y">The rotation across y-axis</param>
        /// <param name="z">The rotation across y-axis</param>
        public void Rotate3D(double x = 0, double y = 0, double z = 0)
        {
            if(this.Rows != 3)
            {
                throw new Exception();
            }

            double[,] rotationMatrix;

            double[,] rotateX = {   { 1, 0, 0 }, 
                                    { 0, Math.Cos(x), -Math.Sin(x) }, 
                                    { 0, Math.Sin(x), Math.Cos(x) }
                                };

            double[,] rotateY = {   { Math.Cos(x), 0, Math.Sin(x) }, 
                                    { 0, 1, 0 }, 
                                    { -Math.Sin(x), 0, Math.Cos(x) }
                                };

            double[,] rotateZ = {   { Math.Cos(x), -Math.Sin(x), 0 }, 
                                    { Math.Sin(x), Math.Cos(x), 0 }, 
                                    { 0, 0, 1 }
                                };

            rotationMatrix = (Matrix)rotateX * rotateY * rotateZ;

            this.matrix = rotationMatrix * this;
        }

        /// <summary>
        /// Translates the matrix.
        /// </summary>
        /// <param name="x">The translation across x-axis</param>
        /// <param name="y">The translation across y-axis</param>
        /// <param name="z">The translation across z-axis</param>
        public void Translate3D(double x = 0, double y = 0, double z = 0)
        {
            if(this.Rows != 3)
            {
                throw new Exception();
            }
            double[] temporary = { x, y, z };
            for(int  i = 0; i < this.Rows; i++)
            {
                for(int j = 0; i < this.Columns; i++)
                {
                    this[i, j] += temporary[i];
                }
            }
        }

        /// <summary>
        /// Scales the matrix.
        /// </summary>
        /// <param name="x">The x factor</param>
        /// <param name="y">The y factor</param>
        /// <param name="z">The z factor</param>
        public void Scale3D(double x = 1, double y = 1, double z = 1)
        {
            if(this.Rows != 3)
            {
                throw new Exception();
            }
            double[] scaleFactor = { x, y, z };
            double[,] scalingMatrix = new double[this.Rows, this.Rows];
            for(int i = 0; i < this.Rows; i++)
            {
                scalingMatrix[i, i] = scaleFactor[i];
            }
            this.matrix = new Matrix(scalingMatrix) * this;
        }

        /// <summary>
        /// Returns the largest element of the matrix.
        /// </summary>
        /// <returns></returns>
        public double GetLargest()
        {
            double largest = this[0, 0];
            for(int i = 0; i < this.Rows; i++)
            {
                for(int j = 0; j < this.Columns; j++)
                {
                    if(this[i, j] > largest)
                    {
                        largest = this[i, j];
                    }
                }
            }
            return largest;
        }

        /// <summary>
        /// Returns the smallest element of the matrix.
        /// </summary>
        /// <returns></returns>
        public double GetSmallest()
        {
            double smallest = this[0, 0];
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if (this[i, j] < smallest)
                    {
                        smallest = this[i, j];
                    }
                }
            }
            return smallest;
        }

        /// <summary>
        /// Prints the matrix.
        /// </summary>
        public void Print()
        {
            for(int i = 0; i < this.Rows; i++)
            {
                for(int j = 0; j < this.Columns; j++)
                {
                    Console.Write("{0}\t", this[i, j]);
                }
                Console.WriteLine('\n');
            }
        }
    }
}
