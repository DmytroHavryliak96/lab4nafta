using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] input = new int[][] { new int[] {11, 1, 0, 0, 0}, 
                                          new int[] {1, 12, 1, 0, 0}, 
                                          new int[] {0, 1, 13, 1, 0}, 
                                          new int[] {0, 0, 1, 14, 1}, 
                                          new int[] {0, 0, 0, 1, 12}
                                        };
            int[] inputB = new int[] { 1, -1, 2, -3, 2 };
            GaussSeidel seidel = new GaussSeidel(input, inputB);
            seidel.CalculateSystem();
            seidel.ShowVectors();
            seidel.CheckFinalVector();
            Console.ReadLine();
        }
    }
}
