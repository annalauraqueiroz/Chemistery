using Microsoft.OpenApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace webapi.Utilities
{
    public class ChemicalEquation
    {

        public string Structure { get; set; }
        public string[] Sides { get; set; }
        public string[] Reactants { get; set; }
        public string[] Products { get; set; }
        public double[] Coefficients { get; set; }
        List<Substance> Substances = new List<Substance>();
        List<string> DistinctElements = new List<string>();

        public ChemicalEquation()
        {

        }
        public ChemicalEquation(string structure)
        {
            Structure = structure;
            Sides = structure.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
            Reactants = Sides[0].Split(" + ");
            Products = Sides[1].Split(" + ");

            foreach (string reactant in Reactants)
            {
                Substances.Add(new Substance(reactant));

            }
            foreach (string product in Products)
            {
                Substances.Add(new Substance(product));
            }
        }
        public string BuildEquation()
        {
            string equation = "";

            for (int i = 0; i < Reactants.Length; i++)
            {
                equation += Coefficients[i] + Reactants[i];
                if (i < Reactants.Length - 1)
                    equation += " + ";
            }
            equation += " -> ";
            for (int i = 0; i < Products.Length; i++)
            {
                equation += Coefficients[i] + Products[i];
                if (i < Products.Length - 1)
                    equation += " + ";
            }
            return equation;
        }
        public List<Dictionary<string, int>> GetDistinctElements()
        {
            List<Dictionary<string, int>> list = new List<Dictionary<string, int>>();
            List<string> elements = new List<string>(); //revisar

            foreach (Substance composto in Substances)
            {
                list.Add(composto.Elements);
            }

            foreach (Dictionary<string, int> l in list)
            {
                elements.AddRange(l.Keys.ToArray()); //lista de elementos nos compostos

            }
            DistinctElements = new List<string>(elements.Distinct().ToList());

            return list;
        }
        public string Balance()
        {

            List<Dictionary<string, int>> lista = new(GetDistinctElements());


            int count = DistinctElements.Count();
            int countSubstances = Substances.Count();
            double[,] matrizComElementos = new double[countSubstances, countSubstances];
            double[] vetorElemento = new double[countSubstances];

            for (int k = 0; k < countSubstances; k++)
                vetorElemento[k] = 0;

            for (int i = 0; i < countSubstances; i++)
            {
                for (int j = 0; j < countSubstances; j++)
                {
                    if (i < count)
                    {
                        if (lista[j].ContainsKey(DistinctElements[i]))
                        {
                            matrizComElementos[i, j] = lista[j].GetValueOrDefault(DistinctElements[i]);
                            continue;
                        }
                    }
                    matrizComElementos[i, j] = 0;

                }
            }

            Matrix matriz = new Matrix(matrizComElementos, vetorElemento);
            matriz.GaussianElimination(matrizComElementos);

            Coefficients = matriz.GetCoefficients(countSubstances);

            double[,] matrix = new double[,] { { 1, 0, 1, 0 }, { 0, 2, 3, 0 } };

            return BuildEquation();

        }
        public static double[] GaussianElimination(double[,] matrix)
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

            double[] coefficients = new double[cols];
            for (int i = cols - 1; i >= 0; i--)
            {
                double sum = matrix[i, cols - 1];
                for (int j = i + 1; j < cols - 1; j++)
                {
                    sum -= matrix[i, j] * coefficients[j];
                }
                coefficients[i] = sum / matrix[i, i];
            }

            return coefficients;
        }
    }
}
