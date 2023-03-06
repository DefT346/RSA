using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class Matrix
    {
        private BigInteger[,] array;

        public Matrix(BigInteger[,] array)
        {
            this.array = array;
        }

        public BigInteger[] Column(int index)
        {
            int rows = array.GetLength(0);
            BigInteger[] col = new BigInteger[rows];           
            for (int i = 0; i < rows; i++)
            {
                col[i] = array[index, i];
            }
            return col;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            int rA = A.array.GetLength(0);
            int cA = A.array.GetLength(1);
            int rB = B.array.GetLength(0);
            int cB = B.array.GetLength(1);

            if (cA != rB)
            {
                Console.WriteLine("Matrixes can't be multiplied!!");
                return new Matrix(new BigInteger[,] { { }, { } });
            }
            else
            {
                BigInteger temp = 0;
                BigInteger[,] kHasil = new BigInteger[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A.array[i, k] * B.array[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }

                return new Matrix(kHasil);
            }
        }
    }
}
