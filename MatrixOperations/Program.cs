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
            Matrix matrixOne = CreateMatrix("One");
            Matrix matrixTwo = CreateMatrix("Two");

            try
            {
                Matrix tempOne = matrixOne + matrixTwo;
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            Matrix matrixThree = CreateMatrix("Three");
            Matrix matrixFour = CreateMatrix("Four");

            try
            {
                Matrix tempTwo = matrixThree * matrixFour;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            Matrix matrixFive = CreateMatrix("Five");
            double factor = ScalarFactor();
            Matrix tempThree = factor * matrixFive;



        }

        static Matrix CreateMatrix(string name)
        {
            string userInput;

            Console.WriteLine("Please, enter a count of rows of a matrix{0}", name);
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


            Console.WriteLine("Please, enter a count of columns of a matrix{0}", name);
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
