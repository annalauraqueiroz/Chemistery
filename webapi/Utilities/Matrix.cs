namespace webapi.Utilities
{
    public class Matrix
    {

        public double[,] MatrixA { get; set; }
        public double[] B { get; set; }
        private double[,] L;
        public double[,] U;

        public Matrix()
        {

        }

        public Matrix(double[,] a, double[] b)
        {
            this.MatrixA = a;
            this.B = b;
        }
        public double[] GetCoefficients(int t)
        {
            double[] coefficients = new double[t];
            double[] column = new double[U.GetLength(1)];
            int n = coefficients.Length;
            double sum = 0;
            double divisao;
            int j;

            for (int k = 0; k < U.GetLength(0); k++)
            {
                column[k] = U[k, U.GetLength(0) - 1];
                coefficients[k] = 1;

            }

            for (int i = n - 1; i >= 0; i--)
            {
                for (j = 0; j < n; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    sum += U[i, j] * coefficients[j];
                }
                divisao = sum / (U[i, i]);
                if (double.IsNaN(divisao) || double.IsInfinity(divisao))
                {
                    divisao = 1;
                }
                if (divisao < 0)
                {
                    divisao *= -1;
                }

                coefficients[i] = divisao;
                sum = 0;
            }

            return coefficients;

        }

        public void GaussianElimination(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int pivotRow = 0;
            int pivotCol = 0;

            while (pivotRow < rows && pivotCol < cols)
            {
                int maxRowIndex = pivotRow;
                double maxValue = matrix[maxRowIndex, pivotCol];

                for (int i = pivotRow + 1; i < rows; i++)
                {
                    if (Math.Abs(matrix[i, pivotCol]) > Math.Abs(maxValue))
                    {
                        maxRowIndex = i;
                        maxValue = matrix[i, pivotCol];
                    }
                }

                if (maxValue == 0)
                {
                    pivotCol++;
                    continue;
                }

                if (maxRowIndex != pivotRow)
                {
                    for (int j = pivotCol; j < cols; j++)
                    {
                        double temp = matrix[pivotRow, j];
                        matrix[pivotRow, j] = matrix[maxRowIndex, j];
                        matrix[maxRowIndex, j] = temp;
                    }
                }

                for (int i = pivotRow + 1; i < rows; i++)
                {
                    double factor = matrix[i, pivotCol] / matrix[pivotRow, pivotCol];
                    for (int j = pivotCol; j < cols; j++)
                    {
                        matrix[i, j] -= factor * matrix[pivotRow, j];
                    }
                }

                pivotRow++;
                pivotCol++;
            }

            U = matrix;
        }
    }
}
