using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class GaussSeidel
    {
        public int NumOfArguements { get; set; }
        private int[][] Matrix;
        private int rows;
        private int columns;
        private int[] b;
        private const double error = 0.01;

        // збіжність
        List<double> M;
        List<double> r;
        List<double> s;

        // результат
        List<double[]> xList;
        public GaussSeidel(int[][] matrix, int[] b)
        {
            NumOfArguements = matrix.GetUpperBound(0) + 1;
            rows = columns = NumOfArguements;
            Matrix = matrix;
            this.b = b;

            M = new List<double>();
            r = new List<double>();
            s = new List<double>();

            Random rand = new Random();

            xList = new List<double[]>();

            double[] xk1 = new double[NumOfArguements];
            for (int i = 0; i < NumOfArguements; i++)
                xk1[i] = rand.Next(0, 5);

            xList.Add(xk1);

            if (!CheckMatrix())
                throw new Exception("enter new natrix");
        }

        public bool CheckMatrix()
        {
            if (!CheckZeroes())
            {
                return false;
                throw new Exception("The elements on the main diagonal must not be equal zero");
            }
            if (!CheckDiagonal())
            {
                return false;
                throw new Exception("The matrix doesn't have the specified main diagonal");
            }

            if (!CheckConvergence())
            {
                return false;
                throw new Exception("The method doesn't have convergence for this matrix");
            }

            return true;

        }

        private bool CheckZeroes()
        {
            for(int i = 0; i < rows; i++)
            {
                if (Matrix[i][i] == 0)
                    return false;
            }
            return true;
        }

        private bool CheckDiagonal()
        { 
            for(int i = 0; i < rows; i++)
            {
                int sum = 0;
                for (int j = 0; j < columns; j++)
                {
                    if (i != j)
                        sum += Math.Abs(Matrix[i][j]);
                }
                if (Math.Abs(Matrix[i][i]) <= sum)
                    return false; 
            }
            return true;
        }

        private bool CheckConvergence()
        {
            for(int i = 0; i < rows; i++)
            {
                double sumR = 0;
                for (int j = i + 1; j < columns; j++)
                    sumR += Math.Abs((double)Matrix[i][j] / Matrix[i][i]);
                r.Add(sumR);
            }

            for (int i = 0; i < rows; i++)
            {
                double sumS = 0;
                for (int j = 0; j < i; j++)
                    sumS += Math.Abs((double)Matrix[i][j] / Matrix[i][i]);
                s.Add(sumS);
            }

            for(int i = 0; i < rows; i++)
            {
                double m = Math.Abs(r[i] / (1 - s[i]));
                M.Add(m);
            }

            if (M.Max() < 1.00)
                return true;

            else
                return false;
            
        }

        public void CalculateSystem()
        {
            CalculateIteration();
            
            while (!CurrentError())
            {
                CalculateIteration();
                

            }
            
        }

        private void CalculateIteration()
        {
            double[] xk = xList.Last();
            double[] xkNew = new double[xk.Length];

            for (int i  = 0; i < rows; i++){
                double sumTemp = 0.0;
                for (int j = 0; j < columns; j++)
                {
                    if (i != j)
                        sumTemp += Matrix[i][j] * xk[j];
                }

                 xkNew[i] = -1 * (sumTemp - b[i]) / Matrix[i][i]; 
                
            }
            xList.Add(xkNew);
        }

        private bool CurrentError()
        {
            double[] xkLast = xList.Last();
            double[] xkPrevious = xList[xList.Count - 2];
            double[] xDifference = new double[NumOfArguements];

            for (int i = 0; i < NumOfArguements; i++)
                xDifference[i] = Math.Abs(xkLast[i] - xkPrevious[i]);

            if (xDifference.Max() <= error)
                return true;
            else return false;

        }

        public void ShowVectors()
        {
            for (int i = 0; i < xList.Count; i++) {
                Console.Write($"k = {i,-2}");
                for (int j = 0; j < NumOfArguements; j++)
                {
                    Console.Write($"  x{j + 1} = {xList[i][j],-19}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void CheckFinalVector()
        {
            double[] vector = xList.Last();
            double[] results = new double[NumOfArguements];

            for(int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for(int j = 0; j < columns; j++)
                    sum += Matrix[i][j] * vector[j];
                results[i] = sum;
                Console.WriteLine($" equation {i + 1}: calculated result = {results[i]}, accurate result b[{i + 1}] = {b[i]}");
            }
        }
    }
}
