using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Follow these steps to add two matrices \n");
                   
            Matrix matrixOne = CreateMatrix();
            matrixOne.Print();

            Matrix matrixTwo = CreateMatrix();
            matrixTwo.Print();

            try
            {
                Matrix tempOne = matrixOne + matrixTwo;
                Console.WriteLine("The sum matrix");
                tempOne.Print();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message + '\n');
                Console.ResetColor();
            }


            Console.WriteLine("Follow these steps to multiply two matrices \n");

            Matrix matrixThree = CreateMatrix();
            matrixThree.Print();

            Matrix matrixFour = CreateMatrix();
            matrixFour.Print();
            
            try
            {
                Matrix tempTwo = matrixThree * matrixFour;
                Console.WriteLine("The product matrix");
                tempTwo.Print();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message + '\n');
                Console.ResetColor();
            }


            Console.WriteLine("Follow these steps for scalar multiplication \n");

            Matrix matrixFive = CreateMatrix();
            matrixFive.Print();

            double factor = ScalarFactor();
            Matrix tempThree = factor * matrixFive;
            Console.WriteLine("The product matrix");
            tempThree.Print();


            Console.WriteLine("Input a new matrix and get the inverse one of it \n");

            Matrix matrixSix = CreateMatrix();
            matrixSix.Print();

            try
            {
                Matrix tempFour = matrixSix.Inverse();
                Console.WriteLine("The inverse matrix");
                tempFour.Print();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message + '\n');
                Console.ResetColor();
            }


            Console.WriteLine("Input a new matrix and get the transpose one of it \n");

            Matrix matrixSeven = CreateMatrix();
            matrixSeven.Print();

            Matrix tempFive = matrixSix.Transpose();
            Console.WriteLine("The transpose matrix");
            tempFive.Print();

            Console.WriteLine("Input a new matrix and check if it is orthogonal \n");

            Matrix matrixEight = CreateMatrix();
            matrixEight.Print();

            Console.WriteLine("The matrix is " + ((matrixEight.IsOrthogonal()) ? "" : "not") + " orthogonal");

            Console.WriteLine("Input a 3d matrix ( Matrix that has 3 rows ) and do some transforations with it \n");

            Matrix matrixNine = CreateMatrix();
            matrixNine.Print();

            matrixNine.Translate3D(1, 2, 3);
            Console.WriteLine("The matrix after translation 1 point across x-axis, 2 point across y-axis, 3 point across z-axis");
            matrixNine.Print();

            matrixNine.Scale3D(2, 3, 6);
            Console.WriteLine("The matrix after scaling 2 times across x-axis, 4 times across y-axis, 6 times across z-axis");
            matrixNine.Print();

            matrixNine.Rotate3D(90, 45, 0);
            Console.WriteLine("The matrix after rotation 90 degres across x-axis, 45 degres y-axis, 0 degres across z-axis");
            matrixNine.Print();

            Console.WriteLine("Input a new matrix and get the largest and the smallest elements of it \n");

            Matrix matrixTen = CreateMatrix();
            matrixTen.Print();

            Console.WriteLine("The largest element of the matrix is {0}", matrixTen.GetLargest());
            Console.WriteLine("The largest element of the matrix is {0}", matrixTen.GetSmallest());

            Console.WriteLine("Bye :) \n");
        }

        /// <summary>
        /// User enters dimensions of the matrix, and creates it randomly or manual.
        /// </summary>
        /// <returns>Returns the matrix</returns>
        public static Matrix CreateMatrix()
        {
            string userInput;

            Console.WriteLine("Please, enter a count of rows of a matrix");
            int rows;
            do
            {
                userInput = Console.ReadLine();
                try
                {
                    rows = int.Parse(userInput);
                    if(rows >= 1)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Please enter a correct value");
                }
                
            }
            while (true);


            Console.WriteLine("Please, enter a count of columns of a matrix");
            int columns;
            do
            {
                userInput = Console.ReadLine();
                try
                {
                    columns = int.Parse(userInput);
                    if (columns >= 1)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a correct value");
                }

            }
            while (true);

            Console.WriteLine("If you want to generate the matrix randomly, enter 'Y' or 'N' for manual input");
            do
            {
                userInput = Console.ReadLine();
                if(userInput == "Y")
                {
                    return new Matrix(rows, columns);
                }
                else if(userInput == "N")
                {
                    double[,] temporary = new double[rows, columns];
                    for(int i = 0; i < rows; i++)
                    {
                        for(int j = 0; j < columns; j++)
                        {
                            do
                            {
                                Console.Write("M[{0}, {1}] = ", i, j);
                                userInput = Console.ReadLine();
                                try
                                {
                                    temporary[i, j] = double.Parse(userInput);
                                    break;
                                }
                                catch(Exception)
                                {
                                    Console.WriteLine("Please, enter a correct value");
                                }
                            }
                            while (true);
                        }
                    }
                    return new Matrix(temporary);
                }
                Console.WriteLine("Please, enter a correct value");
            }
            while (true);
        }


        /// <summary>
        /// User enters the factor, the he wants to multyply.
        /// </summary>
        /// <returns>returns the factor.</returns>
        static double ScalarFactor()
        {
            string userInput;
            Console.WriteLine("Please, enter a factor for scalar multiplication");
            do
            {
                userInput = Console.ReadLine();
                try
                {
                    double factor = double.Parse(userInput);
                    return factor;
                }
                catch(Exception)
                {
                    Console.WriteLine("Please, enter a correct value");
                }

            }
            while (true);
        }
    }
}
