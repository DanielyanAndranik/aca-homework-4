using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    class Matrix
    {
        private double[,] matrix;
        public readonly int Rows;
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
        }

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
            throw new Exception();
        }

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

        public static Matrix operator *(int first, Matrix second)
        {
            int rows = second.Rows;
            int columns = second.Columns;
            double[,] temporary = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    temporary[i, j] = first * second[i, j];
                }
            }
            return new Matrix(temporary);
        }

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

                double[,] temporary = new double[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        temporary[i, j] = this[i, j];
                    }
                }

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
        /// returns the smallest elemnt of the matrix.
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
